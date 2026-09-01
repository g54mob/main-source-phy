using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_REGION
	{
		public string RegionName { get; set; }

		public uint RoundTripLatencyInMilliseconds { get; set; }

		internal PARTY_REGION(PartyCSharpSDK.Interop.PARTY_REGION interopStruct)
		{
			RegionName = interopStruct.GetRegionName();
			RoundTripLatencyInMilliseconds = interopStruct.roundTripLatencyInMilliseconds;
		}
	}
}
