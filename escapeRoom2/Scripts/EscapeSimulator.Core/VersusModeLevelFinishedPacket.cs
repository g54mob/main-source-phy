using System.Text;

public sealed class VersusModeLevelFinishedPacket : Packet
{
	public float finishTime;

	public override byte getTypeId()
	{
		return 111;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in finishTime, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		finishTime = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("finishTime: " + $"{finishTime}");
		return stringBuilder.ToString();
	}
}
