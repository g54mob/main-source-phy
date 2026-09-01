using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreVideo
	{
		public string Uri { get; private set; }

		public uint Height { get; private set; }

		public uint Width { get; private set; }

		public string Caption { get; private set; }

		public string VideoPurposeTag { get; private set; }

		public XStoreImage PreviewImage { get; private set; }

		internal XStoreVideo(XGamingRuntime.Interop.XStoreVideo interopStruct)
		{
			Uri = interopStruct.uri.GetString();
			Height = interopStruct.height;
			Width = interopStruct.width;
			Caption = interopStruct.caption.GetString();
			VideoPurposeTag = interopStruct.videoPurposeTag.GetString();
			PreviewImage = new XStoreImage(interopStruct.previewImage);
		}
	}
}
