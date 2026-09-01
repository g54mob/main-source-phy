using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct ClanChatMsg
	{
		public ChatRoom room;

		public EChatEntryType type;

		public string message;

		public UserData user;
	}
}
