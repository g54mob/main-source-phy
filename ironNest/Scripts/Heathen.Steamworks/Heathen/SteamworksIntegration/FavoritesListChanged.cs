using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct FavoritesListChanged
	{
		public string ip;

		public uint queryPort;

		public uint connectionPort;

		public AppData app;

		public uint flags;

		public bool add;

		public AccountID_t accountId;

		public FavoritesListChanged(string ip, uint queryPort, uint connectionPort, AppData app, uint flags, bool add, AccountID_t accountId)
		{
			this.ip = null;
			this.queryPort = 0u;
			this.connectionPort = 0u;
			this.app = default(AppData);
			this.flags = 0u;
			this.add = false;
			this.accountId = default(AccountID_t);
		}
	}
}
