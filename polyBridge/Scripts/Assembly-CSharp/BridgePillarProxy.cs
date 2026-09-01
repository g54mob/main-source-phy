using System.Collections.Generic;
using UnityEngine;

public class BridgePillarProxy
{
	public Vector3 m_Pos;

	public float m_Height;

	public string m_PrefabName;

	public string m_Guid;

	public string m_AnchorGuid;

	public PrebuiltState m_BridgePrebuiltState;

	public BridgePillarProxy(BridgePillar bridgePillar)
	{
		m_Pos = bridgePillar.transform.position;
		m_Height = bridgePillar.GetTotalHeight();
		m_PrefabName = bridgePillar.name;
		m_Guid = bridgePillar.m_Guid;
		m_AnchorGuid = bridgePillar.m_AnchorGuid;
		m_BridgePrebuiltState = bridgePillar.m_PrebuiltState;
	}

	public BridgePillarProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector3(m_Pos));
		list.AddRange(ByteSerializer.SerializeFloat(m_Height));
		list.AddRange(ByteSerializer.SerializeString(m_PrefabName));
		list.AddRange(ByteSerializer.SerializeString(m_Guid));
		list.AddRange(ByteSerializer.SerializeString(m_AnchorGuid));
		list.AddRange(ByteSerializer.SerializeInt((int)m_BridgePrebuiltState));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector3(bytes, ref offset);
		m_Height = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_PrefabName = ByteSerializer.DeserializeString(bytes, ref offset);
		m_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_AnchorGuid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_BridgePrebuiltState = ((version >= 12) ? ((PrebuiltState)ByteSerializer.DeserializeInt(bytes, ref offset)) : PrebuiltState.NONE);
	}
}
