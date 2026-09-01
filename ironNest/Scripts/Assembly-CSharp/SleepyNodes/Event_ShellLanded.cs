namespace SleepyNodes
{
	[CreateNodeMenu("Events/Shell Landed")]
	[NodeWidth(400)]
	[NodeName("[Event] Shell Landed")]
	public class Event_ShellLanded : EventNode
	{
		public enum HitTypes
		{
			Any = 0,
			Hit = 1,
			Miss = 2
		}

		public ShellDefinition Shell;

		public HitTypes HitType;

		public FilterEntitySet EntityFilter;

		public LocationFilter LocationFilter;

		public EntityContextKeys EntityHit;

		public LocationContextKeys LocationHit;

		private MapEntity cachedEntity;

		private GridReference cachedLocation;

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}

		public override void Run(NodeExecutionState state)
		{
		}
	}
}
