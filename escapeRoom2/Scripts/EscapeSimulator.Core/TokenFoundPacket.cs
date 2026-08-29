using System.Text;

public sealed class TokenFoundPacket : Packet
{
	public Token token;

	public override byte getTypeId()
	{
		return 94;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(token);
	}

	public override void readData(FastBinaryReader reader)
	{
		token = reader.ReadComponent<Token>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("token: " + $"{token}");
		return stringBuilder.ToString();
	}
}
