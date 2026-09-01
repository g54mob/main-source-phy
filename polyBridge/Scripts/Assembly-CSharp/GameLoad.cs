using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoad
{
	private static AsyncOperation m_LoadSceneAsyncOperation;

	public static void LoadScene(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public static void LoadSceneAdditive(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Additive);
	}

	public static void LoadSceneAsync(string name)
	{
		if (LoadSceneAsyncInProgress())
		{
			Debug.LogWarningFormat("Tried to load {0} but load already in progress");
		}
		else
		{
			m_LoadSceneAsyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
		}
	}

	public static void LoadSceneAdditiveAsync(string name)
	{
		SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
	}

	public static void UnloadSceneAsync(string name)
	{
		SceneManager.UnloadSceneAsync(name);
	}

	public static bool LoadSceneAsyncInProgress()
	{
		if (m_LoadSceneAsyncOperation != null)
		{
			return !m_LoadSceneAsyncOperation.isDone;
		}
		return false;
	}
}
