using System.Text;

public sealed class TurnableInteractionPacket : Packet
{
	public Turnable turnable;

	public float rotationChange;

	public override byte getTypeId()
	{
		return 54;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(turnable);
		writer.Write(in rotationChange, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		turnable = reader.ReadComponent<Turnable>();
		rotationChange = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("turnable: " + $"{turnable}");
		stringBuilder.Append("rotationChange: " + $"{rotationChange}");
		return stringBuilder.ToString();
	}
}
