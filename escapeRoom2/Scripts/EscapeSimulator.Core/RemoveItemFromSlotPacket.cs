using System.Text;

public sealed class RemoveItemFromSlotPacket : Packet
{
	public Slot slot;

	public Item item;

	public override byte getTypeId()
	{
		return 31;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(slot);
		writer.WriteComponent(item);
	}

	public override void readData(FastBinaryReader reader)
	{
		slot = reader.ReadComponent<Slot>();
		item = reader.ReadComponent<Item>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("slot: " + $"{slot}");
		stringBuilder.Append("item: " + $"{item}");
		return stringBuilder.ToString();
	}
}
