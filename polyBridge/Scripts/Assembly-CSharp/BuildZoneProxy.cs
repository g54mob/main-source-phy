using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildZoneProxy
{
	public Vector2 m_Pos;

	public Vector2 m_Size;

	public bool m_LockPosition;

	public float m_RotationDegrees;

	public BuildZoneType m_BuildZoneType;

	public Vector3[] m_Verts;

	public string m_UndoGuid;

	public BuildZoneProxy(BuildZone buildZone)
	{
		m_Pos = buildZone.GetPosition();
		m_Size = buildZone.GetSize();
		m_LockPosition = buildZone.m_LockPosition;
		m_RotationDegrees = buildZone.m_RotationDegrees;
		m_BuildZoneType = buildZone.m_Type;
		m_Verts = new Vector3[buildZone.m_VertsLocalSpace.Length];
		Array.Copy(buildZone.m_VertsLocalSpace, m_Verts, buildZone.m_VertsLocalSpace.Length);
		m_UndoGuid = buildZone.m_SandboxItem.m_UndoGuid;
	}

	public BuildZoneProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector2(m_Pos));
		list.AddRange(ByteSerializer.SerializeVector2(m_Size));
		list.AddRange(ByteSerializer.SerializeBool(m_LockPosition));
		list.AddRange(ByteSerializer.SerializeFloat(m_RotationDegrees));
		list.AddRange(ByteSerializer.SerializeInt((int)m_BuildZoneType));
		if (m_BuildZoneType == BuildZoneType.TRIANGLE)
		{
			list.AddRange(ByteSerializer.SerializeInt(m_Verts.Length));
			Vector3[] verts = m_Verts;
			foreach (Vector3 value in verts)
			{
				list.AddRange(ByteSerializer.SerializeVector3(value));
			}
		}
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector2(bytes, ref offset);
		m_Size = ByteSerializer.DeserializeVector2(bytes, ref offset);
		m_LockPosition = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_RotationDegrees = ((version >= 43) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : 0f);
		m_BuildZoneType = ((version >= 62) ? ((BuildZoneType)ByteSerializer.DeserializeInt(bytes, ref offset)) : BuildZoneType.RECTANGLE);
		if (m_BuildZoneType != BuildZoneType.TRIANGLE)
		{
			return;
		}
		int num = ((version >= 62) ? ByteSerializer.DeserializeInt(bytes, ref offset) : 0);
		if (num > 0)
		{
			m_Verts = new Vector3[num];
			for (int i = 0; i < num; i++)
			{
				m_Verts[i] = ((version >= 62) ? ByteSerializer.DeserializeVector3(bytes, ref offset) : Vector3.zero);
			}
		}
	}
}
