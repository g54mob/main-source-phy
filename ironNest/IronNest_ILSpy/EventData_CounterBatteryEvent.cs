using SleepyNodes;

public class EventData_CounterBatteryEvent : EventNode.EventData
{
	public enum EventTypes
	{
		Any,
		Started,
		Paused,
		Unpaused,
		TimeAdded,
		TimeSubtracted,
		Expired
	}

	public EventTypes EventType;
}
