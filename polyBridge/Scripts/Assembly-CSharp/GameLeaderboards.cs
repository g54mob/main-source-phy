using Steamworks.Data;
using UnityEngine;

public class GameLeaderboards
{
	public static readonly ulong INVALID_PUBLISHED_FIELD_ID = ulong.MaxValue;

	public static readonly int MIN_SCORES_FOR_HISTOGRAM = 10;

	public static readonly int NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE = 100;

	public static int NUM_AROUND_SCORES_ABOVE_DISPLAYED_LEVEL_COMPLETE = 80;

	public static int NUM_AROUND_SCORES_BELOW_DISPLAYED_LEVEL_COMPLETE = 19;

	public static readonly int NUM_TOP_SCORES_DISPLAYED = 100;

	public static readonly int NUM_AROUND_SCORES_ABOVE_DISPLAYED = 80;

	public static readonly int NUM_AROUND_SCORES_BELOW_DISPLAYED = 19;

	public static GameLeaderboardEntry[] CreateGameLeaderboardEntries(LeaderboardEntry[] steamEntries)
	{
		if (steamEntries == null || steamEntries.Length == 0)
		{
			return null;
		}
		GameLeaderboardEntry[] array = new GameLeaderboardEntry[steamEntries.Length];
		for (int i = 0; i < steamEntries.Length; i++)
		{
			array[i] = new GameLeaderboardEntry(steamEntries[i]);
		}
		return array;
	}

	public static void UploadScoreComplete(bool uploaded)
	{
		GameUI.m_Instance.m_LevelComplete.m_LeaderboardPanel.ForceRefresh();
	}

	public static bool CurrentLevelAllowsLeaderboards()
	{
		switch (GameManager.GetGameMode())
		{
		case GameMode.CAMPAIGN:
			return !Game.IsCurrentLevelTutorial();
		case GameMode.WORKSHOP:
			if (Workshop.m_LastPlayedWorkshopItem != null)
			{
				return WeeklyChallenges.IsAWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId());
			}
			return false;
		default:
			return false;
		}
	}

	public static bool CurrentLevelAllowsUploadToLeaderboards()
	{
		switch (GameManager.GetGameMode())
		{
		case GameMode.CAMPAIGN:
			return true;
		case GameMode.WORKSHOP:
			if (Workshop.m_LastPlayedWorkshopItem != null)
			{
				return WeeklyChallenges.IsALiveWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId());
			}
			return false;
		default:
			return false;
		}
	}

	public static int ComputePlayerPercentile(GameLeaderboard topsScoresLeaderboard, GameLeaderboardEntry[] topScores)
	{
		int num = -1;
		string steamId = SteamUtils.GetSteamId();
		foreach (GameLeaderboardEntry gameLeaderboardEntry in topScores)
		{
			if (gameLeaderboardEntry.GetId() == steamId)
			{
				num = gameLeaderboardEntry.GetGlobalRank();
				break;
			}
		}
		return num switch
		{
			-1 => 0, 
			1 => 100, 
			_ => ComputePercentileFromRank(num, topsScoresLeaderboard.GetEntryCount()), 
		};
	}

	public static int ComputePercentileFromRank(int playerRank, int numRanks)
	{
		int num = numRanks - playerRank;
		return Mathf.FloorToInt(100f * (float)num / (float)numRanks);
	}

	public static string FormatScore(int score, LeaderboardFilterState filter)
	{
		if (filter.m_LeaderboardsFilter == LeaderboardsFilter.LOWEST_STRESS)
		{
			return Utils.FormatPercentageToTwoDecimalPlaces(ConvertScoreToStress(score));
		}
		return Utils.FormatCash(score);
	}

	public static string FormatRank(int rank)
	{
		if (rank > 0)
		{
			return Utils.FormatIntegerWithCommas(rank);
		}
		return string.Empty;
	}

	public static float ConvertScoreToStress(int score)
	{
		return (float)score / 10000f;
	}

	public static string GetKey(string levelId)
	{
		return levelId ?? "";
	}

	public static string GetUnbreakingKey(string levelId)
	{
		return levelId + "_unbreaking";
	}

	public static string GetStressKey(string levelId)
	{
		return levelId + "_stress";
	}

	public static int ConvertStressToScore(float stressNormalized)
	{
		return Mathf.FloorToInt(stressNormalized * 100f * 100f);
	}

	private static string FormatHeader(int percentile)
	{
		return string.Format(Localize.Get("UI_TOP_PERCENT"), percentile);
	}

	public static int TryGetBestPlayerScore(LeaderboardFilterState filterState)
	{
		LeaderboardFilterState leaderboardFilterState = new LeaderboardFilterState();
		leaderboardFilterState.CopyFrom(filterState);
		leaderboardFilterState.m_LeaderboardsView = LeaderboardsView.AROUND_YOU;
		LeaderboardEntry[] scores = SteamLeaderboardScoresCache.GetScores(leaderboardFilterState);
		if (scores == null)
		{
			leaderboardFilterState.m_LeaderboardsView = LeaderboardsView.FRIENDS;
			scores = SteamLeaderboardScoresCache.GetScores(leaderboardFilterState);
		}
		if (scores == null)
		{
			return 0;
		}
		ulong steamIdAsUlong = SteamUtils.GetSteamIdAsUlong();
		LeaderboardEntry[] array = scores;
		for (int i = 0; i < array.Length; i++)
		{
			LeaderboardEntry leaderboardEntry = array[i];
			if (steamIdAsUlong == (ulong)leaderboardEntry.User.Id)
			{
				return leaderboardEntry.Score;
			}
		}
		return 0;
	}

	public static int TryGetBestPlayerScorePercentile(LeaderboardFilterState filterState)
	{
		LeaderboardFilterState leaderboardFilterState = new LeaderboardFilterState();
		leaderboardFilterState.CopyFrom(filterState);
		leaderboardFilterState.m_LeaderboardsView = LeaderboardsView.AROUND_YOU;
		LeaderboardEntry[] scores = SteamLeaderboardScoresCache.GetScores(leaderboardFilterState);
		if (scores == null)
		{
			leaderboardFilterState.m_LeaderboardsView = LeaderboardsView.FRIENDS;
			scores = SteamLeaderboardScoresCache.GetScores(leaderboardFilterState);
		}
		if (scores == null)
		{
			return -1;
		}
		ulong steamIdAsUlong = SteamUtils.GetSteamIdAsUlong();
		LeaderboardEntry[] array = scores;
		for (int i = 0; i < array.Length; i++)
		{
			LeaderboardEntry leaderboardEntry = array[i];
			if (steamIdAsUlong == (ulong)leaderboardEntry.User.Id)
			{
				string leaderboardKey = SteamLeaderboards.GetLeaderboardKey(filterState.m_LevelId, filterState.m_LeaderboardsFilter);
				if (string.IsNullOrEmpty(leaderboardKey) || !SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey))
				{
					return -1;
				}
				if (leaderboardEntry.GlobalRank == 1)
				{
					return 100;
				}
				return ComputePercentileFromRank(leaderboardEntry.GlobalRank, SteamLeaderboards.m_Leaderboards[leaderboardKey].EntryCount);
			}
		}
		return -1;
	}

	public static string GetLeadboardKey(LeaderboardFilterState filter)
	{
		if (filter.m_LeaderboardsFilter == LeaderboardsFilter.UNBREAKING)
		{
			return GetUnbreakingKey(filter.m_LevelId);
		}
		if (filter.m_LeaderboardsFilter == LeaderboardsFilter.LOWEST_STRESS)
		{
			return GetStressKey(filter.m_LevelId);
		}
		return GetKey(filter.m_LevelId);
	}
}
