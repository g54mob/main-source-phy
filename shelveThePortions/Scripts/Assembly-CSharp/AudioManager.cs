using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	private readonly string AudioClipObjectPath = "Scriptable Objects/AudioClipObjects";

	[Range(1f, 3f)]
	[SerializeField]
	private int audioFadeDistanceMultiplier = 2;

	[SerializeField]
	private int audioSourceCounts = 5;

	private List<AudioSource> AudioSources;

	private List<AudioClipObject> audioClipObjectsList;

	private List<AudioClipTypes> AudioClipEnums;

	private Dictionary<AudioClipTypes, int> AudioClipDict;

	private Dictionary<AudioClipTypes, AudioClipObject> AudioClipObjectDict;

	private Transform gameCenter;

	private WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.1f);

	protected override void Awake()
	{
		base.Awake();
		LoadAudioFiles();
		InitiateAudioSources();
		EventManager.MainMenuLoaded += ResetAudioManager;
		EventManager.GameLoading += ResetAudioManager;
		EventManager.GameStart += ResetAudioManager;
		EventManager.GameEnd += ResetAudioManager;
	}

	protected override void OnDestroy()
	{
		EventManager.MainMenuLoaded -= ResetAudioManager;
		EventManager.GameLoading -= ResetAudioManager;
		EventManager.GameStart -= ResetAudioManager;
		EventManager.GameEnd -= ResetAudioManager;
		base.OnDestroy();
	}

	public void ResetAudioManager()
	{
		gameCenter = SceneSingleton<FirstPersonController>.Instance.transform;
		StopAllCoroutines();
		if (AudioClipDict != null)
		{
			foreach (AudioClipTypes item in AudioClipDict.Keys.ToList())
			{
				AudioClipDict[item] = 0;
			}
		}
		foreach (AudioSource audioSource in AudioSources)
		{
			audioSource.Stop();
		}
	}

	private void InitiateAudioSources()
	{
		if (AudioSources == null)
		{
			AudioSources = new List<AudioSource>();
		}
		if (AudioSources.Count == 0)
		{
			AudioSources = new List<AudioSource>();
			for (int i = 0; i < audioSourceCounts; i++)
			{
				AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
				audioSource.playOnAwake = false;
				AudioSources.Add(audioSource);
			}
		}
	}

	private void LoadAudioFiles()
	{
		AudioClipDict = new Dictionary<AudioClipTypes, int>();
		AudioClipObjectDict = new Dictionary<AudioClipTypes, AudioClipObject>();
		audioClipObjectsList = new List<AudioClipObject>();
		audioClipObjectsList.AddRange(Resources.LoadAll<AudioClipObject>(AudioClipObjectPath));
		AudioClipEnums = new List<AudioClipTypes>();
		foreach (AudioClipObject audioClipObjects in audioClipObjectsList)
		{
			AudioClipEnums.Add(audioClipObjects.audioClipType);
			AudioClipObjectDict.Add(audioClipObjects.audioClipType, audioClipObjects);
			AudioClipDict.Add(audioClipObjects.audioClipType, 0);
		}
	}

	public AudioClipObject GetAudioClipObject(AudioClipTypes audioClipType)
	{
		return AudioClipObjectDict[audioClipType];
	}

	public void PlayClip(AudioClipTypes audioClipType, Vector3 AudioSourcePosition = default(Vector3))
	{
		if (audioClipType == AudioClipTypes.None)
		{
			return;
		}
		AudioSource unusedSource = GetUnusedSource();
		if (unusedSource == null)
		{
			return;
		}
		AudioClipObject audioClipObject = audioClipObjectsList[AudioClipEnums.IndexOf(audioClipType)];
		if (CanPlayAudioClip(audioClipObject, AudioSourcePosition))
		{
			AudioClipDict[audioClipType]++;
			unusedSource.clip = audioClipObject.GetAudioClip();
			unusedSource.volume = GetAudioVolume(audioClipObject);
			if (!audioClipObject.isPlayGlobaly)
			{
				ConfigureDistanceVolume(unusedSource, audioClipObject, AudioSourcePosition);
			}
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(PlayAudioSource(unusedSource, audioClipType));
			}
		}
	}

	private void ConfigureDistanceVolume(AudioSource audioSource, AudioClipObject audioClipObject, Vector3 AudioSourcePosition)
	{
		if (!(AudioSourcePosition == default(Vector3)))
		{
			float audioFullVolumeDistance = audioClipObject.audioFullVolumeDistance;
			float num = audioFullVolumeDistance * (float)audioFadeDistanceMultiplier;
			float num2 = Vector3.Distance(AudioSourcePosition, gameCenter.position);
			if (num2 > audioFullVolumeDistance + num)
			{
				audioSource.volume = 0f;
				return;
			}
			num2 -= audioFullVolumeDistance;
			float num3 = num2 / num;
			audioSource.volume -= audioSource.volume * num3;
		}
	}

	private bool CanPlayAudioClip(AudioClipObject audioClipObject, Vector3 AudioSourcePosition)
	{
		if (AudioClipDict[audioClipObject.audioClipType] >= audioClipObject.copiesPlayedAtOnce)
		{
			return false;
		}
		if (audioClipObject.isPlayGlobaly)
		{
			return true;
		}
		if (AudioSourcePosition != default(Vector3))
		{
			float audioFullVolumeDistance = audioClipObject.audioFullVolumeDistance;
			float num = audioFullVolumeDistance * (float)audioFadeDistanceMultiplier;
			if (Vector3.Distance(AudioSourcePosition, gameCenter.position) > audioFullVolumeDistance + num)
			{
				return false;
			}
		}
		return true;
	}

	private IEnumerator PlayAudioSource(AudioSource audioSource, AudioClipTypes audioClipType)
	{
		audioSource.Play();
		while (audioSource.isPlaying)
		{
			yield return null;
		}
		audioSource.clip = null;
		AudioClipDict[audioClipType]--;
	}

	private AudioSource GetUnusedSource()
	{
		foreach (AudioSource audioSource in AudioSources)
		{
			if (!audioSource.isPlaying)
			{
				return audioSource;
			}
		}
		return null;
	}

	public float GetAudioVolume(AudioClipObject audioClipObject)
	{
		return audioClipObject.volume * AudioSlidersController.GetVolumePercent(audioClipObject.audioType);
	}
}
