using System.Collections.Generic;

public class EventUnitProxy
{
	public string m_Guid;

	public EventUnitProxy(EventUnit unit)
	{
		switch (unit.m_Type)
		{
		case EventUnitType.ZED_AXIS_VEHICLE:
			m_Guid = (unit.GetZedAxisVehicle() ? unit.GetZedAxisVehicle().m_Guid : string.Empty);
			break;
		case EventUnitType.HYDRAULICS_PHASE:
			m_Guid = (unit.GetHydraulicsPhase() ? unit.GetHydraulicsPhase().m_Guid : string.Empty);
			break;
		case EventUnitType.VEHICLE:
			m_Guid = (unit.GetVehicle() ? unit.GetVehicle().m_Guid : string.Empty);
			break;
		case EventUnitType.VEHICLE_RESTART_PHASE:
			m_Guid = (unit.GetVehicleRestartPhase() ? unit.GetVehicleRestartPhase().m_Guid : string.Empty);
			break;
		default:
			m_Guid = string.Empty;
			break;
		}
	}

	public EventUnitProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeString(m_Guid));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		if (version >= 7)
		{
			m_Guid = ByteSerializer.DeserializeString(bytes, ref offset);
			return;
		}
		string text = ByteSerializer.DeserializeString(bytes, ref offset);
		if (!string.IsNullOrEmpty(text))
		{
			m_Guid = text;
		}
		string text2 = ByteSerializer.DeserializeString(bytes, ref offset);
		if (!string.IsNullOrEmpty(text2))
		{
			m_Guid = text2;
		}
		string text3 = ByteSerializer.DeserializeString(bytes, ref offset);
		if (!string.IsNullOrEmpty(text3))
		{
			m_Guid = text3;
		}
	}
}
