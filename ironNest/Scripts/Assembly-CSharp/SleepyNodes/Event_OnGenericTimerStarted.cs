namespace SleepyNodes
{
	[CreateNodeMenu("Events/Generic Timer Started")]
	[NodeWidth(400)]
	[NodeName("[Event] Generic Timer Started")]
	public class Event_OnGenericTimerStarted : EventNode
	{
		public string TimerID;

		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}
	}
}
