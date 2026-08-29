using System.Text;

public sealed class PaintPaintablePacket : Packet
{
	public Paintable paintable;

	public float change;

	public bool gestureDown;

	public bool gestureUp;

	public PaintableEdit edit;

	public override byte getTypeId()
	{
		return 64;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(paintable);
		writer.Write(in change, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in gestureDown, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in gestureUp, default(FastBinaryWriter.ForPrimitives));
		writer.WritePaintableEdit(edit);
	}

	public override void readData(FastBinaryReader reader)
	{
		paintable = reader.ReadComponent<Paintable>();
		change = reader.ReadSingle();
		gestureDown = reader.ReadBoolean();
		gestureUp = reader.ReadBoolean();
		edit = reader.ReadPaintableEdit();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("paintable: " + $"{paintable}");
		stringBuilder.AppendLine("change: " + $"{change}");
		stringBuilder.AppendLine("gestureDown: " + $"{gestureDown}");
		stringBuilder.AppendLine("gestureUp: " + $"{gestureUp}");
		stringBuilder.Append("edit: " + $"{edit}");
		return stringBuilder.ToString();
	}
}
