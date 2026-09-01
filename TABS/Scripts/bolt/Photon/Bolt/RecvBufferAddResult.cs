namespace Photon.Bolt
{
	internal enum RecvBufferAddResult
	{
		Old = 0,
		OutOfBounds = 1,
		AlreadyExists = 2,
		Added = 3
	}
}
