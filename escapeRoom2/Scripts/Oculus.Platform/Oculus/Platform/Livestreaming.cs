namespace Oculus.Platform
{
	public static class Livestreaming
	{
		public static void SetStatusUpdateNotificationCallback(Message<Oculus.Platform.Models.LivestreamingStatus>.Callback callback)
		{
			Callback.SetNotificationCallback(Message.MessageType.Notification_Livestreaming_StatusChange, callback);
		}
	}
}
