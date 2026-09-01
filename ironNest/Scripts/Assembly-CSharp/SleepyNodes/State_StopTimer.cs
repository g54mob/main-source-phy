namespace SleepyNodes
{
	[CreateNodeMenu("Timer/Stop")]
	[NodeWidth(400)]
	[NodeName("[Timer] Stop")]
	public class State_StopTimer : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public bool RemoveFromScene;

		public float RemoveFromSceneDelay;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
