using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Leaderboard")]
	[HelpURL("https://heathen.group/kb/leaderboards/")]
	public class SteamLeaderboardData : MonoBehaviour
	{
		public enum LeaderboardSortMethod
		{
			TopIsLowestScore = 1,
			TopIsHighestScore = 2
		}

		public enum LeaderboardDisplayType
		{
			Numeric = 1,
			TimeSeconds = 2,
			TimeMilliSeconds = 3
		}

		public string apiName;

		public bool createIfMissing;

		public LeaderboardDisplayType createAsDisplay;

		public LeaderboardSortMethod createWithSort;

		[FormerlySerializedAs("_delegates")]
		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<string> delegates;

		private LeaderboardData _data;

		private SteamLeaderboardDataEvents _events;

		public LeaderboardData Data
		{
			get
			{
				return default(LeaderboardData);
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		private void Interface_OnReady()
		{
		}

		private void Start()
		{
		}
	}
}
