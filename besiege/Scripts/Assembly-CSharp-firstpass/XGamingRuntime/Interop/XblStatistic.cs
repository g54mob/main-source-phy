namespace XGamingRuntime.Interop
{
	public struct XblStatistic
	{
		[NativeTypeName("const char *")]
		public unsafe sbyte* statisticName;

		[NativeTypeName("const char *")]
		public unsafe sbyte* statisticType;

		[NativeTypeName("const char *")]
		public unsafe sbyte* value;
	}
}
