namespace SleepyNodes
{
	[CreateNodeMenu("Cards/Requisition Points")]
	[NodeWidth(400)]
	[NodeName("Requisition Points")]
	public class State_AddRequisitionPoints : StateNode
	{
		public enum Operations
		{
			Add = 0,
			Spend = 1,
			Set = 2
		}

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public Operations Operation;

		public ContextVariableOrInline_Int Amount;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
