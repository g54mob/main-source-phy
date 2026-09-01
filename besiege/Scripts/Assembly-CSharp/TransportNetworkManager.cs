using System;
using System.Collections.Generic;
using Localisation;
using Modding;
using UnityEngine;

public class TransportNetworkManager : MonoBehaviour, IConnectionHandler
{
	private int serverChannel;

	private int playerChannel;

	private int machineChannel;

	private int logicChannel;

	private int ghostChannel;

	private int levelChannel;

	private int inputChannel;

	private int clientInputChannel;

	private int pingChannel;

	private int camChannel;

	private int modChannel;

	private BesiegeDataFrame dataFrame;

	private IBaseNetworkManager networkManager;

	private BaseConnection lanConnection;

	private BaseConnection connection;

	private GameObject connectGameObject;

	public string ConnectAddress
	{
		get
		{
			return (!HasConnection) ? "N/A" : CurrentConnection.ConnectAddress;
		}
	}

	public int ConnectPort
	{
		get
		{
			return HasConnection ? CurrentConnection.ConnectPort : 0;
		}
	}

	public ServerConnectionState ServerState
	{
		get
		{
			return HasConnection ? CurrentConnection.ServerState : ServerConnectionState.Disconnected;
		}
	}

	public ClientConnectionState ClientState
	{
		get
		{
			return HasConnection ? CurrentConnection.ClientState : ClientConnectionState.Disconnected;
		}
	}

	public bool HasConnection
	{
		get
		{
			return CurrentConnection != null;
		}
	}

	private BaseConnection CurrentConnection
	{
		get
		{
			return (OptionsMaster.networkType != PlayerNetworkType.LAN) ? connection : lanConnection;
		}
	}

	public string CurrentNetwork
	{
		get
		{
			return (!(CurrentConnection != null)) ? string.Empty : CurrentConnection.CurrentNetwork;
		}
	}

	public string NetworkString
	{
		get
		{
			return (!(CurrentConnection != null)) ? string.Empty : CurrentConnection.NetworkString;
		}
	}

	public int ServerMessageHeaderSize()
	{
		return 1;
	}

	public int PlayerMessageHeaderSize()
	{
		return 2;
	}

	public int LevelMessageHeaderSize()
	{
		return 7;
	}

	public int LogicMessageHeaderSize()
	{
		return 7;
	}

	public int MachineMessageHeaderSize()
	{
		return 9;
	}

	public int InputMessageHeaderSize()
	{
		return 5;
	}

	public int GhostMessageHeaderSize()
	{
		return 2;
	}

	public int ConnectAttempt()
	{
		return HasConnection ? CurrentConnection.ConnectAttempt : 0;
	}

	public int Ping()
	{
		return HasConnection ? CurrentConnection.Ping() : 0;
	}

	public ulong TrafficIn()
	{
		return (!HasConnection) ? 0 : CurrentConnection.TrafficIn;
	}

	public ulong TrafficOut()
	{
		return (!HasConnection) ? 0 : CurrentConnection.TrafficOut;
	}

	public ushort PlayerID()
	{
		return (ushort)(HasConnection ? CurrentConnection.PlayerID : 0);
	}

	public ulong LobbyID()
	{
		return (!HasConnection) ? 0 : CurrentConnection.LobbyID;
	}

	public ulong ServerID()
	{
		return (!HasConnection) ? 0 : CurrentConnection.ServerID;
	}

	public void Initialize(IBaseNetworkManager networkManager)
	{
		this.networkManager = networkManager;
		ConfigureMasterServer();
		dataFrame = new BesiegeDataFrame();
	}

	private void SetupConnection()
	{
		if (connectGameObject == null)
		{
			connectGameObject = new GameObject("TransportConnection");
			UnityEngine.Object.DontDestroyOnLoad(connectGameObject);
		}
		if (!StatMaster.IsLevelEditorOnly)
		{
			Debug.Log("[TransportNetworkManager] InitializeConnection " + OptionsMaster.networkType);
		}
		switch (OptionsMaster.networkType)
		{
		case PlayerNetworkType.Steam:
			if (SteamManager.Initialized)
			{
				connection = connectGameObject.AddComponent<SteamConnection>();
			}
			else
			{
				connection = connectGameObject.AddComponent<UnetConnection>();
			}
			break;
		case PlayerNetworkType.Playfab:
			connection = connectGameObject.AddComponent<PlayfabConnection>();
			break;
		case PlayerNetworkType.LAN:
			lanConnection = connectGameObject.AddComponent<LanConnection>();
			break;
		default:
			connection = connectGameObject.AddComponent<UnetConnection>();
			break;
		}
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Initializing " + CurrentConnection.GetType().Name + " id=" + CurrentConnection.GetInstanceID());
		}
		CurrentConnection.Initialize(this);
		SetConnectionChannels();
	}

	public void ResetConnection()
	{
		if (!(CurrentConnection == null))
		{
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("Shutting down " + CurrentConnection.GetType().Name + " id=" + CurrentConnection.GetInstanceID());
			}
			CurrentConnection.Dispose(true, false);
			if (OptionsMaster.networkType == PlayerNetworkType.LAN)
			{
				lanConnection = null;
			}
			else
			{
				connection = null;
			}
		}
	}

	public void ConfigureMasterServer()
	{
		MasterServer.ipAddress = OptionsMaster.BesiegeConfig.MasterserverIP;
		MasterServer.port = OptionsMaster.BesiegeConfig.MasterserverPort;
	}

	private void OnDisconnect()
	{
		Stop();
		networkManager.OnTimeout();
	}

	public void SendServerMessage(byte destByte, byte[] data)
	{
		if (StatMaster.isServer)
		{
			networkManager.OnServerMessage(CurrentConnection.NetworkID, destByte, data, ServerMessageHeaderSize());
			return;
		}
		data[0] = destByte;
		CurrentConnection.SendNetworkMessage((ushort)CurrentConnection.ConnectionID, serverChannel, data);
	}

	public void SendPlayerMessage(ushort playerId, ushort sender, byte[] data)
	{
		NetworkCompression.WriteUInt16(sender, data, 0);
		CurrentConnection.SendNetworkMessage(playerId, playerChannel, data);
	}

	public void DisconnectPlayer(ushort playerId)
	{
		CurrentConnection.DisconnectPlayer(playerId);
	}

	public void SendCamData(byte[] data)
	{
		CurrentConnection.SendNetworkMessage((ushort)CurrentConnection.ConnectionID, camChannel, data);
	}

	public void SendInputData(ushort playerId, int session, byte[] data, ushort dataSize)
	{
		NetworkCompression.WriteUInt16(playerId, data, 0);
		NetworkCompression.WriteUInt16(dataSize, data, 2);
		data[4] = (byte)session;
		CurrentConnection.SendNetworkMessage((ushort)CurrentConnection.ConnectionID, inputChannel, data);
	}

	public void SendInputData(List<ushort> players, byte[] data)
	{
		for (int i = 0; i < players.Count; i++)
		{
			CurrentConnection.SendNetworkMessage(players[i], clientInputChannel, data);
		}
	}

	public void SendGhostData(ushort playerId, byte[] data)
	{
		NetworkCompression.WriteUInt16(playerId, data, 0);
		if (StatMaster.isServer)
		{
			networkManager.OnGhostData(playerId, data, GhostMessageHeaderSize(), data.Length);
		}
		else
		{
			CurrentConnection.SendNetworkMessage((ushort)CurrentConnection.ConnectionID, ghostChannel, data);
		}
	}

	public void SendGhostData(List<ushort> players, byte[] data)
	{
		for (int i = 0; i < players.Count; i++)
		{
			CurrentConnection.SendNetworkMessage(players[i], ghostChannel, data);
		}
	}

	public void SendLevelData(uint frame, int session, ushort current, byte[] data)
	{
		NetworkCompression.WriteUInt(frame, false, data, 0);
		data[4] = (byte)session;
		NetworkCompression.WriteUInt16(current, data, 5);
		CurrentConnection.BroadcastMessage(levelChannel, data);
	}

	public void SendLogicData(uint frame, int session, ushort current, byte[] data)
	{
		NetworkCompression.WriteUInt(frame, false, data, 0);
		data[4] = (byte)session;
		NetworkCompression.WriteUInt16(current, data, 5);
		CurrentConnection.BroadcastMessage(logicChannel, data);
	}

	public void SendMachineData(List<ushort> players, ushort machineId, uint frame, int session, ushort current, byte[] data)
	{
		NetworkCompression.WriteUInt16(machineId, data, 0);
		NetworkCompression.WriteUInt(frame, false, data, 2);
		data[6] = (byte)session;
		NetworkCompression.WriteUInt16(current, data, 7);
		for (int i = 0; i < players.Count; i++)
		{
			CurrentConnection.SendNetworkMessage(players[i], machineChannel, data);
		}
	}

	public void SendMachineData(ushort playerId, uint frame, int session, ushort current, byte[] data)
	{
		List<ushort> list = new List<ushort>();
		list.Add((ushort)CurrentConnection.ConnectionID);
		SendMachineData(list, playerId, frame, session, current, data);
	}

	public void SendModData(ushort playerId, byte[] data)
	{
		CurrentConnection.SendNetworkMessage(playerId, modChannel, data);
	}

	private void SetConnectionChannels()
	{
		serverChannel = CurrentConnection.AddChannel(BesiegeQosType.AllCostDelivery);
		playerChannel = CurrentConnection.AddChannel(BesiegeQosType.AllCostDelivery);
		inputChannel = CurrentConnection.AddChannel(BesiegeQosType.AllCostDelivery);
		camChannel = CurrentConnection.AddChannel(BesiegeQosType.Reliable);
		clientInputChannel = CurrentConnection.AddChannel(BesiegeQosType.Reliable);
		logicChannel = CurrentConnection.AddChannel(BesiegeQosType.Reliable);
		ghostChannel = CurrentConnection.AddChannel(BesiegeQosType.Unreliable);
		levelChannel = CurrentConnection.AddChannel(BesiegeQosType.Unreliable);
		machineChannel = CurrentConnection.AddChannel(BesiegeQosType.Unreliable);
		pingChannel = CurrentConnection.AddChannel(BesiegeQosType.Unreliable);
		modChannel = CurrentConnection.AddChannel(BesiegeQosType.Reliable);
	}

	public void Host(int port)
	{
		ResetConnection();
		SetupConnection();
		if (!CurrentConnection.Listen(port))
		{
			Debug.LogWarning("Could not start server...");
		}
	}

	public void JoinIPServer(string address, int port)
	{
		ResetConnection();
		SetupConnection();
		CurrentConnection.ConnectToIP(address, port);
	}

	public void JoinSteamServer(ulong serverId)
	{
		ResetConnection();
		OptionsMaster.networkType = PlayerNetworkType.Steam;
		SetupConnection();
		CurrentConnection.ConnectSteam(serverId);
	}

	public void JoinLobby(ulong lobbySteamId, string password)
	{
		ResetConnection();
		OptionsMaster.networkType = PlayerNetworkType.Steam;
		SetupConnection();
		CurrentConnection.ConnectToLobby(lobbySteamId);
	}

	public void JoinPlayfabServer(string pfNetworkId)
	{
		ResetConnection();
		OptionsMaster.networkType = PlayerNetworkType.Playfab;
		SetupConnection();
		CurrentConnection.ConnectPlayfab(pfNetworkId);
	}

	public void Stop()
	{
		StatMaster.IsLevelEditorOnly = false;
		if (CurrentConnection != null)
		{
			CurrentConnection.Shutdown();
		}
	}

	private void OnApplicationQuit()
	{
		CleanConnection(true, true);
	}

	private void OnDestroy()
	{
		CleanConnection(false, false);
	}

	private void CleanConnection(bool disposeSilently, bool isQuitting)
	{
		if (!(CurrentConnection == null))
		{
			CurrentConnection.Dispose(disposeSilently, isQuitting);
			if (OptionsMaster.networkType == PlayerNetworkType.LAN)
			{
				lanConnection = null;
			}
			else
			{
				connection = null;
			}
		}
	}

	public void SetUPNPEnabled(bool enabled)
	{
		NetworkScene.ServerSettings.useUPNP = enabled;
	}

	public int GetPing(ushort playerId)
	{
		return CurrentConnection.GetPing(playerId);
	}

	public void SendPingData(ushort playerId, byte[] data)
	{
		if (CurrentConnection.NetworkID == playerId)
		{
			networkManager.OnPingData(data, data.Length);
		}
		else
		{
			CurrentConnection.SendNetworkMessage(playerId, pingChannel, data);
		}
	}

	public void SetServerFull()
	{
		if (BesiegeLogFilter.logInfo)
		{
			Debug.Log("Server is full!");
		}
		CurrentConnection.Disconnect(LocalisationManager.GetTranslation(2943));
	}

	public void OnJoinFailed(int joinError)
	{
		int num = 0;
		switch (joinError)
		{
		case 1:
			num = 3014;
			break;
		case 2:
			num = 3015;
			break;
		case 3:
			num = 3016;
			break;
		case 5:
			num = 3018;
			break;
		case 6:
			num = 3019;
			break;
		case 8:
			num = 2944;
			break;
		default:
			num = 3017;
			break;
		}
		string text = LocalisationManager.GetTranslation(num);
		if (joinError == 7)
		{
			text = "Mismatched mods!";
		}
		if (BesiegeLogFilter.logError)
		{
			Debug.LogError("Couldn't join server: " + text + " (error " + joinError + ", loc: " + num + ")");
		}
		CurrentConnection.Disconnect(LocalisationManager.GetTranslation(2944) + "\n" + text);
	}

	public void SetPlayerID(ushort id, ulong lobbyId)
	{
		CurrentConnection.SetPlayerID(id, lobbyId);
	}

	public void IncrementSentMessages()
	{
		networkManager.IncrementSentMessages();
	}

	public void OnConnected()
	{
		networkManager.OnConnect();
	}

	public void OnDisconnected(bool wasOnDestroy)
	{
		networkManager.OnDisconnect(wasOnDestroy);
	}

	public void OnPlayerJoin(ushort playerId)
	{
		networkManager.OnPlayerJoin(playerId);
	}

	public void OnPlayerLeave(ushort playerId)
	{
		networkManager.OnPlayerLeave(playerId);
	}

	public void OnDataEvent(ushort playerId, int channel, byte[] buffer, int bufferSize)
	{
		if (channel == machineChannel)
		{
			ushort playerId2 = NetworkCompression.ReadUInt16(buffer, 0);
			uint frame = BitConverter.ToUInt32(buffer, 2);
			int session = buffer[6];
			ushort current = NetworkCompression.ReadUInt16(buffer, 7);
			dataFrame.Update(frame, session, current, buffer, bufferSize);
			networkManager.OnMachineData(playerId2, dataFrame);
		}
		else if (channel == inputChannel)
		{
			ushort playerId3 = NetworkCompression.ReadUInt16(buffer, 0);
			ushort num = NetworkCompression.ReadUInt16(buffer, 2);
			int session2 = buffer[4];
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, 5, array, 0, num);
			networkManager.OnInputData(playerId3, session2, array, 0);
		}
		else if (channel == clientInputChannel)
		{
			byte inputID = buffer[0];
			ushort num = NetworkCompression.ReadUInt16(buffer, 1);
			int srcOffset = 3;
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(buffer, srcOffset, array2, 0, num);
			NetworkAddPiece.Instance.OnInputData(CurrentConnection.NetworkID, inputID, array2);
		}
		else if (channel == ghostChannel)
		{
			ushort playerId4 = NetworkCompression.ReadUInt16(buffer, 0);
			networkManager.OnGhostData(playerId4, buffer, GhostMessageHeaderSize(), bufferSize);
		}
		else if (channel == pingChannel)
		{
			networkManager.OnPingData(buffer, bufferSize);
		}
		else if (channel == modChannel)
		{
			ModNetworking.OnRawMessage(playerId, buffer, bufferSize);
		}
		if (StatMaster.isServer)
		{
			if (channel == serverChannel)
			{
				networkManager.OnServerMessage(playerId, buffer[0], buffer, ServerMessageHeaderSize());
			}
			else if (channel == camChannel)
			{
				networkManager.OnCamData(playerId, buffer);
			}
		}
		else if (channel == playerChannel)
		{
			ushort playerId5 = NetworkCompression.ReadUInt16(buffer, 0);
			networkManager.OnClientMessage(playerId5, buffer, PlayerMessageHeaderSize());
		}
		else if (channel == levelChannel || channel == logicChannel)
		{
			uint frame2 = BitConverter.ToUInt32(buffer, 0);
			int session3 = buffer[4];
			ushort current2 = NetworkCompression.ReadUInt16(buffer, 5);
			dataFrame.Update(frame2, session3, current2, buffer, bufferSize);
			if (channel == levelChannel)
			{
				networkManager.OnLevelData(dataFrame);
			}
			else
			{
				networkManager.OnLogicData(dataFrame);
			}
		}
	}

	public void SetErrorMessage(string message)
	{
		networkManager.SetDisconnectMessage(message);
	}

	public void OnClientConnectionStateChanged(ClientConnectionState newState)
	{
		networkManager.OnClientConnectionStateChanged(newState);
	}

	public void OnServerConnectionStateChanged(ServerConnectionState newState)
	{
		networkManager.OnServerConnectionStateChanged(newState);
	}
}
