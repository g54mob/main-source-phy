using System.Text;

public sealed class SyncTimerTimePacket : Packet
{
	public float timerTime;

	public override byte getTypeId()
	{
		return 95;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in timerTime, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		timerTime = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("timerTime: " + $"{timerTime}");
		return stringBuilder.ToString();
	}
}
