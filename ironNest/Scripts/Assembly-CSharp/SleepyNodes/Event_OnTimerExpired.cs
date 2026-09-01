namespace SleepyNodes
{
	[CreateNodeMenu("Events/Timer Expired")]
	[NodeWidth(400)]
	[NodeName("[Event] Timer Expired")]
	public class Event_OnTimerExpired : EventNode
	{
		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}
	}
}
