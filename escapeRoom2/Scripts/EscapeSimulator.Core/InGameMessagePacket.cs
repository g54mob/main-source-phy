using System.Text;

public sealed class InGameMessagePacket : Packet
{
	public string messageText;

	public ChatMessage.Type messageType;

	public override byte getTypeId()
	{
		return 96;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(messageText);
		int value = (int)messageType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		messageText = reader.ReadString();
		messageType = (ChatMessage.Type)reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("messageText: " + ToStringHelper.Stringify(messageText));
		stringBuilder.Append("messageType: " + $"{messageType}");
		return stringBuilder.ToString();
	}
}
