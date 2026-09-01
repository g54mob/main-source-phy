using System;
using UnityEngine;

public class HostJoinWindow : MonoBehaviour, IBaseNetworkManager
{
	private const int MARGIN_TOP = 20;

	private const int MARGIN_LEFT = 10;

	private string ipAddress = "127.0.0.1";

	private int serverListenPort = 27015;

	private ulong steamId;

	private TransportNetworkManager transportManager;

	public bool isServer
	{
		get
		{
			return StatMaster.isHosting;
		}
	}

	public ushort PlayerID
	{
		get
		{
			return transportManager.PlayerID();
		}
	}

	private void Awake()
	{
		BesiegeEntryPoint.onConnectToIpServer = (Action<string, int, string>)Delegate.Combine(BesiegeEntryPoint.onConnectToIpServer, new Action<string, int, string>(ConnectToIpServer));
		BesiegeEntryPoint.OnConnectToPlayfabServer = (Action<string>)Delegate.Combine(BesiegeEntryPoint.OnConnectToPlayfabServer, new Action<string>(ConnectToPlayfabServer));
		BesiegeEntryPoint.onConnectToServer = (Action<ulong, string>)Delegate.Combine(BesiegeEntryPoint.onConnectToServer, new Action<ulong, string>(ConnectToServer));
		BesiegeEntryPoint.onConnectToLobby = (Action<ulong, string>)Delegate.Combine(BesiegeEntryPoint.onConnectToLobby, new Action<ulong, string>(ConnectToLobby));
		BesiegeEntryPoint.onStartServer = (Action<DedicatedServerMode>)Delegate.Combine(BesiegeEntryPoint.onStartServer, new Action<DedicatedServerMode>(StartServer));
		BesiegeEntryPoint.onTestMachine = (Action<string, float, bool, int>)Delegate.Combine(BesiegeEntryPoint.onTestMachine, new Action<string, float, bool, int>(TestMachine));
	}

	private void Start()
	{
		StatMaster.SetLogFilter(0);
		base.gameObject.AddComponent<NetworkAnalyser>();
		transportManager = base.gameObject.AddComponent<TransportNetworkManager>();
		transportManager.Initialize(this);
		base.gameObject.AddComponent<ExternalIpResolver>();
		ipAddress = PlayerPrefs.GetString("networktestip", ipAddress);
		serverListenPort = PlayerPrefs.GetInt("networktestport", serverListenPort);
	}

	private void OnGUI()
	{
		ClientConnectionState clientState = transportManager.ClientState;
		ServerConnectionState serverState = transportManager.ServerState;
		string text = ((!SteamManager.Initialized) ? "UNET" : "STEAM");
		GUI.Box(new Rect(5f, 5f, 650f, 450f), string.Concat("[", text, "] ClientState=", clientState, " - ServerState=", serverState));
		if (clientState == ClientConnectionState.Disconnected && serverState == ServerConnectionState.Disconnected)
		{
			GUI.changed = false;
			ipAddress = GUI.TextField(new Rect(20f, 30f, 250f, 30f), ipAddress, 100);
			string s = GUI.TextField(new Rect(20f, 60f, 250f, 30f), serverListenPort.ToString(), 25);
			int.TryParse(s, out serverListenPort);
			if (GUI.changed)
			{
				PlayerPrefs.SetString("networktestip", ipAddress);
				PlayerPrefs.SetInt("networktestport", serverListenPort);
			}
			string s2 = GUI.TextField(new Rect(280f, 60f, 250f, 30f), steamId.ToString(), 25);
			ulong.TryParse(s2, out steamId);
			if (GUI.Button(new Rect(20f, 90f, 250f, 30f), "Start server"))
			{
				StartServer();
			}
			if (GUI.Button(new Rect(20f, 120f, 250f, 30f), "Join server"))
			{
				JoinServer();
			}
		}
		else if (GUI.Button(new Rect(20f, 140f, 250f, 50f), "stop"))
		{
			transportManager.Stop();
		}
	}

	private void JoinServer()
	{
		Debug.Log("Joining server " + ipAddress + ":" + serverListenPort);
		transportManager.JoinIPServer(ipAddress, serverListenPort);
	}

	private void StartServer()
	{
		StatMaster.isHosting = true;
		Debug.Log("Starting server at port " + serverListenPort);
		transportManager.Host(serverListenPort);
	}

	public void ConnectToLobby(ulong lobbySteamId, string password)
	{
		if (!SteamManager.Initialized)
		{
			Debug.LogError("Could not join lobby, are you sure Steam is on?");
		}
		else
		{
			transportManager.JoinLobby(lobbySteamId, password);
		}
	}

	public void ConnectToIpServer(string ipAddress, int port, string password)
	{
		this.ipAddress = ipAddress;
		serverListenPort = port;
		JoinServer();
	}

	public void ConnectToPlayfabServer(string playfabNetworkId)
	{
		Debug.Log("[HostJoinWindow] ConnectToGDKServer " + playfabNetworkId);
		ipAddress = playfabNetworkId;
		serverListenPort = -1;
		JoinServer();
	}

	public void StartServer(DedicatedServerMode mode)
	{
		StatMaster.isHosting = true;
		StartServer();
	}

	public void TestMachine(string machinePath, float testDuration, bool isHeadless, int numTestMachines)
	{
		throw new NotImplementedException();
	}

	public void SetDisconnectMessage(string message)
	{
		Debug.Log("SetDisconnectMessage: " + message);
	}

	public void OnTimeout()
	{
		Debug.Log("OnTimeout");
	}

	public void OnConnect()
	{
		Debug.Log("OnConnect");
	}

	public void OnDisconnect(bool isOnDestroy)
	{
		Debug.Log("OnDisconnect");
		StatMaster.isHosting = false;
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

	public void Stop()
	{
		Debug.Log("Stop");
		StatMaster.isHosting = false;
	}

	public void IncrementSentMessages()
	{
		Debug.Log("IncrementSentMessages");
	}

	public void OnPlayerJoin(ushort playerId)
	{
		Debug.Log("OnPlayerJoin, playerId=" + playerId);
		byte[] array = new byte[12]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0
		};
		NetworkCompression.WriteUInt16(playerId, array, 2);
		byte[] bytes = BitConverter.GetBytes(0);
		Buffer.BlockCopy(bytes, 0, array, 4, bytes.Length);
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Player " + playerId + " joined, sending lobby ID " + 0);
		}
		transportManager.SendPlayerMessage(playerId, 0, array);
	}

	public void OnPlayerLeave(ushort playerId)
	{
		Debug.Log("OnPlayerLeave, playerId=" + playerId);
	}

	public void OnServerMessage(ushort playerId, byte sentToClients, byte[] data, int offset)
	{
		Debug.Log("OnServerMessage, playerId=" + playerId);
	}

	public void OnClientMessage(ushort playerId, byte[] data, int offset)
	{
		Debug.Log("OnClientMessage, playerId=" + playerId);
		transportManager.SetPlayerID(1, 0uL);
	}

	public void OnLevelData(BesiegeDataFrame frame)
	{
		Debug.Log("OnLevelData");
	}

	public void OnLogicData(BesiegeDataFrame frame)
	{
		Debug.Log("OnLogicData");
	}

	public void OnMachineData(ushort playerId, BesiegeDataFrame frame)
	{
		Debug.Log("OnMachineData, playerId=" + playerId);
	}

	public void OnInputData(ushort playerId, int session, byte[] data, int offset)
	{
		Debug.Log("OnInputData, playerId=" + playerId);
	}

	public void OnGhostData(ushort playerId, byte[] data, int offset, int size)
	{
		Debug.Log("OnGhostData, playerId=" + playerId);
	}

	public void OnCamData(ushort playerId, byte[] data)
	{
		Debug.Log("OnCamData, playerId=" + playerId);
	}

	public void OnClientConnectionStateChanged(ClientConnectionState clientConnectionState)
	{
	}

	public void OnServerConnectionStateChanged(ServerConnectionState serverConnectionState)
	{
	}

	public void OnPingData(byte[] messageBuffer, int dataSize)
	{
		Debug.Log("OnPingData");
	}

	public void ConnectToServer(ulong serverId, string password)
	{
		throw new NotImplementedException();
	}
}
