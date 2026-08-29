using System.Text;

public sealed class DialInteractionPacket : Packet
{
	public Dial dial;

	public float angleChangeDelta;

	public DialAnnotation annotation;

	public override byte getTypeId()
	{
		return 61;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(dial);
		writer.Write(in angleChangeDelta, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(annotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		dial = reader.ReadComponent<Dial>();
		angleChangeDelta = reader.ReadSingle();
		annotation = reader.ReadComponent<DialAnnotation>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("dial: " + $"{dial}");
		stringBuilder.AppendLine("angleChangeDelta: " + $"{angleChangeDelta}");
		stringBuilder.Append("annotation: " + $"{annotation}");
		return stringBuilder.ToString();
	}
}
