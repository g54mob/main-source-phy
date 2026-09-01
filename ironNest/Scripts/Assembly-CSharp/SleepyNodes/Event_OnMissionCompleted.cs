namespace SleepyNodes
{
	[CreateNodeMenu("Events/Mission Completed")]
	[NodeWidth(400)]
	[NodeName("[Event] Mission Completed")]
	public class Event_OnMissionCompleted : EventNode
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
