using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct StringFilter
	{
		public string key;

		public string value;

		public ELobbyComparison comparison;
	}
}
