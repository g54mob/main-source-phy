using System.Text;

public sealed class KickPlayerPacket : Packet
{
	public NetPlayerId playerBeingKicked;

	public override byte getTypeId()
	{
		return 91;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteNetPlayerId(playerBeingKicked);
	}

	public override void readData(FastBinaryReader reader)
	{
		playerBeingKicked = reader.ReadNetPlayerId();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("playerBeingKicked: " + $"{playerBeingKicked}");
		return stringBuilder.ToString();
	}
}
