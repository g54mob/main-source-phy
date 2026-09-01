using System;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LobbyMessagePayload
	{
		public ulong id;

		public byte[] data;

		public byte[] inventory;
	}
}
