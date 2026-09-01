using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamInventoryItemData), "Base Prices", "label")]
	[RequireComponent(typeof(SteamInventoryItemDataEvents))]
	[RequireComponent(typeof(SteamInventoryItemData))]
	[AddComponentMenu(null)]
	public class SteamInventoryItemBasePrice : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamInventoryItemData _inspector;

		private SteamInventoryItemDataEvents _events;

		private void Awake()
		{
		}

		private void HandleChange()
		{
		}
	}
}
