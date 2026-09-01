namespace UdpKit.Platform.Photon
{
	internal enum ConnectState
	{
		Idle = 0,
		Connected = 1,
		Refused = 2,
		CreateRoomPending = 3,
		CreateRoomFailed = 4,
		JoinRoomPending = 5,
		JoinRoomFailed = 6,
		DisconnectPending = 7,
		DirectPending = 8,
		DirectFailed = 9,
		DirectSuccess = 10,
		RelayPending = 11,
		RelayFailed = 12,
		RelaySuccess = 13
	}
}
