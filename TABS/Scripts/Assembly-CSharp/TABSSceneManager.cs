using System;
using System.Linq;
using DM;
using Landfall.TABS;
using Landfall.TABS.GameState;
using Landfall.TABS.Workshop;
using LevelCreator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TABSSceneManager
{
	private const string GAME_SCENE = "GameScene";

	private const string MAIN_MENU = "MainMenu";

	private const string UNIT_CREATOR = "UnitCreator_GamepadUI";

	private const string CUSTOM_CONTENT_PAGE = "CustomContentPage";

	private const string WORKSHOP_SCENE = "WorkshopScene";

	private const string CREDITS_SCENE = "Credits";

	private const string LEVEL_CREATOR = "Editor Scene";

	private const string LEVEL_SCENE = "LevelScene";

	private static bool m_LoadedSameLevel;

	private static bool m_Inited;

	private static string m_ExpectedScene;

	private static bool m_IsLoading;

	private static MapAsset m_currentLoadedMap;

	public static UnitBlueprint CurrentlyLoadingUnit;

	public static MapAsset CurrentLoadedMap => m_currentLoadedMap;

	public static bool IsLoading => m_IsLoading;

	private static void Init()
	{
		if (!m_Inited)
		{
			m_Inited = true;
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	}

	private static MapAsset GetCurrentMapAsset()
	{
		string name = SceneManager.GetActiveScene().name;
		MapAsset[] array = ContentDatabase.Instance().GetAllMapAssetsOrdered().ToArray();
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			MapAsset mapAsset = array[i];
			if (mapAsset.MapName == name)
			{
				return mapAsset;
			}
		}
		return null;
	}

	private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("Done loading a scene: " + scene.name + " Mode: " + mode);
		if (scene.name == m_ExpectedScene)
		{
			m_currentLoadedMap = GetCurrentMapAsset();
			ServiceLocator.GetService<LoadingScreenHandler>().HideLoadingScreen(null);
		}
		else if (m_LoadedSameLevel && scene.name == "GameScene")
		{
			m_LoadedSameLevel = false;
			SceneSettings.Instance.ResetToDefaultTeamLine();
			SceneSettings.Instance.LoadDefaultWinConditions();
			ServiceLocator.GetService<LoadingScreenHandler>().HideLoadingScreen(null);
			Debug.Log("OnSceneLoaded: " + scene.name + " Expected: " + m_ExpectedScene);
		}
		if (scene.name == "GameScene")
		{
			ServiceLocator.GetService<GameStateManager>().EnterNewScene();
			ServiceLocator.GetService<GameStateManager>().EnterPlacementState();
		}
		m_IsLoading = false;
	}

	public static bool IsInMainMenuScene()
	{
		return IsSceneLoaded("MainMenu");
	}

	public static bool IsInGameScene()
	{
		return IsSceneLoaded("GameScene");
	}

	public static bool IsInUnitCreatorScene()
	{
		return IsSceneLoaded("UnitCreator_GamepadUI");
	}

	public static void LoadMainMenu(bool forceInstantLoad)
	{
		CampaignPlayerDataHolder.BackToMenu();
		CampaignHandler.ResetLoadedLevel();
		LoadScene("MainMenu", forceInstantLoad);
	}

	public static void LoadMainMenu()
	{
		LoadMainMenu(forceInstantLoad: false);
	}

	public static void LoadMainMenuForUpload(object data)
	{
		DMWorkshopHandler.uploadData = data;
		LoadMainMenu(forceInstantLoad: true);
	}

	public static void LoadUnitCreator(UnitBlueprint unit)
	{
		CurrentlyLoadingUnit = unit;
		LoadScene("UnitCreator_GamepadUI");
	}

	public static void LoadUnitCreator()
	{
		CurrentlyLoadingUnit = null;
		LoadScene("UnitCreator_GamepadUI");
	}

	public static bool IsInCustomContentPage()
	{
		return SceneManager.GetActiveScene().name == "CustomContentPage";
	}

	public static void LoadCustomContentPage(bool instantLoad = false)
	{
		LoadScene("CustomContentPage", instantLoad);
	}

	public static void LoadCustomContentPageAdditive()
	{
		SceneManager.LoadScene("CustomContentPage", LoadSceneMode.Additive);
	}

	public static void UnLoadCustomContentPage()
	{
		SceneManager.UnloadSceneAsync("CustomContentPage");
	}

	public static void LoadCampaign()
	{
		TABSCampaignLevelAsset currentLevel = CampaignPlayerDataHolder.GetCurrentLevel();
		if (currentLevel.CustomMap != default(DatabaseID))
		{
			CustomMap userMap = ContentDatabase.Instance().GetUserMap(currentLevel.CustomMap);
			if (!(userMap != null))
			{
				ModalPanel service = ServiceLocator.GetService<ModalPanel>();
				string singlePhrase = Localizer.GetSinglePhrase("POPUP_LOADERROR");
				service.PopUp(string.Format(singlePhrase, currentLevel.Entity.Name));
				return;
			}
			SpawnLevel.SetCustomMapToLoad(userMap);
		}
		LoadMap(currentLevel.MapAsset);
	}

	public static void LoadWorkshopScene()
	{
		LoadScene("WorkshopScene");
	}

	public static void LoadCreditsScene()
	{
		LoadScene("Credits");
	}

	public static void LoadLevelCreator(DMEditor.StartState startState, string levelToLoadOnStart = "")
	{
		DMEditor.SetStartState(startState);
		DMEditor.SetLevelToLoadOnStart(levelToLoadOnStart);
		LoadScene("Editor Scene");
	}

	public static void LoadScene(string sceneName, bool forceInstantLoad = false)
	{
		Init();
		System.Action action = delegate
		{
			m_ExpectedScene = sceneName;
			ServiceLocator.GetService<GameStateManager>().EnterNoneState();
			SceneManager.LoadScene(sceneName);
			Debug.Log("Done loading normal scene");
		};
		if (forceInstantLoad)
		{
			action();
		}
		else
		{
			ServiceLocator.GetService<LoadingScreenHandler>().ShowLoadingScreen(action);
		}
	}

	public static void LoadMap(MapAsset map, bool invokeExistingDismissActionOfPopupPanel = true)
	{
		Init();
		if (m_IsLoading)
		{
			Debug.LogError("Trying to load map while loading, illegal...");
			return;
		}
		m_IsLoading = true;
		if (map == null)
		{
			Debug.LogError("Trying to load a null MapAsset. This will cause crashes in release builds of certain platforms.");
		}
		string mapName = map.MapName;
		if (m_ExpectedScene == mapName)
		{
			m_LoadedSameLevel = true;
		}
		m_ExpectedScene = mapName;
		ModalPanel service = ServiceLocator.GetService<ModalPanel>();
		if (service.IsPopupOpen)
		{
			service.CloseWaitPopup();
		}
		Debug.Log("Loading Map: " + mapName + " ExpectedScene: " + m_ExpectedScene);
		System.Action onFinished = delegate
		{
			if (mapName == GetCurrentMap() && mapName != "LevelScene")
			{
				ServiceLocator.GetService<GameStateManager>().EnterNoneState();
				Debug.Log("Tried to load same scene twice, wont let you!");
				CoroutineAsyncLoader coroutineAsyncLoader = new GameObject("GameSceneAsyncLoader").FetchComponent<CoroutineAsyncLoader>();
				AsyncOperation op = SceneManager.UnloadSceneAsync("GameScene");
				coroutineAsyncLoader.DoCoroutine(op, delegate
				{
					SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
					Debug.Log("Done loading game scene when map is already the same");
				});
				Debug.Log("Done settings up load game scene thing");
			}
			else
			{
				ServiceLocator.GetService<GameStateManager>().EnterNoneState();
				LoadMapAndGameScenes(mapName);
			}
		};
		ServiceLocator.GetService<LoadingScreenHandler>().ShowLoadingScreen(onFinished);
	}

	public static string GetCurrentMap()
	{
		return SceneManager.GetActiveScene().name;
	}

	public static void ReloadMap()
	{
		LoadMap(CurrentLoadedMap);
	}

	private static void LoadMapAndGameScenes(string mapName)
	{
		SceneManager.LoadScene(mapName);
		SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
	}

	private static bool IsSceneLoaded(string sceneName)
	{
		int i = 0;
		for (int sceneCount = SceneManager.sceneCount; i < sceneCount; i++)
		{
			Scene sceneAt = SceneManager.GetSceneAt(i);
			if (sceneAt.isLoaded && sceneAt.name == sceneName)
			{
				return true;
			}
		}
		return false;
	}
}
