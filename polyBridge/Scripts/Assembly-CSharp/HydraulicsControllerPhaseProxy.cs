using System.Collections.Generic;

public class HydraulicsControllerPhaseProxy
{
	public string m_HydraulicsPhaseGuid;

	public List<string> m_PistonGuids = new List<string>();

	public List<BridgeSplitJointProxy> m_BridgeSplitJoints = new List<BridgeSplitJointProxy>();

	public bool m_DisableNewAdditions;

	public HydraulicsControllerPhaseProxy(HydraulicsControllerPhase controllerPhase)
	{
		m_HydraulicsPhaseGuid = controllerPhase.m_HydraulicsPhase.m_Guid;
		m_DisableNewAdditions = controllerPhase.m_DisableNewAdditions;
		foreach (Piston piston in controllerPhase.m_Pistons)
		{
			if (piston.gameObject.activeInHierarchy)
			{
				m_PistonGuids.Add(piston.m_Guid);
			}
		}
		foreach (BridgeSplitJoint splitJoint in controllerPhase.m_SplitJoints)
		{
			if (splitJoint.m_BridgeJoint.gameObject.activeInHierarchy && splitJoint.m_BridgeJoint.m_IsSplit)
			{
				if (splitJoint.m_SplitJointState == SplitJointState.NONE_SPLIT)
				{
					splitJoint.m_SplitJointState = SplitJointState.ALL_SPLIT;
				}
				m_BridgeSplitJoints.Add(new BridgeSplitJointProxy(splitJoint));
			}
		}
	}

	public HydraulicsControllerPhaseProxy(int version, byte[] bytes, ref int offset)
	{
		DeserializeBinary(version, bytes, ref offset);
	}

	public byte[] SerializeBinary()
	{
		List<byte> list = new List<byte>();
		list.AddRange(ByteSerializer.SerializeString(m_HydraulicsPhaseGuid));
		SerializePistonGuids(list);
		list.AddRange(ByteSerializer.SerializeInt(m_BridgeSplitJoints.Count));
		foreach (BridgeSplitJointProxy bridgeSplitJoint in m_BridgeSplitJoints)
		{
			list.AddRange(bridgeSplitJoint.SerializeBinary());
		}
		list.AddRange(ByteSerializer.SerializeBool(m_DisableNewAdditions));
		return list.ToArray();
	}

	public void DeserializeBinary(int version, byte[] bytes, ref int offset)
	{
		m_HydraulicsPhaseGuid = ByteSerializer.DeserializeString(bytes, ref offset);
		DeserializePistonGuids(bytes, ref offset);
		if (version > 2)
		{
			int num = ByteSerializer.DeserializeInt(bytes, ref offset);
			for (int i = 0; i < num; i++)
			{
				m_BridgeSplitJoints.Add(new BridgeSplitJointProxy(bytes, ref offset));
			}
		}
		else
		{
			int num2 = ByteSerializer.DeserializeInt(bytes, ref offset);
			for (int j = 0; j < num2; j++)
			{
				ByteSerializer.DeserializeString(bytes, ref offset);
			}
		}
		if (version > 13)
		{
			m_DisableNewAdditions = ByteSerializer.DeserializeBool(bytes, ref offset);
		}
	}

	private void SerializePistonGuids(List<byte> bytes)
	{
		bytes.AddRange(ByteSerializer.SerializeInt(m_PistonGuids.Count));
		foreach (string pistonGuid in m_PistonGuids)
		{
			bytes.AddRange(ByteSerializer.SerializeString(pistonGuid));
		}
	}

	private void DeserializePistonGuids(byte[] bytes, ref int offset)
	{
		int num = ByteSerializer.DeserializeInt(bytes, ref offset);
		for (int i = 0; i < num; i++)
		{
			m_PistonGuids.Add(ByteSerializer.DeserializeString(bytes, ref offset));
		}
	}
}
