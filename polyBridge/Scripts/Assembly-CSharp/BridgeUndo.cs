using System.Collections.Generic;
using UnityEngine;

public class BridgeUndo
{
	public static Stack<Queue<BridgeActionPacket>> m_Stack = new Stack<Queue<BridgeActionPacket>>();

	private static Stack<BridgeActionPacket> m_TempStack = new Stack<BridgeActionPacket>();

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

	public static bool CanUndo()
	{
		if (GameToolMode.GetMode() == GameToolModeType.ERASE && GameInput.IsDown((GameInput.GetActiveGameDevice() != GameDevice.Gamepad) ? BindingType.DRAW_BUILD : BindingType.ERASE) && !GameUI.IsPointerOverGameObject())
		{
			return false;
		}
		return m_Stack.Count > 0;
	}

	public static void Undo()
	{
		if (!CanUndo())
		{
			return;
		}
		Queue<BridgeActionPacket> queue = m_Stack.Pop();
		m_TempStack.Clear();
		foreach (BridgeActionPacket item in queue)
		{
			m_TempStack.Push(item);
		}
		while (m_TempStack.Count > 0)
		{
			BridgeActionPacket bridgeActionPacket = m_TempStack.Pop();
			switch (bridgeActionPacket.m_Action)
			{
			case BridgeAction.CREATE_JOINT:
				UndoCreateJoint(bridgeActionPacket);
				break;
			case BridgeAction.DELETE_JOINT:
				UndoDeleteJoint(bridgeActionPacket);
				break;
			case BridgeAction.TRANSLATE_JOINT:
				UndoTranslateJoint(bridgeActionPacket);
				break;
			case BridgeAction.CREATE_EDGE:
				UndoCreateEdge(bridgeActionPacket);
				break;
			case BridgeAction.DELETE_EDGE:
				UndoDeleteEdge(bridgeActionPacket);
				break;
			case BridgeAction.MAKE_ANCHOR:
				UndoMakeAnchor(bridgeActionPacket);
				break;
			case BridgeAction.UNMAKE_ANCHOR:
				UndoUnMakeAnchor(bridgeActionPacket);
				break;
			case BridgeAction.PISTON_SLIDER_TRANSLATE:
				UndoTranslatePistonSlider(bridgeActionPacket);
				break;
			case BridgeAction.SPRING_SLIDER_TRANSLATE:
				UndoTranslateSpringSlider(bridgeActionPacket);
				break;
			case BridgeAction.SPLIT_JOINT:
				UndoSplitJoint(bridgeActionPacket);
				break;
			case BridgeAction.UNSPLIT_JOINT:
				UndoUnSplitJoint(bridgeActionPacket);
				break;
			case BridgeAction.CREATE_PILLAR:
				UndoCreatePillar(bridgeActionPacket);
				break;
			case BridgeAction.DELETE_PILLAR:
				UndoDeletePillar(bridgeActionPacket);
				break;
			case BridgeAction.TRANSLATE_PILLAR:
				UndoTranslatePillar(bridgeActionPacket);
				break;
			case BridgeAction.SERIALIZE_BRIDGE_PRE:
			{
				Quaternion clipboardRotation = ClipboardManager.GetClipboardRotation();
				Bridge.ClearAndLoadforUndo(bridgeActionPacket.m_BridgeSaveDataPre);
				if (bridgeActionPacket.m_ClipboardSaveData != null)
				{
					bridgeActionPacket.m_ClipboardSaveData.RestoreToClipboard(clipboardRotation);
				}
				break;
			}
			case BridgeAction.HYDRAULICS_CONTROLLER_ADD_SPLIT_JOINT:
				UndoHydraulicsControllerAddSplitJoint(bridgeActionPacket);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_REMOVE_SPLIT_JOINT:
				UndoHydraulicsControllerRemoveSplitJoint(bridgeActionPacket);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_ADD_PISTON:
				UndoHydraulicsControllerAddPiston(bridgeActionPacket);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_REMOVE_PISTON:
				UndoHydraulicsControllerRemovePiston(bridgeActionPacket);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_ENABLE_NEW_ADDITIONS:
				UndoHydraulicsControllerEnableNewAdditions(bridgeActionPacket);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_DISABLE_NEW_ADDITIONS:
				UndoHydraulicsControllerDisableNewAdditions(bridgeActionPacket);
				break;
			case BridgeAction.HYDRAULICS_CONTROLLER_CHANGE_SPLIT_JOINT_STATE:
				UndoHydraulicsControllerChangeSplitJointState(bridgeActionPacket);
				break;
			case BridgeAction.SPLIT_JOINT_SELECTOR_CYCLE:
				UndoCycleSplitJointSelector(bridgeActionPacket);
				break;
			default:
				Debug.LogErrorFormat("Unsupported action {0}", bridgeActionPacket.m_Action.ToString());
				break;
			case BridgeAction.SERIALIZE_BRIDGE_POST:
				break;
			}
		}
		BridgeRedo.m_Stack.Push(queue);
	}

	public static void UndoCreateJoint(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if ((bool)bridgeJoint)
		{
			bridgeJoint.gameObject.SetActive(value: false);
			GameStateBuild.ClearFirstBreakAttachedToJoint(bridgeJoint.m_Guid);
		}
	}

	public static void UndoDeleteJoint(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if ((bool)bridgeJoint)
		{
			bridgeJoint.gameObject.SetActive(value: true);
		}
		else
		{
			BridgeJoints.CreateJointFromProxy(packet.m_Joint);
		}
	}

	public static void UndoCreateEdge(BridgeActionPacket packet)
	{
		BridgeEdge bridgeEdge = ((!string.IsNullOrEmpty(packet.m_Edge.m_Guid)) ? BridgeEdges.FindByGuid(packet.m_Edge.m_Guid) : BridgeEdges.FindEnabledEdgeByJointGuids(packet.m_Edge.m_NodeA_Guid, packet.m_Edge.m_NodeB_Guid, packet.m_Edge.m_Material));
		if ((bool)bridgeEdge)
		{
			bridgeEdge.ForceDisable();
			GameStateBuild.ClearFirstBreakAttachedToJoint(bridgeEdge.m_JointA.m_Guid);
		}
	}

	public static void UndoDeleteEdge(BridgeActionPacket packet)
	{
		BridgeEdge bridgeEdge = ((!string.IsNullOrEmpty(packet.m_Edge.m_Guid)) ? BridgeEdges.FindByGuid(packet.m_Edge.m_Guid) : BridgeEdges.FindDisabledEdgeByJointGuids(packet.m_Edge.m_NodeA_Guid, packet.m_Edge.m_NodeB_Guid, packet.m_Edge.m_Material));
		if ((bool)bridgeEdge)
		{
			bridgeEdge.ForceEnable();
			bridgeEdge.RefreshJointSelectorNumbers();
			Piston pistonOnEdge = Pistons.GetPistonOnEdge(bridgeEdge);
			if (!pistonOnEdge)
			{
				return;
			}
			pistonOnEdge.m_Slider.gameObject.SetActive(value: true);
			{
				foreach (string hydraulicsPhase3 in packet.m_HydraulicsPhases)
				{
					HydraulicsPhase hydraulicsPhase = HydraulicsPhases.FindByGuid(hydraulicsPhase3);
					if (hydraulicsPhase != null)
					{
						HydraulicsController.AddPistonToHydraulicsPhase(hydraulicsPhase, pistonOnEdge);
					}
				}
				return;
			}
		}
		BridgeEdge bridgeEdge2 = BridgeEdges.CreateEdgeFromProxy(packet.m_Edge);
		if ((bool)bridgeEdge2 && bridgeEdge2.IsPiston() && packet.m_Piston != null)
		{
			Piston piston = Pistons.CreatePistonFromProxy(packet.m_Piston);
			if ((bool)piston)
			{
				piston.m_Slider.gameObject.SetActive(value: true);
				foreach (string hydraulicsPhase4 in packet.m_HydraulicsPhases)
				{
					HydraulicsPhase hydraulicsPhase2 = HydraulicsPhases.FindByGuid(hydraulicsPhase4);
					if (hydraulicsPhase2 != null)
					{
						HydraulicsController.AddPistonToHydraulicsPhase(hydraulicsPhase2, piston);
					}
				}
			}
		}
		if ((bool)bridgeEdge2 && bridgeEdge2.IsSpring() && packet.m_Spring != null)
		{
			BridgeSprings.CreateSpringFromProxy(packet.m_Spring);
		}
	}

	private static void UndoTranslateJoint(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if (!bridgeJoint)
		{
			return;
		}
		bridgeJoint.transform.position = bridgeJoint.m_BuildPos - packet.m_Translation;
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

	private static void UndoMakeAnchor(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if ((bool)bridgeJoint)
		{
			bridgeJoint.RevertAnchor();
		}
	}

	private static void UndoUnMakeAnchor(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if ((bool)bridgeJoint)
		{
			bridgeJoint.MakeAnchor();
		}
	}

	private static void UndoTranslatePistonSlider(BridgeActionPacket packet)
	{
		Piston piston = Pistons.FindByGuid(packet.m_Piston.m_Guid);
		if ((bool)piston)
		{
			piston.m_Slider.SetNormalizedValue(piston.m_Slider.GetNormalizedValue() - packet.m_Translation.x);
			piston.m_Slider.SetVisibilityExpireTime();
		}
	}

	private static void UndoTranslateSpringSlider(BridgeActionPacket packet)
	{
		BridgeSpring bridgeSpring = BridgeSprings.FindByGuid(packet.m_Spring.m_Guid);
		if ((bool)bridgeSpring)
		{
			bridgeSpring.m_Slider.SetNormalizedValue(bridgeSpring.m_Slider.GetNormalizedValue() - packet.m_Translation.x);
			bridgeSpring.RefreshVisualization();
			bridgeSpring.m_Slider.SetVisibilityExpireTime();
		}
	}

	public static void UndoSplitJoint(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if ((bool)bridgeJoint)
		{
			bridgeJoint.UnSplit();
		}
	}

	public static void UndoUnSplitJoint(BridgeActionPacket packet)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if (!bridgeJoint)
		{
			return;
		}
		bridgeJoint.Split();
		foreach (string hydraulicsPhase2 in packet.m_HydraulicsPhases)
		{
			HydraulicsPhase hydraulicsPhase = HydraulicsPhases.FindByGuid(hydraulicsPhase2);
			if (hydraulicsPhase != null)
			{
				HydraulicsController.FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase)?.AddSplitJoint(bridgeJoint, (bridgeJoint.m_SplitJointState != SplitJointState.NONE_SPLIT) ? bridgeJoint.m_SplitJointState : SplitJointState.ALL_SPLIT);
			}
		}
	}

	public static void UndoCreatePillar(BridgeActionPacket packet)
	{
		BridgePillar bridgePillar = BridgePillars.FindByGuid(packet.m_Pillar.m_Guid);
		if ((bool)bridgePillar)
		{
			bridgePillar.gameObject.SetActive(value: false);
		}
	}

	public static void UndoDeletePillar(BridgeActionPacket packet)
	{
		BridgePillar bridgePillar = BridgePillars.FindByGuid(packet.m_Pillar.m_Guid);
		if ((bool)bridgePillar)
		{
			bridgePillar.gameObject.SetActive(value: true);
		}
		else
		{
			BridgePillars.CreateBridgePillarFromProxy(packet.m_Pillar);
		}
	}

	private static void UndoTranslatePillar(BridgeActionPacket packet)
	{
		BridgePillar bridgePillar = BridgePillars.FindByGuid(packet.m_Pillar.m_Guid);
		if ((bool)bridgePillar)
		{
			bridgePillar.transform.position = bridgePillar.transform.position - new Vector3(packet.m_Translation.x, 0f, 0f);
			BridgeJoint anchor = bridgePillar.GetAnchor();
			if (anchor != null)
			{
				anchor.transform.position = anchor.transform.position - packet.m_Translation;
				anchor.m_SandboxItem.SetOutlineDirty(dirty: true);
			}
			bridgePillar.SetTopHeightBasedOnTotalHeight(bridgePillar.GetTotalHeight() - packet.m_Translation.y);
		}
	}

	public static void UndoHydraulicsControllerAddSplitJoint(BridgeActionPacket packet)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(packet.m_HydraulicsPhaseGuid);
		if (hydraulicsControllerPhase != null)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
			if (bridgeJoint != null && hydraulicsControllerPhase.AffectsSplitJoint(bridgeJoint))
			{
				hydraulicsControllerPhase.RemoveSplitJoint(bridgeJoint);
				PulseHydraulicControllerIcons(packet.m_HydraulicsPhaseGuid);
			}
		}
	}

	public static void UndoHydraulicsControllerRemoveSplitJoint(BridgeActionPacket packet)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(packet.m_HydraulicsPhaseGuid);
		if (hydraulicsControllerPhase != null)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
			if (bridgeJoint != null && !hydraulicsControllerPhase.AffectsSplitJoint(bridgeJoint))
			{
				hydraulicsControllerPhase.AddSplitJoint(bridgeJoint, packet.m_SplitJointState);
				PulseHydraulicControllerIcons(packet.m_HydraulicsPhaseGuid);
			}
		}
	}

	public static void UndoHydraulicsControllerAddPiston(BridgeActionPacket packet)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(packet.m_HydraulicsPhaseGuid);
		if (hydraulicsControllerPhase != null && packet.m_Piston != null)
		{
			Piston piston = Pistons.FindByGuid(packet.m_Piston.m_Guid);
			if (piston != null && hydraulicsControllerPhase.m_Pistons.Contains(piston))
			{
				hydraulicsControllerPhase.m_Pistons.Remove(piston);
				PulseHydraulicControllerIcons(packet.m_HydraulicsPhaseGuid);
			}
		}
	}

	public static void UndoHydraulicsControllerRemovePiston(BridgeActionPacket packet)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(packet.m_HydraulicsPhaseGuid);
		if (hydraulicsControllerPhase != null && packet.m_Piston != null)
		{
			Piston piston = Pistons.FindByGuid(packet.m_Piston.m_Guid);
			if (piston != null && !hydraulicsControllerPhase.m_Pistons.Contains(piston))
			{
				hydraulicsControllerPhase.m_Pistons.Add(piston);
				PulseHydraulicControllerIcons(packet.m_HydraulicsPhaseGuid);
			}
		}
	}

	public static void UndoHydraulicsControllerEnableNewAdditions(BridgeActionPacket packet)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(packet.m_HydraulicsPhaseGuid);
		if (hydraulicsControllerPhase != null)
		{
			hydraulicsControllerPhase.m_DisableNewAdditions = true;
			if ((bool)GameUI.m_Instance && (bool)GameUI.m_Instance.m_HydraulicsController)
			{
				GameUI.m_Instance.m_HydraulicsController.UpdateStageOffIconForAllPhases();
				PulseHydraulicControllerIcons(packet.m_HydraulicsPhaseGuid);
			}
		}
	}

	public static void UndoHydraulicsControllerDisableNewAdditions(BridgeActionPacket packet)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(packet.m_HydraulicsPhaseGuid);
		if (hydraulicsControllerPhase != null)
		{
			hydraulicsControllerPhase.m_DisableNewAdditions = false;
			if ((bool)GameUI.m_Instance && (bool)GameUI.m_Instance.m_HydraulicsController)
			{
				GameUI.m_Instance.m_HydraulicsController.UpdateStageOffIconForAllPhases();
				PulseHydraulicControllerIcons(packet.m_HydraulicsPhaseGuid);
			}
		}
	}

	public static void UndoHydraulicsControllerChangeSplitJointState(BridgeActionPacket packet)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(packet.m_HydraulicsPhaseGuid);
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(packet.m_Joint.m_Guid);
		if (hydraulicsControllerPhase != null && bridgeJoint != null)
		{
			bridgeJoint.SetSplitJointState(packet.m_PrevSplitJointState);
			hydraulicsControllerPhase.SetStateForJoint(bridgeJoint, bridgeJoint.m_SplitJointState);
			PulseHydraulicControllerIcons(packet.m_HydraulicsPhaseGuid);
		}
	}

	public static void UndoCycleSplitJointSelector(BridgeActionPacket packet)
	{
		BridgeEdge bridgeEdge = ((!string.IsNullOrEmpty(packet.m_Edge.m_Guid)) ? BridgeEdges.FindByGuid(packet.m_Edge.m_Guid) : BridgeEdges.FindEnabledEdgeByJointGuids(packet.m_Edge.m_NodeA_Guid, packet.m_Edge.m_NodeB_Guid, packet.m_Edge.m_Material));
		if (bridgeEdge == null)
		{
			return;
		}
		if (packet.m_BridgeJointSelectorSide == BridgeJointSelectorSide.A)
		{
			bridgeEdge.m_JointAPart = packet.m_PrevSplitJointPart;
			if ((bool)bridgeEdge.m_JointSelectorA)
			{
				BridgeJointSelectors.ShowBridgeJointSelectorForUndo(bridgeEdge.m_JointSelectorA);
			}
		}
		else
		{
			bridgeEdge.m_JointBPart = packet.m_PrevSplitJointPart;
			if ((bool)bridgeEdge.m_JointSelectorB)
			{
				BridgeJointSelectors.ShowBridgeJointSelectorForUndo(bridgeEdge.m_JointSelectorB);
			}
		}
		bridgeEdge.RefreshJointSelectorNumbers();
	}

	private static void PulseHydraulicControllerIcons(string hydraulicsPhaseGuid)
	{
		GameUI.m_Instance.m_BottomBar.PulseHydraulicControllerIcon();
		HydraulicsPhase hydraulicsPhase = HydraulicsPhases.FindByGuid(hydraulicsPhaseGuid);
		if (hydraulicsPhase != null)
		{
			EventStage stageWithUnit = GameUI.m_Instance.m_HydraulicsController.m_Stages.GetStageWithUnit(hydraulicsPhase.gameObject);
			if (stageWithUnit != null)
			{
				stageWithUnit.Pulse();
			}
		}
	}
}
