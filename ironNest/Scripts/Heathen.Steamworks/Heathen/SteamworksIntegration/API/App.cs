using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using AOT;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration.API
{
	public static class App
	{
		public static class Client
		{
			[Serializable]
			public class UnityEventServersDisconnected : UnityEvent<EResult>
			{
			}

			[Serializable]
			public class UnityEventServersConnectFailure : UnityEvent<EResult, bool>
			{
			}

			[Serializable]
			public class UnityEventBroadcastUploadStop : UnityEvent<EBroadcastUploadResult>
			{
			}

			private static CallResult<FileDetailsResult_t> _mFileDetailResultT;

			private static Callback<SteamServerConnectFailure_t> _mSteamServerConnectFailureT;

			private static Callback<SteamServersConnected_t> _mSteamServersConnectedT;

			private static Callback<SteamServersDisconnected_t> _mSteamServersDisconnectedT;

			public static bool LoggedOn => false;

			public static bool IsSubscribed => false;

			public static bool IsSubscribedFromFamilySharing => false;

			public static bool IsSubscribedFromFreeWeekend => false;

			public static bool IsVacBanned => false;

			public static UserData Owner => default(UserData);

			public static string[] AvailableLanguages => null;

			public static bool IsBeta => false;

			public static string CurrentBetaName => null;

			public static string CurrentGameLanguage => null;

			public static DlcData[] Dlc => null;

			public static bool IsCybercafe => false;

			public static bool IsLowViolence => false;

			public static int BuildId => 0;

			public static string InstallDirectory => null;

			public static int DlcCount => 0;

			public static string LaunchCommandLine => null;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static void Initialise(AppData appId)
			{
			}

			public static void Initialise(AppData appId, InputActionData[] actions)
			{
			}

			private static void OnCallbackWaitThreadOnDoWork(object o, DoWorkEventArgs doWorkEventArgs)
			{
			}

			private static void Application_focusChanged(bool hasFocus)
			{
			}

			public static bool IsAppInstalled(AppData appId)
			{
				return false;
			}

			public static bool IsDlcInstalled(AppData appId)
			{
				return false;
			}

			public static bool GetDlcDownloadProgress(AppData appId, out ulong bytesDownloaded, out ulong bytesTotal)
			{
				bytesDownloaded = default(ulong);
				bytesTotal = default(ulong);
				return false;
			}

			public static string GetAppInstallDirectory(AppData appId)
			{
				return null;
			}

			public static DepotId_t[] InstalledDepots(AppData appId)
			{
				return null;
			}

			public static string QueryLaunchParam(string key)
			{
				return null;
			}

			public static void InstallDlc(AppData appId)
			{
			}

			public static void UninstallDlc(AppData appId)
			{
			}

			public static bool IsSubscribedApp(AppData appId)
			{
				return false;
			}

			public static bool IsTimedTrial(out uint secondsAllowed, out uint secondsPlayed)
			{
				secondsAllowed = default(uint);
				secondsPlayed = default(uint);
				return false;
			}

			public static bool GetCurrentBetaName(out string name)
			{
				name = null;
				return false;
			}

			public static DateTime GetEarliestPurchaseTime(AppData appId)
			{
				return default(DateTime);
			}

			public static void GetFileDetails(string name, Action<FileDetailsResult, bool> callback)
			{
			}

			public static bool MarkContentCorrupt(bool missingFilesOnly)
			{
				return false;
			}
		}

		public static class Server
		{
			public static CSteamID ID => default(CSteamID);

			public static bool LoggedOn { get; private set; }

			public static SteamGameServerConfiguration Configuration { get; set; }

			private static void OnSteamServersDisconnected(EResult result)
			{
			}

			private static void OnSteamServersConnected()
			{
			}

			private static void OnSteamServerConnectFailure(EResult result, bool retrying)
			{
			}

			public static void Initialise(AppData appId, SteamGameServerConfiguration serverConfiguration)
			{
			}

			private static void OnCallbackWaitThreadOnDoWork(object o, DoWorkEventArgs doWorkEventArgs)
			{
			}

			public static void LogOn()
			{
			}

			public static void SendUpdatedServerDetailsToSteam()
			{
			}

			public static void RegisterCallbacks()
			{
			}
		}

		internal static readonly Dictionary<uint, (string name, bool available)> DlcAppCash;

		public static int CallbackTickMilliseconds;

		public static bool IsDebugging;

		private static BackgroundWorker _callbackWaitThread;

		private static bool _suspendCallbacks;

		private static SteamAPIWarningMessageHook_t _mSteamAPIWarningMessageHook;

		public static bool Initialised { get; private set; }

		public static bool HasInitialisationError { get; private set; }

		public static string InitialisationErrorMessage { get; private set; }

		public static AppData Id => default(AppData);

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RunTimeInit()
		{
		}

		private static void Application_quitting()
		{
		}

		public static void Unload()
		{
		}

		private static void CallbackWaitThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		}

		private static void CallbackWaitThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
		}

		[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
		private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
		{
		}
	}
}
