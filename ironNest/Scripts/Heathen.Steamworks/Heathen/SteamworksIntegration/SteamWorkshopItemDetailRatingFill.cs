using UnityEngine;
using UnityEngine.UI;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamWorkshopItemDetailData), "Ratings Fill", "image")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamWorkshopItemDetailDataEvents))]
	[RequireComponent(typeof(SteamWorkshopItemDetailData))]
	public class SteamWorkshopItemDetailRatingFill : MonoBehaviour
	{
		public Image image;

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
