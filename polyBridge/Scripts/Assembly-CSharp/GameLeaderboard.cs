using Steamworks.Data;

public class GameLeaderboard
{
	public Leaderboard m_SteamLeaderboard;

	public GameLeaderboard(Leaderboard steamLeaderboard)
	{
		m_SteamLeaderboard = steamLeaderboard;
	}

	public int GetEntryCount()
	{
		return m_SteamLeaderboard.EntryCount;
	}
}
