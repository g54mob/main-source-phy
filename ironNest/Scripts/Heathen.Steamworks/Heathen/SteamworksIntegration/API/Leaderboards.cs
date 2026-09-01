using System;
using System.Collections.Generic;
using System.Text;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration.API
{
	public static class Leaderboards
	{
		private struct AttachUgcRequest
		{
			public LeaderboardData Leaderboard;

			public UGCHandle_t Ugc;

			public Action<LeaderboardUgcSet, bool> Callback;
		}

		private struct DownloadScoreRequest
		{
			public bool UserRequest;

			public CSteamID[] Users;

			public SteamLeaderboard_t Leaderboard;

			public ELeaderboardDataRequest Request;

			public int Start;

			public int End;

			public int MaxDetailsPerEntry;

			public Action<LeaderboardEntry[], bool> Callback;
		}

		private struct UploadScoreRequest
		{
			public LeaderboardData Leaderboard;

			public ELeaderboardUploadScoreMethod Method;

			public int Score;

			public int[] Details;

			public Action<LeaderboardScoreUploaded, bool> Callback;
		}

		private struct FindOrCreateRequest
		{
			public string APIName;

			public bool CreateIfMissing;

			public ELeaderboardDisplayType DisplayType;

			public ELeaderboardSortMethod SortMethod;

			public Action<LeaderboardData, bool> Callback;
		}

		public static class Client
		{
			public static UnityEvent<LeaderboardScoreUploaded, bool> OnScoreUploaded;

			public static UnityEvent<LeaderboardUgcSet, bool> OnUgcAttached;

			private static CallResult<LeaderboardUGCSet_t> _mLeaderboardUgcSetT;

			private static CallResult<LeaderboardScoresDownloaded_t> _mLeaderboardScoresDownloadedT;

			private static CallResult<LeaderboardFindResult_t> _mLeaderboardFindResultT;

			private static CallResult<LeaderboardScoreUploaded_t> _mLeaderboardScoreUploadedT;

			private static Queue<AttachUgcRequest> _ugcQueue;

			private static Queue<DownloadScoreRequest> _downloadQueue;

			private static Queue<UploadScoreRequest> _uploadQueue;

			private static Queue<FindOrCreateRequest> _findOrCreateQueue;

			public static float RequestTimeout { get; set; }

			public static int PendingSetUgcRequests => 0;

			public static int PendingDownloadScoreRequests => 0;

			public static int PendingUploadScoreRequests => 0;

			public static int PendingFindOrCreateRequests => 0;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			private static void ExecuteUgcRequest()
			{
			}

			private static void ExecuteDownloadRequest()
			{
			}

			private static void ExecuteUploadRequest()
			{
			}

			private static void ExecuteFindOrCreateRequest()
			{
			}

			public static void AttachUgc(LeaderboardData leaderboard, UGCHandle_t ugc, Action<LeaderboardUgcSet, bool> callback)
			{
			}

			public static void AttachUgc(LeaderboardData leaderboard, string fileName, byte[] data, Action<LeaderboardUgcSet, bool> callback)
			{
			}

			public static void AttachUgc(LeaderboardData leaderboard, string fileName, object jsonObject, Encoding encoding, Action<LeaderboardUgcSet, bool> callback)
			{
			}

			public static void AttachUgc(LeaderboardData leaderboard, string fileName, string content, Encoding encoding, Action<LeaderboardUgcSet, bool> callback)
			{
			}

			public static void DownloadEntries(LeaderboardData leaderboard, ELeaderboardDataRequest request, int start, int end, int maxDetailsPerEntry, Action<LeaderboardEntry[], bool> callback)
			{
			}

			public static void DownloadEntries(LeaderboardData leaderboard, CSteamID[] users, int maxDetailsPerEntry, Action<LeaderboardEntry[], bool> callback)
			{
			}

			public static void DownloadEntries(LeaderboardData leaderboard, UserData[] users, int maxDetailsPerEntry, Action<LeaderboardEntry[], bool> callback)
			{
			}

			public static void Find(string leaderboardName, Action<LeaderboardData, bool> callback)
			{
			}

			public static void FindOrCreate(string leaderboardName, ELeaderboardSortMethod sortingMethod, ELeaderboardDisplayType displayType, Action<LeaderboardData, bool> callback)
			{
			}

			public static ELeaderboardDisplayType GetDisplayType(LeaderboardData leaderboard)
			{
				return default(ELeaderboardDisplayType);
			}

			public static int GetEntryCount(LeaderboardData leaderboard)
			{
				return 0;
			}

			public static string GetName(LeaderboardData leaderboard)
			{
				return null;
			}

			public static ELeaderboardSortMethod GetSortMethod(LeaderboardData leaderboard)
			{
				return default(ELeaderboardSortMethod);
			}

			public static void UploadScore(LeaderboardData leaderboard, ELeaderboardUploadScoreMethod method, int score, int[] details, Action<LeaderboardScoreUploaded, bool> callback = null)
			{
			}

			private static LeaderboardEntry[] ProcessScoresDownloaded(LeaderboardScoresDownloaded_t param, bool bIOFailure, int maxDetailEntries)
			{
				return null;
			}
		}
	}
}
