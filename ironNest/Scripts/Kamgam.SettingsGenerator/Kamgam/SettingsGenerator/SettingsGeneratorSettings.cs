using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator
{
	public class SettingsGeneratorSettings : ScriptableObject
	{
		public const string Version = "1.77.0";

		public const string SettingsFilePath = "Assets/Resources/SettingsGenerator/SettingsGeneratorSettings.asset";

		public const string SettingsDirPath = "Assets/Resources/SettingsGenerator/";

		public const string _showEditorInfoLogsHint = "You can turn this log message off in the settings (Tools > Settings Generator > Settings : Show Editor Info Logs).";

		public const string ShowEditorInfoLogsHint = "You can turn this log message off in the settings (Tools > Settings Generator > Settings : Show Editor Info Logs).";

		[Header("Editor Settings")]
		[SerializeField]
		[Tooltip("Turn off if you no longer want to see the 'Setting has no effect in the Editor. Please try in a build.' log messages.")]
		public bool ShowEditorInfoLogs;

		[Header("Runtime Settings")]
		[Tooltip("Sets the provider that will be used.\nNOTICE: If you have a SettingsInitializer in your very first loaded scene then that will be used instead. The examples use that technique to set the used provider.\n\nDo NOT use providers from the examples here. Those will be overwritten if you update the asset. You should create a new one (usually happens automatically).")]
		[SerializeField]
		public SettingsProvider DefaultProvider;

		public const string _DefaultProviderFieldName = "DefaultProvider";

		[SerializeField]
		[Tooltip("Any log above this log level will not be shown. To turn off all logs choose 'NoLogs'")]
		public Logger.LogLevel LogLevel;

		private static SettingsGeneratorSettings cachedConfig;

		public bool HasDefaultProvider => false;

		public SettingsProvider Provider => null;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void bindLoggerLevelToSetting()
		{
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void onAfterSceneLoadAtRuntime()
		{
		}

		private void onSceneLoaded(Scene scene, LoadSceneMode mode)
		{
		}

		public void InitializeAtRuntime()
		{
		}

		public static T GetOrCreateSetting<T>(string id, SettingData.DataType dataType) where T : class
		{
			return null;
		}

		public static T GetSetting<T>(string id) where T : class
		{
			return null;
		}

		public static ISetting GetSetting(string id)
		{
			return null;
		}

		public static Settings GetSettings()
		{
			return null;
		}

		public static SettingsProvider GetProvider()
		{
			return null;
		}

		public static SettingsGeneratorSettings GetOrCreateSettings()
		{
			return null;
		}

		public static SettingsGeneratorSettings GetOrCreate()
		{
			return null;
		}
	}
}
