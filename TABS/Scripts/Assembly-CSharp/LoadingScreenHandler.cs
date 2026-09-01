using System;
using System.Collections;
using UnityEngine;

public class LoadingScreenHandler : ServicePrefab
{
	private enum LoadingScreenFadeState
	{
		FadedOut = 0,
		Fading = 1,
		FadedIn = 2
	}

	private const float FadedInAlphaValue = 1f;

	private const float FadedOutAlphaValue = 0f;

	private CanvasGroup m_fade;

	private Action m_onFadingFinished;

	private Coroutine fadeRoutine;

	private LoadingScreenFadeState currentLoadingScreenFadeState;

	public override void OnAwake()
	{
		currentLoadingScreenFadeState = LoadingScreenFadeState.FadedOut;
		Init();
	}

	private void Init()
	{
		m_fade = GetComponentInChildren<CanvasGroup>();
	}

	public void ShowLoadingScreen(Action onFinished)
	{
		if (currentLoadingScreenFadeState == LoadingScreenFadeState.Fading || currentLoadingScreenFadeState == LoadingScreenFadeState.FadedIn)
		{
			onFinished?.Invoke();
		}
		else
		{
			InternalFadeBlack(fade: true, onFinished);
		}
	}

	public void HideLoadingScreen(Action onFinished)
	{
		if (currentLoadingScreenFadeState == LoadingScreenFadeState.Fading || currentLoadingScreenFadeState == LoadingScreenFadeState.FadedOut)
		{
			onFinished?.Invoke();
			if (fadeRoutine != null)
			{
				StopFadeCoroutine(fadeToBlack: false);
			}
		}
		else
		{
			InternalFadeBlack(fade: false, onFinished);
		}
	}

	private void InternalFadeBlack(bool fade, Action onFinished)
	{
		if (fadeRoutine != null)
		{
			StopFadeCoroutine(fade);
		}
		fadeRoutine = StartCoroutine(FadeScreen(fade, 10f));
		m_onFadingFinished = onFinished;
	}

	private void StopFadeCoroutine(bool fadeToBlack)
	{
		m_fade.alpha = (fadeToBlack ? 1f : 0f);
		currentLoadingScreenFadeState = (fadeToBlack ? LoadingScreenFadeState.FadedIn : LoadingScreenFadeState.FadedOut);
		if (m_onFadingFinished != null)
		{
			m_onFadingFinished?.Invoke();
			m_onFadingFinished = null;
		}
		StopCoroutine(fadeRoutine);
	}

	private IEnumerator FadeScreen(bool fadeToBlack, float speed = 1f)
	{
		currentLoadingScreenFadeState = LoadingScreenFadeState.Fading;
		float target = (fadeToBlack ? 1f : 0f);
		while (true)
		{
			m_fade.alpha = Mathf.MoveTowards(m_fade.alpha, target, Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.02f) * speed);
			if (m_fade.alpha == target)
			{
				break;
			}
			yield return null;
		}
		yield return new WaitForSecondsRealtime(0.2f);
		if (fadeToBlack)
		{
			currentLoadingScreenFadeState = LoadingScreenFadeState.FadedIn;
		}
		else
		{
			currentLoadingScreenFadeState = LoadingScreenFadeState.FadedOut;
		}
	}

	private void Update()
	{
		if (m_onFadingFinished != null && currentLoadingScreenFadeState != LoadingScreenFadeState.Fading)
		{
			m_onFadingFinished?.Invoke();
			m_onFadingFinished = null;
		}
	}

	private void SetScreen(float alpha)
	{
		m_fade.alpha = alpha;
	}
}
