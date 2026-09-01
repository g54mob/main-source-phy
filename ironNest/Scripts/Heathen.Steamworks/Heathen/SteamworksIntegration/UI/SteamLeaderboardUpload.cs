using System.Collections.Generic;
using UnityEngine;

namespace Heathen.SteamworksIntegration.UI
{
	[ModularComponent(typeof(SteamLeaderboardData), "Upload", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLeaderboardData))]
	public class SteamLeaderboardUpload : MonoBehaviour
	{
		public enum Mode
		{
			KeepBest = 0,
			ForceUpdate = 1
		}

		[SettingsField(0, false, "Upload")]
		public Mode mode;

		[SettingsField(0, false, "Upload")]
		public int score;

		[SettingsField(0, false, "Upload")]
		public List<int> details;

		private SteamLeaderboardData _mInspector;

		private SteamLeaderboardDataEvents _mEvents;

		private void Awake()
		{
		}

		public void Upload()
		{
		}

		public void Upload<T>(T attachment)
		{
		}
	}
}
