using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreAddonLicense
	{
		public string SkuStoreId { get; private set; }

		public string InAppOfferToken { get; private set; }

		public bool IsActive { get; private set; }

		public DateTime ExpirationDate { get; private set; }

		internal XStoreAddonLicense(XGamingRuntime.Interop.XStoreAddonLicense interopStruct)
		{
			SkuStoreId = interopStruct.GetSkuStoreId();
			InAppOfferToken = interopStruct.GetInAppOfferToken();
			IsActive = interopStruct.isActive.Value;
			ExpirationDate = interopStruct.expirationDate.DateTime;
		}
	}
}
