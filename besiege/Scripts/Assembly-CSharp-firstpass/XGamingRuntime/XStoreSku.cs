using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreSku
	{
		public string SkuId { get; private set; }

		public string Title { get; private set; }

		public string Description { get; private set; }

		public string Language { get; private set; }

		public XStorePrice Price { get; private set; }

		public bool IsTrial { get; private set; }

		public bool IsInUserCollection { get; private set; }

		public XStoreCollectionData CollectionData { get; private set; }

		public bool IsSubscription { get; private set; }

		public XStoreSubscriptionInfo SubscriptionInfo { get; private set; }

		public string[] BundledSkus { get; private set; }

		public XStoreImage[] Images { get; private set; }

		public XStoreVideo[] Videos { get; private set; }

		public XStoreAvailability[] Availabilities { get; private set; }

		internal XStoreSku(XGamingRuntime.Interop.XStoreSku interopStruct)
		{
			SkuId = interopStruct.skuId.GetString();
			Title = interopStruct.title.GetString();
			Description = interopStruct.description.GetString();
			Language = interopStruct.language.GetString();
			Price = new XStorePrice(interopStruct.price);
			IsTrial = interopStruct.isTrial.Value;
			IsInUserCollection = interopStruct.isInUserCollection.Value;
			CollectionData = new XStoreCollectionData(interopStruct.collectionData);
			IsSubscription = interopStruct.isSubscription.Value;
			SubscriptionInfo = new XStoreSubscriptionInfo(interopStruct.subscriptionInfo);
			BundledSkus = interopStruct.GetBundledSkus((UTF8StringPtr x) => x.GetString());
			Images = interopStruct.GetImages((XGamingRuntime.Interop.XStoreImage x) => new XStoreImage(x));
			Videos = interopStruct.GetVideos((XGamingRuntime.Interop.XStoreVideo x) => new XStoreVideo(x));
			Availabilities = interopStruct.GetAvailabilities((XGamingRuntime.Interop.XStoreAvailability x) => new XStoreAvailability(x));
		}
	}
}
