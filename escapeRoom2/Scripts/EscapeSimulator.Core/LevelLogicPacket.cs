using System.Text;
using UnityEngine;

public sealed class LevelLogicPacket : Packet
{
	public byte id;

	public byte[] data;

	public Packet toConcretePacket(LevelLogic levelLogic)
	{
		Packet packet = levelLogic.getPacket(id);
		if (packet == null)
		{
			Debug.LogError(string.Format("Could not get '{0}' with ID {1} from '{2}'.", "Packet", id, levelLogic), levelLogic);
			return null;
		}
		packet.packetId = packetId;
		packet.sessionId = sessionId;
		packet.receiverId = receiverId;
		packet.senderId = senderId;
		using FastBinaryReader reader = new FastBinaryReader(data);
		packet.readData(reader);
		packet.dataSize = reader.Position;
		return packet;
	}

	public static LevelLogicPacket fromConcretePacket(Packet concretePacket)
	{
		FastBinaryWriter writer;
		using (SharedWriter.borrow(out writer))
		{
			concretePacket.writeData(writer);
			return new LevelLogicPacket
			{
				packetId = concretePacket.packetId,
				sessionId = concretePacket.sessionId,
				receiverId = concretePacket.receiverId,
				senderId = concretePacket.senderId,
				id = concretePacket.getTypeId(),
				data = writer.ToArray()
			};
		}
	}

	public override byte getTypeId()
	{
		return 0;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(id);
		writer.WriteByteArray(data);
	}

	public override void readData(FastBinaryReader reader)
	{
		id = reader.ReadByte();
		data = reader.ReadByteArray();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("id: " + $"{id}");
		stringBuilder.Append("data: " + ToStringHelper.Stringify(data, (byte e) => $"{e}"));
		return stringBuilder.ToString();
	}
}
