namespace SleepyNodes
{
	[CreateNodeMenu("Events/Mission Failed")]
	[NodeWidth(400)]
	[NodeName("[Event] Mission Failed")]
	public class Event_OnMissionFailed : EventNode
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
