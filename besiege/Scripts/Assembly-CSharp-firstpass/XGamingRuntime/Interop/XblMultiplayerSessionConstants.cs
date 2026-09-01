using System;

namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerSessionConstants
	{
		internal readonly uint MaxMembersInSession;

		internal readonly XblMultiplayerSessionVisibility Visibility;

		private unsafe readonly ulong* InitiatorXuids;

		internal readonly SizeT InitiatorXuidsCount;

		internal readonly UTF8StringPtr CustomJson;

		internal readonly UTF8StringPtr SessionCloudComputePackageConstantsJson;

		internal readonly ulong MemberReservedTimeout;

		internal readonly ulong MemberInactiveTimeout;

		internal readonly ulong MemberReadyTimeout;

		internal readonly ulong SessionEmptyTimeout;

		internal readonly ulong ArbitrationTimeout;

		internal readonly ulong ForfeitTimeout;

		internal readonly NativeBool EnableMetricsLatency;

		internal readonly NativeBool EnableMetricsBandwidthDown;

		internal readonly NativeBool EnableMetricsBandwidthUp;

		internal readonly NativeBool EnableMetricsCustom;

		private unsafe readonly XblMultiplayerMemberInitialization* MemberInitialization;

		internal readonly XblMultiplayerPeerToPeerRequirements PeerToPeerRequirements;

		internal readonly XblMultiplayerPeerToHostRequirements PeerToHostRequirements;

		internal readonly UTF8StringPtr MeasurementServerAddressesJson;

		internal readonly NativeBool ClientMatchmakingCapable;

		internal readonly XblMultiplayerSessionCapabilities SessionCapabilities;

		internal unsafe T[] GetInitiatorXuids<T>(Func<ulong, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)InitiatorXuids, InitiatorXuidsCount, ctor);
		}

		internal unsafe T GetMemberInitialization<T>(Func<XblMultiplayerMemberInitialization, T> ctor) where T : class
		{
			return Converters.PtrToClass((IntPtr)MemberInitialization, ctor);
		}
	}
}
