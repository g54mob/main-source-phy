using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerSessionReference
	{
		public string Scid { get; private set; }

		public string SessionTemplateName { get; private set; }

		public string SessionName { get; private set; }

		internal XblMultiplayerSessionReference(XGamingRuntime.Interop.XblMultiplayerSessionReference interopStruct)
		{
			Scid = interopStruct.GetScid();
			SessionTemplateName = interopStruct.GetSessionTemplateName();
			SessionName = interopStruct.GetSessionName();
		}
	}
}
