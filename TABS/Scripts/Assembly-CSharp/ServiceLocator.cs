using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitCode.Debug;
using BitCode.Performance;
using Landfall.TABS.GameState;
using Landfall.TABS.Save;
using Landfall.TABS.Services;
using TFBGames;
using Unity.Entities;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
	private static bool _initialized = false;

	private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

	private static List<IService> _serviceList = new List<IService>();

	[SerializeField]
	private ServiceAsset[] m_serviceAssets;

	[SerializeField]
	private ServicePrefab[] m_servicePrefabs;

	[SerializeField]
	private GameObject[] m_globalPrefabs;

	[SerializeField]
	private GameObject[] m_platformPrefabs;

	[SerializeField]
	private GameObject[] m_earlyPlatformPrefabs;

	[SerializeField]
	private DebugConsoleUI m_debugConsole;

	private static ServiceLocator m_instance;

	public static void RegisterServiceAsSelf<T>(T service, Type serviceType) where T : IService
	{
		if (!_services.ContainsKey(serviceType))
		{
			_services.Add(serviceType, service);
			_serviceList.Add(service);
			service.OnRegister();
		}
	}

	public static void RegisterService<T>(T service) where T : IService
	{
		Type typeFromHandle = typeof(T);
		if (!_services.ContainsKey(typeFromHandle))
		{
			_services.Add(typeFromHandle, service);
			_serviceList.Add(service);
			service.OnRegister();
		}
	}

	public static void UnRegisterSerice<T>() where T : IService
	{
		Type typeFromHandle = typeof(T);
		if (_services.ContainsKey(typeFromHandle))
		{
			IService item = _services[typeFromHandle];
			_services[typeFromHandle].UnRegister();
			_services.Remove(typeFromHandle);
			_serviceList.Remove(item);
		}
	}

	public static T GetService<T>() where T : IService
	{
		if (_services.ContainsKey(typeof(T)))
		{
			return (T)_services[typeof(T)];
		}
		return default(T);
	}

	private async Task InitializeServices()
	{
		int i = 0;
		for (int num = m_earlyPlatformPrefabs.Length; i < num; i++)
		{
			GameObject gameObject = m_earlyPlatformPrefabs[i];
			if (!(gameObject == null))
			{
				UnityEngine.Object.Instantiate(gameObject, base.transform, worldPositionStays: false).name = gameObject.name;
			}
		}
		SettingsProfileManager settingsManager = new SettingsProfileManager();
		RegisterService(settingsManager);
		RegisterService(new GameStateManager());
		SharedServiceUpdater serviceUpdater = new SharedServiceUpdater(ScriptBehaviourUpdateOrder.CurrentPlayerLoop);
		RegisterService(serviceUpdater);
		RegisterService(base.gameObject.AddComponent<MaxUnitsController>());
		RegisterService(new LocalMultiplayerStateMonitor());
		RegisterService((IPlatformManager)(await new SteamManager().BuildPlatform()));
		RegisterService(new DlcCheckerService());
		RegisterService(new UnitsSpawnMonitor());
		RegisterServiceAsSelf(new TimeService(), typeof(ITimeService));
		RegisterService(new AchievementService());
		for (int j = 0; j < m_serviceAssets.Length; j++)
		{
			ServiceAsset service = m_serviceAssets[j].GetService();
			RegisterServiceAsSelf(service, service.GetType());
		}
		for (int k = 0; k < m_servicePrefabs.Length; k++)
		{
			ServicePrefab service2 = m_servicePrefabs[k].GetService();
			RegisterServiceAsSelf(service2, service2.GetType());
			service2.transform.SetParent(base.transform, worldPositionStays: false);
			service2.name = m_servicePrefabs[k].name;
		}
		for (int l = 0; l < m_globalPrefabs.Length; l++)
		{
			UnityEngine.Object.Instantiate(m_globalPrefabs[l], base.transform, worldPositionStays: false).name = m_globalPrefabs[l].name;
		}
		for (int m = 0; m < m_platformPrefabs.Length; m++)
		{
			GameObject gameObject2 = m_platformPrefabs[m];
			if (!(gameObject2 == null))
			{
				UnityEngine.Object.Instantiate(gameObject2, base.transform, worldPositionStays: false).name = gameObject2.name;
			}
		}
		RegisterService((ISaveLoaderService)new SteamSavesLoader());
		RegisterService((IDlcManagerService)new SteamDlcManagerService());
		RegisterService((IPlatformNetworkManagerService)new SteamNetworkManagerService());
		RegisterService((IGameInvitationService)new SteamGameInvitationService());
		RegisterService((IFriendService)new SteamFriendService());
		RegisterService((IPlatformUtils)new SteamPlatformUtils());
		RegisterService((IAccountPermissions)new AccountPermissionsNoUser());
		RegisterService((IPlayerPrefsPlatform)new PlayerPrefsDefault());
		RegisterService(new GameObjectPooling());
		RegisterService(new PlayerCamerasManager());
		RegisterService(new SplitScreenController());
		InitializeOnlineMultiplayerServices();
		PerformanceCounters counters = PerformanceCounters.CreateForFrameTimingSystem(serviceUpdater, 60);
		PerformanceDetector performanceDetector = null;
		DynamicResolutionManager dynamicResolutionManager = null;
		RegisterService(new PerformanceCounterService(settingsManager, counters, performanceDetector, dynamicResolutionManager));
		RegisterService(new LanguageInitializationService());
		GameObject obj = new GameObject("ShowUiModePopup");
		obj.transform.SetParent(base.transform, worldPositionStays: false);
		RegisterService(obj.AddComponent<ShowUiModePopup>());
		GetService<LanguageInitializationService>().QueueLanguageInitializationCallback();
		base.gameObject.AddComponent<AccountPropertyInitializer>();
		RegisterService(new PermissionsHelper());
	}

	private void InitializeOnlineMultiplayerServices()
	{
		PlatformSyncedNetworkService service = base.gameObject.AddComponent<PlatformSyncedNetworkService>();
		NetworkQuitController service2 = base.gameObject.AddComponent<NetworkQuitController>();
		NetworkDataCourier service3 = base.gameObject.AddComponent<NetworkDataCourier>();
		ProjectMarsGameServiceAsset service4 = GetService<ProjectMarsGameServiceAsset>();
		RegisterService((INetworkService)service);
		RegisterService((INetworkQuitController)service2);
		RegisterService(new SocialProfileService());
		RegisterService((INetworkDataCourier)service3);
		MultiplayerSessionMetadata.SetMetaDataKeys(service4);
		RegisterService((INetworkUserAuthenticator)new DummyUserAuthenticator());
	}

	private async void Awake()
	{
		if (_initialized)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		_initialized = true;
		await InitializeServices();
		int count = _serviceList.Count;
		for (int i = 0; i < count; i++)
		{
			_serviceList[i].OnAwake();
		}
		Application.quitting += ApplicationQuitting;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void ApplicationQuitting()
	{
		SharedServiceUpdater service = GetService<SharedServiceUpdater>();
		int count = _serviceList.Count;
		for (int i = 0; i < count; i++)
		{
			_serviceList[i].UnRegister();
		}
		_services.Clear();
		_serviceList.Clear();
		IDisposable disposable;
		if (service != null && (disposable = service) != null)
		{
			disposable.Dispose();
		}
	}

	private void Start()
	{
		int count = _serviceList.Count;
		for (int i = 0; i < count; i++)
		{
			_serviceList[i].OnStart();
		}
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
		int count = _serviceList.Count;
		for (int i = 0; i < count; i++)
		{
			_serviceList[i].OnUpdate();
		}
	}

	private void FixedUpdate()
	{
		int count = _serviceList.Count;
		for (int i = 0; i < count; i++)
		{
			_serviceList[i].OnFixedUpdate();
		}
	}

	private void LateUpdate()
	{
		int count = _serviceList.Count;
		for (int i = 0; i < count; i++)
		{
			_serviceList[i].OnLateUpdate();
		}
	}
}
