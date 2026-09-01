using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerPeerToHostRequirements
	{
		public ulong LatencyMaximum { get; private set; }

		public ulong BandwidthDownMinimumInKbps { get; private set; }

		public ulong BandwidthUpMinimumInKbps { get; private set; }

		public XblMultiplayerMetrics HostSelectionMetric { get; private set; }

		internal XblMultiplayerPeerToHostRequirements(XGamingRuntime.Interop.XblMultiplayerPeerToHostRequirements interopStruct)
		{
			LatencyMaximum = interopStruct.LatencyMaximum;
			BandwidthDownMinimumInKbps = interopStruct.BandwidthDownMinimumInKbps;
			BandwidthUpMinimumInKbps = interopStruct.BandwidthUpMinimumInKbps;
			HostSelectionMetric = interopStruct.HostSelectionMetric;
		}
	}
}
