namespace Photon.Bolt
{
	internal enum ReplicationMode
	{
		EveryoneExceptController = 0,
		Everyone = 1,
		OnlyOwnerAndController = 2,
		LocalForEachPlayer = 3
	}
}
