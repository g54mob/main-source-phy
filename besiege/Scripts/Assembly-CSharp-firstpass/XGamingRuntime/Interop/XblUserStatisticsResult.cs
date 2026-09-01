namespace XGamingRuntime.Interop
{
	public struct XblUserStatisticsResult
	{
		[NativeTypeName("uint64_t")]
		public ulong xboxUserId;

		public unsafe XblServiceConfigurationStatistic* serviceConfigStatistics;

		[NativeTypeName("uint32_t")]
		public uint serviceConfigStatisticsCount;
	}
}
