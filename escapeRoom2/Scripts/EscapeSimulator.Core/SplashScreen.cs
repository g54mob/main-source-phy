using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
	private enum SplashState
	{
		InitialWait = 0,
		FadeIn = 1,
		FadeWait = 2,
		LogoFadeOut = 3,
		SceneLoading = 4,
		WaitForSceneInit = 5,
		ActiveSceneWarmUp = 6,
		FadeOut = 7,
		GameReady = 8,
		GameStarted = 9
	}

	public static bool finished = true;

	public SplashUI ui;

	private SplashState splashState;

	private AsyncOperation sceneLoad;

	private AsyncOperation sceneUnload;

	private float timerShowSpinner;

	private float stateTimer;

	private void Awake()
	{
		PineTesting.init();
		finished = false;
		ui.Fader_Logos.alpha = 0f;
		ui.Fader.alpha = 1f;
		Cursor.visible = false;
		PineTesting.mark("awake");
	}

	private void changeState(SplashState state)
	{
		stateTimer = 0f;
		splashState = state;
	}

	private void Update()
	{
		timerShowSpinner += Time.deltaTime;
		if (timerShowSpinner > 2.5f)
		{
			ui.Fader_SpinnerParent.gameObject.SetActive(value: true);
		}
		if (splashState == SplashState.InitialWait)
		{
			stateTimer += Time.deltaTime;
			if (stateTimer >= 1f)
			{
				changeState(SplashState.FadeIn);
			}
		}
		else if (splashState == SplashState.FadeIn)
		{
			stateTimer += Time.deltaTime;
			ui.Fader_Logos.alpha = Mathf.Clamp01(stateTimer);
			if (stateTimer >= 1f)
			{
				AssetBundleLoader.loadAssetBundle(AssetBundleType.Dependencies);
				AssetBundleLoader.startLoadScene("Lobby1");
				changeState(SplashState.FadeWait);
			}
		}
		else if (splashState == SplashState.FadeWait)
		{
			if (AssetBundleLoader.processLoadingScene("Lobby1"))
			{
				Debug.Log("load scene if asset bundle is done");
				Debug.Log(RuntimeManager.CoreSystem);
				Debug.Log("SteamManager.Initialized in SplashScreen: " + SteamManager.Initialized);
				PlayerSave.init(Menu.getPreferredLanguage());
				PlayerSave.setAllVolumes();
				sceneLoad = SceneManager.LoadSceneAsync("Lobby1", LoadSceneMode.Additive);
				sceneLoad.allowSceneActivation = true;
				PineTesting.mark("LoadSceneAsync");
				changeState(SplashState.SceneLoading);
			}
		}
		else if (splashState == SplashState.SceneLoading)
		{
			if (sceneLoad.progress >= 0.9f)
			{
				sceneLoad.allowSceneActivation = true;
				PineTesting.mark("allowSceneActivation");
				changeState(SplashState.WaitForSceneInit);
			}
		}
		else if (splashState == SplashState.WaitForSceneInit)
		{
			if (sceneLoad.isDone)
			{
				Scene sceneByName = SceneManager.GetSceneByName("Lobby1");
				SceneManager.SetActiveScene(sceneByName);
				SceneManager.MoveGameObjectToScene(base.gameObject, sceneByName);
				PineTesting.mark("SetActiveScene");
				changeState(SplashState.ActiveSceneWarmUp);
			}
		}
		else if (splashState == SplashState.ActiveSceneWarmUp)
		{
			if (Menu.framesWithGame > 5)
			{
				changeState(SplashState.LogoFadeOut);
			}
		}
		else if (splashState == SplashState.LogoFadeOut)
		{
			stateTimer += Time.deltaTime;
			ui.Fader_Logos.alpha = Mathf.Clamp01(1f - stateTimer);
			if (stateTimer >= 1f)
			{
				changeState(SplashState.FadeOut);
			}
		}
		else if (splashState == SplashState.FadeOut)
		{
			stateTimer += Time.deltaTime;
			if (stateTimer >= 0.5f)
			{
				sceneUnload = SceneManager.UnloadSceneAsync("SplashScreen");
				changeState(SplashState.GameReady);
			}
		}
		else if (splashState == SplashState.GameReady && sceneUnload.isDone)
		{
			Object.Destroy(base.gameObject);
			changeState(SplashState.GameStarted);
			finished = true;
		}
	}
}
