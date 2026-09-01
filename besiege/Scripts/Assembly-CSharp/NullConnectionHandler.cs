internal class NullConnectionHandler : IConnectionHandler
{
	public void IncrementSentMessages()
	{
	}

	public void OnClientConnectionStateChanged(ClientConnectionState newState)
	{
	}

	public void OnConnected()
	{
	}

	public void OnDisconnected(bool wasOnDestroy)
	{
	}

	public void OnPlayerJoin(ushort playerId)
	{
	}

	public void OnPlayerLeave(ushort playerId)
	{
	}

	public void OnServerConnectionStateChanged(ServerConnectionState newState)
	{
	}

	public void OnDataEvent(ushort playerId, int channelId, byte[] buffer, int bufferSize)
	{
	}

	public void SetErrorMessage(string message)
	{
	}
}
