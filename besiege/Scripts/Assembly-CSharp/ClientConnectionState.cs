public enum ClientConnectionState
{
	Disconnected = 0,
	AttemptingDirectConnect = 1,
	DirectConnectFailed = 2,
	ResolvingHost = 3,
	HostListReceived = 4,
	HostNotFound = 5,
	GettingZones = 6,
	FailedToGetZones = 7,
	FindingLobby = 8,
	LobbyNotFound = 9,
	JoiningLobby = 10,
	LobbyJoined = 11,
	FailedToJoinLobby = 12,
	PlayfabLogin = 13,
	Disconnecting = 14,
	PunchingThroughToServer = 15,
	HolePunchedFailed = 16,
	Connecting = 17,
	CRCMismatch = 18,
	Connected = 19
}
