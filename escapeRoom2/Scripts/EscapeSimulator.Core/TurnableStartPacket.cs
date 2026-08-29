using System.Text;

public sealed class TurnableStartPacket : Packet
{
	public Turnable turnable;

	public override byte getTypeId()
	{
		return 53;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(turnable);
	}

	public override void readData(FastBinaryReader reader)
	{
		turnable = reader.ReadComponent<Turnable>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("turnable: " + $"{turnable}");
		return stringBuilder.ToString();
	}
}
