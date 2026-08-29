namespace Oculus.Platform
{
	public static class NetSync
	{
		public static void SetConnectionStatusChangedNotificationCallback(Message<Oculus.Platform.Models.NetSyncConnection>.Callback callback)
		{
			Callback.SetNotificationCallback(Message.MessageType.Notification_NetSync_ConnectionStatusChanged, callback);
		}

		public static void SetSessionsChangedNotificationCallback(Message<Oculus.Platform.Models.NetSyncSessionsChangedNotification>.Callback callback)
		{
			Callback.SetNotificationCallback(Message.MessageType.Notification_NetSync_SessionsChanged, callback);
		}
	}
}
