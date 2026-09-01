using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LobbyEnter
	{
		public LobbyEnter_t Data;

		public LobbyData Lobby => default(LobbyData);

		public EChatRoomEnterResponse Response => default(EChatRoomEnterResponse);

		public bool Locked => false;

		public static implicit operator LobbyEnter(LobbyEnter_t native)
		{
			return default(LobbyEnter);
		}

		public static implicit operator LobbyEnter_t(LobbyEnter heathen)
		{
			return default(LobbyEnter_t);
		}
	}
}
