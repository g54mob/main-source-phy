using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct FavoriteGame
	{
		public AppId_t appId;

		public uint ipAddress;

		public ushort connectionPort;

		public ushort queryPort;

		public DateTime LastPlayedOnServer;

		public bool isHistory;

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
