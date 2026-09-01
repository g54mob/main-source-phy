using System.Collections.Generic;
using UnityEngine;

public class PistonProxy
{
	public float m_NormalizedValue;

	public string m_NodeA_Guid;

	public string m_NodeB_Guid;

	public string m_Guid;

	public PistonProxy(Piston piston)
	{
		m_NormalizedValue = piston.m_Slider.GetNormalizedValue();
		m_NodeA_Guid = piston.m_JointA.m_Guid;
		m_NodeB_Guid = piston.m_JointB.m_Guid;
		m_Guid = piston.m_Guid;
	}

	public PistonProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
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

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_NormalizedValue = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_NodeA_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_NodeB_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		if (version < 8)
		{
			m_NormalizedValue = FixupNormalizedValue(m_NormalizedValue);
		}
	}

	private static float FixupNormalizedValue(float normalizedValue)
	{
		if (normalizedValue < 0.25f)
		{
			return Mathf.Lerp(1f, 0.5f, Mathf.Clamp01(normalizedValue / 0.25f));
		}
		if (normalizedValue > 0.75f)
		{
			return Mathf.Lerp(0.5f, 1f, Mathf.Clamp01((normalizedValue - 0.75f) / 0.25f));
		}
		return Mathf.Lerp(0f, 0.5f, Mathf.Clamp01(Mathf.Abs(normalizedValue - 0.5f) / 0.25f));
	}
}
