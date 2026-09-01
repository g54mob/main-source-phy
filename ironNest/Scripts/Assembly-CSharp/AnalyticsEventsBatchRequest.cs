using System.Collections.Generic;

public class AnalyticsEventsBatchRequest
{
	public List<AnalyticsEventRequest_Boot> BootEvents { get; set; }

	public List<AnalyticsEventRequest_Mission> MissionEvents { get; set; }

	public List<AnalyticsEventRequest_Generic> GenericEvents { get; set; }
}
