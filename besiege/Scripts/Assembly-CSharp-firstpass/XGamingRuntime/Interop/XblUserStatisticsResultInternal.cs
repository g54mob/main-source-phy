using System;

namespace XGamingRuntime.Interop
{
	internal struct XblUserStatisticsResultInternal
	{
		internal readonly ulong xboxUserId;

		private readonly IntPtr serviceConfigStatistics;

		private readonly uint serviceConfigStatisticsCount;

		internal T[] GetServiceConfigStatistics<T>(Func<XblServiceConfigurationStatisticInternal, T> ctor)
		{
			return Converters.PtrToClassArray(serviceConfigStatistics, serviceConfigStatisticsCount, ctor);
		}
	}
}
