using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsyncCode : MonoBehaviour
{
	public FadeScreen fadeCode;

	public string levelToLoad;

	private IEnumerator Start()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Single);
		async.allowSceneActivation = false;
		yield return async;
		yield return fadeCode.FadeIn();
	}
}
