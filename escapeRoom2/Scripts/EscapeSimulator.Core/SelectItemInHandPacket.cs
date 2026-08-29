using System.Text;

public sealed class SelectItemInHandPacket : Packet
{
	public Item selectedItem;

	public override byte getTypeId()
	{
		return 86;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(selectedItem);
	}

	public override void readData(FastBinaryReader reader)
	{
		selectedItem = reader.ReadComponent<Item>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("selectedItem: " + $"{selectedItem}");
		return stringBuilder.ToString();
	}
}
