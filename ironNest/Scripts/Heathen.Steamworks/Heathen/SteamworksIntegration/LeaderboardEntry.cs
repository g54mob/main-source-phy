using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	public class LeaderboardEntry
	{
		public LeaderboardEntry_t Entry;

		public int[] Details;

		public string CashedUgcFileName;

		public UnityEvent<string> EvtUgcDownloaded;

		public UserData User => default(UserData);

		public int Rank => 0;

		public int Score => 0;

		public UGCHandle_t UgcHandle => default(UGCHandle_t);

		public int this[int index] => 0;

		public bool HasCashedUgcFileName => false;

		public void GetAttachedUgc<T>(Action<T, bool> callback = null)
		{
		}

		public bool StartUgcDownload(uint priority = 0u)
		{
			return false;
		}

		public bool StartUgcDownload(uint priority, Action<RemoteStorageDownloadUGCResult_t, bool> callback)
		{
			return false;
		}

		public float UgcDownloadProgress()
		{
			return 0f;
		}

		private void HandleUgcDownloadResult(RemoteStorageDownloadUGCResult_t param, bool bIOFailure)
		{
		}
	}
}
