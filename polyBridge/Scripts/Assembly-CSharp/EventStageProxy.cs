using System.Collections.Generic;

public class EventStageProxy
{
	public List<EventUnitProxy> m_Units = new List<EventUnitProxy>();

	public EventStageProxy(EventStage stage)
	{
		foreach (EventUnit unit in stage.m_Units)
		{
			m_Units.Add(new EventUnitProxy(unit));
		}
	}

	public EventStageProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeInt(m_Units.Count));
		foreach (EventUnitProxy unit in m_Units)
		{
			list.AddRange(unit.SerializeBinary());
		}
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Units.Add(new EventUnitProxy(version, bytes, ref offset));
		}
	}
}
