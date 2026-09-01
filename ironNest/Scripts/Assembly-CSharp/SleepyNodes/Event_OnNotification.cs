namespace SleepyNodes
{
	[CreateNodeMenu("Events/Notification")]
	[NodeWidth(400)]
	[NodeName("[Event] Notification")]
	public class Event_OnNotification : EventNode
	{
		public string NotifID;

		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}
	}
}
