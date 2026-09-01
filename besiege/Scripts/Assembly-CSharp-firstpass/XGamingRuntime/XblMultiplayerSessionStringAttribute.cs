using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerSessionStringAttribute
	{
		public string Name { get; private set; }

		public string Value { get; private set; }

		public XblMultiplayerSessionStringAttribute(string name, string value)
		{
			Name = name;
			Value = value;
		}

		internal XblMultiplayerSessionStringAttribute(XGamingRuntime.Interop.XblMultiplayerSessionStringAttribute interopStruct)
		{
			Name = interopStruct.GetName();
			Value = interopStruct.GetValue();
		}
	}
}
