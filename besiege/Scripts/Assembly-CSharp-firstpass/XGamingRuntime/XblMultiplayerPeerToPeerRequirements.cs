using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerPeerToPeerRequirements
	{
		public ulong LatencyMaximum { get; set; }

		public ulong BandwidthMinimumInKbps { get; set; }

		public XblMultiplayerPeerToPeerRequirements()
		{
		}

		internal XblMultiplayerPeerToPeerRequirements(XGamingRuntime.Interop.XblMultiplayerPeerToPeerRequirements interopStruct)
		{
			LatencyMaximum = interopStruct.LatencyMaximum;
			BandwidthMinimumInKbps = interopStruct.BandwidthMinimumInKbps;
		}
	}
}
