using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreQueryResult
	{
		internal XStoreProductQueryHandle QueryHandle { get; private set; }

		public bool HasMorePages { get; private set; }

		public XStoreProduct[] PageItems { get; private set; }

		internal XStoreQueryResult(XStoreProductQueryHandle queryHandle, XStoreProduct[] pageItems, bool hasMorePages)
		{
			QueryHandle = queryHandle;
			PageItems = pageItems;
			HasMorePages = hasMorePages;
		}
	}
}
