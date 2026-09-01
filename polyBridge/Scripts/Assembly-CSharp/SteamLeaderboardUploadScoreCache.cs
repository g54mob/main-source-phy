using System.Collections.Generic;

public class SteamLeaderboardUploadScoreCache
{
	private static Dictionary<string, SteamLeaderboardUploadScore> m_Cache = new Dictionary<string, SteamLeaderboardUploadScore>();

	public static SteamLeaderboardUploadScore GetScore(string key)
	{
		if (m_Cache.ContainsKey(key) && m_Cache[key] != null)
		{
			return m_Cache[key];
		}
		return null;
	}

	public static void CacheScore(string key, int score)
	{
		if (m_Cache.ContainsKey(key))
		{
			m_Cache[key] = new SteamLeaderboardUploadScore(score);
		}
		else
		{
			m_Cache.Add(key, new SteamLeaderboardUploadScore(score));
		}
	}
}
