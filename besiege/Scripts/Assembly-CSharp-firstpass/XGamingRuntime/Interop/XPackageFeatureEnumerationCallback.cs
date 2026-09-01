using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal unsafe delegate NativeBool XPackageFeatureEnumerationCallback(IntPtr context, XPackageFeature* feature);
}
