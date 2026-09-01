using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	public static class XboxLiveGlobal
	{
		[DllImport("Microsoft.Xbox.Services.141.GDK.C.Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		[return: NativeTypeName("HRESULT")]
		public unsafe static extern int XblGetScid([NativeTypeName("const char **")] sbyte** scid);
	}
}
