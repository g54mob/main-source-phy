namespace XGamingRuntime.Interop
{
	internal struct XStoreVideo
	{
		internal readonly UTF8StringPtr uri;

		internal readonly uint height;

		internal readonly uint width;

		internal readonly UTF8StringPtr caption;

		internal readonly UTF8StringPtr videoPurposeTag;

		internal readonly XStoreImage previewImage;

		internal XStoreVideo(XGamingRuntime.XStoreVideo publicObject, DisposableCollection disposableCollection)
		{
			uri = new UTF8StringPtr(publicObject.Uri, disposableCollection);
			height = publicObject.Height;
			width = publicObject.Width;
			caption = new UTF8StringPtr(publicObject.Caption, disposableCollection);
			videoPurposeTag = new UTF8StringPtr(publicObject.VideoPurposeTag, disposableCollection);
			previewImage = new XStoreImage(publicObject.PreviewImage, disposableCollection);
		}
	}
}
