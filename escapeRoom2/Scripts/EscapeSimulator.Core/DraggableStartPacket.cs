using System.Text;

public sealed class DraggableStartPacket : Packet
{
	public Draggable draggable;

	public override byte getTypeId()
	{
		return 32;
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
