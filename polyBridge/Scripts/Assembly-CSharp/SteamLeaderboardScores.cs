using Steamworks.Data;

public class SteamLeaderboardScores
{
	public LeaderboardEntry[] m_Scores;

	public float m_ExpireTime;

	public SteamLeaderboardScores(LeaderboardEntry[] scores, float expireTime)
	{
		m_Scores = scores;
		m_ExpireTime = expireTime;
	}
}
