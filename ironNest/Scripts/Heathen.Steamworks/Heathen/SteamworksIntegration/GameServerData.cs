using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class GameServerData : gameserveritem_t
	{
		public List<StringKeyValuePair> rules;

		public List<ServerPlayerEntry> players;

		public UnityEvent evtDataUpdated;

		public string IpAddress => null;

		public ushort QueryPort => 0;

		public ushort ConnectionPort => 0;

		public CSteamID SteamId => default(CSteamID);

		public AppId_t AppId => default(AppId_t);

		public bool UsesPassword => false;

		public bool IsSecured => false;

		public int PlayerCount => 0;

		public int BotPlayerCount => 0;

		public int MaxPlayerCount => 0;

		public int Ping => 0;

		public int Version => 0;

		public DateTime LastPlayed => default(DateTime);

		public string Description
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string Tags
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string Name
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string Map
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string Directory
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public GameServerData(gameserveritem_t item)
		{
		}

		public void Update(gameserveritem_t item)
		{
		}
	}
}
