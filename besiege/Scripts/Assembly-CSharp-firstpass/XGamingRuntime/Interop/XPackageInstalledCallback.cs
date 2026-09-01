using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal unsafe delegate void XPackageInstalledCallback(IntPtr context, XPackageDetails* details);
}
