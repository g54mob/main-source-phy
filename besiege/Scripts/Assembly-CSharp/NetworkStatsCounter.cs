using System;
using UnityEngine;

[Serializable]
public class NetworkStatsCounter
{
	private uint prevPacketsSent;

	private uint prevPacketsReceived;

	private double lastUpdate;

	private double prevBytesSent;

	private double prevBytesReceived;

	private DateTime startTime;

	private LinearWeightedMovingAverage bytesReceivedPerSecond = new LinearWeightedMovingAverage();

	private LinearWeightedMovingAverage bytesSentPerSecond = new LinearWeightedMovingAverage();

	private LinearWeightedMovingAverage packetsReceivedPerSecond = new LinearWeightedMovingAverage();

	private LinearWeightedMovingAverage packetsSentPerSecond = new LinearWeightedMovingAverage();

	private Guid guid;

	public uint BytesSent { get; private set; }

	public uint BytesReceived { get; private set; }

	public uint PacketsSent { get; private set; }

	public uint PacketsReceived { get; private set; }

	public int BytesReceivedPerSecond
	{
		get
		{
			return (int)bytesReceivedPerSecond.Average;
		}
	}

	public int BytesSentPerSecond
	{
		get
		{
			return (int)bytesSentPerSecond.Average;
		}
	}

	public int PacketsReceivedPerSecond
	{
		get
		{
			return (int)packetsReceivedPerSecond.Average;
		}
	}

	public int PacketsSentPerSecond
	{
		get
		{
			return (int)packetsSentPerSecond.Average;
		}
	}

	public double LastPacketSent { get; private set; }

	public double LastPacketReceived { get; private set; }

	public double TimeoutTime
	{
		get
		{
			return NetworkTime - LastPacketReceived;
		}
	}

	public double NetworkTime
	{
		get
		{
			return TimeSinceStart.TotalSeconds;
		}
	}

	public double NetworkTimeMS
	{
		get
		{
			return TimeSinceStart.TotalMilliseconds;
		}
	}

	private TimeSpan TimeSinceStart
	{
		get
		{
			return DateTime.Now - startTime;
		}
	}

	public NetworkStatsCounter()
	{
		guid = Guid.NewGuid();
		Clear();
		Touch();
	}

	public void IncrementBytesSent(uint size)
	{
		BytesSent += size;
		PacketsSent++;
		LastPacketSent = NetworkTime;
	}

	public void IncrementBytesReceived(uint size)
	{
		BytesReceived += size;
		PacketsReceived++;
		LastPacketReceived = NetworkTime;
	}

	public void Touch()
	{
		double lastPacketReceived = (LastPacketSent = NetworkTime);
		LastPacketReceived = lastPacketReceived;
	}

	public void Update()
	{
		if (!(lastUpdate + 1.0 > NetworkTime))
		{
			bytesSentPerSecond += (double)BytesSent - prevBytesSent;
			bytesReceivedPerSecond += (double)BytesReceived - prevBytesReceived;
			packetsSentPerSecond += (double)(PacketsSent - prevPacketsSent);
			packetsReceivedPerSecond += (double)(PacketsReceived - prevPacketsReceived);
			prevBytesSent = BytesSent;
			prevBytesReceived = BytesReceived;
			prevPacketsSent = PacketsSent;
			prevPacketsReceived = PacketsReceived;
			lastUpdate = NetworkTime;
		}
	}

	public void DebugStats()
	{
		string message = string.Format("[IN] {0} packets {1} bytes {2}k/s [OUT] {3} packets {4} bytes {5}k/s\nLastPacketReceived {6}, LastPacketSent {7}, TimeoutTime {8}", PacketsReceived, BytesReceived, BytesReceivedPerSecond, PacketsSent, BytesSent, BytesSentPerSecond, LastPacketReceived, LastPacketSent, TimeoutTime);
		Debug.Log(message);
		ConsoleController.ShowMessage(message);
	}

	public void Clear()
	{
		lastUpdate = 0.0;
		BytesReceived = 0u;
		BytesSent = 0u;
		LastPacketReceived = 0.0;
		LastPacketSent = 0.0;
		PacketsSent = 0u;
		PacketsReceived = 0u;
		bytesSentPerSecond.Clear();
		bytesReceivedPerSecond.Clear();
		packetsSentPerSecond.Clear();
		packetsReceivedPerSecond.Clear();
		startTime = DateTime.Now;
	}

	public override int GetHashCode()
	{
		return guid.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		NetworkStatsCounter networkStatsCounter = obj as NetworkStatsCounter;
		if (networkStatsCounter == null)
		{
			return false;
		}
		return guid.Equals(networkStatsCounter.guid);
	}
}
