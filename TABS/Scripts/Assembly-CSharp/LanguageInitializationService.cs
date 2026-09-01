using TFBGames;

public class LanguageInitializationService : IService
{
	private const string InitializeLanguageSettingKey = "VIDEO_SHOULD_INITIALIZE_LANGUAGE";

	private const string LanguageSettingKey = "VIDEO_LANGUAGE";

	private GlobalSettingsHandler m_globalSettingsHandler;

	private SettingsInstance m_languageSettingsInstance;

	private IPlayerPrefsPlatform m_playerPrefs;

	private WaitForStorage m_waitForStorage;

	private void OnStorageReady()
	{
		int num = 0;
		int language = 0;
		if (m_playerPrefs != null)
		{
			num = m_playerPrefs.GetInt("VIDEO_SHOULD_INITIALIZE_LANGUAGE", 0);
			language = m_playerPrefs.GetInt("VIDEO_LANGUAGE", 0);
		}
		if (num == 0)
		{
			language = (int)Localizer.GetSystemLanguage();
			if (m_playerPrefs != null)
			{
				m_playerPrefs.SetInt("VIDEO_SHOULD_INITIALIZE_LANGUAGE", 1);
				m_playerPrefs.Save();
			}
			SettingsProfileManager service = ServiceLocator.GetService<SettingsProfileManager>();
			GlobalSettingsHandler globalSettingsHandler = ((m_globalSettingsHandler != null) ? m_globalSettingsHandler : ServiceLocator.GetService<GlobalSettingsHandler>());
			SettingsInstance settingsInstance = ((globalSettingsHandler != null) ? globalSettingsHandler.GetSettingsInstance("VIDEO_SHOULD_INITIALIZE_LANGUAGE") : null);
			if (settingsInstance != null && service != null)
			{
				settingsInstance.LoadSettings(service.CurrentSettingsProfile);
			}
		}
		SetLanguage(language);
	}

	private void SetLanguage(int languageIndex)
	{
		Localizer.Initialize((Localizer.Language)languageIndex);
		if (m_playerPrefs != null)
		{
			m_playerPrefs.SetInt("VIDEO_LANGUAGE", languageIndex);
		}
		GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
		if (service != null)
		{
			SettingsInstance settingsInstance = service.GetSettingsInstance("VIDEO_LANGUAGE");
			if (settingsInstance != null)
			{
				settingsInstance.currentValue = languageIndex;
			}
		}
	}

	public void QueueLanguageInitializationCallback()
	{
		if (m_waitForStorage == null)
		{
			m_waitForStorage = ServiceLocator.GetService<WaitForStorage>();
		}
		if (!(m_waitForStorage == null))
		{
			m_waitForStorage.FireWhenReady(OnStorageReady);
		}
	}

	public void OnRegister()
	{
	}

	public void OnAwake()
	{
		m_playerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		m_waitForStorage = ServiceLocator.GetService<WaitForStorage>();
		m_globalSettingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
		if (m_globalSettingsHandler == null)
		{
			m_languageSettingsInstance = m_globalSettingsHandler.GetSettingsInstance("VIDEO_LANGUAGE");
		}
	}

	public void OnStart()
	{
	}

	public void OnUpdate()
	{
	}

	public void OnFixedUpdate()
	{
	}

	public void OnLateUpdate()
	{
	}

	public void UnRegister()
	{
	}

	private void RunDebugOptions()
	{
	}
}
