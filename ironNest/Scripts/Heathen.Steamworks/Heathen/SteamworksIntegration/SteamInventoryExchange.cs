using System;
using System.Collections.Generic;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamInventoryItemData), "Exchange", null)]
	[RequireComponent(typeof(SteamInventoryItemData))]
	[AddComponentMenu(null)]
	public class SteamInventoryExchange : MonoBehaviour
	{
		[Serializable]
		public struct RecipeEntry
		{
			public int id;

			public uint count;
		}

		[SettingsField(0, false, null)]
		public List<RecipeEntry> recipe;

		private SteamInventoryItemData _mInspector;

		private SteamInventoryItemDataEvents _mEvents;

		public bool IsCanExchange => false;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleInventoryUpdated(InventoryResult _)
		{
		}

		public void RefreshCanExchange()
		{
		}

		public void Exchange()
		{
		}
	}
}
