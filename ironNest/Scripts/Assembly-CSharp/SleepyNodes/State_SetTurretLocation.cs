namespace SleepyNodes
{
	[CreateNodeMenu("Turret/Set Location")]
	[NodeWidth(400)]
	[NodeName("Set Turret Location")]
	public class State_SetTurretLocation : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public LocationSelection LocationToMoveTo;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
