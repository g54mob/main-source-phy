using System.Text;

public sealed class DialFinalizePacket : Packet
{
	public Dial dial;

	public float targetAngle;

	public override byte getTypeId()
	{
		return 63;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(dial);
		writer.Write(in targetAngle, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		dial = reader.ReadComponent<Dial>();
		targetAngle = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("dial: " + $"{dial}");
		stringBuilder.Append("targetAngle: " + $"{targetAngle}");
		return stringBuilder.ToString();
	}
}
