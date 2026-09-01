using SleepyNodes;

public class EventData_CounterBatteryEvent : EventNode.EventData
{
	public enum EventTypes
	{
		Any = 0,
		Started = 1,
		Paused = 2,
		Unpaused = 3,
		TimeAdded = 4,
		TimeSubtracted = 5,
		Expired = 6
	}

	public EventTypes EventType;
}
