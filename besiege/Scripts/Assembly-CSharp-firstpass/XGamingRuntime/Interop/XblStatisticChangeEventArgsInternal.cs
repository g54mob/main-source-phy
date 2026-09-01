using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblStatisticChangeEventArgsInternal
	{
		internal readonly ulong xboxUserId;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
		internal readonly byte[] serviceConfigurationId;

		internal readonly XblStatisticInternal latestStatistic;
	}
}
