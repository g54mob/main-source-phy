using System.Text;

public sealed class PuzzleCompletedPacket : Packet
{
	public string puzzleName;

	public int puzzleIndex;

	public bool puzzleRequiresTeamwork;

	public bool puzzleMuteSound;

	public override byte getTypeId()
	{
		return 99;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(puzzleName);
		writer.Write(in puzzleIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in puzzleRequiresTeamwork, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in puzzleMuteSound, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		puzzleName = reader.ReadString();
		puzzleIndex = reader.ReadInt32();
		puzzleRequiresTeamwork = reader.ReadBoolean();
		puzzleMuteSound = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("puzzleName: " + ToStringHelper.Stringify(puzzleName));
		stringBuilder.AppendLine("puzzleIndex: " + $"{puzzleIndex}");
		stringBuilder.AppendLine("puzzleRequiresTeamwork: " + $"{puzzleRequiresTeamwork}");
		stringBuilder.Append("puzzleMuteSound: " + $"{puzzleMuteSound}");
		return stringBuilder.ToString();
	}
}
