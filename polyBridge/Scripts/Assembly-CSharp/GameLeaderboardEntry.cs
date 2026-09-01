using Steamworks.Data;

public class GameLeaderboardEntry
{
	private LeaderboardEntry m_SteamLeaderboardEntry;

	public GameLeaderboardEntry(LeaderboardEntry steamLeaderboardEntry)
	{
		m_SteamLeaderboardEntry = steamLeaderboardEntry;
	}

	public string GetId()
	{
		return m_SteamLeaderboardEntry.User.Id.ToString();
	}

	public string GetName()
	{
		return m_SteamLeaderboardEntry.User.Name;
	}

	public int GetScore()
	{
		return m_SteamLeaderboardEntry.Score;
	}

	public int GetGlobalRank()
	{
		return m_SteamLeaderboardEntry.GlobalRank;
	}

	public bool HasBreaks()
	{
		if (m_SteamLeaderboardEntry.Details != null && m_SteamLeaderboardEntry.Details.Length == 1 && (m_SteamLeaderboardEntry.Details[0] & 1) > 0)
		{
			return true;
		}
		return false;
	}
}
