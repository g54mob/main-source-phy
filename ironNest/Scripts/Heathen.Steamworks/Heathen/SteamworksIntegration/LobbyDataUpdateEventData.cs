using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LobbyDataUpdateEventData
	{
		public LobbyData lobby;

		public LobbyMemberData? Member;

		public static implicit operator LobbyDataUpdateEventData(LobbyDataUpdate_t c)
		{
			return default(LobbyDataUpdateEventData);
		}
	}
}
