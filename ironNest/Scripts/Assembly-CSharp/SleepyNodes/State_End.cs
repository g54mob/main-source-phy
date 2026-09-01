namespace SleepyNodes
{
	[CreateNodeMenu("Mission/Mission Complete")]
	[NodeName("Mission Complete")]
	[NodeWidth(200)]
	public class State_End : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
