namespace SleepyNodes
{
	[CreateNodeMenu("Branches/Split")]
	[NodeName("Split Branch")]
	[NodeWidth(300)]
	public class State_SplitBranch : StateNode
	{
		public bool InheritContextVariables;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never, dynamicPortList = true)]
		public string[] To;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
