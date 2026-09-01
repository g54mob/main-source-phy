namespace SleepyNodes
{
	[CreateNodeMenu("Timer/Pause")]
	[NodeWidth(400)]
	[NodeName("[Timer] Pause")]
	public class State_PauseTimer : StateNode
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
