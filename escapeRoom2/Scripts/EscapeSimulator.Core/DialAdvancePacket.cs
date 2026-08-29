using System.Text;

public sealed class DialAdvancePacket : Packet
{
	public Dial dial;

	public DialAnnotation annotation;

	public override byte getTypeId()
	{
		return 62;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(dial);
		writer.WriteComponent(annotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		dial = reader.ReadComponent<Dial>();
		annotation = reader.ReadComponent<DialAnnotation>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("dial: " + $"{dial}");
		stringBuilder.Append("annotation: " + $"{annotation}");
		return stringBuilder.ToString();
	}
}
