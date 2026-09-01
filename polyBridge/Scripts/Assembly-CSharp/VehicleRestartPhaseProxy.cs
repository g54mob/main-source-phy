using System.Collections.Generic;

public class VehicleRestartPhaseProxy
{
	public float m_TimeDelaySeconds;

	public string m_Guid;

	public string m_VehicleGuid;

	public string m_UndoGuid;

	public VehicleRestartPhaseProxy(VehicleRestartPhase phase)
	{
		m_TimeDelaySeconds = phase.m_TimeDelaySeconds;
		m_Guid = phase.m_Guid;
		m_VehicleGuid = phase.m_VehicleGuid;
		m_UndoGuid = phase.m_SandboxItem.m_UndoGuid;
	}

	public VehicleRestartPhaseProxy(byte[] bytes, ref int offset)
	{
		DeserializeBinary(bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeFloat(m_TimeDelaySeconds));
		list.AddRange(ByteSerializer.SerializeString(m_Guid));
		list.AddRange(ByteSerializer.SerializeString(m_VehicleGuid));
		return list.ToArray();
	}

	public void DeserializeBinary(byte[] bytes, ref int offset)
	{
		m_TimeDelaySeconds = ByteSerializer.DeserializeFloat(bytes, ref offset);
		m_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
		m_VehicleGuid = ByteSerializer.DeserializeString(bytes, ref offset);
	}
}
