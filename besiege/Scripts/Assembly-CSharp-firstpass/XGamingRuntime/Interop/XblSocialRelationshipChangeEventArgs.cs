namespace XGamingRuntime.Interop
{
	public struct XblSocialRelationshipChangeEventArgs
	{
		[NativeTypeName("uint64_t")]
		public ulong callerXboxUserId;

		public XblSocialNotificationType socialNotification;

		[NativeTypeName("uint64_t *")]
		public unsafe ulong* xboxUserIds;

		[NativeTypeName("size_t")]
		public SizeT xboxUserIdsCount;
	}
}
