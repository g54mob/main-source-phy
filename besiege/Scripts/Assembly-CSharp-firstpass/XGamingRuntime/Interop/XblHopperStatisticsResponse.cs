namespace XGamingRuntime.Interop
{
	public struct XblHopperStatisticsResponse
	{
		[NativeTypeName("char *")]
		public unsafe sbyte* hopperName;

		[NativeTypeName("int64_t")]
		public long estimatedWaitTime;

		[NativeTypeName("uint32_t")]
		public uint playersWaitingToMatch;
	}
}
