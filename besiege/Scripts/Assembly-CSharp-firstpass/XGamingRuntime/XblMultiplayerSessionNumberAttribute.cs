using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerSessionNumberAttribute
	{
		public string Name { get; private set; }

		public double Value { get; private set; }

		public XblMultiplayerSessionNumberAttribute(string name, double value)
		{
			Name = name;
			Value = value;
		}

		internal XblMultiplayerSessionNumberAttribute(XGamingRuntime.Interop.XblMultiplayerSessionNumberAttribute interopStruct)
		{
			Name = interopStruct.GetName();
			Value = interopStruct.value;
		}
	}
}
