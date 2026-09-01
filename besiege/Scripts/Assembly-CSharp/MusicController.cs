using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicController : SingleInstance<MusicController>
{
	public class AudioRef
	{
		public AudioClip clip;

		public float volume = 0.15f;

		public float pitch = 1f;

		public AudioSource source;
	}

	[HideInInspector]
	public bool customMusicPresent;

	public AudioMixer mixer;

	[SerializeField]
	private AnimationCurve pitchSfxCurve = AnimationCurve.Linear(0f, 0f, 2f, 2f);

	[SerializeField]
	private AnimationCurve pitchAmbientCurve = AnimationCurve.Linear(0f, 0.75f, 2f, 1.25f);

	[SerializeField]
	protected AudioSource source1;

	[SerializeField]
	protected AudioSource source2;

	public float crossFadeDuration = 1f;

	public static float AmbienceFade = 1f;

	private float musicVolume = 101f;

	private float sfxVolume = 101f;

	private float blockVolume = 101f;

	private float blockWaterVolume = 101f;

	private float physicsVolume = 101f;

	private float physicsWaterVolume = 101f;

	private float ambientVolume = 101f;

	private float ambientVolume2 = 101f;

	private float uiVolume = 101f;

	private bool transitioning;

	private float ambientPitch = 1f;

	private float sfxPitch = 1f;

	private float underwaterPitch = 1f;

	public AudioRef mainMenuAudio = new AudioRef();

	public AudioRef current = new AudioRef();

	public AudioRef next = new AudioRef();

	private bool focused = true;

	public bool HasAnySources
	{
		get
		{
			return source1 != null;
		}
	}

	public override string Name
	{
		get
		{
			return "MusicController (Created)";
		}
	}

	private void Awake()
	{
		current.source = source2;
		next.source = source1;
		SingleInstance<MusicController>.Initialize(this);
		SceneManager.sceneLoaded += OnSceneLoad;
		customMusicPresent = false;
	}

	public override void SetUp()
	{
		if (source1 == null)
		{
			Transform transform = base.transform.FindChild("MUSIC");
			if (transform != null)
			{
				source1 = transform.GetComponent<AudioSource>();
			}
			else
			{
				GameObject gameObject = new GameObject("MUSIC");
				source1 = gameObject.AddComponent<AudioSource>();
				source1.loop = true;
				source1.volume = 0.15f;
				source1.playOnAwake = false;
			}
		}
		mainMenuAudio.clip = source1.clip;
		mainMenuAudio.volume = source1.volume;
		mainMenuAudio.pitch = source1.pitch;
		if (source2 == null)
		{
			Transform transform = base.transform.FindChild("CUSTOM MUSIC");
			if (transform != null)
			{
				source2 = transform.GetComponent<AudioSource>();
			}
			else
			{
				GameObject gameObject2 = new GameObject("CUSTOM MUSIC");
				source2 = gameObject2.AddComponent<AudioSource>();
				source2.loop = true;
				source2.volume = 0.55f;
				source2.playOnAwake = false;
			}
		}
		if (OptionsMaster.BesiegeConfig.MusicEnabled && source1.gameObject.activeInHierarchy && source1.enabled)
		{
			PlayMainMenuTrack();
		}
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		customMusicPresent = Object.FindObjectOfType<SetCustomLevelMusic>() != null;
		if (!customMusicPresent)
		{
			PlayMainMenuTrack();
		}
	}

	public void PlayCustomTrack(AudioClip clip, float vol, float pitch = 1f, bool ignoreConfig = false)
	{
		if (!(current.clip == clip) && !(next.source == null) && (!transitioning || !(next.clip == clip)))
		{
			next.clip = clip;
			next.volume = vol;
			next.pitch = pitch;
			next.source.clip = clip;
			next.source.volume = 0f;
			StopAllCoroutines();
			StartCoroutine(Transition(ignoreConfig));
		}
	}

	public void PlayMainMenuTrack()
	{
		PlayCustomTrack(mainMenuAudio.clip, mainMenuAudio.volume, mainMenuAudio.pitch);
	}

	public IEnumerator Transition(bool ignoreConfig = false)
	{
		transitioning = true;
		if (ignoreConfig || OptionsMaster.BesiegeConfig.MusicEnabled)
		{
			next.source.Play();
		}
		float vol1 = current.source.volume;
		float vol2 = next.source.volume;
		float pitch = next.source.pitch;
		for (float t = 0f; t < crossFadeDuration * 2f; t += Time.deltaTime)
		{
			float pct = t / crossFadeDuration;
			current.source.volume = Mathf.Lerp(vol1, 0f, pct * 0.5f);
			if (pct < 1f)
			{
				next.source.volume = Mathf.Lerp(vol2, next.volume, pct);
				next.source.pitch = Mathf.Lerp(pitch, next.pitch, pct);
			}
			else
			{
				next.source.volume = next.volume;
				next.source.pitch = next.pitch;
			}
			yield return null;
		}
		current.volume = 0f;
		current.source.Stop();
		next.source.volume = next.volume;
		Swap();
		transitioning = false;
	}

	protected void Swap()
	{
		AudioRef audioRef = new AudioRef();
		SetRefTo(current, audioRef);
		SetRefTo(next, current);
		SetRefTo(audioRef, next);
	}

	protected void SetRefTo(AudioRef from, AudioRef to)
	{
		to.clip = from.clip;
		to.volume = from.volume;
		to.pitch = from.pitch;
		to.source = from.source;
	}

	public void Mute()
	{
		if (current.source.volume > 0f)
		{
			current.source.Pause();
		}
		if (next.source.volume > 0f && next.source.isPlaying)
		{
			next.source.Pause();
		}
	}

	public void Resume()
	{
		if (current.source.volume > 0f)
		{
			current.source.Play();
		}
		if (next.source.volume > 0f)
		{
			if (transitioning)
			{
				next.source.Play();
			}
			else
			{
				next.source.volume = 0f;
			}
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		focused = hasFocus;
	}

	private void LateUpdate()
	{
		BesiegeConfig besiegeConfig = OptionsMaster.BesiegeConfig ?? OptionsMaster.DefaultConfig;
		UpdateVolume("UIVolume", besiegeConfig.UIVolume, ref uiVolume);
		UpdateVolume("MusicVolume", besiegeConfig.MusicVolume, ref musicVolume);
		UpdateVolume("SfxVolume", besiegeConfig.SfxVolume, ref sfxVolume);
		UpdateVolume("BlockVolume", besiegeConfig.BlockVolume, ref blockVolume);
		UpdateVolume("BlockWaterVolume", besiegeConfig.BlockVolume, ref blockWaterVolume);
		UpdateVolume("PhysicsVolume", besiegeConfig.PhysicsVolume, ref physicsVolume);
		UpdateVolume("PhysicsWaterVolume", besiegeConfig.PhysicsVolume, ref physicsWaterVolume);
		float num = ((!focused && besiegeConfig.DuckVolumeUnfocused) ? Mathf.Min(besiegeConfig.AmbientVolume, 10f) : besiegeConfig.AmbientVolume);
		num *= AmbienceFade;
		UpdateVolume("AmbientVolume", num, ref ambientVolume);
		UpdateVolume("AmbientAltVolume", num, ref ambientVolume2);
		if (!StatMaster.SimulationStartInProgress)
		{
			float pitch = pitchAmbientCurve.Evaluate(Time.timeScale);
			UpdatePitch("AmbientPitch", pitch, ref ambientPitch);
			pitch = pitchSfxCurve.Evaluate(Time.timeScale);
			UpdatePitch("SfxPitch", pitch, ref sfxPitch);
			UpdatePitch("UnderwaterPitch", pitch, ref underwaterPitch);
		}
	}

	public void UpdateVolume(string parameter, float vol, ref float current)
	{
		if (vol != current)
		{
			current = vol;
			if (current <= 1f)
			{
				mixer.SetFloat(parameter, -80f);
			}
			else
			{
				mixer.SetFloat(parameter, Mathf.Log(current / 100f, 10f) * 20f);
			}
		}
	}

	public void UpdatePitch(string parameter, float pitch, ref float current)
	{
		if (pitch != current)
		{
			current = pitch;
			mixer.SetFloat(parameter, pitch);
		}
	}
}
