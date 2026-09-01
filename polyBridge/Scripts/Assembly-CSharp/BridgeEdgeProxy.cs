using System.Collections.Generic;

public class BridgeEdgeProxy
{
	public BridgeMaterialType m_Material;

	public string m_Guid;

	public string m_NodeA_Guid;

	public string m_NodeB_Guid;

	public SplitJointPart m_JointAPart;

	public SplitJointPart m_JointBPart;

	public PrebuiltState m_BridgePrebuiltState;

	public BridgeEdgeProxy()
	{
	}

	public BridgeEdgeProxy(BridgeEdge edge)
	{
		m_Material = edge.m_Material.m_MaterialType;
		m_Guid = edge.m_Guid;
		m_NodeA_Guid = edge.m_JointA.m_Guid;
		m_NodeB_Guid = edge.m_JointB.m_Guid;
		m_JointAPart = edge.m_JointAPart;
		m_JointBPart = edge.m_JointBPart;
		m_BridgePrebuiltState = edge.m_PrebuiltState;
	}

	public BridgeEdgeProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeInt((int)m_Material));
		list.AddRange(ByteSerializer.SerializeString(m_NodeA_Guid));
		list.AddRange(ByteSerializer.SerializeString(m_NodeB_Guid));
		list.AddRange(ByteSerializer.SerializeInt((int)m_JointAPart));
		list.AddRange(ByteSerializer.SerializeInt((int)m_JointBPart));
		list.AddRange(ByteSerializer.SerializeInt((int)m_BridgePrebuiltState));
		list.AddRange(ByteSerializer.SerializeString(m_Guid));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Material = (BridgeMaterialType)ByteSerializer.DeserializeInt(bytes, ref offset);
		m_NodeA_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_NodeB_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_JointAPart = (SplitJointPart)ByteSerializer.DeserializeInt(bytes, ref offset);
		m_JointBPart = (SplitJointPart)ByteSerializer.DeserializeInt(bytes, ref offset);
		if (version == 10 || version == 11)
		{
			bool flag = ByteSerializer.DeserializeBool(bytes, ref offset);
			m_BridgePrebuiltState = (flag ? PrebuiltState.HARD_LOCKED : PrebuiltState.NONE);
		}
		m_BridgePrebuiltState = ((version >= 12) ? ((PrebuiltState)ByteSerializer.DeserializeInt(bytes, ref offset)) : PrebuiltState.NONE);
		m_Guid = ((version >= 15) ? ByteSerializer.DeserializeString(bytes, ref offset) : string.Empty);
	}
}
