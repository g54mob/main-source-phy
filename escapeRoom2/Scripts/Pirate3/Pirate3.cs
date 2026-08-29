using System;

public static class Pirate3
{
	public static int getPacketCount()
	{
		return 1;
	}

	public static Packet getPacket(byte id)
	{
		if (id == 0)
		{
			return new Pirate3Logic.OnStartGunpowderPacket();
		}
		return null;
	}

	public static Type getPacketType(byte id)
	{
		if (id == 0)
		{
			return typeof(Pirate3Logic.OnStartGunpowderPacket);
		}
		return null;
	}
}
