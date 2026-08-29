using System.Text;

public sealed class PingItemPacket : Packet
{
	public Item item;

	public override byte getTypeId()
	{
		return 89;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(item);
	}

	public override void readData(FastBinaryReader reader)
	{
		item = reader.ReadComponent<Item>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("item: " + $"{item}");
		return stringBuilder.ToString();
	}
}
