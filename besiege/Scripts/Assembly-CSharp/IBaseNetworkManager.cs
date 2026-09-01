public interface IBaseNetworkManager
{
	ushort PlayerID { get; }

	void SetDisconnectMessage(string message);

	void OnTimeout();

	void OnConnect();

	void OnDisconnect(bool isOnDestroy);

	void Stop();

	void IncrementSentMessages();

	void OnPlayerJoin(ushort playerId);

	void OnPlayerLeave(ushort playerId);

	void OnServerMessage(ushort playerId, byte sentToClients, byte[] data, int offset);

	void OnClientMessage(ushort playerId, byte[] data, int offset);

	void OnLevelData(BesiegeDataFrame frame);

	void OnLogicData(BesiegeDataFrame frame);

	void OnMachineData(ushort playerId, BesiegeDataFrame frame);

	void OnInputData(ushort playerId, int session, byte[] data, int offset);

	void OnGhostData(ushort playerId, byte[] data, int offset, int size);

	void OnCamData(ushort playerId, byte[] data);

	void OnClientConnectionStateChanged(ClientConnectionState clientConnectionState);

	void OnServerConnectionStateChanged(ServerConnectionState serverConnectionState);

	void OnPingData(byte[] messageBuffer, int dataSize);
}
