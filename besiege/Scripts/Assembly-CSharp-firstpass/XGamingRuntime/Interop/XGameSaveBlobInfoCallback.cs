using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate NativeBool XGameSaveBlobInfoCallback(XGameSaveBlobInfo info, IntPtr context);
}
