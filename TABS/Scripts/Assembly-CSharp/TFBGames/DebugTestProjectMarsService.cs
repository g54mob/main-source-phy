using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BitCode.Users;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Photon.Bolt;
using UdpKit.Platform.Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TFBGames
{
	public class DebugTestProjectMarsService : MonoBehaviour
	{
		private enum SessionVisibility
		{
			Public = 0,
			Private = 1,
			All = 2
		}

		[SerializeField]
		[Tooltip("Don't destroy this game object when scenes change. Only works if this object doesn't have a parent.")]
		protected bool m_dontDestroyOnLoad = true;

		[Space]
		[SerializeField]
		[Tooltip("Display on-screen info.")]
		protected bool m_displayInfo = true;

		[SerializeField]
		[Tooltip("Display Bolt settings.")]
		protected bool m_displayBoltSettings;

		[SerializeField]
		[Tooltip("Log when the network service's ISRunning state changes.")]
		protected bool m_logIsRunningChanges;

		[SerializeField]
		[Tooltip("Log when a scene is loaded.")]
		protected bool m_logSceneLoads;

		[Header("Input")]
		[SerializeField]
		[Tooltip("Enable the input for testing.")]
		protected bool _enableInput;

		[SerializeField]
		[Tooltip("Allow pressing keys when \"Display Info\" is false. The key \"Toggle Display Info\" is always allowed.")]
		protected bool _allowInputWhenHidden;

		[SerializeField]
		[Tooltip("Key to press to start Bolt as a server (for testing only).")]
		protected KeyCode m_startServerKey = KeyCode.F1;

		[SerializeField]
		[Tooltip("Key to press to start Bolt as a client (for testing only).")]
		protected KeyCode m_startClientKey = KeyCode.F2;

		[SerializeField]
		[Tooltip("Key to press to create a session with the first Simulation map.")]
		protected KeyCode m_simulationMapKey = KeyCode.F3;

		[SerializeField]
		[Tooltip("Key to press to create a session with the first Main map.")]
		protected KeyCode m_mainMapKey = KeyCode.F4;

		[SerializeField]
		[Tooltip("Key to press to join a random session.")]
		protected KeyCode m_randomSessionKey = KeyCode.F5;

		[SerializeField]
		[Tooltip("Key to press to get all sessions.")]
		protected KeyCode m_getSessionsKey = KeyCode.F6;

		[SerializeField]
		[Tooltip("Key to press to join the first open session found from the queries list.")]
		protected KeyCode m_joinSessionKey = KeyCode.F7;

		[SerializeField]
		[Tooltip("Key to press to create a private session with the first Main map")]
		protected KeyCode m_shutdownKey = KeyCode.F8;

		[SerializeField]
		[Tooltip("Key to press to shutdown the network system.")]
		protected KeyCode m_privateMainMapKey = KeyCode.Home;

		[SerializeField]
		[Tooltip("Key to press to get private sessions.")]
		protected KeyCode m_getPrivateSessionsKey = KeyCode.PageDown;

		[SerializeField]
		[Tooltip("Key to press to get all sessions.")]
		protected KeyCode m_getPublicSessionsKey = KeyCode.PageDown;

		[SerializeField]
		[Tooltip("Key to toggle on-screen info.")]
		protected KeyCode m_toggleDisplayInfo = KeyCode.Backslash;

		private const int MaxPingToShow = 1000;

		private NetworkBattleController m_battleController;

		private INetworkService m_networkService;

		private GameModeService _gameModeService;

		private BaseGameMode _baseGameMode;

		private GameStateManager m_gameStateManager;

		private AccountManager m_accountManager;

		private FieldInfo m_networkServiceStateFieldInfo;

		private int m_connectionsCount;

		private bool m_isRunning;

		private string m_sessionId;

		private Vector2 m_scrollPos;

		private int m_getBattleControllerDelay;

		private float? m_lastPing;

		private SessionVisibility currentSessionVisibility = SessionVisibility.All;

		private readonly string[] m_pingStrings = new string[1002];

		private readonly string[] m_lastPingStrings = new string[1002];

		private void Awake()
		{
			if (m_dontDestroyOnLoad && base.transform.parent == null)
			{
				Object.DontDestroyOnLoad(base.gameObject);
			}
			CreatePingStrings();
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void Start()
		{
			m_networkService = ServiceLocator.GetService<INetworkService>();
			_gameModeService = ServiceLocator.GetService<GameModeService>();
			_baseGameMode = ((_gameModeService != null) ? _gameModeService.CurrentGameMode : null);
			m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
			m_accountManager = ServiceLocator.GetService<AccountManager>();
			m_networkServiceStateFieldInfo = typeof(NetworkService).GetField("state", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (m_networkServiceStateFieldInfo == null)
			{
				LogInfo("Failed to get reflection info for the NetworkService's state field.");
			}
		}

		private void OnDestroy()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			m_getBattleControllerDelay = 2;
			if (m_logSceneLoads)
			{
				LogInfo("OnSceneLoaded: " + scene.name);
			}
		}

		private void Update()
		{
		}

		private void OnGUI()
		{
		}

		private void CreatePingStrings()
		{
			int i = 0;
			for (int num = m_pingStrings.Length; i < num; i++)
			{
				if (i > 1000)
				{
					m_pingStrings[i] = $"<color=#55ff55>Ping:</color> {1000}+";
					m_lastPingStrings[i] = $"<color=#ff5555>Last Ping:</color> {1000}+";
				}
				else
				{
					m_pingStrings[i] = $"<color=#55ff55>Ping:</color> {i}";
					m_lastPingStrings[i] = $"<color=#ff5555>Last Ping:</color> {i}";
				}
			}
		}

		private void UpdateServices()
		{
			m_connectionsCount = 0;
			if (m_networkService.IsRunning && m_networkService.IsConnected && BoltNetwork.Connections != null)
			{
				foreach (BoltConnection connection in BoltNetwork.Connections)
				{
					_ = connection;
					m_connectionsCount++;
				}
			}
			if (m_getBattleControllerDelay > 0)
			{
				m_getBattleControllerDelay--;
				if (m_getBattleControllerDelay <= 0)
				{
					m_battleController = ServiceLocator.GetService<NetworkBattleController>();
				}
			}
			if (_gameModeService != null && _baseGameMode != _gameModeService.CurrentGameMode)
			{
				_baseGameMode = _gameModeService.CurrentGameMode;
			}
			if (m_logIsRunningChanges && m_isRunning != m_networkService.IsRunning)
			{
				m_isRunning = m_networkService.IsRunning;
				LogInfo($"IsRunning changed: {m_isRunning}");
			}
		}

		private void UpdateInput()
		{
			if (!_enableInput)
			{
				return;
			}
			if (Input.GetKeyDown(m_toggleDisplayInfo))
			{
				m_displayInfo = !m_displayInfo;
			}
			if (m_displayInfo || _allowInputWhenHidden)
			{
				if (Input.GetKeyDown(m_startServerKey) && !m_networkService.IsRunning)
				{
					BoltLauncher.StartServer();
				}
				if (Input.GetKeyDown(m_startClientKey) && !m_networkService.IsRunning)
				{
					BoltLauncher.StartClient();
				}
				if (Input.GetKeyDown(m_simulationMapKey))
				{
					CreateSession(MapAsset.MapType.Simulation, 0);
				}
				if (Input.GetKeyDown(m_mainMapKey))
				{
					CreateSession(MapAsset.MapType.Main, 0);
				}
				if (Input.GetKeyDown(m_randomSessionKey))
				{
					JoinRandomSession();
				}
				if (Input.GetKeyDown(m_getSessionsKey))
				{
					GetSessions(SessionVisibility.All);
				}
				if (Input.GetKeyDown(m_getPrivateSessionsKey))
				{
					GetSessions(SessionVisibility.Private);
				}
				if (Input.GetKeyDown(m_getPublicSessionsKey))
				{
					GetSessions(SessionVisibility.Public);
				}
				if (Input.GetKeyDown(m_joinSessionKey))
				{
					JoinSession();
				}
				if (Input.GetKeyDown(m_shutdownKey))
				{
					Shutdown();
				}
				if (Input.GetKeyDown(m_privateMainMapKey))
				{
					CreateSession(MapAsset.MapType.Main, 0, isPublicSession: false);
				}
			}
		}

		private void JoinSession()
		{
			LogInfo("JoinSession");
			if (string.IsNullOrEmpty(m_sessionId))
			{
				LogInfo("JoinSession: session ID is not valid. First get the sessions.");
			}
			else if (m_networkService.IsRunning && !m_networkService.IsClient)
			{
				LogInfo("JoinSession: IsRunning and not client. Shutdown first.");
			}
			else
			{
				m_networkService.JoinSessionAsync(isQuickGame: false, new JoinSessionProperties(m_sessionId, null), OnJoinSession);
			}
		}

		private void OnJoinSession(NetworkSession session, NetworkException exception)
		{
			LogInfo("OnJoinSession: session: (" + SessionToString(session) + ")" + NetworkExceptionString(exception));
		}

		private void GetSessions(SessionVisibility sessionVisibility)
		{
			LogInfo("GetSessions");
			if (m_networkService.IsRunning && !m_networkService.IsClient)
			{
				LogInfo("GetSessions: IsRunning and not client. Shutdown first.");
				return;
			}
			currentSessionVisibility = sessionVisibility;
			m_networkService.GetSessionsAsync(OnGetSessions);
		}

		private void OnGetSessions(NetworkSession[] sessions, NetworkException exception)
		{
			string text = ((sessions != null) ? sessions.Length.ToString() : "null");
			LogInfo("OnGetSessions: " + text + NetworkExceptionString(exception));
			if (sessions == null || sessions.Length == 0)
			{
				return;
			}
			m_sessionId = null;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Sessions:");
			int i = 0;
			for (int num = sessions.Length; i < num; i++)
			{
				NetworkSession networkSession = sessions[i];
				if (m_sessionId == null && networkSession.IsOpen && networkSession.IsVisible)
				{
					m_sessionId = networkSession.Id;
				}
				switch (currentSessionVisibility)
				{
				case SessionVisibility.All:
					stringBuilder.AppendLine($"{i + 1}) {SessionToString(networkSession)}");
					break;
				case SessionVisibility.Public:
					if (networkSession.Metadata.HostRoomIsPublic)
					{
						stringBuilder.AppendLine($"{i + 1}) {SessionToString(networkSession)}");
					}
					break;
				case SessionVisibility.Private:
					if (!networkSession.Metadata.HostRoomIsPublic)
					{
						stringBuilder.AppendLine($"{i + 1}) {SessionToString(networkSession)}");
					}
					break;
				}
			}
			LogInfo(stringBuilder.ToString());
		}

		private void JoinRandomSession()
		{
			LogInfo("JoinRandomSession");
			if (m_networkService.IsRunning && !m_networkService.IsClient)
			{
				LogInfo("JoinRandomSession: IsRunning and not client. Shutdown first.");
			}
			else
			{
				m_networkService.JoinRandomSessionAsync(null, OnJoinRandomSession);
			}
		}

		private void OnJoinRandomSession(NetworkSession session, NetworkException exception)
		{
			LogInfo("OnJoinRandomSession: session: (" + SessionToString(session) + ")" + NetworkExceptionString(exception));
		}

		private void Shutdown()
		{
			LogInfo("Shutdown");
			m_networkService.ShutdownAsync(OnShutdown);
		}

		private void OnShutdown(NetworkException exception)
		{
			LogInfo("OnShutdown" + NetworkExceptionString(exception));
		}

		private void CreateSession(MapAsset.MapType mapType, int mapIndex, bool isPublicSession = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Multiplayer Debug SessionInfo");
			stringBuilder.AppendLine($"CreateSession: mapType: {mapType}");
			stringBuilder.AppendLine($"mapIndex: {mapIndex}");
			stringBuilder.AppendLine($"canPlayCrossNetwork: {true}");
			stringBuilder.AppendLine($"isPublicSession {isPublicSession}");
			LogInfo(stringBuilder.ToString());
			CreateSessionProperties properties = new CreateSessionProperties(mapType, mapIndex, canPlayCrossNetwork: true, isPublicSession);
			m_networkService.CreateSessionAsync(properties, OnCreateSession);
		}

		private void OnCreateSession(NetworkSession session, NetworkException exception)
		{
			LogInfo("OnCreateSession: session: (" + SessionToString(session) + ")" + NetworkExceptionString(exception));
		}

		private static string SessionToString(NetworkSession session)
		{
			if (session == null)
			{
				return string.Empty;
			}
			return $"isOpen: {session.IsOpen}     isVisible: {session.IsVisible}     " + $"mapType: {session.Metadata.RoomMapType}     mapIndex: {session.Metadata.RoomMapIndex}     " + $"platform: {session.Metadata.HostPlatform}     version: {session.Metadata.GameVersionNumber}     " + "playerName: " + session.Metadata.HostPlayerDisplayName + "     sessionId: " + session.Id;
		}

		private static string NetworkExceptionString(NetworkException exception, bool addNewLine = true)
		{
			if (exception == null)
			{
				return string.Empty;
			}
			string arg = (addNewLine ? "\n" : string.Empty);
			return $"{arg}<color=#ff5555>ErrorCode: {exception.ErrorCode}</color>     exception: {exception}";
		}

		private void DrawDebug()
		{
			float num = 20f;
			float num2 = 10f;
			Vector2 vector = new Vector2(400f, 200f);
			Rect rect = new Rect(num, num + 50f, vector.x, vector.x);
			GUI.Box(rect, string.Empty);
			GUI.Box(rect, string.Empty);
			rect.xMin += num2;
			rect.xMax -= num2;
			rect.yMin += num2;
			rect.yMax -= num2;
			GUILayout.BeginArea(rect);
			m_scrollPos = GUILayout.BeginScrollView(m_scrollPos);
			if (_enableInput)
			{
				DrawHeading($"(Press {m_toggleDisplayInfo} to show/hide)");
			}
			DrawBoltSettings();
			DrawUserInfo();
			DrawPlatformInfo();
			DrawNetworkServiceInfo();
			DrawBattleControllerInfo();
			DrawGameState();
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		private void DrawHeading(string heading)
		{
			Color color = GUI.color;
			GUI.color = Color.yellow;
			GUILayout.Label(heading);
			GUI.color = color;
		}

		private void DrawProperty(string propertyName, string propertyValue)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				GUILayout.Label(propertyValue);
			}
			else
			{
				GUILayout.Label("<color=#55ff55>" + propertyName + ":</color> " + propertyValue);
			}
		}

		private void DrawPingString(float ping)
		{
			int value = (int)(ping * 1000f);
			value = Mathf.Clamp(value, 0, m_pingStrings.Length - 1);
			GUILayout.Label(m_pingStrings[value]);
		}

		private void DrawLastPingString(float ping)
		{
			int value = (int)(ping * 1000f);
			value = Mathf.Clamp(value, 0, m_lastPingStrings.Length - 1);
			GUILayout.Label(m_lastPingStrings[value]);
		}

		private void DrawBoltSettings()
		{
			if (m_displayBoltSettings && !(BoltRuntimeSettings.instance == null))
			{
				BoltConfig configCopy = BoltRuntimeSettings.instance.GetConfigCopy();
				if (configCopy != null)
				{
					DrawHeading("Bolt");
					DrawProperty("FPS", configCopy.framesPerSecond.ToString());
					DrawProperty("ServerSendRate", configCopy.serverSendRate.ToString());
					DrawProperty("ClientSendRate", configCopy.clientSendRate.ToString());
					DrawProperty("fixedDeltaTime", Time.fixedDeltaTime.ToString("f3"));
					DrawProperty("timeScale", Time.timeScale.ToString("f7"));
				}
			}
		}

		private void DrawUserInfo()
		{
			ILocalAccount localAccount = ((m_accountManager != null) ? m_accountManager.ActiveAccount : null);
			if (localAccount != null && localAccount.Name != null && localAccount.Name.Status == UserAccountPropertyStatus.Loaded)
			{
				DrawProperty("User", localAccount.Name.Value);
			}
		}

		private void DrawPlatformInfo()
		{
		}

		private void DrawNetworkServiceInfo()
		{
			if (m_networkService == null)
			{
				return;
			}
			DrawHeading("NetworkService");
			if (m_networkServiceStateFieldInfo != null)
			{
				DrawProperty("State", ((NetworkService.State)m_networkServiceStateFieldInfo.GetValue(m_networkService)/*cast due to .constrained prefix*/).ToString());
			}
			DrawProperty("IsRunning", m_networkService.IsRunning.ToString());
			DrawProperty("Svr/Clnt", (m_networkService.IsServer ? "Server" : (m_networkService.IsClient ? "Client" : "Neither")) ?? "");
			DrawProperty("IsConnected", m_networkService.IsConnected.ToString());
			if (m_connectionsCount > 0)
			{
				DrawProperty("Connections", m_connectionsCount.ToString());
			}
			string regionCode = m_networkService.RegionCode;
			PhotonRegion photonRegion = ((PhotonRegion.regions != null && !string.IsNullOrEmpty(regionCode)) ? PhotonRegion.GetRegion(regionCode) : null);
			string text = regionCode;
			if (photonRegion != null && !string.IsNullOrEmpty(photonRegion.Name))
			{
				text = photonRegion.Name + " [" + regionCode + "]";
			}
			if (!string.IsNullOrEmpty(text))
			{
				DrawProperty("Region", text);
			}
			float? lastPing = null;
			if (m_networkService.IsConnected && m_networkService.IsClient && BoltNetwork.Server != null)
			{
				lastPing = BoltNetwork.Server.PingNetwork;
			}
			else if (m_networkService.IsConnected && m_networkService.IsServer && BoltNetwork.Clients != null)
			{
				DrawProperty("Client Count", BoltNetwork.Clients.Count().ToString());
				using (IEnumerator<BoltConnection> enumerator = BoltNetwork.Clients.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						BoltConnection current = enumerator.Current;
						lastPing = current.PingNetwork;
					}
				}
			}
			if (lastPing.HasValue)
			{
				m_lastPing = lastPing;
				DrawPingString(lastPing.Value);
			}
			else if (m_lastPing.HasValue)
			{
				DrawLastPingString(m_lastPing.Value);
			}
		}

		private void DrawBattleControllerInfo()
		{
			if (!(m_battleController == null))
			{
				DrawHeading("BattleController");
				DrawProperty("Phase", m_battleController.Phase.ToString());
				DrawProperty("RemotePhase", m_battleController.RemotePhase.ToString());
			}
		}

		private void DrawGameState()
		{
			if (!(_gameModeService == null) && _baseGameMode != null)
			{
				DrawHeading("GameState/Mode");
				DrawProperty("GameModeState", m_gameStateManager.GameState.ToString());
				DrawProperty("BaseGameMode", (_baseGameMode != null) ? _baseGameMode.GetType().Name : "null");
				DrawProperty("GameModeState", CampaignPlayerDataHolder.CurrentGameModeState.ToString());
			}
		}

		private static void LogInfo(string message, params object[] args)
		{
			message = "<color=#55ff55>[DBG-NET-SERVICE]</color> " + message + "     " + $"({Time.frameCount} / {Time.realtimeSinceStartup})";
			Debug.LogFormat(message, args);
		}
	}
}
