using System.Collections.Generic;
using UnityEngine;

public class ZedAxisVehicleProxy
{
	public Vector2 m_Pos;

	public string m_PrefabName;

	public string m_Guid;

	public string m_UndoGuid;

	public float m_TimeDelaySeconds;

	public float m_Speed;

	public Quaternion m_Rot;

	public float m_RotationDegrees;

	public float m_UniformScale;

	public string m_ModId;

	public bool m_Reverse;

	public bool m_SnapToWaterLine;

	public ZedAxisVehicleProxy(ZedAxisVehicle vehicle)
	{
		m_Pos = vehicle.transform.position;
		m_Rot = vehicle.transform.rotation;
		m_UniformScale = ((vehicle.m_ScalingTransform != null) ? (vehicle.m_ScalingTransform.localScale.x / vehicle.m_OriginalScale.x) : 1f);
		m_PrefabName = vehicle.name;
		m_Guid = vehicle.m_Guid;
		m_UndoGuid = vehicle.m_SandboxItem.m_UndoGuid;
		m_TimeDelaySeconds = vehicle.m_TimeDelaySeconds;
		m_Speed = vehicle.m_Speed;
		m_RotationDegrees = vehicle.m_RotationDegrees;
		m_ModId = vehicle.m_ModId;
		m_Reverse = vehicle.m_Reverse;
		m_SnapToWaterLine = vehicle.m_SnapToWaterLine;
	}

	public ZedAxisVehicleProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector2(m_Pos));
		list.AddRange(ByteSerializer.SerializeString(m_PrefabName));
		list.AddRange(ByteSerializer.SerializeString(m_Guid));
		list.AddRange(ByteSerializer.SerializeFloat(m_TimeDelaySeconds));
		list.AddRange(ByteSerializer.SerializeFloat(m_Speed));
		list.AddRange(ByteSerializer.SerializeQuaternion(m_Rot));
		list.AddRange(ByteSerializer.SerializeFloat(m_RotationDegrees));
		list.AddRange(ByteSerializer.SerializeFloat(m_UniformScale));
		list.AddRange(ByteSerializer.SerializeString(m_ModId));
		list.AddRange(ByteSerializer.SerializeBool(m_SnapToWaterLine));
		list.AddRange(ByteSerializer.SerializeBool(m_Reverse));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector2(bytes, ref offset);
		m_PrefabName = ByteSerializer.DeserializeString(bytes, ref offset);
		m_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_TimeDelaySeconds = ByteSerializer.DeserializeFloat(bytes, ref offset);
		if (version >= 8)
		{
			m_Speed = ByteSerializer.DeserializeFloat(bytes, ref offset);
		}
		if (version >= 28)
		{
			m_Rot = ByteSerializer.DeserializeQuaternion(bytes, ref offset);
			m_RotationDegrees = ByteSerializer.DeserializeFloat(bytes, ref offset);
		}
		m_UniformScale = ((version >= 49) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : 1f);
		m_ModId = ((version >= 54) ? ByteSerializer.DeserializeString(bytes, ref offset) : string.Empty);
		m_SnapToWaterLine = version >= 56 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_Reverse = version >= 57 && ByteSerializer.DeserializeBool(bytes, ref offset);
	}
}
