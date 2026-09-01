using System;
using System.Collections;
using System.Collections.Generic;
using Besiege.Networking;
using Localisation;
using Open.Nat;
using UnityEngine;
using UnityEngine.Networking;

public class UnetConnection : BaseConnection
{
	private const string GametypeName = "BesiegeMPServer";

	[SerializeField]
	private GlobalConfig globalConfig;

	[SerializeField]
	private ConnectionConfig connectionCfg;

	[SerializeField]
	private bool networkSimulation;

	private bool isInitialized;

	private bool isTimingOut;

	private int bufferSize = 1500;

	private int joinError;

	private int natConnectPort;

	private int natListenPort;

	private int socketId = -1;

	private float nextPunchThroughTimeout;

	private float nextReconnectTime;

	private string natConnectAdress;

	private string serverHostName = "Nameless Server";

	private string serverHostString;

	private HostTopology hostTopology;

	private ConnectionSimulatorConfig simulationCfg;

	private ServerHost foundHost;

	private ExtendedNATHelper natHelper;

	private byte[] messageBuffer;

	private List<ushort> playerList;

	private List<int> clientSocketIds;

	protected virtual bool IsLAN
	{
		get
		{
			return false;
		}
	}

	public override string CurrentNetwork
	{
		get
		{
			return (!StatMaster.isHosting) ? OptionsMaster.BesiegeConfig.LastConnectedAddress : BesiegeNetworkManager.Instance.ExternalIP;
		}
	}

	public override int AddChannel(BesiegeQosType qosType)
	{
		QosType value;
		switch (qosType)
		{
		case BesiegeQosType.AllCostDelivery:
			value = QosType.AllCostDelivery;
			break;
		case BesiegeQosType.Reliable:
			value = QosType.Reliable;
			break;
		case BesiegeQosType.Unreliable:
			value = QosType.Unreliable;
			break;
		default:
			value = QosType.Reliable;
			break;
		}
		return connectionCfg.AddChannel(value);
	}

	public override void BroadcastMessage(int channel, byte[] data)
	{
		for (int i = 0; i < playerList.Count; i++)
		{
			ushort num = playerList[i];
			if (num != networkID)
			{
				SendNetworkMessage(num, channel, data);
			}
		}
	}

	public override void ConnectPlayfab(string pfNetworkId)
	{
		Debug.LogError("[UnetConnection] ConnectPlayfab is not supported");
	}

	public override void ConnectToIP(string serverAddress, int serverPort)
	{
		ResetStats();
		InitTransport();
		hostTopology = new HostTopology(connectionCfg, OptionsMaster.maxPlayersPerHost);
		connectAddress = serverAddress;
		connectPort = serverPort;
		if (clientConnectionState == ClientConnectionState.Disconnected || clientConnectionState == ClientConnectionState.LobbyJoined)
		{
			if (BesiegeLogFilter.logInfo)
			{
				Debug.Log("Attempting to connect to the server directly");
			}
			SetClientConnectionState(ClientConnectionState.AttemptingDirectConnect);
		}
		if (clientConnectionState == ClientConnectionState.AttemptingDirectConnect)
		{
			ConnectToServer(serverAddress, serverPort, serverPort, false);
			return;
		}
		if (clientConnectionState == ClientConnectionState.DirectConnectFailed)
		{
			StartCoroutine(FindNATHost(serverAddress, serverPort));
			return;
		}
		if (clientConnectionState == ClientConnectionState.LobbyNotFound)
		{
			StartCoroutine(FindNATHost(serverAddress, serverPort));
			return;
		}
		Debug.LogWarning("Not expecting clientConnection state: " + clientConnectionState);
		Stop();
	}

	public override void ConnectToLobby(ulong lobbyId)
	{
		Debug.LogError("[UnetConnection] ConnectToLobby is not supported");
	}

	public override void Disconnect()
	{
		ShutdownClient();
		SetClientConnectionState(ClientConnectionState.Disconnected);
		connectionHandler.OnDisconnected(false);
	}

	public override void DisconnectPlayer(ushort playerId)
	{
		if (StatMaster.isHosting)
		{
			ushort socket;
			if (clientSocketIds.Contains(playerId))
			{
				socket = playerId;
				playerId = 1;
				clientSocketIds.Remove(playerId);
			}
			else
			{
				socket = (ushort)socketId;
			}
			DisconnectSocket(socket, playerId);
		}
	}

	public override int GetPing(ushort playerId)
	{
		ushort hostId;
		if (clientSocketIds.Contains(playerId))
		{
			hostId = playerId;
			playerId = 1;
		}
		else
		{
			hostId = (ushort)socketId;
		}
		byte error;
		int currentRtt = NetworkTransport.GetCurrentRtt(hostId, (!StatMaster.isHosting) ? connectionID : playerId, out error);
		if (error != 0)
		{
			return 999;
		}
		return currentRtt;
	}

	public override bool Listen(int serverPort)
	{
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Hosting server on port: " + serverPort);
		}
		SetServerConnectionState(ServerConnectionState.InitializingHost);
		InitTransport();
		hostTopology = new HostTopology(connectionCfg, OptionsMaster.maxPlayersPerHost);
		if (!IsLAN && NetworkScene.ServerSettings.useUPNP)
		{
			natHelper.RemoveAllPortMappings();
			natHelper.mapPort(serverPort, serverPort, 0, Besiege.Networking.Protocol.Udp, "Besiege", OnPortMappingDone);
		}
		int num = serverPort;
		while (socketId == -1)
		{
			socketId = NetworkTransport.AddHost(hostTopology, num);
			if (socketId != -1)
			{
				break;
			}
			Debug.LogWarning("Server port already in use, picking a higher port...");
			num++;
		}
		if (num != serverPort)
		{
			Debug.Log("Hosting server on a different port: " + serverPort);
			serverPort = num;
		}
		connectAddress = StatMaster.ExternalIP;
		connectPort = serverPort;
		playerList.Clear();
		serverHostName = "Nameless Server";
		if (!IsLAN)
		{
			InitStubServer();
			StartCoroutine(natHelper.startListeningForPunchthrough(OnHolePunchedServer));
			if (StatMaster.FacilitatorGUID != 0L)
			{
				RegisterMasterserver();
			}
		}
		SetServerConnectionState(ServerConnectionState.WaitingForConnection);
		networkID = 0;
		connectionHandler.OnConnected();
		connectionHandler.OnPlayerJoin(networkID);
		return true;
	}

	public override void SendNetworkMessage(ushort playerId, int channel, byte[] data)
	{
		if (socketId != -1)
		{
			ushort hostId;
			if (clientSocketIds.Contains(playerId))
			{
				hostId = playerId;
				playerId = 1;
			}
			else
			{
				hostId = (ushort)socketId;
			}
			byte error;
			NetworkTransport.Send(hostId, (!StatMaster.isHosting) ? connectionID : playerId, channel, data, data.Length, out error);
			connectionHandler.IncrementSentMessages();
			trafficOut += (ulong)data.Length;
		}
	}

	public override void SetPlayerID(ushort id, ulong lobbyId)
	{
		networkID = id;
		isTimingOut = false;
		SetClientConnectionState(ClientConnectionState.Connected);
		connectionHandler.OnConnected();
	}

	public override int Ping()
	{
		byte error;
		return (clientConnectionState == ClientConnectionState.Connected && !StatMaster.isHosting) ? NetworkTransport.GetCurrentRtt(socketId, connectionID, out error) : 0;
	}

	public override void Stop()
	{
		ShutdownServer();
		SetServerConnectionState(ServerConnectionState.Disconnected);
		connectionHandler.OnDisconnected(false);
	}

	private void CreateGlobalConfig()
	{
		globalConfig = new GlobalConfig();
		globalConfig.ThreadAwakeTimeout = 1u;
		globalConfig.ReactorModel = ReactorModel.SelectReactor;
		globalConfig.ReactorMaximumReceivedMessages = 1024;
		globalConfig.ReactorMaximumSentMessages = 1024;
		globalConfig.MaxPacketSize = 1480;
	}

	private void CreateConnectionConfig()
	{
		connectionCfg = new ConnectionConfig();
		connectionCfg.PacketSize = 1480;
		connectionCfg.FragmentSize = 500;
		connectionCfg.ResendTimeout = 300u;
		connectionCfg.DisconnectTimeout = 5000u;
		connectionCfg.ConnectTimeout = 5000u;
		connectionCfg.MinUpdateTimeout = 10u;
		connectionCfg.PingTimeout = 1000u;
		connectionCfg.ReducedPingTimeout = 800u;
		connectionCfg.AllCostTimeout = 20u;
		connectionCfg.NetworkDropThreshold = 50;
		connectionCfg.OverflowDropThreshold = 50;
		connectionCfg.MaxConnectionAttempt = 10;
		connectionCfg.AckDelay = 33u;
		connectionCfg.MaxCombinedReliableMessageSize = 100;
		connectionCfg.MaxCombinedReliableMessageCount = 10;
		connectionCfg.MaxSentMessageQueueSize = 200;
		connectionCfg.IsAcksLong = false;
		connectionCfg.UsePlatformSpecificProtocols = false;
		connectionCfg.WebSocketReceiveBufferMaxSize = 0;
	}

	private void DisconnectSocket(int socket, int connection)
	{
		if (socket != -1)
		{
			byte error;
			NetworkTransport.Disconnect(socket, connection, out error);
		}
	}

	public override void Initialize()
	{
		networkID = 0;
		messageBuffer = new byte[bufferSize];
		playerList = new List<ushort>();
		clientSocketIds = new List<int>();
		InitConfigs();
		if (!IsLAN)
		{
			InitNatHelper();
		}
	}

	private void InitConfigs()
	{
		if (globalConfig == null)
		{
			CreateGlobalConfig();
		}
		if (connectionCfg == null)
		{
			CreateConnectionConfig();
		}
	}

	private void InitNatHelper()
	{
		natHelper = (ExtendedNATHelper)NATHelper.singleton;
		natHelper.OnDoneConnectingToFacilitator += OnDoneConnectingToFacilitator;
	}

	private void InitStubServer()
	{
		int num = 49152;
		int max = 65535;
		int num2 = num;
		NetworkConnectionError networkConnectionError = NetworkConnectionError.AlreadyConnectedToAnotherServer;
		if (Network.isServer)
		{
			Network.Disconnect();
		}
		do
		{
			num2 = UnityEngine.Random.Range(num, max);
			try
			{
				networkConnectionError = Network.InitializeServer(0, num2, false);
			}
			catch (Exception)
			{
			}
		}
		while (networkConnectionError != NetworkConnectionError.NoError);
	}

	private void InitTransport()
	{
		if (!isInitialized)
		{
			NetworkTransport.Init(globalConfig);
		}
		isInitialized = true;
	}

	private void ResetStats()
	{
		connectAttempt = 1;
		trafficIn = (trafficOut = 0uL);
		networkID = 0;
		playerList.Clear();
	}

	private bool CheckTimeout()
	{
		if (!isTimingOut)
		{
			return false;
		}
		if (clientConnectionState == ClientConnectionState.PunchingThroughToServer)
		{
			if (Time.time > nextPunchThroughTimeout)
			{
				SetClientConnectionState(ClientConnectionState.HolePunchedFailed);
				return true;
			}
			return false;
		}
		if (Time.realtimeSinceStartup >= nextReconnectTime)
		{
			if (++connectAttempt > OptionsMaster.BesiegeConfig.MaxReconnectAttempts)
			{
				if (prevClientConnectionState == ClientConnectionState.Connecting)
				{
					SetClientConnectionState(ClientConnectionState.DirectConnectFailed);
					return true;
				}
				Timeout();
				return true;
			}
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("Reconnecting attempt " + connectAttempt + "/" + OptionsMaster.BesiegeConfig.MaxReconnectAttempts);
			}
			Reconnect();
			nextReconnectTime = Time.realtimeSinceStartup + OptionsMaster.BesiegeConfig.ReconnectTimeout;
		}
		return false;
	}

	private void ConnectToServer(string address, int serverPort, int clientListenPort, bool isNAT)
	{
		if (BesiegeLogFilter.logInfo)
		{
			Debug.Log("Client connecting to " + address + ":" + serverPort + ", " + ((!isNAT) ? "using direct connect." : "using NAT punchthrough."));
		}
		SetClientConnectionState(ClientConnectionState.Connecting);
		byte error;
		if (networkSimulation)
		{
			simulationCfg = new ConnectionSimulatorConfig(20, 90, 60, 110, 0.08f);
			socketId = NetworkTransport.AddHostWithSimulator(hostTopology, 20, 110);
			connectionID = NetworkTransport.ConnectWithSimulator(socketId, address, serverPort, 0, out error, simulationCfg);
		}
		else
		{
			if (isNAT)
			{
				socketId = NetworkTransport.AddHost(hostTopology, clientListenPort);
			}
			else
			{
				socketId = NetworkTransport.AddHost(hostTopology);
			}
			connectionID = NetworkTransport.Connect(socketId, address, serverPort, 0, out error);
		}
		if (error != 0 || connectionID == 0)
		{
			SetClientConnectionState(ClientConnectionState.HostNotFound);
			return;
		}
		nextReconnectTime = Time.realtimeSinceStartup + OptionsMaster.BesiegeConfig.ReconnectTimeout;
		isTimingOut = true;
	}

	private IEnumerator FindNATHost(string address, int port)
	{
		SetClientConnectionState(ClientConnectionState.ResolvingHost);
		if (LogFilter.logDebug)
		{
			Debug.Log("Trying to resolve NAT host '" + address + "'...");
		}
		MasterServer.ClearHostList();
		MasterServer.RequestHostList("BesiegeMPServer");
		float timeElapsed = 0f;
		while (clientConnectionState == ClientConnectionState.ResolvingHost)
		{
			if (MasterServer.PollHostList().Length != 0)
			{
				SetClientConnectionState(ClientConnectionState.HostListReceived);
				HostData[] serverlist = MasterServer.PollHostList();
				for (int i = 0; i < serverlist.Length; i++)
				{
					if (string.IsNullOrEmpty(serverlist[i].comment))
					{
						continue;
					}
					string[] hostInfo = serverlist[i].comment.Split(":"[0]);
					if (hostInfo.Length != 4)
					{
						Debug.LogWarning("Host data has an invalid length(" + hostInfo.Length + "): " + serverlist[i].comment);
					}
					else if (!(hostInfo[0] != address))
					{
						ulong guid;
						int parsedPort;
						if (!ulong.TryParse(hostInfo[2], out guid) || guid == 0L)
						{
							Debug.LogError("Host data has an invalid GUID: " + hostInfo[2]);
						}
						else if (!int.TryParse(hostInfo[3], out parsedPort) || parsedPort == 0)
						{
							Debug.LogError("Host data has an invalid port: " + hostInfo[3]);
						}
						else if (parsedPort == port)
						{
							foundHost = new ServerHost(hostInfo[0], hostInfo[1], guid, parsedPort);
							break;
						}
					}
				}
				MasterServer.ClearHostList();
			}
			timeElapsed += Time.deltaTime;
			if (timeElapsed >= OptionsMaster.BesiegeConfig.HostResolveTimeout)
			{
				if (LogFilter.logDebug)
				{
					Debug.LogWarning("Host resolvement timed out...");
				}
				break;
			}
			yield return null;
		}
		if (foundHost == null)
		{
			if (LogFilter.logDebug)
			{
				Debug.LogError("No server could be found...");
			}
			SetClientConnectionState(ClientConnectionState.HostNotFound);
			yield break;
		}
		if (BesiegeLogFilter.logInfo)
		{
			Debug.Log("Host(" + address + ") resolved, now connecting over NAT...");
		}
		connectAttempt = 1;
		JoinNAT();
	}

	private void JoinNAT()
	{
		if (LogFilter.logDebug)
		{
			Debug.Log("Attempting to punch through to the server: " + foundHost.GUID);
		}
		SetClientConnectionState(ClientConnectionState.PunchingThroughToServer);
		nextPunchThroughTimeout = Time.time + OptionsMaster.BesiegeConfig.PunchThroughTimeout + 1f;
		isTimingOut = true;
		StartCoroutine(natHelper.punchThroughToServer(foundHost.GUID, OnHolePunchedClient));
	}

	private void OnDoneConnectingToFacilitator(ulong guid)
	{
		if (guid != 0L)
		{
			if (Network.isServer && isInitialized && !IsLAN)
			{
				RegisterMasterserver();
			}
			else if (clientConnectionState == ClientConnectionState.PunchingThroughToServer)
			{
				nextPunchThroughTimeout = Time.time + OptionsMaster.BesiegeConfig.PunchThroughTimeout + 1f;
				isTimingOut = true;
				JoinNAT();
			}
		}
	}

	private void OnHolePunchedClient(int natListenPort, int natConnectPort, bool success)
	{
		if (!success)
		{
			SetClientConnectionState(ClientConnectionState.HolePunchedFailed);
			if (LogFilter.logDebug)
			{
				Debug.LogError("Failed to punch through to the server");
			}
		}
		else
		{
			natConnectAdress = PickCorrectAddressToConnectTo(foundHost.ExternalIP, foundHost.InternalIP);
			this.natConnectPort = natConnectPort;
			this.natListenPort = natListenPort;
			ConnectToServer(natConnectAdress, natConnectPort, natListenPort, true);
		}
	}

	public void OnHolePunchedServer(int natListenPort, ulong clientGUID)
	{
		int item = NetworkTransport.AddHost(hostTopology, natListenPort);
		clientSocketIds.Add(item);
	}

	private void OnPortMappingDone(Mapping mapping, bool wasSuccessful, Exception mappingException)
	{
		if (!wasSuccessful)
		{
			if (BesiegeLogFilter.logInfo)
			{
				Debug.LogWarning("Port forwarding failed for: " + mapping.ToString() + ", " + mappingException.ToString());
			}
		}
		else if (BesiegeLogFilter.logInfo)
		{
			Debug.Log("Port forwarded: " + mapping.ToString());
		}
	}

	private void RegisterMasterserver()
	{
		string text = string.Join(":", new string[4]
		{
			connectAddress,
			Network.player.ipAddress,
			StatMaster.FacilitatorGUID.ToString(),
			connectPort.ToString()
		});
		if (!(serverHostString == text))
		{
			serverHostString = text;
			CancelInvoke("FinializeMasterServerRegistration");
			MasterServer.UnregisterHost();
			Invoke("FinializeMasterServerRegistration", 1f);
		}
	}

	private void FinializeMasterServerRegistration()
	{
		MasterServer.RegisterHost("BesiegeMPServer", serverHostName, serverHostString);
	}

	private string PickCorrectAddressToConnectTo(string hostExternalIP, string hostInternalIP)
	{
		if (!string.IsNullOrEmpty(StatMaster.ExternalIP) && !string.IsNullOrEmpty(hostExternalIP))
		{
			if (hostExternalIP == StatMaster.ExternalIP && !string.IsNullOrEmpty(hostInternalIP))
			{
				if (hostInternalIP == Network.player.ipAddress)
				{
					if (BesiegeLogFilter.logDebug)
					{
						Debug.Log("Using NATTraversal localhost address.");
					}
					return "127.0.0.1";
				}
				if (BesiegeLogFilter.logDebug)
				{
					Debug.Log("Using NATTraversal host's local ip address.");
				}
				return hostInternalIP;
			}
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("Using NATTraversal host's external ip address.");
			}
			return hostExternalIP;
		}
		if (StatMaster.ExternalIP == string.Empty && hostExternalIP != string.Empty)
		{
			Debug.LogError("Host NATTraversal only supports ipv4 connections and client does not support ipv4.");
		}
		return string.Empty;
	}

	private void Reconnect()
	{
		ShutdownClient();
		InitTransport();
		if (prevClientConnectionState == ClientConnectionState.PunchingThroughToServer)
		{
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("Reconnecting over nat, natConnectAdress=" + natConnectAdress + ", natConnectPort=" + natConnectPort + ", natListenPort=" + natListenPort);
			}
			ConnectToServer(natConnectAdress, natConnectPort, natListenPort, true);
		}
		else
		{
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("Reconnecting directly, connectAddress=" + connectAddress + ", connectPort=" + connectPort);
			}
			ConnectToServer(connectAddress, connectPort, connectPort, false);
		}
	}

	private void RetryPunchthrough()
	{
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log(string.Format("Retrying punchthrough, attempt {0}/{1}", connectAttempt, OptionsMaster.BesiegeConfig.MaxReconnectAttempts));
		}
		JoinNAT();
	}

	protected override void SetServerConnectionState(ServerConnectionState newState)
	{
		base.SetServerConnectionState(newState);
		ServerConnectionState serverConnectionState = base.serverConnectionState;
		if (serverConnectionState == ServerConnectionState.InitializationFailed)
		{
			connectionHandler.OnDisconnected(false);
		}
	}

	protected override void SetClientConnectionState(ClientConnectionState newState)
	{
		base.SetClientConnectionState(newState);
		switch (clientConnectionState)
		{
		case ClientConnectionState.LobbyNotFound:
			if (connectAddress == Network.player.ipAddress || connectAddress == "127.0.0.1")
			{
				Timeout();
			}
			else
			{
				ConnectToIP(connectAddress, connectPort);
			}
			break;
		case ClientConnectionState.HostNotFound:
			Disconnect(LocalisationManager.GetTranslation(2010));
			break;
		case ClientConnectionState.HolePunchedFailed:
			if (++connectAttempt > OptionsMaster.BesiegeConfig.MaxReconnectAttempts)
			{
				Disconnect(LocalisationManager.GetTranslation(2017));
			}
			else
			{
				RetryPunchthrough();
			}
			break;
		case ClientConnectionState.DirectConnectFailed:
			ShutdownClient();
			if (connectAddress == Network.player.ipAddress || connectAddress == "127.0.0.1")
			{
				Timeout();
			}
			else
			{
				ConnectToIP(connectAddress, connectPort);
			}
			break;
		case ClientConnectionState.CRCMismatch:
			Disconnect(LocalisationManager.GetTranslation(2028));
			break;
		}
	}

	public override void ShutdownClient()
	{
		isTimingOut = false;
		if (clientConnectionState == ClientConnectionState.Connected)
		{
			DisconnectSocket(socketId, connectionID);
		}
		StopAllCoroutines();
		if (!IsLAN && natHelper != null)
		{
			natHelper.StopPunchingThrough();
		}
		currentLobbyID = 0uL;
		socketId = -1;
		ShutdownTransport();
	}

	public override void Shutdown()
	{
		if (natHelper != null)
		{
			natHelper.OnDoneConnectingToFacilitator -= OnDoneConnectingToFacilitator;
		}
		base.Shutdown();
	}

	public override void ShutdownServer()
	{
		if (Network.isServer)
		{
			MasterServer.UnregisterHost();
			Network.Disconnect();
		}
		foreach (ushort player in playerList)
		{
			if (clientSocketIds.Contains(player))
			{
				DisconnectSocket(player, 1);
			}
			else
			{
				DisconnectSocket(socketId, player);
			}
		}
		StopAllCoroutines();
		if (!IsLAN && natHelper != null)
		{
			natHelper.StopListeningForPunchthrough();
		}
		playerList.Clear();
		socketId = -1;
		ShutdownTransport();
	}

	private void ShutdownTransport()
	{
		if (isInitialized)
		{
			isInitialized = false;
			NetworkTransport.Shutdown();
		}
	}

	private void Timeout()
	{
		if (BesiegeLogFilter.logInfo)
		{
			Debug.Log("Client connection timed out...");
		}
		SetClientConnectionState(ClientConnectionState.Disconnected);
		trafficIn = (trafficOut = 0uL);
		Disconnect(LocalisationManager.GetTranslation(2023));
	}

	private void OnConnectEvent(ushort playerId)
	{
		if (StatMaster.isHosting)
		{
			playerList.Add(playerId);
			connectionHandler.OnPlayerJoin(playerId);
		}
	}

	private void OnDisconnectEvent(ushort playerId, byte error)
	{
		if (playerId == networkID)
		{
			if (error == 10)
			{
				SetClientConnectionState(ClientConnectionState.CRCMismatch);
			}
			else
			{
				string empty = string.Empty;
				switch ((NetworkError)error)
				{
				case NetworkError.Ok:
					empty = LocalisationManager.GetTranslation(2024);
					break;
				case NetworkError.Timeout:
					empty = LocalisationManager.GetTranslation(2025);
					break;
				case NetworkError.VersionMismatch:
					empty = LocalisationManager.GetTranslation(2026);
					break;
				default:
					empty = string.Format(LocalisationManager.GetTranslation(2027), (NetworkError)error);
					break;
				}
				socketId = -1;
				Disconnect(empty);
			}
		}
		if (StatMaster.isHosting)
		{
			if (clientSocketIds.Contains(playerId) && NetworkTransport.RemoveHost(playerId))
			{
				clientSocketIds.Remove(playerId);
			}
			connectionHandler.OnPlayerLeave(playerId);
			playerList.Remove(playerId);
		}
	}

	private bool IsValidPlayer(ushort playerId)
	{
		PlayerData player;
		if (!Playerlist.GetPlayer(playerId, out player))
		{
			Debug.LogWarning("Got data event from unknown playerId: " + playerId);
			return false;
		}
		if (player.isZombie)
		{
			return false;
		}
		player.lastPacketTime = Time.time;
		return true;
	}

	private void OnDataEvent(ushort playerId, int channel, int dataSize)
	{
		trafficIn += (ulong)dataSize;
		if (!StatMaster.isHosting || IsValidPlayer(playerId))
		{
			connectionHandler.OnDataEvent(playerId, channel, messageBuffer, dataSize);
		}
	}

	private void Update()
	{
		if (CheckTimeout() || !isInitialized)
		{
			return;
		}
		NetworkEventType networkEventType;
		do
		{
			int hostId;
			int connectionId;
			int channelId;
			int receivedSize;
			byte error;
			networkEventType = NetworkTransport.Receive(out hostId, out connectionId, out channelId, messageBuffer, bufferSize, out receivedSize, out error);
			connectionId = ((!clientSocketIds.Contains(hostId)) ? connectionId : hostId);
			switch (networkEventType)
			{
			case NetworkEventType.ConnectEvent:
				OnConnectEvent((ushort)connectionId);
				break;
			case NetworkEventType.DisconnectEvent:
				OnDisconnectEvent((ushort)connectionId, error);
				if (!StatMaster.isHosting)
				{
					return;
				}
				break;
			case NetworkEventType.DataEvent:
				OnDataEvent((ushort)connectionId, channelId, receivedSize);
				break;
			}
		}
		while (networkEventType != NetworkEventType.Nothing && isInitialized);
	}

	public override void ConnectSteam(ulong gameserverId)
	{
		Debug.LogError("Connect using a gameserverId is not supported");
	}
}
