namespace UdpKit
{
	internal enum UdpConnectionError
	{
		None = 0,
		SequenceOutOfBounds = 1,
		IncorrectCommand = 2,
		SendWindowFull = 3,
		UnknownStreamChannel = 4,
		InvalidBlockNumber = 5
	}
}
