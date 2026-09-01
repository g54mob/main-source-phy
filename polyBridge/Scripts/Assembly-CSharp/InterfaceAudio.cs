using DarkTonic.MasterAudio;

public class InterfaceAudio
{
	public static void Play(string id)
	{
		MasterAudio.PlaySoundAndForget(id);
	}

	public static void PlayErrorBeep()
	{
		Play("ui_error");
	}

	public static void PlayToggleAudio()
	{
		Play("ui_settings_toggle");
	}
}
