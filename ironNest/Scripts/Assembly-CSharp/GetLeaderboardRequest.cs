using System;

public class GetLeaderboardRequest
{
	public int Amount { get; set; }

	public Guid UserId { get; set; }

	public Gamemodes Gamemode { get; set; }

	public LeaderboardPeriod Period { get; set; }

	public DateTime? DayUtc { get; set; }
}
