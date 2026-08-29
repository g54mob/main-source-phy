using System.Text;

public sealed class DraggableFinalizePacket : Packet
{
	public Draggable draggable;

	public override byte getTypeId()
	{
		return 34;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(draggable);
	}

	public override void readData(FastBinaryReader reader)
	{
		draggable = reader.ReadComponent<Draggable>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("draggable: " + $"{draggable}");
		return stringBuilder.ToString();
	}
}
