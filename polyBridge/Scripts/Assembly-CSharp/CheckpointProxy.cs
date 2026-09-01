using System.Collections.Generic;
using UnityEngine;

public class CheckpointProxy
{
	public Vector2 m_Pos;

	public string m_PrefabName;

	public string m_VehicleGuid;

	public string m_VehicleRestartPhaseGuid;

	public bool m_TriggerTimeline;

	public bool m_StopVehicle;

	public bool m_ReverseVehicleOnRestart;

	public bool m_InvisibleInSim;

	public string m_Guid;

	public string m_UndoGuid;

	public CheckpointProxy(Checkpoint checkpoint)
	{
		m_Pos = checkpoint.transform.position;
		m_PrefabName = checkpoint.name;
		m_VehicleGuid = checkpoint.m_VehicleGuid;
		m_VehicleRestartPhaseGuid = checkpoint.m_VehicleRestartPhaseGuid;
		m_TriggerTimeline = checkpoint.m_TriggerTimeline;
		m_StopVehicle = checkpoint.m_StopVehicle;
		m_ReverseVehicleOnRestart = checkpoint.m_ReverseVehicleOnRestart;
		m_InvisibleInSim = checkpoint.m_InvisibleInSim;
		m_Guid = checkpoint.m_Guid;
		m_UndoGuid = checkpoint.m_SandboxItem.m_UndoGuid;
	}

	public CheckpointProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeVector2(m_Pos));
		list.AddRange(ByteSerializer.SerializeString(m_PrefabName));
		list.AddRange(ByteSerializer.SerializeString(m_VehicleGuid));
		list.AddRange(ByteSerializer.SerializeString(m_VehicleRestartPhaseGuid));
		list.AddRange(ByteSerializer.SerializeBool(m_TriggerTimeline));
		list.AddRange(ByteSerializer.SerializeBool(m_StopVehicle));
		list.AddRange(ByteSerializer.SerializeBool(m_ReverseVehicleOnRestart));
		list.AddRange(ByteSerializer.SerializeBool(m_InvisibleInSim));
		list.AddRange(ByteSerializer.SerializeString(m_Guid));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_Pos = ByteSerializer.DeserializeVector2(bytes, ref offset);
		m_PrefabName = ByteSerializer.DeserializeString(bytes, ref offset);
		m_VehicleGuid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_VehicleRestartPhaseGuid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_TriggerTimeline = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_StopVehicle = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_ReverseVehicleOnRestart = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_InvisibleInSim = version >= 73 && ByteSerializer.DeserializeBool(bytes, ref offset);
		m_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
	}
}
