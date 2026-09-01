using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XUserGetTokenAndSignatureUtf16Data
	{
		public SizeT TokenCount;

		public SizeT SignatureCount;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string Token;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string Signature;
	}
}
