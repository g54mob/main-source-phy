using System.Text;

public sealed class LevelSyncReadyPacket : Packet
{
	public bool shouldStartInteractions;

	public override byte getTypeId()
	{
		return 6;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in shouldStartInteractions, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		shouldStartInteractions = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("shouldStartInteractions: " + $"{shouldStartInteractions}");
		return stringBuilder.ToString();
	}
}
