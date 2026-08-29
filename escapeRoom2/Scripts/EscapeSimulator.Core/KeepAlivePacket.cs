public sealed class KeepAlivePacket : Packet
{
	public override byte getTypeId()
	{
		return 1;
	}

	public override void writeData(FastBinaryWriter writer)
	{
	}

	public override void readData(FastBinaryReader reader)
	{
	}
}
