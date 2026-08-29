using System;
using System.Collections.Generic;

public class Batch
{
	public enum NetworkResult
	{
		Received = 0,
		Sent = 1,
		Failed = 2
	}

	public NetworkResult result;

	public Packet[] packets;

	public NetPlayerId senderId;

	public float localTime;

	public int headerSize;

	public int dataSize;

	public int totalSize => headerSize + dataSize;

	public bool isIgnored(HashSet<NetPlayerId> ignoredPlayerIds, HashSet<Type> selectedPacketTypes)
	{
		if (ignoredPlayerIds.Contains(senderId))
		{
			return true;
		}
		Packet[] array = packets;
		foreach (Packet packet in array)
		{
			if (selectedPacketTypes.Contains(packet.GetType()))
			{
				return false;
			}
		}
		return true;
	}

	public int getTotalSizeWithoutIgnoredPackets(HashSet<Type> selectedPacketTypes)
	{
		int num = 0;
		Packet[] array = packets;
		foreach (Packet packet in array)
		{
			if (selectedPacketTypes.Contains(packet.GetType()))
			{
				num += packet.totalSize;
			}
		}
		return headerSize + num;
	}
}
