namespace XGamingRuntime
{
	public class XUserGetTokenAndSignatureUtf16Data
	{
		public string Token { get; private set; }

		public string Signature { get; private set; }

		internal XUserGetTokenAndSignatureUtf16Data(string token, string signature)
		{
			Token = token;
			Signature = signature;
		}
	}
}
