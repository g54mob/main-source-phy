using System.Text;

public abstract class Packet
{
	public uint packetId;

	public int sessionId;

	public NetPlayerId receiverId = NetPlayerId.Empty;

	public NetPlayerId senderId = NetPlayerId.Empty;

	public float sendTime;

	public int dataSize;

	public string stackTrace;

	public int headerSize => 9 + receiverId.getSizeInBytes();

	public int totalSize => headerSize + dataSize;

	public abstract byte getTypeId();

	public void write(FastBinaryWriter writer)
	{
		writer.Write(getTypeId());
		writer.Write(in packetId, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in sessionId, default(FastBinaryWriter.ForPrimitives));
		writer.WriteNetPlayerId(receiverId);
		writer.WriteNetPlayerId(senderId);
		int position = writer.Position;
		writeData(writer);
		dataSize = writer.Position - position;
	}

	public void read(FastBinaryReader reader)
	{
		packetId = reader.ReadUInt32();
		sessionId = reader.ReadInt32();
		receiverId = reader.ReadNetPlayerId();
		senderId = reader.ReadNetPlayerId();
		int position = reader.Position;
		readData(reader);
		dataSize = reader.Position - position;
	}

	public abstract void writeData(FastBinaryWriter writer);

	public abstract void readData(FastBinaryReader reader);

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"type: {GetType()}");
		stringBuilder.AppendLine(string.Format("{0}: {1}", "packetId", packetId));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "sessionId", sessionId));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "senderId", senderId));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "receiverId", receiverId));
		stringBuilder.AppendLine();
		stringBuilder.AppendLine(string.Format("{0}: {1}", "headerSize", headerSize));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "dataSize", dataSize));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "totalSize", totalSize));
		return stringBuilder.ToString();
	}
}
