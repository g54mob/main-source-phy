using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_HydraulicsController : MonoBehaviour
{
	[Header("Panel")]
	public RectTransform m_Panel;

	public Panel_LevelInfoLite m_LevelInfoLite;

	public RectTransform m_ViewportRectTransform;

	public RectTransform m_ContentRectTransform;

	public Panel_Stages m_Stages;

	public int m_MinPanelWidth;

	public int m_MinPanelHeight;

	public int m_MaxPanelHeight;

	[Header("Controls")]
	public Toggle m_ThreeWayJointsToggle;

	public Button m_AddAll;

	public Button m_RemoveAll;

	public Button m_OK;

	[NonSerialized]
	public bool m_Locked;

	private EventUnit m_HoverIcon;

	private EventUnit m_SelectedPhase;

	private float ENLARGE_PISTON_COLLIDER_FACTOR = 1.8f;

	private PointerEvents m_ThreeWayJointsTogglePointerEvents;

	private PointerEventData m_PointerEventData;

	private List<RaycastResult> m_RaycastResults = new List<RaycastResult>();

	private bool m_UpdateOffIconsNextFrame;

	private float m_ContentLastX;

	private float m_ContentLastY;

	private bool m_IsDraggingScrollbar;

	private const int DEFAULT_PANEL_WIDTH = 420;

	private const int DEFAULT_PANEL_HEIGHT = 192;

	private void Awake()
	{
		m_ThreeWayJointsTogglePointerEvents = m_ThreeWayJointsToggle.GetComponent<PointerEvents>();
		m_ThreeWayJointsTogglePointerEvents.RegisterOnClickedDelegate(OnThreeWayJointsToggle);
		m_Stages.m_OnEnableCallback = SelectFirstPhase;
		m_OK.onClick.AddListener(Close);
		m_AddAll.onClick.AddListener(OnAddAll);
		m_RemoveAll.onClick.AddListener(OnRemoveAll);
	}

	private void Start()
	{
		m_PointerEventData = new PointerEventData(EventSystem.current);
	}

	private void OnEnable()
	{
		Bridge.CancelSelection();
		BridgeJoints.RefreshThreeWaySplitJointNumberVisibility();
		BridgeJoints.SetHydraulicControllerSortOrder();
		m_ThreeWayJointsToggle.isOn = SandboxSettings.m_ThreeWaySplitJointsEnabled;
		m_AddAll.gameObject.SetActive(HydraulicsPhases.m_Phases.Count > 0);
		m_RemoveAll.gameObject.SetActive(HydraulicsPhases.m_Phases.Count > 0);
		ActivePanels.Add(base.gameObject);
		m_UpdateOffIconsNextFrame = true;
		m_ContentLastX = m_ContentRectTransform.anchoredPosition.x;
		m_ContentLastY = m_ContentRectTransform.anchoredPosition.y;
		m_AddAll.interactable = !m_Locked;
		m_RemoveAll.interactable = !m_Locked;
		m_ThreeWayJointsToggle.interactable = !m_Locked;
		m_OK.interactable = !m_Locked;
		GameUI.m_Instance.m_BottomBar.SetHydraulicsControllerIconColor(GameUI.m_Instance.m_GoldColor);
		GameUI.m_Instance.m_BottomBar.SetMaterialIconsInteractive(interactable: false);
		GameUI.m_Instance.m_BuildToolBar.SetGameToolIconsInteractive(interactable: false);
		UpdateStageOffIconForAllPhases();
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_BottomBar.gameObject.SetActive(value: false);
	}

	private void OnDisable()
	{
		BridgeJoints.HideHydraulicControllerTwoWaySplitUI();
		BridgeJoints.HideThreeWaySplitJointNumbers();
		BridgeJoints.SetDefaultSortOrder();
		GameUI.m_Instance.m_BottomBar.SetHydraulicsControllerIconColor(Color.white);
		GameUI.m_Instance.m_BottomBar.SetMaterialIconsInteractive(interactable: true);
		GameUI.m_Instance.m_BuildToolBar.SetGameToolIconsInteractive(interactable: true);
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_BottomBar.gameObject.SetActive(value: true);
	}

	private void Update()
	{
		ProcessInput();
		if (m_UpdateOffIconsNextFrame)
		{
			UpdateStageOffIconForAllPhases();
			m_UpdateOffIconsNextFrame = false;
		}
		UpdatePanelDimensions();
		UpdateHover();
		UpdateScrollbarState();
		if (ActivePanels.IsTopPanel(base.gameObject) && !PopUpMessage.IsActive())
		{
			ShowGamepadLegend();
		}
	}

	public void UpdateForCurrentDevice()
	{
		m_Panel.anchoredPosition = new Vector2(0f, (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? GamepadLegend.HEIGHT : 0);
	}

	public void OnAddAll()
	{
		HydraulicsPhase selectedHydraulicsPhase = GetSelectedHydraulicsPhase();
		if (selectedHydraulicsPhase == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		RecordAddAllForUndo(selectedHydraulicsPhase);
		HydraulicsController.AddAllPistonsToPhase(selectedHydraulicsPhase);
		HydraulicsController.AddAllSplitJointsToPhase(selectedHydraulicsPhase);
		HydraulicsController.EnableNewAdditionsFromPhase(selectedHydraulicsPhase);
		EventStage stageWithUnit = EventTimelines.GetStageWithUnit(selectedHydraulicsPhase.gameObject);
		if (stageWithUnit != null)
		{
			m_Stages.EnableOffIconForStage(stageWithUnit, enable: false);
		}
		InterfaceAudio.Play("ui_menu_select");
	}

	public void OnRemoveAll()
	{
		HydraulicsPhase selectedHydraulicsPhase = GetSelectedHydraulicsPhase();
		if (selectedHydraulicsPhase == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		RecordRemoveAllForUndo(selectedHydraulicsPhase);
		HydraulicsController.RemoveAllPistonsFromPhase(selectedHydraulicsPhase);
		HydraulicsController.RemoveAllSplitJointsFromPhase(selectedHydraulicsPhase);
		HydraulicsController.DisableNewAdditionsFromPhase(selectedHydraulicsPhase);
		EventStage stageWithUnit = EventTimelines.GetStageWithUnit(selectedHydraulicsPhase.gameObject);
		if (stageWithUnit != null)
		{
			m_Stages.EnableOffIconForStage(stageWithUnit, enable: true);
		}
		InterfaceAudio.Play("ui_menu_select");
	}

	public EventUnit GetSelectedPhase()
	{
		return m_SelectedPhase;
	}

	public HydraulicsPhase GetSelectedHydraulicsPhase()
	{
		if (!m_SelectedPhase)
		{
			return null;
		}
		return m_SelectedPhase.GetHydraulicsPhase();
	}

	public bool SelectPiston(Vector2 screenPos)
	{
		EnlargePistonColliders();
		BridgeEdge bridgeEdge = null;
		int num = Physics.RaycastNonAlloc(Cameras.MainCamera().ScreenPointToRay(screenPos), Utils.m_RaycastHits, float.MaxValue, Utils.EDGE_LAYER_MASK);
		for (int i = 0; i < num; i++)
		{
			bridgeEdge = Utils.m_RaycastHits[i].transform.parent.GetComponent<BridgeEdge>();
			if ((bool)bridgeEdge && bridgeEdge.IsPiston())
			{
				break;
			}
		}
		ShrinkPistonColliders();
		if ((bool)bridgeEdge && bridgeEdge.IsPiston())
		{
			HydraulicsPhase selectedHydraulicsPhase = GetSelectedHydraulicsPhase();
			Piston pistonOnEdge = Pistons.GetPistonOnEdge(bridgeEdge);
			HydraulicsController.TogglePiston(selectedHydraulicsPhase, pistonOnEdge);
			return true;
		}
		return false;
	}

	public BridgeJoint SelectSplitJoint(Vector2 screenPos)
	{
		BridgeJoint bridgeJoint = null;
		Collider closestRaycastHit = Utils.GetClosestRaycastHit(screenPos, Utils.JOINT_HOTSPOT_LAYER_MASK);
		if ((bool)closestRaycastHit)
		{
			bridgeJoint = closestRaycastHit.transform.parent.GetComponent<BridgeJoint>();
		}
		if (!bridgeJoint || !bridgeJoint.m_IsSplit)
		{
			return bridgeJoint;
		}
		if (!bridgeJoint.m_Split3.activeInHierarchy)
		{
			HydraulicsController.ToggleSplitJoint(GetSelectedHydraulicsPhase(), bridgeJoint);
			return bridgeJoint;
		}
		ProcessThreeWayJointClick(bridgeJoint, screenPos);
		return bridgeJoint;
	}

	public EventUnit GetHoverIcon()
	{
		if (m_PointerEventData == null)
		{
			return null;
		}
		m_PointerEventData.position = GameInput.GetMousePosition();
		m_RaycastResults.Clear();
		GameUI.m_Instance.m_Raycaster.Raycast(m_PointerEventData, m_RaycastResults);
		foreach (RaycastResult raycastResult in m_RaycastResults)
		{
			if ((bool)raycastResult.gameObject.transform.parent && (bool)raycastResult.gameObject.transform.parent.GetComponent<EventUnit>())
			{
				return raycastResult.gameObject.transform.parent.GetComponent<EventUnit>();
			}
		}
		return null;
	}

	public bool IsDraggingScrollbar()
	{
		return m_IsDraggingScrollbar;
	}

	public void UpdateStageOffIconForAllPhases()
	{
		foreach (HydraulicsControllerPhase controllerPhase in HydraulicsController.m_ControllerPhases)
		{
			if (controllerPhase != null && controllerPhase.m_HydraulicsPhase != null)
			{
				EventStage stageWithUnit = EventTimelines.GetStageWithUnit(controllerPhase.m_HydraulicsPhase.gameObject);
				if (stageWithUnit != null)
				{
					m_Stages.EnableOffIconForStage(stageWithUnit, controllerPhase.m_DisableNewAdditions);
				}
			}
		}
	}

	public void ProcessClick(Vector2 mouseScreenPos)
	{
		if (BridgeJointSelectors.CycleUnderMouse(mouseScreenPos, forward: true) || m_SelectedPhase == null)
		{
			return;
		}
		bool flag = GameUI.m_Instance.m_HydraulicsController.m_Locked;
		if (Game.IsCurrentLevelTutorial() && CampaignTutorial.m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_DISABLE_HYDRO)
		{
			flag = false;
		}
		if (!flag)
		{
			BridgeJoint bridgeJoint = GameUI.m_Instance.m_HydraulicsController.SelectSplitJoint(GameInput.GetMousePosition());
			if (!bridgeJoint || !bridgeJoint.m_IsSplit)
			{
				GameUI.m_Instance.m_HydraulicsController.SelectPiston(GameInput.GetMousePosition());
			}
		}
	}

	private void UpdateScrollbarState()
	{
		if (Mathf.Abs(m_ContentRectTransform.anchoredPosition.y - m_ContentLastY) > 0.001f)
		{
			m_IsDraggingScrollbar = true;
		}
		if (Mathf.Abs(m_ContentRectTransform.anchoredPosition.x - m_ContentLastX) > 0.001f)
		{
			m_IsDraggingScrollbar = true;
		}
		m_ContentLastX = m_ContentRectTransform.anchoredPosition.x;
		m_ContentLastY = m_ContentRectTransform.anchoredPosition.y;
		if (m_IsDraggingScrollbar && GameInput.GetMouseButtonJustReleased(0))
		{
			m_IsDraggingScrollbar = false;
		}
	}

	private void ProcessInput()
	{
		if (GameInput.GetMouseButtonJustPressed(0) && (bool)m_HoverIcon && (bool)m_HoverIcon.GetHydraulicsPhase())
		{
			SelectPhase(m_HoverIcon);
			InterfaceAudio.Play("ui_build_hydraulic_select");
		}
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && !Game.IsCurrentLevelTutorial() && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			Close();
		}
	}

	private void UpdateHover()
	{
		EventUnit hoverIcon = GetHoverIcon();
		if ((!hoverIcon || (bool)hoverIcon.GetHydraulicsPhase() || (bool)hoverIcon.GetVehicle()) && hoverIcon != m_HoverIcon)
		{
			if ((bool)m_HoverIcon)
			{
				m_HoverIcon.UnHover();
			}
			m_HoverIcon = hoverIcon;
			if ((bool)m_HoverIcon)
			{
				m_HoverIcon.Hover();
			}
		}
	}

	private void EnlargePistonColliders()
	{
		foreach (Piston piston in Pistons.m_Pistons)
		{
			piston.m_EdgeCollider.size = new Vector3(piston.m_EdgeCollider.size.x, piston.m_EdgeCollider.size.y * ENLARGE_PISTON_COLLIDER_FACTOR, piston.m_EdgeCollider.size.z);
		}
	}

	private void ShrinkPistonColliders()
	{
		foreach (Piston piston in Pistons.m_Pistons)
		{
			piston.m_EdgeCollider.size = new Vector3(piston.m_EdgeCollider.size.x, piston.m_EdgeCollider.size.y / ENLARGE_PISTON_COLLIDER_FACTOR, piston.m_EdgeCollider.size.z);
		}
	}

	private void SelectFirstPhase()
	{
		EventUnit firstHydraulicsPhase = m_Stages.GetFirstHydraulicsPhase();
		if ((bool)firstHydraulicsPhase)
		{
			SelectPhase(firstHydraulicsPhase);
		}
	}

	private void SelectPhase(EventUnit unit)
	{
		if (m_SelectedPhase == unit)
		{
			if ((bool)m_SelectedPhase)
			{
				m_SelectedPhase.Select();
			}
			return;
		}
		if ((bool)m_SelectedPhase)
		{
			m_SelectedPhase.DeSelect();
		}
		m_SelectedPhase = unit;
		if ((bool)m_SelectedPhase)
		{
			m_SelectedPhase.Select();
		}
	}

	private void Close()
	{
		InterfaceAudio.Play("ui_window_close");
		base.gameObject.SetActive(value: false);
	}

	private void ProcessThreeWayJointClick(BridgeJoint clickedJoint, Vector2 screenPos)
	{
		if (m_SelectedPhase == null)
		{
			return;
		}
		SplitJointNumber splitJointNumber = BridgeJointSelectors.SplitJointNumberUnderMouse(screenPos);
		if (!splitJointNumber || (!clickedJoint.IsThreeWaySplitJoint() && !clickedJoint.TwoWayShouldFunctionAsThreeWay()))
		{
			HydraulicsController.ToggleSplitJoint(GetSelectedHydraulicsPhase(), clickedJoint);
			return;
		}
		SplitJointState splitJointState = clickedJoint.m_SplitJointState;
		clickedJoint.SelectSplitPart(splitJointNumber.m_SplitJointPart);
		HydraulicsController.SetSplitJointStateForPhase(m_SelectedPhase.GetHydraulicsPhase(), clickedJoint, clickedJoint.m_SplitJointState);
		if (clickedJoint.m_SplitJointState == SplitJointState.NONE_SPLIT)
		{
			HydraulicsController.RemoveSplitJointFromPhase(clickedJoint, splitJointState, m_SelectedPhase.GetHydraulicsPhase());
			InterfaceAudio.Play("ui_build_splitJoint_remove");
		}
		else if (!HydraulicsController.PhaseAffectsSplitJoint(GetSelectedHydraulicsPhase(), clickedJoint))
		{
			HydraulicsController.AddSplitJointToPhase(clickedJoint, GetSelectedHydraulicsPhase());
			InterfaceAudio.Play("ui_build_splitJoint_create");
		}
		else if (splitJointState != clickedJoint.m_SplitJointState)
		{
			RecordSplitJointStateChangeForUndo(m_SelectedPhase.GetHydraulicsPhase(), clickedJoint, splitJointState);
			InterfaceAudio.Play("ui_build_splitJoint_create");
		}
	}

	private void OnThreeWayJointsToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		SandboxSettings.m_ThreeWaySplitJointsEnabled = m_ThreeWayJointsToggle.isOn;
		Profiles.SaveActiveProfile();
	}

	private void RecordRemoveAllForUndo(HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase == null || (hydraulicsControllerPhase.IsEmpty() && hydraulicsControllerPhase.m_DisableNewAdditions))
		{
			return;
		}
		BridgeActions.StartRecording();
		foreach (BridgeSplitJoint splitJoint in hydraulicsControllerPhase.m_SplitJoints)
		{
			BridgeActions.HydraulicsControllerRemoveSplitJoint(hydraulicsPhase, splitJoint.m_BridgeJoint, splitJoint.m_SplitJointState);
		}
		foreach (Piston piston in hydraulicsControllerPhase.m_Pistons)
		{
			BridgeActions.HydraulicsControllerRemovePiston(hydraulicsPhase, piston);
		}
		if (!hydraulicsControllerPhase.m_DisableNewAdditions)
		{
			BridgeActions.HydraulicsControllerDisableNewAdditions(hydraulicsPhase);
		}
		BridgeActions.FlushRecording();
	}

	private void RecordAddAllForUndo(HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase == null)
		{
			return;
		}
		int num = 0;
		foreach (Piston piston in Pistons.m_Pistons)
		{
			if (!hydraulicsControllerPhase.m_Pistons.Contains(piston))
			{
				num++;
			}
		}
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (!joint.m_IsSplit || !joint.gameObject.activeInHierarchy)
			{
				continue;
			}
			if (!hydraulicsControllerPhase.AffectsSplitJoint(joint))
			{
				num++;
				continue;
			}
			BridgeSplitJoint bridgeSplitJoint = hydraulicsControllerPhase.GetBridgeSplitJoint(joint);
			if (bridgeSplitJoint != null && bridgeSplitJoint.m_SplitJointState != SplitJointState.ALL_SPLIT)
			{
				num++;
			}
		}
		if (num == 0 && !hydraulicsControllerPhase.m_DisableNewAdditions)
		{
			return;
		}
		BridgeActions.StartRecording();
		foreach (Piston piston2 in Pistons.m_Pistons)
		{
			if (!hydraulicsControllerPhase.m_Pistons.Contains(piston2))
			{
				BridgeActions.HydraulicsControllerAddPiston(hydraulicsPhase, piston2);
			}
		}
		foreach (BridgeJoint joint2 in BridgeJoints.m_Joints)
		{
			if (!joint2.m_IsSplit || !joint2.gameObject.activeInHierarchy)
			{
				continue;
			}
			if (!hydraulicsControllerPhase.AffectsSplitJoint(joint2))
			{
				BridgeActions.HydraulicsControllerAddSplitJoint(hydraulicsPhase, joint2);
				continue;
			}
			BridgeSplitJoint bridgeSplitJoint2 = hydraulicsControllerPhase.GetBridgeSplitJoint(joint2);
			if (bridgeSplitJoint2 != null && bridgeSplitJoint2.m_SplitJointState != SplitJointState.ALL_SPLIT)
			{
				BridgeActions.HydraulicsControllerChangeSplitState(hydraulicsPhase, joint2, SplitJointState.ALL_SPLIT);
			}
		}
		if (hydraulicsControllerPhase.m_DisableNewAdditions)
		{
			BridgeActions.HydraulicsControllerEnableNewAdditions(hydraulicsPhase);
		}
		BridgeActions.FlushRecording();
	}

	private void RecordSplitJointStateChangeForUndo(HydraulicsPhase hydraulicsPhase, BridgeJoint joint, SplitJointState prevState)
	{
		BridgeActions.StartRecording();
		BridgeActions.HydraulicsControllerChangeSplitState(hydraulicsPhase, joint, prevState);
		BridgeActions.FlushRecording();
	}

	private void UpdatePanelDimensions()
	{
		float num = Mathf.Abs(m_LevelInfoLite.m_Panel.sizeDelta.x - (float)m_LevelInfoLite.m_MinPanelWidth);
		float num2 = Mathf.Abs(m_LevelInfoLite.m_Panel.sizeDelta.y - (float)m_LevelInfoLite.m_MinPanelHeight);
		m_Panel.sizeDelta = new Vector2(420f + num, 192f + num2);
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		if (Game.IsCurrentLevelTutorial())
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
		}
	}
}
