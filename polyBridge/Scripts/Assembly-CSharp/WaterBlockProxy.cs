using System.Collections.Generic;
using UnityEngine;

public class WaterBlockProxy
{
	public Vector3 m_Pos;

	public float m_Width;

	public float m_Height;

	public bool m_LockPosition;

	public string m_UndoGuid;

	public WaterBlockProxy(Vector3 pos, float width, float height)
	{
		m_Pos = pos;
		m_Width = width;
		m_Height = height;
	}

	public WaterBlockProxy(WaterBlock waterBlock)
	{
		m_Pos = waterBlock.transform.position;
		m_Width = waterBlock.m_Width;
		m_Height = waterBlock.m_Height;
		m_LockPosition = waterBlock.m_LockPosition;
		m_UndoGuid = waterBlock.m_SandboxItem.m_UndoGuid;
	}

	public WaterBlockProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector3(m_Pos));
		list.AddRange(ByteSerializer.SerializeFloat(m_Width));
		list.AddRange(ByteSerializer.SerializeFloat(m_Height));
		list.AddRange(ByteSerializer.SerializeBool(m_LockPosition));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_Width = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_Height = ByteSerializer.DeserializeFloat(bytes, ref offset);
		if (version >= 12)
		{
			m_LockPosition = ByteSerializer.DeserializeBool(bytes, ref offset);
		}
	}
}
