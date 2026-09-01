namespace SleepyNodes
{
	[CreateNodeMenu("Mission/Leaderboard Points")]
	[NodeWidth(400)]
	[NodeName("Leaderboard Points")]
	public class State_AddLeaderboardPoints : StateNode
	{
		public enum Operations
		{
			Add = 0,
			Remove = 1
		}

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public Operations Operation;

		public ContextVariableOrInline_Int Amount;

		public string ActionName;

		public string ActionDetails;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
