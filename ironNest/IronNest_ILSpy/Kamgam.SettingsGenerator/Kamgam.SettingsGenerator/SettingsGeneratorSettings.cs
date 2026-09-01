using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator;

public class SettingsGeneratorSettings : ScriptableObject
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Logger.LogLevel> _003C_003E9__13_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal Logger.LogLevel _003CbindLoggerLevelToSetting_003Eb__13_0()
		{
			//IL_003e: Expected I4, but got O
			SettingsGeneratorSettings orCreate = GetOrCreate();
			if ((object)orCreate != null)
			{
				return orCreate.LogLevel;
			}
			NullReferenceException ex = new NullReferenceException();
			return (Logger.LogLevel)ex;
		}
	}

	public const string Version = "1.77.0";

	public const string SettingsFilePath = "Assets/Resources/SettingsGenerator/SettingsGeneratorSettings.asset";

	public const string SettingsDirPath = "Assets/Resources/SettingsGenerator/";

	public const string _showEditorInfoLogsHint = "You can turn this log message off in the settings (Tools > Settings Generator > Settings : Show Editor Info Logs).";

	public const string ShowEditorInfoLogsHint = "You can turn this log message off in the settings (Tools > Settings Generator > Settings : Show Editor Info Logs).";

	public bool ShowEditorInfoLogs = true;

	public SettingsProvider DefaultProvider;

	public const string _DefaultProviderFieldName = "DefaultProvider";

	public Logger.LogLevel LogLevel = Logger.LogLevel.Warning;

	private static SettingsGeneratorSettings cachedConfig;

	public bool HasDefaultProvider => DefaultProvider != null;

	public SettingsProvider Provider
	{
		get
		{
			if (SettingsInitializer._instance != null)
			{
				SettingsInitializer instance = SettingsInitializer._instance;
				if ((object)SettingsInitializer._instance != null)
				{
					return instance.Provider;
				}
				return (SettingsProvider)(object)new NullReferenceException();
			}
			return DefaultProvider;
		}
	}

	private static void bindLoggerLevelToSetting()
	{
		Func<Logger.LogLevel> onGetLogLevel = _003C_003Ec._003C_003E9__13_0;
		if (_003C_003Ec._003C_003E9__13_0 == null)
		{
			onGetLogLevel = (_003C_003Ec._003C_003E9__13_0 = delegate
			{
				//IL_003e: Expected I4, but got O
				SettingsGeneratorSettings orCreate = GetOrCreate();
				if ((object)orCreate == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (Logger.LogLevel)ex;
				}
				return orCreate.LogLevel;
			});
		}
		Logger.OnGetLogLevel = onGetLogLevel;
	}

	private static void onAfterSceneLoadAtRuntime()
	{
		SettingsGeneratorSettings orCreate = GetOrCreate();
		SettingsProvider provider = orCreate.Provider;
		if (provider != null)
		{
			SettingsProvider provider2 = orCreate.Provider;
			if (!provider2.DisableAutoInitialization && !SettingsInitializer.Exists)
			{
				SettingsProvider provider3 = orCreate.Provider;
				if (!(provider3 != null))
				{
					Logger.LogWarning("Could not load settings. Please set the 'SettingsProvider' on Resources/SettingsGenerator/SettingsGeneratorSettings or (legacy) add a SettingsInitializer to your scene.");
				}
				else
				{
					SettingsProvider provider4 = orCreate.Provider;
					if (provider4.PreInitializationEvents != null)
					{
						provider4.PreInitializationEvents.Invoke();
					}
					SettingsProvider provider5 = orCreate.Provider;
					Settings settings = provider5.Settings;
				}
			}
		}
		UnityAction<Scene, LoadSceneMode> value = orCreate.onSceneLoaded;
		SceneManager.sceneLoaded += value;
	}

	private unsafe void onSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		//IL_00c5: Expected O, but got Ref
		//IL_00d4: Expected O, but got I4
		//IL_0151: Expected O, but got Ref
		//IL_0169: Expected O, but got I4
		SettingsProvider provider = Provider;
		if (!(provider != null))
		{
			return;
		}
		SettingsProvider provider2 = Provider;
		if (provider2.DisableAutoInitialization)
		{
			SettingsProvider provider3 = Provider;
			if (!provider3.HasSettings())
			{
				return;
			}
		}
		SettingsProvider provider4 = Provider;
		if (!provider4.ApplyOnSceneLoad)
		{
			return;
		}
		Scene scene3 = default(Scene);
		Scene? scene2 = (Scene)(&scene3);
		SettingsApplier applier = SettingsApplier.GetApplier((Scene?)(object)0);
		if (applier == null)
		{
			SettingsProvider provider5 = Provider;
			if (provider5 != null)
			{
				SettingsProvider provider6 = Provider;
				Scene? scene4 = (Scene)(&scene3);
				SettingsApplier settingsApplier = SettingsApplier.CreateApplier(provider6, (Scene?)(object)0);
			}
		}
	}

	public void InitializeAtRuntime()
	{
		if (SettingsInitializer.Exists)
		{
			return;
		}
		SettingsProvider provider = Provider;
		if (!(provider != null))
		{
			Logger.LogWarning("Could not load settings. Please set the 'SettingsProvider' on Resources/SettingsGenerator/SettingsGeneratorSettings or (legacy) add a SettingsInitializer to your scene.");
			return;
		}
		SettingsProvider provider2 = Provider;
		if (provider2.PreInitializationEvents != null)
		{
			provider2.PreInitializationEvents.Invoke();
		}
		SettingsProvider provider3 = Provider;
		Settings settings = provider3.Settings;
	}

	public static T GetOrCreateSetting<T>(string id, SettingData.DataType dataType) where T : class
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		SettingsGeneratorSettings orCreate = GetOrCreate();
		if ((object)orCreate != null)
		{
			SettingsProvider provider = orCreate.Provider;
			if (!(provider != null))
			{
				return null;
			}
			SettingsGeneratorSettings orCreate2 = GetOrCreate();
			if ((object)orCreate2 != null)
			{
				SettingsProvider provider2 = orCreate2.Provider;
				if ((object)provider2 != null)
				{
					Settings settings = provider2.Settings;
					if ((object)settings != null)
					{
						ISetting orCreate3 = settings.GetOrCreate(id, dataType);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						T result = default(T);
						return result;
					}
				}
			}
		}
		return (T)(object)new NullReferenceException();
	}

	public static T GetSetting<T>(string id) where T : class
	{
		ISetting setting = GetSetting(id);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		T result = default(T);
		return result;
	}

	public static ISetting GetSetting(string id)
	{
		SettingsGeneratorSettings orCreate = GetOrCreate();
		if ((object)orCreate != null)
		{
			SettingsProvider provider = orCreate.Provider;
			if (!(provider != null))
			{
				return null;
			}
			SettingsGeneratorSettings orCreate2 = GetOrCreate();
			if ((object)orCreate2 != null)
			{
				SettingsProvider provider2 = orCreate2.Provider;
				if ((object)provider2 != null)
				{
					Settings settings = provider2.Settings;
					if ((object)settings != null)
					{
						return settings.GetSetting(id);
					}
				}
			}
		}
		return (ISetting)new NullReferenceException();
	}

	public static Settings GetSettings()
	{
		SettingsGeneratorSettings orCreate = GetOrCreate();
		if ((object)orCreate != null)
		{
			SettingsProvider provider = orCreate.Provider;
			if (!(provider != null))
			{
				return null;
			}
			SettingsGeneratorSettings orCreate2 = GetOrCreate();
			if ((object)orCreate2 != null)
			{
				SettingsProvider provider2 = orCreate2.Provider;
				if ((object)provider2 != null)
				{
					return provider2.Settings;
				}
			}
		}
		return (Settings)(object)new NullReferenceException();
	}

	public static SettingsProvider GetProvider()
	{
		SettingsGeneratorSettings orCreate = GetOrCreate();
		if ((object)orCreate != null)
		{
			return orCreate.Provider;
		}
		return (SettingsProvider)(object)new NullReferenceException();
	}

	public static SettingsGeneratorSettings GetOrCreateSettings()
	{
		return GetOrCreate();
	}

	public static SettingsGeneratorSettings GetOrCreate()
	{
		if (!(cachedConfig == null))
		{
			goto IL_00ac;
		}
		if ("Assets/Resources/SettingsGenerator/SettingsGeneratorSettings.asset" != null)
		{
			string text = "Assets/Resources/SettingsGenerator/SettingsGeneratorSettings.asset".Replace("Assets/Resources/", "");
			if (text != null)
			{
				string path = text.Replace(".asset", "");
				SettingsGeneratorSettings settingsGeneratorSettings = Resources.Load<SettingsGeneratorSettings>(path);
				cachedConfig = settingsGeneratorSettings;
				goto IL_00ac;
			}
		}
		return (SettingsGeneratorSettings)(object)new NullReferenceException();
		IL_00ac:
		return cachedConfig;
	}
}
