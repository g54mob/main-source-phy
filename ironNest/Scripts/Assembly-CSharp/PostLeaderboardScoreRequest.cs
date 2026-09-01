using System;

public class PostLeaderboardScoreRequest
{
	public Guid UserId { get; set; }

	public Guid SessionId { get; set; }

	public bool ClientTampered { get; set; }

	public LeaderboardRunData RunData { get; set; }

	public string ImageExtension { get; set; }

	public string PerformanceStatsJson { get; set; }
}
