using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LobbyGameServer
	{
		public CSteamID id;

		public uint ipAddress;

		public ushort port;

		public string IpAddress
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
	}
}
