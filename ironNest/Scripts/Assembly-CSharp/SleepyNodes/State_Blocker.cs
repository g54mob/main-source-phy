namespace SleepyNodes
{
	[CreateNodeMenu("Wait/Blocker")]
	[NodeName("Blocker")]
	[NodeWidth(400)]
	public class State_Blocker : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false, backingValue = ShowBackingValue.Never)]
		public StateNode Block;

		public bool InvertBlocking;

		private bool blocked;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
