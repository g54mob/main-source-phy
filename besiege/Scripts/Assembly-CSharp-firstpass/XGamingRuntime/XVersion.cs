using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XVersion
	{
		public ushort Major { get; private set; }

		public ushort Minor { get; private set; }

		public ushort Build { get; private set; }

		public ushort Revision { get; private set; }

		internal XVersion(XGamingRuntime.Interop.XVersion interopStruct)
		{
			Major = interopStruct.major;
			Minor = interopStruct.minor;
			Build = interopStruct.build;
			Revision = interopStruct.revision;
		}
	}
}
