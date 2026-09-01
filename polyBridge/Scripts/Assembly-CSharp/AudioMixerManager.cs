using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
	[SerializeField]
	private AudioMixer _mainMixer;

	[SerializeField]
	private AnimationCurve _faderCurve;

	[SerializeField]
	private bool _debug;

	public static int DEFAULT_MASTER_VOLUME = 70;

	public static int DEFAULT_AMBIENT_VOLUME = 90;

	public static int DEFAULT_SFX_VOLUME = 100;

	public static int DEFAULT_MUSIC_VOLUME = 60;

	public static int DEFAULT_UI_VOLUME = 80;

	private static AudioMixer MainMixer;

	private static AnimationCurve FaderCurve;

	public const string AMBIENCE_BUS_NAME = "Ambience";

	public const string HYDRAULICLOOP_BUS_NAME = "Sim - HydraulicLoop";

	public const string SIMULATION_BUS_NAME = "Simulation";

	public const string VEHICLE_BUS_NAME = "Sim - Vehicle";

	public static bool m_PausedSFX;

	private static readonly float MUSIC_VOLUME_FADE_IN_TIME_SECONDS = 3f;

	private static float m_StartMusicVolumeFadeInTime;

	private static bool m_MusicVolumeFadeInComplete;

	private static bool m_AllowedToStartFadeIn;

	private static float m_LastSimulationPitch = 1f;

	private const string MASTERVOL = "MasterVolume";

	private const string MUSICVOL = "MusicVolume";

	private const string AMBVOL = "AmbientVolume";

	private const string SFXVOL = "SFXVolume";

	private const string UIVOL = "UIVolume";

	public static float Pitch { get; private set; } = 1f;

	public static AudioMixerManager Instance { get; private set; }

	public static float MasterVolume
	{
		get
		{
			return AudioUtilites.DecibelToLinear(MainMixer.GetFloat("MasterVolume"));
		}
		set
		{
			MainMixer.SetFloat("MasterVolume", AudioUtilites.LinearToDecibel(value));
		}
	}

	public static float MusicVolume
	{
		get
		{
			return AudioUtilites.DecibelToLinear(MainMixer.GetFloat("MusicVolume"));
		}
		set
		{
			MainMixer.SetFloat("MusicVolume", AudioUtilites.LinearToDecibel(value));
		}
	}

	public static float AmbientVolume
	{
		get
		{
			return AudioUtilites.DecibelToLinear(MainMixer.GetFloat("AmbientVolume"));
		}
		set
		{
			MainMixer.SetFloat("AmbientVolume", AudioUtilites.LinearToDecibel(value));
		}
	}

	public static float SFXVolume
	{
		get
		{
			return AudioUtilites.DecibelToLinear(MainMixer.GetFloat("SFXVolume"));
		}
		set
		{
			MainMixer.SetFloat("SFXVolume", AudioUtilites.LinearToDecibel(value));
		}
	}

	public static float UIVolume
	{
		get
		{
			return AudioUtilites.DecibelToLinear(MainMixer.GetFloat("UIVolume"));
		}
		set
		{
			MainMixer.SetFloat("UIVolume", AudioUtilites.LinearToDecibel(value));
		}
	}

	private void Awake()
	{
		MainMixer = _mainMixer;
		FaderCurve = _faderCurve;
		m_StartMusicVolumeFadeInTime = 0f;
		m_MusicVolumeFadeInComplete = false;
		m_AllowedToStartFadeIn = false;
		if (Instance != null && Instance != this)
		{
			Debug.LogError("More than one AudioMixerManager exists.");
		}
		Instance = this;
	}

	private void Update()
	{
		if (!m_MusicVolumeFadeInComplete && m_AllowedToStartFadeIn)
		{
			if (Mathf.Approximately(m_StartMusicVolumeFadeInTime, 0f))
			{
				m_StartMusicVolumeFadeInTime = Time.unscaledTime;
			}
			float num = Mathf.Clamp01((Time.unscaledTime - m_StartMusicVolumeFadeInTime) / MUSIC_VOLUME_FADE_IN_TIME_SECONDS);
			if (Mathf.Approximately(num, 1f))
			{
				m_MusicVolumeFadeInComplete = true;
			}
			SetMusicVolume(Mathf.Lerp(0.2f * (float)Profiles.m_ActiveProfile.m_MusicVolume / 100f, (float)Profiles.m_ActiveProfile.m_MusicVolume / 100f, num));
		}
	}

	public static void AllowedToStartMusicFadeIn()
	{
		m_AllowedToStartFadeIn = true;
	}

	public static void Set(float master, float ambient, float sfx, float music, float ui)
	{
		MasterVolume = FaderCurve.Evaluate(master);
		AmbientVolume = FaderCurve.Evaluate(ambient);
		SFXVolume = FaderCurve.Evaluate(sfx);
		UIVolume = FaderCurve.Evaluate(ui);
		if (m_MusicVolumeFadeInComplete)
		{
			MusicVolume = FaderCurve.Evaluate(music);
		}
	}

	public static void PauseSFX()
	{
		if (Instance._debug)
		{
			Debug.Log("AudioMixerManager.PauseSFX()");
		}
		MasterAudio.MuteBus("Ambience");
		MasterAudio.MuteBus("Sim - HydraulicLoop");
		MasterAudio.MuteBus("Simulation");
		MasterAudio.MuteBus("Sim - Vehicle");
		m_PausedSFX = true;
	}

	public static void UnPauseSFX()
	{
		if (Instance._debug)
		{
			Debug.Log("AudioMixerManager.UnPauseSFX()");
		}
		MasterAudio.UnmuteBus("Ambience");
		MasterAudio.UnmuteBus("Sim - HydraulicLoop");
		MasterAudio.UnmuteBus("Simulation");
		MasterAudio.UnmuteBus("Sim - Vehicle");
		m_PausedSFX = false;
	}

	public static void PauseSimulationSFX()
	{
		if (Instance._debug)
		{
			Debug.Log("AudioMixerManager.PauseSimulationSFX()");
		}
		MasterAudio.MuteBus("Sim - HydraulicLoop");
		MasterAudio.MuteBus("Simulation");
		MasterAudio.MuteBus("Sim - Vehicle");
	}

	public static void UnPauseSimulationSFX()
	{
		if (Instance._debug)
		{
			Debug.Log("AudioMixerManager.UnPauseSimulationSFX()");
		}
		MasterAudio.UnmuteBus("Sim - HydraulicLoop");
		MasterAudio.UnmuteBus("Simulation");
		MasterAudio.UnmuteBus("Sim - Vehicle");
	}

	public static void PauseNonSimulationSFX()
	{
		if (Instance._debug)
		{
			Debug.Log("AudioMixerManager.PauseNonSimulationSFX()");
		}
		MasterAudio.MuteBus("Ambience");
	}

	public static void UnPauseNonSimulationSFX()
	{
		if (Instance._debug)
		{
			Debug.Log("AudioMixerManager.UnPauseNonSimulationSFX()");
		}
		MasterAudio.UnmuteBus("Ambience");
	}

	public static void PauseMusic()
	{
		if (Instance._debug)
		{
			Debug.Log("AudioMixerManager.PauseMusic()");
		}
		MasterAudio.PauseAllPlaylists();
	}

	public static void UnPauseMusic()
	{
		if (Instance._debug)
		{
			Debug.Log("AudioMixerManager.UnPauseMusic()");
		}
		MasterAudio.UnpauseAllPlaylists();
	}

	public static void ChangeSimulationPitch(float setPitch, float duration = 0.5f)
	{
		if (!Mathf.Approximately(m_LastSimulationPitch, setPitch))
		{
			m_LastSimulationPitch = setPitch;
			MasterAudio.GlideBusByPitch("Simulation", setPitch - Pitch, duration);
			MasterAudio.GlideBusByPitch("Sim - HydraulicLoop", setPitch - Pitch, duration);
			Pitch = setPitch;
			if (setPitch != 0f)
			{
				VehicleAudio.S_AddedPitch = setPitch - 1f;
				MasterAudio.UnmuteBus("Sim - Vehicle");
			}
			else
			{
				VehicleAudio.S_AddedPitch = -0.9f;
				MasterAudio.MuteBus("Sim - Vehicle");
			}
		}
	}

	public static void SetMusicVolume(float volume)
	{
		MusicVolume = FaderCurve.Evaluate(volume);
	}
}
