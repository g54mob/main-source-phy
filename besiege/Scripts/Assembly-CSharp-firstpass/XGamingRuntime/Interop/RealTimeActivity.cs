using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	public static class RealTimeActivity
	{
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void XblRealTimeActivityConnectionStateChangeHandler(IntPtr context, XblRealTimeActivityConnectionState connectionState);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void XblRealTimeActivityResyncHandler(IntPtr context);

		[DllImport("Microsoft.Xbox.Services.141.GDK.C.Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		[return: NativeTypeName("XblFunctionContext")]
		public static extern int XblRealTimeActivityAddConnectionStateChangeHandler([NativeTypeName("XblContextHandle")] IntPtr xboxLiveContext, [NativeTypeName("XblRealTimeActivityConnectionStateChangeHandler *")] XblRealTimeActivityConnectionStateChangeHandler handler, IntPtr context);

		[DllImport("Microsoft.Xbox.Services.141.GDK.C.Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		[return: NativeTypeName("HRESULT")]
		public static extern int XblRealTimeActivityRemoveConnectionStateChangeHandler([NativeTypeName("XblContextHandle")] IntPtr xboxLiveContext, [NativeTypeName("XblFunctionContext")] int token);

		[DllImport("Microsoft.Xbox.Services.141.GDK.C.Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		[return: NativeTypeName("XblFunctionContext")]
		public static extern int XblRealTimeActivityAddResyncHandler([NativeTypeName("XblContextHandle")] IntPtr xboxLiveContext, [NativeTypeName("XblRealTimeActivityResyncHandler *")] XblRealTimeActivityResyncHandler handler, IntPtr context);

		[DllImport("Microsoft.Xbox.Services.141.GDK.C.Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		[return: NativeTypeName("HRESULT")]
		public static extern int XblRealTimeActivityRemoveResyncHandler([NativeTypeName("XblContextHandle")] IntPtr xboxLiveContext, [NativeTypeName("XblFunctionContext")] int token);
	}
}
