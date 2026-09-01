using System.Collections.Generic;

public class HydraulicsControllerProxy
{
	public List<HydraulicsControllerPhaseProxy> m_Phases = new List<HydraulicsControllerPhaseProxy>();

	public HydraulicsControllerProxy()
	{
		m_Phases.Clear();
		foreach (HydraulicsControllerPhase controllerPhase in HydraulicsController.m_ControllerPhases)
		{
			m_Phases.Add(new HydraulicsControllerPhaseProxy(controllerPhase));
		}
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeInt(m_Phases.Count));
		foreach (HydraulicsControllerPhaseProxy phase in m_Phases)
		{
			list.AddRange(phase.SerializeBinary());
		}
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		m_Phases.Clear();
		for (int i = 0; i < num; i++)
		{
			m_Phases.Add(new HydraulicsControllerPhaseProxy(version, bytes, ref offset));
		}
	}
}
