using System.Collections;
using UnityEngine;

public class IntroSceneManager : SceneSingleton<IntroSceneManager>
{
	private bool MenuSceneLoaded;

	public void Start()
	{
		FPSLimiter.UpdateFPSLimit();
		PlayerPrefs.SetInt("OpenMainMenu", 1);
		Invoke("LoadMainMenuScene", 0.5f);
		StartCoroutine(StartIntro());
	}

	private IEnumerator StartIntro()
	{
		yield return new WaitForSecondsRealtime(1f);
		LoadMainMenuScene();
	}

	public void LoadMainMenuScene()
	{
		if (!MenuSceneLoaded)
		{
			MenuSceneLoaded = true;
			StopAllCoroutines();
			Singleton<ScenesManager>.Instance.GoToGameScene(NoTween: true);
		}
	}
}
