using System.Text;

public sealed class DialStartPacket : Packet
{
	public Dial dial;

	public override byte getTypeId()
	{
		return 60;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(dial);
	}

	public override void readData(FastBinaryReader reader)
	{
		dial = reader.ReadComponent<Dial>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("dial: " + $"{dial}");
		return stringBuilder.ToString();
	}
}
