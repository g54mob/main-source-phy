namespace XGamingRuntime.Interop
{
	[NativeTypeName("uint32_t")]
	public enum XblTicketStatus : uint
	{
		Unknown = 0u,
		Expired = 1u,
		Searching = 2u,
		Found = 3u,
		Canceled = 4u
	}
}
