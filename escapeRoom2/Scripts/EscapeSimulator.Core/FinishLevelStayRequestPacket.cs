public sealed class FinishLevelStayRequestPacket : Packet
{
	public override byte getTypeId()
	{
		return 121;
	}

	public override void writeData(FastBinaryWriter writer)
	{
	}

	public override void readData(FastBinaryReader reader)
	{
	}
}
