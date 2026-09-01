namespace SleepyNodes
{
	[CreateNodeMenu("Events/Time Interval")]
	[NodeWidth(400)]
	[NodeName("[Event] Time Interval")]
	public class Event_TimeInterval : EventNode
	{
		public bool TriggerOnStart;

		public float MinSeconds;

		public float MaxSeconds;

		private float NextTrigger;

		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}
	}
}
