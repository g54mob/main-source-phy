using System.Text;

public sealed class TurnableAdvancePacket : Packet
{
	public Turnable turnable;

	public TurnableAnnotation annotation;

	public override byte getTypeId()
	{
		return 55;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(turnable);
		writer.WriteComponent(annotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		turnable = reader.ReadComponent<Turnable>();
		annotation = reader.ReadComponent<TurnableAnnotation>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("turnable: " + $"{turnable}");
		stringBuilder.Append("annotation: " + $"{annotation}");
		return stringBuilder.ToString();
	}
}
