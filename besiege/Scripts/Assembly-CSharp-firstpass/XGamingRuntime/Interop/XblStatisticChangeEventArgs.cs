namespace XGamingRuntime.Interop
{
	public struct XblStatisticChangeEventArgs
	{
		[NativeTypeName("uint64_t")]
		public ulong xboxUserId;

		[NativeTypeName("char [40]")]
		public unsafe fixed sbyte serviceConfigurationId[40];

		public XblStatistic latestStatistic;
	}
}
