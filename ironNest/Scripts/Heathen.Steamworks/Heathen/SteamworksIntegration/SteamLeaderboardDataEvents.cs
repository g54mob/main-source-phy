using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[ModularEvents(typeof(SteamLeaderboardData))]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLeaderboardData))]
	public class SteamLeaderboardDataEvents : MonoBehaviour
	{
		[EventField]
		public UnityEvent onChange;

		[EventField]
		public UnityEvent onFindOrCreate;

		[EventField]
		public UnityEvent onFindOrCreateFailure;

		[EventField]
		public UnityEvent<LeaderboardScoreUploaded> onScoreUploaded;

		[EventField]
		public UnityEvent<LeaderboardScoreUploaded> onRankChanged;

		[FormerlySerializedAs("onUGCAttached")]
		[EventField]
		public UnityEvent<LeaderboardUgcSet> onUgcAttached;

		private SteamLeaderboardData _mInspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleUgcAttached(LeaderboardUgcSet arg0, bool arg1)
		{
		}

		private void HandleScoreUpload(LeaderboardScoreUploaded arg0, bool arg1)
		{
		}
	}
}
