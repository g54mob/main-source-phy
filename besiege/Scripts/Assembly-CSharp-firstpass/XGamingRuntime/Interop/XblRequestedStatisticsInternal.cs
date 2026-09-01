using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblRequestedStatisticsInternal
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
		internal readonly byte[] serviceConfigurationId;

		private readonly IntPtr statistics;

		private readonly uint statisticsCount;

		internal XblRequestedStatisticsInternal(XGamingRuntime.XblRequestedStatistics requestedStatistics, DisposableCollection disposableCollection)
		{
			serviceConfigurationId = Converters.StringToNullTerminatedUTF8ByteArray(requestedStatistics.ServiceConfigurationId, 40);
			SizeT count;
			statistics = Converters.StringArrayToUTF8StringArray(requestedStatistics.Statistics, disposableCollection, out count);
			statisticsCount = count.ToUInt32();
		}

		internal static bool ValidateFields(string scid)
		{
			return scid != null && Converters.StringToNullTerminatedUTF8ByteArray(scid).Length <= 40;
		}
	}
}
