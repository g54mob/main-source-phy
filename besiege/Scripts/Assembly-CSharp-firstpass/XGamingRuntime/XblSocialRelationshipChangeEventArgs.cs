using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public struct XblSocialRelationshipChangeEventArgs
	{
		public ulong callerXboxUserId;

		public XblSocialNotificationType socialNotification;

		public ulong[] xboxUserIds;
	}
}
