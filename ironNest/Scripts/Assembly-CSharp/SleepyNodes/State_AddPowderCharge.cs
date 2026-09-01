namespace SleepyNodes
{
	[CreateNodeMenu("Cards/Add Poweder Charge")]
	[NodeWidth(400)]
	[NodeName("Add Powder Charge")]
	public class State_AddPowderCharge : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public ContextVariableOrInline_Int Amount;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
