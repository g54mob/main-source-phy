using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLeaderboardData), "Ranks", "label")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLeaderboardData))]
	[RequireComponent(typeof(SteamLeaderboardDataEvents))]
	public class SteamLeaderboardRank : MonoBehaviour
	{
		public TextMeshProUGUI label;

		private SteamLeaderboardData _inspector;

		private SteamLeaderboardDataEvents _events;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleRankChange(LeaderboardScoreUploaded arg0)
		{
		}

		private void HandleOnChanged()
		{
		}
	}
}
