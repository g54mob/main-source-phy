using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerActivityRecentPlayerUpdate
	{
		public ulong Xuid { get; set; }

		public XblMultiplayerActivityEncounterType EncounterType { get; set; }

		public XblMultiplayerActivityRecentPlayerUpdate()
		{
		}

		internal XblMultiplayerActivityRecentPlayerUpdate(XGamingRuntime.Interop.XblMultiplayerActivityRecentPlayerUpdate interopStruct)
		{
			Xuid = interopStruct.xuid;
			EncounterType = interopStruct.encounterType;
		}
	}
}
