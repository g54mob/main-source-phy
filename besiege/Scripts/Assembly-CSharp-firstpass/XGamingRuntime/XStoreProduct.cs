using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreProduct
	{
		public string StoreId { get; private set; }

		public string Title { get; private set; }

		public string Description { get; private set; }

		public string Language { get; private set; }

		public string InAppOfferToken { get; private set; }

		public string LinkUri { get; private set; }

		public XStoreProductKind ProductKind { get; private set; }

		public XStorePrice Price { get; private set; }

		public bool HasDigitalDownload { get; private set; }

		public bool IsInUserCollection { get; private set; }

		public string[] Keywords { get; private set; }

		public XStoreSku[] Skus { get; private set; }

		public XStoreImage[] Images { get; private set; }

		public XStoreVideo[] Videos { get; private set; }

		internal XStoreProduct(XGamingRuntime.Interop.XStoreProduct interopStruct)
		{
			StoreId = interopStruct.storeId.GetString();
			Title = interopStruct.title.GetString();
			Description = interopStruct.description.GetString();
			Language = interopStruct.language.GetString();
			InAppOfferToken = interopStruct.inAppOfferToken.GetString();
			LinkUri = interopStruct.linkUri.GetString();
			ProductKind = interopStruct.productKind;
			Price = new XStorePrice(interopStruct.price);
			HasDigitalDownload = interopStruct.hasDigitalDownload.Value;
			IsInUserCollection = interopStruct.isInUserCollection.Value;
			Keywords = interopStruct.GetKeywords((UTF8StringPtr x) => x.GetString());
			Skus = interopStruct.GetSkus((XGamingRuntime.Interop.XStoreSku x) => new XStoreSku(x));
			Images = interopStruct.GetImages((XGamingRuntime.Interop.XStoreImage x) => new XStoreImage(x));
			Videos = interopStruct.GetVideos((XGamingRuntime.Interop.XStoreVideo x) => new XStoreVideo(x));
		}
	}
}
