namespace SleepyNodes
{
	[CreateNodeMenu("Branches/Condition")]
	[NodeWidth(600)]
	[NodeName("Condition Branch")]
	public class State_ConditionBranch : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode OnFail;

		public TargetSelection EntityFilter;

		public ConditionSet Conditions;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
