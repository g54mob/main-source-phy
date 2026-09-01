using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerSessionTag
	{
		public string Value { get; private set; }

		public XblMultiplayerSessionTag(string value)
		{
			Value = value;
		}

		internal XblMultiplayerSessionTag(XGamingRuntime.Interop.XblMultiplayerSessionTag interopStruct)
		{
			Value = interopStruct.GetValue();
		}
	}
}
