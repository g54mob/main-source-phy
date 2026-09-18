using System;
using UnityEngine;

public class LoopAudioSource : MonoBehaviour
{
	[SerializeField]
	private AudioClipTypes audioClipType;

	[SerializeField]
	private bool isPlayOnEnable;

	private AudioSource audioSource;

	private float fullVolume;

	private AudioClipObject currentAudioPlayingObject;

	private bool isPlaying;

	private void Awake()
	{
		currentAudioPlayingObject = Singleton<AudioManager>.Instance.GetAudioClipObject(audioClipType);
		audioSource = base.gameObject.AddComponent<AudioSource>();
		audioSource.playOnAwake = isPlayOnEnable;
		if (!(currentAudioPlayingObject == null))
		{
			RefreshAudio();
			audioSource.volume = 0f;
			if (currentAudioPlayingObject.isPlayGlobaly)
			{
				audioSource.maxDistance = 1000f;
				audioSource.minDistance = 1000f;
				return;
			}
			audioSource.maxDistance = currentAudioPlayingObject.audioFullVolumeDistance;
			audioSource.minDistance = 0f;
			audioSource.spatialBlend = 1f;
			audioSource.rolloffMode = AudioRolloffMode.Linear;
		}
	}

	private void OnEnable()
	{
		if (!(currentAudioPlayingObject == null))
		{
			isPlaying = false;
			RefreshAudio();
			if (!isPlayOnEnable)
			{
				audioSource.Stop();
			}
			else
			{
				PlayAudio();
			}
			AudioSlidersController instance = Singleton<AudioSlidersController>.Instance;
			instance.OnSlidersChange = (Action)Delegate.Combine(instance.OnSlidersChange, new Action(RefreshAudio));
			EventManager.GamePause += PauseAudio;
			EventManager.GameEnd += PauseAudio;
			EventManager.GameResume += ResumeAudio;
		}
	}

	private void OnDisable()
	{
		if (!(currentAudioPlayingObject == null))
		{
			if (Singleton<AudioSlidersController>.Instance != null)
			{
				AudioSlidersController instance = Singleton<AudioSlidersController>.Instance;
				instance.OnSlidersChange = (Action)Delegate.Remove(instance.OnSlidersChange, new Action(RefreshAudio));
			}
			EventManager.GamePause -= PauseAudio;
			EventManager.GameEnd -= PauseAudio;
			EventManager.GameResume -= ResumeAudio;
		}
	}

	public void PlayAudio()
	{
		if (base.gameObject.activeInHierarchy && !isPlaying)
		{
			isPlaying = true;
			TweenController.FadeInAudioSource(base.gameObject, fullVolume, audioSource, TweenDuration.Super_Short);
			PlayNextAudioClip();
		}
	}

	public void StopAudio()
	{
		if (isPlaying)
		{
			isPlaying = false;
			TweenController.FadeOutAudioSource(base.gameObject, audioSource, TweenDuration.Super_Short, delegate
			{
				audioSource.Stop();
			});
		}
	}

	private void PauseAudio()
	{
		if (isPlaying)
		{
			audioSource.Pause();
		}
	}

	private void ResumeAudio()
	{
		if (isPlaying)
		{
			audioSource.UnPause();
		}
	}

	public void RefreshAudio()
	{
		if (currentAudioPlayingObject != null)
		{
			fullVolume = Singleton<AudioManager>.Instance.GetAudioVolume(currentAudioPlayingObject);
			audioSource.volume = fullVolume;
		}
	}

	private void Update()
	{
		if (!(currentAudioPlayingObject == null) && isPlaying && !audioSource.isPlaying)
		{
			PlayNextAudioClip();
		}
	}

	public void PlayNextAudioClip()
	{
		if (!(currentAudioPlayingObject == null))
		{
			audioSource.clip = currentAudioPlayingObject.GetAudioClip();
			audioSource.Play();
		}
	}
}
