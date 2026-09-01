namespace SleepyNodes
{
	[CreateNodeMenu("Mission/Mission Failed")]
	[NodeName("Mission Failed")]
	[NodeWidth(200)]
	public class State_MissionFailed : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
