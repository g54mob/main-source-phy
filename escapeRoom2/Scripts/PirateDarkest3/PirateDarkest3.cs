using System;

public static class PirateDarkest3
{
	public static int getPacketCount()
	{
		return 1;
	}

	public static Packet getPacket(byte id)
	{
		if (id == 0)
		{
			return new PirateDarkest3Logic.OnStartGunpowderPacket();
		}
		return null;
	}

	public static Type getPacketType(byte id)
	{
		if (id == 0)
		{
			return typeof(PirateDarkest3Logic.OnStartGunpowderPacket);
		}
		return null;
	}
}
