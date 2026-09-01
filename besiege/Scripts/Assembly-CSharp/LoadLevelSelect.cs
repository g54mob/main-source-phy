using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelSelect : ClickBehaviour
{
	public Transform button;

	public string levelToLoad;

	public FadeScreen FadeCodey;

	private void Awake()
	{
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		AudioSource component = GetComponent<AudioSource>();
		if (component != null)
		{
			component.Play();
		}
	}

	public override void OnClickReleased()
	{
		AudioSource component = GetComponent<AudioSource>();
		if (component != null)
		{
			component.Play();
		}
		GetLevelToLoad();
		StartCoroutine(loadLevel());
	}

	private void GetLevelToLoad()
	{
		switch (StatMaster.GetCurrentIsland())
		{
		case Island.Ipsilon:
			levelToLoad = "LevelSelect";
			break;
		case Island.Tolbrynd:
			levelToLoad = "LevelSelect2";
			break;
		case Island.Valfross:
			levelToLoad = "LevelSelect3";
			break;
		case Island.Krolmar:
			levelToLoad = "LevelSelect4";
			break;
		case Island.Water:
		case Island.WaterSandbox:
			levelToLoad = "LevelSelectWater";
			break;
		default:
			levelToLoad = "TITLE SCREEN";
			break;
		}
	}

	private IEnumerator loadLevel()
	{
		if (FadeCodey != null)
		{
			yield return FadeCodey.FadeIn();
		}
		SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
	}
}
