using System;
using System.Collections.Generic;
using System.Net.Http;
using Steamworks;
using UnityEngine;

public class SteamLeaderboardsUpload
{
	private static readonly string LEADERBOARD_NAME_FIELD = "leaderboard_name";

	private static readonly string LEADERBOARD_UNBREAKING_NAME_FIELD = "leaderboard_unbreaking_name";

	private static readonly string LEADERBOARD_STRESS_NAME_FIELD = "leaderboard_stress_name";

	private static readonly string SCORE_FIELD = "score";

	private static readonly string PHYSICS_VERSION = "physics_version";

	private static readonly string STRESS_SCORE_FIELD = "stress_score";

	private static readonly string DID_BREAK = "did_break";

	public static async void UploadLeaderboardScore(string levelId, int score, float maxStressNormalized, bool didBreak, bool underBudget, BridgeSaveData bridgeSaveData, LeaderboardsFilter filter, Action<bool> callback)
	{
		Dictionary<string, string> values = GenerateUploadValues(levelId, score, maxStressNormalized, didBreak, underBudget);
		if (!values.ContainsKey(LEADERBOARD_NAME_FIELD) && !values.ContainsKey(LEADERBOARD_UNBREAKING_NAME_FIELD) && !values.ContainsKey(LEADERBOARD_STRESS_NAME_FIELD))
		{
			callback?.Invoke(obj: false);
			return;
		}
		if (!SteamManager.HasAuthTicket())
		{
			try
			{
				AuthTicket authTicket = await SteamUser.GetAuthSessionTicketAsync();
				if (authTicket == null)
				{
					callback?.Invoke(obj: false);
					return;
				}
				SteamManager.RegisterTicket(authTicket);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Caught exception trying to get AuthSessionTicket: " + ex.Message);
				callback?.Invoke(obj: false);
				return;
			}
		}
		if (string.IsNullOrEmpty(SteamManager.GetTicket()))
		{
			callback?.Invoke(obj: false);
			return;
		}
		if (values.ContainsKey(LEADERBOARD_NAME_FIELD))
		{
			SteamLeaderboardUploadScoreCache.CacheScore(GameLeaderboards.GetKey(levelId), score);
			SteamLeaderboardScoresCache.ClearScores(levelId);
		}
		if (values.ContainsKey(LEADERBOARD_UNBREAKING_NAME_FIELD))
		{
			SteamLeaderboardUploadScoreCache.CacheScore(GameLeaderboards.GetUnbreakingKey(levelId), score);
			SteamLeaderboardScoresCache.ClearScoresUnbreaking(levelId);
		}
		if (values.ContainsKey(LEADERBOARD_STRESS_NAME_FIELD))
		{
			string stressKey = GameLeaderboards.GetStressKey(levelId);
			int score2 = GameLeaderboards.ConvertStressToScore(maxStressNormalized);
			SteamLeaderboardUploadScoreCache.CacheScore(stressKey, score2);
			SteamLeaderboardScoresCache.ClearScoresStress(levelId);
		}
		MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
		byte[] array = bridgeSaveData.SerializeBinary();
		multipartFormDataContent.Add(new ByteArrayContent(array, 0, array.Length), "save", Utils.GenerateUniqueId());
		foreach (KeyValuePair<string, string> item in values)
		{
			multipartFormDataContent.Add(new StringContent(item.Value), item.Key);
		}
		multipartFormDataContent.Add(new StringContent(levelId), "levelid");
		multipartFormDataContent.Add(new StringContent(Game.GetLevelCheckssum(levelId)), "level_checksum");
		multipartFormDataContent.Add(new StringContent(filter.ToString()), "filter");
		multipartFormDataContent.Add(new StringContent(SteamUtils.GetSteamId()), "steamid");
		try
		{
			HttpResponseMessage httpResponseMessage = await Game.m_HttpClient.PostAsync(Game.LEADERBOARD_UPLOAD_URL, multipartFormDataContent);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				string text = await httpResponseMessage.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(text))
				{
					Debug.Log(text);
				}
				callback?.Invoke(obj: true);
			}
			else
			{
				callback?.Invoke(obj: false);
			}
		}
		catch (Exception ex2)
		{
			Debug.Log("Exception '" + ex2.Message + "' when trying to upload score.");
			callback?.Invoke(obj: false);
		}
	}

	private static Dictionary<string, string> GenerateUploadValues(string levelId, int score, float maxStressNormalized, bool didBreak, bool underBudget)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		string key = GameLeaderboards.GetKey(levelId);
		if (ScoreIsBetterThanCached(key, score))
		{
			dictionary.Add(LEADERBOARD_NAME_FIELD, key);
		}
		dictionary.Add(SCORE_FIELD, score.ToString());
		if (didBreak)
		{
			dictionary.Add(DID_BREAK, "1");
		}
		if (!didBreak)
		{
			string unbreakingKey = GameLeaderboards.GetUnbreakingKey(levelId);
			if (ScoreIsBetterThanCached(unbreakingKey, score))
			{
				dictionary.Add(LEADERBOARD_UNBREAKING_NAME_FIELD, unbreakingKey);
			}
			if (!dictionary.ContainsKey(SCORE_FIELD))
			{
				dictionary.Add(SCORE_FIELD, score.ToString());
			}
		}
		int num = GameLeaderboards.ConvertStressToScore(maxStressNormalized);
		if (!didBreak)
		{
			num = Mathf.Clamp(num, 0, 9999);
		}
		if (underBudget && num < 10000)
		{
			string stressKey = GameLeaderboards.GetStressKey(levelId);
			if (ScoreIsBetterThanCached(stressKey, num))
			{
				dictionary.Add(LEADERBOARD_STRESS_NAME_FIELD, stressKey);
			}
			dictionary.Add(STRESS_SCORE_FIELD, num.ToString());
		}
		dictionary.Add(PHYSICS_VERSION, GameManager.GetPhysicsEngineVersion().ToString());
		return dictionary;
	}

	private static bool ScoreIsBetterThanCached(string leaderboardKey, int score)
	{
		SteamLeaderboardUploadScore score2 = SteamLeaderboardUploadScoreCache.GetScore(leaderboardKey);
		if (score2 == null)
		{
			return true;
		}
		return score < score2.m_Score;
	}
}
