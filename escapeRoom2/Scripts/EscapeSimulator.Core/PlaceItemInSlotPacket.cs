using System.Text;

public sealed class PlaceItemInSlotPacket : Packet
{
	public Slot slot;

	public Item item;

	public SlotPlacement placement;

	public override byte getTypeId()
	{
		return 29;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(slot);
		writer.WriteComponent(item);
		writer.Write((byte)placement);
	}

	public override void readData(FastBinaryReader reader)
	{
		slot = reader.ReadComponent<Slot>();
		item = reader.ReadComponent<Item>();
		placement = (SlotPlacement)reader.ReadByte();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("slot: " + $"{slot}");
		stringBuilder.AppendLine("item: " + $"{item}");
		stringBuilder.Append("placement: " + $"{placement}");
		return stringBuilder.ToString();
	}
}
