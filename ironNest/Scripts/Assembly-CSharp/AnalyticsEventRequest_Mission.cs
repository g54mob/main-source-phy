using System;

public class AnalyticsEventRequest_Mission
{
	public string EventType { get; set; }

	public string DeviceId { get; set; }

	public Guid UserId { get; set; }

	public string MissionId { get; set; }

	public double Value { get; set; }

	public string Payload { get; set; }

	public DateTime CreatedAtUtc { get; set; }
}
