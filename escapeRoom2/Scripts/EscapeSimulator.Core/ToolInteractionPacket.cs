using System.Text;

public sealed class ToolInteractionPacket : Packet
{
	public Item tool;

	public ToolContext context;

	public override byte getTypeId()
	{
		return 104;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(tool);
		writer.WriteToolContext(context);
	}

	public override void readData(FastBinaryReader reader)
	{
		tool = reader.ReadComponent<Item>();
		context = reader.ReadToolContext();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("tool: " + $"{tool}");
		stringBuilder.Append("context: " + $"{context}");
		return stringBuilder.ToString();
	}
}
