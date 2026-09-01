using System;

namespace XGamingRuntime.Interop
{
	internal struct XStoreSku
	{
		internal readonly UTF8StringPtr skuId;

		internal readonly UTF8StringPtr title;

		internal readonly UTF8StringPtr description;

		internal readonly UTF8StringPtr language;

		internal readonly XStorePrice price;

		internal readonly NativeBool isTrial;

		internal readonly NativeBool isInUserCollection;

		internal readonly XStoreCollectionData collectionData;

		internal readonly NativeBool isSubscription;

		internal readonly XStoreSubscriptionInfo subscriptionInfo;

		internal readonly uint bundledSkusCount;

		private unsafe readonly UTF8StringPtr* bundledSkus;

		internal readonly uint imagesCount;

		private unsafe readonly XStoreImage* images;

		internal readonly uint videosCount;

		private unsafe readonly XStoreVideo* videos;

		internal readonly uint availabilitiesCount;

		private unsafe readonly XStoreAvailability* availabilities;

		internal unsafe XStoreSku(XGamingRuntime.XStoreSku publicObject, DisposableCollection disposableCollection)
		{
			skuId = new UTF8StringPtr(publicObject.SkuId, disposableCollection);
			title = new UTF8StringPtr(publicObject.Title, disposableCollection);
			description = new UTF8StringPtr(publicObject.Description, disposableCollection);
			language = new UTF8StringPtr(publicObject.Language, disposableCollection);
			price = new XStorePrice(publicObject.Price, disposableCollection);
			isTrial = new NativeBool(publicObject.IsTrial);
			isInUserCollection = new NativeBool(publicObject.IsInUserCollection);
			collectionData = new XStoreCollectionData(publicObject.CollectionData, disposableCollection);
			isSubscription = new NativeBool(publicObject.IsSubscription);
			subscriptionInfo = new XStoreSubscriptionInfo(publicObject.SubscriptionInfo);
			bundledSkus = (UTF8StringPtr*)Converters.ClassArrayToPtr(publicObject.BundledSkus, (Func<string, DisposableCollection, UTF8StringPtr>)((string x, DisposableCollection _) => new UTF8StringPtr(x, disposableCollection)), disposableCollection, out bundledSkusCount).ToPointer();
			images = (XStoreImage*)Converters.ClassArrayToPtr(publicObject.Images, (Func<XGamingRuntime.XStoreImage, DisposableCollection, XStoreImage>)((XGamingRuntime.XStoreImage x, DisposableCollection _) => new XStoreImage(x, disposableCollection)), disposableCollection, out imagesCount).ToPointer();
			videos = (XStoreVideo*)Converters.ClassArrayToPtr(publicObject.Videos, (Func<XGamingRuntime.XStoreVideo, DisposableCollection, XStoreVideo>)((XGamingRuntime.XStoreVideo x, DisposableCollection _) => new XStoreVideo(x, disposableCollection)), disposableCollection, out videosCount).ToPointer();
			availabilities = (XStoreAvailability*)Converters.ClassArrayToPtr(publicObject.Availabilities, (Func<XGamingRuntime.XStoreAvailability, DisposableCollection, XStoreAvailability>)((XGamingRuntime.XStoreAvailability x, DisposableCollection _) => new XStoreAvailability(x, disposableCollection)), disposableCollection, out availabilitiesCount).ToPointer();
		}

		internal unsafe T[] GetBundledSkus<T>(Func<UTF8StringPtr, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)bundledSkus, bundledSkusCount, ctor);
		}

		internal unsafe T[] GetImages<T>(Func<XStoreImage, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)images, imagesCount, ctor);
		}

		internal unsafe T[] GetVideos<T>(Func<XStoreVideo, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)videos, videosCount, ctor);
		}

		internal unsafe T[] GetAvailabilities<T>(Func<XStoreAvailability, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)availabilities, availabilitiesCount, ctor);
		}
	}
}
