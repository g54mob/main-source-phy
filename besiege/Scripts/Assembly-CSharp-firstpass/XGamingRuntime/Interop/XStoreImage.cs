namespace XGamingRuntime.Interop
{
	internal struct XStoreImage
	{
		internal readonly UTF8StringPtr uri;

		internal readonly uint height;

		internal readonly uint width;

		internal readonly UTF8StringPtr caption;

		internal readonly UTF8StringPtr imagePurposeTag;

		internal XStoreImage(XGamingRuntime.XStoreImage publicObject, DisposableCollection disposableCollection)
		{
			uri = new UTF8StringPtr(publicObject.Uri, disposableCollection);
			height = publicObject.Height;
			width = publicObject.Width;
			caption = new UTF8StringPtr(publicObject.Caption, disposableCollection);
			imagePurposeTag = new UTF8StringPtr(publicObject.ImagePurposeTag, disposableCollection);
		}
	}
}
