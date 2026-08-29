using System.Text;

public sealed class UpdateLadderPacket : Packet
{
	public Game.LadderMovementRuntimeData ladderData;

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteIReadWrite(ladderData);
	}

	public override void readData(FastBinaryReader reader)
	{
		ladderData = reader.ReadIReadWrite<Game.LadderMovementRuntimeData>();
	}

	public override byte getTypeId()
	{
		return 26;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("ladderData: " + ToStringHelper.Stringify(ladderData));
		return stringBuilder.ToString();
	}
}
