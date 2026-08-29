public sealed class StoppedEndScreenAnimationPacket : Packet
{
	public override byte getTypeId()
	{
		return 11;
	}

	public override void writeData(FastBinaryWriter writer)
	{
	}

	public override void readData(FastBinaryReader reader)
	{
	}
}
