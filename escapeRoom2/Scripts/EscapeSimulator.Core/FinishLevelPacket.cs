using System.Text;

public sealed class FinishLevelPacket : Packet
{
	public Finish finish;

	public override byte getTypeId()
	{
		return 15;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(finish);
	}

	public override void readData(FastBinaryReader reader)
	{
		finish = reader.ReadComponent<Finish>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("finish: " + $"{finish}");
		return stringBuilder.ToString();
	}
}
