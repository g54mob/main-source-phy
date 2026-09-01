using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamWorkshopItemDetailData), "Up Votes", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamWorkshopItemDetailDataEvents))]
	[RequireComponent(typeof(SteamWorkshopItemDetailData))]
	public class SteamWorkshopItemDetailUpVoteLabel : MonoBehaviour
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
