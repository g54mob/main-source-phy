using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreRateAndReviewResult
	{
		public bool WasUpdated { get; private set; }

		internal XStoreRateAndReviewResult(XGamingRuntime.Interop.XStoreRateAndReviewResult interopStruct)
		{
			WasUpdated = interopStruct.wasUpdated.Value;
		}
	}
}
