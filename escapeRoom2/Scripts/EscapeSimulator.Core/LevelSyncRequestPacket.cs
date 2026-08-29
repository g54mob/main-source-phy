public sealed class LevelSyncRequestPacket : Packet
{
	public override byte getTypeId()
	{
		return 2;
	}

	public override void writeData(FastBinaryWriter writer)
	{
	}

	public override void readData(FastBinaryReader reader)
	{
	}
}
