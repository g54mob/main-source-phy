using System.Collections.Generic;
using UnityEngine;

internal class BridgeActions
{
	private static Queue<BridgeActionPacket> m_RecordingQueue;

	public static void StartRecording()
	{
		if (m_RecordingQueue != null)
		{
			CancelRecording();
		}
		m_RecordingQueue = new Queue<BridgeActionPacket>();
	}

	public static bool IsRecording()
	{
		if (m_RecordingQueue == null)
		{
			return false;
		}
		return true;
	}

	public static void FlushRecording()
	{
		if (m_RecordingQueue == null || m_RecordingQueue.Count == 0)
		{
			CancelRecording();
			return;
		}
		if (GameManager.GetGameMode() == GameMode.SANDBOX)
		{
			Sandbox.m_UnsavedChanges = true;
		}
		BridgeUndo.m_Stack.Push(m_RecordingQueue);
		BridgeRedo.m_Stack.Clear();
		CancelRecording();
	}

	public static void CancelRecording()
	{
		m_RecordingQueue = null;
	}

	public static void Create(BridgeJoint joint)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.CREATE_JOINT, joint);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Create(BridgeEdge edge)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.CREATE_EDGE, edge);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Delete(BridgeJoint joint)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.DELETE_JOINT, joint);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Delete(BridgeEdge edge)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.DELETE_EDGE, edge);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Create(List<BridgeJoint> joints)
	{
		if (m_RecordingQueue == null)
		{
			return;
		}
		foreach (BridgeJoint joint in joints)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.CREATE_JOINT, joint);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Create(List<BridgeEdge> edges)
	{
		if (m_RecordingQueue == null)
		{
			return;
		}
		foreach (BridgeEdge edge in edges)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.CREATE_EDGE, edge);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Create(List<BridgePillar> bridgePillars)
	{
		if (m_RecordingQueue == null)
		{
			return;
		}
		foreach (BridgePillar bridgePillar in bridgePillars)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.CREATE_PILLAR, bridgePillar);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Delete(List<BridgeJoint> joints)
	{
		if (m_RecordingQueue == null)
		{
			return;
		}
		foreach (BridgeJoint joint in joints)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.DELETE_JOINT, joint);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Delete(HashSet<BridgeEdge> edges)
	{
		if (m_RecordingQueue == null)
		{
			return;
		}
		foreach (BridgeEdge edge in edges)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.DELETE_EDGE, edge);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Delete(HashSet<BridgePillar> bridgePillars)
	{
		if (m_RecordingQueue == null)
		{
			return;
		}
		foreach (BridgePillar bridgePillar in bridgePillars)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.DELETE_PILLAR, bridgePillar);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Translate(BridgeJoint joint, Vector3 translation)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.TRANSLATE_JOINT, joint, translation);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void MakeAnchor(BridgeJoint joint)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.MAKE_ANCHOR, joint);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void UnMakeAnchor(BridgeJoint joint)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.UNMAKE_ANCHOR, joint);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void UnMakeAnchors(HashSet<BridgeJoint> joints)
	{
		if (m_RecordingQueue == null)
		{
			return;
		}
		foreach (BridgeJoint joint in joints)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.UNMAKE_ANCHOR, joint);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void TranslatePistonSlider(Piston piston, float translation)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.PISTON_SLIDER_TRANSLATE, piston, new Vector3(translation, 0f, 0f));
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void TranslateSpringSlider(BridgeSpring spring, float translation)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.SPRING_SLIDER_TRANSLATE, spring, new Vector3(translation, 0f, 0f));
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void SplitJoint(BridgeJoint joint)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.SPLIT_JOINT, joint);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void UnSplitJoint(BridgeJoint joint)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.UNSPLIT_JOINT, joint);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Create(BridgePillar bridgePillar)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.CREATE_PILLAR, bridgePillar);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Delete(BridgePillar pillar)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.DELETE_PILLAR, pillar);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void Translate(BridgePillar pillar, Vector3 translation)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.TRANSLATE_PILLAR, pillar, translation);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void SerializeBridgePre(BridgeSaveData bridgeSaveData, ClipboardSaveData clipboardSaveData)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.SERIALIZE_BRIDGE_PRE, bridgeSaveData, clipboardSaveData);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void SerializeBridgePost(BridgeSaveData bridgeSaveData, ClipboardSaveData clipboardSaveData)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.SERIALIZE_BRIDGE_POST, bridgeSaveData, clipboardSaveData);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void HydraulicsControllerAddSplitJoint(HydraulicsPhase hydraulicsPhase, BridgeJoint joint)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.HYDRAULICS_CONTROLLER_ADD_SPLIT_JOINT, joint, joint.m_SplitJointState, hydraulicsPhase.m_Guid);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void HydraulicsControllerRemoveSplitJoint(HydraulicsPhase hydraulicsPhase, BridgeJoint joint, SplitJointState prevSplitJointState)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.HYDRAULICS_CONTROLLER_REMOVE_SPLIT_JOINT, joint, prevSplitJointState, hydraulicsPhase.m_Guid);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void HydraulicsControllerAddPiston(HydraulicsPhase hydraulicsPhase, Piston piston)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.HYDRAULICS_CONTROLLER_ADD_PISTON, piston, hydraulicsPhase.m_Guid);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void HydraulicsControllerRemovePiston(HydraulicsPhase hydraulicsPhase, Piston piston)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.HYDRAULICS_CONTROLLER_REMOVE_PISTON, piston, hydraulicsPhase.m_Guid);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void HydraulicsControllerDisableNewAdditions(HydraulicsPhase hydraulicsPhase)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.HYDRAULICS_CONTROLLER_DISABLE_NEW_ADDITIONS, hydraulicsPhase.m_Guid);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void HydraulicsControllerEnableNewAdditions(HydraulicsPhase hydraulicsPhase)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.HYDRAULICS_CONTROLLER_ENABLE_NEW_ADDITIONS, hydraulicsPhase.m_Guid);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void HydraulicsControllerChangeSplitState(HydraulicsPhase hydraulicsPhase, BridgeJoint joint, SplitJointState prevSplitJointState)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.HYDRAULICS_CONTROLLER_CHANGE_SPLIT_JOINT_STATE, hydraulicsPhase.m_Guid, joint, prevSplitJointState, joint.m_SplitJointState);
			m_RecordingQueue.Enqueue(item);
		}
	}

	public static void CycleSplitJointSelector(BridgeEdge edge, SplitJointPart prevSplitJointPart, SplitJointPart splitJointPart, BridgeJointSelectorSide side)
	{
		if (m_RecordingQueue != null)
		{
			BridgeActionPacket item = new BridgeActionPacket(BridgeAction.SPLIT_JOINT_SELECTOR_CYCLE, edge, prevSplitJointPart, splitJointPart, side);
			m_RecordingQueue.Enqueue(item);
		}
	}
}
