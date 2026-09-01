using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
	public float fadeInSpeed = 1f;

	public float fadeOutSpeed = 1f;

	public Renderer fadeBG;

	public float maxAlpha = 1f;

	public bool normalFadeIn;

	public bool useDeltaTime;

	public float startWaitDuration;

	public bool fadeIn;

	public float WaitWhileVisibleDuration;

	public bool enableVisOnAwake = true;

	public List<FadeAudio> fadeAudioCode = new List<FadeAudio>();

	private Color startCol;

	private IEnumerator fadeInCoroutine;

	private IEnumerator fadeOutCoroutine;

	public static bool faded;

	public static int count;

	private bool counted;

	private void Awake()
	{
		if (!counted)
		{
			count++;
			counted = true;
		}
		if (enableVisOnAwake)
		{
			fadeBG.enabled = true;
			MusicController.AmbienceFade = 0f;
		}
	}

	private void OnEnable()
	{
		if (!counted)
		{
			count++;
			counted = true;
		}
	}

	private IEnumerator Start()
	{
		if (fadeIn)
		{
			fadeBG.enabled = false;
		}
		startCol = fadeBG.material.GetColor("_TintColor");
		yield return new WaitForSeconds(startWaitDuration);
		if (fadeIn)
		{
			yield return FadeIn();
			yield return new WaitForSeconds(WaitWhileVisibleDuration);
		}
		if (normalFadeIn)
		{
			FadeOut();
		}
	}

	public Coroutine FadeIn()
	{
		if (fadeOutCoroutine != null)
		{
			StopCoroutine(fadeOutCoroutine);
			fadeOutCoroutine = null;
		}
		if (ReferenceMaster.onSceneTransition != null)
		{
			ReferenceMaster.onSceneTransition();
		}
		FadeSound();
		fadeInCoroutine = Fade(maxAlpha);
		return StartCoroutine(fadeInCoroutine);
	}

	public Coroutine FadeOut()
	{
		if (fadeInCoroutine != null)
		{
			StopCoroutine(fadeInCoroutine);
			fadeInCoroutine = null;
		}
		if ((bool)SingleInstanceFindOnly<AddPiece>.Instance)
		{
			SingleInstanceFindOnly<AddPiece>.Instance.ResetMapperTargets();
		}
		fadeOutCoroutine = Fade(0f);
		return StartCoroutine(fadeOutCoroutine);
	}

	private IEnumerator Fade(float alpha)
	{
		if (alpha > 0f)
		{
			faded = true;
		}
		fadeBG.enabled = true;
		float cTime = 0f;
		float rate = 1f / fadeOutSpeed;
		Color newCol = startCol;
		float startAlpha = fadeBG.material.GetColor("_TintColor").a;
		float a = MusicController.AmbienceFade;
		while (cTime < 1f)
		{
			cTime = ((!useDeltaTime) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.deltaTime * rate));
			newCol.a = Mathf.Lerp(startAlpha, alpha, cTime);
			fadeBG.material.SetColor("_TintColor", newCol);
			MusicController.AmbienceFade = Mathf.Lerp(a, 1f - Mathf.Clamp01(alpha * 5f), cTime);
			yield return null;
		}
		MusicController.AmbienceFade = 1f - Mathf.Clamp01(alpha * 5f);
		if (alpha > 0f)
		{
			fadeInCoroutine = null;
			yield break;
		}
		faded = false;
		fadeOutCoroutine = null;
		fadeBG.enabled = false;
	}

	private void FadeSound()
	{
		for (int i = 0; i < fadeAudioCode.Count; i++)
		{
			fadeAudioCode[i].FadeOut(fadeInSpeed);
		}
	}

	private void OnDisable()
	{
		if (counted)
		{
			count--;
			counted = false;
		}
		if (count <= 0)
		{
			count = 0;
			faded = false;
			MusicController.AmbienceFade = 1f;
		}
	}
}
