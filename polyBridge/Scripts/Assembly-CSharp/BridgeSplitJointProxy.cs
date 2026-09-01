using System.Collections.Generic;

public class BridgeSplitJointProxy
{
	public string m_BridgeJointGuid;

	public SplitJointState m_SplitJointState;

	public BridgeSplitJointProxy(BridgeSplitJoint splitJoint)
	{
		m_BridgeJointGuid = splitJoint.m_BridgeJoint.m_Guid;
		m_SplitJointState = splitJoint.m_SplitJointState;
	}

	public BridgeSplitJointProxy(byte[] bytes, ref int offset)
	{
		DeserializeBinary(bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeString(m_BridgeJointGuid));
		list.AddRange(ByteSerializer.SerializeInt((int)m_SplitJointState));
		return list.ToArray();
	}

	public void DeserializeBinary(byte[] bytes, ref int offset)
	{
		m_BridgeJointGuid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_SplitJointState = (SplitJointState)ByteSerializer.DeserializeInt(bytes, ref offset);
	}
}
