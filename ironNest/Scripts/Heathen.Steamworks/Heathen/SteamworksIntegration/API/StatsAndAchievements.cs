using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration.API
{
	public static class StatsAndAchievements
	{
		public static class Client
		{
			private class ImageRequestCallbackLink
			{
				public bool IsAchievement;

				public string APIName;

				public Action<Texture2D> Callback;
			}

			private static List<ImageRequestCallbackLink> _pendingLinks;

			private static Dictionary<int, Texture2D> _loadedImages;

			private static UserStatsReceivedEvent _onUserStatsReceived;

			private static UserStatsUnloadedEvent _onUserStatsUnloaded;

			private static UserStatsStoredEvent _onUserStatsStored;

			private static UserAchievementStoredEvent _onUserAchievementStored;

			private static UnityEvent<string, bool> _onAchievementStatusChanged;

			private static Callback<UserAchievementIconFetched_t> _userAchievementIconFetchedT;

			private static CallResult<NumberOfCurrentPlayers_t> _numberOfCurrentPlayersT;

			private static CallResult<GlobalAchievementPercentagesReady_t> _globalAchievementPercentagesReadyT;

			private static CallResult<GlobalStatsReceived_t> _globalStatsReceivedT;

			private static CallResult<UserStatsReceived_t> _userStatsReceivedT2;

			public static UnityEvent<string, bool> OnAchievementStatusChanged => null;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static bool ClearAchievement(string achievementApiName)
			{
				return false;
			}

			public static bool GetAchievement(string achievementApiName, out bool achieved)
			{
				achieved = default(bool);
				return false;
			}

			public static bool GetAchievement(string achievementApiName, out bool achieved, out DateTime unlockTime)
			{
				achieved = default(bool);
				unlockTime = default(DateTime);
				return false;
			}

			public static bool GetAchievement(UserData userId, string achievementApiName, out bool achieved)
			{
				achieved = default(bool);
				return false;
			}

			public static bool GetAchievement(UserData userId, string achievementApiName, out bool achieved, out DateTime unlockTime)
			{
				achieved = default(bool);
				unlockTime = default(DateTime);
				return false;
			}

			public static bool GetAchievementAchievedPercent(string achievementApiName, out float percent)
			{
				percent = default(float);
				return false;
			}

			public static string GetAchievementDisplayAttribute(string achievementApiName, string key)
			{
				return null;
			}

			public static string GetAchievementDisplayAttribute(string achievementApiName, AchievementAttributes attribute)
			{
				return null;
			}

			public static bool GetAchievementIcon(string achievementApiName, Action<Texture2D> callback)
			{
				return false;
			}

			public static string GetAchievementName(uint index)
			{
				return null;
			}

			public static string[] GetAchievementNames()
			{
				return null;
			}

			public static bool GetGlobalStat(string statApiName, out long data)
			{
				data = default(long);
				return false;
			}

			public static bool GetGlobalStat(string statApiName, out double data)
			{
				data = default(double);
				return false;
			}

			public static void GetMostAchievedAchievements(Action<EResult, (AchievementData achievement, float percentage)[], bool> callback)
			{
			}

			public static int GetMostAchievedAchievementInfo(out string achievementApiName, out float percent, out bool achieved)
			{
				achievementApiName = null;
				percent = default(float);
				achieved = default(bool);
				return 0;
			}

			public static int GetNextMostAchievedAchievementInfo(int previousIndex, out string achievementApiName, out float percent, out bool achieved)
			{
				achievementApiName = null;
				percent = default(float);
				achieved = default(bool);
				return 0;
			}

			public static uint GetNumAchievements()
			{
				return 0u;
			}

			public static void GetNumberOfCurrentPlayers(Action<NumberOfCurrentPlayers_t, bool> callback)
			{
			}

			public static bool GetStat(string statApiName, out int data)
			{
				data = default(int);
				return false;
			}

			public static bool GetStat(string statApiName, out float data)
			{
				data = default(float);
				return false;
			}

			public static bool GetStat(UserData userId, string statApiName, out int data)
			{
				data = default(int);
				return false;
			}

			public static bool GetStat(UserData userId, string statApiName, out float data)
			{
				data = default(float);
				return false;
			}

			public static bool IndicateAchievementProgress(string achievementApiName, uint progress, uint maxProgress)
			{
				return false;
			}

			public static void RequestGlobalAchievementPercentages(Action<GlobalAchievementPercentagesReady_t, bool> callback)
			{
			}

			public static void RequestGlobalStats(int historyDays, Action<GlobalStatsReceived_t, bool> callback)
			{
			}

			public static void RequestUserStats(UserData userId, Action<UserStatsReceived, bool> callback)
			{
			}

			public static bool ResetAllStats(bool achievementsToo)
			{
				return false;
			}

			public static bool SetAchievement(string achievementApiName)
			{
				return false;
			}

			public static bool SetStat(string statApiName, int data)
			{
				return false;
			}

			public static bool SetStat(string statApiName, float data)
			{
				return false;
			}

			public static bool StoreStats()
			{
				return false;
			}

			public static bool UpdateAvgRateStat(string statApiName, float countThisSession, double sessionLength)
			{
				return false;
			}

			private static void HandleIconImageLoaded(UserAchievementIconFetched_t param)
			{
			}

			private static bool LoadImage(int imageHandle)
			{
				return false;
			}
		}

		public static class Server
		{
			private static CallResult<GSStatsReceived_t> _mGsStatsReceivedT;

			private static CallResult<GSStatsStored_t> _mGsStatsStoredT;

			public static bool ClearUserAchievement(CSteamID userId, string achievementApiName)
			{
				return false;
			}

			public static bool GetUserAchievement(CSteamID userId, string achievementApiName, out bool achieved)
			{
				achieved = default(bool);
				return false;
			}

			public static bool GetUserStat(CSteamID userId, string statApiName, out int data)
			{
				data = default(int);
				return false;
			}

			public static bool GetUserStat(CSteamID userId, string statApiName, out float data)
			{
				data = default(float);
				return false;
			}

			public static void RequestUserStats(CSteamID userId, Action<GSStatsReceived_t, bool> callback)
			{
			}

			public static bool SetUserAchievement(CSteamID userId, string achievementApiName)
			{
				return false;
			}

			public static bool SetUserStat(CSteamID userId, string statApiName, int data)
			{
				return false;
			}

			public static bool SetUserStat(CSteamID userId, string statApiName, float data)
			{
				return false;
			}

			public static void StoreUserStats(CSteamID userId, Action<GSStatsStored_t, bool> callback)
			{
			}

			public static bool UpdateUserAvgRateStat(CSteamID userId, string statApiName, float count, double length)
			{
				return false;
			}
		}
	}
}
