using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BesiegeDlc;
using Localisation;
using Modding;
using UnityEngine;
using UnityEngine.UI;

public class NetworkScene : MonoBehaviour, ILocalisationAware
{
	[HideInInspector]
	public NetworkHUD hud;

	public InputField ipInput;

	public static NetworkScene Instance;

	public static bool IsReconnect = false;

	public static bool HasConnected = false;

	public static string LastIP = string.Empty;

	public static int LastPort = 0;

	public static ulong? LastSteamLobbyID = null;

	public static ulong? LastSteamServerID = null;

	public static string LastPlayfabServerID = null;

	public static string LastPassword;

	public static ServerSettings ServerSettings = new ServerSettings();

	public List<PlayerData> clientList;

	public List<ushort> clientIDList;

	private NetworkAuxAddPiece networkAuxAddPiece;

	private bool sceneChanged;

	private BesiegeNetworkManager networkManager;

	private string testMachinePath;

	private float testDuration = -1f;

	private int numTestMachines;

	private bool hostReady;

	private Coroutine enableMenu;

	public bool IsHostReady()
	{
		return hostReady;
	}

	private void Awake()
	{
		Instance = this;
		BesiegeEntryPoint.onConnectToIpServer = (Action<string, int, string>)Delegate.Combine(BesiegeEntryPoint.onConnectToIpServer, new Action<string, int, string>(ConnectToIpServer));
		BesiegeEntryPoint.OnConnectToPlayfabServer = (Action<string>)Delegate.Combine(BesiegeEntryPoint.OnConnectToPlayfabServer, new Action<string>(ConnectToPlayfabServer));
		BesiegeEntryPoint.onConnectToServer = (Action<ulong, string>)Delegate.Combine(BesiegeEntryPoint.onConnectToServer, new Action<ulong, string>(ConnectToServer));
		BesiegeEntryPoint.onConnectToLobby = (Action<ulong, string>)Delegate.Combine(BesiegeEntryPoint.onConnectToLobby, new Action<ulong, string>(ConnectToLobby));
		BesiegeEntryPoint.onStartServer = (Action<DedicatedServerMode>)Delegate.Combine(BesiegeEntryPoint.onStartServer, new Action<DedicatedServerMode>(StartServer));
		BesiegeEntryPoint.onTestMachine = (Action<string, float, bool, int>)Delegate.Combine(BesiegeEntryPoint.onTestMachine, new Action<string, float, bool, int>(TestMachine));
		hud.multiverseUI.SetActive(false);
		StatMaster.isMP = true;
		enableMenu = StartCoroutine(IEEnableMenu());
		SetDefaultPlayerName();
	}

	private void Start()
	{
		clientIDList = new List<ushort>();
		clientList = new List<PlayerData>();
		networkAuxAddPiece = NetworkAuxAddPiece.Instance;
		networkManager = BesiegeNetworkManager.Instance;
		networkManager.onConnected = OnConnect;
		networkManager.onDisconnected = OnDisconnect;
		networkManager.onTimeout = OnTimeout;
		networkManager.onPlayerJoin = OnPlayerJoin;
		networkManager.onPlayerLeave = OnPlayerLeave;
		networkManager.onClientConnectionStateChanged = OnClientConnectionStateChanged;
		networkManager.onServerConnectionStateChanged = OnServerConnectionStateChanged;
	}

	private IEnumerator IEEnableMenu()
	{
		yield return new WaitForSeconds(0.1f);
		hud.multiverseUI.SetActive(true);
		enableMenu = null;
	}

	private void StartHeadlessServer(bool isSpectator)
	{
		if (StatMaster.isHeadless)
		{
			DisableNonEssentials();
		}
		if (enableMenu != null)
		{
			StopCoroutine(enableMenu);
		}
		if (string.IsNullOrEmpty(OptionsMaster.BesiegeConfig.PlayerName))
		{
			OptionsMaster.BesiegeConfig.PlayerName = "SERVER";
		}
		hud.EnableConnectionWidget();
		hud.ToggleMultiverseOptions(false);
		string text = Path.Combine(StaticSettings.DataPath, "ServerConfig.xml");
		ServerConfig serverConfig = new ServerConfig();
		if (!File.Exists(text))
		{
			serverConfig.MapRotation.Add("notRocketLeague.blv");
			serverConfig.Save(text);
		}
		else
		{
			serverConfig.Load(text);
		}
		ServerSettings serverSettings = new ServerSettings();
		StatMaster.Mode.levelEdit = true;
		serverSettings.levelEditor = true;
		OptionsMaster.spectatorEnabled = isSpectator;
		serverSettings.levelEditor = serverConfig.LevelEditorEnabled;
		serverSettings.password = serverConfig.Password;
		serverSettings.maxPlayers = serverConfig.MaxPlayers;
		serverSettings.useUPNP = serverConfig.UseUPNPForwarding;
		ObtainLocalDlcMask(serverSettings);
		if (!serverConfig.LevelEditorEnabled)
		{
			serverSettings.playList = serverConfig.MapRotation;
			serverSettings.playListIndex = 0;
		}
		int port = serverConfig.Port;
		Host(serverSettings, port);
	}

	private static void ObtainLocalDlcMask(ServerSettings settings)
	{
		List<uint> localDlcTypes = DlcManager.Instance.GetLocalDlcTypes(true);
		settings.dlcMask = DlcManager.Instance.GetMaskFromDlcTypes(localDlcTypes);
	}

	public void SaveServerConfig()
	{
		string configPath = Path.Combine(StaticSettings.DataPath, "ServerConfig.xml");
		ServerConfig serverConfig = new ServerConfig();
		serverConfig.LevelEditorEnabled = ServerSettings.levelEditor;
		serverConfig.Password = ServerSettings.password;
		serverConfig.MapRotation = ServerSettings.playList;
		serverConfig.MaxPlayers = ServerSettings.maxPlayers;
		serverConfig.UseUPNPForwarding = ServerSettings.useUPNP;
		serverConfig.Port = LastPort;
		serverConfig.Save(configPath);
	}

	public void UpdateSettings(ServerSettings settings)
	{
		uint dlcMask = ServerSettings.dlcMask;
		ServerSettings = settings;
		if (dlcMask != settings.dlcMask && DlcManager.Instance.DlcSettingsChanged != null)
		{
			DlcManager.Instance.DlcSettingsChanged();
		}
		LevelEditor.Instance.OnUpdateSettings(settings);
	}

	public bool GetMachine(ushort playerId, out ServerMachine machine)
	{
		PlayerData player;
		if (!Playerlist.GetPlayer(playerId, out player) || player.isSpectator)
		{
			machine = null;
			return false;
		}
		machine = player.machine;
		return true;
	}

	public void Host(ServerSettings settings, int port)
	{
		hostReady = false;
		IsReconnect = false;
		StatMaster.isServer = true;
		UpdateSettings(settings);
		networkManager.SetUPNPEnabled(settings.useUPNP);
		networkManager.Host(port);
		LastIP = string.Empty;
		LastPort = port;
		if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
		{
			StartCoroutine(showAbortIfStuck(25f));
		}
	}

	private IEnumerator showAbortIfStuck(float duration)
	{
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			if (networkManager.isConnected)
			{
				yield break;
			}
			yield return null;
		}
		hud.ToggleAbortButton(true);
	}

	public void Join(string ip, int port)
	{
		StatMaster.isServer = false;
		IsReconnect = ip.Equals(LastIP);
		LastIP = ip;
		LastPort = port;
		LastSteamLobbyID = null;
		LastSteamServerID = null;
		LastPlayfabServerID = null;
		hud.ToggleAbortButton(true);
		networkManager.SetUPNPEnabled(ReferenceMaster.UPNPStatus != UPNPStatus.Initializing);
		networkManager.Join(ip, port);
	}

	public void Join(string pfNetworkId)
	{
		StatMaster.isServer = false;
		IsReconnect = pfNetworkId.Equals(LastIP);
		LastIP = pfNetworkId;
		LastSteamLobbyID = null;
		LastSteamServerID = null;
		LastPlayfabServerID = pfNetworkId;
		hud.ToggleAbortButton(true);
		networkManager.SetUPNPEnabled(ReferenceMaster.UPNPStatus != UPNPStatus.Initializing);
		networkManager.Join(pfNetworkId);
	}

	public void Join(ulong gameserverId)
	{
		StatMaster.isServer = false;
		IsReconnect = gameserverId.ToString().Equals(LastIP);
		LastIP = gameserverId.ToString();
		LastSteamServerID = gameserverId;
		LastSteamLobbyID = null;
		LastPlayfabServerID = null;
		IsReconnect = false;
		hud.ToggleAbortButton(true);
		networkManager.SetUPNPEnabled(false);
		networkManager.Join(gameserverId);
	}

	public void ForceAbort()
	{
		networkManager.Stop();
	}

	public static void ResetMPSettings()
	{
		StatMaster.ResetStateSettings();
		LevelEditor.ResetLevelSettings();
	}

	public void OnSceneChanged()
	{
		ResetMPSettings();
		CanvasFollowVisability.Reset();
	}

	public void ManualStop(string reason)
	{
		networkManager.SetDisconnectMessage(reason);
		ManualStop();
	}

	public void ManualStop()
	{
		networkManager.Stop();
	}

	protected void OnApplicationQuit()
	{
		sceneChanged = true;
		ManualStop();
	}

	private void OnConnect()
	{
		NetworkHUD.connecting = false;
		StatMaster.isHosting = StatMaster.isServer;
		StatMaster.isClient = !StatMaster.isHosting;
		StatMaster.networkActive = true;
		HasConnected = true;
		networkAuxAddPiece.SetOwner(networkManager.PlayerID);
		ReferenceMaster.InvokeOnConnect();
	}

	protected void OnDestroy()
	{
		BesiegeEntryPoint.onConnectToIpServer = (Action<string, int, string>)Delegate.Remove(BesiegeEntryPoint.onConnectToIpServer, new Action<string, int, string>(ConnectToIpServer));
		BesiegeEntryPoint.OnConnectToPlayfabServer = (Action<string>)Delegate.Remove(BesiegeEntryPoint.OnConnectToPlayfabServer, new Action<string>(ConnectToPlayfabServer));
		BesiegeEntryPoint.onConnectToServer = (Action<ulong, string>)Delegate.Remove(BesiegeEntryPoint.onConnectToServer, new Action<ulong, string>(ConnectToServer));
		BesiegeEntryPoint.onConnectToLobby = (Action<ulong, string>)Delegate.Remove(BesiegeEntryPoint.onConnectToLobby, new Action<ulong, string>(ConnectToLobby));
		BesiegeEntryPoint.onStartServer = (Action<DedicatedServerMode>)Delegate.Remove(BesiegeEntryPoint.onStartServer, new Action<DedicatedServerMode>(StartServer));
		BesiegeEntryPoint.onTestMachine = (Action<string, float, bool, int>)Delegate.Remove(BesiegeEntryPoint.onTestMachine, new Action<string, float, bool, int>(TestMachine));
	}

	private void OnDisconnect(bool isOnDestroy)
	{
		bool flag = true;
		if (PlayerData.hasLocalPlayer)
		{
			PlayerData localPlayer = PlayerData.localPlayer;
			flag = localPlayer.isSpectator;
		}
		sceneChanged = isOnDestroy;
		ShutdownClient();
		if (!sceneChanged && !flag)
		{
			hud.prevBuild = ((!Machine.IsStartMachine(MachineObjectTracker.lastBuild)) ? MachineObjectTracker.lastBuild : null);
		}
		ReferenceMaster.InvokeOnDisconnect();
	}

	private void OnTimeout()
	{
		ShutdownClient();
	}

	public void ShutdownClient()
	{
		AddPiece.isEditingLevel = (AddPiece.disableBlockPlacement = false);
		StatMaster.IsLevelEditorOnly = false;
		while (StatMaster.stopHotkeys)
		{
			StatMaster.StopHotKeys(false);
		}
		if (!sceneChanged)
		{
			networkAuxAddPiece.OnClientStop();
		}
		else
		{
			Playerlist.ClearPlayers();
		}
		PlayerData.hasLocalPlayer = false;
		PlayerData.localPlayer = null;
		networkAuxAddPiece.receivedGameState = (networkAuxAddPiece.requestedSimFrame = false);
	}

	public void OnHostReady()
	{
		hostReady = true;
	}

	private void OnPlayerJoin(ushort playerId)
	{
		if (playerId == networkManager.PlayerID)
		{
			PlayerData player = networkAuxAddPiece.PlayerConnected(playerId, true);
			hud.OnStartHost();
			networkAuxAddPiece.InitServerPlayer(player, networkAuxAddPiece.GetPlayerConfig());
			hud.OnGameStateReceived();
			if (StatMaster.isServer)
			{
				SingleInstanceFindOnly<LevelEditorUI>.Instance.SetUIState(ServerSettings.levelEditor ? LevelEditorUI.UIState.BuildMode : LevelEditorUI.UIState.Inactive);
			}
		}
		else
		{
			if (!hostReady)
			{
				return;
			}
			bool flag = Playerlist.Players.Count >= ServerSettings.maxPlayers;
			PlayerData player = networkAuxAddPiece.PlayerConnected(playerId, true);
			byte[] array = new byte[flag ? 1 : 12];
			array[0] = (byte)(flag ? 1u : 0u);
			if (!flag)
			{
				bool flag2 = !string.IsNullOrEmpty(ServerSettings.password);
				array[1] = (byte)(flag2 ? 1u : 0u);
				NetworkCompression.WriteUInt16(playerId, array, 2);
				byte[] bytes = BitConverter.GetBytes(networkManager.LobbyID);
				Buffer.BlockCopy(bytes, 0, array, 4, bytes.Length);
				if (flag2)
				{
					player.passCorrect = false;
				}
				if (BesiegeLogFilter.logDebug)
				{
					Debug.Log("Player " + playerId + " joined, PW protected: " + flag2 + ", sending lobby ID " + networkManager.LobbyID);
				}
				clientList.Add(player);
				clientIDList.Add(playerId);
			}
			networkAuxAddPiece.SendPlayerMessage(playerId, RPCMessageType.Init, array);
			SingleInstance<Events>.Instance.PlayerJoined(playerId);
			if (flag)
			{
				if (BesiegeLogFilter.logDebug)
				{
					Debug.Log("Player " + playerId + " joined, server full");
				}
				networkAuxAddPiece.PlayerDisconnected(playerId);
			}
		}
	}

	private void OnPlayerLeave(ushort playerId)
	{
		PlayerData player;
		if (!StatMaster.isClient && hostReady && Playerlist.GetPlayer(playerId, out player) && !player.isLocalPlayer && clientList.Contains(player))
		{
			SingleInstance<Events>.Instance.PlayerLeave(playerId);
			clientIDList.Remove(playerId);
			clientList.Remove(player);
			networkAuxAddPiece.RemoveServerPlayer(player);
		}
	}

	private string GetClientConnectionStateString(ClientConnectionState newState)
	{
		string empty = string.Empty;
		switch (newState)
		{
		case ClientConnectionState.Disconnected:
		case ClientConnectionState.Disconnecting:
			return LocalisationManager.GetTranslation(2005);
		case ClientConnectionState.AttemptingDirectConnect:
			return LocalisationManager.GetTranslation(2006);
		case ClientConnectionState.DirectConnectFailed:
			return LocalisationManager.GetTranslation(2007);
		case ClientConnectionState.ResolvingHost:
			return LocalisationManager.GetTranslation(2008);
		case ClientConnectionState.HostListReceived:
			return LocalisationManager.GetTranslation(2009);
		case ClientConnectionState.HostNotFound:
			return LocalisationManager.GetTranslation(2010);
		case ClientConnectionState.FindingLobby:
			return LocalisationManager.GetTranslation(2011);
		case ClientConnectionState.LobbyNotFound:
			return LocalisationManager.GetTranslation(2012);
		case ClientConnectionState.JoiningLobby:
			return LocalisationManager.GetTranslation(2013);
		case ClientConnectionState.LobbyJoined:
			return LocalisationManager.GetTranslation(2014);
		case ClientConnectionState.FailedToJoinLobby:
			return LocalisationManager.GetTranslation(2015);
		case ClientConnectionState.PunchingThroughToServer:
			return string.Format(LocalisationManager.GetTranslation(2016), networkManager.ConnectAttempt(), OptionsMaster.BesiegeConfig.MaxReconnectAttempts);
		case ClientConnectionState.HolePunchedFailed:
			return LocalisationManager.GetTranslation(2017);
		case ClientConnectionState.PlayfabLogin:
		case ClientConnectionState.Connecting:
			return string.Format(LocalisationManager.GetTranslation(2018), networkManager.ConnectAttempt(), OptionsMaster.BesiegeConfig.MaxReconnectAttempts);
		case ClientConnectionState.Connected:
			return LocalisationManager.GetTranslation(2019);
		case ClientConnectionState.CRCMismatch:
			return LocalisationManager.GetTranslation(2020);
		default:
			return LocalisationManager.GetTranslation(2021);
		}
	}

	private void OnClientConnectionStateChanged(ClientConnectionState newState)
	{
		try
		{
			string clientConnectionStateString = GetClientConnectionStateString(newState);
			hud.SetLoadingText(clientConnectionStateString);
		}
		catch (Exception)
		{
		}
	}

	private void CloseMultiverseScreen()
	{
		hud.EnableConnectionWidget();
		hud.ToggleMultiverseOptions(false);
	}

	private bool SetSteamName()
	{
		if (SteamManager.Initialized)
		{
			OptionsMaster.BesiegeConfig.PlayerName = SingleInstance<WorkshopManager>.Instance.GetPlayerName();
			return true;
		}
		return false;
	}

	private void SetDefaultPlayerName()
	{
		if (string.IsNullOrEmpty(OptionsMaster.BesiegeConfig.PlayerName) || OptionsMaster.BesiegeConfig.PlayerName.Equals("UNKNOWN PLAYER"))
		{
			bool flag = false;
			if (!SetSteamName())
			{
				OptionsMaster.BesiegeConfig.PlayerName = LocalisationManager.GetTranslation(1947);
			}
		}
	}

	private string GetServerConnectionString(ServerConnectionState state)
	{
		string result = string.Empty;
		switch (state)
		{
		case ServerConnectionState.Disconnected:
			result = LocalisationManager.GetTranslation(3459);
			break;
		case ServerConnectionState.InitializationFailed:
			result = LocalisationManager.GetTranslation(3461);
			break;
		case ServerConnectionState.InitializingHost:
			result = LocalisationManager.GetTranslation(3460);
			break;
		case ServerConnectionState.WaitingForConnection:
		case ServerConnectionState.WaitingForPlatformConnection:
			result = LocalisationManager.GetTranslation(3462);
			break;
		}
		return result;
	}

	private void OnServerConnectionStateChanged(ServerConnectionState state)
	{
		if (state == ServerConnectionState.WaitingForConnection || state == ServerConnectionState.WaitingForPlatformConnection)
		{
			UpdateSettings(ServerSettings);
		}
		string serverConnectionString = GetServerConnectionString(state);
		hud.SetLoadingText(serverConnectionString);
		InitializeTestRoutine(state);
	}

	private void InitializeTestRoutine(ServerConnectionState state)
	{
		if (!string.IsNullOrEmpty(testMachinePath) && state == ServerConnectionState.WaitingForConnection)
		{
			PerformanceTest performanceTest = new GameObject("PerformanceTest").AddComponent<PerformanceTest>();
			performanceTest.LoadAndTestMachine(testMachinePath, testDuration, numTestMachines);
		}
	}

	public void ConnectToLobby(ulong lobbySteamId, string password)
	{
		if (!SteamManager.Initialized)
		{
			Debug.LogError("Could not join lobby, are you sure Steam is on?");
			return;
		}
		LastSteamLobbyID = lobbySteamId;
		LastSteamServerID = null;
		LastPlayfabServerID = null;
		LastPassword = password;
		PrepareConnect();
		StatMaster.isServer = false;
		networkManager.JoinLobby(lobbySteamId, password);
	}

	public void ConnectToIpServer(string ipAddress, int port, string password)
	{
		PrepareConnect();
		Join(ipAddress, port);
	}

	public void ConnectToPlayfabServer(string playfabNetworkId)
	{
		Debug.Log("[NetworkScene] ConnectToPlayfabServer " + playfabNetworkId);
		if (networkManager.isConnected && networkManager.CurrentNetwork.Equals(playfabNetworkId))
		{
			Debug.LogWarning("[NetworkScene] ConnectToPlayfabServer Skipping join, already joined this network!");
			return;
		}
		PrepareConnect();
		Join(playfabNetworkId);
	}

	public void ConnectToServer(ulong serverId, string password)
	{
		PrepareConnect();
		Join(serverId);
	}

	private void PrepareConnect()
	{
		if (enableMenu != null)
		{
			StopCoroutine(enableMenu);
		}
		SetDefaultPlayerName();
		ManualStop();
		hud.ToggleAbortButton(true);
		networkManager.SetDisconnectMessage(string.Empty);
		CloseMultiverseScreen();
	}

	public void Reconnect()
	{
		ManualStop();
		CloseMultiverseScreen();
		Join(LastIP, LastPort);
	}

	private void RemoveChildBehaviours(Transform transform)
	{
		MonoBehaviour[] componentsInChildren = transform.GetComponentsInChildren<MonoBehaviour>(true);
		foreach (MonoBehaviour obj in componentsInChildren)
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
	}

	private void DisableCamera(Camera camera)
	{
		camera.enabled = false;
		RemoveChildBehaviours(camera.transform);
	}

	private void DisableCameras()
	{
		Camera[] allCameras = Camera.allCameras;
		foreach (Camera camera in allCameras)
		{
			DisableCamera(camera);
		}
		CinematicCam obj = UnityEngine.Object.FindObjectOfType<CinematicCam>();
		UnityEngine.Object.DestroyImmediate(obj);
	}

	private void DisableHUD()
	{
		HudInputControl hudInputControl = UnityEngine.Object.FindObjectOfType<HudInputControl>();
		if (hudInputControl != null)
		{
			hudInputControl.ToggleHUD(false);
		}
		GameObject gameObject = GameObject.Find("HUD");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		GameObject gameObject2 = GameObject.Find("Canvas");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(false);
		}
		TimeSliderView instance = TimeSliderView.Instance;
		instance.transform.SetParent(null);
	}

	private void DisableSound()
	{
		AudioListener audioListener = UnityEngine.Object.FindObjectOfType<AudioListener>();
		if (audioListener == null)
		{
			audioListener = new GameObject().AddComponent<AudioListener>();
			audioListener.enabled = false;
		}
		MusicController musicController = UnityEngine.Object.FindObjectOfType<MusicController>();
		if (musicController != null)
		{
			musicController.enabled = false;
		}
		AudioListener.volume = 0f;
	}

	private void DisableInput()
	{
		InputManager instance = SingleInstance<InputManager>.Instance;
		instance.enabled = false;
	}

	private void DisablePlayerLabelManager()
	{
		PlayerLabelManager playerLabelManager = UnityEngine.Object.FindObjectOfType<PlayerLabelManager>();
		if (playerLabelManager != null)
		{
			playerLabelManager.enabled = false;
		}
	}

	private void DisableNonEssentials()
	{
		DisableInput();
		DisableHUD();
		DisablePlayerLabelManager();
		DisableCameras();
		DisableSound();
	}

	public void StartServer(DedicatedServerMode mode)
	{
		bool isSpectator = true;
		if (mode == DedicatedServerMode.NonDedicatedLanSpectator)
		{
			StatMaster.isHeadless = false;
			isSpectator = true;
			OptionsMaster.networkType = PlayerNetworkType.LAN;
		}
		else if (mode == DedicatedServerMode.NonDedicatedInternet || mode == DedicatedServerMode.NonDedicatedLan)
		{
			StatMaster.isHeadless = false;
			isSpectator = false;
			if (mode == DedicatedServerMode.NonDedicatedLan)
			{
				OptionsMaster.networkType = PlayerNetworkType.LAN;
			}
			else
			{
				OptionsMaster.networkType = PlayerNetworkType.Playfab;
			}
		}
		else
		{
			if (mode == DedicatedServerMode.Lan)
			{
				OptionsMaster.networkType = PlayerNetworkType.LAN;
			}
			StatMaster.isHeadless = true;
		}
		if (BesiegeLogFilter.logDebug)
		{
			Debug.LogFormat("Starting dedicated server with mode: {0}", mode);
		}
		StartHeadlessServer(isSpectator);
	}

	public void TestMachine(string machinePath, float duration, bool isHeadless, int numMachines)
	{
		testMachinePath = machinePath;
		testDuration = duration;
		numTestMachines = numMachines;
		StatMaster.isHeadless = isHeadless;
		if (!isHeadless)
		{
			Screen.fullScreen = false;
		}
		StartHeadlessServer(false);
	}

	public void OnLocalisationChange()
	{
		SetDefaultPlayerName();
	}
}
