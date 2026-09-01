using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate void XPackageInstallationProgressCallback(IntPtr context, XPackageInstallationMonitorHandle monitor);
}
