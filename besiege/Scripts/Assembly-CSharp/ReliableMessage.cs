using System;

public abstract class ReliableMessage
{
	private const int HeaderLength = 12;

	public byte[] Data { get; set; }

	public uint MessageID { get; set; }

	public uint Timestamp { get; set; }

	public uint Frame { get; set; }

	public ReliableMessage()
	{
	}

	public ReliableMessage(uint messageID, uint timestamp, uint frame, byte[] buffer)
	{
		MessageID = messageID;
		Timestamp = timestamp;
		Frame = frame;
		if (buffer != null)
		{
			Data = new byte[buffer.Length];
			Buffer.BlockCopy(buffer, 0, Data, 0, buffer.Length);
		}
	}

	public override bool Equals(object obj)
	{
		ReliableMessage reliableMessage = obj as ReliableMessage;
		if (reliableMessage == null)
		{
			return false;
		}
		return MessageID.Equals(reliableMessage.MessageID);
	}

	public override int GetHashCode()
	{
		return MessageID.GetHashCode();
	}

	public void Unpack(byte[] buffer)
	{
		MessageID = NetworkCompression.ReadUInt(false, buffer, 0);
		Timestamp = NetworkCompression.ReadUInt(false, buffer, 4);
		Frame = NetworkCompression.ReadUInt(false, buffer, 8);
		if (buffer.Length > 12)
		{
			Data = new byte[buffer.Length - 12];
			Buffer.BlockCopy(buffer, 12, Data, 0, buffer.Length - 12);
		}
	}

	public byte[] GetBytes()
	{
		byte[] array = ((Data != null) ? new byte[12 + Data.Length] : new byte[12]);
		NetworkCompression.WriteUInt(MessageID, false, array, 0);
		NetworkCompression.WriteUInt(Timestamp, false, array, 4);
		NetworkCompression.WriteUInt(Frame, false, array, 8);
		if (Data != null)
		{
			Buffer.BlockCopy(Data, 0, array, 12, Data.Length);
		}
		return array;
	}

	public override string ToString()
	{
		return string.Format("[ReliableMessage] ID={0} Timestamp={1}, Frame={2}, data={3}", MessageID, Timestamp, Frame, ConnectionHelper.DebugMessage(Data, 12));
	}
}
