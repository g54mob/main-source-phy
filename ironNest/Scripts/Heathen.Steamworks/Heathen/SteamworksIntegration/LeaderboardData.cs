using System;
using System.ComponentModel;
using System.Text;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LeaderboardData : IEquatable<SteamLeaderboard_t>, IEquatable<ulong>, IEquatable<string>
	{
		[Serializable]
		public struct GetAllRequest
		{
			public bool create;

			public string name;

			public ELeaderboardDisplayType type;

			public ELeaderboardSortMethod sort;
		}

		public string apiName;

		public SteamLeaderboard_t id;

		public readonly string DisplayName => null;

		public readonly bool IsValid => false;

		public readonly int EntryCount => 0;

		public readonly void GetUserEntry(int maxDetailEntries, Action<LeaderboardEntry, bool> callback)
		{
		}

		public readonly void GetTopEntries(int count, int maxDetailEntries, Action<LeaderboardEntry[], bool> callback)
		{
		}

		public readonly void GetTopEntriesWithUser(int topCount, int maxDetailEntries, Action<LeaderboardEntry[], bool> callback)
		{
		}

		public readonly void GetEntries(ELeaderboardDataRequest request, int start, int end, int maxDetailEntries, Action<LeaderboardEntry[], bool> callback)
		{
		}

		public readonly void GetEntries(UserData[] users, int maxDetailEntries, Action<LeaderboardEntry[], bool> callback)
		{
		}

		public readonly void GetAllEntries(int maxDetailEntries, Action<LeaderboardEntry[], bool> callback)
		{
		}

		public readonly void GetEntries(CSteamID[] users, int maxDetailEntries, Action<LeaderboardEntry[], bool> callback)
		{
		}

		public static void Get(string name, Action<LeaderboardData, bool> callback)
		{
		}

		public static LeaderboardData Get(ulong id)
		{
			return default(LeaderboardData);
		}

		public static LeaderboardData Get(SteamLeaderboard_t id)
		{
			return default(LeaderboardData);
		}

		public static void GetAll(GetAllRequest[] commands, Action<LeaderboardData[], EResult> callback)
		{
		}

		private static void BgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
		}

		public static void GetOrCreate(string name, ELeaderboardDisplayType displayType, ELeaderboardSortMethod sortMethod, Action<LeaderboardData, bool> callback)
		{
		}

		public readonly void UploadScoreKeepBest(int score, Action<LeaderboardScoreUploaded, bool> callback = null)
		{
		}

		public readonly void UploadScoreForceUpdate(int score, Action<LeaderboardScoreUploaded, bool> callback = null)
		{
		}

		public readonly void UploadScoreKeepBest(int score, int[] details, Action<LeaderboardScoreUploaded, bool> callback = null)
		{
		}

		public readonly void UploadScoreForceUpdate(int score, int[] details, Action<LeaderboardScoreUploaded, bool> callback = null)
		{
		}

		public readonly void UploadScore(int score, ELeaderboardUploadScoreMethod method, Action<LeaderboardScoreUploaded, bool> callback = null)
		{
		}

		public readonly void UploadScore(int score, int[] scoreDetails, ELeaderboardUploadScoreMethod method, Action<LeaderboardScoreUploaded, bool> callback = null)
		{
		}

		public readonly void AttachUgc(string fileName, object jsonObject, Encoding encoding, Action<LeaderboardUgcSet, bool> callback = null)
		{
		}

		public readonly void AttachUgc(string fileName, object jsonObject, Action<LeaderboardUgcSet, bool> callback = null)
		{
		}

		public readonly void ForceUploadScore(string score)
		{
		}

		public readonly void ForceUploadScore(int score)
		{
		}

		public readonly void ForceUploadScore(int score, int[] details)
		{
		}

		public readonly void KeepBestUploadScore(string score)
		{
		}

		public readonly void KeepBestUploadScore(int score)
		{
		}

		public readonly void KeepBestUploadScore(int score, int[] details)
		{
		}

		public override readonly string ToString()
		{
			return null;
		}

		public override readonly int GetHashCode()
		{
			return 0;
		}

		public override readonly bool Equals(object obj)
		{
			return false;
		}

		public readonly bool Equals(SteamLeaderboard_t other)
		{
			return false;
		}

		public readonly bool Equals(ulong other)
		{
			return false;
		}

		public readonly bool Equals(string other)
		{
			return false;
		}

		public static bool operator ==(LeaderboardData l, LeaderboardData r)
		{
			return false;
		}

		public static bool operator ==(LeaderboardData l, ulong r)
		{
			return false;
		}

		public static bool operator ==(LeaderboardData l, string r)
		{
			return false;
		}

		public static bool operator ==(LeaderboardData l, SteamLeaderboard_t r)
		{
			return false;
		}

		public static bool operator !=(LeaderboardData l, LeaderboardData r)
		{
			return false;
		}

		public static bool operator !=(LeaderboardData l, ulong r)
		{
			return false;
		}

		public static bool operator !=(LeaderboardData l, string r)
		{
			return false;
		}

		public static bool operator !=(LeaderboardData l, SteamLeaderboard_t r)
		{
			return false;
		}

		public static implicit operator ulong(LeaderboardData c)
		{
			return 0uL;
		}

		public static implicit operator LeaderboardData(ulong id)
		{
			return default(LeaderboardData);
		}

		public static implicit operator SteamLeaderboard_t(LeaderboardData c)
		{
			return default(SteamLeaderboard_t);
		}

		public static implicit operator LeaderboardData(SteamLeaderboard_t id)
		{
			return default(LeaderboardData);
		}

		public static implicit operator string(LeaderboardData c)
		{
			return null;
		}
	}
}
