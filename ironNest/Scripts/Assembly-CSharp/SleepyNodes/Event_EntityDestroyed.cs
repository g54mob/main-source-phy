namespace SleepyNodes
{
	[CreateNodeMenu("Events/Entity Destroyed")]
	[NodeWidth(400)]
	[NodeName("[Event] Entity Destroyed")]
	public class Event_EntityDestroyed : EventNode
	{
		public enum LookupTypes
		{
			Any = 0,
			Count = 1,
			All = 2
		}

		public FilterEntitySet EntityFilter;

		public LookupTypes LookupType;

		public int Amount;

		public bool ResetCountAfterTrigger;

		public EntityContextKeys EntityDestroyed;

		private int numberOfEntitiesSeen;

		private MapEntity cachedEntity;

		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}

		public override void Run(NodeExecutionState state)
		{
		}
	}
}
