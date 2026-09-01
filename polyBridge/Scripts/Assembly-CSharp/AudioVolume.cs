public class AudioVolume
{
	private static bool m_Mute;

	private static bool m_MuteInBackground;

	private static bool m_ForceMuted;

	public static void Mute(bool on)
	{
		m_Mute = on;
		ApplyVolumes();
	}

	public static void MuteInBackground(bool on)
	{
		m_MuteInBackground = on;
	}

	public static void MuteSFX(bool on)
	{
		float num = (float)Profiles.m_ActiveProfile.m_MasterVolume / 100f;
		float num2 = (float)Profiles.m_ActiveProfile.m_SFXVolume / 100f;
		float ambient = (float)Profiles.m_ActiveProfile.m_AmbientVolume / 100f;
		float music = (float)Profiles.m_ActiveProfile.m_MusicVolume / 100f;
		float ui = (float)Profiles.m_ActiveProfile.m_UIVolume / 100f;
		AudioMixerManager.Set(m_Mute ? 0f : num, ambient, on ? 0f : num2, music, ui);
	}

	public static void ApplyVolumes()
	{
		float sfx = ((GameStateManager.GetState() == GameState.MAIN_MENU) ? 0f : ((float)Profiles.m_ActiveProfile.m_SFXVolume / 100f));
		float ambient = ((GameStateManager.GetState() == GameState.MAIN_MENU) ? 0f : ((float)Profiles.m_ActiveProfile.m_AmbientVolume / 100f));
		float num = (float)Profiles.m_ActiveProfile.m_MasterVolume / 100f;
		float music = (float)Profiles.m_ActiveProfile.m_MusicVolume / 100f;
		float ui = (float)Profiles.m_ActiveProfile.m_UIVolume / 100f;
		AudioMixerManager.Set((m_Mute || m_ForceMuted) ? 0f : num, ambient, sfx, music, ui);
	}

	public static void UpdateManual()
	{
		if (GameInput.MousePointerOutsideGame() && m_MuteInBackground)
		{
			m_ForceMuted = true;
			ApplyVolumes();
		}
		if (!GameInput.MousePointerOutsideGame() && m_ForceMuted)
		{
			m_ForceMuted = false;
			ApplyVolumes();
		}
	}
}
