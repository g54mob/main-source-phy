namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerSessionQuery
	{
		[NativeTypeName("char [40]")]
		public unsafe fixed sbyte Scid[40];

		[NativeTypeName("uint32_t")]
		public uint MaxItems;

		public bool IncludePrivateSessions;

		public bool IncludeReservations;

		public bool IncludeInactiveSessions;

		[NativeTypeName("uint64_t *")]
		public unsafe ulong* XuidFilters;

		[NativeTypeName("size_t")]
		public SizeT XuidFiltersCount;

		[NativeTypeName("const char *")]
		public unsafe sbyte* KeywordFilter;

		[NativeTypeName("char [100]")]
		public unsafe fixed sbyte SessionTemplateNameFilter[100];

		public XblMultiplayerSessionVisibility VisibilityFilter;

		[NativeTypeName("uint32_t")]
		public uint ContractVersionFilter;
	}
}
