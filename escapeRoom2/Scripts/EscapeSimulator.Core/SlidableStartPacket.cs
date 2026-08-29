using System.Text;

public sealed class SlidableStartPacket : Packet
{
	public Slidable slidable;

	public override byte getTypeId()
	{
		return 37;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(slidable);
	}

	public override void readData(FastBinaryReader reader)
	{
		slidable = reader.ReadComponent<Slidable>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("slidable: " + $"{slidable}");
		return stringBuilder.ToString();
	}
}
