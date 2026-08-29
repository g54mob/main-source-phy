using System.Text;

public sealed class FinishLevelSwapPlaceRequestPacket : Packet
{
	public int index1;

	public int index2;

	public override byte getTypeId()
	{
		return 117;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in index1, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in index2, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		index1 = reader.ReadInt32();
		index2 = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("index1: " + $"{index1}");
		stringBuilder.Append("index2: " + $"{index2}");
		return stringBuilder.ToString();
	}
}
