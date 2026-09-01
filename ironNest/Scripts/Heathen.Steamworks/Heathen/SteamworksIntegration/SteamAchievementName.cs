using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamAchievementData), "Names", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamAchievementData))]
	public class SteamAchievementName : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamAchievementData _mData;

		private void Awake()
		{
		}

		public void Refresh()
		{
		}
	}
}
