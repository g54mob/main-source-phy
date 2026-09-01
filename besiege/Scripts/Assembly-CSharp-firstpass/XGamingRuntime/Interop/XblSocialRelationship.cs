namespace XGamingRuntime.Interop
{
	public struct XblSocialRelationship
	{
		[NativeTypeName("uint64_t")]
		public ulong xboxUserId;

		public bool isFavorite;

		public bool isFollowingCaller;

		[NativeTypeName("const char **")]
		public unsafe sbyte** socialNetworks;

		[NativeTypeName("size_t")]
		public SizeT socialNetworksCount;
	}
}
