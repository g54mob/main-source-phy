using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate void XblPresenceDevicePresenceChangedHandler(IntPtr context, ulong xuid, XblPresenceDeviceType deviceType, [MarshalAs(UnmanagedType.U1)] bool isUserLoggedOnDevice);
}
