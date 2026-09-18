using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleSceneLoader : MonoBehaviour
	{
		[Header("References")]
		public Animator animator;

		[Header("Parameters")]
		public bool showCursor;

		[SerializeField]
		private List<string> _sceneNames;

		private void OnEnable()
		{
			if ((bool)animator)
			{
				animator.gameObject.SetActive(value: true);
				animator.SetBool("Active", value: false);
			}
			if (showCursor)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		public void QuitApplication()
		{
			Application.Quit();
		}

		public void NextScene()
		{
			string item = SceneManager.GetActiveScene().name;
			SwitchScene(_sceneNames[(_sceneNames.IndexOf(item) + 1) % _sceneNames.Count]);
		}

		public void PreviousScene()
		{
			string item = SceneManager.GetActiveScene().name;
			SwitchScene(_sceneNames[(_sceneNames.IndexOf(item) - 1 + _sceneNames.Count) % _sceneNames.Count]);
		}

		public void SwitchScene(string sceneName)
		{
			StartCoroutine(C_SwitchScene(sceneName));
		}

		private IEnumerator C_SwitchScene(string sceneName)
		{
			if ((bool)animator)
			{
				animator.gameObject.SetActive(value: true);
				animator.SetBool("Active", value: true);
				yield return new WaitForSeconds(0.5f);
			}
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
			while (!asyncLoad.isDone)
			{
				yield return null;
			}
		}
	}
}
