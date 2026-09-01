namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerActivityInfo
	{
		internal readonly ulong xuid;

		internal readonly UTF8StringPtr connectionString;

		internal readonly XblMultiplayerActivityJoinRestriction joinRestriction;

		internal readonly SizeT maxPlayers;

		internal readonly SizeT currentPlayers;

		internal readonly UTF8StringPtr groupId;

		internal readonly XblMultiplayerActivityPlatform platform;

		internal XblMultiplayerActivityInfo(XGamingRuntime.XblMultiplayerActivityInfo publicObject, DisposableCollection disposableCollection)
		{
			xuid = publicObject.Xuid;
			connectionString = new UTF8StringPtr(publicObject.ConnectionString, disposableCollection);
			joinRestriction = publicObject.JoinRestriction;
			maxPlayers = new SizeT(publicObject.MaxPlayers);
			currentPlayers = new SizeT(publicObject.CurrentPlayers);
			groupId = new UTF8StringPtr(publicObject.GroupId, disposableCollection);
			platform = publicObject.Platform;
		}
	}
}
