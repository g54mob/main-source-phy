using System.Collections.Generic;
using UnityEngine;

public class VehicleStopTriggerProxy
{
	public Vector2 m_Pos;

	public Quaternion m_Rot;

	public float m_Height;

	public float m_RotationDegrees;

	public bool m_Flipped;

	public bool m_InvisibleInSim;

	public string m_PrefabName;

	public string m_StopVehicleGuid;

	public string m_UndoGuid;

	public VehicleStopTriggerProxy(VehicleStopTrigger trigger)
	{
		m_Pos = trigger.transform.position;
		m_Rot = trigger.transform.rotation;
		m_Height = trigger.m_Height;
		m_RotationDegrees = trigger.m_RotationDegrees;
		m_Flipped = trigger.m_Flipped;
		m_InvisibleInSim = trigger.m_InvisibleInSim;
		m_PrefabName = trigger.name;
		m_StopVehicleGuid = trigger.m_VehicleGuid;
		m_UndoGuid = trigger.m_SandboxItem.m_UndoGuid;
	}

	public VehicleStopTriggerProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector2(m_Pos));
		list.AddRange(ByteSerializer.SerializeQuaternion(m_Rot));
		list.AddRange(ByteSerializer.SerializeFloat(m_Height));
		list.AddRange(ByteSerializer.SerializeFloat(m_RotationDegrees));
		list.AddRange(ByteSerializer.SerializeBool(m_Flipped));
		list.AddRange(ByteSerializer.SerializeBool(m_InvisibleInSim));
		list.AddRange(ByteSerializer.SerializeString(m_PrefabName));
		list.AddRange(ByteSerializer.SerializeString(m_StopVehicleGuid));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector2(bytes, ref offset);
		m_Rot = ByteSerializer.DeserializeQuaternion(bytes, ref offset);
		m_Height = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_RotationDegrees = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_Flipped = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_InvisibleInSim = version >= 73 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_PrefabName = ByteSerializer.DeserializeString(bytes, ref offset);
		m_StopVehicleGuid = ByteSerializer.DeserializeString(bytes, ref offset);
	}
}
