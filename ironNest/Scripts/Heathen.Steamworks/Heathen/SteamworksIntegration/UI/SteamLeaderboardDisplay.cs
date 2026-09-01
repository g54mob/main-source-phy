using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.UI
{
	[ModularComponent(typeof(SteamLeaderboardData), "Display", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLeaderboardData))]
	public class SteamLeaderboardDisplay : MonoBehaviour
	{
		[ElementField("Display", 0)]
		public bool alwaysIncludePlayer;

		[ElementField("Display", 0)]
		public Transform collectionRoot;

		[TemplateField("Display", 0)]
		public GameObject entryTemplate;

		private SteamLeaderboardData _mInspector;

		private readonly List<GameObject> _createdRecords;

		private void Awake()
		{
		}

		public void GetTopEntries(int count)
		{
		}

		public void GetNearByEntries(int count)
		{
		}

		public void GetTopEntriesWithUser(int topCount)
		{
		}

		public void GetEntries(ELeaderboardDataRequest request, int start, int end, int maxDetailEntries)
		{
		}

		private void HandleBoardResults(LeaderboardEntry[] entries, bool ioError)
		{
		}

		public void Display(LeaderboardEntry[] entries)
		{
		}
	}
}
