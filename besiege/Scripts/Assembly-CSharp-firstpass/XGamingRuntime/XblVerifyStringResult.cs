using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblVerifyStringResult
	{
		public XblVerifyStringResultCode ResultCode { get; private set; }

		public string FirstOffendingSubstring { get; private set; }

		internal XblVerifyStringResult(XGamingRuntime.Interop.XblVerifyStringResult interopStruct)
		{
			ResultCode = interopStruct.resultCode;
			FirstOffendingSubstring = interopStruct.firstOffendingSubstring.GetString();
		}
	}
}
