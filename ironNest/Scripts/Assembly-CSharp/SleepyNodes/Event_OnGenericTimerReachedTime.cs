namespace SleepyNodes
{
	[CreateNodeMenu("Events/Generic Timer Reached")]
	[NodeWidth(400)]
	[NodeName("[Event] Generic Timer Reached")]
	public class Event_OnGenericTimerReachedTime : EventNode
	{
		public string TimerID;

		public float TimeReached;

		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}
	}
}
