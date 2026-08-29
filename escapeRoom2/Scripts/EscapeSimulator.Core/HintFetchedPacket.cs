using System.Text;

public sealed class HintFetchedPacket : Packet
{
	public int puzzleIndex;

	public int hintIndex;

	public NetPlayerId playerThatRequestedHint;

	public override byte getTypeId()
	{
		return 107;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in puzzleIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hintIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteNetPlayerId(playerThatRequestedHint);
	}

	public override void readData(FastBinaryReader reader)
	{
		puzzleIndex = reader.ReadInt32();
		hintIndex = reader.ReadInt32();
		playerThatRequestedHint = reader.ReadNetPlayerId();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("puzzleIndex: " + $"{puzzleIndex}");
		stringBuilder.AppendLine("hintIndex: " + $"{hintIndex}");
		stringBuilder.Append("playerThatRequestedHint: " + $"{playerThatRequestedHint}");
		return stringBuilder.ToString();
	}
}
