using System;

public class AnalyticsEventRequest_Boot
{
	public string EventType { get; set; }

	public string DeviceId { get; set; }

	public Guid UserId { get; set; }

	public long? SteamId { get; set; }

	public long? GogId { get; set; }

	public DateTime CreatedAtUtc { get; set; }
}
