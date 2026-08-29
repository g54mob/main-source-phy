using System.Text;

public sealed class FinishLevelCheezeStatePacket : Packet
{
	public bool down;

	public override byte getTypeId()
	{
		return 120;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in down, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		down = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("down: " + $"{down}");
		return stringBuilder.ToString();
	}
}
