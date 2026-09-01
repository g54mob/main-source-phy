namespace XGamingRuntime.Interop
{
	public struct XblServiceConfigurationStatistic
	{
		[NativeTypeName("char [40]")]
		public unsafe fixed sbyte serviceConfigurationId[40];

		public unsafe XblStatistic* statistics;

		[NativeTypeName("uint32_t")]
		public uint statisticsCount;
	}
}
