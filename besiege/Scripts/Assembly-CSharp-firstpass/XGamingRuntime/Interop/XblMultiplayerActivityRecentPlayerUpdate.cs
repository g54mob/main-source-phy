namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerActivityRecentPlayerUpdate
	{
		internal readonly ulong xuid;

		internal readonly XblMultiplayerActivityEncounterType encounterType;

		internal XblMultiplayerActivityRecentPlayerUpdate(XGamingRuntime.XblMultiplayerActivityRecentPlayerUpdate publicObject)
		{
			xuid = publicObject.Xuid;
			encounterType = publicObject.EncounterType;
		}
	}
}
