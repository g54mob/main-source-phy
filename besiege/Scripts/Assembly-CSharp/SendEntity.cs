using System;

public class SendEntity
{
	public bool hasPosition;

	public bool hasRotation;

	public bool hasVector;

	public byte[] Position;

	public byte[] Rotation;

	public byte[] Vector;

	public byte[] EventList;

	public int eventCount;

	public uint id;

	public static int MAX_EVENTS = 32;

	public SendEntity(bool compressed)
	{
		EventList = new byte[MAX_EVENTS];
		if (compressed)
		{
			Position = new byte[6];
			Rotation = new byte[7];
			Vector = new byte[6];
		}
		else
		{
			Position = new byte[12];
			Rotation = new byte[16];
			Vector = new byte[12];
		}
		Clear();
	}

	public void Clear()
	{
		hasPosition = (hasRotation = (hasVector = false));
		eventCount = 0;
	}

	public void Set(SendEntity entity)
	{
		hasPosition = entity.hasPosition;
		if (hasPosition)
		{
			Buffer.BlockCopy(entity.Position, 0, Position, 0, Position.Length);
		}
		hasRotation = entity.hasRotation;
		if (hasRotation)
		{
			Buffer.BlockCopy(entity.Rotation, 0, Rotation, 0, Rotation.Length);
		}
		hasVector = entity.hasVector;
		if (hasVector)
		{
			Buffer.BlockCopy(entity.Vector, 0, Vector, 0, Vector.Length);
		}
		eventCount = entity.eventCount;
		Buffer.BlockCopy(entity.EventList, 0, EventList, 0, eventCount);
	}

	public void AddPosition(byte[] data, int offset)
	{
		hasPosition = true;
		Buffer.BlockCopy(data, offset, Position, 0, Position.Length);
	}

	public void AddRotation(byte[] data, int offset)
	{
		hasRotation = true;
		Buffer.BlockCopy(data, offset, Rotation, 0, Rotation.Length);
	}

	public void AddVector(byte[] data, int offset)
	{
		hasVector = true;
		Buffer.BlockCopy(data, offset, Vector, 0, Vector.Length);
	}

	public void AddPosition(byte[] pos)
	{
		AddPosition(pos, 0);
	}

	public void AddRotation(byte[] rot)
	{
		AddRotation(rot, 0);
	}

	public void AddVector(byte[] vec)
	{
		AddVector(vec, 0);
	}

	public void AddEvent(byte eventCode)
	{
		EventList[eventCount++] = eventCode;
	}

	public static int GetMaxDataSize(bool compressed)
	{
		return 1 + ((!compressed) ? 40 : 19) + MAX_EVENTS;
	}

	public static int GetDataSize(byte entityState, bool compressed)
	{
		return 1 + ((!compressed) ? ((HasPosition(entityState) ? 12 : 0) + (HasRotation(entityState) ? 16 : 0) + (HasVector(entityState) ? 12 : 0)) : ((HasPosition(entityState) ? 6 : 0) + (HasRotation(entityState) ? 7 : 0) + (HasVector(entityState) ? 6 : 0))) + EventCount(entityState);
	}

	public int GetEventDataSize()
	{
		return 1 + eventCount;
	}

	public int GetDataSize()
	{
		return 1 + (hasPosition ? Position.Length : 0) + (hasRotation ? Rotation.Length : 0) + (hasVector ? Vector.Length : 0) + eventCount;
	}

	public static int EventCount(byte entityState)
	{
		return entityState >> 3;
	}

	public static bool HasPosition(byte entityState)
	{
		return (entityState & 1) != 0;
	}

	public static bool HasVector(byte entityState)
	{
		return (entityState & 2) != 0;
	}

	public static bool HasRotation(byte entityState)
	{
		return (entityState & 4) != 0;
	}

	public void Encode(byte[] buffer, int offset)
	{
		buffer[offset] = (byte)((hasPosition ? 1 : 0) | (hasVector ? 2 : 0) | (hasRotation ? 4 : 0) | (eventCount << 3));
		offset++;
		Buffer.BlockCopy(EventList, 0, buffer, offset, eventCount);
		offset += eventCount;
		if (hasPosition)
		{
			Buffer.BlockCopy(Position, 0, buffer, offset, Position.Length);
			offset += Position.Length;
		}
		if (hasRotation)
		{
			Buffer.BlockCopy(Rotation, 0, buffer, offset, Rotation.Length);
			offset += Rotation.Length;
		}
		if (hasVector)
		{
			Buffer.BlockCopy(Vector, 0, buffer, offset, Vector.Length);
			offset += Vector.Length;
		}
	}
}
