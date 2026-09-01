using TFBGames;
using UnityEngine;

public class ApplicationFocus : MonoBehaviour
{
	private SettingsInstance m_muteBackgroundSetting;

	private float m_globalVolume;

	private void Start()
	{
		m_muteBackgroundSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("AUDIO_MUTEBACKGROUND");
		m_globalVolume = ServiceLocator.GetService<VolumeService>().GlobalVolume;
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (m_muteBackgroundSetting != null)
		{
			if (hasFocus)
			{
				AudioListener.volume = m_globalVolume;
			}
			else if (m_muteBackgroundSetting.currentValue == 1)
			{
				AudioListener.volume = 0f;
			}
		}
	}
}
