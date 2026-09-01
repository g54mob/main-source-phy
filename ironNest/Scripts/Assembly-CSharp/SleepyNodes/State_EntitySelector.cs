namespace SleepyNodes
{
	[CreateNodeMenu("Entity/Entity Selector")]
	[NodeWidth(400)]
	[NodeName("Entity Selector")]
	public class State_EntitySelector : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public TargetSelection EntitySelection;

		public EntityContextKeys ContextKey;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
