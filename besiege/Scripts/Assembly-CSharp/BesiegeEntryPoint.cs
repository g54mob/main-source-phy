using System;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Threading;
using BesiegeDlc;
using Localisation;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BesiegeEntryPoint : MonoBehaviour
{
	public const string ForceArgumentsPrefName = "ForceArguments";

	public const string CMD_CONNECT = "connect";

	public const string CMD_CONNECT_SERVER = "connect_server";

	public const string CMD_CONNECT_LOBBY = "connect_lobby";

	public const string CMD_TEST_MACHINE = "test_machine";

	public const string CMD_LOAD_SP_LEVEL = "load_level";

	public const string CMD_LOAD_SCENE = "load_scene";

	public const string ARG_PASSWORD = "password";

	public const string ARG_DEDICATED = "dedicated";

	public const string ARG_DEBUG = "debug";

	public const string ARG_TEST_DURATION = "test_duration";

	public const string ARG_TEST_HEADLESS = "test_headless";

	public const string ARG_TEST_NUM_MACHINES = "test_num_machines";

	public const string ARG_ENABLE_JOINT_PARENTING = "enable_joint_parenting";

	public const string ARG_UNLOCK_SPEED = "unlock_speed";

	public const string ARG_MAX_FPS = "max_fps";

	public const string ARG_DEVELOPER = "developer";

	public const string CMD_PF_CONNECT = "pf_connect";

	public const string CMD_PF_JOIN = "pf_join";

	public const string ARG_MODPATHS = "mod";

	public const string ARG_LEVELEDITOR_ONLY = "leveleditor_only";

	public const string ARG_ENVIRONMENT = "level_environment";

	public const string ARG_MP_LEVEL = "mp_level";

	public static Action<ulong, string> onConnectToLobby;

	public static Action<string, int, string> onConnectToIpServer;

	public static Action<string> OnConnectToPlayfabServer;

	public static Action<ulong, string> onConnectToServer;

	public static Action<DedicatedServerMode> onStartServer;

	public static Action<string, float, bool, int> onTestMachine;

	private string forceArguments = string.Empty;

	[SerializeField]
	private string multiplayerScene;

	[SerializeField]
	private string defaultSceneToLoad;

	public TextMesh loadingMessage;

	public Arguments Args;

	private static BesiegeEntryPoint instance;

	private AudioListener tempAudioListener;

	private FadeScreen currentFadeScreen;

	public static bool transitioning;

	public static bool loadingLevel;

	public static void CreateEntryPoint(Arguments args)
	{
		if (instance == null)
		{
			Debug.LogError("Got no EntryPoint instance, this shouldn't happen.");
		}
		else if (!transitioning)
		{
			instance.Args = args;
			instance.InitEntryPoint();
		}
	}

	private void Awake()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Initialize();
	}

	private void Initialize()
	{
		string[] array = ((!Application.isEditor || string.IsNullOrEmpty(forceArguments)) ? Environment.GetCommandLineArgs() : forceArguments.Split(' '));
		Args = new Arguments(array);
		tempAudioListener = base.gameObject.AddComponent<AudioListener>();
		SetLogLevels();
		SetThreadCultureToInvariant();
		Debug.LogFormat("[Besiege] version: {0}, using startup arguments: ", VersionNumber.GetVersionString(), string.Join(", ", array));
	}

	private void SetThreadCultureToInvariant()
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
	}

	private void SetLogLevels()
	{
		if (Args.Exists("developer"))
		{
			StatMaster statMaster = SingleInstance<StatMaster>.Instance;
			bool flag = (SingleInstance<StatMaster>.Instance.OverrideIsDebug = true);
			SingleInstance<StatMaster>.Instance.isDebug = flag;
			statMaster.isDeveloper = flag;
			StatMaster.SetLogFilter(0);
		}
		else if (Args.Exists("debug"))
		{
			SingleInstance<StatMaster>.Instance.isDebug = (SingleInstance<StatMaster>.Instance.OverrideIsDebug = true);
			StatMaster.SetLogFilter(1);
		}
		else
		{
			StatMaster.SetLogFilter(1);
		}
	}

	private void Start()
	{
		InitEntryPoint();
	}

	private void CheckSceneLoadPermission(Action onSuccess)
	{
		onSuccess();
	}

	public void InitEntryPoint()
	{
		if (SceneManager.sceneCount > 1)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (ProvidedInvalidArguments())
		{
			Debug.LogError("Invalid entry point arguments detected... Command line: " + string.Join(" ", Environment.GetCommandLineArgs()));
			Application.Quit();
		}
		CheckSceneLoadPermission(delegate
		{
			StatMaster.isLoadingLevels = true;
			StartCoroutine(InitEntryPointIE());
		});
	}

	private IEnumerator InitEntryPointIE()
	{
		transitioning = true;
		string sceneName = GetSceneToLoad();
		bool hasLoadingText = loadingMessage != null;
		NetworkAnalyser networkAnalyser = SingleInstanceFindOnly<NetworkAnalyser>.Instance;
		float maxTimeToLoad = 20f;
		float t = 0f;
		if (hasLoadingText)
		{
			bool error = false;
			loadingMessage.text = "Initialising (1/4)...";
			while (!networkAnalyser.DoneTesting)
			{
				UpdateLoadingText();
				if (t > maxTimeToLoad)
				{
					Debug.LogError("[Entry Point]: took too long to test network connection");
					error = true;
					break;
				}
				t += Time.unscaledDeltaTime;
				yield return null;
			}
			if (error)
			{
				Debug.LogError("[Entry Point]: (2/4) NetworkAnalyzer took too long");
				loadingMessage.text = "Initialising (2/4): Took too long to test network connection";
			}
			else
			{
				loadingMessage.text = "Initialising (2/4)...";
			}
			error = false;
			t = 0f;
			while (!SaveCompletedLevels.loadedProgress)
			{
				if (t > maxTimeToLoad)
				{
					Debug.LogError("[Entry Point]: took too long to load level progress");
					error = true;
					break;
				}
				t += Time.unscaledDeltaTime;
				yield return null;
			}
			if (error)
			{
				Debug.LogError("[Entry Point]: (3/4) Took too long to load level progress");
				loadingMessage.text = "Initialising (3/4): Took too long to load level progress";
			}
			else
			{
				loadingMessage.text = "Initialising (3/4)...";
			}
			UpdateLoadingText();
			if (!networkAnalyser.UsingAlternativeConnection)
			{
				yield return new WaitForSecondsRealtime(1f);
			}
			loadingMessage.text = "Initialising (4/4)...";
		}
		if (SceneManager.GetActiveScene().name != sceneName)
		{
			currentFadeScreen = UnityEngine.Object.FindObjectOfType<FadeScreen>();
			if (currentFadeScreen != null)
			{
				yield return currentFadeScreen.FadeIn();
			}
			loadingLevel = true;
			SceneManager.sceneLoaded += OnSceneLoaded;
			if (SceneManager.sceneCountInBuildSettings == 2)
			{
				yield return SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
			}
			else
			{
				if (!DlcManager.Instance.IsLevelAllowed(sceneName))
				{
					sceneName = "TITLE SCREEN";
				}
				yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
			}
			loadingLevel = false;
			StartCoroutine(IEOnLoadComplete());
		}
		else
		{
			StartCoroutine(FinalizeEntryPoint());
		}
	}

	private IEnumerator IEOnLoadComplete()
	{
		yield return null;
		int count = SceneManager.sceneCount;
		string sceneName = SceneManager.GetSceneAt(count - 1).name;
		int s;
		if (IsSPLevel(sceneName, out s) && ReferenceMaster.onLevelLoadComplete != null)
		{
			ReferenceMaster.onLevelLoadComplete(s);
		}
		if (ReferenceMaster.onSceneLoaded != null)
		{
			ReferenceMaster.onSceneLoaded();
		}
		transitioning = false;
	}

	public static bool IsSPLevel(string sceneName)
	{
		int i;
		return IsSPLevel(sceneName, out i);
	}

	public static bool IsSPLevel(string sceneName, out int i)
	{
		switch (sceneName)
		{
		case "SANDBOX":
		case "MISTY MOUNTAIN":
		case "BARREN EXPANSE":
		case "WATER SANDBOX":
		case "LEGACY SANDBOX":
			i = -1;
			return true;
		default:
		{
			if (sceneName.Contains("ZZ"))
			{
				i = -2;
				return true;
			}
			int startIndex = Mathf.Max(sceneName.Length - 2, 0);
			sceneName = sceneName.Substring(startIndex);
			return int.TryParse(sceneName, out i);
		}
		}
	}

	public static void StartServer(DedicatedServerMode mode)
	{
		instance.StartCoroutine(IEStartServer(mode));
	}

	private static IEnumerator IEStartServer(DedicatedServerMode mode)
	{
		loadingLevel = true;
		yield return SceneManager.LoadSceneAsync(instance.multiplayerScene, LoadSceneMode.Single);
		loadingLevel = false;
		yield return null;
		NetworkScene.Instance.StartServer(mode);
	}

	protected void UpdateLoadingText()
	{
		string empty = string.Empty;
		NetworkAnalyser networkAnalyser = SingleInstanceFindOnly<NetworkAnalyser>.Instance;
		if (!(loadingMessage == null))
		{
			if (networkAnalyser.UsingAlternativeConnection)
			{
				loadingMessage.text = LocalisationManager.GetTranslation(3203);
				loadingMessage.transform.position = new Vector3(-0.66f, loadingMessage.transform.position.y, loadingMessage.transform.position.z);
				return;
			}
			empty = ((!networkAnalyser.NATConnectionTester.DoneTesting) ? (empty + "NAT Connection: Analyzing... , ") : (empty + "NAT Connection: \u00a0\u00a0✓\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0, "));
			empty = ((!networkAnalyser.FacilitatorController.DoneTesting) ? (empty + "Facilitator: Analyzing... , ") : (empty + "Facilitator: \u00a0\u00a0✓\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0, "));
			empty = ((!networkAnalyser.RegionController.DoneTesting) ? (empty + "Region: Analyzing...") : (empty + "Region: \u00a0\u00a0✓"));
			loadingMessage.text = empty;
		}
	}

	private bool ProvidedInvalidArguments()
	{
		if (Args.Exists("connect_lobby") && BesiegeArgumentsHelper.ParseLobbyId(Args.Single("connect_lobby")) == 0L)
		{
			Debug.Log("Invalid argument: Incorrect Lobby id: " + Args.Single("connect_lobby"));
			return true;
		}
		if (Args.Exists("pf_connect") && string.IsNullOrEmpty(Args.Single("pf_connect")))
		{
			Debug.Log("Invalid argument: Incorrect connection string: " + Args.Single("pf_connect"));
			return true;
		}
		if (Args.Exists("pf_join") && string.IsNullOrEmpty(Args.Single("pf_join")))
		{
			Debug.Log("Invalid argument: Incorrect connection string: " + Args.Single("pf_join"));
			return true;
		}
		if (Args.Exists("connect") && BesiegeArgumentsHelper.ParseIPPort(Args.Single("connect")) == null)
		{
			Debug.Log("Invalid argument: Incorrect connection string: " + Args.Single("connect"));
			return true;
		}
		if (Args.Exists("test_machine") && BesiegeArgumentsHelper.ParseMachinePath(Args.Single("test_machine")) == null)
		{
			Debug.Log("Invalid argument: Could not parse machine path: " + Args.Single("test_machine"));
			return true;
		}
		if (Args.Exists("connect_server") && BesiegeArgumentsHelper.ParseServerId(Args.Single("connect_server")) == 0L)
		{
			Debug.Log("Invalid argument: Incorrect connection string: " + Args.Single("connect_server"));
			return true;
		}
		if (Args.Exists("test_duration") && object.Equals(BesiegeArgumentsHelper.ParseFloat(Args.Single("test_duration")), float.MaxValue))
		{
			Debug.Log("Invalid argument: Test duration could not be parsed: " + Args.Single("test_duration"));
			return true;
		}
		return false;
	}

	private string GetSceneToLoad()
	{
		if (Args.Exists("connect_lobby") || Args.Exists("connect") || Args.Exists("connect_server") || Args.Exists("pf_connect") || Args.Exists("dedicated") || Args.Exists("test_machine"))
		{
			return multiplayerScene;
		}
		if (Args.Exists("load_level"))
		{
			string text = BesiegeArgumentsHelper.ParseSceneName(Args.Single("load_level"));
			if (text != null)
			{
				return text;
			}
			Debug.LogError("Level does not exist: " + Args.Single("load_level"));
		}
		else if (Args.Exists("load_scene"))
		{
			return BesiegeArgumentsHelper.ParseSceneName(Args.Single("load_scene"));
		}
		return defaultSceneToLoad;
	}

	private IEnumerator FinalizeEntryPoint()
	{
		for (int i = 0; i < 5; i++)
		{
			yield return null;
		}
		currentFadeScreen = null;
		try
		{
			InvokeEntryPointHandler();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			Debug.LogError("Could not invoke the entry point handler(s).");
			Debug.LogException(ex2);
			Application.Quit();
		}
		StatMaster.isLoadingLevels = false;
		transitioning = false;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode newScene)
	{
		UnityEngine.Object.DestroyImmediate(tempAudioListener);
		SceneManager.sceneLoaded -= OnSceneLoaded;
		StartCoroutine(FinalizeEntryPoint());
	}

	private void InvokeEntryPointHandler()
	{
		string text = string.Empty;
		if (Args.Exists("password"))
		{
			text = Args.Single("password");
		}
		if (Args.Exists("enable_joint_parenting"))
		{
			StatMaster.UseJointParenting = true;
		}
		if (Args.Exists("unlock_speed"))
		{
			StatMaster.UnlockSpeedSliders = true;
		}
		if (Args.Exists("leveleditor_only"))
		{
			StatMaster.IsLevelEditorOnly = true;
		}
		else
		{
			StatMaster.IsLevelEditorOnly = false;
		}
		if (Args.Exists("level_environment"))
		{
			int result = 0;
			int.TryParse(Args.Single("level_environment"), out result);
			StatMaster.DefaultLevelEnvironment = (LevelSettings.LevelEnvironment)result;
		}
		if (Args.Exists("mp_level"))
		{
			string text2 = Args.Single("mp_level");
			if (!string.IsNullOrEmpty(text2))
			{
				StatMaster.DefaultMPLevel = text2;
			}
		}
		if (Args.Exists("max_fps"))
		{
			decimal result2;
			if (decimal.TryParse(Args.Single("max_fps"), out result2))
			{
				OptionsMaster.BesiegeConfig.FPSLock = (FPSLock)(int)result2;
				CapFPS.SetTargetFrameRate(StatMaster.MaxFPS);
			}
			else
			{
				Debug.Log("Failed to parse MaxFPS to int: " + Args.Single("max_fps"));
			}
		}
		if (Debug.isDebugBuild && SingleInstance<StatMaster>.Instance.isDebug)
		{
			GameObject gameObject = (GameObject)Resources.Load("SRDebugger.Init");
			if (gameObject != null)
			{
				UnityEngine.Object.Instantiate(gameObject);
			}
			else
			{
				Debug.Log("SRDebugger.Init prefab not found");
			}
		}
		if (Args.Exists("connect_lobby"))
		{
			ulong arg = BesiegeArgumentsHelper.ParseLobbyId(Args.Single("connect_lobby"));
			onConnectToLobby(arg, text);
		}
		else if (Args.Exists("connect"))
		{
			IPEndPoint iPEndPoint = BesiegeArgumentsHelper.ParseIPPort(Args.Single("connect"));
			onConnectToIpServer(iPEndPoint.Address.ToString(), iPEndPoint.Port, text);
		}
		else if (Args.Exists("connect_server"))
		{
			ulong arg2 = BesiegeArgumentsHelper.ParseServerId(Args.Single("connect_server"));
			onConnectToServer(arg2, text);
		}
		else if (Args.Exists("pf_connect"))
		{
			OnConnectToPlayfabServer(Args.Single("pf_connect"));
		}
		else if (Args.Exists("pf_join"))
		{
			ulong result3;
			string pfNetworkId;
			if (ulong.TryParse(Args.Single("pf_join"), out result3) && (SingleInstance<WorkshopManager>.Instance as SteamWorkshopManager).GetPlayfabNetworkId((CSteamID)result3, out pfNetworkId))
			{
				OnConnectToPlayfabServer(pfNetworkId);
			}
		}
		else if (Args.Exists("dedicated"))
		{
			string text3 = Args.Single("dedicated");
			int result4 = 0;
			if (text3 != null)
			{
				int.TryParse(text3, out result4);
			}
			DedicatedServerMode obj = DedicatedServerMode.NonDedicatedLan;
			if (Enum.IsDefined(typeof(DedicatedServerMode), result4))
			{
				obj = (DedicatedServerMode)result4;
			}
			onStartServer(obj);
		}
		else if (Args.Exists("test_machine"))
		{
			string arg3 = BesiegeArgumentsHelper.ParseMachinePath(Args.Single("test_machine"));
			float arg4 = BesiegeArgumentsHelper.ParseFloat(Args.Single("test_duration"));
			bool arg5 = Args.Exists("test_headless");
			int result5 = 100;
			if (Args.Exists("test_num_machines"))
			{
				int.TryParse(Args.Single("test_num_machines"), out result5);
			}
			onTestMachine(arg3, arg4, arg5, result5);
		}
	}
}
