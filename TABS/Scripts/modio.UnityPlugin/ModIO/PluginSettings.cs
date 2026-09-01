using System;
using System.IO;
using UnityEngine;

namespace ModIO
{
	public class PluginSettings : ScriptableObject
	{
		public class VariableDirectoryAttribute : PropertyAttribute
		{
		}

		[Serializable]
		public struct RequestLoggingOptions
		{
			[Tooltip("Should failed requests be logged as warnings")]
			public bool errorsAsWarnings;

			[Tooltip("Log all web request responses made received")]
			public bool logAllResponses;

			[Tooltip("Should the sending of a request be logged separately")]
			public bool logOnSend;
		}

		[Serializable]
		public struct Data
		{
			[Header("API Settings")]
			[Tooltip("API URL to use when making requests")]
			public string apiURL;

			[Tooltip("Game Id assigned to your game profile")]
			public int gameId;

			[Tooltip("API Key assigned to your game profile")]
			public string gameAPIKey;

			[Tooltip("User Portal that this build of the game will be launching through.")]
			public UserPortal userPortal;

			[Tooltip("Amount of memory the request cache is permitted to grow to (KB)")]
			public uint requestCacheSizeKB;

			public RequestLoggingOptions requestLogging;

			[Header("Standalone Directories")]
			[Tooltip("Directory to use for mod installations")]
			[VariableDirectory]
			public string installationDirectory;

			[Tooltip("Directory to use for cached server data")]
			[VariableDirectory]
			public string cacheDirectory;

			[Tooltip("Directory to use for user data")]
			[VariableDirectory]
			public string userDirectory;

			[Header("Editor Directories")]
			[Tooltip("Directory to use for mod installations")]
			[VariableDirectory]
			public string installationDirectoryEditor;

			[Tooltip("Directory to use for cached server data")]
			[VariableDirectory]
			public string cacheDirectoryEditor;

			[Tooltip("Directory to use for user data")]
			[VariableDirectory]
			public string userDirectoryEditor;

			[Obsolete("Use requestLogging.logAllResponses instead.")]
			public bool logAllRequests
			{
				get
				{
					return requestLogging.logAllResponses;
				}
				set
				{
					requestLogging.logAllResponses = value;
				}
			}
		}

		public static readonly string FILE_PATH = "modio_settings";

		private static bool _loaded = false;

		private static Data _dataInstance;

		[SerializeField]
		private Data m_data;

		public static Data data
		{
			get
			{
				if (!_loaded)
				{
					_dataInstance = LoadDataFromAsset(FILE_PATH);
					_loaded = true;
				}
				return _dataInstance;
			}
		}

		public static string API_URL => data.apiURL;

		public static int GAME_ID => data.gameId;

		public static string GAME_API_KEY => data.gameAPIKey;

		public static RequestLoggingOptions REQUEST_LOGGING => data.requestLogging;

		public static UserPortal USER_PORTAL => data.userPortal;

		public static uint CACHE_SIZE => data.requestCacheSizeKB;

		[Obsolete("Use DataStorage.INSTALLATION_DIRECTORY instead.")]
		public static string INSTALLATION_DIRECTORY => DataStorage.INSTALLATION_DIRECTORY;

		[Obsolete("Use DataStorage.CACHE_DIRECTORY instead.")]
		public static string CACHE_DIRECTORY => DataStorage.CACHE_DIRECTORY;

		[Obsolete("Use UserDataStorage.USER_DIRECTORY instead.")]
		public static string USER_DIRECTORY => UserDataStorage.USER_DIRECTORY;

		public static Data LoadDataFromAsset(string assetPath)
		{
			PluginSettings pluginSettings = Resources.Load<PluginSettings>(assetPath);
			Data result;
			if (pluginSettings == null)
			{
				result = default(Data);
			}
			else
			{
				result = pluginSettings.m_data;
				if (Application.isPlaying)
				{
					if (result.cacheDirectory != null)
					{
						result.cacheDirectory = ReplaceDirectoryVariables(result.cacheDirectory, result.gameId);
					}
					if (result.installationDirectory != null)
					{
						result.installationDirectory = ReplaceDirectoryVariables(result.installationDirectory, result.gameId);
					}
					if (result.userDirectory != null)
					{
						result.userDirectory = ReplaceDirectoryVariables(result.userDirectory, result.gameId);
					}
				}
			}
			return result;
		}

		public static string ReplaceDirectoryVariables(string directory, int gameId)
		{
			string text = Application.persistentDataPath;
			if (IOUtilities.PathEndsWithDirectorySeparator(text))
			{
				text = text.Remove(text.Length - 1);
			}
			string text2 = Application.dataPath;
			if (IOUtilities.PathEndsWithDirectorySeparator(text2))
			{
				text2 = text2.Remove(text2.Length - 1);
			}
			string text3 = Application.temporaryCachePath;
			if (IOUtilities.PathEndsWithDirectorySeparator(text3))
			{
				text3 = text3.Remove(text3.Length - 1);
			}
			directory = directory.Replace("$PERSISTENT_DATA_PATH$", text).Replace("$DATA_PATH$", text2).Replace("$TEMPORARY_CACHE_PATH$", text3)
				.Replace("$BUILD_GUID$", Application.buildGUID)
				.Replace("$COMPANY_NAME$", Application.companyName)
				.Replace("$PRODUCT_NAME$", Application.productName)
				.Replace("$APPLICATION_IDENTIFIER", Application.identifier)
				.Replace("$GAME_ID$", gameId.ToString())
				.Replace("$CURRENT_DIRECTORY$", Directory.GetCurrentDirectory());
			return directory;
		}
	}
}
