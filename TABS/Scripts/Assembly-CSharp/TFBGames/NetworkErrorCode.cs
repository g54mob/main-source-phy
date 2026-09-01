namespace TFBGames
{
	public enum NetworkErrorCode
	{
		SystemIsRunning = 0,
		Shutdown = 1,
		FailedToStart = 2,
		FailedToConnectToServer = 3,
		ConnectionRefused = 4,
		Disconnected = 5,
		FailedToConnectToSession = 6,
		FailedToCreateSession = 7,
		ServerModeRequired = 8,
		ClientModeRequired = 9,
		Timeout = 10,
		ServiceIsBusyWithAsync = 11,
		UserCancelled = 12,
		FailedToCreatePlatformSession = 13,
		FailedToConnectToPlatformSession = 14,
		FailedToLeaveToPlatformSession = 15,
		SystemIsRunningInWrongRegion = 16,
		VersionMismatch = 17,
		FailedToRetrieveJoinData = 18,
		NoInternetConnection = 19,
		UserAuthenticationFailed = 20
	}
}
