using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HDRPVolumePostProcessPack
{
	public class DemoStartup : MonoBehaviour
	{
		[SerializeField]
		private ScrollRect scrollRect;

		[SerializeField]
		private string HDRPDemoSceneName = "SampleScene";

		[SerializeField]
		private Camera uicamera;

		[SerializeField]
		private List<Transform> enableAfterSceneLoaded;

		private void Awake()
		{
			foreach (Transform item in enableAfterSceneLoaded)
			{
				item.gameObject.SetActive(value: false);
			}
		}

		private IEnumerator Start()
		{
			scrollRect.verticalNormalizedPosition = 1f;
			if (SceneManager.GetSceneByName(HDRPDemoSceneName).isLoaded)
			{
				yield return SceneManager.UnloadSceneAsync(HDRPDemoSceneName);
			}
		}

		public void onClickStart()
		{
			SceneManager.LoadScene(parameters: new LoadSceneParameters
			{
				loadSceneMode = LoadSceneMode.Additive
			}, sceneName: HDRPDemoSceneName);
			uicamera.gameObject.SetActive(value: false);
			base.gameObject.SetActive(value: false);
			foreach (Transform item in enableAfterSceneLoaded)
			{
				item.gameObject.SetActive(value: true);
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}
	}
}
