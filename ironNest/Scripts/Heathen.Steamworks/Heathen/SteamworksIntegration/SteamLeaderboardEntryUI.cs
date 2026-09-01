using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[RequireComponent(typeof(SteamUserData))]
	[HelpURL(null)]
	public class SteamLeaderboardEntryUI : MonoBehaviour, ILeaderboardEntryDisplay
	{
		[SerializeField]
		private TextMeshProUGUI score;

		[SerializeField]
		private TextMeshProUGUI rank;

		private SteamUserData _userData;

		private LeaderboardEntry _entry;

		public LeaderboardEntry Entry
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		private void SetEntry(LeaderboardEntry entry)
		{
		}
	}
}
