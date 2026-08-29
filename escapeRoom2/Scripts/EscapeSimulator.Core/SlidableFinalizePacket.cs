using System.Text;

public sealed class SlidableFinalizePacket : Packet
{
	public Slidable slidable;

	public float percent;

	public override byte getTypeId()
	{
		return 39;
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
