using System;

public class GetSessionKeyRequest
{
	public Guid UserId { get; set; }

	public Gamemodes Gamemode { get; set; }

	public string PerformanceStatsJson { get; set; }
}
