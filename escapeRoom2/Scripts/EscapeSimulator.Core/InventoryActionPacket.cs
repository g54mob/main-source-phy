using System.Text;

public sealed class InventoryActionPacket : Packet
{
	public Item item;

	public bool isAddedToInventory;

	public bool disableItemWhenRemoving;

	public override byte getTypeId()
	{
		return 75;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(item);
		writer.Write(in isAddedToInventory, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in disableItemWhenRemoving, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		item = reader.ReadComponent<Item>();
		isAddedToInventory = reader.ReadBoolean();
		disableItemWhenRemoving = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("item: " + $"{item}");
		stringBuilder.AppendLine("isAddedToInventory: " + $"{isAddedToInventory}");
		stringBuilder.Append("disableItemWhenRemoving: " + $"{disableItemWhenRemoving}");
		return stringBuilder.ToString();
	}
}
