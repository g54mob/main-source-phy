namespace XGamingRuntime.Interop
{
	internal struct XStoreSubscriptionInfo
	{
		internal readonly NativeBool hasTrialPeriod;

		internal readonly XStoreDurationUnit trialPeriodUnit;

		internal readonly uint trialPeriod;

		internal readonly XStoreDurationUnit billingPeriodUnit;

		internal readonly uint billingPeriod;

		internal XStoreSubscriptionInfo(XGamingRuntime.XStoreSubscriptionInfo publicObject)
		{
			hasTrialPeriod = new NativeBool(publicObject.HasTrialPeriod);
			trialPeriodUnit = publicObject.TrialPeriodUnit;
			trialPeriod = publicObject.TrialPeriod;
			billingPeriodUnit = publicObject.BillingPeriodUnit;
			billingPeriod = publicObject.BillingPeriod;
		}
	}
}
