public interface IConnectionHandler
{
	void IncrementSentMessages();

	void OnConnected();

	void OnDisconnected(bool wasOnDestroy);

	void OnPlayerJoin(ushort playerId);

	void OnPlayerLeave(ushort playerId);

	void OnDataEvent(ushort playerId, int channelId, byte[] buffer, int bufferSize);

	void SetErrorMessage(string message);

	void OnClientConnectionStateChanged(ClientConnectionState newState);

	void OnServerConnectionStateChanged(ServerConnectionState newState);
}
