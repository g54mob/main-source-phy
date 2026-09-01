namespace SleepyNodes
{
	[CreateNodeMenu("Entity/Set Entity State")]
	[NodeWidth(400)]
	[NodeName("Set Entity State")]
	public class State_SetEntityState : StateNode
	{
		public enum Operations
		{
			Add = 0,
			Remove = 1
		}

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public TargetSelection Entity;

		public Operations Operation;

		public MapEntityStates State;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
