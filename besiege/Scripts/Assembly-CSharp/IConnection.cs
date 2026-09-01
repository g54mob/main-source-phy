public interface IConnection
{
	ClientConnectionState ClientState { get; }

	ServerConnectionState ServerState { get; }

	int ConnectPort { get; }

	int ConnectAttempt { get; }

	int ConnectionID { get; }

	ulong LobbyID { get; }

	ulong ServerID { get; }

	ulong TrafficIn { get; }

	ulong TrafficOut { get; }

	ushort PlayerID { get; }

	ushort NetworkID { get; }

	string ConnectAddress { get; }

	int AddChannel(BesiegeQosType qosType);

	void BroadcastMessage(int channel, byte[] data);

	void ConnectToIP(string serverAddress, int serverPort);

	void ConnectToLobby(ulong lobbyId);

	void DisconnectPlayer(ushort playerId);

	void Disconnect();

	void Disconnect(string errorMessage);

	void Initialize();

	void Initialize(IConnectionHandler connectionHandler);

	bool Listen(int serverPort);

	int GetPing(ushort playerId);

	int Ping();

	void SetPlayerID(ushort id, ulong lobbyId);

	void SetConnectionHandler(IConnectionHandler handler);

	void SendNetworkMessage(ushort playerId, int channel, byte[] data);

	void Shutdown();

	void ShutdownClient();

	void ShutdownServer();

	void Stop();

	void Dispose();

	void Dispose(bool silentDispose, bool isQuitting);
}
