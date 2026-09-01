using System;

namespace XGamingRuntime.Interop
{
	internal struct XStoreProduct
	{
		internal readonly UTF8StringPtr storeId;

		internal readonly UTF8StringPtr title;

		internal readonly UTF8StringPtr description;

		internal readonly UTF8StringPtr language;

		internal readonly UTF8StringPtr inAppOfferToken;

		internal readonly UTF8StringPtr linkUri;

		internal readonly XStoreProductKind productKind;

		internal readonly XStorePrice price;

		internal readonly NativeBool hasDigitalDownload;

		internal readonly NativeBool isInUserCollection;

		internal readonly uint keywordsCount;

		private unsafe readonly UTF8StringPtr* keywords;

		internal readonly uint skusCount;

		private unsafe readonly XStoreSku* skus;

		internal readonly uint imagesCount;

		private unsafe readonly XStoreImage* images;

		internal readonly uint videosCount;

		private unsafe readonly XStoreVideo* videos;

		internal unsafe XStoreProduct(XGamingRuntime.XStoreProduct publicObject, DisposableCollection disposableCollection)
		{
			storeId = new UTF8StringPtr(publicObject.StoreId, disposableCollection);
			title = new UTF8StringPtr(publicObject.Title, disposableCollection);
			description = new UTF8StringPtr(publicObject.Description, disposableCollection);
			language = new UTF8StringPtr(publicObject.Language, disposableCollection);
			inAppOfferToken = new UTF8StringPtr(publicObject.InAppOfferToken, disposableCollection);
			linkUri = new UTF8StringPtr(publicObject.LinkUri, disposableCollection);
			productKind = publicObject.ProductKind;
			price = new XStorePrice(publicObject.Price, disposableCollection);
			hasDigitalDownload = new NativeBool(publicObject.HasDigitalDownload);
			isInUserCollection = new NativeBool(publicObject.IsInUserCollection);
			keywords = (UTF8StringPtr*)Converters.ClassArrayToPtr(publicObject.Keywords, (Func<string, DisposableCollection, UTF8StringPtr>)((string x, DisposableCollection _) => new UTF8StringPtr(x, disposableCollection)), disposableCollection, out keywordsCount).ToPointer();
			skus = (XStoreSku*)Converters.ClassArrayToPtr(publicObject.Skus, (Func<XGamingRuntime.XStoreSku, DisposableCollection, XStoreSku>)((XGamingRuntime.XStoreSku x, DisposableCollection _) => new XStoreSku(x, disposableCollection)), disposableCollection, out skusCount).ToPointer();
			images = (XStoreImage*)Converters.ClassArrayToPtr(publicObject.Images, (Func<XGamingRuntime.XStoreImage, DisposableCollection, XStoreImage>)((XGamingRuntime.XStoreImage x, DisposableCollection _) => new XStoreImage(x, disposableCollection)), disposableCollection, out imagesCount).ToPointer();
			videos = (XStoreVideo*)(void*)Converters.ClassArrayToPtr(publicObject.Videos, (Func<XGamingRuntime.XStoreVideo, DisposableCollection, XStoreVideo>)((XGamingRuntime.XStoreVideo x, DisposableCollection _) => new XStoreVideo(x, disposableCollection)), disposableCollection, out videosCount);
		}

		internal unsafe T[] GetKeywords<T>(Func<UTF8StringPtr, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)keywords, keywordsCount, ctor);
		}

		internal unsafe T[] GetSkus<T>(Func<XStoreSku, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)skus, skusCount, ctor);
		}

		internal unsafe T[] GetImages<T>(Func<XStoreImage, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)images, imagesCount, ctor);
		}

		internal unsafe T[] GetVideos<T>(Func<XStoreVideo, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)videos, videosCount, ctor);
		}
	}
}
