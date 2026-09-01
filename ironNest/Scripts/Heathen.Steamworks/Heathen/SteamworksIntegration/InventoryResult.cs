using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct InventoryResult
	{
		public ItemDetail[] items;

		public EResult result;

		public DateTime Timestamp;
	}
}
