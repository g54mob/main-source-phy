using System.Text;

public sealed class LevelPredicateCallPacket : Packet
{
	public int predicateType;

	public bool isDone;

	public override byte getTypeId()
	{
		return 100;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in predicateType, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isDone, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		predicateType = reader.ReadInt32();
		isDone = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("predicateType: " + $"{predicateType}");
		stringBuilder.Append("isDone: " + $"{isDone}");
		return stringBuilder.ToString();
	}
}
