using System.Collections.Generic;
using UnityEngine;

public class PlatformProxy
{
	public Vector2 m_Pos;

	public float m_Width;

	public float m_Height;

	public bool m_Flipped;

	public bool m_Solid;

	public string m_UndoGuid;

	public PlatformProxy(Platform platform)
	{
		m_Pos = platform.transform.position;
		m_Width = platform.m_Width;
		m_Height = platform.m_Height;
		m_Flipped = platform.m_Flipped;
		m_Solid = platform.m_Solid;
		m_UndoGuid = platform.m_SandboxItem.m_UndoGuid;
	}

	public PlatformProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector2(m_Pos));
		list.AddRange(ByteSerializer.SerializeFloat(m_Width));
		list.AddRange(ByteSerializer.SerializeFloat(m_Height));
		list.AddRange(ByteSerializer.SerializeBool(m_Flipped));
		list.AddRange(ByteSerializer.SerializeBool(m_Solid));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector2(bytes, ref offset);
		m_Width = Mathf.Abs(ByteSerializer.DeserializeFloat(bytes, ref offset));
		m_Height = Mathf.Abs(ByteSerializer.DeserializeFloat(bytes, ref offset));
		m_Flipped = ByteSerializer.DeserializeBool(bytes, ref offset);
		if (version >= 22)
		{
			m_Solid = ByteSerializer.DeserializeBool(bytes, ref offset);
		}
		else
		{
			ByteSerializer.DeserializeInt(bytes, ref offset);
		}
	}
}
