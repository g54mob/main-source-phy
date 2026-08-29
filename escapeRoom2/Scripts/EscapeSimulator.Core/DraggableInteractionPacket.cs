using System.Text;

public sealed class DraggableInteractionPacket : Packet
{
	public Draggable draggable;

	public bool hasMoved;

	public override byte getTypeId()
	{
		return 33;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(draggable);
		writer.Write(in hasMoved, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		draggable = reader.ReadComponent<Draggable>();
		hasMoved = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("draggable: " + $"{draggable}");
		stringBuilder.Append("hasMoved: " + $"{hasMoved}");
		return stringBuilder.ToString();
	}
}
