using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Localisation;
using Steamworks;
using UnityEngine;

public class SteamConnection : BaseConnection
{
	private const long PACKET_MAGIC_HEADER = 65535L;

	private const long PACKET_PING = 64250L;

	private const long PACKET_USERDISCONNECTED = 64506L;

	private const long PACKET_USERKICKED = 64507L;

	private const long PACKET_INTRODUCTION = 257L;

	private const float P2PConnectionTimeout = 5f;

	private const float KeepAliveInterval = 0.5f;

	private const float MinReconnectTime = 10f;

	public NetworkStatsCounter NetworkStats;

	private int customReliableChannel;

	private uint networkFrame;

	private uint messageID;

	private float lastStatsDebugPrint;

	private CSteamID currentSteamLobbyID;

	private Thread networkingThread;

	private ConcurrentQueue<SteamPacket> receiveQueue = new ConcurrentQueue<SteamPacket>();

	private List<ReliableSteamMessage> unackedMessages = new List<ReliableSteamMessage>();

	private List<int> channels = new List<int>();

	private Dictionary<int, EP2PSend> channelQosTypes = new Dictionary<int, EP2PSend>();

	private Callback<LobbyChatMsg_t> m_CallbackLobbyChatMsg;

	private bool isHosting;

	private CSteamID clientSteamID;

	private bool isTimingOut;

	private double ping;

	private string _GenerateString;

	private Callback<LobbyEnter_t> m_CallbackLobbyEnter;

	private Callback<LobbyMatchList_t> m_CallbackLobbyList;

	private Callback<P2PSessionConnectFail_t> m_CallbackP2PSessionConnectFailClient;

	private LinearWeightedMovingAverage pingAvg = new LinearWeightedMovingAverage(5);

	private float lastAckSent;

	private float nextReconnectTime;

	private CSteamID steamServerId;

	private ISteamMatchmakingPingResponse matchmakingPingResponse;

	private ISteamMatchmakingServerListResponse internalServerlistResponse;

	private HServerListRequest serverlistRequest = HServerListRequest.Invalid;

	private HServerQuery serverPingRequest = HServerQuery.Invalid;

	private List<gameserveritem_t> servers = new List<gameserveritem_t>();

	private bool clientGotKicked;

	private bool isIPPublished;

	private bool steamConnected;

	private bool serverInitialized;

	private int serverPort;

	private DateTime startDateTime = DateTime.Now;

	private Callback<SteamServersConnected_t> m_CallbackSteamServersConnected;

	private Callback<SteamServerConnectFailure_t> m_CallbackSteamServersConnectFailure;

	private Callback<SteamServersDisconnected_t> m_CallbackSteamServersDisconnected;

	private Callback<P2PSessionRequest_t> m_CallbackP2PSessionRequest;

	private Callback<P2PSessionConnectFail_t> m_CallbackP2PSessionConnectFailServer;

	private Callback<LobbyCreated_t> m_CallbackSteamLobbyCreated;

	private List<CSteamID> steamConnections;

	private List<NetworkStatsCounter> ClientStatsList = new List<NetworkStatsCounter>();

	private Dictionary<CSteamID, SteamSession> acceptedConnections;

	private Dictionary<CSteamID, SteamSession> deadConnections;

	private Dictionary<ushort, SteamSession> steamIDLookup;

	[SerializeField]
	private Dictionary<CSteamID, NetworkStatsCounter> ClientStats = new Dictionary<CSteamID, NetworkStatsCounter>();

	private bool updatePackets;

	public override ulong LobbyID
	{
		get
		{
			return currentSteamLobbyID.m_SteamID;
		}
	}

	public override string CurrentNetwork
	{
		get
		{
			return (!StatMaster.isHosting) ? OptionsMaster.BesiegeConfig.LastConnectedAddress : BesiegeNetworkManager.Instance.ExternalIP;
		}
	}

	private CSteamID _ConnectedTo
	{
		get
		{
			return steamServerId;
		}
		set
		{
			steamServerId = value;
			serverID = value.m_SteamID;
		}
	}

	private double NetworkTime
	{
		get
		{
			return (DateTime.Now - startDateTime).TotalSeconds;
		}
	}

	public override int AddChannel(BesiegeQosType qosType)
	{
		int nextChannelId = GetNextChannelId();
		EP2PSend value;
		switch (qosType)
		{
		case BesiegeQosType.Reliable:
		case BesiegeQosType.AllCostDelivery:
			value = EP2PSend.k_EP2PSendReliable;
			break;
		case BesiegeQosType.Unreliable:
			value = EP2PSend.k_EP2PSendUnreliable;
			break;
		default:
			value = EP2PSend.k_EP2PSendReliable;
			break;
		}
		channels.Add(nextChannelId);
		channelQosTypes.Add(nextChannelId, value);
		return nextChannelId;
	}

	public override void BroadcastMessage(int channel, byte[] data)
	{
		for (int i = 0; i < steamConnections.Count; i++)
		{
			SendNetworkMessage(steamConnections[i], channel, data);
		}
	}

	public override void ConnectPlayfab(string pfNetworkId)
	{
		Debug.LogError("[SteamConnection] ConnectPlayfab is not supported");
	}

	public override void ConnectSteam(ulong gameserverId)
	{
		connectAttempt = 1;
		if (BesiegeLogFilter.logDebug)
		{
			Debug.LogFormat(Thread.CurrentThread.ManagedThreadId + ") Connecting to gameserver {0}", gameserverId);
		}
		InitializeClient();
		ConnectTo((CSteamID)gameserverId);
	}

	public override void ConnectToIP(string serverAddress, int serverPort)
	{
		IPAddress address = IPAddress.Parse(serverAddress);
		connectAddress = serverAddress;
		connectPort = serverPort;
		connectAttempt = 1;
		if (BesiegeLogFilter.logDebug)
		{
			Debug.LogFormat("Connecting to host {0}:{1}", serverAddress, serverPort);
		}
		InitializeClient();
		servers.Clear();
		if (IPAddressHelper.IsInternal(serverAddress))
		{
			SetClientConnectionState(ClientConnectionState.ResolvingHost);
			if (serverlistRequest.m_HServerListRequest != IntPtr.Zero)
			{
				SteamMatchmakingServers.CancelQuery(serverlistRequest);
			}
			serverlistRequest = RefreshList(SteamServerList.LAN);
		}
		else
		{
			SetClientConnectionState(ClientConnectionState.AttemptingDirectConnect);
			if (serverPingRequest.m_HServerQuery != -1)
			{
				SteamMatchmakingServers.CancelServerQuery(serverPingRequest);
			}
			serverPingRequest = ConnectTo(address.ToUint(), (ushort)serverPort);
		}
	}

	public override void ConnectToLobby(ulong lobbyId)
	{
		InitializeClient();
		connectAttempt = 1;
		CSteamID lobbySteamId = (CSteamID)lobbyId;
		JoinLobby(lobbySteamId);
	}

	public override void Disconnect()
	{
		if (clientConnectionState != ClientConnectionState.Disconnected)
		{
			if (BesiegeLogFilter.logDebug)
			{
				Debug.LogFormat("Disconnecting from server '{0}'", (!_ConnectedTo.IsValid()) ? "not connected" : _ConnectedTo.ToString());
			}
			ShutdownClient();
			SetClientConnectionState(ClientConnectionState.Disconnected);
			connectionHandler.OnDisconnected(false);
		}
	}

	public override void DisconnectPlayer(ushort playerId)
	{
		if (steamIDLookup.ContainsKey(playerId))
		{
			SteamSession steamSession = steamIDLookup[playerId];
			UnregisterConnection(steamSession.SteamID);
			connectionHandler.OnPlayerLeave(playerId);
		}
	}

	public override int GetPing(ushort playerId)
	{
		if (!steamIDLookup.ContainsKey(playerId))
		{
			return 0;
		}
		return (int)steamIDLookup[playerId].Ping;
	}

	public override void Initialize()
	{
		AddChannel(BesiegeQosType.Reliable);
		customReliableChannel = AddChannel(BesiegeQosType.Unreliable);
		NetworkStats = new NetworkStatsCounter();
		OptionsMaster.BesiegeConfig.MaximumTransmissionUnit = 1200;
		isHosting = StatMaster.isServer;
		clientSteamID = SteamUser.GetSteamID();
	}

	public override bool Listen(int serverPort)
	{
		SetServerConnectionState(ServerConnectionState.InitializingHost);
		bool flag = InitializeServer(serverPort);
		if (!flag)
		{
			SetServerConnectionState(ServerConnectionState.InitializationFailed);
		}
		return flag;
	}

	protected virtual bool InitializeServer(int gamePort)
	{
		steamConnections = new List<CSteamID>();
		acceptedConnections = new Dictionary<CSteamID, SteamSession>();
		deadConnections = new Dictionary<CSteamID, SteamSession>();
		steamIDLookup = new Dictionary<ushort, SteamSession>();
		m_CallbackSteamServersConnected = Callback<SteamServersConnected_t>.CreateGameServer(OnSteamServersConnected);
		m_CallbackSteamServersConnectFailure = Callback<SteamServerConnectFailure_t>.CreateGameServer(OnSteamServersConnectFailure);
		m_CallbackSteamServersDisconnected = Callback<SteamServersDisconnected_t>.CreateGameServer(OnSteamServersDisconnected);
		m_CallbackP2PSessionRequest = Callback<P2PSessionRequest_t>.CreateGameServer(OnP2PSessionRequestServer);
		m_CallbackP2PSessionConnectFailServer = Callback<P2PSessionConnectFail_t>.CreateGameServer(OnP2PSessionConnectFailServer);
		m_CallbackSteamLobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
		m_CallbackLobbyChatMsg = Callback<LobbyChatMsg_t>.Create(OnLobbyChatMessage);
		serverPort = gamePort;
		if (!GameServer.Init(IPAddress.Any.ToUint(), (ushort)gamePort, (ushort)(gamePort + 1), EServerMode.eServerModeAuthentication, VersionNumber.GetVersionString()))
		{
			if (BesiegeLogFilter.logDebug)
			{
				Debug.LogError("GameServer.Init failed and returned false.");
			}
			return false;
		}
		SteamGameServer.SetProduct(Application.productName);
		SteamGameServer.SetGameDescription("Besiege server");
		SteamGameServer.SetServerName("Nameless Besiege server");
		SteamGameServer.SetMaxPlayerCount(NetworkScene.ServerSettings.maxPlayers);
		SteamGameServer.SetMapName("MP Sandbox");
		SteamGameServer.EnableHeartbeats(true);
		SteamGameServer.ForceHeartbeat();
		SteamGameServer.LogOnAnonymous();
		int maxPlayers = NetworkScene.ServerSettings.maxPlayers;
		SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, maxPlayers);
		SetServerConnectionState(ServerConnectionState.WaitingForPlatformConnection);
		startDateTime = DateTime.Now;
		NetworkStats.Clear();
		networkID = 0;
		connectionHandler.OnConnected();
		connectionHandler.OnPlayerJoin(networkID);
		serverInitialized = true;
		StartPacketUpdate();
		return true;
	}

	private void OnLobbyChatMessage(LobbyChatMsg_t pCallback)
	{
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("[OnLobbyChatMessage] user chat: {0}, state: {1}", pCallback.m_ulSteamIDUser, (EChatEntryType)pCallback.m_eChatEntryType);
		}
		byte[] array = new byte[1024];
		CSteamID pSteamIDUser;
		EChatEntryType peChatEntryType;
		int lobbyChatEntry = SteamMatchmaking.GetLobbyChatEntry((CSteamID)pCallback.m_ulSteamIDLobby, (int)pCallback.m_iChatID, out pSteamIDUser, array, array.Length, out peChatEntryType);
		if (lobbyChatEntry > 0)
		{
			if (isHosting)
			{
				ParseLobbyMessageServer(pSteamIDUser, array);
			}
			else
			{
				ParseLobbyMessageClient(pSteamIDUser, array);
			}
		}
		else if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("[OnLobbyChatMessage] Could not get lobby chat entry, ret: {0}, pCallback.m_iChatID: {1}", lobbyChatEntry);
		}
	}

	private void ParseLobbyMessageServer(CSteamID fromSteamID, byte[] message)
	{
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("[ParseLobbyMessageServer] parsing message from: {0}, length: {1}", fromSteamID, message.Length);
		}
		using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(message)))
		{
			long num = binaryReader.ReadInt64();
			if (num != 65535)
			{
				return;
			}
			long num2 = binaryReader.ReadInt64();
			if (num2 == 64506)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log("[Server] Received disconnect packet from user");
				}
				if (acceptedConnections.ContainsKey(fromSteamID))
				{
					DisconnectPlayer(acceptedConnections[fromSteamID].ConnectionID);
				}
			}
		}
	}

	private void ParseLobbyMessageClient(CSteamID fromSteamID, byte[] message)
	{
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("[ParseLobbyMessageClient] parsing message from: {0}, length: {1}", fromSteamID, message.Length);
		}
		if (steamServerId == CSteamID.Nil)
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("[ParseLobbyMessageClient] Not connected to server yet...");
			}
			return;
		}
		bool flag = SteamMatchmaking.GetLobbyOwner(currentSteamLobbyID).m_SteamID == clientSteamID.m_SteamID;
		if (fromSteamID != steamServerId && !flag)
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("[ParseLobbyMessageClient] fromSteamID: {0} does not match the server id: {1}, our own id is: {2}", fromSteamID, steamServerId, clientSteamID);
			}
			return;
		}
		using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(message)))
		{
			long num = binaryReader.ReadInt64();
			if (num != 65535)
			{
				return;
			}
			long num2 = binaryReader.ReadInt64();
			if (num2 != 64507)
			{
				return;
			}
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("[ParseLobbyMessageClient] Got USERKICKED message", fromSteamID, steamServerId);
			}
			long num3 = binaryReader.ReadInt64();
			if (num3 != (long)clientSteamID.m_SteamID)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.LogFormat("[ParseLobbyMessageClient] playerSteamId '{0}' does not match my id: {1}", num3, clientSteamID.m_SteamID);
				}
				return;
			}
			string errorMessage = string.Empty;
			try
			{
				errorMessage = binaryReader.ReadString();
			}
			catch
			{
			}
			clientGotKicked = true;
			Disconnect(errorMessage);
		}
	}

	private void OnP2PSessionConnectFailServer(P2PSessionConnectFail_t param)
	{
		P2PRemove(param.m_steamIDRemote);
	}

	private void OnSteamServersConnected(SteamServersConnected_t callback)
	{
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Connnected from the Steam servers.");
		}
		SetLobbyData();
		steamConnected = true;
	}

	private void OnSteamServersDisconnected(SteamServersDisconnected_t callback)
	{
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Disconnected from the Steam servers.");
		}
		steamConnected = false;
	}

	private void OnSteamServersConnectFailure(SteamServerConnectFailure_t callback)
	{
		steamConnected = false;
		if (BesiegeLogFilter.logDebug)
		{
			Debug.LogWarning("We were unable to connect to Steams' Servers");
		}
	}

	private void OnP2PSessionRequestServer(P2PSessionRequest_t callback)
	{
		CSteamID steamIDRemote = callback.m_steamIDRemote;
		if (!steamIDRemote.IsValid() || deadConnections.ContainsKey(steamIDRemote))
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("SteamID {0} tried to connect but is invalid or a dead connection", steamIDRemote);
			}
		}
		else if (SteamGameServerNetworking.AcceptP2PSessionWithUser(callback.m_steamIDRemote))
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("A player successfully connected with us. SteamID: {0}", callback.m_steamIDRemote);
			}
			if (!ClientStats.ContainsKey(steamIDRemote))
			{
				NetworkStatsCounter networkStatsCounter = new NetworkStatsCounter();
				networkStatsCounter.Touch();
				ClientStatsList.Add(networkStatsCounter);
				ClientStats.Add(steamIDRemote, networkStatsCounter);
			}
			SteamSession steamSession = new SteamSession(steamIDRemote);
			steamSession.PlayerStatus = SessionPlayerStatus.Pending;
			if (!acceptedConnections.ContainsKey(steamIDRemote))
			{
				acceptedConnections.Add(steamIDRemote, steamSession);
			}
			else
			{
				acceptedConnections[steamIDRemote] = steamSession;
			}
			SendConnectedMessage(callback.m_steamIDRemote);
		}
	}

	private void RegisterConnection(CSteamID steamIDRemote, SteamSession steamSession)
	{
		steamIDLookup.Add(acceptedConnections[steamIDRemote].ConnectionID, steamSession);
		steamConnections.Add(steamIDRemote);
		P2PTouch(steamIDRemote);
	}

	private void UnregisterConnection(CSteamID steamIDRemote)
	{
		steamConnections.Remove(steamIDRemote);
		P2PRemove(steamIDRemote);
	}

	private void OnP2PSessionConnectFail(P2PSessionConnectFail_t callback)
	{
		OnP2PSessionConnectFailClient(callback);
	}

	private void OnLobbyCreated(LobbyCreated_t result)
	{
		if (result.m_eResult == EResult.k_EResultOK)
		{
			currentSteamLobbyID = (CSteamID)result.m_ulSteamIDLobby;
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log(string.Concat("Lobby created(", currentSteamLobbyID, ")."));
			}
			SetLobbyData();
		}
		else if (BesiegeLogFilter.logDebug)
		{
			Debug.LogError("Failed to create lobby created, error: " + result.m_eResult);
		}
	}

	public string GetPlayerName(CSteamID steamID)
	{
		try
		{
			return acceptedConnections[steamID].Username;
		}
		catch
		{
			return string.Empty;
		}
	}

	private void SetLobbyData()
	{
		if (!currentSteamLobbyID.IsValid())
		{
			int maxPlayers = NetworkScene.ServerSettings.maxPlayers;
			SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, maxPlayers);
			return;
		}
		string personaName = SteamFriends.GetPersonaName();
		string pchValue = personaName + "'s game";
		SteamMatchmaking.SetLobbyData(currentSteamLobbyID, "name", pchValue);
		if (!string.IsNullOrEmpty(StatMaster.ExternalIP))
		{
			SteamMatchmaking.SetLobbyData(currentSteamLobbyID, "publicIP", StatMaster.ExternalIP);
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log("Setting lobby data[serverip]: " + StatMaster.ExternalIP);
			}
		}
		IEnumerable<string> internalIPs = IPAddressHelper.GetInternalIPs();
		string text = string.Join("|", internalIPs.ToArray());
		SteamMatchmaking.SetLobbyData(currentSteamLobbyID, "internalIPs", text);
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log("Setting lobby data[internalIPs]: " + text);
		}
		SteamMatchmaking.SetLobbyData(currentSteamLobbyID, "port", serverPort.ToString());
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log("Setting lobby data[port]: " + serverPort);
			Debug.Log("Setting LobbyGameServer: " + SteamGameServer.GetSteamID());
		}
		SteamMatchmaking.SetLobbyGameServer(currentSteamLobbyID, 0u, 0, SteamGameServer.GetSteamID());
	}

	private void ClearLobbyData()
	{
		if (currentSteamLobbyID.IsValid())
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log("Clearing lobby data...");
			}
			SteamMatchmaking.DeleteLobbyData(currentSteamLobbyID, "name");
			SteamMatchmaking.DeleteLobbyData(currentSteamLobbyID, "publicIP");
			SteamMatchmaking.DeleteLobbyData(currentSteamLobbyID, "internalIPs");
			SteamMatchmaking.DeleteLobbyData(currentSteamLobbyID, "port");
		}
	}

	private bool SendConnectedMessage(CSteamID client)
	{
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				binaryWriter.Write(4386);
				binaryWriter.Write(UnityEngine.Random.Range(15, 23));
				return SendClientMessageReliable(client, memoryStream.ToArray(), (int)memoryStream.Length, 0);
			}
		}
	}

	private void SendLobbyClientKick(CSteamID client)
	{
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				binaryWriter.Write(65535L);
				binaryWriter.Write(64507L);
				binaryWriter.Write(client.m_SteamID);
				binaryWriter.Write(LocalisationManager.GetTranslation(2024));
				byte[] array = memoryStream.ToArray();
				SteamMatchmaking.SendLobbyChatMsg(currentSteamLobbyID, array, array.Length);
			}
		}
	}

	private bool SendClientHandshake(CSteamID client, string connectionString)
	{
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				binaryWriter.Write(257L);
				binaryWriter.Write(connectionString);
				return SendClientMessageReliable(client, memoryStream.ToArray(), (int)memoryStream.Length, 0);
			}
		}
	}

	private bool P2PTouch(CSteamID SessionId)
	{
		if (!deadConnections.ContainsKey(SessionId))
		{
			if (acceptedConnections.ContainsKey(SessionId))
			{
				ClientStats[SessionId].Touch();
			}
			else
			{
				acceptedConnections.Add(SessionId, new SteamSession(SessionId));
			}
			return true;
		}
		return false;
	}

	private void P2PRemove(CSteamID SessionId)
	{
		double num = 0.0;
		SteamSession value;
		if (acceptedConnections.TryGetValue(SessionId, out value))
		{
			num = ClientStats[SessionId].TimeoutTime;
			ClientStats[SessionId].Touch();
			deadConnections.Add(SessionId, value);
		}
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("Removing P2P Session Id: {0}, IdleTime: {1}", SessionId, (value == null) ? 9999.0 : num);
		}
		steamConnections.Remove(SessionId);
		steamIDLookup.Remove(value.ConnectionID);
		acceptedConnections.Remove(SessionId);
	}

	private void NetworkingThreadUpdateServer()
	{
		uint pcubMsgSize;
		while (SteamGameServerNetworking.IsP2PPacketAvailable(out pcubMsgSize))
		{
			SteamPacket steamPacket = new SteamPacket();
			steamPacket.Buffer = new byte[pcubMsgSize];
			steamPacket.Channel = 0;
			if (!SteamGameServerNetworking.ReadP2PPacket(steamPacket.Buffer, pcubMsgSize, out steamPacket.BufferSize, out steamPacket.SteamID))
			{
				break;
			}
			NetworkStats.IncrementBytesReceived(pcubMsgSize);
			ClientStats[steamPacket.SteamID].IncrementBytesReceived(pcubMsgSize);
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(steamPacket.Buffer)))
			{
				long num = binaryReader.ReadInt64();
				if (num == 64250)
				{
					double packetLastClientTS = binaryReader.ReadDouble();
					double lastClientPing = binaryReader.ReadDouble();
					HandleClientPing(steamPacket.SteamID, packetLastClientTS, lastClientPing);
					P2PTouch(steamPacket.SteamID);
				}
				else
				{
					receiveQueue.Enqueue(steamPacket);
				}
			}
		}
		foreach (int channel in channels)
		{
			if (channel == 0)
			{
				continue;
			}
			while (SteamGameServerNetworking.IsP2PPacketAvailable(out pcubMsgSize, channel))
			{
				SteamPacket steamPacket = new SteamPacket();
				steamPacket.Buffer = new byte[pcubMsgSize];
				steamPacket.Channel = channel;
				if (!SteamGameServerNetworking.ReadP2PPacket(steamPacket.Buffer, pcubMsgSize, out steamPacket.BufferSize, out steamPacket.SteamID, channel))
				{
					break;
				}
				NetworkStats.IncrementBytesReceived(pcubMsgSize);
				ClientStats[steamPacket.SteamID].IncrementBytesReceived(pcubMsgSize);
				if (channel == customReliableChannel)
				{
					HandleCustomReliableMessageServer(steamPacket);
				}
				else
				{
					receiveQueue.Enqueue(steamPacket);
				}
			}
		}
	}

	private void HandleCustomReliableMessageServer(SteamPacket packet)
	{
		ReliableSteamMessage msg;
		if (!HandleCustomReliableMessage(packet, out msg))
		{
			return;
		}
		using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(msg.Data)))
		{
			long num = binaryReader.ReadInt64();
			if (num == 64250)
			{
				double packetLastClientTS = binaryReader.ReadDouble();
				double lastClientPing = binaryReader.ReadDouble();
				HandleClientPing(msg.SteamID, packetLastClientTS, lastClientPing);
				P2PTouch(msg.SteamID);
			}
		}
	}

	private void UpdateServer()
	{
		if (!isIPPublished && !string.IsNullOrEmpty(StatMaster.ExternalIP) && currentSteamLobbyID.IsValid())
		{
			SetLobbyData();
			isIPPublished = true;
		}
		int count = receiveQueue.Count;
		while (count-- > 0)
		{
			SteamPacket result;
			if (!receiveQueue.TryDequeue(out result))
			{
				continue;
			}
			if (result.Channel != 0)
			{
				if (!acceptedConnections.ContainsKey(result.SteamID))
				{
					break;
				}
				P2PTouch(result.SteamID);
				ushort playerId = acceptedConnections[result.SteamID].ConnectionID;
				connectionHandler.OnDataEvent(playerId, result.Channel, result.Buffer, (int)result.BufferSize);
				continue;
			}
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(result.Buffer)))
			{
				switch (binaryReader.ReadInt64())
				{
				case 64506L:
					if (BesiegeLogFilter.logDev)
					{
						Debug.Log("[Server] Received disconnect packet from user");
					}
					if (acceptedConnections.ContainsKey(result.SteamID))
					{
						DisconnectPlayer(acceptedConnections[result.SteamID].ConnectionID);
					}
					break;
				case 257L:
				{
					string connectionString = binaryReader.ReadString();
					string username = binaryReader.ReadString();
					HandleClientHandshake(result.SteamID, connectionString, username);
					break;
				}
				}
			}
		}
		if (updatePackets)
		{
			GameServer.RunCallbacks();
			NetworkingThreadUpdateServer();
		}
		CheckExpiredConnections();
	}

	public HServerListRequest RefreshList(SteamServerList listType, ISteamMatchmakingServerListResponse response = null)
	{
		return RefreshList(listType, new MatchMakingKeyValuePair_t[0], response);
	}

	public HServerListRequest RefreshList(SteamServerList listType, MatchMakingKeyValuePair_t[] filters, ISteamMatchmakingServerListResponse response = null)
	{
		AppId_t appID = SteamUtils.GetAppID();
		return RefreshList(appID, listType, new MatchMakingKeyValuePair_t[0], response);
	}

	public HServerListRequest RefreshList(AppId_t appId, SteamServerList listType, MatchMakingKeyValuePair_t[] filters, ISteamMatchmakingServerListResponse response = null)
	{
		if (response == null)
		{
			internalServerlistResponse = new ISteamMatchmakingServerListResponse(SteamMatchmakingServerListResponse_ServerResponded, SteamMatchmakingServerListResponse_ServerFailedToResponded, delegate(HServerListRequest a, EMatchMakingServerResponse b)
			{
				SteamMatchmakingServerListResponse_OnRefreshComplete(a, b, listType);
			});
			response = internalServerlistResponse;
		}
		switch (listType)
		{
		case SteamServerList.Favorites:
			return SteamMatchmakingServers.RequestFavoritesServerList(appId, filters, (uint)filters.Length, response);
		case SteamServerList.Friends:
			return SteamMatchmakingServers.RequestFriendsServerList(appId, filters, (uint)filters.Length, response);
		case SteamServerList.History:
			return SteamMatchmakingServers.RequestHistoryServerList(appId, filters, (uint)filters.Length, response);
		case SteamServerList.Internet:
			return SteamMatchmakingServers.RequestInternetServerList(appId, filters, (uint)filters.Length, response);
		case SteamServerList.LAN:
			return SteamMatchmakingServers.RequestLANServerList(appId, response);
		case SteamServerList.Spectator:
			return SteamMatchmakingServers.RequestSpectatorServerList(appId, filters, (uint)filters.Length, response);
		default:
			throw new Exception("\"listType\" must not be unknown.");
		}
	}

	private void ProcessLanServers()
	{
		if (servers.Count == 0)
		{
			IPAddress address = IPAddress.Parse(ConnectAddress);
			if (serverPingRequest.m_HServerQuery != -1)
			{
				SteamMatchmakingServers.CancelServerQuery(serverPingRequest);
			}
			serverPingRequest = ConnectTo(address.ToUint(), (ushort)serverPort);
			return;
		}
		CSteamID cSteamID = CSteamID.Nil;
		foreach (gameserveritem_t server in servers)
		{
			string connectionAddressString = server.m_NetAdr.GetConnectionAddressString();
			string text = connectionAddressString.Split(':')[0];
			if (server.m_NetAdr.GetConnectionPort() == connectPort)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.LogFormat("Checking server host {0}, checking with our target: {1}:{2}, isTargetLocal={3}, isFoundServerLocal={4}", connectionAddressString, connectAddress, connectPort, IPAddressHelper.IsLocalhost(connectAddress), IPAddressHelper.IsLocalhost(text));
				}
				if (IPAddressHelper.IsLocalhost(connectAddress) && IPAddressHelper.IsLocalhost(text))
				{
					cSteamID = server.m_steamID;
					break;
				}
				if (IPAddressHelper.IsInternal(connectAddress) && IPAddressHelper.IsInternal(text) && connectAddress.Equals(text))
				{
					cSteamID = server.m_steamID;
					break;
				}
			}
		}
		if (cSteamID.IsValid())
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log(string.Concat("[", cSteamID, "] Found our desired server: ", cSteamID));
			}
			ConnectTo(cSteamID);
		}
		else
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log("Failed to find a matching server in the lan serverlist, trying the lobbies");
			}
			FindServerLobby();
		}
	}

	private void FindServerLobby()
	{
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log("Trying to get list of available lobbies ...");
		}
		SetClientConnectionState(ClientConnectionState.FindingLobby);
		string externalIP = connectAddress;
		if (IPAddressHelper.IsInternal(connectAddress) || IPAddressHelper.IsLocalhost(connectAddress))
		{
			externalIP = StatMaster.ExternalIP;
		}
		SteamMatchmaking.AddRequestLobbyListStringFilter("publicIP", externalIP, ELobbyComparison.k_ELobbyComparisonEqual);
		SteamMatchmaking.AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter.k_ELobbyDistanceFilterWorldwide);
		SteamMatchmaking.RequestLobbyList();
	}

	private bool MatchesHost(string publicIP, string[] internalIPs, int port)
	{
		if (port != connectPort)
		{
			return false;
		}
		if (publicIP == connectAddress)
		{
			return true;
		}
		if (publicIP == StatMaster.ExternalIP && IPAddressHelper.IsInternal(connectAddress) && (internalIPs.Contains(connectAddress) || connectAddress == "127.0.0.1"))
		{
			return true;
		}
		return false;
	}

	private bool MatchLobby(CSteamID lobbyID)
	{
		string lobbyData = SteamMatchmaking.GetLobbyData(lobbyID, "publicIP");
		string lobbyData2 = SteamMatchmaking.GetLobbyData(lobbyID, "internalIPs");
		string[] internalIPs = lobbyData2.Split('|');
		string lobbyData3 = SteamMatchmaking.GetLobbyData(lobbyID, "port");
		int result = 0;
		int.TryParse(lobbyData3, out result);
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("MatchLobby[{0}] publicIp={1}, internalIp={2}, hostPort={3}, connectAddress={4}, connectPort={5}", lobbyID, lobbyData, lobbyData2, lobbyData3, connectAddress, connectPort);
		}
		return MatchesHost(lobbyData, internalIPs, result);
	}

	protected virtual void InitializeClient()
	{
		m_CallbackLobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
		m_CallbackLobbyList = Callback<LobbyMatchList_t>.Create(OnGetLobbiesList);
		m_CallbackP2PSessionConnectFailClient = Callback<P2PSessionConnectFail_t>.Create(OnP2PSessionConnectFail);
		matchmakingPingResponse = new ISteamMatchmakingPingResponse(MatchmakingPing_ServerResponded, MatchmakingPing_ServerFailedToRespond);
		m_CallbackLobbyChatMsg = Callback<LobbyChatMsg_t>.Create(OnLobbyChatMessage);
		clientGotKicked = false;
		startDateTime = DateTime.Now;
		NetworkStats.Clear();
		lastAckSent = 0f;
		pingAvg.Clear();
		StartPacketUpdate();
	}

	private void MatchmakingPing_ServerResponded(gameserveritem_t gameServer)
	{
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log("Got a ping response from the target server!");
		}
		ConnectTo(gameServer.m_steamID);
	}

	private void MatchmakingPing_ServerFailedToRespond()
	{
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log("Failed to ping the target server");
		}
		FindServerLobby();
	}

	private void JoinLobby(CSteamID lobbySteamId)
	{
		LeaveLobby();
		SetClientConnectionState(ClientConnectionState.JoiningLobby);
		if (!lobbySteamId.IsValid())
		{
			if (BesiegeLogFilter.logDebug)
			{
				Debug.LogError("Tried to join a steam lobby with an invalid id");
			}
			Disconnect();
			return;
		}
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("Joining lobby {0}", lobbySteamId);
		}
		SteamMatchmaking.JoinLobby(lobbySteamId);
	}

	private bool IsLocalLobbyOwnerAsClient()
	{
		if (isHosting)
		{
			return false;
		}
		string lobbyData = SteamMatchmaking.GetLobbyData(currentSteamLobbyID, "publicIP");
		if (lobbyData != StatMaster.ExternalIP)
		{
			return false;
		}
		if (SteamMatchmaking.GetLobbyOwner(currentSteamLobbyID).m_SteamID == SteamUser.GetSteamID().m_SteamID)
		{
			return true;
		}
		return false;
	}

	private void LeaveLobby()
	{
		if (currentSteamLobbyID.IsValid())
		{
			if (IsLocalLobbyOwnerAsClient())
			{
				currentSteamLobbyID = CSteamID.Nil;
				return;
			}
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("Leaving current lobby...");
			}
			SteamMatchmaking.LeaveLobby(currentSteamLobbyID);
		}
		currentSteamLobbyID = CSteamID.Nil;
	}

	private void OnLobbyEntered(LobbyEnter_t result)
	{
		if (result.m_EChatRoomEnterResponse != 1)
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogError(string.Concat("Could not join lobby(", currentSteamLobbyID, "), error: ", result.m_EChatRoomEnterResponse));
			}
			currentSteamLobbyID = CSteamID.Nil;
			SetClientConnectionState(ClientConnectionState.FailedToJoinLobby);
			return;
		}
		if (currentSteamLobbyID.Equals(result.m_ulSteamIDLobby))
		{
			Debug.LogWarning("Already entered this lobby, not sure why it's trying to enter again.");
			return;
		}
		currentSteamLobbyID = (CSteamID)result.m_ulSteamIDLobby;
		if (!isHosting)
		{
			SetClientConnectionState(ClientConnectionState.LobbyJoined);
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log(string.Concat("Lobby(", currentSteamLobbyID, ") joined!"));
			}
			JoinServerOnLobbyEnter();
		}
	}

	private void JoinServerOnLobbyEnter()
	{
		if (isHosting)
		{
			return;
		}
		uint punGameServerIP;
		ushort punGameServerPort;
		CSteamID psteamIDGameServer;
		if (SteamMatchmaking.GetLobbyGameServer(currentSteamLobbyID, out punGameServerIP, out punGameServerPort, out psteamIDGameServer))
		{
			if (!psteamIDGameServer.IsValid())
			{
				Debug.LogWarning("Lobby steam server id is invalid, this should not happen.");
				Disconnect();
			}
			else
			{
				connectAddress = SteamMatchmaking.GetLobbyData(currentSteamLobbyID, "publicIP");
				connectPort = int.Parse(SteamMatchmaking.GetLobbyData(currentSteamLobbyID, "port"));
				ConnectTo(psteamIDGameServer);
			}
		}
		else
		{
			Debug.LogWarning("Lobby has no gameserver, can not connect...");
		}
	}

	private void OnGetLobbiesList(LobbyMatchList_t result)
	{
		CSteamID cSteamID = CSteamID.Nil;
		for (int i = 0; i < result.m_nLobbiesMatching; i++)
		{
			CSteamID lobbyByIndex = SteamMatchmaking.GetLobbyByIndex(i);
			if (MatchLobby(lobbyByIndex))
			{
				cSteamID = lobbyByIndex;
				break;
			}
		}
		if (cSteamID == CSteamID.Nil)
		{
			SetClientConnectionState(ClientConnectionState.LobbyNotFound);
		}
		else
		{
			JoinLobby(cSteamID);
		}
	}

	private void ConnectTo(CSteamID serverId)
	{
		if (BesiegeLogFilter.logDebug)
		{
			Debug.LogFormat("Connecting to serverId: {0}", serverId);
		}
		_ConnectedTo = serverId;
		SendConnectHandshake();
	}

	private void SendConnectHandshake()
	{
		CSteamID connectedTo = _ConnectedTo;
		SetClientConnectionState(ClientConnectionState.Connecting);
		isTimingOut = true;
		nextReconnectTime = Time.realtimeSinceStartup + 10f + OptionsMaster.BesiegeConfig.ReconnectTimeout;
		SendServerHandshake(connectedTo);
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log("Handshake message sent to server...");
		}
	}

	private HServerQuery ConnectTo(uint unIP, ushort usPort)
	{
		return SteamMatchmakingServers.PingServer(unIP, usPort, matchmakingPingResponse);
	}

	protected virtual void HandleClientPing(CSteamID steamIDRemote, double packetLastClientTS, double lastClientPing)
	{
		if (acceptedConnections.ContainsKey(steamIDRemote))
		{
			SteamSession steamSession = acceptedConnections[steamIDRemote];
			steamSession.LastClientTS = packetLastClientTS;
			steamSession.Ping = lastClientPing;
			SendClientPing(steamIDRemote);
		}
	}

	private void SendClientPing(CSteamID steamIDRemote)
	{
		SteamSession steamSession = acceptedConnections[steamIDRemote];
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				binaryWriter.Write(64250L);
				binaryWriter.Write(steamSession.LastClientTS);
				SendCustomReliableMessage(steamIDRemote, memoryStream.ToArray());
			}
		}
	}

	private void SendServerPing()
	{
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				binaryWriter.Write(64250L);
				binaryWriter.Write(NetworkStats.NetworkTimeMS);
				binaryWriter.Write(ping);
				SendCustomReliableMessage(_ConnectedTo, memoryStream.ToArray());
			}
		}
	}

	private void SendServerHandshake(CSteamID server)
	{
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				binaryWriter.Write(257L);
				binaryWriter.Write(GenerateConnectionString());
				binaryWriter.Write(SteamFriends.GetPersonaName());
				SendServerMessageReliable(server, memoryStream.ToArray(), (int)memoryStream.Length, 0);
			}
		}
	}

	private void OnP2PSessionRequestClient(P2PSessionRequest_t callback)
	{
		SteamNetworking.AcceptP2PSessionWithUser(callback.m_steamIDRemote);
	}

	private void OnP2PSessionConnectFailClient(P2PSessionConnectFail_t callback)
	{
		if (clientConnectionState == ClientConnectionState.Connected)
		{
			Disconnect(LocalisationManager.GetTranslation(2023));
		}
	}

	private void SteamMatchmakingServerListResponse_ServerResponded(HServerListRequest request, int serverId)
	{
		gameserveritem_t serverDetails = SteamMatchmakingServers.GetServerDetails(request, serverId);
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("Found Server. Server Name: {0} Server Passworded: {1} Server Ping: {2}, Server host: {3}", serverDetails.GetServerName(), serverDetails.m_bPassword, serverDetails.m_nPing, serverDetails.m_NetAdr.GetConnectionAddressString());
		}
		servers.Add(serverDetails);
	}

	private void SteamMatchmakingServerListResponse_OnRefreshComplete(HServerListRequest request, EMatchMakingServerResponse response, SteamServerList type)
	{
		if (type == SteamServerList.LAN)
		{
			ProcessLanServers();
		}
	}

	private void SteamMatchmakingServerListResponse_ServerFailedToResponded(HServerListRequest request, int serverId)
	{
		Disconnect(LocalisationManager.GetTranslation(2010));
	}

	private string GenerateConnectionString()
	{
		_GenerateString = string.Empty;
		for (int i = 0; i < 5; i++)
		{
			_GenerateString += "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"[UnityEngine.Random.Range(0, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".Length)];
		}
		return _GenerateString;
	}

	private void NetworkingThreadUpdateClient()
	{
		if (_ConnectedTo.IsValid())
		{
			SendKeepAlive();
		}
		uint pcubMsgSize;
		while (SteamNetworking.IsP2PPacketAvailable(out pcubMsgSize))
		{
			SteamPacket steamPacket = new SteamPacket();
			steamPacket.Buffer = new byte[pcubMsgSize];
			steamPacket.Channel = 0;
			if (!SteamNetworking.ReadP2PPacket(steamPacket.Buffer, pcubMsgSize, out steamPacket.BufferSize, out steamPacket.SteamID) || steamPacket.SteamID != _ConnectedTo)
			{
				break;
			}
			NetworkStats.IncrementBytesReceived(pcubMsgSize);
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(steamPacket.Buffer)))
			{
				switch (binaryReader.ReadInt64())
				{
				case 257L:
					if (binaryReader.ReadString() == _GenerateString)
					{
						if (BesiegeLogFilter.logDev)
						{
							Debug.LogFormat("Handshake with server complete, awaiting Init message.");
						}
						isTimingOut = false;
					}
					break;
				case 64250L:
				{
					float num = (float)binaryReader.ReadDouble();
					HandleServerPing(num);
					break;
				}
				}
			}
		}
		foreach (int channel in channels)
		{
			if (channel == 0)
			{
				continue;
			}
			while (SteamNetworking.IsP2PPacketAvailable(out pcubMsgSize, channel))
			{
				SteamPacket steamPacket = new SteamPacket();
				steamPacket.Buffer = new byte[pcubMsgSize];
				steamPacket.Channel = channel;
				if (!SteamNetworking.ReadP2PPacket(steamPacket.Buffer, pcubMsgSize, out steamPacket.BufferSize, out steamPacket.SteamID, channel) || steamPacket.SteamID != _ConnectedTo)
				{
					break;
				}
				NetworkStats.IncrementBytesReceived(pcubMsgSize);
				if (channel == customReliableChannel)
				{
					HandleCustomReliableMessageClient(steamPacket);
				}
				else
				{
					receiveQueue.Enqueue(steamPacket);
				}
			}
		}
	}

	private void HandleCustomReliableMessageClient(SteamPacket packet)
	{
		ReliableSteamMessage msg;
		if (!HandleCustomReliableMessage(packet, out msg))
		{
			return;
		}
		using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(msg.Data)))
		{
			long num = binaryReader.ReadInt64();
			if (num == 64250)
			{
				float num2 = (float)binaryReader.ReadDouble();
				HandleServerPing(num2);
			}
		}
	}

	private void HandleServerPing(double packetLastClientTS)
	{
		ping = pingAvg.Average;
	}

	private void UpdateClient()
	{
		if (!_ConnectedTo.IsValid())
		{
			return;
		}
		if (CheckConnectTimeout())
		{
			Disconnect(LocalisationManager.GetTranslation(2010));
			return;
		}
		int count = receiveQueue.Count;
		while (count-- > 0)
		{
			SteamPacket result;
			if (receiveQueue.TryDequeue(out result))
			{
				connectionHandler.OnDataEvent(0, result.Channel, result.Buffer, (int)result.BufferSize);
			}
		}
		if (updatePackets)
		{
			SteamAPI.RunCallbacks();
			NetworkingThreadUpdateClient();
		}
	}

	private void StartPacketUpdate()
	{
		networkFrame = 0u;
		updatePackets = true;
	}

	private int GetNextChannelId()
	{
		int i;
		for (i = 0; channelQosTypes.ContainsKey(i); i++)
		{
		}
		return i;
	}

	private void UpdatePlayerStats()
	{
		foreach (NetworkStatsCounter value in ClientStats.Values)
		{
			value.Update();
		}
	}

	private void UpdateNetworkStats()
	{
		NetworkStats.Update();
		if (StatMaster.ShowNetworkStats && (serverConnectionState != ServerConnectionState.Disconnected || clientConnectionState != ClientConnectionState.Disconnected) && lastStatsDebugPrint + 1f < Time.time)
		{
			NetworkStats.DebugStats();
			lastStatsDebugPrint = Time.time;
		}
		if (isHosting)
		{
			UpdatePlayerStats();
		}
	}

	private void Update()
	{
		UpdateNetworkStats();
		if (serverConnectionState != ServerConnectionState.Disconnected)
		{
			UpdateServer();
		}
		if (clientConnectionState != ClientConnectionState.Disconnected)
		{
			UpdateClient();
		}
		if (updatePackets)
		{
			networkFrame++;
		}
	}

	private void SendKeepAlive()
	{
		if (!(lastAckSent + 0.5f > (float)NetworkTime))
		{
			SendServerPing();
			lastAckSent = (float)NetworkTime;
		}
	}

	private bool CheckConnectTimeout()
	{
		if (!isTimingOut)
		{
			return false;
		}
		if (Time.realtimeSinceStartup >= nextReconnectTime)
		{
			if (++connectAttempt > OptionsMaster.BesiegeConfig.MaxReconnectAttempts)
			{
				if (prevClientConnectionState == ClientConnectionState.AttemptingDirectConnect)
				{
					SetClientConnectionState(ClientConnectionState.DirectConnectFailed);
					return true;
				}
				Disconnect(LocalisationManager.GetTranslation(2023));
				return true;
			}
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("Reconnecting attempt " + connectAttempt + "/" + OptionsMaster.BesiegeConfig.MaxReconnectAttempts);
			}
			nextReconnectTime = Time.realtimeSinceStartup + 10f + OptionsMaster.BesiegeConfig.ReconnectTimeout;
			SendConnectHandshake();
		}
		return false;
	}

	private void DebugConnectionState(CSteamID client)
	{
		P2PSessionState_t pConnectionState;
		bool flag = ((!isHosting) ? SteamNetworking.GetP2PSessionState(client, out pConnectionState) : SteamGameServerNetworking.GetP2PSessionState(client, out pConnectionState));
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("GetP2PSessionState(m_RemoteSteamId, out ConnectionState) : " + flag + " -- ConnectionState.m_bConnecting={0}, ConnectionState.m_bConnectionActive={1}, ConnectionState.m_bUsingRelay={2}, ConnectionState.m_eP2PSessionError={3}, ConnectionState.m_nBytesQueuedForSend={4}", pConnectionState.m_bConnecting, pConnectionState.m_bConnectionActive, pConnectionState.m_bUsingRelay, pConnectionState.m_eP2PSessionError, pConnectionState.m_nBytesQueuedForSend);
		}
	}

	private void CheckExpiredConnections()
	{
		CleanupDeadConnections(false);
	}

	private void CleanupDeadConnections(bool forceQuit)
	{
		List<CSteamID> list = new List<CSteamID>();
		foreach (KeyValuePair<CSteamID, SteamSession> deadConnection in deadConnections)
		{
			CSteamID key = deadConnection.Key;
			SteamSession value = deadConnection.Value;
			if (BesiegeLogFilter.logDebug)
			{
				Debug.LogFormat("Cleaning up dead connection {0}, IdleTime={1}", key, ClientStats[key].TimeoutTime);
			}
			if (!forceQuit)
			{
				connectionHandler.OnPlayerLeave(value.ConnectionID);
			}
			SendLobbyClientKick(key);
			steamIDLookup.Remove(value.ConnectionID);
			SteamGameServerNetworking.CloseP2PSessionWithUser(key);
			if (ClientStats.ContainsKey(key))
			{
				NetworkStatsCounter item = ClientStats[key];
				ClientStatsList.Remove(item);
				ClientStats.Remove(key);
			}
			list.Add(key);
		}
		for (int i = 0; i < list.Count; i++)
		{
			deadConnections.Remove(list[i]);
		}
	}

	private void HandleClientHandshake(CSteamID steamIDRemote, string connectionString, string username)
	{
		if (!steamConnections.Contains(steamIDRemote) && acceptedConnections.ContainsKey(steamIDRemote))
		{
			ushort playerId = CreateConnectionId(steamIDRemote);
			SteamSession steamSession = acceptedConnections[steamIDRemote];
			steamSession.Username = username;
			steamSession.PlayerStatus = SessionPlayerStatus.InServer;
			steamSession.ConnectionID = playerId;
			RegisterConnection(steamIDRemote, steamSession);
			DebugConnectionState(steamIDRemote);
			SendClientHandshake(steamIDRemote, connectionString);
			connectionHandler.OnPlayerJoin(playerId);
			P2PSessionState_t pConnectionState;
			if (SteamGameServerNetworking.GetP2PSessionState(steamIDRemote, out pConnectionState) && pConnectionState.m_bUsingRelay == 1)
			{
				string message = string.Format("Player {0} is using a relay server. The connection/performance may be lacking a bit.", username);
				NetworkAuxAddPiece.Instance.SendConsolePrint(message);
			}
		}
	}

	private ushort CreateConnectionId(CSteamID steamId)
	{
		ushort num = 1;
		while (true)
		{
			bool flag = false;
			foreach (SteamSession value in acceptedConnections.Values)
			{
				if (num == value.ConnectionID)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				num++;
				continue;
			}
			break;
		}
		return num;
	}

	private bool SendServerMessageReliable(CSteamID server, byte[] buffer, int size, int channel)
	{
		return SendServerMessage(server, buffer, size, channel, EP2PSend.k_EP2PSendReliable);
	}

	private bool SendServerMessage(CSteamID server, byte[] buffer, int size, int channel, EP2PSend qosType)
	{
		NetworkStats.IncrementBytesSent((uint)size);
		return SteamNetworking.SendP2PPacket(server, buffer, (uint)size, qosType, channel);
	}

	private bool SendClientMessageReliable(CSteamID player, byte[] buffer, int size, int channel)
	{
		return SendClientMessage(player, buffer, size, channel, EP2PSend.k_EP2PSendReliable);
	}

	private bool SendClientMessageNoDelay(CSteamID player, byte[] buffer, int size, int channel)
	{
		return SendClientMessage(player, buffer, buffer.Length, channel, EP2PSend.k_EP2PSendUnreliableNoDelay);
	}

	private bool SendClientMessage(CSteamID player, byte[] buffer, int size, int channel, EP2PSend qosType)
	{
		NetworkStats.IncrementBytesSent((uint)size);
		ClientStats[player].IncrementBytesSent((uint)size);
		return SteamGameServerNetworking.SendP2PPacket(player, buffer, (uint)size, qosType, channel);
	}

	private void SendNetworkMessage(CSteamID player, int channel, byte[] data)
	{
		if (isHosting)
		{
			SendClientMessage(player, data, data.Length, channel, channelQosTypes[channel]);
		}
		else
		{
			SendServerMessage(player, data, data.Length, channel, channelQosTypes[channel]);
		}
	}

	public override int Ping()
	{
		return (int)ping;
	}

	public override void SendNetworkMessage(ushort playerId, int channel, byte[] data)
	{
		if (isHosting)
		{
			if (steamIDLookup.ContainsKey(playerId))
			{
				SendNetworkMessage(steamIDLookup[playerId].SteamID, channel, data);
			}
		}
		else
		{
			SendNetworkMessage(_ConnectedTo, channel, data);
		}
	}

	public override void SetPlayerID(ushort id, ulong lobbyId)
	{
		isTimingOut = false;
		networkID = id;
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Got SetPlayerID(Init) message from the server, officially connected!");
		}
		SetClientConnectionState(ClientConnectionState.Connected);
		connectionHandler.OnConnected();
	}

	public override void ShutdownClient()
	{
		if (clientConnectionState == ClientConnectionState.Disconnected)
		{
			return;
		}
		updatePackets = false;
		ClearReceiveQueue();
		unackedMessages.Clear();
		isTimingOut = false;
		if (_ConnectedTo.IsValid())
		{
			if (!clientGotKicked)
			{
				SendLobbyClientDisconnect();
			}
			SteamNetworking.CloseP2PSessionWithUser(_ConnectedTo);
		}
		FinalizeLobbyShutdown();
		_ConnectedTo = CSteamID.Nil;
		if (serverlistRequest.m_HServerListRequest != IntPtr.Zero)
		{
			SteamMatchmakingServers.CancelQuery(serverlistRequest);
			serverlistRequest = HServerListRequest.Invalid;
		}
		if (serverPingRequest.m_HServerQuery != -1)
		{
			SteamMatchmakingServers.CancelServerQuery(serverPingRequest);
			serverPingRequest = HServerQuery.Invalid;
		}
		m_CallbackLobbyEnter.Dispose();
		m_CallbackLobbyList.Dispose();
		m_CallbackLobbyChatMsg.Dispose();
		m_CallbackP2PSessionConnectFailClient.Dispose();
		matchmakingPingResponse = null;
	}

	private void FinalizeLobbyShutdown()
	{
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log("FinalizeLobbyShutdown, isQuitting: " + isQuitting);
		}
		if (isQuitting)
		{
			LeaveLobby();
		}
		else
		{
			StartCoroutine(FinalizeLobbyShutdownIE());
		}
	}

	private void SendLobbyClientDisconnect()
	{
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log("[Client] Sending disconnect msg");
				}
				binaryWriter.Write(65535L);
				binaryWriter.Write(64506L);
				byte[] array = memoryStream.ToArray();
				SteamMatchmaking.SendLobbyChatMsg(currentSteamLobbyID, array, array.Length);
			}
		}
	}

	private void RemovePlayers()
	{
		List<CSteamID> list = acceptedConnections.Keys.ToList();
		for (int i = 0; i < list.Count(); i++)
		{
			P2PRemove(list[i]);
		}
		CleanupDeadConnections(true);
		if (deadConnections.Count > 0)
		{
			Debug.LogWarning("NOT ALL PLAYERS ARE REMOVED, FIX THIS");
		}
	}

	public override void ShutdownServer()
	{
		if (serverConnectionState != ServerConnectionState.Disconnected)
		{
			updatePackets = false;
			RemovePlayers();
			ClearLobbyData();
			ClearReceiveQueue();
			unackedMessages.Clear();
			steamConnections.Clear();
			acceptedConnections.Clear();
			deadConnections.Clear();
			steamIDLookup.Clear();
			m_CallbackSteamServersConnected.Dispose();
			m_CallbackSteamServersConnectFailure.Dispose();
			m_CallbackSteamServersDisconnected.Dispose();
			m_CallbackP2PSessionRequest.Dispose();
			m_CallbackSteamLobbyCreated.Dispose();
			m_CallbackLobbyChatMsg.Dispose();
			m_CallbackP2PSessionConnectFailServer.Dispose();
			if (steamConnected)
			{
				SteamGameServer.LogOff();
			}
			if (serverInitialized)
			{
				SteamGameServer.EnableHeartbeats(false);
				GameServer.Shutdown();
			}
			FinalizeLobbyShutdown();
			serverInitialized = false;
		}
	}

	private IEnumerator FinalizeLobbyShutdownIE()
	{
		yield return new WaitForSeconds(1f);
		LeaveLobby();
	}

	public void Stop(string errorMessage)
	{
		connectionHandler.SetErrorMessage(errorMessage);
		Stop();
	}

	public override void Stop()
	{
		if (serverConnectionState != ServerConnectionState.Disconnected)
		{
			ShutdownServer();
			SetServerConnectionState(ServerConnectionState.Disconnected);
			connectionHandler.OnDisconnected(false);
		}
	}

	protected override void SetServerConnectionState(ServerConnectionState newState)
	{
		base.SetServerConnectionState(newState);
		ServerConnectionState serverConnectionState = base.serverConnectionState;
		if (serverConnectionState == ServerConnectionState.InitializationFailed)
		{
			Stop(LocalisationManager.GetTranslation(2022));
		}
	}

	protected override void SetClientConnectionState(ClientConnectionState newState)
	{
		base.SetClientConnectionState(newState);
		switch (clientConnectionState)
		{
		case ClientConnectionState.FailedToJoinLobby:
			Disconnect(LocalisationManager.GetTranslation(2015));
			break;
		case ClientConnectionState.HostNotFound:
		case ClientConnectionState.LobbyNotFound:
			Disconnect(LocalisationManager.GetTranslation(2010));
			break;
		}
	}

	public override void Shutdown()
	{
		connectionHandler.SetErrorMessage(string.Empty);
		base.Shutdown();
	}

	private uint GetMessageID()
	{
		if (messageID == uint.MaxValue)
		{
			messageID = 0u;
		}
		else
		{
			messageID++;
		}
		return messageID;
	}

	private bool HandleCustomReliableMessage(SteamPacket packet, out ReliableSteamMessage msg)
	{
		msg = ReliableSteamMessage.From(packet.SteamID, packet.Buffer);
		if (unackedMessages.Contains(msg))
		{
			double num = NetworkStats.NetworkTimeMS - (double)msg.Timestamp;
			pingAvg += num;
			unackedMessages.Remove(msg);
			return false;
		}
		if (msg.Data == null)
		{
			return false;
		}
		ReliableSteamMessage reliableSteamMessage = new ReliableSteamMessage(msg.MessageID, msg.Timestamp, msg.Frame, null, packet.SteamID);
		SendUnreliableMessage(packet.SteamID, reliableSteamMessage.GetBytes());
		return true;
	}

	private void SendUnacknoledgedMessages()
	{
		List<ReliableSteamMessage> list = new List<ReliableSteamMessage>();
		foreach (ReliableSteamMessage unackedMessage in unackedMessages)
		{
			if (NetworkStats.NetworkTimeMS - (double)unackedMessage.Timestamp > 3000.0)
			{
				list.Add(unackedMessage);
				continue;
			}
			unackedMessage.Timestamp = (uint)NetworkStats.NetworkTimeMS;
			SendUnreliableMessage(unackedMessage.SteamID, unackedMessage.GetBytes());
		}
		foreach (ReliableSteamMessage item in list)
		{
			unackedMessages.Remove(item);
		}
	}

	private void SendCustomReliableMessage(CSteamID steamIDRemote, byte[] buffer)
	{
		ReliableSteamMessage reliableSteamMessage = new ReliableSteamMessage(GetMessageID(), (uint)NetworkStats.NetworkTimeMS, networkFrame, buffer, steamIDRemote);
		unackedMessages.Add(reliableSteamMessage);
		SendUnreliableMessage(steamIDRemote, reliableSteamMessage.GetBytes());
	}

	private void SendUnreliableMessage(CSteamID steamIDRemote, byte[] buffer)
	{
		if (isHosting)
		{
			SendClientMessage(steamIDRemote, buffer, buffer.Length, customReliableChannel, EP2PSend.k_EP2PSendUnreliableNoDelay);
		}
		else
		{
			SendServerMessage(steamIDRemote, buffer, buffer.Length, customReliableChannel, EP2PSend.k_EP2PSendUnreliableNoDelay);
		}
	}

	private void StopPacketUpdate()
	{
		updatePackets = false;
	}

	private void ClearReceiveQueue()
	{
		while (receiveQueue.Count > 0)
		{
			SteamPacket result;
			receiveQueue.TryDequeue(out result);
		}
	}
}
