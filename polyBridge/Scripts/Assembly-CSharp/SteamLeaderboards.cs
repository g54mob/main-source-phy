using System;
using System.Collections.Generic;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

public class SteamLeaderboards
{
	public static Dictionary<string, Leaderboard> m_Leaderboards = new Dictionary<string, Leaderboard>();

	public static async void CacheLeaderboards(string levelID)
	{
		string key = GameLeaderboards.GetKey(levelID);
		if (!m_Leaderboards.ContainsKey(key))
		{
			Leaderboard? leaderboard = await SteamUserStats.FindLeaderboardAsync(key);
			if (leaderboard.HasValue)
			{
				try
				{
					m_Leaderboards.Add(key, leaderboard.Value);
				}
				catch (Exception arg)
				{
					Debug.LogWarning($"Caught exception '{arg}' adding leaderboard ID {key} to m_Leaderboards");
				}
			}
		}
		string unbreakingKey = GameLeaderboards.GetUnbreakingKey(levelID);
		if (!m_Leaderboards.ContainsKey(unbreakingKey))
		{
			Leaderboard? leaderboard2 = await SteamUserStats.FindLeaderboardAsync(unbreakingKey);
			if (leaderboard2.HasValue)
			{
				try
				{
					m_Leaderboards.Add(unbreakingKey, leaderboard2.Value);
				}
				catch (Exception arg2)
				{
					Debug.LogWarning($"Caught exception '{arg2}' adding leaderboard ID {unbreakingKey} to m_Leaderboards");
				}
			}
		}
		string stressKey = GameLeaderboards.GetStressKey(levelID);
		if (m_Leaderboards.ContainsKey(stressKey))
		{
			return;
		}
		Leaderboard? leaderboard3 = await SteamUserStats.FindLeaderboardAsync(stressKey);
		if (leaderboard3.HasValue)
		{
			try
			{
				m_Leaderboards.Add(stressKey, leaderboard3.Value);
			}
			catch (Exception arg3)
			{
				Debug.LogWarning($"Caught exception '{arg3}' adding leaderboard ID {stressKey} to m_Leaderboards");
			}
		}
	}

	public static int ComputePlayerPercentile(Leaderboard topsScoresLeaderboard, LeaderboardEntry[] topScores)
	{
		int num = -1;
		string steamId = SteamUtils.GetSteamId();
		for (int i = 0; i < topScores.Length; i++)
		{
			LeaderboardEntry leaderboardEntry = topScores[i];
			if (leaderboardEntry.User.Id.ToString() == steamId)
			{
				num = leaderboardEntry.GlobalRank;
				break;
			}
		}
		switch (num)
		{
		case -1:
			return -1;
		case 1:
			return 100;
		default:
		{
			int num2 = topsScoresLeaderboard.EntryCount - num;
			return Mathf.FloorToInt(100f * (float)num2 / (float)topsScoresLeaderboard.EntryCount);
		}
		}
	}

	public static string GetLeaderboardKey(string levelID, LeaderboardsFilter filter)
	{
		switch (filter)
		{
		case LeaderboardsFilter.ALL:
			return GameLeaderboards.GetKey(levelID);
		case LeaderboardsFilter.UNBREAKING:
			return GameLeaderboards.GetUnbreakingKey(levelID);
		case LeaderboardsFilter.LOWEST_STRESS:
			return GameLeaderboards.GetStressKey(levelID);
		default:
			Debug.LogWarning($"Unrecognized LeaderboardsFilter: {filter}");
			return string.Empty;
		}
	}
}
