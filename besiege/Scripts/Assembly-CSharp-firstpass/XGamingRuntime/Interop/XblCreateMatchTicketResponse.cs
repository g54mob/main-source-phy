namespace XGamingRuntime.Interop
{
	public struct XblCreateMatchTicketResponse
	{
		[NativeTypeName("char [40]")]
		public unsafe fixed sbyte matchTicketId[40];

		[NativeTypeName("int64_t")]
		public long estimatedWaitTime;
	}
}
