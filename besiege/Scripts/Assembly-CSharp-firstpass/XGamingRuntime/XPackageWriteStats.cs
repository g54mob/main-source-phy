using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XPackageWriteStats
	{
		public ulong Interval { get; private set; }

		public ulong Budget { get; private set; }

		public ulong Elapsed { get; private set; }

		public ulong BytesWritten { get; private set; }

		internal XPackageWriteStats(XGamingRuntime.Interop.XPackageWriteStats interopStruct)
		{
			Interval = interopStruct.interval;
			Budget = interopStruct.budget;
			Elapsed = interopStruct.elapsed;
			BytesWritten = interopStruct.bytesWritten;
		}
	}
}
