using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
	public SplashScreenAnimated m_SplashScreenAnimated;

	private bool m_PlayedSplashAnimation;

	private readonly float DEFAULT_NORMALIZED_VOLUME = 0.7f;

	private void Awake()
	{
	}

	private void Start()
	{
		Time.timeScale = 1f;
		StartCoroutine(LoadScene());
	}

	private void Update()
	{
		if (!m_PlayedSplashAnimation && (m_SplashScreenAnimated.m_AudioSource.clip.loadState == AudioDataLoadState.Loaded || m_SplashScreenAnimated.m_AudioSource.clip.loadState == AudioDataLoadState.Failed))
		{
			m_SplashScreenAnimated.Animate(GetNormalizedVolume());
			m_PlayedSplashAnimation = true;
		}
	}

	private float GetNormalizedVolume()
	{
		try
		{
			string activeProfileName = ProfileInfo.GetActiveProfileName();
			if (string.IsNullOrEmpty(activeProfileName))
			{
				return DEFAULT_NORMALIZED_VOLUME;
			}
			Profile profile = new Profile();
			if (profile == null)
			{
				return DEFAULT_NORMALIZED_VOLUME;
			}
			profile.Init(activeProfileName);
			if (!profile.Load())
			{
				return DEFAULT_NORMALIZED_VOLUME;
			}
			if (profile.m_Mute)
			{
				return 0f;
			}
			return (float)profile.m_MasterVolume / 100f * (float)profile.m_SFXVolume / 100f;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception trying to read volume from profile: " + ex.Message);
			return DEFAULT_NORMALIZED_VOLUME;
		}
	}

	private IEnumerator LoadScene()
	{
		yield return null;
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("AlwaysLoaded");
		asyncOperation.allowSceneActivation = false;
		while (!asyncOperation.isDone)
		{
			if (asyncOperation.progress >= 0.9f && m_SplashScreenAnimated.IsFinished())
			{
				asyncOperation.allowSceneActivation = true;
			}
			yield return null;
		}
	}
}
