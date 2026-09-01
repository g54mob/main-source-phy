using System.Collections.Generic;
using Poly.Physics;
using UnityEngine;

public class VehicleProxy
{
	public string m_DisplayName;

	public Vector2 m_Pos;

	public Quaternion m_Rot;

	public string m_PrefabName;

	public float m_TargetSpeed;

	public float m_Mass;

	public float m_BrakingForceMultiplier;

	public Poly.Physics.Vehicle.StrengthMethod m_StrengthMethod;

	public float m_Acceleration;

	public float m_MaxSlope;

	public float m_DesiredAcceleration;

	public float m_ShocksMultiplier;

	public float m_RotationDegrees;

	public float m_TimeDelaySeconds;

	public bool m_IdleOnDownhill;

	public bool m_Flipped;

	public bool m_OrderedCheckpoints;

	public string m_Guid;

	public List<string> m_CheckpointGuids = new List<string>();

	public float m_UniformScale;

	public string m_SkinID;

	public string m_UndoGuid;

	public string m_ModId;

	public VehicleProxy(Vehicle vehicle)
	{
		m_Pos = vehicle.transform.position;
		m_Rot = vehicle.transform.rotation;
		m_UniformScale = ((vehicle.m_ScalingTransform != null) ? (vehicle.m_ScalingTransform.localScale.x / vehicle.m_OriginalScale.x) : 1f);
		m_TargetSpeed = vehicle.m_TargetSpeed;
		m_Mass = vehicle.m_Mass;
		m_BrakingForceMultiplier = vehicle.m_BrakingForceMultiplier;
		m_Acceleration = vehicle.m_Acceleration;
		m_IdleOnDownhill = vehicle.m_IdleOnDownhill;
		m_DesiredAcceleration = vehicle.m_DesiredAcceleration;
		m_ShocksMultiplier = vehicle.m_ShocksMultiplier;
		m_RotationDegrees = vehicle.m_RotationDegrees;
		m_TimeDelaySeconds = vehicle.m_TimeDelaySeconds;
		m_Flipped = vehicle.m_Flipped;
		m_OrderedCheckpoints = vehicle.m_OrderedCheckpoints;
		m_PrefabName = vehicle.name;
		m_Guid = vehicle.m_Guid;
		m_SkinID = vehicle.m_SkinID;
		m_UndoGuid = vehicle.m_SandboxItem.m_UndoGuid;
		m_ModId = vehicle.m_ModId;
		foreach (Checkpoint checkpoint in vehicle.m_Checkpoints)
		{
			m_CheckpointGuids.Add(checkpoint.m_Guid);
		}
	}

	public VehicleProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeString(m_DisplayName));
		list.AddRange(ByteSerializer.SerializeVector2(m_Pos));
		list.AddRange(ByteSerializer.SerializeQuaternion(m_Rot));
		list.AddRange(ByteSerializer.SerializeString(m_PrefabName));
		list.AddRange(ByteSerializer.SerializeFloat(m_TargetSpeed));
		list.AddRange(ByteSerializer.SerializeFloat(m_Mass));
		list.AddRange(ByteSerializer.SerializeFloat(m_BrakingForceMultiplier));
		list.AddRange(ByteSerializer.SerializeInt((int)m_StrengthMethod));
		list.AddRange(ByteSerializer.SerializeFloat(m_Acceleration));
		list.AddRange(ByteSerializer.SerializeFloat(m_MaxSlope));
		list.AddRange(ByteSerializer.SerializeFloat(m_DesiredAcceleration));
		list.AddRange(ByteSerializer.SerializeFloat(m_ShocksMultiplier));
		list.AddRange(ByteSerializer.SerializeFloat(m_RotationDegrees));
		list.AddRange(ByteSerializer.SerializeFloat(m_TimeDelaySeconds));
		list.AddRange(ByteSerializer.SerializeBool(m_IdleOnDownhill));
		list.AddRange(ByteSerializer.SerializeBool(m_Flipped));
		list.AddRange(ByteSerializer.SerializeBool(m_OrderedCheckpoints));
		list.AddRange(ByteSerializer.SerializeString(m_Guid));
		list.AddRange(ByteSerializer.SerializeFloat(m_UniformScale));
		list.AddRange(ByteSerializer.SerializeString(m_SkinID));
		list.AddRange(ByteSerializer.SerializeString(m_ModId));
		SerializeCheckpointGuids(list);
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_DisplayName = ByteSerializer.DeserializeString(bytes, ref offset);
		m_Pos = ByteSerializer.DeserializeVector2(bytes, ref offset);
		m_Rot = ByteSerializer.DeserializeQuaternion(bytes, ref offset);
		m_PrefabName = ByteSerializer.DeserializeString(bytes, ref offset);
		m_TargetSpeed = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_Mass = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_BrakingForceMultiplier = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_StrengthMethod = (Poly.Physics.Vehicle.StrengthMethod)ByteSerializer.DeserializeInt(bytes, ref offset);
		m_Acceleration = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_MaxSlope = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_DesiredAcceleration = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_ShocksMultiplier = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_RotationDegrees = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_TimeDelaySeconds = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_IdleOnDownhill = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_Flipped = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_OrderedCheckpoints = ByteSerializer.DeserializeBool(bytes, ref offset);
		m_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_UniformScale = ((version >= 50) ? ByteSerializer.DeserializeFloat(bytes, ref offset) : 1f);
		m_SkinID = ((version >= 51) ? ByteSerializer.DeserializeString(bytes, ref offset) : string.Empty);
		m_ModId = ((version >= 54) ? ByteSerializer.DeserializeString(bytes, ref offset) : string.Empty);
		DeserializeCheckpointGuids(bytes, ref offset);
	}

	private void SerializeCheckpointGuids(List<byte> bytes)
	{
		Vehicle vehicle = Vehicles.FindByGuid(m_Guid);
		if (vehicle == null)
		{
			Debug.LogWarningFormat("Could not find vehicle {0} in SerializeCheckpointGuids()", m_Guid);
			return;
		}
		bytes.AddRange(ByteSerializer.SerializeInt(vehicle.m_Checkpoints.Count));
		foreach (Checkpoint checkpoint in vehicle.m_Checkpoints)
		{
			bytes.AddRange(ByteSerializer.SerializeString(checkpoint.m_Guid));
		}
	}

	private void DeserializeCheckpointGuids(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_CheckpointGuids.Add(ByteSerializer.DeserializeString(bytes, ref offset));
		}
	}
}
