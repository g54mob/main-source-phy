using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct ItemDetail
	{
		public SteamItemDetails_t ItemDetails;

		public ItemProperty[] properties;

		public string dynamicProperties;

		public ItemTag[] tags;

		public SteamItemInstanceID_t ItemId => default(SteamItemInstanceID_t);

		public ItemData Definition => default(ItemData);

		public ushort Quantity => 0;

		public ESteamItemFlags Flags => default(ESteamItemFlags);
	}
}
