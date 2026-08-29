using System;
using System.Collections.Generic;
using System.IO;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

[ExecuteAlways]
public class GameStarter : MonoBehaviour
{
	public static bool isWaitingForSession;

	public static GameStarter splitScreenGame;

	private static string splitScreenHostUsername;

	private static string splitScreenClientUsername;

	public static string[] ignoredListNames = new string[13]
	{
		"persistentIdDatabase", "saveableGameObjects", "saveableComponents", "saveableEnabledComponents", "saveableTransforms", "saveableParticleSystems", "saveableVisualEffects", "saveableLineRenderers", "saveableTexts", "saveableTextsTMP",
		"saveableImages", "saveableSpriteRenderers", "saveableSprites"
	};

	[Header("Save-system")]
	[ReadOnly]
	public List<GameObject> persistentIdDatabase;

	[ReadOnly]
	public List<GameObject> saveableGameObjects;

	[ReadOnly]
	public List<Component> saveableComponents;

	[ReadOnly]
	public List<Component> saveableEnabledComponents;

	[ReadOnly]
	public List<Transform> saveableTransforms;

	[ReadOnly]
	public List<ParticleSystem> saveableParticleSystems;

	[ReadOnly]
	public List<VisualEffect> saveableVisualEffects;

	[ReadOnly]
	public List<LineRenderer> saveableLineRenderers;

	[ReadOnly]
	public List<Text> saveableTexts;

	[ReadOnly]
	public List<TMP_Text> saveableTextsTMP;

	[ReadOnly]
	public List<Image> saveableImages;

	[ReadOnly]
	public List<SpriteRenderer> saveableSpriteRenderers;

	[ReadOnly]
	public List<Sprite> saveableSprites;

	[Header("Gameplay")]
	public bool disableLevelLogic;

	public float overrideTimerInitialTime = -1f;

	[Range(0f, 16f)]
	public float examineModeBackgroundFadeMultiplier = 0.25f;

	[Range(0f, 16f)]
	public float examineModeLightStrength = 1f;

	[Range(-5f, 16f)]
	public float examineModeExposureLimitMin = -1f;

	[Range(-5f, 16f)]
	public float examineModeExposureLimitMax = 1f;

	[Range(-5f, 16f)]
	public float examineModeExposureCompensation = 1f;

	[Range(0f, 16f)]
	public float characterLightStrength = 1f;

	public Color itemUIOverlayColor = new Color(0f, 0f, 0f, 0f);

	[Range(0f, 16f)]
	public float itemUIExposure = 1f;

	[Range(0f, 10f)]
	public float itemUISaturation;

	[Header("Multiplayer")]
	public bool showVoiceChatIndicators = true;

	public bool useProximityVoiceChatInGame;

	[NonSerialized]
	public CustomLevelLoaderData customRoomData;

	[HideInInspector]
	public bool isSplitscreen;

	[HideInInspector]
	public bool loadLatestSave;

	[HideInInspector]
	public GameDifficulty gameDifficulty;

	[Header("Level")]
	public GameObject levelContainer;

	public Menu lobbyMenu;

	public Transform[] impostorCloneExplicitSync = Array.Empty<Transform>();

	[Header("Hints")]
	public Color hintTintColor = Color.grey;

	public List<Game.Puzzle> puzzles = new List<Game.Puzzle>();

	public Transform hintsParent;

	[Header("Roles")]
	public List<Game.Role> roles;

	[Header("VR")]
	public VR.Type vrType;

	public Color vrWallSphereColor = Color.white;

	public List<Collider> vrSpecificColliders = new List<Collider>();

	public List<Collider> vrCanvasColliderWhitelist = new List<Collider>();

	public List<Rigidbody> vrWallSphereRigidbodyWhitelist = new List<Rigidbody>();

	public Transform vrLevelCompletedCanvasPose;

	public Transform vrLowestNavMeshPoint;

	public float vrOculusRenderScale = 1.25f;

	public float vrOculusCullingDistance;

	public bool vrOculusColorGradingEnabled = true;

	public bool vrSparklesEnabled = true;

	public float vrReachDistance = -1f;

	[Header("Audio")]
	public EventReference soundStep;

	public EventReference soundPuzzleCompleted;

	public EventReference soundLevelCompleted;

	public EventReference soundLevelEntering;

	public StudioEventEmitter emitterAmbience;

	public StudioEventEmitter emitterMusic;

	[Header("Low Devices")]
	public List<GameObject> turnOffOnLowDevices = new List<GameObject>();

	public List<GameObject> turnOnOnLowDevices = new List<GameObject>();

	private AssetBundleAsyncRequest<GameObject> gamePrefabRequest;

	private AssetBundleAsyncRequest<GameObject> playerPrefabRequest;

	private GameObject editorForTesting;

	private CustomLevelLoaderData customLevelDataForTesting;

	public CanvasScaler[] levelUiCanvasScalers;

	private bool isCustomLevel
	{
		get
		{
			if (customRoomData == null)
			{
				return customLevelDataForTesting != null;
			}
			return true;
		}
	}

	private void OnEnable()
	{
		PineTesting.mark("awake");
		if (!Is.Editor)
		{
			isSplitscreen = false;
			loadLatestSave = false;
			disableLevelLogic = false;
		}
		if (Application.isPlaying)
		{
			if (!SteamManager.Initialized && Is.Steam)
			{
				Debug.LogError("SteamManager is not initialized.");
			}
			PlayerSave.init(Language.English);
			Localization.init(PlayerSave.getSettings().language);
			Controller.init();
			initSession();
			playerPrefabRequest = AssetBundleLoader.getAssetAsync<GameObject>(AssetBundleType.Characters, "Assets/_Misc/NetPlayer.prefab");
			gamePrefabRequest = AssetBundleLoader.getAssetAsync<GameObject>(AssetBundleType.GamePrefab, "Assets/_Misc/Game.prefab");
			RoomEditor.isUsingRoomEditor = false;
		}
	}

	public void startTestingFromRoomEditor(CustomLevelLoaderData customLevelData, GameObject container, GameObject editor)
	{
		customLevelDataForTesting = customLevelData;
		editorForTesting = editor;
		base.enabled = true;
		levelContainer = container;
	}

	private void initSession()
	{
		if (isSplitscreen)
		{
			initSplitScreenSession();
			return;
		}
		Net session = Net.getSession();
		if (session == null)
		{
			session = Net.createSession(initWithVoice: false);
			if (Is.Steam && !Is.perfTest())
			{
				session.startHost(LobbyType.SteamPine, ConnectionMode.SteamMessages);
			}
			else
			{
				session.startHost(LobbyType.Offline, ConnectionMode.Offline);
			}
			Net.setSession(session);
			isWaitingForSession = true;
		}
	}

	private void initSplitScreenSession()
	{
		Net net = Net.createSession(initWithVoice: false);
		net.gameDifficulty = gameDifficulty;
		net.connectionMode = ConnectionMode.Splitscreen;
		Net.setSession(net, base.gameObject);
		if (splitScreenHostUsername == null)
		{
			UnityEngine.Random.InitState(DateTime.Now.GetHashCode());
			splitScreenHostUsername = Net.botNames[UnityEngine.Random.Range(0, Net.botNames.Length)];
			splitScreenClientUsername = Net.botNames[UnityEngine.Random.Range(0, Net.botNames.Length)];
		}
		if (UnityUtils.isObjectInMainOrTopSplitScreenScene(base.gameObject))
		{
			net.netMode = NetMode.Host;
			net.localPlayerId = Net.SPLITSCREEN_HOST_ID;
			net.hostPlayerId = Net.SPLITSCREEN_HOST_ID;
		}
		else
		{
			net.netMode = NetMode.Client;
			net.localPlayerId = Net.SPLITSCREEN_CLIENT_ID;
			net.hostPlayerId = Net.SPLITSCREEN_HOST_ID;
		}
		net.players.Clear();
		net.players.Add(new NetPlayerData
		{
			id = Net.SPLITSCREEN_HOST_ID,
			username = splitScreenHostUsername
		});
		net.players.Add(new NetPlayerData
		{
			id = Net.SPLITSCREEN_CLIENT_ID,
			username = splitScreenClientUsername
		});
	}

	private void Update()
	{
		if (Application.isEditor)
		{
			PineTesting.recordFPS();
		}
		if (!Application.isPlaying || gamePrefabRequest == null || !gamePrefabRequest.isDone() || playerPrefabRequest == null || !playerPrefabRequest.isDone())
		{
			return;
		}
		Net session = Net.getSession(base.gameObject);
		if (session != null && session.players.Count != 0)
		{
			isWaitingForSession = false;
			Game game = UnityEngine.Object.Instantiate(gamePrefabRequest.getAsset().GetComponent<Game>());
			game.netPlayerMesh = playerPrefabRequest.getAsset();
			session.game = game;
			VR vR = VR.initInstance(UnityUtils.isObjectInMainOrTopSplitScreenScene(base.gameObject));
			vR.init(VR.Type.ForceNone);
			game.vr = vR;
			if (isCustomLevel && levelContainer.GetComponent<LevelContainerEditor>().baseSpotLight.TryGetComponent<Light>(out var component))
			{
				RenderSettings.sun = component;
			}
			gamePrefabRequest = null;
			playerPrefabRequest = null;
			base.enabled = false;
			initGame(game);
		}
	}

	private void initGame(Game game)
	{
		if (isSplitscreen)
		{
			initSplitScreenGame(game);
			Debug.Log("Initialized split-screen game instance <b>" + game.name + "</b>.", base.gameObject);
		}
		LevelLogic levelLogic = GetComponent<LevelLogic>();
		if (isCustomLevel)
		{
			if (levelLogic != null)
			{
				UnityEngine.Object.Destroy(levelLogic);
			}
			levelLogic = base.gameObject.AddComponent<LuaLevelLogic>();
			HashSet<string> invokableFunctions;
			string text = RoomEditor.setupAndGenerateLevelLogicLua(levelContainer, dry: false, out invokableFunctions);
			string path = Path.Combine(customRoomData?.path ?? customLevelDataForTesting?.path, "Code.room");
			if (File.Exists(path))
			{
				CodeData codeData = JsonUtility.FromJson<CodeData>(File.ReadAllText(path));
				Debug.Log("Lua script in custom level:\n" + codeData.script + "\n\nInvokable functions:\n" + string.Join("\n", codeData.invokeableFunctions));
				game.initLuaLevelLogicScript(codeData.script, new HashSet<string>(codeData.invokeableFunctions));
				if (text != codeData.script)
				{
					Debug.Log("UpToDate script is different from the one in the level. This is a valid situation, ES2 core api changed, but the script is still valid - using old api.");
				}
			}
			else
			{
				Debug.Log("SCRIPT NOT FOUND! THIS WILL POTENTIALLY DESYNC. Using default script.");
				game.initLuaLevelLogicScript(text, invokableFunctions);
			}
			Game.recalculateSaveableObjects(this);
		}
		else if (levelLogic == null)
		{
			Debug.LogError("Missing <b>LevelLogic</b> component on <b>GameStarter</b>. Adding default instance...");
			levelLogic = base.gameObject.AddComponent<LevelLogic>();
		}
		if (disableLevelLogic)
		{
			Debug.LogError("Destroying <b>" + levelLogic.GetType().Name + "</b>! Set <b>GameStarter.disableLevelLogic</b> to false to enable it.");
			UnityEngine.Object.Destroy(levelLogic);
			levelLogic = base.gameObject.AddComponent<LevelLogic>();
		}
		if (levelContainer == null)
		{
			Debug.LogError("<b>GameStarter.levelContainer</b> is not assigned!");
			levelContainer = GameObject.Find("LevelContainer");
		}
		game.levelLogic = levelLogic;
		game.levelContainer = levelContainer;
		game.isSplitscreen = isSplitscreen;
		game.loadLatestSave = loadLatestSave;
		game.disableLevelLogic = disableLevelLogic;
		game.overrideTimerInitialTime = overrideTimerInitialTime;
		game.showVoiceChatIndicators = showVoiceChatIndicators;
		game.useProximityVoiceChat = useProximityVoiceChatInGame;
		game.examineModeBackgroundFadeMultiplier = examineModeBackgroundFadeMultiplier;
		game.examineModeLightStrength = examineModeLightStrength;
		game.examineModeExposureLimitMin = examineModeExposureLimitMin;
		game.examineModeExposureLimitMax = examineModeExposureLimitMax;
		game.examineModeExposureCompensation = examineModeExposureCompensation;
		game.characterLightStrength = characterLightStrength;
		game.itemUIOverlayColor = itemUIOverlayColor;
		game.itemUIExposure = itemUIExposure;
		game.itemUISaturation = itemUISaturation;
		game.soundStepDefault = soundStep;
		game.emitterAmbience = emitterAmbience;
		game.emitterMusic = emitterMusic;
		game.soundLevelEntering = soundLevelEntering;
		game.soundLevelComplete = soundLevelCompleted;
		game.soundPuzzleComplete = soundPuzzleCompleted;
		game.impostorCloneExplicitSyncEntries = impostorCloneExplicitSync;
		game.customRoomData = customRoomData;
		game.lobbyMenu = lobbyMenu;
		game.hintTintColor = hintTintColor;
		game.puzzles = puzzles;
		game.roles = roles;
		game.turnOffOnLowDevices = turnOffOnLowDevices;
		game.turnOnOnLowDevices = turnOnOnLowDevices;
		game.persistentIdDatabase = persistentIdDatabase;
		game.saveableGameObjects = saveableGameObjects;
		game.saveableComponents = saveableComponents;
		game.saveableEnabledComponents = saveableEnabledComponents;
		game.saveableTransforms = saveableTransforms;
		game.saveableParticleSystems = saveableParticleSystems;
		game.saveableVisualEffects = saveableVisualEffects;
		game.saveableLineRenderers = saveableLineRenderers;
		game.saveableTexts = saveableTexts;
		game.saveableTextsTMP = saveableTextsTMP;
		game.saveableImages = saveableImages;
		game.saveableSpriteRenderers = saveableSpriteRenderers;
		game.saveableSprites = saveableSprites;
		game.vrWallSphereColor = vrWallSphereColor;
		game.vrSpecificColliders = vrSpecificColliders;
		game.vrCanvasColliderWhitelist = vrCanvasColliderWhitelist;
		game.vrWallSphereRigidbodyWhitelist = vrWallSphereRigidbodyWhitelist;
		game.vrLevelCompletedCanvasPose = vrLevelCompletedCanvasPose;
		game.vrLowestNavMeshPoint = vrLowestNavMeshPoint;
		game.vrOculusRenderScale = vrOculusRenderScale;
		game.vrOculusCullingDistance = vrOculusCullingDistance;
		game.vrOculusColorGradingEnabled = vrOculusColorGradingEnabled;
		game.vrSparklesEnabled = vrSparklesEnabled;
		game.vrReachDistance = ((vrReachDistance >= 0f) ? vrReachDistance : game.vrReachDistance);
		game.levelUiCanvasScalers = levelUiCanvasScalers;
		if (customLevelDataForTesting != null)
		{
			game.runtimeEditorGameObject = editorForTesting;
			game.isTestingPlaymodeInEditor = true;
			game.customRoomData = customLevelDataForTesting;
		}
		game.init();
	}

	private void initSplitScreenGame(Game game)
	{
		if (UnityUtils.isObjectInMainOrTopSplitScreenScene(base.gameObject))
		{
			game.changePlayerCameraRect(new Rect(0f, 0.5f, 1f, 0.5f));
			game.hasControl = true;
			game.name = "game_HOST";
			return;
		}
		splitScreenGame = this;
		game.changePlayerCameraRect(new Rect(0f, 0f, 1f, 0.5f));
		game.hasControl = false;
		game.name = "game_CLIENT";
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			Scene sceneAt = SceneManager.GetSceneAt(i);
			if (!(SceneManager.GetActiveScene() == sceneAt))
			{
				SceneManager.MoveGameObjectToScene(game.gameObject, sceneAt);
				break;
			}
		}
		levelContainer.transform.position = new Vector3(0f, 0f, 300f);
		game.transform.position = new Vector3(0f, 0f, 300f);
		UnityEngine.Object.Destroy(game.transform.GetComponentInChildren<EventSystem>().gameObject);
		Light[] componentsInChildren = levelContainer.GetComponentsInChildren<Light>(includeInactive: true);
		foreach (Light light in componentsInChildren)
		{
			if (light.type == LightType.Directional)
			{
				light.gameObject.SetActive(value: false);
			}
		}
		MaterialState[] componentsInChildren2 = levelContainer.GetComponentsInChildren<MaterialState>(includeInactive: true);
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			componentsInChildren2[j].relinkStates();
		}
	}
}
