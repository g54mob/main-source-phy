using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLeaderboardData), "User Entries", "entryUI")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLeaderboardData))]
	[RequireComponent(typeof(SteamLeaderboardDataEvents))]
	public class SteamLeaderboardUserEntry : MonoBehaviour
	{
		public SteamLeaderboardEntryUI entryUI;

		private SteamLeaderboardData _mInspector;

		private SteamLeaderboardDataEvents _mEvents;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public void Refresh()
		{
		}

		private void HandleRankChange(LeaderboardScoreUploaded arg0)
		{
		}
	}
}
