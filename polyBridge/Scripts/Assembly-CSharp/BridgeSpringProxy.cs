using System.Collections.Generic;

public class BridgeSpringProxy
{
	public float m_NormalizedValue;

	public string m_NodeA_Guid;

	public string m_NodeB_Guid;

	public string m_Guid;

	public BridgeSpringProxy(BridgeSpring spring)
	{
		m_NormalizedValue = spring.m_Slider.GetNormalizedValue();
		m_NodeA_Guid = spring.m_ParentEdge.m_JointA.m_Guid;
		m_NodeB_Guid = spring.m_ParentEdge.m_JointB.m_Guid;
		m_Guid = spring.m_Guid;
	}

	public BridgeSpringProxy(byte[] bytes, ref int offset)
	{
		DeserializeBinary(bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeFloat(m_NormalizedValue));
		list.AddRange(ByteSerializer.SerializeString(m_NodeA_Guid));
		list.AddRange(ByteSerializer.SerializeString(m_NodeB_Guid));
		list.AddRange(ByteSerializer.SerializeString(m_Guid));
		return list.ToArray();
	}

	public void DeserializeBinary(byte[] bytes, ref int offset)
	{
		m_NormalizedValue = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_NodeA_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_NodeB_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
	}
}
