using System;
using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamWorkshopItemDetailData), "Date Updated", "settings")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamWorkshopItemDetailDataEvents))]
	[RequireComponent(typeof(SteamWorkshopItemDetailData))]
	public class SteamWorkshopItemDetailModifiedDate : MonoBehaviour
	{
		[Serializable]
		public class Settings
		{
			public string format;

			public TextMeshProUGUI label;
		}

		public Settings settings;

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
