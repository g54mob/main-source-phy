using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreGameLicense
	{
		public string SkuStoreId { get; private set; }

		public bool IsActive { get; private set; }

		public bool IsTrialOwnedByThisUser { get; private set; }

		public bool IsDiscLicense { get; private set; }

		public bool IsTrial { get; private set; }

		public uint TrialTimeRemainingInSeconds { get; private set; }

		public string TrialUniqueId { get; private set; }

		public DateTime ExpirationDate { get; private set; }

		internal XStoreGameLicense(XGamingRuntime.Interop.XStoreGameLicense interopStruct)
		{
			SkuStoreId = interopStruct.GetSkuStoreId();
			IsActive = interopStruct.isActive.Value;
			IsTrialOwnedByThisUser = interopStruct.isTrialOwnedByThisUser.Value;
			IsDiscLicense = interopStruct.isDiscLicense.Value;
			IsTrial = interopStruct.isTrial.Value;
			TrialTimeRemainingInSeconds = interopStruct.trialTimeRemainingInSeconds;
			TrialUniqueId = interopStruct.GetTrialUniqueId();
			ExpirationDate = interopStruct.expirationDate.DateTime;
		}
	}
}
