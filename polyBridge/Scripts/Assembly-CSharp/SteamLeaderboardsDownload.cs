using System;
using System.Threading.Tasks;
using Steamworks;
using Steamworks.Data;

public class SteamLeaderboardsDownload
{
	public static async void DownloadLeaderboard(string levelId, int delayMilliseconds, int topCount, int aboveCount, int belowCount, LeaderboardFilterState filter, Action<GameLeaderboard, GameLeaderboardEntry[]> callback)
	{
		if (!SteamManager.IsLoggedOn())
		{
			callback?.Invoke(null, null);
			return;
		}
		string leaderboardKey = GameLeaderboards.GetLeadboardKey(filter);
		LeaderboardEntry[] scores = SteamLeaderboardScoresCache.GetScores(filter);
		if (scores != null && SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey))
		{
			if (callback != null)
			{
				GameLeaderboard arg = new GameLeaderboard(SteamLeaderboards.m_Leaderboards[leaderboardKey]);
				GameLeaderboardEntry[] arg2 = GameLeaderboards.CreateGameLeaderboardEntries(scores);
				callback?.Invoke(arg, arg2);
			}
			return;
		}
		string key = GameLeaderboards.GetKey(levelId);
		if (!SteamLeaderboards.m_Leaderboards.ContainsKey(key))
		{
			Leaderboard? leaderboard = await SteamUserStats.FindLeaderboardAsync(key);
			if (leaderboard.HasValue && !SteamLeaderboards.m_Leaderboards.ContainsKey(key))
			{
				SteamLeaderboards.m_Leaderboards.Add(key, leaderboard.Value);
			}
		}
		string unbreakingKey = GameLeaderboards.GetUnbreakingKey(levelId);
		if (!SteamLeaderboards.m_Leaderboards.ContainsKey(unbreakingKey))
		{
			Leaderboard? leaderboard2 = await SteamUserStats.FindLeaderboardAsync(unbreakingKey);
			if (leaderboard2.HasValue && !SteamLeaderboards.m_Leaderboards.ContainsKey(unbreakingKey))
			{
				SteamLeaderboards.m_Leaderboards.Add(unbreakingKey, leaderboard2.Value);
			}
		}
		string stressKey = GameLeaderboards.GetStressKey(levelId);
		if (!SteamLeaderboards.m_Leaderboards.ContainsKey(stressKey))
		{
			Leaderboard? leaderboard3 = await SteamUserStats.FindLeaderboardAsync(stressKey);
			if (leaderboard3.HasValue && !SteamLeaderboards.m_Leaderboards.ContainsKey(stressKey))
			{
				SteamLeaderboards.m_Leaderboards.Add(stressKey, leaderboard3.Value);
			}
		}
		await Task.Delay(delayMilliseconds);
		LeaderboardEntry[] array2;
		if (filter.m_LeaderboardsView == LeaderboardsView.FRIENDS)
		{
			LeaderboardEntry[] array = ((!SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey)) ? null : (await SteamLeaderboards.m_Leaderboards[leaderboardKey].GetScoresFromFriendsAsync()));
			array2 = array;
		}
		else if (filter.m_LeaderboardsView == LeaderboardsView.TOP_SCORES)
		{
			LeaderboardEntry[] array = ((!SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey)) ? null : (await SteamLeaderboards.m_Leaderboards[leaderboardKey].GetScoresAsync(topCount)));
			array2 = array;
		}
		else
		{
			LeaderboardEntry[] array = ((!SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey)) ? null : (await SteamLeaderboards.m_Leaderboards[leaderboardKey].GetScoresAroundUserAsync(-aboveCount, belowCount)));
			array2 = array;
			if (array2 == null || array2.Length == 0)
			{
				array = ((!SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey)) ? null : (await SteamLeaderboards.m_Leaderboards[leaderboardKey].GetScoresAsync(topCount)));
				array2 = array;
			}
		}
		if (array2 != null)
		{
			SteamLeaderboardScoresCache.CacheScores(array2, filter);
		}
		if (SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey))
		{
			GameLeaderboard arg3 = new GameLeaderboard(SteamLeaderboards.m_Leaderboards[leaderboardKey]);
			GameLeaderboardEntry[] arg4 = GameLeaderboards.CreateGameLeaderboardEntries(array2);
			callback?.Invoke(arg3, arg4);
		}
		else
		{
			callback?.Invoke(null, null);
		}
	}
}
