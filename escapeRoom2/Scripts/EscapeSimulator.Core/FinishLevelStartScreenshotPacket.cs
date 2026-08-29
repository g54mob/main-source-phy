public sealed class FinishLevelStartScreenshotPacket : Packet
{
	public override byte getTypeId()
	{
		return 125;
	}

	public override void writeData(FastBinaryWriter writer)
	{
	}

	public override void readData(FastBinaryReader reader)
	{
	}
}
