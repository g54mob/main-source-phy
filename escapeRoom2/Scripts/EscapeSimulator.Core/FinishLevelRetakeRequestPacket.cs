public sealed class FinishLevelRetakeRequestPacket : Packet
{
	public override byte getTypeId()
	{
		return 123;
	}

	public override void writeData(FastBinaryWriter writer)
	{
	}

	public override void readData(FastBinaryReader reader)
	{
	}
}
