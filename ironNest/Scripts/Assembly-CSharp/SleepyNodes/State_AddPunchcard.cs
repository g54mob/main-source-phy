namespace SleepyNodes
{
	[CreateNodeMenu("Cards/Add Punchcard")]
	[NodeWidth(400)]
	[NodeName("Add Punchcard")]
	public class State_AddPunchcard : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public PunchcardDefinitionV2 Punchcard;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
