namespace SleepyNodes
{
	[CreateNodeMenu("Timer/Modify Time")]
	[NodeWidth(400)]
	[NodeName("[Timer] Modify Time")]
	public class State_TimerAddTime : StateNode
	{
		public enum ModifyTypes
		{
			Add = 0,
			Set = 1,
			Subtract = 2,
			Reset = 3
		}

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public ModifyTypes ModifyType;

		public float Time;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
