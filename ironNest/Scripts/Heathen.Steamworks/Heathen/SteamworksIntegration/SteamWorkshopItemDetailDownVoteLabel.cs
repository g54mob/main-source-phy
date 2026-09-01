using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamWorkshopItemDetailData), "Down Votes", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamWorkshopItemDetailDataEvents))]
	[RequireComponent(typeof(SteamWorkshopItemDetailData))]
	public class SteamWorkshopItemDetailDownVoteLabel : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamWorkshopItemDetailData _mInspector;

		private SteamWorkshopItemDetailDataEvents _mEvents;

		private void Awake()
		{
		}

		private void HandleChanged()
		{
		}
	}
}
