namespace SleepyNodes
{
	[NodeName("DEPRECATED")]
	[NodeWidth(400)]
	public class State_AddMedals : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
