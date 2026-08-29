using System;

public static class Space2
{
	public static int getPacketCount()
	{
		return 1;
	}

	public static Packet getPacket(byte id)
	{
		if (id == 0)
		{
			return new Space2Logic.PlasmaDistancePacket();
		}
		return null;
	}

	public static Type getPacketType(byte id)
	{
		if (id == 0)
		{
			return typeof(Space2Logic.PlasmaDistancePacket);
		}
		return null;
	}
}
