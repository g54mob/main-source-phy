using System;
using System.Collections;
using System.Collections.Generic;
using BesiegeDlc;
using InternalModding.Mods;
using UnityEngine;

public class BesiegeNetworkManager : MonoBehaviour, IBaseNetworkManager
{
	public static BesiegeNetworkManager Instance;

	[HideInInspector]
	public bool isConnected;

	[HideInInspector]
	public bool isConnecting;

	[HideInInspector]
	public string disconnectMessage;

	[HideInInspector]
	public List<ModList.Mod> mismatchedMods;

	public int sentMessages;

	public Action<ushort> onPlayerJoin;

	public Action<ushort> onPlayerLeave;

	public Action<ushort, byte, byte[], int> onServerMessage;

	public Action<ushort, byte[], int> onClientMessage;

	public Action<BesiegeDataFrame> onLevelData;

	public Action<BesiegeDataFrame> onLogicData;

	public Action<ushort, BesiegeDataFrame> onMachineData;

	public Action<ushort, int, byte[], int> onInputData;

	public Action<ushort, byte[], int, int> onGhostData;

	public Action<ushort, byte[]> onCamData;

	public Action onPingData;

	public Action onTimeout;

	public Action onConnected;

	public Action<bool> onDisconnected;

	public Action<ClientConnectionState> onClientConnectionStateChanged;

	public Action<ServerConnectionState> onServerConnectionStateChanged;

	private ServerConnectionState serverConnectionState;

	private TransportNetworkManager currentManager;

	public int Ping
	{
		get
		{
			return currentManager.Ping();
		}
	}

	public ushort PlayerID
	{
		get
		{
			return currentManager.PlayerID();
		}
	}

	public ulong TrafficIn
	{
		get
		{
			return currentManager.TrafficIn();
		}
	}

	public ulong TrafficOut
	{
		get
		{
			return currentManager.TrafficOut();
		}
	}

	public int ServerMessageHeaderSize
	{
		get
		{
			return currentManager.ServerMessageHeaderSize();
		}
	}

	public int PlayerMessageHeaderSize
	{
		get
		{
			return currentManager.PlayerMessageHeaderSize();
		}
	}

	public int LevelMessageHeaderSize
	{
		get
		{
			return currentManager.LevelMessageHeaderSize();
		}
	}

	public int LogicMessageHeaderSize
	{
		get
		{
			return currentManager.LogicMessageHeaderSize();
		}
	}

	public int MachineMessageHeaderSize
	{
		get
		{
			return currentManager.MachineMessageHeaderSize();
		}
	}

	public int InputMessageHeaderSize
	{
		get
		{
			return currentManager.InputMessageHeaderSize();
		}
	}

	public int GhostMessageHeaderSize
	{
		get
		{
			return currentManager.GhostMessageHeaderSize();
		}
	}

	public string ExternalIP
	{
		get
		{
			return StatMaster.ExternalIP;
		}
	}

	public ulong LobbyID
	{
		get
		{
			return currentManager.LobbyID();
		}
	}

	public ulong ServerID
	{
		get
		{
			return currentManager.ServerID();
		}
	}

	public bool IsInHostingState
	{
		get
		{
			return serverConnectionState != ServerConnectionState.Disconnected;
		}
	}

	public bool IsHostInitialized
	{
		get
		{
			return serverConnectionState == ServerConnectionState.WaitingForConnection || serverConnectionState == ServerConnectionState.WaitingForPlatformConnection;
		}
	}

	public string CurrentNetwork
	{
		get
		{
			return currentManager.CurrentNetwork;
		}
	}

	public string NetworkString
	{
		get
		{
			return currentManager.NetworkString;
		}
	}

	protected void Awake()
	{
		Instance = this;
		currentManager = GetComponent<TransportNetworkManager>();
		currentManager.Initialize(this);
	}

	public void ResetConnection()
	{
		currentManager.ResetConnection();
	}

	public int ConnectAttempt()
	{
		return currentManager.ConnectAttempt();
	}

	public void SetPlayerID(ushort id, ulong lobbyId)
	{
		currentManager.SetPlayerID(id, lobbyId);
	}

	public void Host(int port)
	{
		currentManager.Host(port);
	}

	public void SetDisconnectMessage(string message)
	{
		disconnectMessage = message;
	}

	internal void ConfigureMasterServer()
	{
		currentManager.ConfigureMasterServer();
	}

	public void Join(string ip, int port)
	{
		isConnecting = true;
		currentManager.JoinIPServer(ip, port);
	}

	public void Join(string pfNetworkId)
	{
		isConnecting = true;
		currentManager.JoinPlayfabServer(pfNetworkId);
	}

	public void Join(ulong serverId)
	{
		isConnecting = true;
		currentManager.JoinSteamServer(serverId);
	}

	public void JoinLobby(ulong lobbySteamId, string password)
	{
		isConnecting = true;
		currentManager.JoinLobby(lobbySteamId, password);
	}

	public void SendServerMessage(byte destByte, byte[] data)
	{
		currentManager.SendServerMessage(destByte, data);
	}

	public void SendPlayerMessage(ushort playerId, ushort sender, byte[] messageData)
	{
		currentManager.SendPlayerMessage(playerId, sender, messageData);
	}

	public void SendLevelData(uint frame, int session, ushort current, byte[] data)
	{
		currentManager.SendLevelData(frame, session, current, data);
	}

	public void SendLogicData(uint frame, int session, ushort current, byte[] data)
	{
		currentManager.SendLogicData(frame, session, current, data);
	}

	public void SendMachineData(ushort playerId, uint frame, int session, ushort current, byte[] data)
	{
		currentManager.SendMachineData(playerId, frame, session, current, data);
	}

	public void SendMachineData(List<ushort> players, ushort machineId, uint frame, int session, ushort current, byte[] data)
	{
		currentManager.SendMachineData(players, machineId, frame, session, current, data);
	}

	public void SendCamData(byte[] data)
	{
		currentManager.SendCamData(data);
	}

	public void SendInputData(ushort playerId, int session, byte[] data, ushort dataSize)
	{
		currentManager.SendInputData(playerId, session, data, dataSize);
	}

	public void SendInputData(List<ushort> players, byte[] data)
	{
		currentManager.SendInputData(players, data);
	}

	public void SendGhostData(ushort playerId, byte[] data)
	{
		currentManager.SendGhostData(playerId, data);
	}

	public void SendGhostData(List<ushort> players, byte[] data)
	{
		currentManager.SendGhostData(players, data);
	}

	public void SetServerFull()
	{
		currentManager.SetServerFull();
	}

	public void OnJoinFailed(int error)
	{
		currentManager.OnJoinFailed(error);
	}

	public void OnDlcJoinFailed(uint serverDlcMask)
	{
		List<uint> dlcTypesFromMask = DlcManager.Instance.GetDlcTypesFromMask(serverDlcMask);
		List<DlcManager.DlcType> dlcTypes = DlcManager.Convert(dlcTypesFromMask);
		List<DlcManager.DlcStatus> dlcIssues = new List<DlcManager.DlcStatus>();
		DlcManager.Instance.TestDlcTypes(dlcTypes, dlcIssues);
		DlcMismatchUI.Show(dlcIssues, 4459);
		currentManager.OnJoinFailed(8);
	}

	public void OnJoinFailedMod(List<ModList.Mod> mismatches)
	{
		Debug.LogError("Could not join due to mismatched mods: " + CompatibilityChecker.MismatchesToString(mismatches));
		mismatchedMods = mismatches;
		currentManager.OnJoinFailed(7);
	}

	public void DisconnectPlayer(ushort playerId)
	{
		currentManager.DisconnectPlayer(playerId);
	}

	public void Stop()
	{
		if (!isConnected && !isConnecting)
		{
			if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
			{
				currentManager.Stop();
			}
			return;
		}
		if (StatMaster.isServer && currentManager.HasConnection)
		{
			DisconnectAllPlayers();
		}
		currentManager.Stop();
	}

	public void OnPlayerJoin(ushort playerId)
	{
		if (onPlayerJoin != null)
		{
			onPlayerJoin(playerId);
		}
	}

	public void OnPlayerLeave(ushort playerId)
	{
		if (onPlayerLeave != null)
		{
			onPlayerLeave(playerId);
		}
	}

	public void OnConnect()
	{
		isConnected = true;
		disconnectMessage = string.Empty;
		mismatchedMods = null;
		isConnecting = false;
		if (onConnected != null)
		{
			onConnected();
		}
	}

	public void OnDisconnect(bool isOnDestroy)
	{
		if (isConnected || isConnecting || StatMaster.isServer)
		{
			if (onDisconnected != null)
			{
				onDisconnected(isOnDestroy);
			}
			StatMaster.isServer = false;
			isConnected = (isConnecting = false);
		}
	}

	public void OnTimeout()
	{
		if (onTimeout != null)
		{
			onTimeout();
		}
		StatMaster.isServer = false;
		isConnected = false;
	}

	public void OnClientConnectionStateChanged(ClientConnectionState newState)
	{
		if (onClientConnectionStateChanged != null)
		{
			onClientConnectionStateChanged(newState);
		}
	}

	public void OnServerConnectionStateChanged(ServerConnectionState newState)
	{
		serverConnectionState = newState;
		if (onServerConnectionStateChanged != null)
		{
			onServerConnectionStateChanged(newState);
		}
	}

	public void IncrementSentMessages()
	{
		if (sentMessages == 0)
		{
			StartCoroutine(ResetSentMessages());
		}
		sentMessages++;
	}

	private IEnumerator ResetSentMessages()
	{
		yield return new WaitForSeconds(OptionsMaster.chokeWaitTime);
		sentMessages = 0;
	}

	public void SetUPNPEnabled(bool enabled)
	{
		currentManager.SetUPNPEnabled(enabled);
	}

	public int GetPlayerPing(ushort networkId)
	{
		PlayerData player;
		if (Playerlist.GetPlayer(networkId, out player))
		{
			Debug.LogWarning("Could not get player with networkId: " + networkId);
			return 0;
		}
		return GetPlayerPing(player);
	}

	public int GetPlayerPing(PlayerData player)
	{
		return (player.networkId != PlayerID) ? currentManager.GetPing(player.networkId) : 0;
	}

	public void RequestPings(ushort recipientPlayerId)
	{
		byte[] array = new byte[4 * Playerlist.Players.Count];
		int num = 0;
		foreach (PlayerData player in Playerlist.Players)
		{
			NetworkCompression.WriteUInt16(player.networkId, array, num);
			NetworkCompression.WriteUInt16((ushort)player.ping, array, num + 2);
			num += 4;
		}
		currentManager.SendPingData(recipientPlayerId, array);
	}

	public void OnPingData(byte[] messageBuffer, int dataSize)
	{
		for (int i = 0; i < dataSize; i += 4)
		{
			PlayerData player;
			if (Playerlist.GetPlayer(NetworkCompression.ReadUInt16(messageBuffer, i), out player))
			{
				player.ping = NetworkCompression.ReadUInt16(messageBuffer, i + 2);
			}
		}
		if (onPingData != null)
		{
			onPingData();
		}
	}

	private void DisconnectAllPlayers()
	{
		if (!StatMaster.isHosting)
		{
			return;
		}
		PlayerData[] array = Playerlist.Players.ToArray();
		foreach (PlayerData playerData in array)
		{
			if (!playerData.isLocalPlayer)
			{
				DisconnectPlayer(playerData.networkId);
			}
		}
	}

	public void OnServerMessage(ushort playerId, byte sentToClients, byte[] data, int offset)
	{
		onServerMessage(playerId, sentToClients, data, offset);
	}

	public void OnClientMessage(ushort playerId, byte[] data, int offset)
	{
		onClientMessage(playerId, data, offset);
	}

	public void OnLevelData(BesiegeDataFrame frame)
	{
		onLevelData(frame);
	}

	public void OnLogicData(BesiegeDataFrame frame)
	{
		onLogicData(frame);
	}

	public void OnMachineData(ushort playerId, BesiegeDataFrame frame)
	{
		onMachineData(playerId, frame);
	}

	public void OnInputData(ushort playerId, int session, byte[] data, int offset)
	{
		onInputData(playerId, session, data, offset);
	}

	public void OnGhostData(ushort playerId, byte[] data, int offset, int size)
	{
		onGhostData(playerId, data, offset, size);
	}

	public void OnCamData(ushort playerId, byte[] data)
	{
		onCamData(playerId, data);
	}
}
