using System.Collections.Generic;
using Steamworks.Data;
using UnityEngine;

public class SteamLeaderboardScoresCache
{
	private static Dictionary<string, SteamLeaderboardScores> m_Cache = new Dictionary<string, SteamLeaderboardScores>();

	private static readonly string AROUND_YOU_POSTFIX = "_around";

	private static readonly string FRIENDS_POSTFIX = "_friends";

	private static readonly string TOP_POSTFIX = "_top";

	private static readonly float CACHE_ENTRY_EXPIRE_TIME_SECONDS = 300f;

	public static LeaderboardEntry[] GetScores(LeaderboardFilterState filter)
	{
		string leaderboardKeyFromFilter = GetLeaderboardKeyFromFilter(filter);
		if (m_Cache.ContainsKey(leaderboardKeyFromFilter) && m_Cache[leaderboardKeyFromFilter] != null && Time.realtimeSinceStartup < m_Cache[leaderboardKeyFromFilter].m_ExpireTime)
		{
			return m_Cache[leaderboardKeyFromFilter].m_Scores;
		}
		return null;
	}

	public static void CacheScores(LeaderboardEntry[] scores, LeaderboardFilterState filter)
	{
		string leaderboardKeyFromFilter = GetLeaderboardKeyFromFilter(filter);
		if (m_Cache.ContainsKey(leaderboardKeyFromFilter))
		{
			m_Cache[leaderboardKeyFromFilter] = new SteamLeaderboardScores(scores, Time.realtimeSinceStartup + CACHE_ENTRY_EXPIRE_TIME_SECONDS);
		}
		else
		{
			m_Cache.Add(leaderboardKeyFromFilter, new SteamLeaderboardScores(scores, Time.realtimeSinceStartup + CACHE_ENTRY_EXPIRE_TIME_SECONDS));
		}
	}

	public static void ClearAll()
	{
		m_Cache.Clear();
	}

	public static void ClearScores(string levelId)
	{
		string key = GameLeaderboards.GetKey(levelId);
		ClearScoresAroundYou(key);
		ClearScoresFriends(key);
		ClearScoresTop(key);
	}

	public static void ClearScoresUnbreaking(string levelId)
	{
		string unbreakingKey = GameLeaderboards.GetUnbreakingKey(levelId);
		ClearScoresAroundYou(unbreakingKey);
		ClearScoresFriends(unbreakingKey);
		ClearScoresTop(unbreakingKey);
	}

	public static void ClearScoresStress(string levelId)
	{
		string stressKey = GameLeaderboards.GetStressKey(levelId);
		ClearScoresAroundYou(stressKey);
		ClearScoresFriends(stressKey);
		ClearScoresTop(stressKey);
	}

	private static void ClearScoresAroundYou(string key)
	{
		string key2 = $"{key}{AROUND_YOU_POSTFIX}{GameLeaderboards.NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE}";
		if (m_Cache.ContainsKey(key2))
		{
			m_Cache[key2] = null;
		}
		key2 = $"{key}{AROUND_YOU_POSTFIX}{GameLeaderboards.NUM_TOP_SCORES_DISPLAYED}";
		if (m_Cache.ContainsKey(key2))
		{
			m_Cache[key2] = null;
		}
	}

	private static void ClearScoresFriends(string key)
	{
		string key2 = $"{key}{FRIENDS_POSTFIX}{GameLeaderboards.NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE}";
		if (m_Cache.ContainsKey(key2))
		{
			m_Cache[key2] = null;
		}
		key2 = $"{key}{FRIENDS_POSTFIX}{GameLeaderboards.NUM_TOP_SCORES_DISPLAYED}";
		if (m_Cache.ContainsKey(key2))
		{
			m_Cache[key2] = null;
		}
	}

	private static void ClearScoresTop(string key)
	{
		string key2 = $"{key}{TOP_POSTFIX}{GameLeaderboards.NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE}";
		if (m_Cache.ContainsKey(key2))
		{
			m_Cache[key2] = null;
		}
		key2 = $"{key}{TOP_POSTFIX}{GameLeaderboards.NUM_TOP_SCORES_DISPLAYED}";
		if (m_Cache.ContainsKey(key2))
		{
			m_Cache[key2] = null;
		}
	}

	private static string GetLeaderboardKeyFromFilter(LeaderboardFilterState filter)
	{
		string text = SteamLeaderboards.GetLeaderboardKey(filter.m_LevelId, filter.m_LeaderboardsFilter);
		switch (filter.m_LeaderboardsView)
		{
		case LeaderboardsView.AROUND_YOU:
			text += AROUND_YOU_POSTFIX;
			break;
		case LeaderboardsView.FRIENDS:
			text += FRIENDS_POSTFIX;
			break;
		case LeaderboardsView.TOP_SCORES:
			text += TOP_POSTFIX;
			break;
		}
		return $"{text}{filter.m_TopCount}";
	}
}
