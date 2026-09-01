using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerActivityInfo
	{
		public ulong Xuid { get; set; }

		public string ConnectionString { get; set; }

		public XblMultiplayerActivityJoinRestriction JoinRestriction { get; set; }

		public int MaxPlayers { get; set; }

		public int CurrentPlayers { get; set; }

		public string GroupId { get; set; }

		public XblMultiplayerActivityPlatform Platform { get; set; }

		public XblMultiplayerActivityInfo()
		{
		}

		internal XblMultiplayerActivityInfo(XGamingRuntime.Interop.XblMultiplayerActivityInfo interopStruct)
		{
			Xuid = interopStruct.xuid;
			ConnectionString = interopStruct.connectionString.GetString();
			JoinRestriction = interopStruct.joinRestriction;
			MaxPlayers = interopStruct.maxPlayers.ToInt32();
			CurrentPlayers = interopStruct.currentPlayers.ToInt32();
			GroupId = interopStruct.groupId.GetString();
			Platform = interopStruct.platform;
		}
	}
}
