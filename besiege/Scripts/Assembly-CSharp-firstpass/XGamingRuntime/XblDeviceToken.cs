using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblDeviceToken
	{
		public string Value { get; private set; }

		internal XblDeviceToken(XGamingRuntime.Interop.XblDeviceToken interopStruct)
		{
			Value = interopStruct.GetValue();
		}
	}
}
