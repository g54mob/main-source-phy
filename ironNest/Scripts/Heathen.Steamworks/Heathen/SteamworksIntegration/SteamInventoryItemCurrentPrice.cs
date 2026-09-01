using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamInventoryItemData), "Current Prices", "label")]
	[RequireComponent(typeof(SteamInventoryItemDataEvents))]
	[RequireComponent(typeof(SteamInventoryItemData))]
	[AddComponentMenu(null)]
	public class SteamInventoryItemCurrentPrice : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamInventoryItemData _mInspector;

		private SteamInventoryItemDataEvents _mEvents;

		private void Awake()
		{
		}

		private void HandleChange()
		{
		}
	}
}
