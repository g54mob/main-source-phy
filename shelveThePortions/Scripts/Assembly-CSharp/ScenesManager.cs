using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : Singleton<ScenesManager>
{
	public static bool isSceneLoading = false;

	private static string gameSceneName = "Scene_Game";

	private static string free_GameSceneName = "Scene_Game_Free";

	private static string introScene = "Scene_Intro";

	[SerializeField]
	private Image blackImage;

	[SerializeField]
	private GameObject blackImageCanvas;

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void GoToScene(int sceneNumber, bool NoTween = false)
	{
		if (NoTween)
		{
			SceneManager.LoadScene(sceneNumber);
			return;
		}
		FadeInBlack(delegate
		{
			SceneManager.LoadScene(sceneNumber);
		});
	}

	public void GoToScene(string sceneName, bool NoTween = false)
	{
		if (NoTween)
		{
			SceneManager.LoadScene(sceneName);
			return;
		}
		FadeInBlack(delegate
		{
			SceneManager.LoadScene(sceneName);
		});
	}

	public void GoToGameScene(bool NoTween = false)
	{
		if (NoTween)
		{
			SceneManager.LoadScene(1);
			return;
		}
		FadeInBlack(delegate
		{
			SceneManager.LoadScene(1);
		});
	}

	public void GoToIntroScene(bool NoTween = false)
	{
		if (NoTween)
		{
			SceneManager.LoadScene(introScene);
			return;
		}
		FadeInBlack(delegate
		{
			SceneManager.LoadScene(introScene);
		});
	}

	public void ReloadScene(bool NoTween = false)
	{
		EventManager.ActivateEvent(EventTypes.GameRestart);
		if (NoTween)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			return;
		}
		FadeInBlack(delegate
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		});
	}

	public static void ExitApplication()
	{
		Application.Quit();
	}

	private void FadeInBlack(Action OnComplete)
	{
		UIBackKeyManager.ClearQueue();
		blackImageCanvas.gameObject.SetActive(value: true);
		AlphaSystem.Alphalizer(blackImage, 1f, TweenDuration.Short, IgnoreTimeScale: true, delegate
		{
			if (OnComplete != null)
			{
				OnComplete();
			}
		});
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		blackImageCanvas.gameObject.SetActive(value: true);
		AlphaSystem.Alphalizer(blackImage, 0f, TweenDuration.Short, IgnoreTimeScale: true, delegate
		{
			blackImageCanvas.gameObject.SetActive(value: false);
		});
	}
}
