namespace XGamingRuntime.Interop
{
	internal struct XVersion
	{
		internal readonly ushort major;

		internal readonly ushort minor;

		internal readonly ushort build;

		internal readonly ushort revision;

		internal XVersion(XGamingRuntime.XVersion publicObject)
		{
			major = publicObject.Major;
			minor = publicObject.Minor;
			build = publicObject.Build;
			revision = publicObject.Revision;
		}
	}
}
