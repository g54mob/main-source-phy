public sealed class ConvertLevelToSinglePlayerPacket : Packet
{
	public override byte getTypeId()
	{
		return 128;
	}

	public override void writeData(FastBinaryWriter writer)
	{
	}

	public override void readData(FastBinaryReader reader)
	{
	}
}
