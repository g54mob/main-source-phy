namespace XGamingRuntime.Interop
{
	internal struct XblVerifyStringResult
	{
		internal readonly XblVerifyStringResultCode resultCode;

		internal readonly UTF8StringPtr firstOffendingSubstring;
	}
}
