using Localisation;
using UnityEngine;

public abstract class BaseConnection : MonoBehaviour, IConnection
{
	protected int connectAttempt;

	protected int connectionID;

	protected int connectPort;

	protected ushort networkID;

	protected ulong currentLobbyID;

	protected ulong serverID;

	protected ulong trafficIn;

	protected ulong trafficOut;

	protected bool isQuitting;

	protected string connectAddress;

	protected ClientConnectionState clientConnectionState;

	protected ServerConnectionState serverConnectionState;

	protected ClientConnectionState prevClientConnectionState;

	protected ServerConnectionState prevServerConnectionState;

	protected IConnectionHandler connectionHandler = new NullConnectionHandler();

	protected bool isDisposed;

	public int ConnectPort
	{
		get
		{
			return connectPort;
		}
	}

	public int ConnectAttempt
	{
		get
		{
			return connectAttempt;
		}
	}

	public int ConnectionID
	{
		get
		{
			return connectionID;
		}
	}

	public ulong TrafficIn
	{
		get
		{
			return trafficIn;
		}
	}

	public ulong TrafficOut
	{
		get
		{
			return trafficOut;
		}
	}

	public virtual ulong LobbyID
	{
		get
		{
			return currentLobbyID;
		}
	}

	public virtual ulong ServerID
	{
		get
		{
			return serverID;
		}
	}

	public ushort PlayerID
	{
		get
		{
			return networkID;
		}
	}

	public ushort NetworkID
	{
		get
		{
			return networkID;
		}
	}

	public string ConnectAddress
	{
		get
		{
			return connectAddress;
		}
	}

	public ServerConnectionState ServerState
	{
		get
		{
			return serverConnectionState;
		}
	}

	public ClientConnectionState ClientState
	{
		get
		{
			return clientConnectionState;
		}
	}

	public abstract string CurrentNetwork { get; }

	public virtual string NetworkString
	{
		get
		{
			string currentNetwork = CurrentNetwork;
			return string.Format("IP: {0}", (!string.IsNullOrEmpty(currentNetwork)) ? currentNetwork : LocalisationManager.GetTranslation(1934));
		}
	}

	public abstract int AddChannel(BesiegeQosType qosType);

	public abstract void BroadcastMessage(int channel, byte[] data);

	public abstract void ConnectToIP(string serverAddress, int serverPort);

	public abstract void ConnectSteam(ulong gameserverId);

	public abstract void ConnectPlayfab(string pfNetworkId);

	public abstract void ConnectToLobby(ulong lobbyId);

	public abstract void DisconnectPlayer(ushort playerId);

	public abstract void Disconnect();

	public void Disconnect(string errorMessage)
	{
		connectionHandler.SetErrorMessage(errorMessage);
		Disconnect();
	}

	public abstract void Initialize();

	public void Initialize(IConnectionHandler connectionHandler)
	{
		SetConnectionHandler(connectionHandler);
		Initialize();
	}

	public abstract bool Listen(int serverPort);

	public abstract int GetPing(ushort playerId);

	public abstract int Ping();

	public abstract void SetPlayerID(ushort id, ulong lobbyId);

	public void SetConnectionHandler(IConnectionHandler handler)
	{
		connectionHandler = handler;
	}

	public abstract void SendNetworkMessage(ushort playerId, int channel, byte[] data);

	public virtual void Shutdown()
	{
		if (serverConnectionState != ServerConnectionState.Disconnected)
		{
			Stop();
		}
		else
		{
			Disconnect();
		}
	}

	public abstract void ShutdownClient();

	public abstract void ShutdownServer();

	public abstract void Stop();

	public virtual void Dispose()
	{
		Dispose(false, false);
	}

	public virtual void Dispose(bool silentDispose, bool isQuitting)
	{
		if (!isDisposed)
		{
			this.isQuitting = isQuitting;
			bool isHosting = StatMaster.isHosting;
			ShutdownClient();
			ShutdownServer();
			if (isHosting)
			{
				SetServerConnectionState(ServerConnectionState.Disconnected);
			}
			if (!silentDispose)
			{
				connectionHandler.OnDisconnected(true);
			}
			isDisposed = true;
		}
	}

	protected void PublishClientStateChange()
	{
		if (BesiegeLogFilter.logInfo)
		{
			Debug.Log(string.Concat("ClientStateChanged: ", prevClientConnectionState, " => ", clientConnectionState));
		}
		connectionHandler.OnClientConnectionStateChanged(clientConnectionState);
	}

	protected void PublishServerStateChange()
	{
		if (BesiegeLogFilter.logInfo)
		{
			Debug.Log(string.Concat("ServerStateChanged: ", prevServerConnectionState, " => ", serverConnectionState));
		}
		connectionHandler.OnServerConnectionStateChanged(serverConnectionState);
	}

	protected virtual void SetServerConnectionState(ServerConnectionState newState)
	{
		ServerConnectionState serverConnectionState = this.serverConnectionState;
		prevServerConnectionState = serverConnectionState;
		this.serverConnectionState = newState;
		PublishServerStateChange();
	}

	protected virtual void SetClientConnectionState(ClientConnectionState newState)
	{
		ClientConnectionState clientConnectionState = this.clientConnectionState;
		prevClientConnectionState = clientConnectionState;
		this.clientConnectionState = newState;
		PublishClientStateChange();
	}

	protected virtual void OnDestroy()
	{
		if (BesiegeLogFilter.logDev)
		{
			Debug.LogFormat("BaseConnection::OnDestroy, instance: {0}", GetInstanceID());
		}
		Dispose();
	}
}
