using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamInventoryItemData), "Quantities", "label")]
	[RequireComponent(typeof(SteamInventoryItemDataEvents))]
	[RequireComponent(typeof(SteamInventoryItemData))]
	[AddComponentMenu(null)]
	public class SteamInventoryItemQuantity : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamInventoryItemData _mInspector;

		private SteamInventoryItemDataEvents _mEvents;

		private void Awake()
		{
		}

		private void HandleStateChange()
		{
		}
	}
}
