using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamWorkshopItemDetailData), "Titles", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamWorkshopItemDetailDataEvents))]
	[RequireComponent(typeof(SteamWorkshopItemDetailData))]
	public class SteamWorkshopItemDetailTitle : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamWorkshopItemDetailData _inspector;

		private SteamWorkshopItemDetailDataEvents _events;

		private void Awake()
		{
		}

		private void HandleChanged()
		{
		}
	}
}
