using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XGameSaveBlob
	{
		public XGameSaveBlobInfo info;

		private IntPtr data;

		public void CopyData(byte[] buffer)
		{
			Marshal.Copy(data, buffer, 0, Convert.ToInt32(info.Size));
		}
	}
}
