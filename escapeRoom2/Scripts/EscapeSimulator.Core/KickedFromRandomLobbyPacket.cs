public sealed class KickedFromRandomLobbyPacket : Packet
{
	public override byte getTypeId()
	{
		return 9;
	}

	public override void writeData(FastBinaryWriter writer)
	{
	}

	public override void readData(FastBinaryReader reader)
	{
	}
}
