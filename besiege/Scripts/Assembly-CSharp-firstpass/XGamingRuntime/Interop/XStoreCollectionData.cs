namespace XGamingRuntime.Interop
{
	internal struct XStoreCollectionData
	{
		internal readonly TimeT acquiredDate;

		internal readonly TimeT startDate;

		internal readonly TimeT endDate;

		internal readonly NativeBool isTrial;

		internal readonly uint trialTimeRemainingInSeconds;

		internal readonly uint quantity;

		internal readonly UTF8StringPtr campaignId;

		internal readonly UTF8StringPtr developerOfferId;

		internal XStoreCollectionData(XGamingRuntime.XStoreCollectionData publicObject, DisposableCollection disposableCollection)
		{
			acquiredDate = new TimeT(publicObject.AcquiredDate);
			startDate = new TimeT(publicObject.StartDate);
			endDate = new TimeT(publicObject.EndDate);
			isTrial = new NativeBool(publicObject.IsTrial);
			trialTimeRemainingInSeconds = publicObject.TrialTimeRemainingInSeconds;
			quantity = publicObject.Quantity;
			campaignId = new UTF8StringPtr(publicObject.CampaignId, disposableCollection);
			developerOfferId = new UTF8StringPtr(publicObject.DeveloperOfferId, disposableCollection);
		}
	}
}
