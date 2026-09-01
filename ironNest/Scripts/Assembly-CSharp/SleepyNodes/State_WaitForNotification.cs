namespace SleepyNodes
{
	[CreateNodeMenu("Wait/For Notification")]
	[NodeName("Wait For Notification")]
	[NodeWidth(400)]
	public class State_WaitForNotification : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public string NotifID;

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}

		public override void OnNotification(NodeExecutionState state, string notif)
		{
		}
	}
}
