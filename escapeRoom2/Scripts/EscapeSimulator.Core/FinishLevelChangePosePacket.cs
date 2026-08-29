using System.Text;

public sealed class FinishLevelChangePosePacket : Packet
{
	public int poseIndex;

	public override byte getTypeId()
	{
		return 116;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in poseIndex, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		poseIndex = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("poseIndex: " + $"{poseIndex}");
		return stringBuilder.ToString();
	}
}
