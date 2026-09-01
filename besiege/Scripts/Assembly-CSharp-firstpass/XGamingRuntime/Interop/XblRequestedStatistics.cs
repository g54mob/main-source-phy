namespace XGamingRuntime.Interop
{
	public struct XblRequestedStatistics
	{
		[NativeTypeName("char [40]")]
		public unsafe fixed sbyte serviceConfigurationId[40];

		[NativeTypeName("const char **")]
		public unsafe sbyte** statistics;

		[NativeTypeName("uint32_t")]
		public uint statisticsCount;
	}
}
