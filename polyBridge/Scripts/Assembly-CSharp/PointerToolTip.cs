using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointerToolTip : MonoBehaviour
{
	public TextMeshProUGUI m_Text;

	public RectTransform m_RectTransform;

	public Image m_Background;

	public Image m_Outline;

	private static readonly string FORMAT_MOUSE_LEFT_ICON = "<sprite name=tooltip_mouseleftdrk>";

	private static readonly string FORMAT_MOUSE_RIGHT_ICON = "<sprite name=tooltip_mouserightdrk>";

	private static readonly string FORMAT_MOUSE_LEFT_DRAG_ICON = "<sprite name=tooltip_mouseleftswipe>";

	private static readonly string FORMAT_MOUSE_LEFT_DOUBLECLICK_ICON = "<sprite name=tooltip_mouseleftdoubledrk3>";

	private static readonly string FORMAT_SPACING = "     ";

	public void UpdateManual()
	{
		base.gameObject.SetActive(value: false);
		if (CampaignTutorial.IsRunning() && GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.UI_DRAW_ROAD || CampaignTutorial.m_CurrentStage == CampaignTutorialStage.UI_DRAW_WOOD || CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRO_DRAW)
			{
				if (BridgeJointPlacement.IsDrawing())
				{
					UpdateForDrawingGamepad();
				}
				else if (BridgeJointPlacement.m_HoverJoint != null && GameToolMode.GetMode() != GameToolModeType.MOVE)
				{
					UpdateForHoverJointGamepad(BridgeJointPlacement.m_HoverJoint);
				}
				else if (GameToolMode.GetMode() == GameToolModeType.BUILD && BridgeJointPlacement.m_SnapToJoint != null && GamepadManager.CursorMovingSlowly())
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("TOOLTIP_START_DRAWING", string.Empty));
				}
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.UI_SELECT_ROAD)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"));
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.UI_SELECT_WOOD)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("UI_MATERIALS"));
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"));
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.UI_SELECT_BRIDGE)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("UI_GROUP_SELECT"));
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.UI_COPY_BRIDGE)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.DPAD_UP, Localize.Get("UI_MOBILE_SELECTION_COPY"));
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.UI_PASTE_BRIDGE)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_MOBILE_CLIPBOARD_PASTE"));
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.UI_SIMULATE || CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRO_SIMULATE || CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_FIRST_SIM || CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SECOND_SIM)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.START, Localize.Get("UI_TUTORIAL_SIMULATE"));
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRO_DRAG)
			{
				if (Pistons.MouseIsOverPistonSlider() || (bool)Pistons.m_SliderFollowingMouse)
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("TOOLTIP_ADJUST_HYDRAULIC", string.Empty));
				}
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRO_MAKE_SPLIT)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, "x2 " + Localize.Get("TOOLTIP_SPLIT_JOINT", string.Empty));
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SELECT_CONTROLLER || CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_CLICK_D || CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SHOW_LEVEL_INFO || CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_DISABLE_HYDRO)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"));
			}
			else if (CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_FAILED)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("TOOLTIP_RETRY"));
			}
		}
		else
		{
			if (GameStateManager.GetState() != GameState.BUILD || ActivePanels.m_Panels.Count > 0 || GameStateBuild.m_CameraInTransition)
			{
				return;
			}
			string text = string.Empty;
			if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
			{
				text = UpdateForHydraulicsController();
			}
			else if (BridgeTrace.IsTracingActive())
			{
				text = UpdateForTracing();
				UpdateForTracingGamepad();
			}
			else if (HydraulicsPhases.m_Phases.Count > 0 && BridgeJointSelectors.JointSelectorIsUnderMouse() && !Pistons.m_SliderFollowingMouse && !BridgeSprings.m_SliderFollowingMouse)
			{
				text = Localize.Get("TOOLTIP_CYCLE_JOINT_NUMBER", (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? string.Empty : FORMAT_MOUSE_LEFT_ICON);
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text);
			}
			else if (Pistons.m_Pistons.Count > 0 && (Pistons.MouseIsOverPistonSlider() || (bool)Pistons.m_SliderFollowingMouse))
			{
				text = Localize.Get("TOOLTIP_ADJUST_HYDRAULIC", (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? string.Empty : FORMAT_MOUSE_LEFT_DRAG_ICON);
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text);
			}
			else if (BridgeSprings.m_BridgeSprings.Count > 0 && SandboxSettings.m_SpringAdjustmentsAllowed && (BridgeSprings.MouseIsOverSpringSlider() || (bool)BridgeSprings.m_SliderFollowingMouse))
			{
				text = Localize.Get("TOOLTIP_ADJUST_SPRING", (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? string.Empty : FORMAT_MOUSE_LEFT_DRAG_ICON);
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text);
			}
			else if (!ClipboardManager.ReadyToPaste() && BridgeJointPlacement.m_HoverJoint != null && !BridgeJointPlacement.IsDrawing() && GameToolMode.GetMode() != GameToolModeType.MOVE)
			{
				text = UpdateForHoverJoint();
				if (GameToolMode.GetMode() == GameToolModeType.BUILD && !ClipboardManager.ReadyToPaste() && GamepadManager.CursorMovingSlowly())
				{
					UpdateForHoverJointGamepad(BridgeJointPlacement.m_HoverJoint);
				}
			}
			else if (!ClipboardManager.ReadyToPaste() && BridgeJointPlacement.InPlacementMode() && Bridge.m_BuildMaterialType != BridgeMaterialType.PILLAR)
			{
				text = UpdateForDrawing();
				if (GamepadManager.CursorMovingSlowly() || BridgeJointPlacement.IsDrawing())
				{
					UpdateForDrawingGamepad();
				}
			}
			else if (ClipboardManager.ReadyToPaste())
			{
				text = Localize.Get("TOOLTIP_PASTE", FORMAT_MOUSE_LEFT_ICON);
				text += FORMAT_SPACING;
				text += Localize.Get("TOOLTIP_CANCEL_TIP", FORMAT_MOUSE_RIGHT_ICON);
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_MOBILE_CLIPBOARD_PASTE"), GamepadButtonType.DPAD_RIGHT, Localize.Get("UI_MOBILE_CLIPBOARD_FLIP_HOR"), GamepadButtonType.DPAD_UP, Localize.Get("UI_MOBILE_CLIPBOARD_FLIP_VER"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			}
			else if (!BridgeSelectionSet.IsEmpty() && GameToolMode.GetMode() != GameToolModeType.MOVE)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.DPAD_UP, Localize.Get("UI_MOBILE_SELECTION_COPY"), GamepadButtonType.DPAD_DOWN, Localize.Get("UI_MOBILE_SELECTION_DELETE"), GamepadButtonType.DPAD_RIGHT, Localize.Get("UI_MOBILE_SELECTION_CUT"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			}
			else if ((bool)GameStateBuild.m_HoverEdge && (GameStateBuild.m_HoverEdgeSeconds > 0.25f || GamepadManager.CursorMovingSlowly()) && !Pistons.m_SliderFollowingMouse && !BridgeSprings.m_SliderFollowingMouse && !CampaignTutorial.IsRunning() && GameToolMode.GetMode() != GameToolModeType.MOVE)
			{
				if (!BridgeSelectionSet.ContainsEdge(GameStateBuild.m_HoverEdge))
				{
					string localizedMaterialDisplayName = BridgeMaterials.GetLocalizedMaterialDisplayName(GameStateBuild.m_HoverEdge.m_Material.m_MaterialType);
					text = Localize.Get("TOOLTIP_SELECT_TRUSS", (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? string.Empty : FORMAT_MOUSE_RIGHT_ICON, localizedMaterialDisplayName);
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, text);
				}
			}
			else if ((bool)BridgePillars.GetBridgePillarAtScreenPos(GameInput.GetMousePosition()) && GamepadManager.CursorMovingSlowly() && !Pistons.m_SliderFollowingMouse && !BridgeSprings.m_SliderFollowingMouse && !CampaignTutorial.IsRunning() && GameToolMode.GetMode() != GameToolModeType.MOVE)
			{
				if (!BridgeSelectionSet.ContainsPillar(BridgePillars.GetBridgePillarAtScreenPos(GameInput.GetMousePosition())))
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("BINDING_SELECT_FOUNDATION"));
				}
			}
			else if (GameToolMode.GetMode() == GameToolModeType.BUILD && BridgeJointPlacement.m_SnapToJoint != null && !BridgeJointPlacement.IsDrawing() && (GamepadManager.CursorMovingSlowly() || BridgeJointPlacement.IsDrawing()))
			{
				text = Localize.Get("TOOLTIP_START_DRAWING", string.Empty);
				if ((BridgeJointPlacement.m_SnapToJoint.m_IsAnchor && !BridgePillars.IsBridgePillarAnchor(BridgeJointPlacement.m_SnapToJoint.m_Guid)) || BridgeJointPlacement.m_SnapToJoint.IsConnectedToLockedPrebuilt())
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text);
				}
				else
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text, GamepadButtonType.NORTH, BridgeJointPlacement.m_SnapToJoint.m_IsAnchor ? Localize.Get("UI_MOVE") : Localize.Get("UI_MOVE_JOINT_HOLD"));
				}
			}
			else if (BridgeEdges.AtLeastOneActiveEdge() && GameToolMode.GetMode() == GameToolModeType.BUILD)
			{
				if (!GroupSelect.IsActive() && !CampaignTutorial.IsRunning())
				{
					if (GameStateCommonInput.AllowedToPanCamera(withMouse: false))
					{
						GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.RIGHTSTICK, Localize.Get("UI_PAN"), GamepadButtonType.WEST, Localize.Get("UI_ERASE_HOLD"), GamepadButtonType.EAST, Localize.Get("UI_GROUP_SELECT"));
					}
					else
					{
						GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.WEST, Localize.Get("UI_ERASE_HOLD"), GamepadButtonType.EAST, Localize.Get("UI_GROUP_SELECT"));
					}
				}
			}
			else if (GameToolMode.GetMode() == GameToolModeType.BUILD)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.RIGHTSTICK, Localize.Get("UI_PAN"));
			}
			if (!Game.IsCurrentLevelTutorial())
			{
				if (ClipboardManager.ReadyToPaste())
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("UI_MOBILE_ROTATE"), GamepadButtonType.TRIGGER_LEFT, GamepadButtonType.TRIGGER_RIGHT, Localize.Get("UI_ZOOM"));
				}
				else
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("UI_MATERIALS"), GamepadButtonType.TRIGGER_LEFT, GamepadButtonType.TRIGGER_RIGHT, Localize.Get("UI_ZOOM"));
				}
			}
			if (!SuppressTooltip() && GameUI.HudIsActive() && !GameUI.IsPointerOverGameObject() && !string.IsNullOrEmpty(text) && !Profiles.m_ActiveProfile.m_DisableBuildHelpTooltips && GameInput.GetActiveGameDevice() != GameDevice.Gamepad && !Game.IsRunningOnSteamDeck())
			{
				GameUI.m_Instance.m_PointerToolTip.ForceEnable();
				GameUI.m_Instance.m_PointerToolTip.Set(text);
				GameUI.m_Instance.m_PointerToolTip.m_RectTransform.anchoredPosition = new Vector2(0f, (GameManager.GetGameMode() == GameMode.SANDBOX) ? (-110f) : (-55f));
			}
		}
	}

	public void Set(string text)
	{
		if (m_Text.text != text)
		{
			GameUI.SetAndEnableText(m_Text, text);
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_RectTransform);
		}
	}

	public void Enable()
	{
		base.gameObject.SetActive(value: true);
	}

	public void ForceEnable()
	{
		base.gameObject.SetActive(value: true);
	}

	public void Disable()
	{
		base.gameObject.SetActive(value: false);
	}

	public void SetColors(Color backgroundColor, Color outlineColor)
	{
		m_Background.color = backgroundColor;
		m_Outline.color = outlineColor;
	}

	public void UpdateForCopyPaste()
	{
		string empty = string.Empty;
		empty = Localize.Get("TOOLTIP_PASTE", FORMAT_MOUSE_LEFT_ICON);
		empty += FORMAT_SPACING;
		empty += Localize.Get("TOOLTIP_CANCEL_TIP", FORMAT_MOUSE_RIGHT_ICON);
		Set(empty);
	}

	private string UpdateForHydraulicsController()
	{
		if (HydraulicsPhases.m_Phases.Count > 0 && BridgeJointSelectors.JointSelectorIsUnderMouse())
		{
			return Localize.Get("TOOLTIP_CYCLE_JOINT_NUMBER", FORMAT_MOUSE_LEFT_ICON);
		}
		string text = string.Empty;
		BridgeJoint splitJointUnderPointer = GetSplitJointUnderPointer(GameInput.GetMousePosition());
		if (splitJointUnderPointer != null)
		{
			HydraulicsPhase selectedHydraulicsPhase = GameUI.m_Instance.m_HydraulicsController.GetSelectedHydraulicsPhase();
			if ((bool)selectedHydraulicsPhase)
			{
				if (HydraulicsController.PhaseAffectsSplitJoint(selectedHydraulicsPhase, splitJointUnderPointer))
				{
					if (splitJointUnderPointer.m_Split3.activeInHierarchy)
					{
						if (ThreeWaySplitJointWillToggleOff(splitJointUnderPointer))
						{
							text = Localize.Get("TOOLTIP_REMOVE_SPLIT_JOINT", FORMAT_MOUSE_LEFT_ICON);
						}
					}
					else
					{
						text = Localize.Get("TOOLTIP_REMOVE_SPLIT_JOINT", FORMAT_MOUSE_LEFT_ICON);
					}
				}
				else
				{
					text = Localize.Get("TOOLTIP_ADD_SPLIT_JOINT", FORMAT_MOUSE_LEFT_ICON);
				}
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			return text;
		}
		BridgeEdge bridgeEdge = null;
		int num = Physics.RaycastNonAlloc(Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition()), Utils.m_RaycastHits, float.MaxValue, Utils.EDGE_LAYER_MASK);
		for (int i = 0; i < num; i++)
		{
			bridgeEdge = Utils.m_RaycastHits[i].transform.parent.GetComponent<BridgeEdge>();
			if ((bool)bridgeEdge && bridgeEdge.IsPiston())
			{
				break;
			}
		}
		if ((bool)bridgeEdge && bridgeEdge.IsPiston())
		{
			Piston pistonOnEdge = Pistons.GetPistonOnEdge(bridgeEdge);
			HydraulicsPhase selectedHydraulicsPhase2 = GameUI.m_Instance.m_HydraulicsController.GetSelectedHydraulicsPhase();
			if ((bool)selectedHydraulicsPhase2 && (bool)pistonOnEdge)
			{
				text = ((!HydraulicsController.PhaseAffectsPiston(selectedHydraulicsPhase2, pistonOnEdge)) ? Localize.Get("TOOLTIP_ADD_HYDRAULIC", FORMAT_MOUSE_LEFT_ICON) : Localize.Get("TOOLTIP_REMOVE_HYDRAULIC", FORMAT_MOUSE_LEFT_ICON));
			}
		}
		return text;
	}

	private string UpdateForTracing()
	{
		string result = string.Empty;
		if (BridgeTrace.m_TracingFollowsMouse)
		{
			result = Localize.Get("TOOLTIP_PLACE_TRACE", FORMAT_MOUSE_LEFT_ICON);
			result += FORMAT_SPACING;
			result += Localize.Get("TOOLTIP_CANCEL_TIP", FORMAT_MOUSE_RIGHT_ICON);
		}
		else if (BridgeJointPlacement.m_HoverJoint != null)
		{
			result = Localize.Get("TOOLTIP_START_TRACE", FORMAT_MOUSE_LEFT_ICON);
		}
		return result;
	}

	private void UpdateForTracingGamepad()
	{
		if (BridgeTrace.m_TracingFollowsMouse)
		{
			string text = Localize.Get("TOOLTIP_PLACE_TRACE", string.Empty);
			string text2 = Localize.Get("TOOLTIP_CANCEL_TIP", string.Empty) ?? "";
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text, GamepadButtonType.DPAD_RIGHT, Localize.Get("UI_SHAPE"), GamepadButtonType.EAST, text2);
		}
		else if ((BridgeJointPlacement.m_HoverJoint != null || BridgeJointPlacement.m_SnapToJoint != null) && GamepadManager.CursorMovingSlowly())
		{
			string text3 = Localize.Get("TOOLTIP_START_TRACE", string.Empty);
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text3, GamepadButtonType.DPAD_RIGHT, Localize.Get("UI_SHAPE"));
		}
		else if (BridgeTrace.IsTraceLinePlaced() && !BridgeTrace.IsFilling())
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.DPAD_DOWN, Localize.Get("UI_FILL"), GamepadButtonType.DPAD_RIGHT, Localize.Get("UI_SHAPE"), GamepadButtonType.DPAD_UP, Localize.Get("UI_CLEAR_TRACER"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.DPAD_RIGHT, Localize.Get("UI_SHAPE"), GamepadButtonType.DPAD_UP, Localize.Get("UI_TRACE_TOOL_OFF"));
		}
	}

	private string UpdateForHoverJoint()
	{
		string empty = string.Empty;
		if (!BridgeJointPlacement.SelectionCircleActive())
		{
			empty = Localize.Get("TOOLTIP_START_DRAWING", FORMAT_MOUSE_LEFT_ICON);
			empty += FORMAT_SPACING;
			empty += Localize.Get("TOOLTIP_SELECT_JOINT", FORMAT_MOUSE_RIGHT_ICON);
			if (HydraulicsPhases.m_Phases.Count > 0)
			{
				empty += FORMAT_SPACING;
				empty = ((!BridgeJointPlacement.m_HoverJoint.m_IsSplit) ? (empty + Localize.Get("TOOLTIP_SPLIT_JOINT", FORMAT_MOUSE_LEFT_DOUBLECLICK_ICON)) : (empty + Localize.Get("TOOLTIP_UNSPLIT_JOINT", FORMAT_MOUSE_LEFT_DOUBLECLICK_ICON)));
			}
		}
		else
		{
			empty += Localize.Get("TOOLTIP_STOP_DRAWING", FORMAT_MOUSE_RIGHT_ICON);
		}
		return empty;
	}

	private void UpdateForHoverJointGamepad()
	{
		if (Game.IsCurrentLevelTutorial())
		{
			if (!BridgeJointPlacement.SelectionCircleActive())
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("TOOLTIP_START_DRAWING", string.Empty));
			}
		}
		else if (!BridgeJointPlacement.SelectionCircleActive())
		{
			string text = Localize.Get("TOOLTIP_START_DRAWING", string.Empty);
			string text2 = Localize.Get("TOOLTIP_SELECT_JOINT", string.Empty);
			if (HydraulicsPhases.m_Phases.Count > 0)
			{
				string empty = string.Empty;
				empty = ((!BridgeJointPlacement.m_HoverJoint.m_IsSplit) ? Localize.Get("TOOLTIP_SPLIT_JOINT", string.Empty) : Localize.Get("TOOLTIP_UNSPLIT_JOINT", string.Empty));
				if ((BridgeJointPlacement.m_HoverJoint.m_IsAnchor && !BridgePillars.IsBridgePillarAnchor(BridgeJointPlacement.m_HoverJoint.m_Guid)) || BridgeJointPlacement.m_HoverJoint.IsConnectedToLockedPrebuilt())
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, "x2 " + empty, GamepadButtonType.EAST, text2);
				}
				else
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, "x2 " + empty, GamepadButtonType.EAST, text2, GamepadButtonType.NORTH, BridgeJointPlacement.m_HoverJoint.m_IsAnchor ? Localize.Get("UI_MOVE") : Localize.Get("UI_MOVE_JOINT_HOLD"));
				}
			}
			else if ((BridgeJointPlacement.m_HoverJoint.m_IsAnchor && !BridgePillars.IsBridgePillarAnchor(BridgeJointPlacement.m_HoverJoint.m_Guid)) || BridgeJointPlacement.m_HoverJoint.IsConnectedToLockedPrebuilt())
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text, GamepadButtonType.EAST, text2);
			}
			else
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text, GamepadButtonType.EAST, text2, GamepadButtonType.NORTH, BridgeJointPlacement.m_HoverJoint.m_IsAnchor ? Localize.Get("UI_MOVE") : Localize.Get("UI_MOVE_JOINT_HOLD"));
			}
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("TOOLTIP_STOP_DRAWING", string.Empty));
		}
	}

	private void UpdateForHoverJointGamepad(BridgeJoint hoverJoint)
	{
		if (Game.IsCurrentLevelTutorial())
		{
			if (!BridgeJointPlacement.SelectionCircleActive())
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("TOOLTIP_START_DRAWING", string.Empty));
			}
		}
		else if (!BridgeJointPlacement.SelectionCircleActive())
		{
			string text = Localize.Get("TOOLTIP_START_DRAWING", string.Empty);
			string text2 = Localize.Get("TOOLTIP_SELECT_JOINT", string.Empty);
			if (HydraulicsPhases.m_Phases.Count > 0)
			{
				string empty = string.Empty;
				empty = ((!BridgeJointPlacement.m_HoverJoint.m_IsSplit) ? Localize.Get("TOOLTIP_SPLIT_JOINT", string.Empty) : Localize.Get("TOOLTIP_UNSPLIT_JOINT", string.Empty));
				if ((BridgeJointPlacement.m_HoverJoint.m_IsAnchor && !BridgePillars.IsBridgePillarAnchor(BridgeJointPlacement.m_HoverJoint.m_Guid)) || BridgeJointPlacement.m_HoverJoint.IsConnectedToLockedPrebuilt())
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, "x2 " + empty, GamepadButtonType.EAST, text2);
				}
				else
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, "x2 " + empty, GamepadButtonType.EAST, text2, GamepadButtonType.NORTH, BridgeJointPlacement.m_HoverJoint.m_IsAnchor ? Localize.Get("UI_MOVE") : Localize.Get("UI_MOVE_JOINT_HOLD"));
				}
			}
			else if ((BridgeJointPlacement.m_HoverJoint.m_IsAnchor && !BridgePillars.IsBridgePillarAnchor(BridgeJointPlacement.m_HoverJoint.m_Guid)) || BridgeJointPlacement.m_HoverJoint.IsConnectedToLockedPrebuilt())
			{
				if (BridgeJointPlacement.m_HoverJoint.GetNumConnectedEdges() > 0)
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text, GamepadButtonType.EAST, text2);
				}
				else
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text);
				}
			}
			else
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text, GamepadButtonType.EAST, text2, GamepadButtonType.NORTH, BridgeJointPlacement.m_HoverJoint.m_IsAnchor ? Localize.Get("UI_MOVE") : Localize.Get("UI_MOVE_JOINT_HOLD"));
			}
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("TOOLTIP_STOP_DRAWING", string.Empty));
		}
	}

	private string UpdateForDrawing()
	{
		string text = string.Empty;
		if (BridgeJointPlacement.EdgeBisectDotActive())
		{
			BridgeMaterialType materialTypeToBeBisected = BridgeJointPlacement.GetMaterialTypeToBeBisected();
			if (materialTypeToBeBisected != BridgeMaterialType.INVALID)
			{
				string localizedMaterialDisplayName = BridgeMaterials.GetLocalizedMaterialDisplayName(materialTypeToBeBisected);
				text = Localize.Get("TOOLTIP_BISECT", FORMAT_MOUSE_LEFT_ICON, localizedMaterialDisplayName);
			}
		}
		else
		{
			string localizedMaterialDisplayName2 = BridgeMaterials.GetLocalizedMaterialDisplayName(Bridge.m_BuildMaterialType);
			text = Localize.Get("TOOLTIP_PLACE_MATERIAL", FORMAT_MOUSE_LEFT_ICON, localizedMaterialDisplayName2);
		}
		return text + FORMAT_SPACING + Localize.Get("TOOLTIP_STOP_DRAWING", FORMAT_MOUSE_RIGHT_ICON);
	}

	private void UpdateForDrawingGamepad()
	{
		string text = string.Empty;
		if (BridgeJointPlacement.EdgeBisectDotActive())
		{
			BridgeMaterialType materialTypeToBeBisected = BridgeJointPlacement.GetMaterialTypeToBeBisected();
			if (materialTypeToBeBisected != BridgeMaterialType.INVALID)
			{
				string localizedMaterialDisplayName = BridgeMaterials.GetLocalizedMaterialDisplayName(materialTypeToBeBisected);
				text = Localize.Get("TOOLTIP_BISECT", string.Empty, localizedMaterialDisplayName);
			}
		}
		else
		{
			string localizedMaterialDisplayName2 = BridgeMaterials.GetLocalizedMaterialDisplayName(Bridge.m_BuildMaterialType);
			text = Localize.Get("TOOLTIP_PLACE_MATERIAL", string.Empty, localizedMaterialDisplayName2);
		}
		string text2 = Localize.Get("TOOLTIP_STOP_DRAWING", string.Empty);
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, text, GamepadButtonType.EAST, text2);
	}

	private BridgeJoint GetSplitJointUnderPointer(Vector2 screenPos)
	{
		BridgeJoint bridgeJoint = null;
		Collider closestRaycastHit = Utils.GetClosestRaycastHit(screenPos, Utils.JOINT_HOTSPOT_LAYER_MASK);
		if ((bool)closestRaycastHit)
		{
			bridgeJoint = closestRaycastHit.transform.parent.GetComponent<BridgeJoint>();
		}
		if ((bool)bridgeJoint && bridgeJoint.m_IsSplit)
		{
			return bridgeJoint;
		}
		return null;
	}

	private bool ThreeWaySplitJointWillToggleOff(BridgeJoint joint)
	{
		if (joint.m_SplitJointState == SplitJointState.ALL_SPLIT)
		{
			return true;
		}
		SplitJointNumber splitJointNumber = BridgeJointSelectors.SplitJointNumberUnderMouse(GameInput.GetMousePosition());
		if (!splitJointNumber)
		{
			return false;
		}
		if (splitJointNumber.m_SplitJointPart == SplitJointPart.A && joint.m_SplitJointState == SplitJointState.A_SPLIT_ONLY)
		{
			return true;
		}
		if (splitJointNumber.m_SplitJointPart == SplitJointPart.B && joint.m_SplitJointState == SplitJointState.B_SPLIT_ONLY)
		{
			return true;
		}
		if (splitJointNumber.m_SplitJointPart == SplitJointPart.C && joint.m_SplitJointState == SplitJointState.C_SPLIT_ONLY)
		{
			return true;
		}
		return false;
	}

	private bool SuppressTooltip()
	{
		if (!GroupSelect.IsActive() && !BridgeJointMovement.m_SelectedJoint && GameToolMode.GetMode() != GameToolModeType.MOVE)
		{
			return GameToolMode.GetMode() == GameToolModeType.ERASE;
		}
		return true;
	}
}
