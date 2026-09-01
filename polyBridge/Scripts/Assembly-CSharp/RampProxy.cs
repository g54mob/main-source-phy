using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class RampProxy
{
	public Vector2 m_Pos;

	public List<Vector2> m_ControlPoints = new List<Vector2>();

	public float m_Height;

	public int m_NumSegments;

	public Dreamteck.Splines.Spline.Type m_SplineType;

	public bool m_FlippedVertical;

	public bool m_FlippedHorizontal;

	public bool m_FlippedLegs;

	public bool m_HideLegs;

	public List<Vector2> m_LinePoints = new List<Vector2>();

	public string m_UndoGuid;

	private Ramp m_RampReference;

	public RampProxy(Ramp ramp)
	{
		m_Pos = ramp.transform.position;
		m_ControlPoints = ramp.GetControlPointPositions();
		m_Height = ramp.m_Height;
		m_NumSegments = ramp.m_NumSegments;
		m_SplineType = ramp.m_SplineType;
		m_FlippedVertical = ramp.m_FlippedVertical;
		m_FlippedHorizontal = ramp.m_FlippedHorizontal;
		m_FlippedLegs = ramp.m_FlippedLegs;
		m_HideLegs = ramp.m_HideLegs;
		m_LinePoints = ramp.GetLinePoints();
		m_UndoGuid = ramp.m_SandboxItem.m_UndoGuid;
		m_RampReference = ramp;
	}

	public RampProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector2(m_Pos));
		SerializeControlPoints(list);
		list.AddRange(ByteSerializer.SerializeFloat(m_Height));
		list.AddRange(ByteSerializer.SerializeInt(m_NumSegments));
		list.AddRange(ByteSerializer.SerializeInt((int)m_SplineType));
		list.AddRange(ByteSerializer.SerializeBool(m_FlippedVertical));
		list.AddRange(ByteSerializer.SerializeBool(m_FlippedHorizontal));
		list.AddRange(ByteSerializer.SerializeBool(m_HideLegs));
		list.AddRange(ByteSerializer.SerializeBool(m_FlippedLegs));
		SerializeLinePoints(list);
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector2(bytes, ref offset);
		DeserializeControlPoints(bytes, ref offset);
		m_Height = Mathf.Abs(ByteSerializer.DeserializeFloat(bytes, ref offset));
		m_NumSegments = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_SplineType = (Dreamteck.Splines.Spline.Type)ByteSerializer.DeserializeInt(bytes, ref offset);
		m_FlippedVertical = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_FlippedHorizontal = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_HideLegs = version >= 23 && ByteSerializer.DeserializeBool(bytes, ref offset);
		if (version >= 25)
		{
			m_FlippedLegs = ByteSerializer.DeserializeBool(bytes, ref offset);
		}
		else if (version >= 22)
		{
			ByteSerializer.DeserializeBool(bytes, ref offset);
		}
		else
		{
			ByteSerializer.DeserializeInt(bytes, ref offset);
		}
		if (version >= 13)
		{
			DeserializeLinePoints(bytes, ref offset);
		}
	}

	public void MaybeUpdateLinePoints()
	{
		if ((bool)m_RampReference)
		{
			m_LinePoints = m_RampReference.GetLinePoints();
		}
	}

	private void SerializeControlPoints(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_ControlPoints.Count));
		foreach (Vector2 controlPoint in m_ControlPoints)
		{
			bytes.AddRange(ByteSerializer.SerializeVector2(controlPoint));
		}
	}

	private void SerializeLinePoints(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_LinePoints.Count));
		foreach (Vector2 linePoint in m_LinePoints)
		{
			bytes.AddRange(ByteSerializer.SerializeVector2(linePoint));
		}
	}

	private void DeserializeControlPoints(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_ControlPoints.Add(ByteSerializer.DeserializeVector2(bytes, ref offset));
		}
	}

	private void DeserializeLinePoints(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_LinePoints.Add(ByteSerializer.DeserializeVector2(bytes, ref offset));
		}
	}
}
