using System;

public static class Lobby
{
	public static int getPacketCount()
	{
		return 4;
	}

	public static Packet getPacket(byte id)
	{
		return id switch
		{
			0 => new LobbyLogic.LobbyImageListPacket(), 
			1 => new LobbyLogic.LobbyCurrentImageListPacket(), 
			2 => new LobbyLogic.LobbyImageRequestPacket(), 
			3 => new LobbyLogic.LobbyImageResponsePacket(), 
			_ => null, 
		};
	}

	public static Type getPacketType(byte id)
	{
		return id switch
		{
			0 => typeof(LobbyLogic.LobbyImageListPacket), 
			1 => typeof(LobbyLogic.LobbyCurrentImageListPacket), 
			2 => typeof(LobbyLogic.LobbyImageRequestPacket), 
			3 => typeof(LobbyLogic.LobbyImageResponsePacket), 
			_ => null, 
		};
	}
}
