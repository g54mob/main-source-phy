using System;
using System.Diagnostics;
using TFBGames;
using UnityEngine;

[Serializable]
public class SettingsInstance
{
	[Flags]
	public enum Platform : uint
	{
		None = 0u,
		Desktop = 1u,
		PS4 = 2u,
		XboxOne = 4u,
		Switch = 8u,
		Android = 0x10u,
		IOS = 0x20u,
		XboxOneX = 0x40u,
		PS4Neo = 0x80u,
		XboxLockhart = 0x100u,
		XboxAnaconda = 0x200u,
		PS5 = 0x400u,
		All = 0x7FFu
	}

	public enum SettingsType
	{
		Options = 0,
		Slider = 1
	}

	public bool m_hideSetting;

	public Platform m_platform = Platform.All;

	[SerializeField]
	private bool m_saveInPlayerPrefs = true;

	[SerializeField]
	private bool m_displayPercentage;

	public string m_settingsKey;

	public string settingName = "";

	public string toolTip = "";

	public SettingsType settingsType;

	public bool localizeOptions = true;

	public float min;

	public float max;

	public float defaultSliderValue;

	public string[] options;

	public int defaultValue;

	[SerializeField]
	private GameModeState excludeFromGameModeState;

	[SerializeField]
	private string disabledMessageKey;

	[HideInInspector]
	public int currentValue;

	[HideInInspector]
	public float currentSliderValue;

	private IPlayerPrefsPlatform m_PlayerPrefs;

	private SettingsProfileManager m_SettingsProfileManager;

	public GameModeState ExcludeFromGameModeState
	{
		get
		{
			return excludeFromGameModeState;
		}
		set
		{
			excludeFromGameModeState = value;
		}
	}

	public bool DisableChanges { get; set; }

	public string DisabledMessageKey => disabledMessageKey;

	public float NormalizedSliderValue => Mathf.InverseLerp(min, max, currentSliderValue);

	public bool DisplayPercentage => m_displayPercentage;

	public event Action<int> OnValueChanged;

	public event Action<float> OnSliderValueChanged;

	public void LoadSettings(SettingsProfile settingsProfile)
	{
		if (!m_saveInPlayerPrefs || ApplyOverrides(settingsProfile))
		{
			return;
		}
		FindServices();
		switch (settingsType)
		{
		case SettingsType.Slider:
			currentSliderValue = m_PlayerPrefs.GetFloat(m_settingsKey, defaultSliderValue);
			break;
		case SettingsType.Options:
			if (defaultValue >= 0 || m_PlayerPrefs.HasKey(m_settingsKey))
			{
				currentValue = m_PlayerPrefs.GetInt(m_settingsKey, defaultValue);
			}
			break;
		}
	}

	[Conditional("DISABLE_MODIO")]
	private void CheckAllowModsSetting(SettingsProfile settingsProfile)
	{
	}

	public bool ApplyOverrides(SettingsProfile settingsProfile, bool fireEvent = true)
	{
		if (settingsProfile == null || !settingsProfile.TryGetSettingsOverride(m_settingsKey, out var value))
		{
			return false;
		}
		switch (settingsType)
		{
		case SettingsType.Slider:
			currentSliderValue = value.sliderValue;
			if (fireEvent)
			{
				this.OnSliderValueChanged?.Invoke(currentSliderValue);
			}
			break;
		case SettingsType.Options:
			currentValue = value.value;
			if (fireEvent)
			{
				this.OnValueChanged?.Invoke(currentValue);
			}
			break;
		}
		return true;
	}

	public void SaveSettings()
	{
		if (m_saveInPlayerPrefs)
		{
			FindServices();
			if (settingsType == SettingsType.Slider)
			{
				m_PlayerPrefs.SetFloat(m_settingsKey, currentSliderValue);
				this.OnSliderValueChanged?.Invoke(currentSliderValue);
			}
			if (settingsType == SettingsType.Options)
			{
				m_PlayerPrefs.SetInt(m_settingsKey, currentValue);
				this.OnValueChanged?.Invoke(currentValue);
			}
			if (GlobalSettingsHandler.DoesPlatformAllowRapidFileSystemAccess())
			{
				m_PlayerPrefs.Save();
			}
		}
	}

	public void ResetToDefault()
	{
		FindServices();
		if (m_hideSetting)
		{
			m_PlayerPrefs.DeleteKey(m_settingsKey);
		}
		else if (m_saveInPlayerPrefs)
		{
			if (m_SettingsProfileManager == null || m_SettingsProfileManager.CurrentSettingsProfile == null || !ApplyOverrides(m_SettingsProfileManager.CurrentSettingsProfile, fireEvent: false))
			{
				currentSliderValue = defaultSliderValue;
				currentValue = defaultValue;
			}
			SaveSettings();
		}
	}

	private void FindServices()
	{
		if (m_PlayerPrefs == null)
		{
			m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		}
		if (m_SettingsProfileManager == null)
		{
			m_SettingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
		}
	}
}
