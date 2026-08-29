using System.Text;

public sealed class RouletteTriggeredPacket : Packet
{
	public Roulette roulette;

	public int randomTargetIndex;

	public int positionIndex;

	public override byte getTypeId()
	{
		return 112;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(roulette);
		writer.Write(in randomTargetIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in positionIndex, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		roulette = reader.ReadComponent<Roulette>();
		randomTargetIndex = reader.ReadInt32();
		positionIndex = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("roulette: " + $"{roulette}");
		stringBuilder.AppendLine("randomTargetIndex: " + $"{randomTargetIndex}");
		stringBuilder.Append("positionIndex: " + $"{positionIndex}");
		return stringBuilder.ToString();
	}
}
