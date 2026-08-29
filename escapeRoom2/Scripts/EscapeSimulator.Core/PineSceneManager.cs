using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PineSceneManager : MonoBehaviour
{
	private static string scenePath;

	private static LoadingCanvas.EndCondition endCondition;

	private AsyncOperation sceneLoading;

	[Header("VR")]
	public Canvas vrLoadingCanvasTemplate;

	private bool vrLoadingCanvasSetup;

	private bool vrAllowSceneActivationCoroutineInvoked;

	private float vrMinimalEmptySceneStayTimer = 3f;

	private void Start()
	{
		Debug.Log("PineSceneManager loading scene: " + scenePath);
		if (isVR())
		{
			VR instance = VR.instance;
			Canvas component = LoadingCanvas.get().gameObject.GetComponent<Canvas>();
			instance.setupCanvas(component);
			instance.mapCanvas(component, instance.rig.mainMenuCanvas);
			component.transform.rotation = Quaternion.LookRotation(component.transform.position - instance.rig.headCamera.transform.position, Vector3.up);
			instance.setRigLayer("Default");
			instance.rig.leftController.raycastLineRenderer.enabled = false;
			instance.rig.rightController.raycastLineRenderer.enabled = false;
			instance.setControllerIndicatorState(1, isShown: false);
			instance.setControllerIndicatorState(2, isShown: false);
			instance.rig.overlay.color = new Color(0f, 0f, 0f, 0f);
			instance.rig.leftController.watch.gameObject.SetActive(value: false);
			instance.rig.rightController.watch.gameObject.SetActive(value: false);
			instance.setBlackScreenScene(new List<Canvas> { component });
			VRRaycaster.isOcclusionEnabled = true;
			VRRaycaster.focusedCanvases = null;
		}
		if (scenePath == "Lobby1")
		{
			CustomLevelLoader.data = new CustomLevelLoaderData();
		}
		Resources.UnloadUnusedAssets();
		GC.Collect();
		AssetBundleLoader.startLoadScene(scenePath);
	}

	private void Update()
	{
		if (AssetBundleLoader.processLoadingScene(scenePath))
		{
			sceneLoading = SceneManager.LoadSceneAsync(scenePath);
			sceneLoading.allowSceneActivation = false;
		}
		if (isVR())
		{
			vrMinimalEmptySceneStayTimer -= Time.deltaTime;
			VR.instance.update();
		}
		if (sceneLoading == null || !(sceneLoading.progress >= 0.9f))
		{
			return;
		}
		if (!isVR())
		{
			sceneLoading.allowSceneActivation = true;
		}
		else
		{
			if (VR.instance.isFadingCanvas || vrAllowSceneActivationCoroutineInvoked || vrMinimalEmptySceneStayTimer > 0f)
			{
				return;
			}
			if (scenePath == "Lobby1")
			{
				VR.debugLog("Loading VR main menu scene...");
				VR.instance.fadeInAndOut(0.5f, delegate
				{
					sceneLoading.allowSceneActivation = true;
				}, () => sceneLoading.isDone);
			}
			else
			{
				VR.debugLog("Loading VR level scene...");
				sceneLoading.allowSceneActivation = true;
			}
			LoadingCanvas.get().vrForceShowCanvas = scenePath != "Lobby1";
			vrAllowSceneActivationCoroutineInvoked = true;
		}
	}

	private void OnDestroy()
	{
		endCondition.done = true;
	}

	public static void loadScene(string scenePath, string sceneName, string hint, Texture2D loadingImage)
	{
		if (isVR())
		{
			VR.instance.fadeInAndOut(0.5f, changeScene);
		}
		else
		{
			changeScene();
		}
		void changeScene()
		{
			Debug.Log(scenePath);
			Debug.Log(SceneManager.GetActiveScene().name);
			LoadingCanvas.get().clearAllConditions();
			endCondition = LoadingCanvas.get().enqueueEndCondition(hint, sceneName);
			if (loadingImage != null)
			{
				LoadingCanvas.get().changeImage(loadingImage);
			}
			if (scenePath == SceneManager.GetActiveScene().name)
			{
				SceneManager.LoadSceneAsync(scenePath).completed += delegate
				{
					endCondition.done = true;
				};
			}
			else
			{
				SceneManager.LoadScene("_Scenes/Empty");
				PineSceneManager.scenePath = scenePath;
			}
			if (isVR())
			{
				VR.instance.clearRig();
				Canvas component = LoadingCanvas.get().gameObject.GetComponent<Canvas>();
				component.gameObject.layer = LayerMask.NameToLayer("Default");
				component.enabled = true;
			}
		}
	}

	private static bool isVR()
	{
		if (VR.instance != null)
		{
			return VR.instance.isActive();
		}
		return false;
	}
}
