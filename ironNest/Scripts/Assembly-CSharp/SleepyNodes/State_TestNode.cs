namespace SleepyNodes
{
	[CreateNodeMenu("Test Node")]
	[NodeWidth(400)]
	[NodeName("Test Node")]
	public class State_TestNode : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false)]
		public TargetSelection Targets;

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false)]
		public LocationSelection Location;

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}

		public override object GetValue(NodePort port)
		{
			return null;
		}
	}
}
