using System;
using System.Collections.Generic;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class InventorySettings
	{
		public List<ItemDefinitionSettings> items;

		private InventoryChangedEvent _eventChanged;

		public Currency.Code LocalCurrencyCode => default(Currency.Code);

		public string LocalCurrencySymbol => null;

		public InventoryChangedEvent EventChanged => null;

		private void HandleItemResults(InventoryResult results)
		{
		}
	}
}
