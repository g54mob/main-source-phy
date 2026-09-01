using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal unsafe delegate NativeBool XPackageChunkAvailabilityCallback(IntPtr context, XPackageChunkSelector* selector, XPackageChunkAvailability availability);
}
