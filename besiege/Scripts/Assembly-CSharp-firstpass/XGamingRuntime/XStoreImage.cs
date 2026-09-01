using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreImage
	{
		public string Uri { get; private set; }

		public uint Height { get; private set; }

		public uint Width { get; private set; }

		public string Caption { get; private set; }

		public string ImagePurposeTag { get; private set; }

		internal XStoreImage(XGamingRuntime.Interop.XStoreImage interopStruct)
		{
			Uri = interopStruct.uri.GetString();
			Height = interopStruct.height;
			Width = interopStruct.width;
			Caption = interopStruct.caption.GetString();
			ImagePurposeTag = interopStruct.imagePurposeTag.GetString();
		}
	}
}
