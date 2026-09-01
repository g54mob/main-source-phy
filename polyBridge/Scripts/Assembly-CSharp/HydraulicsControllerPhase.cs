using System.Collections.Generic;

public class HydraulicsControllerPhase
{
	public HydraulicsPhase m_HydraulicsPhase;

	public List<Piston> m_Pistons;

	public List<BridgeSplitJoint> m_SplitJoints;

	public bool m_DisableNewAdditions;

	public HydraulicsControllerPhase(HydraulicsPhase hydraulicsPhase, List<Piston> pistons, List<BridgeJoint> joints, bool disableNewAdditions)
	{
		m_HydraulicsPhase = hydraulicsPhase;
		m_Pistons = new List<Piston>(pistons);
		m_SplitJoints = new List<BridgeSplitJoint>();
		m_DisableNewAdditions = disableNewAdditions;
		foreach (BridgeJoint joint in joints)
		{
			m_SplitJoints.Add(new BridgeSplitJoint(joint, (joint.m_SplitJointState != SplitJointState.NONE_SPLIT) ? joint.m_SplitJointState : SplitJointState.ALL_SPLIT));
		}
	}

	public bool AffectsSplitJoint(BridgeJoint joint)
	{
		foreach (BridgeSplitJoint splitJoint in m_SplitJoints)
		{
			if (splitJoint.m_BridgeJoint == joint)
			{
				return true;
			}
		}
		return false;
	}

	public SplitJointState GetStateForJoint(BridgeJoint joint)
	{
		foreach (BridgeSplitJoint splitJoint in m_SplitJoints)
		{
			if (splitJoint.m_BridgeJoint == joint)
			{
				return splitJoint.m_SplitJointState;
			}
		}
		return SplitJointState.NONE_SPLIT;
	}

	public void SetStateForJoint(BridgeJoint joint, SplitJointState state)
	{
		foreach (BridgeSplitJoint splitJoint in m_SplitJoints)
		{
			if (splitJoint.m_BridgeJoint == joint)
			{
				splitJoint.m_SplitJointState = state;
			}
		}
	}

	public BridgeSplitJoint GetBridgeSplitJoint(BridgeJoint joint)
	{
		foreach (BridgeSplitJoint splitJoint in m_SplitJoints)
		{
			if (splitJoint.m_BridgeJoint == joint)
			{
				return splitJoint;
			}
		}
		return null;
	}

	public void AddSplitJoint(BridgeJoint joint, SplitJointState state)
	{
		m_SplitJoints.Add(new BridgeSplitJoint(joint, state));
	}

	public void RemoveSplitJoint(BridgeJoint joint)
	{
		for (int num = m_SplitJoints.Count - 1; num >= 0; num--)
		{
			if (m_SplitJoints[num].m_BridgeJoint == joint)
			{
				m_SplitJoints.RemoveAt(num);
			}
		}
	}

	public void RemoveAllSplitJoints()
	{
		foreach (BridgeSplitJoint splitJoint in m_SplitJoints)
		{
			splitJoint.m_BridgeJoint.m_SplitJointState = SplitJointState.NONE_SPLIT;
		}
		m_SplitJoints.Clear();
	}

	public void FixupSplitJointState()
	{
		foreach (BridgeSplitJoint splitJoint in m_SplitJoints)
		{
			if (!splitJoint.m_BridgeJoint.HasConnectedEdgeUsingSplitJointPart(SplitJointPart.C) && splitJoint.m_SplitJointState != SplitJointState.ALL_SPLIT && splitJoint.m_SplitJointState != SplitJointState.NONE_SPLIT)
			{
				splitJoint.m_SplitJointState = SplitJointState.ALL_SPLIT;
			}
		}
	}

	public bool IsEmpty()
	{
		if (m_SplitJoints.Count == 0)
		{
			return m_Pistons.Count == 0;
		}
		return false;
	}

	public bool HasDataToClear()
	{
		if (m_Pistons.Count <= 0 && m_SplitJoints.Count <= 0)
		{
			return m_DisableNewAdditions;
		}
		return true;
	}
}
