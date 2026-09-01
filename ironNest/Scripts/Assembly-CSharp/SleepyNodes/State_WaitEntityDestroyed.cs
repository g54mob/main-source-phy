namespace SleepyNodes
{
	[CreateNodeMenu("Wait/For Entity Destroyed")]
	[NodeWidth(400)]
	[NodeName("Wait Entity Destroyed")]
	public class State_WaitEntityDestroyed : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public TargetSelection Entites;

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}
	}
}
