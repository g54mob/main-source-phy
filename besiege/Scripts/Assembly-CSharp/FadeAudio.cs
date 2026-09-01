using System.Collections;
using UnityEngine;

public class FadeAudio : MonoBehaviour
{
	public AudioSource audioSource;

	public bool useDeltaTime;

	public void FadeOut(float duration)
	{
		StartCoroutine(FadeAudioVolume(0f, duration));
	}

	public IEnumerator FadeAudioVolume(float endVol, float duration)
	{
		float cTime = 0f;
		float rate = 1f / duration;
		float startVol = audioSource.volume;
		while (cTime < 1f)
		{
			cTime = ((!useDeltaTime) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.deltaTime * rate));
			audioSource.volume = Mathf.Lerp(startVol, endVol, cTime);
			yield return null;
		}
	}
}
