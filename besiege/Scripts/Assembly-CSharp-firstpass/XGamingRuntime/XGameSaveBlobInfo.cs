using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XGameSaveBlobInfo
	{
		public string Name { get; private set; }

		public uint Size { get; private set; }

		internal XGameSaveBlobInfo(XGamingRuntime.Interop.XGameSaveBlobInfo interopHandle)
		{
			Name = interopHandle.Name.GetString();
			Size = interopHandle.Size;
		}
	}
}
