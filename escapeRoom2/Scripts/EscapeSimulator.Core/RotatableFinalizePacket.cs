using System.Text;

public sealed class RotatableFinalizePacket : Packet
{
	public Rotatable rotatable;

	public override byte getTypeId()
	{
		return 67;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(rotatable);
	}

	public override void readData(FastBinaryReader reader)
	{
		rotatable = reader.ReadComponent<Rotatable>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("rotatable: " + $"{rotatable}");
		return stringBuilder.ToString();
	}
}
