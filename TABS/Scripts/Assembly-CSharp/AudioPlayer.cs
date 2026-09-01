using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class AudioPlayer
{
	public AudioSource source;

	public bool isUsed;

	public float clipLength;

	public float currentTimePlayed;

	public SoundEffectInstance soundEffectInstance;

	public Transform transformToFollow;

	public float defaultPitch;

	public IEnumerator FadeOut(float fadeOutTime = 0.5f)
	{
		float t = 0f;
		float startVolume = source.volume;
		while (t < fadeOutTime)
		{
			t += Time.unscaledDeltaTime;
			source.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutTime);
			yield return null;
		}
		isUsed = false;
		source.Stop();
	}
}
