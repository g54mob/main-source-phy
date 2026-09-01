namespace SleepyNodes
{
	[CreateNodeMenu("Mission/Send Scene Notification")]
	[NodeWidth(400)]
	[NodeName("Send Scene Notification")]
	public class State_SendSceneNotification : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public string MessageID;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
