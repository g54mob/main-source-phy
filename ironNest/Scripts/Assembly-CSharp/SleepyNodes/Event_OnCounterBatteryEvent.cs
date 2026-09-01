namespace SleepyNodes
{
	[CreateNodeMenu("Events/Counter Battery Event")]
	[NodeWidth(400)]
	[NodeName("[Event] Counter Battery Event")]
	public class Event_OnCounterBatteryEvent : EventNode
	{
		public EventData_CounterBatteryEvent.EventTypes ListenerType;

		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}
	}
}
