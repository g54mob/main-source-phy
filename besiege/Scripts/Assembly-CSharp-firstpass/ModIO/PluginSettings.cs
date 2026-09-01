using System;
using System.IO;
using UnityEngine;

namespace ModIO
{
	public class PluginSettings : ScriptableObject, ISerializationCallbackReceiver
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
			internal const int VERSION = 2;

			[VersionedData(2, 2)]
			[HideInInspector]
			public int version;

			[VersionedData(0, "")]
			[Tooltip("API URL to use when making requests")]
			[Header("API Settings")]
			public string apiURL;

			[VersionedData(0, 0)]
			[Tooltip("Game Id assigned to your game profile")]
			public int gameId;

			[Tooltip("API Key assigned to your game profile")]
			[VersionedData(0, "")]
			public string gameAPIKey;

			[Tooltip("User Portal that this build of the game will be launching through.")]
			[VersionedData(2, UserPortal.None)]
			public UserPortal userPortal;

			[Tooltip("Amount of memory the request cache is permitted to grow to (KB).\nA negative value indicates an unlimited cache size.")]
			[VersionedData(1, -1)]
			public int requestCacheSizeKB;

			public RequestLoggingOptions requestLogging;

			[VariableDirectory]
			[Tooltip("Directory to use for mod installations")]
			[Header("Standalone Directories")]
			[VersionedData(0, "$DATA_PATH$/mod.io/mods")]
			public string installationDirectory;

			[Tooltip("Directory to use for cached server data")]
			[VersionedData(0, "$DATA_PATH$/mod.io/cache")]
			[VariableDirectory]
			public string cacheDirectory;

			[VariableDirectory]
			[Tooltip("Directory to use for user data")]
			[VersionedData(0, "$PERSISTENT_DATA_PATH$/mod.io-$GAME_ID$")]
			public string userDirectory;

			[VariableDirectory]
			[Header("Editor Directories")]
			[Tooltip("Directory to use for mod installations")]
			[VersionedData(0, "$CURRENT_DIRECTORY$/mod.io/editor/$GAME_ID$/mods")]
			public string installationDirectoryEditor;

			[VersionedData(0, "$CURRENT_DIRECTORY$/mod.io/editor/$GAME_ID$/cache")]
			[Tooltip("Directory to use for cached server data")]
			[VariableDirectory]
			public string cacheDirectoryEditor;

			[VariableDirectory]
			[VersionedData(0, "$CURRENT_DIRECTORY$/mod.io/editor/$GAME_ID$/user")]
			[Tooltip("Directory to use for user data")]
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

		private static bool _loaded;

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

		public static string API_URL
		{
			get
			{
				return data.apiURL;
			}
		}

		public static int GAME_ID
		{
			get
			{
				return data.gameId;
			}
		}

		public static string GAME_API_KEY
		{
			get
			{
				return data.gameAPIKey;
			}
		}

		public static RequestLoggingOptions REQUEST_LOGGING
		{
			get
			{
				return data.requestLogging;
			}
		}

		public static UserPortal USER_PORTAL
		{
			get
			{
				return data.userPortal;
			}
		}

		public static uint CACHE_SIZE_BYTES
		{
			get
			{
				if (data.requestCacheSizeKB < 0)
				{
					return uint.MaxValue;
				}
				return (uint)(data.requestCacheSizeKB * 1024);
			}
		}

		[Obsolete("Use DataStorage.INSTALLATION_DIRECTORY instead.")]
		public static string INSTALLATION_DIRECTORY
		{
			get
			{
				return DataStorage.INSTALLATION_DIRECTORY;
			}
		}

		[Obsolete("Use DataStorage.CACHE_DIRECTORY instead.")]
		public static string CACHE_DIRECTORY
		{
			get
			{
				return DataStorage.CACHE_DIRECTORY;
			}
		}

		[Obsolete("Use UserDataStorage.USER_DIRECTORY instead.")]
		public static string USER_DIRECTORY
		{
			get
			{
				return UserDataStorage.USER_DIRECTORY;
			}
		}

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
				Debug.Log("[mod.io] PluginSettings variable directories resolved to:\n.cacheDirectory=" + result.cacheDirectory + "\n.installationDirectory=" + result.installationDirectory + "\n.userDirectory=" + result.userDirectory);
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
				.Replace("$BUILD_GUID$", Application.version)
				.Replace("$COMPANY_NAME$", Application.companyName)
				.Replace("$PRODUCT_NAME$", Application.productName)
				.Replace("$APPLICATION_IDENTIFIER", Application.bundleIdentifier)
				.Replace("$GAME_ID$", gameId.ToString())
				.Replace("$CURRENT_DIRECTORY$", Directory.GetCurrentDirectory());
			return directory;
		}

		public static Data UpdateVersionedValues(int dataVersion, Data dataValues)
		{
			if (dataVersion >= 2)
			{
				return dataValues;
			}
			return VersionedDataAttribute.UpdateStructFields(dataVersion, dataValues);
		}

		public void OnAfterDeserialize()
		{
			m_data = UpdateVersionedValues(m_data.version, m_data);
		}

		public void OnBeforeSerialize()
		{
		}

		public static void SetPortal(UserPortal portal)
		{
			Data dataInstance = data;
			if (dataInstance.userPortal != portal)
			{
				dataInstance.userPortal = portal;
				_dataInstance = dataInstance;
			}
		}
	}
}
