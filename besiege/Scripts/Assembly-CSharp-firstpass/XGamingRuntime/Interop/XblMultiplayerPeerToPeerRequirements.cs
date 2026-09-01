namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerPeerToPeerRequirements
	{
		internal readonly ulong LatencyMaximum;

		internal readonly ulong BandwidthMinimumInKbps;

		internal XblMultiplayerPeerToPeerRequirements(XGamingRuntime.XblMultiplayerPeerToPeerRequirements publicObject)
		{
			LatencyMaximum = publicObject.LatencyMaximum;
			BandwidthMinimumInKbps = publicObject.BandwidthMinimumInKbps;
		}
	}
}
