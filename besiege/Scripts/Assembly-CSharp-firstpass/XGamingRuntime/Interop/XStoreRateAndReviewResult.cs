namespace XGamingRuntime.Interop
{
	internal struct XStoreRateAndReviewResult
	{
		internal readonly NativeBool wasUpdated;

		internal XStoreRateAndReviewResult(XGamingRuntime.XStoreRateAndReviewResult publicObject)
		{
			wasUpdated = new NativeBool(publicObject.WasUpdated);
		}
	}
}
