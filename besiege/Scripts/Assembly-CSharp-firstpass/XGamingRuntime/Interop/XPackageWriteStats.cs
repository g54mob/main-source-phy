namespace XGamingRuntime.Interop
{
	internal struct XPackageWriteStats
	{
		internal readonly ulong interval;

		internal readonly ulong budget;

		internal readonly ulong elapsed;

		internal readonly ulong bytesWritten;

		internal XPackageWriteStats(XGamingRuntime.XPackageWriteStats publicObject)
		{
			interval = publicObject.Interval;
			budget = publicObject.Budget;
			elapsed = publicObject.Elapsed;
			bytesWritten = publicObject.BytesWritten;
		}
	}
}
