using System.Collections.Generic;
using DM;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoadScene : MonoBehaviour
{
	private string m_loadedScene;

	private void Awake()
	{
		List<string> mainMenuScenes = ContentDatabase.Instance().GetMainMenuScenes();
		m_loadedScene = mainMenuScenes[Random.Range(0, mainMenuScenes.Count)];
		SceneManager.LoadScene(m_loadedScene, LoadSceneMode.Additive);
	}

	private void Start()
	{
		if (m_loadedScene != null)
		{
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(m_loadedScene));
		}
	}
}
