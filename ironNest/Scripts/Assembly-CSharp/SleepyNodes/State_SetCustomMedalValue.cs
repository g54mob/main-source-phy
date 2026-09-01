namespace SleepyNodes
{
	[CreateNodeMenu("Mission/Set Custom Medal Value")]
	[NodeWidth(400)]
	[NodeName("Set Custom Medal Value")]
	public class State_SetCustomMedalValue : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public string Key;

		public float Value;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
