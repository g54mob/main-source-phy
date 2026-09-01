using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreAvailability
	{
		public string AvailabilityId { get; private set; }

		public XStorePrice Price { get; private set; }

		public DateTime EndDate { get; private set; }

		internal XStoreAvailability(XGamingRuntime.Interop.XStoreAvailability interopStruct)
		{
			AvailabilityId = interopStruct.availabilityId.GetString();
			Price = new XStorePrice(interopStruct.price);
			EndDate = interopStruct.endDate.DateTime;
		}
	}
}
