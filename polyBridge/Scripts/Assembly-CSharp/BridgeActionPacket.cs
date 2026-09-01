using System.Collections.Generic;
using UnityEngine;

public class BridgeActionPacket
{
	public BridgeAction m_Action;

	public BridgeJointProxy m_Joint;

	public BridgeEdgeProxy m_Edge;

	public BridgeSpringProxy m_Spring;

	public PistonProxy m_Piston;

	public BridgePillarProxy m_Pillar;

	public List<string> m_HydraulicsPhases;

	public BridgeSaveData m_BridgeSaveDataPre;

	public ClipboardSaveData m_ClipboardSaveData;

	public BridgeSaveData m_BridgeSaveDataPost;

	public string m_HydraulicsPhaseGuid;

	public SplitJointState m_PrevSplitJointState;

	public SplitJointState m_SplitJointState;

	public SplitJointPart m_PrevSplitJointPart;

	public SplitJointPart m_SplitJointPart;

	public BridgeJointSelectorSide m_BridgeJointSelectorSide;

	public Vector3 m_Translation;

	public BridgeActionPacket(BridgeAction action, BridgeJoint joint)
	{
		m_Action = action;
		m_Joint = new BridgeJointProxy(joint);
		if (HydraulicsController.m_ControllerPhases.Count <= 0 || (action != BridgeAction.SPLIT_JOINT && action != BridgeAction.UNSPLIT_JOINT))
		{
			return;
		}
		m_HydraulicsPhases = new List<string>();
		foreach (HydraulicsControllerPhase controllerPhase in HydraulicsController.m_ControllerPhases)
		{
			if (controllerPhase.AffectsSplitJoint(joint))
			{
				m_HydraulicsPhases.Add(controllerPhase.m_HydraulicsPhase.m_Guid);
			}
		}
	}

	public BridgeActionPacket(BridgeAction action, BridgeEdge edge)
	{
		m_Action = action;
		m_Edge = new BridgeEdgeProxy(edge);
		m_Piston = (edge.IsPiston() ? new PistonProxy(Pistons.GetPistonOnEdge(edge)) : null);
		m_Spring = ((edge.m_SpringCoilVisualization != null) ? new BridgeSpringProxy(edge.m_SpringCoilVisualization) : null);
		if (m_Piston == null || HydraulicsController.m_ControllerPhases.Count <= 0)
		{
			return;
		}
		m_HydraulicsPhases = new List<string>();
		foreach (HydraulicsControllerPhase controllerPhase in HydraulicsController.m_ControllerPhases)
		{
			if (controllerPhase.m_Pistons.Contains(Pistons.GetPistonOnEdge(edge)))
			{
				m_HydraulicsPhases.Add(controllerPhase.m_HydraulicsPhase.m_Guid);
			}
		}
	}

	public BridgeActionPacket(BridgeAction action, BridgeJoint joint, Vector3 translation)
	{
		m_Action = action;
		m_Joint = new BridgeJointProxy(joint);
		m_Translation = translation;
	}

	public BridgeActionPacket(BridgeAction action, Piston piston, Vector3 translation)
	{
		m_Action = action;
		m_Piston = new PistonProxy(piston);
		m_Translation = translation;
	}

	public BridgeActionPacket(BridgeAction action, BridgeSpring spring, Vector3 translation)
	{
		m_Action = action;
		m_Spring = new BridgeSpringProxy(spring);
		m_Translation = translation;
	}

	public BridgeActionPacket(BridgeAction action, BridgePillar pillar)
	{
		m_Action = action;
		m_Pillar = new BridgePillarProxy(pillar);
	}

	public BridgeActionPacket(BridgeAction action, BridgePillar pillar, Vector3 translation)
	{
		m_Action = action;
		m_Pillar = new BridgePillarProxy(pillar);
		m_Translation = translation;
	}

	public BridgeActionPacket(BridgeAction action, BridgeSaveData bridgeSaveData, ClipboardSaveData clipboardSaveData)
	{
		m_Action = action;
		if (action == BridgeAction.SERIALIZE_BRIDGE_PRE)
		{
			m_BridgeSaveDataPre = bridgeSaveData;
		}
		else
		{
			m_BridgeSaveDataPost = bridgeSaveData;
		}
		m_ClipboardSaveData = clipboardSaveData;
	}

	public BridgeActionPacket(BridgeAction action, BridgeJoint joint, SplitJointState splitJointState, string hydraulicsPhaseGuid)
	{
		m_Action = action;
		m_Joint = new BridgeJointProxy(joint);
		m_SplitJointState = splitJointState;
		m_HydraulicsPhaseGuid = hydraulicsPhaseGuid;
	}

	public BridgeActionPacket(BridgeAction action, Piston piston, string hydraulicsPhaseGuid)
	{
		m_Action = action;
		m_Piston = new PistonProxy(piston);
		m_HydraulicsPhaseGuid = hydraulicsPhaseGuid;
	}

	public BridgeActionPacket(BridgeAction action, string hydraulicsPhaseGuid)
	{
		m_Action = action;
		m_HydraulicsPhaseGuid = hydraulicsPhaseGuid;
	}

	public BridgeActionPacket(BridgeAction action, string hydraulicsPhaseGuid, BridgeJoint joint, SplitJointState prevSplitJointState, SplitJointState splitJointState)
	{
		m_Action = action;
		m_HydraulicsPhaseGuid = hydraulicsPhaseGuid;
		m_Joint = new BridgeJointProxy(joint);
		m_PrevSplitJointState = prevSplitJointState;
		m_SplitJointState = splitJointState;
	}

	public BridgeActionPacket(BridgeAction action, BridgeEdge edge, SplitJointPart prevSplitJointPart, SplitJointPart splitJointPart, BridgeJointSelectorSide side)
	{
		m_Action = action;
		m_Edge = new BridgeEdgeProxy(edge);
		m_PrevSplitJointPart = prevSplitJointPart;
		m_SplitJointPart = splitJointPart;
		m_BridgeJointSelectorSide = side;
	}
}
