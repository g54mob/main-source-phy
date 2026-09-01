using System.Collections.Generic;
using UnityEngine;

public class PillarProxy
{
	public Vector3 m_Pos;

	public float m_Height;

	public string m_PrefabName;

	public string m_UndoGuid;

	public PillarProxy(Pillar pillar)
	{
		m_Pos = pillar.transform.position;
		m_Height = pillar.m_Height;
		m_PrefabName = pillar.name;
		m_UndoGuid = pillar.m_SandboxItem.m_UndoGuid;
	}

	public PillarProxy(byte[] bytes, ref int offset)
	{
		DeserializeBinary(bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector3(m_Pos));
		list.AddRange(ByteSerializer.SerializeFloat(m_Height));
		list.AddRange(ByteSerializer.SerializeString(m_PrefabName));
		return list.ToArray();
	}

	public void DeserializeBinary(byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_Height = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_PrefabName = ByteSerializer.DeserializeString(bytes, ref offset);
	}
}
