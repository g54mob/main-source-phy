using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblServiceConfigurationStatisticInternal
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
		internal readonly byte[] serviceConfigurationId;

		private readonly IntPtr statistics;

		private readonly uint statisticsCount;

		internal T[] GetStatistics<T>(Func<XblStatisticInternal, T> ctor)
		{
			return Converters.PtrToClassArray(statistics, statisticsCount, ctor);
		}
	}
}
