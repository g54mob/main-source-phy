using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenCanvas : MonoBehaviour
{
	public float fadeInSpeed = 1f;

	public float fadeOutSpeed = 1f;

	public Image fadeBG;

	public float maxAlpha = 1f;

	public bool normalFadeIn;

	public bool useDeltaTime;

	public float startWaitDuration;

	public bool fadeIn;

	public float WaitWhileVisibleDuration;

	public bool enableVisOnAwake = true;

	public List<FadeAudio> fadeAudioCode = new List<FadeAudio>();

	private IEnumerator fadeInCoroutine;

	private IEnumerator fadeOutCoroutine;

	private void Awake()
	{
		if (enableVisOnAwake)
		{
			fadeBG.enabled = true;
		}
	}

	private IEnumerator Start()
	{
		if (fadeIn)
		{
			fadeBG.enabled = false;
		}
		yield return new WaitForSeconds(startWaitDuration);
		if (fadeIn)
		{
			fadeInCoroutine = FadeIn();
			yield return StartCoroutine(fadeInCoroutine);
			yield return new WaitForSeconds(WaitWhileVisibleDuration);
		}
		if (normalFadeIn)
		{
			fadeOutCoroutine = FadeOut();
			StartCoroutine(fadeOutCoroutine);
		}
	}

	public IEnumerator FadeIn()
	{
		if (fadeOutCoroutine != null)
		{
			StopCoroutine(fadeOutCoroutine);
		}
		FadeSound();
		fadeBG.enabled = true;
		float cTime = 0f;
		float rate = 1f / fadeInSpeed;
		while (cTime < 1f)
		{
			cTime = ((!useDeltaTime) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.deltaTime * rate));
			float a = Mathf.Lerp(0f, maxAlpha, cTime);
			fadeBG.GetComponent<CanvasRenderer>().SetAlpha(a);
			yield return null;
		}
	}

	private IEnumerator FadeOut()
	{
		if (fadeInCoroutine != null)
		{
			StopCoroutine(fadeInCoroutine);
		}
		fadeBG.enabled = true;
		float cTime = 0f;
		float rate = 1f / fadeOutSpeed;
		while (cTime < 1f)
		{
			cTime = ((!useDeltaTime) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.deltaTime * rate));
			float a = Mathf.Lerp(maxAlpha, 0f, cTime);
			fadeBG.GetComponent<CanvasRenderer>().SetAlpha(a);
			yield return null;
		}
		fadeBG.enabled = false;
	}

	private void FadeSound()
	{
		for (int i = 0; i < fadeAudioCode.Count; i++)
		{
			fadeAudioCode[i].FadeOut(fadeInSpeed);
		}
	}
}
