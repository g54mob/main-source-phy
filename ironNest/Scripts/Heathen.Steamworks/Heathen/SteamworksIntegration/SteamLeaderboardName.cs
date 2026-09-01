using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLeaderboardData), "Names", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLeaderboardData))]
	public class SteamLeaderboardName : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamLeaderboardData _mInspector;

		private SteamLeaderboardDataEvents _mEvents;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleOnChanged()
		{
		}
	}
}
