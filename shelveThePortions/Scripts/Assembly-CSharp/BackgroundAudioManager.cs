using System;
using UnityEngine;

public class BackgroundAudioManager : Singleton<BackgroundAudioManager>
{
	private AudioClipObject baseAudioClipObject;

	private AudioClipObject extraAudioClipObject;

	[SerializeField]
	private AudioSource baseAudioSource;

	[SerializeField]
	private AudioSource extraAudioSource;

	private void Start()
	{
		MuteSource();
		RefreshAudio();
		PlayBaseAudio(AudioClipTypes.Background_InGame);
		PlayExtraBackgroundAudio(AudioClipTypes.Background_Ambient);
		AudioSlidersController audioSlidersController = Singleton<AudioSlidersController>.Instance;
		audioSlidersController.OnSlidersChange = (Action)Delegate.Combine(audioSlidersController.OnSlidersChange, new Action(RefreshAudio));
	}

	private void OnDisable()
	{
		if (Singleton<AudioSlidersController>.Instance != null)
		{
			AudioSlidersController audioSlidersController = Singleton<AudioSlidersController>.Instance;
			audioSlidersController.OnSlidersChange = (Action)Delegate.Remove(audioSlidersController.OnSlidersChange, new Action(RefreshAudio));
		}
	}

	public void RefreshAudio()
	{
		if (baseAudioClipObject != null)
		{
			baseAudioSource.volume = Singleton<AudioManager>.Instance.GetAudioVolume(baseAudioClipObject);
		}
		if (extraAudioClipObject != null)
		{
			extraAudioSource.volume = Singleton<AudioManager>.Instance.GetAudioVolume(extraAudioClipObject);
		}
	}

	public void PlayBaseAudio(AudioClipTypes audioClipType)
	{
		if (!(baseAudioClipObject == Singleton<AudioManager>.Instance.GetAudioClipObject(audioClipType)))
		{
			baseAudioClipObject = Singleton<AudioManager>.Instance.GetAudioClipObject(audioClipType);
			baseAudioSource.clip = baseAudioClipObject.audioClipList[UnityEngine.Random.Range(0, baseAudioClipObject.audioClipList.Count)];
			baseAudioSource.Play();
			FadeAudioSource(baseAudioSource, Singleton<AudioManager>.Instance.GetAudioVolume(baseAudioClipObject));
		}
	}

	public void PlayExtraBackgroundAudio(AudioClipTypes audioClipType)
	{
		if (!(extraAudioClipObject == Singleton<AudioManager>.Instance.GetAudioClipObject(audioClipType)))
		{
			extraAudioClipObject = Singleton<AudioManager>.Instance.GetAudioClipObject(audioClipType);
			extraAudioSource.clip = extraAudioClipObject.audioClipList[UnityEngine.Random.Range(0, extraAudioClipObject.audioClipList.Count)];
			extraAudioSource.Play();
			FadeAudioSource(extraAudioSource, Singleton<AudioManager>.Instance.GetAudioVolume(extraAudioClipObject));
		}
	}

	public void StopExtraBackgroundAudio()
	{
		TweenController.FadeOutAudioSource(base.gameObject, extraAudioSource, TweenDuration.Very_Short, delegate
		{
			extraAudioClipObject = null;
			extraAudioSource.Stop();
		}, ignoreTimeScale: true);
	}

	public void FadeAudioSource(AudioSource audioSource, float fadeInVolume)
	{
		TweenController.FadeOutAudioSource(base.gameObject, audioSource, TweenDuration.Very_Short, delegate
		{
			TweenController.FadeInAudioSource(base.gameObject, fadeInVolume, audioSource, TweenDuration.Very_Short);
		});
	}

	private void MuteSource()
	{
		baseAudioSource.volume = 0f;
		extraAudioSource.volume = 0f;
	}

	private void Update()
	{
		if (!baseAudioSource.isPlaying)
		{
			PlayNextBaseAudioClip();
		}
		if (!extraAudioSource.isPlaying)
		{
			PlayNextExtraAudioClip();
		}
	}

	public void PlayNextBaseAudioClip()
	{
		if (!(baseAudioClipObject == null))
		{
			baseAudioSource.clip = baseAudioClipObject.audioClipList[UnityEngine.Random.Range(0, baseAudioClipObject.audioClipList.Count)];
			baseAudioSource.Play();
		}
	}

	public void PlayNextExtraAudioClip()
	{
		if (!(extraAudioClipObject == null))
		{
			extraAudioSource.clip = extraAudioClipObject.audioClipList[UnityEngine.Random.Range(0, extraAudioClipObject.audioClipList.Count)];
			extraAudioSource.Play();
		}
	}
}
