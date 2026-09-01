namespace SleepyNodes
{
	[CreateNodeMenu("Cards/Add Shell")]
	[NodeWidth(400)]
	[NodeName("Add Shell")]
	public class State_AddShell : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public ShellDefinition Shell;

		public ContextVariableOrInline_ShellSlot Slot;

		public ContextVariableOrInline_ShellSource Source;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
