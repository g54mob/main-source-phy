using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct FriendGameInfo
	{
		public FriendGameInfo_t Data;

		public readonly GameData Game => default(GameData);

		public readonly string IpAddress => null;

		public readonly uint IpInt => 0u;

		public readonly ushort GamePort => 0;

		public readonly ushort QueryPort => 0;

		public readonly LobbyData Lobby => default(LobbyData);

		public static implicit operator FriendGameInfo(FriendGameInfo_t native)
		{
			return default(FriendGameInfo);
		}

		public static implicit operator FriendGameInfo_t(FriendGameInfo heathen)
		{
			return default(FriendGameInfo_t);
		}
	}
}
