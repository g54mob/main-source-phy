using System.Collections.Generic;

public class EventTimelineProxy
{
	public string m_CheckpointGuid;

	public List<EventStageProxy> m_Stages = new List<EventStageProxy>();

	public EventTimelineProxy(EventTimeline timeline)
	{
		m_CheckpointGuid = (timeline.m_Checkpoint ? timeline.m_Checkpoint.m_Guid : string.Empty);
		foreach (EventStage stage in timeline.m_Stages)
		{
			m_Stages.Add(new EventStageProxy(stage));
		}
	}

	public EventTimelineProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeString(m_CheckpointGuid));
		list.AddRange(ByteSerializer.SerializeInt(m_Stages.Count));
		foreach (EventStageProxy stage in m_Stages)
		{
			list.AddRange(stage.SerializeBinary());
		}
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_CheckpointGuid = ByteSerializer.DeserializeString(bytes, ref offset);
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_Stages.Add(new EventStageProxy(version, bytes, ref offset));
		}
	}
}
