using System;

public static class Space3
{
	public static int getPacketCount()
	{
		return 3;
	}

	public static Packet getPacket(byte id)
	{
		return id switch
		{
			0 => new Space3Logic.FabricatorButtonFixPacket(), 
			1 => new Space3Logic.WeldPacket(), 
			2 => new Space3Logic.ElevatorMovePacket(), 
			_ => null, 
		};
	}

	public static Type getPacketType(byte id)
	{
		return id switch
		{
			0 => typeof(Space3Logic.FabricatorButtonFixPacket), 
			1 => typeof(Space3Logic.WeldPacket), 
			2 => typeof(Space3Logic.ElevatorMovePacket), 
			_ => null, 
		};
	}
}
