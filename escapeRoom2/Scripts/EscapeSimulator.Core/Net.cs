using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ExitGames.Client.Photon;
using FMODUnity;
using Photon.Realtime;
using Photon.Voice;
using Photon.Voice.Unity;
using Steamworks;
using UnityEngine;
using UnityEngine.Networking;

public class Net
{
	private static Net currentSession;

	private static Net currentSessionSplitScreen;

	internal const string NativeLibraryName = "steam_api64";

	internal const string NativeLibrary_SDKEncryptedAppTicket = "sdkencryptedappticket64";

	public const string CUSTOM_LEVEL_KEY = "CustomLevel";

	public const string HOST_PLAYER_KEY = "HostPlayerId";

	public const string GAME_VERSION_KEY = "GameVersion";

	public const string CONNECTION_MODE_KEY = "ConnectionMode";

	public const string PLAYER_PLATFORM_KEY = "PlayerPlatform";

	public const string PLAYER_AVATAR_KEY = "PlayerAvatar";

	public const string GAME_DIFFICULTY_KEY = "GameDifficulty";

	public const string IS_RANDOM_LOBBY_KEY = "IsRandomLobby";

	public const string IS_DEVELOPER_LOBBY_KEY = "IsDeveloperLobby";

	public const string IS_VR_PLAYER_KEY = "IsVrPlayer";

	public const string IS_OCULUS_PLAYER_KEY = "IsOculusPlayer";

	public const string IS_SWITCH_PLAYER_KEY = "IsNintendoSwitchPlayer";

	public const string IS_STEAM_PLAYER_KEY = "IsSteamPlayer";

	public const string IS_DEMO_PLAYER_KEY = "IsDemoPlayer";

	public const string LOBBY_SERVER_URL = "https://escape-simulator-backend-yfw7w.ondigitalocean.app/";

	public static readonly string LOBBY_CREATE_PATH;

	public const string STEAM_LOBBY_PREFIX = "STEAM";

	public const string PHOTON_LOBBY_PREFIX = "PHOTON";

	public const string LOBBY_PREFIX_SEPARATOR = "::";

	public const int MAX_PLAYERS = 8;

	public const string AUTH_FAILED_CODE = "customAuthFailed";

	public const NetPlatform LOCAL_NET_PLATFORM = NetPlatform.Steam;

	public const string LEVEL_SYNC_TAG = "[Level-Sync]";

	private const string NET_LOG_FILE = "Net.log";

	private const string NET_PREV_LOG_FILE = "Net-prev.log";

	private const int VOICE_SAMPLE_RATE = 48000;

	private const int STEAM_VOICE_PACKET = 960;

	private const int MESSAGES_CHANNEL = 1;

	private const int VOICE_CHANNEL = 2;

	private static readonly HashSet<Type> LEVEL_SYNC_PACKET_TYPES;

	private static readonly HashSet<Type> ALWAYS_SEND_PACKET_TYPES;

	private static readonly HashSet<Type> DONT_RELAY_PACKET_TYPES;

	private static readonly HashSet<Type> CAN_HAVE_SESSION_MISMATCH;

	public static readonly List<Type> ALL_PACKET_TYPES;

	public static readonly HashSet<NetPlayerId> receivedBatchesPlayerIds;

	public static readonly HashSet<NetPlayerId> sentBatchesPlayerIds;

	public static readonly List<Batch> receivedBatches;

	public static readonly List<Batch> sentBatches;

	private readonly Queue<DelayedPacket> delayedPackets = new Queue<DelayedPacket>();

	public static float artificialDelay;

	public HashSet<Type> whitelist;

	public static readonly string[] botNames;

	public const int SPLITSCREEN_DISTANCE = 300;

	public static readonly NetPlayerId SPLITSCREEN_HOST_ID;

	public static readonly NetPlayerId SPLITSCREEN_CLIENT_ID;

	public static readonly NetPlayerId SINGLEPLAYER_ID;

	public readonly List<NetPlayerData> players = new List<NetPlayerData>();

	private readonly Queue<Packet> receivedPackets = new Queue<Packet>();

	private readonly Queue<NetVoice> receivedVoice = new Queue<NetVoice>();

	private readonly List<NetVoice> toSendVoice = new List<NetVoice>();

	private readonly Dictionary<NetPlayerId, List<Packet>> toSend = new Dictionary<NetPlayerId, List<Packet>>();

	private readonly Dictionary<NetPlayerId, List<Packet>> toSendUnreliable = new Dictionary<NetPlayerId, List<Packet>>();

	public NetPlayerId localPlayerId;

	public NetPlayerId hostPlayerId;

	public PhotonService photon;

	public bool pressedHost;

	public IMenuNetCallbacks menuCallbacks;

	public Game game;

	public ConnectionMode connectionMode;

	public NetMode netMode;

	public LobbyType lobbyType;

	public GameDifficulty gameDifficulty;

	public bool isRandomLobby;

	public string lobbyCode;

	public bool useVoice;

	private float countdownTillNewMicrophoneCheck;

	public bool isVR;

	public float lastLocalPlayerSpeakTime;

	private bool lastFrameCanRecordVoice;

	private int lobbyLastFramePlayers;

	private float timeToResendConnections;

	private float packetFailCooldown;

	private uint nextPacketId;

	private static int nextNetInstanceId;

	private int netInstanceId;

	private int _sessionId = 1;

	private Callback<LobbyCreated_t> callbackLobbyCreated;

	private Callback<LobbyEnter_t> callbackLobbyEnter;

	private Callback<LobbyChatUpdate_t> lobbyUserChanged;

	private Callback<LobbyDataUpdate_t> callbackLobbyDataUpdate;

	private Callback<AvatarImageLoaded_t> callbackAvatarLoaded;

	private Callback<GameRichPresenceJoinRequested_t> lobbyJoinRequest;

	private Callback<SteamNetworkingMessagesSessionRequest_t> sessionRequestMessages;

	private Callback<SteamNetworkingMessagesSessionFailed_t> sessionConnectFailMessages;

	private Callback<P2PSessionRequest_t> sessionRequestLegacy;

	private Callback<P2PSessionConnectFail_t> sessionConnectFailLegacy;

	private CSteamID steamEnteringLobbyId = CSteamID.Nil;

	public CSteamID currentLobbySteam = CSteamID.Nil;

	private readonly IntPtr[] messagesBuffer = new IntPtr[1000];

	private readonly byte[] voiceBuffer = new byte[50000];

	private byte[] voipBuffer = new byte[102400];

	private const int BUFFER_SIZE = 1048576;

	private readonly IntPtr sendBuffer = Marshal.AllocHGlobal(1048576);

	private readonly byte[] receiveBuffer = new byte[1048576];

	public int sessionId
	{
		get
		{
			return _sessionId;
		}
		set
		{
			log($"Setting session ID from {_sessionId} to {value}");
			_sessionId = value;
		}
	}

	public static Net getSession(GameObject sceneObject)
	{
		bool isSplitScreen = sceneObject != null && !UnityUtils.isObjectInMainOrTopSplitScreenScene(sceneObject);
		if (sceneObject != null && sceneObject.scene.name == "Lobby1")
		{
			isSplitScreen = false;
		}
		return getSession(isSplitScreen);
	}

	public static Net getSession(bool isSplitScreen = false)
	{
		if (!isSplitScreen)
		{
			return currentSession;
		}
		return currentSessionSplitScreen;
	}

	public static void setSession(Net newSession, GameObject sceneObject)
	{
		setSession(newSession, sceneObject != null && !UnityUtils.isObjectInMainOrTopSplitScreenScene(sceneObject));
	}

	public static void setSession(Net newSession, bool isSplitScreen = false)
	{
		if (isSplitScreen)
		{
			setSession(ref currentSessionSplitScreen);
		}
		else
		{
			setSession(ref currentSession);
		}
		void setSession(ref Net session)
		{
			if (session != null)
			{
				session.log(string.Format("[{0}.{1}] Destroying session (Instance: #{2}, ID: {3}, Is split-screen: {4})...", "Net", "setSession", session.netInstanceId, session.sessionId, isSplitScreen));
				session.destroy();
			}
			string text = ((session != null) ? string.Format("{0} #{1}", "Net", session.netInstanceId) : "null");
			string text2 = ((newSession != null) ? string.Format("{0} #{1}", "Net", newSession.netInstanceId) : "null");
			(newSession ?? session)?.log(string.Format("[{0}.{1}] Setting session from '{2}' to '{3}', Is split-screen: {4}", "Net", "setSession", text, text2, isSplitScreen));
			session = newSession;
		}
	}

	public static Net createSession(bool initWithVoice)
	{
		Net net = new Net(initWithVoice);
		net.log(string.Format("[{0}.{1}] Creating new session ({2}: {3})...", "Net", "createSession", "initWithVoice", initWithVoice));
		return net;
	}

	[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
	private static extern void SteamAPI_SteamNetworkingMessage_t_Release(IntPtr self);

	static Net()
	{
		LOBBY_CREATE_PATH = (Is.Editor ? "match/create_dev/" : "match/create/");
		LEVEL_SYNC_PACKET_TYPES = new HashSet<Type>
		{
			typeof(KeepAlivePacket),
			typeof(LevelSyncRequestPacket),
			typeof(LevelSyncResponsePacket),
			typeof(LevelSyncSceneLoadedPacket),
			typeof(LevelSyncSceneStatePacket),
			typeof(LevelSyncReadyPacket),
			typeof(CharacterCustomizationPacket),
			typeof(SceneCheckRequestPacket),
			typeof(SceneCheckResponsePacket)
		};
		ALWAYS_SEND_PACKET_TYPES = new HashSet<Type>
		{
			typeof(KeepAlivePacket),
			typeof(LevelSyncRequestPacket),
			typeof(LevelSyncResponsePacket),
			typeof(LevelSyncSceneLoadedPacket),
			typeof(LevelSyncSceneStatePacket),
			typeof(LevelSyncReadyPacket),
			typeof(CharacterCustomizationPacket),
			typeof(ReturnToMenuPacket),
			typeof(FailedLoadRoomEditorRoomPacket),
			typeof(KickedFromRandomLobbyPacket),
			typeof(KickPlayerPacket),
			typeof(SceneCheckRequestPacket),
			typeof(SceneCheckResponsePacket)
		};
		DONT_RELAY_PACKET_TYPES = new HashSet<Type>
		{
			typeof(ReturnToMenuPacket),
			typeof(SyncTimerTimePacket),
			typeof(FinishLevelPacket),
			typeof(HintRequestPacket),
			typeof(LookableChangedPacket),
			typeof(RequestAdvancedSearchPacket)
		};
		CAN_HAVE_SESSION_MISMATCH = new HashSet<Type>
		{
			typeof(KeepAlivePacket),
			typeof(LevelSyncRequestPacket),
			typeof(LevelSyncResponsePacket),
			typeof(LevelSyncSceneLoadedPacket),
			typeof(LevelSyncSceneStatePacket),
			typeof(LevelSyncReadyPacket),
			typeof(CharacterCustomizationPacket),
			typeof(SceneCheckRequestPacket),
			typeof(SceneCheckResponsePacket)
		};
		ALL_PACKET_TYPES = new List<Type>();
		receivedBatchesPlayerIds = new HashSet<NetPlayerId>();
		sentBatchesPlayerIds = new HashSet<NetPlayerId>();
		receivedBatches = new List<Batch>();
		sentBatches = new List<Batch>();
		botNames = new string[23]
		{
			"Antonio", "Barbir", "Davor", "Dora", "Filip", "Igor", "Ivan", "Jura", "Kis", "Konrad",
			"Kristijan", "Mario", "Mateja", "Matija", "MatijaS", "Matilda", "Paul", "Sasa", "Toni", "Tomislav",
			"Veronika", "Vinka", "Vladimir"
		};
		SPLITSCREEN_HOST_ID = new NetPlayerId("SplitScreenHost");
		SPLITSCREEN_CLIENT_ID = new NetPlayerId("SplitScreenClient");
		SINGLEPLAYER_ID = new NetPlayerId("SinglePlayer");
		for (byte b = 0; b < EscapeSimulatorCore.getPacketCount(); b++)
		{
			ALL_PACKET_TYPES.Add(EscapeSimulatorCore.getPacketType(b));
		}
	}

	private Net(bool initWithVoice)
	{
		bool flag = true;
		useVoice = initWithVoice && flag;
		isVR = Is.VR;
		netInstanceId = nextNetInstanceId++;
		log("Creating new Net session");
		callbackLobbyCreated = new Callback<LobbyCreated_t>(onSteamLobbyCreated);
		callbackLobbyEnter = new Callback<LobbyEnter_t>(onSteamLobbyEnter);
		callbackLobbyDataUpdate = new Callback<LobbyDataUpdate_t>(onSteamLobbyDataUpdate);
		lobbyUserChanged = new Callback<LobbyChatUpdate_t>(onLobbyUserChanged);
		callbackAvatarLoaded = new Callback<AvatarImageLoaded_t>(onAvatarLoaded);
		lobbyJoinRequest = new Callback<GameRichPresenceJoinRequested_t>(onLobbyJoinRequest);
		sessionRequestMessages = new Callback<SteamNetworkingMessagesSessionRequest_t>(onSessionRequestMessages);
		sessionConnectFailMessages = new Callback<SteamNetworkingMessagesSessionFailed_t>(onSessionConnectFailMessages);
		sessionRequestLegacy = new Callback<P2PSessionRequest_t>(onSessionRequestLegacy);
		sessionConnectFailLegacy = new Callback<P2PSessionConnectFail_t>(onSessionConnectFailLegacy);
	}

	private void initSteam()
	{
		if (!(localPlayerId != null))
		{
			localPlayerId = new NetPlayerId(SteamUser.GetSteamID().m_SteamID.ToString());
			log("inited net steam " + localPlayerId.value);
		}
	}

	private void initOffline()
	{
		if (!(localPlayerId != null))
		{
			localPlayerId = new NetPlayerId(Environment.MachineName);
			log("inited net offline " + localPlayerId.value);
		}
	}

	private void initPhoton()
	{
		photon?.destroy();
		photon = new PhotonService(this)
		{
			onLobbyEnterHost = onPhotonLobbyEnterHost,
			onConnectedToMainServer = onConnectedToMainServer,
			onMatchCreateFail = onPhotonMatchCreateFail,
			onLobbyEnterClient = onPhotonLobbyEnterClient,
			onMatchCreated = onPhotonMatchCreated,
			onLobbyFailEnterDescription = onPhotonLobbyFailEnterDescription,
			onLobbyFailEnter = onPhotonLobbyFailEnter,
			onMessage = onPhotonMessage,
			onSpeakerAdd = onPhotonSpeakerAdd,
			onPlayerDataChanged = onPhotonPlayerDataChanged,
			onLobbyChanged = onPhotonLobbyChanged,
			onPlayerLeft = onPhotonPlayerLeft,
			onLocalPlayerSpeaking = onPhotonLocalPlayerSpeaking,
			onAuthenticationFailed = onAuthenticationFailed,
			onTimeout = onTimeout
		};
		void onAuthenticationFailed()
		{
			log("Photon Authentication Failed");
			photon?.destroy();
			netMode = NetMode.None;
			menuCallbacks?.onMatchCreateFail("customAuthFailed", "");
		}
		void onConnectedToMainServer(string localPlayerId)
		{
			this.localPlayerId = new NetPlayerId(localPlayerId);
		}
		void onPhotonLobbyChanged()
		{
			updateLobbyPlayers();
		}
		void onPhotonLobbyEnterClient(string host)
		{
			log("onPhotonLobbyEnterClient " + photon.client);
			menuCallbacks?.onEnterLobby();
			hostPlayerId = new NetPlayerId(host);
			connectionMode = ConnectionMode.Photon;
			updateLobbyPlayers();
		}
		void onPhotonLobbyEnterHost()
		{
			log("onPhotonLobbyEnterHost " + photon.client);
			log("Photon host: " + photon.client.LocalPlayer.UserId);
			hostPlayerId = new NetPlayerId(photon.client.LocalPlayer.UserId);
			connectionMode = ConnectionMode.Photon;
			setNewRandomSessionId();
			updateLobbyPlayers();
			menuCallbacks?.lobbyReady();
		}
		void onPhotonLobbyFailEnter()
		{
			leaveLobby(finalUpdate: false);
			menuCallbacks?.onCantEnterLobby();
		}
		void onPhotonLobbyFailEnterDescription(string code, string message)
		{
			menuCallbacks?.onCantEnterDetailsLobby(code, message);
		}
		void onPhotonLocalPlayerSpeaking()
		{
			lastLocalPlayerSpeakTime = Time.time;
		}
		void onPhotonMatchCreateFail(string code, string message)
		{
			menuCallbacks?.onMatchCreateFail(code, message);
		}
		void onPhotonMatchCreated(string lobbyCode)
		{
			this.lobbyCode = lobbyCode;
			menuCallbacks?.onLobbyCodeChanged(lobbyCode);
		}
		void onPhotonMessage(byte[] batchBuffer, int batchSizeInBytes, NetPlayerId sender)
		{
			decodeAndEnqueueBatch(batchBuffer, batchSizeInBytes, sender);
		}
		void onPhotonPlayerDataChanged(string playerId)
		{
			updatePlayer(new NetPlayerId(playerId));
		}
		void onPhotonPlayerLeft(string playerId)
		{
			game?.removePlayerFromGame(new NetPlayerId(playerId));
		}
		void onPhotonSpeakerAdd(Speaker speaker, string owner)
		{
			NetPlayerData netPlayerData = players.Find((NetPlayerData x) => x.id.value == owner);
			if (netPlayerData != null)
			{
				UnityEngine.Object.Destroy(netPlayerData.photonSpeaker.gameObject);
			}
			netPlayerData.photonSpeaker = speaker;
		}
		void onTimeout()
		{
			game?.onTimeout();
		}
	}

	private SteamNetworkingIdentity convertPlayerIdToSteamIdentity(NetPlayerId playerId)
	{
		SteamNetworkingIdentity result = default(SteamNetworkingIdentity);
		result.SetSteamID(convertPlayerIdToSteamId(playerId));
		result.m_eType = ESteamNetworkingIdentityType.k_ESteamNetworkingIdentityType_SteamID;
		return result;
	}

	public SteamNetworkingIdentity getLocalPlayerSteamIdentity()
	{
		SteamNetworkingIdentity result = default(SteamNetworkingIdentity);
		result.SetSteamID(SteamUser.GetSteamID());
		result.m_eType = ESteamNetworkingIdentityType.k_ESteamNetworkingIdentityType_SteamID;
		return result;
	}

	private CSteamID convertPlayerIdToSteamId(NetPlayerId playerId)
	{
		if (!ulong.TryParse(playerId.value, out var result))
		{
			log($"Cannot convert '{playerId}' to Steam ID!", isError: true);
			return CSteamID.Nil;
		}
		return (CSteamID)result;
	}

	public void destroy()
	{
		leaveLobby();
		callbackLobbyCreated.Dispose();
		callbackLobbyEnter.Dispose();
		callbackLobbyDataUpdate.Dispose();
		lobbyUserChanged.Dispose();
		callbackAvatarLoaded.Dispose();
		lobbyJoinRequest.Dispose();
		sessionRequestMessages.Dispose();
		sessionConnectFailMessages.Dispose();
		sessionRequestLegacy.Dispose();
		sessionConnectFailLegacy.Dispose();
		if (lobbyType == LobbyType.PhotonPine)
		{
			photon.destroy();
		}
		menuCallbacks = null;
		game = null;
		foreach (NetPlayerData player in players)
		{
			if (player.photonSpeaker != null)
			{
				UnityEngine.Object.Destroy(player.photonSpeaker.gameObject);
			}
		}
	}

	public void startClient(string lobbyCode)
	{
		netMode = NetMode.Client;
		this.lobbyCode = lobbyCode;
		Executor.executeCoroutine(startConnectingToLobby());
	}

	private IEnumerator startConnectingToLobby()
	{
		using UnityWebRequest webRequest = UnityWebRequest.Get("https://escape-simulator-backend-yfw7w.ondigitalocean.app/match/join/" + lobbyCode);
		log("joinPineMatch request sent");
		yield return webRequest.SendWebRequest();
		string text = webRequest.downloadHandler.text;
		log($"joinPineMatch response received: {webRequest.result} ({text}) ({lobbyCode})");
		if (webRequest.result != UnityWebRequest.Result.Success)
		{
			menuCallbacks?.onCantEnterDetailsLobby(webRequest.responseCode.ToString(), webRequest.error);
			yield break;
		}
		if (text == "FAILED" || !text.Contains("::"))
		{
			menuCallbacks?.onFailedToFindLobby(text);
			yield break;
		}
		string[] array = text.Split("::");
		string text2 = array[0];
		if (text2 == "PHOTON")
		{
			lobbyType = LobbyType.PhotonPine;
			initPhoton();
			log("Starting Photon client with lobby code: " + lobbyCode + " and region: " + array[1]);
			photon.startClient(lobbyCode, array[1]);
		}
		else if (text2 == "STEAM")
		{
			ulong result;
			if (!Is.Steam)
			{
				log("Cannot enter Steam lobby from cross-platform build.");
				menuCallbacks?.onCantEnterSteamLobbyFromCrossplatform();
			}
			else if (ulong.TryParse(array[1], out result))
			{
				lobbyType = LobbyType.SteamPine;
				initSteam();
				startSteamLobbyEntering(result);
			}
			else
			{
				log("Could not convert '" + array[1] + "' to Steam ID.", isError: true);
				menuCallbacks?.onFailedToFindLobby(text);
			}
		}
		else
		{
			log("Received invalid protocol from server: " + text2, isError: true);
		}
	}

	public void startClientSteamInvite(string lobbyId)
	{
		initSteam();
		netMode = NetMode.Client;
		lobbyType = LobbyType.SteamPine;
		startSteamLobbyEntering(ulong.Parse(lobbyId));
	}

	private void startSteamLobbyEntering(ulong lobbyId)
	{
		log(string.Format("{0}: {1}", "startSteamLobbyEntering", lobbyId));
		steamEnteringLobbyId = (CSteamID)lobbyId;
		SteamMatchmaking.RequestLobbyData(steamEnteringLobbyId);
	}

	private void onSteamLobbyDataUpdate(LobbyDataUpdate_t data)
	{
		CSteamID cSteamID = new CSteamID(data.m_ulSteamIDLobby);
		gameDifficulty = UnityUtils.tryParseEnum<GameDifficulty>(SteamMatchmaking.GetLobbyData(cSteamID, "GameDifficulty"));
		string lobbyData = SteamMatchmaking.GetLobbyData(cSteamID, "IsRandomLobby");
		if (!bool.TryParse(lobbyData, out isRandomLobby))
		{
			log("Could not parse boolean value from string '" + lobbyData + "'.", isError: true);
		}
		if (steamEnteringLobbyId != CSteamID.Nil)
		{
			string lobbyData2 = SteamMatchmaking.GetLobbyData(cSteamID, "GameVersion");
			if ((!string.IsNullOrEmpty(lobbyData2) && Version.isBackwardsCompatibleWithVersion(lobbyData2)) || Debug.isDebugBuild)
			{
				log($"Joining lobby with ID {cSteamID}...");
				SteamMatchmaking.JoinLobby(steamEnteringLobbyId);
			}
			else
			{
				menuCallbacks?.onCantEnterLobby();
			}
			steamEnteringLobbyId = CSteamID.Nil;
		}
		else if (data.m_ulSteamIDMember != data.m_ulSteamIDLobby)
		{
			updatePlayer(new NetPlayerId(data.m_ulSteamIDMember.ToString()));
		}
	}

	private void onSteamLobbyEnter(LobbyEnter_t lobbyEnterData)
	{
		if (lobbyType != LobbyType.SteamPine || netMode == NetMode.None)
		{
			SteamMatchmaking.LeaveLobby(new CSteamID(lobbyEnterData.m_ulSteamIDLobby));
			log("LEAVE LOBBY " + lobbyType.ToString() + " " + netMode);
			return;
		}
		if (lobbyEnterData.m_EChatRoomEnterResponse != 1)
		{
			log("ERROR Steam lobby join failed because: " + lobbyEnterData.m_EChatRoomEnterResponse);
			handleLeaveLobby();
			return;
		}
		log("onSteamLobbyEnter " + lobbyEnterData.m_ulSteamIDLobby);
		log("NetMode " + netMode);
		currentLobbySteam = new CSteamID(lobbyEnterData.m_ulSteamIDLobby);
		SteamMatchmaking.SetLobbyMemberData(currentLobbySteam, "IsVrPlayer", isVR.ToString());
		SteamMatchmaking.SetLobbyMemberData(currentLobbySteam, "IsDemoPlayer", Is.Demo.ToString());
		if (netMode == NetMode.Host)
		{
			hostPlayerId = localPlayerId;
		}
		else
		{
			hostPlayerId = new NetPlayerId(SteamMatchmaking.GetLobbyOwner(currentLobbySteam).m_SteamID.ToString());
			if (hostPlayerId == localPlayerId && netMode == NetMode.Client)
			{
				log("ERROR Cannot be ES Net client and host of Steam lobby at the same time.");
				handleLeaveLobby();
			}
			if (int.TryParse(SteamMatchmaking.GetLobbyData(currentLobbySteam, "ConnectionMode"), out var result))
			{
				connectionMode = (ConnectionMode)result;
				menuCallbacks?.onEnterLobby();
				game?.onEnterLobby();
			}
			else
			{
				handleLeaveLobby();
			}
		}
		updateLobbyPlayers();
		void handleLeaveLobby()
		{
			leaveLobby(finalUpdate: false);
			menuCallbacks?.onCantEnterLobby();
		}
	}

	public void startHost(LobbyType lobbyType, ConnectionMode connectionMode, GameDifficulty gameDifficulty = GameDifficulty.Normal, bool isRandomLobby = false)
	{
		netMode = NetMode.Host;
		this.lobbyType = lobbyType;
		this.connectionMode = connectionMode;
		this.gameDifficulty = gameDifficulty;
		this.isRandomLobby = isRandomLobby;
		log($"Creating lobby: {lobbyType} has internet: {Application.internetReachability}");
		if (lobbyType == LobbyType.Offline)
		{
			initOffline();
			players.Clear();
			addPlayer(localPlayerId);
			hostPlayerId = localPlayerId;
			return;
		}
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			initSteam();
			players.Clear();
			addPlayer(localPlayerId);
			hostPlayerId = localPlayerId;
			return;
		}
		switch (lobbyType)
		{
		case LobbyType.SteamPine:
			initSteam();
			SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, 8);
			break;
		case LobbyType.PhotonPine:
			initPhoton();
			photon.startHost();
			break;
		}
	}

	private void onSteamLobbyCreated(LobbyCreated_t lobbyData)
	{
		if (lobbyData.m_eResult == EResult.k_EResultOK)
		{
			initSteam();
			setNewRandomSessionId();
			log($"SESSION ID LOBBY CREATED {sessionId}");
			log(string.Format("{0}: {1}", "onSteamLobbyCreated", lobbyData.m_ulSteamIDLobby));
			if (!SteamMatchmaking.SetLobbyData(new CSteamID(lobbyData.m_ulSteamIDLobby), "HostPlayerId", localPlayerId.value))
			{
				log("cannot set lobby data HostPlayerId " + localPlayerId.value, isError: true);
			}
			if (!SteamMatchmaking.SetLobbyData(new CSteamID(lobbyData.m_ulSteamIDLobby), "GameVersion", Version.baseGameVersion))
			{
				log("cannot set lobby data GameVersion " + Version.baseGameVersion, isError: true);
			}
			CSteamID steamIDLobby = new CSteamID(lobbyData.m_ulSteamIDLobby);
			int num = (int)connectionMode;
			if (!SteamMatchmaking.SetLobbyData(steamIDLobby, "ConnectionMode", num.ToString()))
			{
				log(string.Format("cannot set lobby data {0} {1}", "ConnectionMode", (int)connectionMode), isError: true);
			}
			if (!SteamMatchmaking.SetLobbyData(new CSteamID(lobbyData.m_ulSteamIDLobby), "GameDifficulty", gameDifficulty.ToString()))
			{
				log(string.Format("cannot set lobby data {0} {1}", "GameDifficulty", gameDifficulty), isError: true);
			}
			if (!SteamMatchmaking.SetLobbyData(new CSteamID(lobbyData.m_ulSteamIDLobby), "IsRandomLobby", isRandomLobby.ToString().ToLower()))
			{
				log(string.Format("cannot set lobby data {0} {1}", "IsRandomLobby", isRandomLobby), isError: true);
			}
			Executor.executeCoroutine(createPineLobby(lobbyData.m_ulSteamIDLobby));
		}
		else if (lobbyData.m_eResult == EResult.k_EResultNoConnection && GameStarter.isWaitingForSession)
		{
			Net net = createSession(initWithVoice: false);
			setSession(net);
			Debug.LogError("Could not create Steam lobby, starting offline session instead...");
			net.startHost(LobbyType.Offline, ConnectionMode.Offline);
		}
		else
		{
			menuCallbacks?.onMatchCreateFail("007", $"Failed to create steam lobby: {lobbyData.m_eResult}");
		}
	}

	private IEnumerator createPineLobby(ulong steamLobbyId)
	{
		menuCallbacks?.onLobbyCodeChanged("");
		menuCallbacks?.lobbyReady();
		yield return tryToAcquireLobbyCode(string.Format("{0}{1}{2}{3}{4}", "https://escape-simulator-backend-yfw7w.ondigitalocean.app/", LOBBY_CREATE_PATH, "STEAM", "::", steamLobbyId));
	}

	private IEnumerator tryToAcquireLobbyCode(string uri, int retryCount = 1)
	{
		using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
		webRequest.timeout = 30;
		webRequest.downloadHandler = new DownloadHandlerBuffer();
		log("createPineMatch request sent");
		yield return webRequest.SendWebRequest();
		log("createPineMatch response received");
		if (webRequest.result == UnityWebRequest.Result.Success)
		{
			lobbyCode = webRequest.downloadHandler.text;
			log("created pine match " + lobbyCode);
			menuCallbacks?.onLobbyCodeChanged(lobbyCode);
			yield break;
		}
		log($"Unable to access Lobby Code server, attempts: {retryCount}/{3}");
		if (retryCount < 3)
		{
			yield return tryToAcquireLobbyCode(uri, retryCount + 1);
		}
	}

	private void onSessionRequestMessages(SteamNetworkingMessagesSessionRequest_t sessionRequestData)
	{
		log("onSessionRequestMessages " + sessionRequestData.m_identityRemote.GetSteamID().ToString());
		log("acceptSuccess: " + SteamNetworkingMessages.AcceptSessionWithUser(ref sessionRequestData.m_identityRemote));
	}

	private void onSessionConnectFailMessages(SteamNetworkingMessagesSessionFailed_t data)
	{
		log("onSessionConnectFailMessages", isError: true);
		log("SteamNetworkingUtils.GetRelayNetworkStatus: " + SteamNetworkingUtils.GetRelayNetworkStatus(out var pDetails));
		log("state.m_eAvailAnyRelay: " + pDetails.m_eAvailAnyRelay);
		log("m_eAvailNetworkConfig: " + pDetails.m_eAvailNetworkConfig);
		log("m_debugMsg: " + pDetails.m_debugMsg);
		log("Session connect failed: " + (ESteamNetConnectionEnd)data.m_info.m_eEndReason/*cast due to .constrained prefix*/);
		log("m_identityRemote:" + data.m_info.m_identityRemote.GetSteamID().ToString());
		log("m_nUserData:" + data.m_info.m_nUserData);
		log("m_addrRemote:" + data.m_info.m_addrRemote);
		log("m_idPOPRemote:" + data.m_info.m_idPOPRemote.ToString());
		log("m_idPOPRelay:" + data.m_info.m_idPOPRelay.ToString());
		log("m_eState:" + data.m_info.m_eState);
		log("m_szEndDebug:" + data.m_info.m_szEndDebug);
		log("m_szConnectionDescription:" + data.m_info.m_szConnectionDescription);
	}

	private void onSessionRequestLegacy(P2PSessionRequest_t sessionRequestData)
	{
		log("onSessionRequestLegacy " + sessionRequestData.m_steamIDRemote.m_SteamID);
		log("acceptSuccess: " + SteamNetworking.AcceptP2PSessionWithUser(sessionRequestData.m_steamIDRemote));
	}

	private void onSessionConnectFailLegacy(P2PSessionConnectFail_t data)
	{
		log("onSessionConnectFailLegacy", isError: true);
		log("SteamNetworkingUtils.GetRelayNetworkStatus: " + SteamNetworkingUtils.GetRelayNetworkStatus(out var pDetails));
		log("state.m_eAvailAnyRelay: " + pDetails.m_eAvailAnyRelay);
		log("m_eAvailNetworkConfig: " + pDetails.m_eAvailNetworkConfig);
		log("m_debugMsg: " + pDetails.m_debugMsg);
		EP2PSessionError eP2PSessionError = (EP2PSessionError)data.m_eP2PSessionError;
		log("data.m_eP2PSessionError: " + eP2PSessionError);
	}

	private void onLobbyUserChanged(LobbyChatUpdate_t data)
	{
		log("OnLobbyChat " + data.m_ulSteamIDLobby + " " + data.m_ulSteamIDUserChanged + " " + data.m_rgfChatMemberStateChange);
		if (lobbyType == LobbyType.SteamPine)
		{
			updateLobbyPlayers();
		}
	}

	private void onAvatarLoaded(AvatarImageLoaded_t data)
	{
		updatePlayer(new NetPlayerId(data.m_steamID.m_SteamID.ToString()));
		if (lobbyType == LobbyType.PhotonPine && data.m_steamID == SteamUser.GetSteamID())
		{
			ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable { 
			{
				"PlayerAvatar",
				getSteamAvatar(SteamUser.GetSteamID()).serialize()
			} };
			photon.client.LocalPlayer.SetCustomProperties(propertiesToSet);
		}
	}

	private void onLobbyJoinRequest(GameRichPresenceJoinRequested_t data)
	{
		menuCallbacks?.onGotLobbyInvitation(data.m_rgchConnect);
	}

	private void updateLobbyPlayers()
	{
		List<NetPlayerId> list = new List<NetPlayerId>();
		List<NetPlayerId> list2 = new List<NetPlayerId>();
		foreach (NetPlayerData player in players)
		{
			list2.Add(player.id);
		}
		if (lobbyType == LobbyType.SteamPine)
		{
			lobbyLastFramePlayers = SteamMatchmaking.GetNumLobbyMembers(currentLobbySteam);
			for (int i = 0; i < lobbyLastFramePlayers; i++)
			{
				list.Add(new NetPlayerId(SteamMatchmaking.GetLobbyMemberByIndex(currentLobbySteam, i).m_SteamID.ToString()));
			}
		}
		else if (lobbyType == LobbyType.PhotonPine && photon.client.InRoom)
		{
			lobbyLastFramePlayers = photon.client.CurrentRoom.PlayerCount;
			foreach (KeyValuePair<int, Player> player2 in photon.client.CurrentRoom.Players)
			{
				list.Add(new NetPlayerId(player2.Value.UserId));
			}
		}
		foreach (NetPlayerId item in list)
		{
			if (!list2.Contains(item))
			{
				addPlayer(item);
				game?.addPlayerToGame(item);
			}
		}
		foreach (NetPlayerId item2 in list2)
		{
			if (!list.Contains(item2))
			{
				game?.removePlayerFromGame(item2);
				removePlayer(item2);
			}
		}
	}

	private void setNewRandomSessionId()
	{
		sessionId = UnityEngine.Random.Range(int.MinValue, 2147483347);
	}

	public void leaveLobby(bool finalUpdate = true)
	{
		log("leave lobby");
		if (finalUpdate)
		{
			update();
		}
		if (lobbyType == LobbyType.SteamPine)
		{
			foreach (NetPlayerData player in players)
			{
				closeSteamConnectionWith(player.id);
			}
			if (currentLobbySteam != CSteamID.Nil)
			{
				SteamMatchmaking.LeaveLobby(currentLobbySteam);
			}
		}
		else if (lobbyType == LobbyType.PhotonPine && photon.client.InRoom)
		{
			photon.client.OpLeaveRoom(becomeInactive: false);
		}
		netMode = NetMode.None;
	}

	public bool isPlayerInLobby(NetPlayerId playerId)
	{
		foreach (NetPlayerData player in players)
		{
			if (player.id == playerId)
			{
				return true;
			}
		}
		return false;
	}

	public bool isHostInLobby()
	{
		bool result = false;
		if (lobbyType == LobbyType.SteamPine)
		{
			string lobbyData = SteamMatchmaking.GetLobbyData(currentLobbySteam, "HostPlayerId");
			for (int i = 0; i < SteamMatchmaking.GetNumLobbyMembers(currentLobbySteam); i++)
			{
				if (SteamMatchmaking.GetLobbyMemberByIndex(currentLobbySteam, i).m_SteamID.ToString() == lobbyData)
				{
					result = true;
					break;
				}
			}
		}
		if (lobbyType == LobbyType.PhotonPine)
		{
			if (!photon.client.InRoom)
			{
				return photon.client.State != ClientState.Joined;
			}
			string text = photon.client.CurrentRoom.CustomProperties["HostPlayerId"].ToString();
			foreach (KeyValuePair<int, Player> player in photon.client.CurrentRoom.Players)
			{
				if (player.Value.UserId == text)
				{
					result = true;
					break;
				}
			}
		}
		if (connectionMode == ConnectionMode.Splitscreen)
		{
			result = true;
		}
		return result;
	}

	public void lockLobby()
	{
		setLobbyLockedState(isLocked: true);
	}

	public void unlockLobby()
	{
		setLobbyLockedState(isLocked: false);
	}

	private void setLobbyLockedState(bool isLocked)
	{
		if (netMode == NetMode.Host)
		{
			if (lobbyType == LobbyType.SteamPine)
			{
				SteamMatchmaking.SetLobbyJoinable(currentLobbySteam, !isLocked);
			}
			else
			{
				log(string.Format("{0} not implemented for lobby type '{1}'", "setLobbyLockedState", lobbyType));
			}
		}
	}

	private byte[] encodeBatch(List<Packet> packets)
	{
		List<Packet> list = new List<Packet>(packets.Count);
		foreach (Packet packet2 in packets)
		{
			Packet packet = packet2;
			if (packet.GetType() != EscapeSimulatorCore.getPacketType(packet.getTypeId()))
			{
				packet = LevelLogicPacket.fromConcretePacket(packet);
			}
			list.Add(packet);
		}
		FastBinaryWriter writer;
		using (SharedWriter.borrow(out writer))
		{
			writer.Write<ushort>((ushort)packets.Count, default(FastBinaryWriter.ForPrimitives));
			foreach (Packet item in list)
			{
				item.write(writer);
			}
			return writer.ToArray();
		}
	}

	private List<Packet> decodeBatch(byte[] batch, NetPlayerId sender)
	{
		if (sender == localPlayerId)
		{
			log("Received a batch with ID set to local player!", isError: true);
			return null;
		}
		if (!isPlayerInLobby(sender))
		{
			log($"Received a batch from ID '{sender}' which is not in the lobby!", isError: true);
			return null;
		}
		using FastBinaryReader reader = new FastBinaryReader(batch);
		ushort num = reader.ReadUInt16();
		List<Packet> list = new List<Packet>(num);
		for (int i = 0; i < num; i++)
		{
			Packet packet = EscapeSimulatorCore.getPacket(reader.ReadByte());
			packet.read(reader);
			list.Add(packet);
		}
		return list;
	}

	private byte[] encodeVoice(List<NetVoice> voices)
	{
		FastBinaryWriter writer;
		using (SharedWriter.borrow(out writer))
		{
			writer.WriteNetPlayerId(localPlayerId);
			writer.Write<int>(voices.Count, default(FastBinaryWriter.ForPrimitives));
			foreach (NetVoice voice in voices)
			{
				writer.WriteByteArray(voice.voiceData);
			}
			return writer.ToArray();
		}
	}

	private NetVoice[] decodeVoice(byte[] bytes)
	{
		using FastBinaryReader reader = new FastBinaryReader(bytes);
		NetPlayerId netPlayerId = reader.ReadNetPlayerId();
		if (!isPlayerInLobby(netPlayerId))
		{
			log($"[{connectionMode}] Got voice from ID '{netPlayerId}' which is not in the lobby!", isError: true);
			return Array.Empty<NetVoice>();
		}
		int num = reader.ReadInt32();
		NetVoice[] array = new NetVoice[num];
		for (int i = 0; i < num; i++)
		{
			byte[] voiceData = reader.ReadByteArray();
			array[i] = new NetVoice
			{
				sender = netPlayerId,
				voiceData = voiceData
			};
		}
		return array;
	}

	private void decompressNetVoice(NetVoice netVoice)
	{
		uint nBytesWritten;
		EVoiceResult eVoiceResult = SteamUser.DecompressVoice(netVoice.voiceData, (uint)netVoice.voiceData.Length, voipBuffer, (uint)voipBuffer.Length, out nBytesWritten, 48000u);
		if (eVoiceResult == EVoiceResult.k_EVoiceResultBufferTooSmall)
		{
			voipBuffer = new byte[nBytesWritten];
			eVoiceResult = SteamUser.DecompressVoice(netVoice.voiceData, (uint)netVoice.voiceData.Length, voipBuffer, (uint)voipBuffer.Length, out nBytesWritten, 48000u);
		}
		if (eVoiceResult == EVoiceResult.k_EVoiceResultOK)
		{
			NetPlayerData netPlayerData = players.Find((NetPlayerData x) => x.id == netVoice.sender);
			if (netPlayerData.steamSpeaker != null)
			{
				netVoice.voiceData = new byte[960];
				uint num = nBytesWritten / 960;
				for (int num2 = 0; num2 < num; num2++)
				{
					Array.Copy(voipBuffer, num2 * 960, netVoice.voiceData, 0, 960);
					netPlayerData.steamSpeaker.Push(netVoice.voiceData);
				}
			}
		}
		else
		{
			netVoice.voiceData = Array.Empty<byte>();
		}
	}

	public void update(bool canRecordVoice = false)
	{
		foreach (NetPlayerData player in players)
		{
			player.steamSpeaker?.Service();
		}
		if (lobbyType == LobbyType.SteamPine)
		{
			if (currentLobbySteam != CSteamID.Nil && lobbyLastFramePlayers != SteamMatchmaking.GetNumLobbyMembers(currentLobbySteam))
			{
				updateLobbyPlayers();
			}
		}
		else if (lobbyType == LobbyType.PhotonPine)
		{
			if (photon.voice != null && photon.voice.voiceRecorder != null)
			{
				countdownTillNewMicrophoneCheck -= Time.deltaTime;
				if (countdownTillNewMicrophoneCheck <= 0f)
				{
					countdownTillNewMicrophoneCheck = 2f;
					if (photon.voice.voiceRecorder.MicrophoneType == Recorder.MicType.Photon && photon.voice.voiceRecorder.voice == LocalVoiceAudioDummy.Dummy)
					{
						photon.voice.voiceRecorder.RestartRecording(force: true);
						photon.voice.voiceRecorder.MicrophonesEnumerator.Refresh();
						foreach (DeviceInfo item3 in photon.voice.voiceRecorder.MicrophonesEnumerator)
						{
							if (!item3.IsDefault)
							{
								log("Changing microphone device to " + item3.Name);
								photon.voice.voiceRecorder.MicrophoneDevice = item3;
							}
							else
							{
								log("NOT Changing microphone device to " + item3.Name);
							}
						}
					}
					else if (photon.voice.voiceRecorder.MicrophoneType == Recorder.MicType.Unity)
					{
						string[] devices = Microphone.devices;
						log(string.Format("Unity microphones ({0}): {1}", devices.Length, string.Join(", ", devices)));
					}
				}
				if (photon.voice.voiceRecorder.RequiresRestart)
				{
					photon.voice.voiceRecorder.RestartRecording();
				}
			}
			photon.updatePhoton();
		}
		if (hostPlayerId != null)
		{
			if (connectionMode == ConnectionMode.SteamMessages)
			{
				int num = SteamNetworkingMessages.ReceiveMessagesOnChannel(1, messagesBuffer, messagesBuffer.Length);
				for (int i = 0; i < num; i++)
				{
					SteamNetworkingMessage_t steamNetworkingMessage_t = Marshal.PtrToStructure<SteamNetworkingMessage_t>(messagesBuffer[i]);
					Marshal.Copy(steamNetworkingMessage_t.m_pData, receiveBuffer, 0, steamNetworkingMessage_t.m_cbSize);
					decodeAndEnqueueBatch(receiveBuffer, steamNetworkingMessage_t.m_cbSize, new NetPlayerId(steamNetworkingMessage_t.m_identityPeer.GetSteamID64().ToString()));
					SteamAPI_SteamNetworkingMessage_t_Release(messagesBuffer[i]);
				}
				int num2 = SteamNetworkingMessages.ReceiveMessagesOnChannel(2, messagesBuffer, messagesBuffer.Length);
				for (int j = 0; j < num2; j++)
				{
					SteamNetworkingMessage_t steamNetworkingMessage_t2 = Marshal.PtrToStructure<SteamNetworkingMessage_t>(messagesBuffer[j]);
					byte[] array = new byte[steamNetworkingMessage_t2.m_cbSize];
					Marshal.Copy(steamNetworkingMessage_t2.m_pData, array, 0, steamNetworkingMessage_t2.m_cbSize);
					NetVoice[] array2 = decodeVoice(array);
					foreach (NetVoice item in array2)
					{
						receivedVoice.Enqueue(item);
					}
					SteamAPI_SteamNetworkingMessage_t_Release(messagesBuffer[j]);
				}
			}
			else if (connectionMode == ConnectionMode.SteamLegacy)
			{
				uint pcubMsgSize;
				uint pcubMsgSize2;
				while (SteamNetworking.IsP2PPacketAvailable(out pcubMsgSize, 1))
				{
					SteamNetworking.ReadP2PPacket(receiveBuffer, pcubMsgSize, out pcubMsgSize2, out var psteamIDRemote, 1);
					decodeAndEnqueueBatch(receiveBuffer, (int)pcubMsgSize, new NetPlayerId(psteamIDRemote.m_SteamID.ToString()));
				}
				uint pcubMsgSize3;
				while (SteamNetworking.IsP2PPacketAvailable(out pcubMsgSize3, 2))
				{
					byte[] array3 = new byte[pcubMsgSize3];
					SteamNetworking.ReadP2PPacket(array3, pcubMsgSize3, out pcubMsgSize2, out var _, 2);
					NetVoice[] array2 = decodeVoice(array3);
					foreach (NetVoice item2 in array2)
					{
						receivedVoice.Enqueue(item2);
					}
				}
			}
		}
		bool flag = true;
		if (packetFailCooldown > 0f)
		{
			packetFailCooldown -= Time.deltaTime;
			flag = false;
		}
		if (flag)
		{
			sendPacketsInternal(toSend, reliable: true);
			sendPacketsInternal(toSendUnreliable, reliable: false);
		}
		updateVoiceToSend(canRecordVoice);
		sendSteamVoiceInternal();
		while (receivedPackets.Count > 0)
		{
			Packet packet = receivedPackets.Dequeue();
			if (sessionId != packet.sessionId && isHost(packet.senderId))
			{
				log("Host sent a message with different sessionId. Setting local sessionId to received sessionId. Host always has latest sessionId.");
				sessionId = packet.sessionId;
			}
			if (sessionId != packet.sessionId && !CAN_HAVE_SESSION_MISMATCH.Contains(packet.GetType()))
			{
				log($"Session ID mismatch for '{packet.GetType()}' (local session ID: {sessionId} | message session ID: {packet.sessionId})", isError: true);
				continue;
			}
			menuCallbacks?.onLobbyPacket(packet);
			game?.onGamePacket(packet);
		}
		if (useVoice)
		{
			while (receivedVoice.Count > 0)
			{
				NetVoice netVoice = receivedVoice.Dequeue();
				decompressNetVoice(netVoice);
			}
		}
	}

	private void decodeAndEnqueueBatch(byte[] batchBuffer, int batchSizeInBytes, NetPlayerId sender)
	{
		List<Packet> list = decodeBatch(batchBuffer, sender);
		if (list == null)
		{
			return;
		}
		foreach (Packet item in list)
		{
			enqueueIncomingPacket(item, sender);
		}
	}

	private void updateVoiceToSend(bool canRecordVoice)
	{
		if (!useVoice)
		{
			lastFrameCanRecordVoice = false;
			canRecordVoice = false;
		}
		if (connectionMode == ConnectionMode.Photon)
		{
			if (photon.voice != null && photon.voice.voiceRecorder != null)
			{
				photon.voice.voiceRecorder.TransmitEnabled = canRecordVoice;
			}
		}
		else
		{
			if (lastFrameCanRecordVoice != canRecordVoice)
			{
				if (canRecordVoice)
				{
					SteamUser.StartVoiceRecording();
				}
				else
				{
					SteamUser.StopVoiceRecording();
				}
			}
			if (canRecordVoice && currentLobbySteam != CSteamID.Nil && SteamUser.GetAvailableVoice(out var _) == EVoiceResult.k_EVoiceResultOK && SteamUser.GetVoice(bWantCompressed: true, voiceBuffer, (uint)voiceBuffer.Length, out var nBytesWritten) == EVoiceResult.k_EVoiceResultOK && nBytesWritten != 0)
			{
				byte[] array = new byte[nBytesWritten];
				Array.Copy(voiceBuffer, array, nBytesWritten);
				toSendVoice.Add(new NetVoice
				{
					voiceData = array
				});
			}
		}
		lastFrameCanRecordVoice = canRecordVoice;
	}

	private void sendPacketsInternal(Dictionary<NetPlayerId, List<Packet>> packetsToSend, bool reliable)
	{
		foreach (var (netPlayerId2, list2) in packetsToSend)
		{
			if (list2.Count != 0)
			{
				byte[] array = encodeBatch(list2);
				if (connectionMode switch
				{
					ConnectionMode.SteamMessages => sendBatchViaSteamMessages(netPlayerId2, array, reliable), 
					ConnectionMode.SteamLegacy => sendBatchViaSteamLegacy(netPlayerId2, array, reliable), 
					ConnectionMode.Photon => sendBatchViaPhoton(netPlayerId2, array, reliable), 
					ConnectionMode.Splitscreen => sendBatchViaSplitScreen(array), 
					_ => false, 
				})
				{
					list2.Clear();
					continue;
				}
				packetFailCooldown = 0.2f;
				log(string.Format("[{0}] Failed to send batch ({1} bytes) to player '{2}':\n\n{3}", connectionMode, array.Length, netPlayerId2, string.Join("\n\n\n", list2)), isError: true);
			}
		}
		bool sendBatchViaPhoton(NetPlayerId receiver, byte[] batch, bool reliable2)
		{
			return photon.sendData(batch, receiver, reliable2);
		}
		bool sendBatchViaSplitScreen(byte[] batch)
		{
			Net session = getSession();
			Net session2 = getSession(isSplitScreen: true);
			((localPlayerId == session.localPlayerId) ? session2 : session).decodeAndEnqueueBatch(batch, batch.Length, localPlayerId);
			return true;
		}
		bool sendBatchViaSteamLegacy(NetPlayerId receiver, byte[] batch, bool flag)
		{
			EP2PSend eP2PSendType = (flag ? EP2PSend.k_EP2PSendReliable : EP2PSend.k_EP2PSendUnreliable);
			return SteamNetworking.SendP2PPacket(convertPlayerIdToSteamId(receiver), batch, (uint)batch.Length, eP2PSendType, 1);
		}
		bool sendBatchViaSteamMessages(NetPlayerId receiver, byte[] batch, bool flag)
		{
			try
			{
				Marshal.Copy(batch, 0, sendBuffer, batch.Length);
				SteamNetworkingIdentity identityRemote = convertPlayerIdToSteamIdentity(receiver);
				int nSendFlags = ((!flag) ? 1 : 9);
				return SteamNetworkingMessages.SendMessageToUser(ref identityRemote, sendBuffer, (uint)batch.Length, nSendFlags, 1) == EResult.k_EResultOK;
			}
			catch (Exception message)
			{
				log(message);
				return false;
			}
		}
	}

	public void toggleUseVoiceSystem(bool useVoiceSystem)
	{
		useVoice = useVoiceSystem;
		if (lobbyType == LobbyType.PhotonPine)
		{
			return;
		}
		if (useVoice)
		{
			foreach (NetPlayerData player in players)
			{
				initVoiceForPlayer(player.id);
			}
			return;
		}
		foreach (NetPlayerData player2 in players)
		{
			if (player2.steamSpeaker != null)
			{
				player2.steamSpeaker.Stop();
				player2.steamSpeaker = null;
			}
			if (player2.photonSpeaker != null)
			{
				UnityEngine.Object.Destroy(player2.photonSpeaker.gameObject);
				player2.photonSpeaker = null;
			}
		}
	}

	private void initVoiceForPlayer(NetPlayerId playerId)
	{
		ConnectionMode connectionMode = this.connectionMode;
		if (connectionMode == ConnectionMode.SteamMessages || connectionMode == ConnectionMode.SteamLegacy)
		{
			AudioOutDelayControl.PlayDelayConfig playDelayConfig = new AudioOutDelayControl.PlayDelayConfig
			{
				Low = 200,
				High = 400,
				Max = 1000
			};
			PineFmodAudioOut<byte> pineFmodAudioOut = new PineFmodAudioOut<byte>(RuntimeManager.CoreSystem, playDelayConfig, new Photon.Voice.Unity.Logger(), "ES_SteamVoice", debugInfo: true, isSteamVoice: true);
			pineFmodAudioOut.Start(48000, 1, 960);
			players.Find((NetPlayerData x) => x.id == playerId).steamSpeaker = pineFmodAudioOut;
		}
	}

	private void sendSteamVoiceInternal()
	{
		if (!useVoice || toSendVoice.Count == 0)
		{
			return;
		}
		lastLocalPlayerSpeakTime = Time.time;
		byte[] array = encodeVoice(toSendVoice);
		foreach (NetPlayerData player in players)
		{
			NetPlayerId id = player.id;
			if (isLocalPlayer(id))
			{
				continue;
			}
			if (connectionMode == ConnectionMode.SteamMessages)
			{
				Marshal.Copy(array, 0, sendBuffer, array.Length);
				SteamNetworkingIdentity identityRemote = convertPlayerIdToSteamIdentity(id);
				EResult eResult = SteamNetworkingMessages.SendMessageToUser(ref identityRemote, sendBuffer, (uint)array.Length, 5, 2);
				if (eResult != EResult.k_EResultOK)
				{
					log("Failed to SEND VOICE to player: " + id.value, isError: true);
					log("Failed result: " + eResult);
				}
			}
			else if (connectionMode == ConnectionMode.SteamLegacy && !SteamNetworking.SendP2PPacket(convertPlayerIdToSteamId(id), array, (uint)array.Length, EP2PSend.k_EP2PSendUnreliableNoDelay, 2))
			{
				log("Failed to SEND VOICE using Legacy to player: " + id.value, isError: true);
			}
		}
		toSendVoice.Clear();
	}

	public void send(Packet packet, bool allowSendInMessageResponse = false, bool reliable = true, NetPlayerId ignorePlayer = null)
	{
		if (isHostInLobby() && (whitelist == null || whitelist.Contains(packet.GetType()) || ALWAYS_SEND_PACKET_TYPES.Contains(packet.GetType())) && (allowSendInMessageResponse || (game != null && !game.isProcessingPacket)))
		{
			initOutgoingPacket(packet);
			addToSend(reliable ? toSend : toSendUnreliable, packet, ignorePlayer);
		}
	}

	public void sendToSpecificPlayer(Packet packet, NetPlayerId receiverId, bool allowSendInMessageResponse = false, bool reliable = true)
	{
		if (netMode == NetMode.Host)
		{
			sendToSpecificPlayerFromHost(packet, receiverId, allowSendInMessageResponse, reliable);
		}
		else if (netMode == NetMode.Client)
		{
			sendToSpecificPlayerFromClient(packet, receiverId, allowSendInMessageResponse, reliable);
		}
		else
		{
			log("Trying to send a packet to specific player while net mode is none!!", isError: true);
		}
	}

	public void sendToSpecificPlayerFromClient(Packet packet, NetPlayerId receiverId, bool allowSendInMessageResponse = false, bool reliable = true)
	{
		if (netMode != NetMode.Client)
		{
			log(string.Format("Trying to call '{0}' while '{1}' has value of '{2}'.", "sendToSpecificPlayerFromClient", "netMode", netMode), isError: true);
			return;
		}
		packet.receiverId = receiverId;
		send(packet, allowSendInMessageResponse, reliable);
	}

	public void sendToSpecificPlayerFromHost(Packet packet, NetPlayerId receiverId, bool allowSendInMessageResponse = false, bool reliable = true)
	{
		if (netMode != NetMode.Host)
		{
			log(string.Format("Trying to send {0} while '{1}' has value of '{2}'.", packet.GetType(), "netMode", netMode), isError: true);
		}
		else if (receiverId == null || !isPlayerInLobby(receiverId))
		{
			log($"Trying to send {packet.GetType()} to a player that is not in the lobby: {receiverId}", isError: true);
		}
		else if (ALWAYS_SEND_PACKET_TYPES.Contains(packet.GetType()) || allowSendInMessageResponse || (game != null && !game.isProcessingPacket))
		{
			initOutgoingPacket(packet);
			enqueueOutgoingPacketWithSceneSyncCheck(reliable ? toSend : toSendUnreliable, receiverId, packet);
		}
	}

	private void initOutgoingPacket(Packet packet)
	{
		packet.sessionId = sessionId;
		packet.packetId = nextPacketId++;
		packet.sendTime = Time.time;
	}

	public void relay(Packet packet)
	{
		addToSend(toSend, packet, packet.senderId);
	}

	public void relayAll(Packet packet)
	{
		addToSend(toSend, packet, null);
	}

	private void addToSend(Dictionary<NetPlayerId, List<Packet>> sendMap, Packet packet, NetPlayerId ignorePlayer)
	{
		if (netMode == NetMode.None)
		{
			log(string.Format("Cannot execute '{0}' as '{1}' is set to '{2}'.", "addToSend", "netMode", NetMode.None), isError: true);
			return;
		}
		if (netMode == NetMode.Client)
		{
			enqueueOutgoingPacket(sendMap, hostPlayerId, packet);
			return;
		}
		foreach (NetPlayerData player in players)
		{
			if (!isLocalPlayer(player.id) && (!(ignorePlayer != null) || !(player.id == ignorePlayer)))
			{
				enqueueOutgoingPacketWithSceneSyncCheck(sendMap, player.id, packet);
			}
		}
	}

	private void enqueueOutgoingPacketWithSceneSyncCheck(Dictionary<NetPlayerId, List<Packet>> sendMap, NetPlayerId receiver, Packet packet)
	{
		NetPlayerData netPlayerData = players.Find((NetPlayerData player) => player.id == receiver);
		if (netPlayerData == null)
		{
			log($"Cannot enqueue packet to player (ID: {receiver}) which is not in the lobby.", isError: true);
		}
		else if (!netPlayerData.isSynced && !LEVEL_SYNC_PACKET_TYPES.Contains(packet.GetType()))
		{
			log($"Player {getUsername(receiver)} (ID: {receiver}) is not synced, so {packet.GetType()} will not be sent.");
		}
		else
		{
			enqueueOutgoingPacket(sendMap, receiver, packet);
		}
	}

	private void enqueueOutgoingPacket(Dictionary<NetPlayerId, List<Packet>> sendMap, NetPlayerId receiver, Packet packet)
	{
		if (isLocalPlayer(receiver))
		{
			log("Trying to send a packet to local player!", isError: true);
			return;
		}
		if (!sendMap.TryGetValue(receiver, out var value))
		{
			value = new List<Packet>();
			sendMap.Add(receiver, value);
		}
		if (packet.senderId == NetPlayerId.Empty)
		{
			packet.senderId = localPlayerId;
		}
		value.Add(packet);
	}

	private static void registerBatch(Packet[] packets, NetPlayerId senderId, Batch.NetworkResult result, int batchSizeInBytes)
	{
		if (packets.Length != 0)
		{
			int num = 0;
			foreach (Packet packet in packets)
			{
				num += packet.totalSize;
			}
			((result == Batch.NetworkResult.Received) ? receivedBatches : sentBatches).Add(new Batch
			{
				result = result,
				packets = packets,
				senderId = senderId,
				localTime = Time.time,
				headerSize = batchSizeInBytes - num,
				dataSize = num
			});
			((result == Batch.NetworkResult.Received) ? receivedBatchesPlayerIds : sentBatchesPlayerIds).Add(senderId);
		}
	}

	private void enqueueIncomingPacket(Packet packet, NetPlayerId sender)
	{
		if (packet.receiverId == NetPlayerId.Empty || packet.receiverId == localPlayerId)
		{
			receivedPackets.Enqueue(packet);
		}
		if (netMode == NetMode.Host && !isLocalPlayer(packet.receiverId))
		{
			if (packet.receiverId != NetPlayerId.Empty)
			{
				enqueueOutgoingPacketWithSceneSyncCheck(toSend, packet.receiverId, packet);
			}
			else if (!DONT_RELAY_PACKET_TYPES.Contains(packet.GetType()) && !Game.INTERACTION_PACKET_EXTRACTORS.ContainsKey(packet.GetType()))
			{
				addToSend(toSend, packet, sender);
			}
		}
	}

	public string getUsername()
	{
		return getUsername(localPlayerId);
	}

	public string getUsername(NetPlayerId playerId)
	{
		return players.Find((NetPlayerData p) => p.id == playerId)?.username ?? "CAN'T FIND USERNAME!";
	}

	public bool isLocalPlayer()
	{
		return isLocalPlayer(localPlayerId);
	}

	public bool isLocalPlayer(NetPlayerId player)
	{
		if (!(player == localPlayerId))
		{
			return localPlayerId.value == "Singleplayer";
		}
		return true;
	}

	public bool isHost()
	{
		return isHost(localPlayerId);
	}

	public bool isHost(NetPlayerId player)
	{
		return hostPlayerId == player;
	}

	public NetPlayerData getPlayer()
	{
		return getPlayer(localPlayerId);
	}

	public NetPlayerData getPlayer(NetPlayerId playerId)
	{
		return players.Find((NetPlayerData p) => p.id == playerId);
	}

	private void addPlayer(NetPlayerId playerId)
	{
		if (players.Find((NetPlayerData data) => data.id == playerId) != null)
		{
			log($"Cannot add player with ID {playerId} as it already exists!", isError: true);
			return;
		}
		players.Add(new NetPlayerData
		{
			id = playerId
		});
		if (useVoice)
		{
			initVoiceForPlayer(playerId);
		}
		updatePlayer(playerId);
		log($"Added player {getUsername(playerId)} (ID: {playerId} | Is Local: {isLocalPlayer(playerId)}). New player count: {players.Count}");
	}

	private void updatePlayer(NetPlayerId playerId)
	{
		NetPlayerData netPlayerData = players.Find((NetPlayerData netPlayerData2) => netPlayerData2.id == playerId);
		if (netPlayerData == null)
		{
			log($"Cannot update player with ID {playerId} as it does not exist!", isError: true);
			return;
		}
		if (lobbyType == LobbyType.PhotonPine)
		{
			foreach (KeyValuePair<int, Player> player in photon.client.CurrentRoom.Players)
			{
				if (player.Value.UserId == playerId.value)
				{
					AvatarData data = AvatarData.deserialize((byte[])player.Value.CustomProperties["PlayerAvatar"]);
					NetPlatform netPlatform = (NetPlatform)int.Parse(player.Value.CustomProperties["PlayerPlatform"].ToString());
					netPlayerData.avatar = avatarDataToTexture(data, netPlatform == NetPlatform.Steam);
					netPlayerData.username = player.Value.NickName;
					netPlayerData.isVR = player.Value.CustomProperties["IsVrPlayer"].ToString().ToLower() == "true";
					netPlayerData.isOculus = player.Value.CustomProperties["IsOculusPlayer"].ToString().ToLower() == "true";
					netPlayerData.isSwitch = player.Value.CustomProperties["IsNintendoSwitchPlayer"].ToString().ToLower() == "true";
					netPlayerData.isSteam = player.Value.CustomProperties["IsSteamPlayer"].ToString().ToLower() == "true";
					netPlayerData.isDemo = player.Value.CustomProperties["IsDemoPlayer"].ToString().ToLower() == "true";
					break;
				}
			}
		}
		else if (lobbyType == LobbyType.SteamPine)
		{
			int largeFriendAvatar = SteamFriends.GetLargeFriendAvatar(convertPlayerIdToSteamId(playerId));
			if (largeFriendAvatar != -1)
			{
				log($"Setting image of player {playerId} with imageId {largeFriendAvatar}");
				SteamUtils.GetImageSize(largeFriendAvatar, out var pnWidth, out var pnHeight);
				byte[] array = new byte[4 * pnWidth * pnHeight];
				if (SteamUtils.GetImageRGBA(largeFriendAvatar, array, (int)(4 * pnWidth * pnHeight)))
				{
					Texture2D texture2D = new Texture2D((int)pnWidth, (int)pnHeight, TextureFormat.RGBA32, mipChain: false, linear: true);
					texture2D.LoadRawTextureData(array);
					Texture2D texture2D2 = flipTexture(texture2D);
					texture2D2.Apply();
					netPlayerData.avatar = texture2D2;
				}
			}
			CSteamID cSteamID = convertPlayerIdToSteamId(playerId);
			netPlayerData.username = SteamFriends.GetFriendPersonaName(cSteamID) ?? string.Empty;
			string lobbyMemberData = SteamMatchmaking.GetLobbyMemberData(currentLobbySteam, cSteamID, "IsVrPlayer");
			string lobbyMemberData2 = SteamMatchmaking.GetLobbyMemberData(currentLobbySteam, cSteamID, "IsDemoPlayer");
			netPlayerData.isVR = lobbyMemberData != null && lobbyMemberData.ToLower() == "true";
			netPlayerData.isDemo = lobbyMemberData2 != null && lobbyMemberData2.ToLower() == "true";
			log($"{netPlayerData.username} isVR: {netPlayerData.isVR} ({netPlayerData.isVR}) isDemo: {netPlayerData.isDemo} ({netPlayerData.isDemo})");
		}
		menuCallbacks?.onLobbyMemberDataChanged();
	}

	private void removePlayer(NetPlayerId playerId)
	{
		NetPlayerData netPlayerData = players.Find((NetPlayerData player) => player.id == playerId);
		if (netPlayerData == null)
		{
			log($"Cannot remove player (ID: {playerId}) as player does not exist!", isError: true);
			return;
		}
		if (netPlayerData.photonSpeaker != null)
		{
			UnityEngine.Object.Destroy(netPlayerData.photonSpeaker.gameObject);
		}
		netPlayerData.steamSpeaker?.Stop();
		if (netMode == NetMode.Host)
		{
			closeSteamConnectionWith(playerId);
			if (lobbyType == LobbyType.SteamPine && playerId == localPlayerId)
			{
				SteamMatchmaking.JoinLobby(currentLobbySteam);
			}
		}
		players.Remove(netPlayerData);
		log($"Removed player {netPlayerData.username} (ID: {playerId} | Is Local: {isLocalPlayer(playerId)}). New player count: {players.Count}");
	}

	private void closeSteamConnectionWith(NetPlayerId playerId)
	{
		if (lobbyType == LobbyType.SteamPine)
		{
			if (connectionMode == ConnectionMode.SteamMessages)
			{
				SteamNetworkingIdentity identityRemote = convertPlayerIdToSteamIdentity(playerId);
				SteamNetworkingMessages.CloseChannelWithUser(ref identityRemote, 1);
				SteamNetworkingMessages.CloseChannelWithUser(ref identityRemote, 2);
			}
			else if (connectionMode == ConnectionMode.SteamLegacy)
			{
				SteamNetworking.CloseP2PSessionWithUser(convertPlayerIdToSteamId(playerId));
			}
		}
	}

	public void setSynced(NetPlayerId playerId)
	{
		NetPlayerData netPlayerData = players.Find((NetPlayerData player) => player.id == playerId);
		if (netPlayerData != null)
		{
			netPlayerData.isSynced = true;
			log(string.Format("{0} [{1}] Player {2} (ID: {3}) is now synced.", "[Level-Sync]", "setSynced", netPlayerData.username, playerId));
			if (isHost())
			{
				menuCallbacks?.onPlayerSynced(netPlayerData);
			}
		}
	}

	public void setUnsynced(NetPlayerId playerId)
	{
		NetPlayerData netPlayerData = players.Find((NetPlayerData player) => player.id == playerId);
		if (netPlayerData != null)
		{
			netPlayerData.isSynced = false;
			log(string.Format("{0} [{1}] Player {2} (ID: {3}) is no longer synced.", "[Level-Sync]", "setUnsynced", netPlayerData.username, playerId));
		}
	}

	public AvatarData getSteamAvatar(CSteamID player)
	{
		AvatarData avatarData = new AvatarData();
		int largeFriendAvatar = SteamFriends.GetLargeFriendAvatar(player);
		if (largeFriendAvatar != -1)
		{
			SteamUtils.GetImageSize(largeFriendAvatar, out var pnWidth, out var pnHeight);
			byte[] array = new byte[4 * pnWidth * pnHeight];
			if (SteamUtils.GetImageRGBA(largeFriendAvatar, array, (int)(4 * pnWidth * pnHeight)))
			{
				avatarData.avatarData = array;
				avatarData.height = (int)pnHeight;
				avatarData.width = (int)pnWidth;
			}
		}
		return avatarData;
	}

	private Texture2D avatarDataToTexture(AvatarData data, bool isSteam = true)
	{
		if (data == null || data.avatarData == null || data.avatarData.Length == 0)
		{
			log("can't load avatar!");
			return Texture2D.grayTexture;
		}
		Texture2D texture2D = null;
		if (isSteam)
		{
			texture2D = new Texture2D(data.width, data.height, TextureFormat.RGBA32, mipChain: false, linear: true);
			texture2D.LoadRawTextureData(data.avatarData);
			texture2D = flipTexture(texture2D);
			texture2D.Apply();
		}
		else
		{
			texture2D = new Texture2D(data.width, data.height);
			texture2D.LoadImage(data.avatarData);
		}
		return texture2D;
	}

	public static Texture2D flipTexture(Texture2D original)
	{
		Texture2D texture2D = new Texture2D(original.width, original.height);
		int width = original.width;
		int height = original.height;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				texture2D.SetPixel(i, height - j - 1, original.GetPixel(i, j));
			}
		}
		texture2D.Apply();
		return texture2D;
	}

	public void log(object message, bool isError = false)
	{
		string text = (isError ? " ERROR " : " ");
		string message2 = $"[Net #{netInstanceId}] {text} {Time.time} {Time.frameCount}: {message}";
		if (isError)
		{
			Debug.LogError(message2);
		}
		else
		{
			Debug.Log(message2);
		}
	}
}
