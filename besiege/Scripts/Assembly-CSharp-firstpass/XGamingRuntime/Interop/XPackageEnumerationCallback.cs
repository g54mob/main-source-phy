using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal unsafe delegate NativeBool XPackageEnumerationCallback(IntPtr context, XPackageDetails* details);
}
