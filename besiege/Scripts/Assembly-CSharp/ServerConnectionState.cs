public enum ServerConnectionState
{
	Disconnected = 0,
	InitializingHost = 1,
	InitializationFailed = 2,
	WaitingForConnection = 3,
	WaitingForPlatformConnection = 4,
	Connected = 5
}
