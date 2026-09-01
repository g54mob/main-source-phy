namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerPeerToHostRequirements
	{
		internal readonly ulong LatencyMaximum;

		internal readonly ulong BandwidthDownMinimumInKbps;

		internal readonly ulong BandwidthUpMinimumInKbps;

		internal readonly XblMultiplayerMetrics HostSelectionMetric;

		internal XblMultiplayerPeerToHostRequirements(XGamingRuntime.XblMultiplayerPeerToHostRequirements publicObject)
		{
			LatencyMaximum = publicObject.LatencyMaximum;
			BandwidthDownMinimumInKbps = publicObject.BandwidthDownMinimumInKbps;
			BandwidthUpMinimumInKbps = publicObject.BandwidthUpMinimumInKbps;
			HostSelectionMetric = publicObject.HostSelectionMetric;
		}
	}
}
