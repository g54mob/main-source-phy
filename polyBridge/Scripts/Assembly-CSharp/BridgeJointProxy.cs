using System.Collections.Generic;
using UnityEngine;

public class BridgeJointProxy
{
	public Vector3 m_Pos;

	public bool m_IsAnchor;

	public bool m_IsSplit;

	public bool m_NoBuild;

	public string m_Guid;

	public BridgeJointProxy()
	{
	}

	public BridgeJointProxy(BridgeJoint joint)
	{
		m_Pos = joint.transform.position;
		m_IsAnchor = joint.m_IsAnchor;
		m_IsSplit = joint.m_IsSplit;
		m_NoBuild = joint.m_NoBuild;
		m_Guid = joint.m_Guid;
	}

	public BridgeJointProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector3(m_Pos));
		list.AddRange(ByteSerializer.SerializeBool(m_IsAnchor));
		list.AddRange(ByteSerializer.SerializeBool(m_IsSplit));
		list.AddRange(ByteSerializer.SerializeString(m_Guid));
		list.AddRange(ByteSerializer.SerializeBool(m_NoBuild));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_IsAnchor = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_IsSplit = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_NoBuild = version >= 13 && ByteSerializer.DeserializeBool(bytes, ref offset);
	}
}
