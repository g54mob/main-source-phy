namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerManagerMember
	{
		internal readonly uint MemberId;

		internal readonly UTF8StringPtr TeamId;

		internal readonly UTF8StringPtr InitialTeam;

		internal readonly ulong Xuid;

		internal readonly UTF8StringPtr DebugGamertag;

		internal readonly NativeBool IsLocal;

		internal readonly NativeBool IsInLobby;

		internal readonly NativeBool IsInGame;

		internal readonly XblMultiplayerSessionMemberStatus Status;

		internal readonly UTF8StringPtr ConnectionAddress;

		internal readonly UTF8StringPtr PropertiesJson;

		internal readonly UTF8StringPtr DeviceToken;

		internal XblMultiplayerManagerMember(XGamingRuntime.XblMultiplayerManagerMember publicObject, DisposableCollection disposableCollection)
		{
			MemberId = publicObject.MemberId;
			TeamId = new UTF8StringPtr(publicObject.TeamId, disposableCollection);
			InitialTeam = new UTF8StringPtr(publicObject.InitialTeam, disposableCollection);
			Xuid = publicObject.Xuid;
			DebugGamertag = new UTF8StringPtr(publicObject.DebugGamertag, disposableCollection);
			IsLocal = new NativeBool(publicObject.IsLocal);
			IsInLobby = new NativeBool(publicObject.IsInLobby);
			IsInGame = new NativeBool(publicObject.IsInGame);
			Status = publicObject.Status;
			ConnectionAddress = new UTF8StringPtr(publicObject.ConnectionAddress, disposableCollection);
			PropertiesJson = new UTF8StringPtr(publicObject.PropertiesJson, disposableCollection);
			DeviceToken = new UTF8StringPtr(publicObject.DeviceToken, disposableCollection);
		}
	}
}
