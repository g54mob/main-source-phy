namespace SleepyNodes
{
	[CreateNodeMenu("Timer/Start")]
	[NodeWidth(400)]
	[NodeName("[Timer] Start")]
	public class State_StartTimer : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public float InitalTime;

		public CounterBatteryTimer Prefab_BatteryTimer;

		private CounterBatteryTimer spawnedTimer;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
