using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectProxy
{
	public Vector3 m_Pos;

	public Vector3 m_Scale;

	public string m_PrefabName;

	public string m_UndoGuid;

	public FlyingObjectProxy(FlyingObject flyingObject)
	{
		m_Pos = flyingObject.transform.position;
		m_Scale = flyingObject.transform.localScale;
		m_PrefabName = flyingObject.name;
		m_UndoGuid = flyingObject.m_SandboxItem.m_UndoGuid;
	}

	public FlyingObjectProxy(byte[] bytes, ref int offset)
	{
		DeserializeBinary(bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector3(m_Pos));
		list.AddRange(ByteSerializer.SerializeVector3(m_Scale));
		list.AddRange(ByteSerializer.SerializeString(m_PrefabName));
		return list.ToArray();
	}

	public void DeserializeBinary(byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_Scale = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_PrefabName = ByteSerializer.DeserializeString(bytes, ref offset);
	}
}
