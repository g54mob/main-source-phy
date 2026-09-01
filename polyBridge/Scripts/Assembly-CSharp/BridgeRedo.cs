using System.Collections.Generic;
using UnityEngine;

public class BridgeRedo
{
	public static Stack<Queue<BridgeActionPacket>> m_Stack = new Stack<Queue<BridgeActionPacket>>();

	public static void Reset()
	{
		m_Stack.Clear();
	}

	public static void ClearClipboardSaveDataFromStack()
	{
		Queue<BridgeActionPacket>[] array = m_Stack.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			foreach (BridgeActionPacket item in array[i])
			{
				if (item != null)
				{
					item.m_ClipboardSaveData = null;
				}
			}
		}
	}

	public static bool CanRedo()
	{
		if (GameToolMode.GetMode() == GameToolModeType.ERASE && GameInput.IsDown((GameInput.GetActiveGameDevice() != GameDevice.Gamepad) ? BindingType.DRAW_BUILD : BindingType.ERASE) && !GameUI.IsPointerOverGameObject())
		{
			return false;
		}
		return m_Stack.Count > 0;
	}

	public static void Redo()
	{
		if (!CanRedo())
		{
			return;
		}
		Queue<BridgeActionPacket> queue = m_Stack.Pop();
		foreach (BridgeActionPacket item in queue)
		{
			switch (item.m_Action)
			{
			case BridgeAction.CREATE_JOINT:
				RedoCreateJoint(item);
				break;
			case BridgeAction.DELETE_JOINT:
				RedoDeleteJoint(item);
				break;
			case BridgeAction.TRANSLATE_JOINT:
				RedoTranslateJoint(item);
				break;
			case BridgeAction.CREATE_EDGE:
				RedoCreateEdge(item);
				break;
			case BridgeAction.DELETE_EDGE:
				RedoDeleteEdge(item);
				break;
			case BridgeAction.MAKE_ANCHOR:
				RedoMakeAnchor(item);
				break;
			case BridgeAction.UNMAKE_ANCHOR:
				RedoUnMakeAnchor(item);
				break;
			case BridgeAction.PISTON_SLIDER_TRANSLATE:
				RedoTranslatePistonSlider(item);
				break;
			case BridgeAction.SPRING_SLIDER_TRANSLATE:
				RedoTranslateSpringSlider(item);
				break;
			case BridgeAction.SPLIT_JOINT:
				RedoSplitJoint(item);
				break;
			case BridgeAction.UNSPLIT_JOINT:
				RedoUnSplitJoint(item);
				break;
			case BridgeAction.CREATE_PILLAR:
				RedoCreatePillar(item);
				break;
			case BridgeAction.DELETE_PILLAR:
				RedoDeletePillar(item);
				break;
			case BridgeAction.TRANSLATE_PILLAR:
				RedoTranslatePillar(item);
				break;
			case BridgeAction.SERIALIZE_BRIDGE_POST:
			{
				Quaternion clipboardRotation = ClipboardManager.GetClipboardRotation();
				Bridge.ClearAndLoadforUndo(item.m_BridgeSaveDataPost);
				if (item.m_ClipboardSaveData != null)
				{
					item.m_ClipboardSaveData.RestoreToClipboard(clipboardRotation);
				}
				break;
			}
			case BridgeAction.HYDRAULICS_CONTROLLER_ADD_SPLIT_JOINT:
				RedoHydraulicsControllerAddSplitJoint(item);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_REMOVE_SPLIT_JOINT:
				RedoHydraulicsControllerRemoveSplitJoint(item);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_ADD_PISTON:
				RedoHydraulicsControllerAddPiston(item);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_REMOVE_PISTON:
				RedoHydraulicsControllerRemovePiston(item);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_ENABLE_NEW_ADDITIONS:
				RedoHydraulicsControllerEnableNewAdditions(item);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_DISABLE_NEW_ADDITIONS:
				RedoHydraulicsControllerDisableNewAdditions(item);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_CHANGE_SPLIT_JOINT_STATE:
				RedoHydraulicsControllerChangeSplitJointState(item);
				break;
			case BridgeAction.SPLIT_JOINT_SELECTOR_CYCLE:
				RedoCycleSplitJointSelector(item);
				break;
			default:
				Debug.LogErrorFormat("Unsupported action {0}", item.m_Action.ToString());
				break;
			case BridgeAction.SERIALIZE_BRIDGE_PRE:
				break;
			}
		}
		BridgeUndo.m_Stack.Push(queue);
	}

	private static void RedoCreateJoint(BridgeActionPacket packet)
	{
		BridgeUndo.UndoDeleteJoint(packet);
	}

	private static void RedoDeleteJoint(BridgeActionPacket packet)
	{
		BridgeUndo.UndoCreateJoint(packet);
	}

	private static void RedoTranslateJoint(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if (!bridgeJoint)
		{
			return;
		}
		bridgeJoint.transform.Translate(packet.m_Translation);
		bridgeJoint.m_BuildPos = bridgeJoint.transform.position;
		bridgeJoint.TryRecreateSpringVisualizationForAttachedEdges();
		if (bridgeJoint.m_SandboxItem != null)
		{
			bridgeJoint.m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		GameStateBuild.ClearFirstBreakAttachedToJoint(bridgeJoint.m_Guid);
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(bridgeJoint))
		{
			item.UpdateTransform();
			item.UpdateJointSelectors();
		}
	}

	private static void RedoCreateEdge(BridgeActionPacket packet)
	{
		BridgeUndo.UndoDeleteEdge(packet);
	}

	private static void RedoDeleteEdge(BridgeActionPacket packet)
	{
		BridgeUndo.UndoCreateEdge(packet);
	}

	private static void RedoMakeAnchor(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if ((bool)bridgeJoint)
		{
			bridgeJoint.MakeAnchor();
		}
	}

	private static void RedoUnMakeAnchor(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if ((bool)bridgeJoint)
		{
			bridgeJoint.RevertAnchor();
		}
	}

	private static void RedoTranslatePistonSlider(BridgeActionPacket packet)
	{
		Piston piston = Pistons.FindByGuid(packet.m_Piston.m_Guid);
		if ((bool)piston)
		{
			piston.m_Slider.SetNormalizedValue(piston.m_Slider.GetNormalizedValue() + packet.m_Translation.x);
			piston.m_Slider.SetVisibilityExpireTime();
		}
	}

	private static void RedoTranslateSpringSlider(BridgeActionPacket packet)
	{
		BridgeSpring bridgeSpring = BridgeSprings.FindByGuid(packet.m_Spring.m_Guid);
		if ((bool)bridgeSpring)
		{
			bridgeSpring.m_Slider.SetNormalizedValue(bridgeSpring.m_Slider.GetNormalizedValue() + packet.m_Translation.x);
			bridgeSpring.RefreshVisualization();
			bridgeSpring.m_Slider.SetVisibilityExpireTime();
		}
	}

	private static void RedoSplitJoint(BridgeActionPacket packet)
	{
		BridgeUndo.UndoUnSplitJoint(packet);
	}

	private static void RedoUnSplitJoint(BridgeActionPacket packet)
	{
		BridgeUndo.UndoSplitJoint(packet);
	}

	private static void RedoCreatePillar(BridgeActionPacket packet)
	{
		BridgeUndo.UndoDeletePillar(packet);
	}

	private static void RedoDeletePillar(BridgeActionPacket packet)
	{
		BridgeUndo.UndoCreatePillar(packet);
	}

	private static void RedoTranslatePillar(BridgeActionPacket packet)
	{
		BridgePillar bridgePillar = BridgePillars.FindByGuid(packet.m_Pillar.m_Guid);
		if ((bool)bridgePillar)
		{
			bridgePillar.transform.Translate(packet.m_Translation.x, 0f, 0f);
			BridgeJoint anchor = bridgePillar.GetAnchor();
			if (anchor != null)
			{
				anchor.transform.Translate(packet.m_Translation);
			}
			bridgePillar.SetTopHeightBasedOnTotalHeight(bridgePillar.GetTotalHeight() + packet.m_Translation.y);
		}
	}

	public static void RedoHydraulicsControllerAddSplitJoint(BridgeActionPacket packet)
	{
		BridgeUndo.UndoHydraulicsControllerRemoveSplitJoint(packet);
	}

	public static void RedoHydraulicsControllerRemoveSplitJoint(BridgeActionPacket packet)
	{
		BridgeUndo.UndoHydraulicsControllerAddSplitJoint(packet);
	}

	public static void RedoHydraulicsControllerAddPiston(BridgeActionPacket packet)
	{
		BridgeUndo.UndoHydraulicsControllerRemovePiston(packet);
	}

	public static void RedoHydraulicsControllerRemovePiston(BridgeActionPacket packet)
	{
		BridgeUndo.UndoHydraulicsControllerAddPiston(packet);
	}

	public static void RedoHydraulicsControllerEnableNewAdditions(BridgeActionPacket packet)
	{
		BridgeUndo.UndoHydraulicsControllerDisableNewAdditions(packet);
	}

	public static void RedoHydraulicsControllerDisableNewAdditions(BridgeActionPacket packet)
	{
		BridgeUndo.UndoHydraulicsControllerEnableNewAdditions(packet);
	}

	public static void RedoHydraulicsControllerChangeSplitJointState(BridgeActionPacket packet)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(packet.m_HydraulicsPhaseGuid);
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if (hydraulicsControllerPhase != null && bridgeJoint != null)
		{
			bridgeJoint.SetSplitJointState(packet.m_SplitJointState);
			hydraulicsControllerPhase.SetStateForJoint(bridgeJoint, bridgeJoint.m_SplitJointState);
		}
	}

	public static void RedoCycleSplitJointSelector(BridgeActionPacket packet)
	{
		BridgeEdge bridgeEdge = ((!string.IsNullOrEmpty(packet.m_Edge.m_Guid)) ? BridgeEdges.FindByGuid(packet.m_Edge.m_Guid) : BridgeEdges.FindEnabledEdgeByJointGuids(packet.m_Edge.m_NodeA_Guid, packet.m_Edge.m_NodeB_Guid, packet.m_Edge.m_Material));
		if (bridgeEdge == null)
		{
			return;
		}
		if (packet.m_BridgeJointSelectorSide == BridgeJointSelectorSide.A)
		{
			bridgeEdge.m_JointAPart = packet.m_SplitJointPart;
			if ((bool)bridgeEdge.m_JointSelectorA)
			{
				BridgeJointSelectors.ShowBridgeJointSelectorForUndo(bridgeEdge.m_JointSelectorA);
			}
		}
		else
		{
			bridgeEdge.m_JointBPart = packet.m_SplitJointPart;
			if ((bool)bridgeEdge.m_JointSelectorB)
			{
				BridgeJointSelectors.ShowBridgeJointSelectorForUndo(bridgeEdge.m_JointSelectorB);
			}
		}
		bridgeEdge.RefreshJointSelectorNumbers();
	}
}
