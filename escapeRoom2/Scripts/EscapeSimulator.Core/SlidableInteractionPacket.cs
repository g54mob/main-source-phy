using System.Text;

public sealed class SlidableInteractionPacket : Packet
{
	public Slidable slidable;

	public float percent;

	public override byte getTypeId()
	{
		return 38;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(slidable);
		writer.Write(in percent, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		slidable = reader.ReadComponent<Slidable>();
		percent = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("slidable: " + $"{slidable}");
		stringBuilder.Append("percent: " + $"{percent}");
		return stringBuilder.ToString();
	}
}
