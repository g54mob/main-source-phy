using System;
using System.Collections.Generic;
using BitCode.Users;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Services/GlobalSettingsHandler")]
public class GlobalSettingsHandler : ServiceAsset
{
	private static readonly string SETTINGS_VERSION = "SETTINGS_VERSION";

	[SerializeField]
	private int m_settingsVersion;

	[SerializeField]
	private AudioMixer m_audioMixer;

	[SerializeField]
	private SettingsInstance[] m_videoSettings;

	[SerializeField]
	private SettingsInstance[] m_audioSettings;

	[SerializeField]
	private SettingsInstance[] m_gameplaySettings;

	[SerializeField]
	private SettingsInstance[] m_controlSettings;

	[SerializeField]
	private SettingsInstance[] m_bugsSettings;

	[SerializeField]
	private SettingsInstance[] m_twitchSettings;

	private Dictionary<string, SettingsInstance> m_settingsDict = new Dictionary<string, SettingsInstance>();

	private static Resolution[] _filteredResolutions = null;

	private IPlayerPrefsPlatform m_PlayerPrefs;

	private WaitForStorage m_WaitForStorage;

	private AccountManager m_AccountManager;

	private SettingsProfileManager m_SettingsProfileManager;

	private bool isSavingSettings;

	private SettingsInstance m_controllerAny;

	public SettingsInstance[] VideoSettings => m_videoSettings;

	public SettingsInstance[] AudioSettings => m_audioSettings;

	public SettingsInstance[] GameplaySettings => m_gameplaySettings;

	public SettingsInstance[] ControlSettings => m_controlSettings;

	public SettingsInstance[] BugsSettings => m_bugsSettings;

	public SettingsInstance[] TwitchSettings => m_twitchSettings;

	public static SettingsInstance.Platform CurrentPlatform => SettingsInstance.Platform.Desktop;

	public override void OnUpdate()
	{
		if (m_controllerAny == null || m_controllerAny.currentValue != 0)
		{
			return;
		}
		PlayerActions instance = PlayerActions.Instance;
		if (instance.ActiveDevice == InputDevice.Null)
		{
			return;
		}
		InputDeviceStyle deviceStyle = instance.ActiveDevice.DeviceStyle;
		if (deviceStyle != InputDeviceStyle.Xbox360 && deviceStyle != InputDeviceStyle.XboxOne && deviceStyle != InputDeviceStyle.PlayStation4)
		{
			if (instance.ExcludeDevices.Contains(instance.ActiveDevice))
			{
				Debug.LogError($"Already excluded: {instance.ActiveDevice.GUID}");
			}
			else
			{
				instance.ExcludeDevices.Add(instance.ActiveDevice);
			}
		}
	}

	public static Resolution[] GetResolutionsWithHighestRefreshRate()
	{
		if (_filteredResolutions != null)
		{
			return _filteredResolutions;
		}
		Resolution[] resolutions = Screen.resolutions;
		if (resolutions.Length < 1)
		{
			return new Resolution[1]
			{
				new Resolution
				{
					width = 640,
					height = 480,
					refreshRate = 60
				}
			};
		}
		List<Resolution> list = new List<Resolution>();
		Resolution item = resolutions[0];
		int index = 0;
		list.Add(item);
		for (int i = 1; i < resolutions.Length; i++)
		{
			Resolution resolution = resolutions[i];
			if (resolution.width == item.width && resolution.height == item.height && resolution.refreshRate > item.refreshRate)
			{
				list[index] = resolution;
			}
			else
			{
				index = list.Count;
				list.Add(resolution);
			}
			item = resolution;
		}
		_filteredResolutions = list.ToArray();
		return _filteredResolutions;
	}

	public static FullScreenMode GetFullScreenMode(int value)
	{
		FullScreenMode result = FullScreenMode.FullScreenWindow;
		switch (value)
		{
		case 0:
			result = FullScreenMode.FullScreenWindow;
			break;
		case 1:
			result = FullScreenMode.ExclusiveFullScreen;
			break;
		case 2:
			result = FullScreenMode.Windowed;
			break;
		}
		return result;
	}

	public static int GetFullScreenMode(FullScreenMode mode)
	{
		int result = 0;
		switch (mode)
		{
		case FullScreenMode.FullScreenWindow:
			result = 0;
			break;
		case FullScreenMode.ExclusiveFullScreen:
			result = 1;
			break;
		case FullScreenMode.Windowed:
			result = 2;
			break;
		}
		return result;
	}

	public static bool DoesPlatformAllowRapidFileSystemAccess()
	{
		return true;
	}

	public static RigidbodyInterpolation GetInterpolationMode(int value)
	{
		switch (value)
		{
		case 0:
			return RigidbodyInterpolation.None;
		case 1:
			return RigidbodyInterpolation.Interpolate;
		default:
			return RigidbodyInterpolation.None;
		}
	}

	public override void OnAwake()
	{
		m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		m_WaitForStorage = ServiceLocator.GetService<WaitForStorage>();
		m_AccountManager = ServiceLocator.GetService<AccountManager>();
		m_SettingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
		m_AccountManager.ActiveAccountChanged += OnActiveAccountChanged;
		m_SettingsProfileManager.SettingsProfileChanged += OnSettingsChanged;
		Init();
	}

	public override void OnStart()
	{
		Debug.Log("Start Settings Ready");
		m_WaitForStorage.FireWhenReady(RegisterSettingCallbacks);
		Debug.Log("End Settings Ready");
	}

	public override void UnRegister()
	{
		if (m_AccountManager != null)
		{
			m_AccountManager.ActiveAccountChanged -= OnActiveAccountChanged;
		}
		RemoveSettingCallbacks();
	}

	private void OnActiveAccountChanged(ILocalAccount account)
	{
		RemoveSettingCallbacks();
		m_settingsDict.Clear();
		Init(registerSettingCallbacks: true);
	}

	private void OnSettingsChanged(SettingsProfile newSettingsProfile)
	{
		if (isSavingSettings)
		{
			return;
		}
		foreach (SettingsInstance value in m_settingsDict.Values)
		{
			value.LoadSettings(newSettingsProfile);
		}
	}

	private void Init(bool registerSettingCallbacks = false)
	{
		m_WaitForStorage.FireWhenReady(OnStorageReady);
		if (registerSettingCallbacks)
		{
			m_WaitForStorage.FireWhenReady(RegisterSettingCallbacks);
		}
	}

	private void OnStorageReady()
	{
		for (int i = 0; i < m_videoSettings.Length; i++)
		{
			if (m_videoSettings[i].m_settingsKey == "VIDEO_RESOLUTION")
			{
				Resolution[] resolutionsWithHighestRefreshRate = GetResolutionsWithHighestRefreshRate();
				int currentValue = resolutionsWithHighestRefreshRate.Length - 1;
				m_videoSettings[i].options = new string[resolutionsWithHighestRefreshRate.Length];
				for (int j = 0; j < resolutionsWithHighestRefreshRate.Length; j++)
				{
					m_videoSettings[i].options[j] = resolutionsWithHighestRefreshRate[j].width + " x " + resolutionsWithHighestRefreshRate[j].height + " @ " + resolutionsWithHighestRefreshRate[j].refreshRate;
					if (Screen.width == resolutionsWithHighestRefreshRate[j].width && Screen.height == resolutionsWithHighestRefreshRate[j].height)
					{
						currentValue = j;
					}
				}
				m_videoSettings[i].currentValue = currentValue;
			}
			else if (m_videoSettings[i].m_settingsKey == "VIDEO_WINDOW_MODE")
			{
				m_videoSettings[i].currentValue = GetFullScreenMode(Screen.fullScreenMode);
			}
			else if (m_videoSettings[i].m_settingsKey == "VIDEO_LANGUAGE")
			{
				string[] names = Enum.GetNames(typeof(Localizer.Language));
				m_videoSettings[i].options = names;
			}
		}
		InitSettings(m_videoSettings, m_settingsDict);
		InitSettings(m_audioSettings, m_settingsDict);
		InitSettings(m_gameplaySettings, m_settingsDict);
		InitSettings(m_controlSettings, m_settingsDict);
		InitSettings(m_bugsSettings, m_settingsDict);
		InitSettings(m_twitchSettings, m_settingsDict);
		if (m_PlayerPrefs.HasKey(SETTINGS_VERSION))
		{
			int num = m_PlayerPrefs.GetInt(SETTINGS_VERSION);
			if (m_settingsVersion != num)
			{
				ResetToDefault();
			}
		}
		else
		{
			ResetToDefault();
		}
		m_PlayerPrefs.SetInt(SETTINGS_VERSION, m_settingsVersion);
		m_controllerAny = GetSettingsInstance("CONTROL_ALLOW_ANY");
		if (!m_PlayerPrefs.HasKey("resetUiMode"))
		{
			GetSettingsInstance("UI_INPUT_MODE")?.ResetToDefault();
			m_PlayerPrefs.SetInt("resetUiMode", 1);
		}
	}

	private void InitSettings(SettingsInstance[] settings, Dictionary<string, SettingsInstance> settingsDict)
	{
		foreach (SettingsInstance settingsInstance in settings)
		{
			if (!settingsInstance.m_hideSetting)
			{
				settingsInstance.LoadSettings(m_SettingsProfileManager.CurrentSettingsProfile);
				settingsDict.Add(settingsInstance.m_settingsKey, settingsInstance);
			}
		}
	}

	public void ResetToDefault()
	{
		ResetToDefault(m_videoSettings);
		ResetToDefault(m_audioSettings);
		ResetToDefault(m_gameplaySettings);
		ResetToDefault(m_controlSettings);
		if (Bugs._DLC_ACTIVATED)
		{
			ResetToDefault(m_bugsSettings);
		}
		ResetToDefault(m_twitchSettings);
	}

	public void ResetToDefault(SettingsInstance[] settings)
	{
		for (int i = 0; i < settings.Length; i++)
		{
			settings[i].ResetToDefault();
		}
	}

	public void RegisterSettingCallbacks()
	{
		RegisterSettingsChangeHandler("VIDEO_SHADOW", UpdateShadowQuality);
		RegisterSettingsChangeHandler("VIDEO_VSYNC", UpdateVSync);
		RegisterSettingsChangeHandler("VIDEO_FRAMERATE", UpdateFrameRate);
		RegisterSettingsChangeHandler("VIDEO_LOD", UpdateLODBias);
		RegisterSettingsChangeHandler("VIDEO_LANGUAGE", UpdateLanguange);
		RegisterSettingsChangeHandlerFloat("AUDIO_MASTER", UpdateMasterVolume);
		RegisterSettingsChangeHandlerFloat("AUDIO_SFX", UpdateEffectsVolume);
		RegisterSettingsChangeHandlerFloat("AUDIO_MUSIC", UpdateMusicVolume);
		RegisterSettingsChangeHandler("VIDEO_CONSOLE_MODE", UpdateConsoleDisplayMode);
		RegisterSettingsChangeHandler("CONTROL_ALLOW_ANY", UpdateControllerAny);
	}

	public void RemoveSettingCallbacks()
	{
		DeregisterSettingsChangeHandler("VIDEO_SHADOW", UpdateShadowQuality);
		DeregisterSettingsChangeHandler("VIDEO_VSYNC", UpdateVSync);
		DeregisterSettingsChangeHandler("VIDEO_FRAMERATE", UpdateFrameRate);
		DeregisterSettingsChangeHandler("VIDEO_LOD", UpdateLODBias);
		DeregisterSettingsChangeHandler("VIDEO_LANGUAGE", UpdateLanguange);
		DeregisterSettingsChangeHandlerFloat("AUDIO_MASTER", UpdateMasterVolume);
		DeregisterSettingsChangeHandlerFloat("AUDIO_SFX", UpdateMasterVolume);
		DeregisterSettingsChangeHandlerFloat("AUDIO_MUSIC", UpdateMasterVolume);
		DeregisterSettingsChangeHandler("VIDEO_CONSOLE_MODE", UpdateConsoleDisplayMode);
		DeregisterSettingsChangeHandler("CONTROL_ALLOW_ANY", UpdateControllerAny);
	}

	public SettingsInstance GetSettingsInstance(string settingsKey)
	{
		if (m_settingsDict == null || settingsKey.Equals(string.Empty))
		{
			return null;
		}
		if (m_settingsDict.ContainsKey(settingsKey))
		{
			return m_settingsDict[settingsKey];
		}
		return null;
	}

	public void SaveAllSettings()
	{
		if (m_settingsDict == null)
		{
			Debug.LogError("Error: Settings dictionary on GlobalSettingsHandler is null.");
			return;
		}
		isSavingSettings = true;
		foreach (string key in m_settingsDict.Keys)
		{
			GetSettingsInstance(key)?.SaveSettings();
		}
		isSavingSettings = false;
		m_PlayerPrefs.Save();
	}

	public void RegisterSettingsChangeHandler(string settingsKey, Action<int> changeCallback, bool invokeWithCurrentSetting = true)
	{
		SettingsInstance settingsInstance = GetSettingsInstance(settingsKey);
		settingsInstance.OnValueChanged += changeCallback;
		if (invokeWithCurrentSetting)
		{
			changeCallback(settingsInstance.currentValue);
		}
	}

	public void RegisterSettingsChangeHandlerFloat(string settingsKey, Action<float> changeCallback, bool invokeWithCurrentSetting = true)
	{
		SettingsInstance settingsInstance = GetSettingsInstance(settingsKey);
		settingsInstance.OnSliderValueChanged += changeCallback;
		if (invokeWithCurrentSetting)
		{
			changeCallback(settingsInstance.currentSliderValue);
		}
	}

	public void DeregisterSettingsChangeHandler(string settingsKey, Action<int> changeCallback)
	{
		SettingsInstance settingsInstance = GetSettingsInstance(settingsKey);
		if (settingsInstance != null)
		{
			settingsInstance.OnValueChanged -= changeCallback;
		}
	}

	public void DeregisterSettingsChangeHandlerFloat(string settingsKey, Action<float> changeCallback)
	{
		SettingsInstance settingsInstance = GetSettingsInstance(settingsKey);
		if (settingsInstance != null)
		{
			settingsInstance.OnSliderValueChanged -= changeCallback;
		}
	}

	private void UpdateShadowQuality(int value)
	{
		switch (value)
		{
		case 0:
			QualitySettings.shadows = ShadowQuality.Disable;
			break;
		case 1:
			QualitySettings.shadows = ShadowQuality.HardOnly;
			QualitySettings.shadowResolution = ShadowResolution.Low;
			QualitySettings.shadowDistance = 25f;
			QualitySettings.shadowCascades = 0;
			break;
		case 2:
			QualitySettings.shadows = ShadowQuality.All;
			QualitySettings.shadowResolution = ShadowResolution.Medium;
			QualitySettings.shadowDistance = 50f;
			QualitySettings.shadowCascades = 2;
			break;
		case 3:
			QualitySettings.shadows = ShadowQuality.All;
			QualitySettings.shadowResolution = ShadowResolution.High;
			QualitySettings.shadowDistance = 100f;
			QualitySettings.shadowCascades = 4;
			break;
		case 4:
			QualitySettings.shadows = ShadowQuality.All;
			QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
			QualitySettings.shadowDistance = 150f;
			QualitySettings.shadowCascades = 4;
			break;
		default:
			QualitySettings.shadows = ShadowQuality.All;
			QualitySettings.shadowResolution = ShadowResolution.High;
			QualitySettings.shadowDistance = 100f;
			QualitySettings.shadowCascades = 4;
			break;
		}
	}

	private void UpdateVSync(int value)
	{
		QualitySettings.vSyncCount = value;
	}

	private void UpdateFrameRate(int value)
	{
		int targetFrameRate = -1;
		switch (value)
		{
		case 0:
			targetFrameRate = -1;
			break;
		case 1:
			targetFrameRate = 30;
			break;
		case 2:
			targetFrameRate = 60;
			break;
		case 3:
			targetFrameRate = 144;
			break;
		}
		Application.targetFrameRate = targetFrameRate;
	}

	private void UpdateMasterVolume(float value)
	{
		value = Mathf.InverseLerp(0f, 100f, value);
		value = Mathf.Max(value, 0.0001f);
		m_audioMixer.SetFloat("VolumeMaster", Mathf.Log10(value) * 20f);
	}

	private void UpdateEffectsVolume(float value)
	{
		value = Mathf.InverseLerp(0f, 100f, value);
		value = Mathf.Max(value, 0.0001f);
		m_audioMixer.SetFloat("VolumeEffects", Mathf.Log10(value) * 20f);
	}

	private void UpdateMusicVolume(float value)
	{
		value = Mathf.InverseLerp(0f, 100f, value);
		value = Mathf.Max(value, 0.0001f);
		m_audioMixer.SetFloat("VolumeMusic", Mathf.Log10(value) * 20f);
	}

	private void UpdateLODBias(int value)
	{
		switch (value)
		{
		case 0:
			QualitySettings.lodBias = 1f;
			break;
		case 1:
			QualitySettings.lodBias = 2f;
			break;
		case 2:
			QualitySettings.lodBias = 10f;
			break;
		case 3:
			QualitySettings.lodBias = float.PositiveInfinity;
			break;
		default:
			QualitySettings.lodBias = 2f;
			break;
		}
	}

	private void UpdateLanguange(int value)
	{
		Localizer.SetLanguage((Localizer.Language)value);
	}

	private void UpdateControllerAny(int value)
	{
		if (value == 1)
		{
			PlayerActions.Instance.ExcludeDevices.Clear();
		}
	}

	private void UpdateConsoleDisplayMode(int value)
	{
		if (CurrentPlatform == SettingsInstance.Platform.XboxOneX || CurrentPlatform == SettingsInstance.Platform.PS4Neo)
		{
			ConsoleDisplayMode displayMode = ConsoleDisplayMode.Undefined;
			switch (value)
			{
			case 0:
				displayMode = ConsoleDisplayMode.UHD;
				break;
			case 1:
				displayMode = ConsoleDisplayMode.HD;
				break;
			}
			m_SettingsProfileManager?.CreateAppropriateSettingsProfile(displayMode);
		}
	}
}
