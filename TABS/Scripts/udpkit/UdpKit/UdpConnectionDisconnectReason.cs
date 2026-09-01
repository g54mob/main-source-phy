namespace UdpKit
{
	public enum UdpConnectionDisconnectReason
	{
		Unknown = 0,
		Timeout = 1,
		Error = 2,
		Disconnected = 3,
		Authentication = 4,
		MaxCCUReached = 5,
		CloudTimeout = 6
	}
}
