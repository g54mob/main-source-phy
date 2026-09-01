namespace SleepyNodes
{
	[CreateNodeMenu("Timer/Unpause")]
	[NodeWidth(400)]
	[NodeName("[Timer] Unpause")]
	public class State_UnpauseTimer : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}
	}
}
