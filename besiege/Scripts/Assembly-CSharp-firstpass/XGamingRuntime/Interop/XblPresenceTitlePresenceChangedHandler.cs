using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate void XblPresenceTitlePresenceChangedHandler(IntPtr context, ulong xuid, uint titleId, XblPresenceTitleState titleState);
}
