using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamAchievementData), "Descriptions", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamAchievementData))]
	public class SteamAchievementDescription : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamAchievementData _mData;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public void Refresh()
		{
		}
	}
}
