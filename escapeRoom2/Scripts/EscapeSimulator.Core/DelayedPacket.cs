using System.Collections.Generic;

public class DelayedPacket
{
	public float timeToEnqueue;

	public Packet packet;

	public NetPlayerId receiver;

	public Dictionary<NetPlayerId, List<Packet>> sendMap;
}
