using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblGuid
	{
		public string Value { get; private set; }

		internal XblGuid(XGamingRuntime.Interop.XblGuid interopStruct)
		{
			Value = interopStruct.GetValue();
		}
	}
}
