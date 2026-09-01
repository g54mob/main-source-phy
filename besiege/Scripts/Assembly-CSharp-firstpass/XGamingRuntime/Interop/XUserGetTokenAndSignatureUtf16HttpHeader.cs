using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XUserGetTokenAndSignatureUtf16HttpHeader
	{
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;
	}
}
