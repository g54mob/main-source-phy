using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate NativeBool XGameSaveContainerInfoCallback(XGameSaveContainerInfo info, IntPtr context);
}
