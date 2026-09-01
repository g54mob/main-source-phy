using UnityEngine;
using UnityEngine.UI;

public class Panel_BuildToolBar : MonoBehaviour
{
	[Header("Buttons Top")]
	public Button m_SelectButton;

	public Button m_MoveButton;

	public Button m_EraseButton;

	public Button m_TraceButton;

	[Header("Buttons Middle")]
	public Button m_GridButton;

	public Button m_GridSelectedButton;

	public Button m_AutoTriangulateButton;

	public Button m_AutoTriangulateSelectedButton;

	public GameObject m_SnapContainer;

	public Button m_SnapButton;

	public Button m_SnapSelectedButton;

	public Button m_EdgeBisectButton;

	public Button m_EdgeBisectSelectedButton;

	public Button m_AutoDrawButton;

	public Button m_AutoDrawSelectedButton;

	[Header("Buttons Bottom")]
	public Button m_TrashButton;

	public Button m_UndoButton;

	public Button m_RedoButton;

	[Header("Icons")]
	public Image m_SelectIcon;

	public Image m_MoveIcon;

	public Image m_EraseIcon;

	[Header("HelpArrows")]
	public GameObject m_AutoTriangulateHelpArrow;

	[Header("Trace")]
	public Panel_TraceTool m_TraceToolPanel;

	private readonly float TRACE_TOOL_PANEL_Y_MOUSE = -88.6f;

	private readonly float TRACE_TOOL_PANEL_Y_GAMEPAD = -8.6f;

	private void Start()
	{
		m_SelectButton.onClick.AddListener(OnSelect);
		m_MoveButton.onClick.AddListener(OnMove);
		m_EraseButton.onClick.AddListener(OnErase);
		m_TraceButton.onClick.AddListener(OnTraceTool);
		m_GridButton.onClick.AddListener(OnGrid);
		m_GridSelectedButton.onClick.AddListener(OnGridSelected);
		m_AutoTriangulateButton.onClick.AddListener(OnAutoTriangulate);
		m_AutoTriangulateSelectedButton.onClick.AddListener(OnAutoTriangulateSelected);
		m_SnapButton.onClick.AddListener(OnSnap);
		m_SnapSelectedButton.onClick.AddListener(OnSnapSelected);
		m_EdgeBisectButton.onClick.AddListener(OnEdgeBisect);
		m_EdgeBisectSelectedButton.onClick.AddListener(OnEdgeBisectSelected);
		m_AutoDrawButton.onClick.AddListener(OnAutoDraw);
		m_AutoDrawSelectedButton.onClick.AddListener(OnAutoDrawSelected);
		m_TrashButton.onClick.AddListener(OnClear);
		m_UndoButton.onClick.AddListener(OnUndo);
		m_RedoButton.onClick.AddListener(OnRedo);
	}

	private void OnEnable()
	{
		m_SelectIcon.color = Color.white;
		m_MoveIcon.color = Color.white;
		m_EraseIcon.color = Color.white;
	}

	private void OnDisable()
	{
		m_AutoTriangulateHelpArrow.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		UpdateBuildUndoRedoButtons();
		UpdateAutoTriangulateHelpArrow();
	}

	public void UpdateForCurrentDevice()
	{
		m_MoveButton.gameObject.SetActive(GameInput.GetActiveGameDevice() != GameDevice.Gamepad);
		m_EraseButton.gameObject.SetActive(GameInput.GetActiveGameDevice() != GameDevice.Gamepad);
		m_SnapContainer.SetActive(GameInput.GetActiveGameDevice() == GameDevice.Gamepad);
		RectTransform component = m_TraceToolPanel.GetComponent<RectTransform>();
		component.anchoredPosition = new Vector2(component.anchoredPosition.x, (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? TRACE_TOOL_PANEL_Y_GAMEPAD : TRACE_TOOL_PANEL_Y_MOUSE);
	}

	public void SetGameToolIconsInteractive(bool interactable)
	{
		m_SelectButton.interactable = interactable;
		m_MoveButton.interactable = interactable;
		m_EraseButton.interactable = interactable;
		m_TraceButton.interactable = interactable;
	}

	public void UpdateGameToolModeIcons(GameToolModeType mode)
	{
		m_SelectIcon.color = ((mode == GameToolModeType.SELECT) ? GameUI.m_Instance.m_GoldColor : Color.white);
		m_MoveIcon.color = ((mode == GameToolModeType.MOVE) ? GameUI.m_Instance.m_GoldColor : Color.white);
		m_EraseIcon.color = ((mode == GameToolModeType.ERASE) ? GameUI.m_Instance.m_GoldColor : Color.white);
	}

	public void OnSelect()
	{
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		if (GameToolMode.GetMode() == GameToolModeType.SELECT)
		{
			GameToolMode.SelectModeActivate(on: false);
			BridgeSelectionSet.CancelSelection();
		}
		else
		{
			GameToolMode.SelectModeActivate(on: true);
		}
	}

	public void OnMove()
	{
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		if (GameToolMode.GetMode() == GameToolModeType.MOVE)
		{
			GameToolMode.MoveModeActivate(on: false);
		}
		else
		{
			GameToolMode.MoveModeActivate(on: true);
		}
	}

	public void OnErase()
	{
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		if (GameToolMode.GetMode() == GameToolModeType.ERASE)
		{
			GameToolMode.EraseModeActivate(on: false);
		}
		else
		{
			GameToolMode.EraseModeActivate(on: true);
		}
	}

	public void OnTraceTool()
	{
		if (!BridgeTrace.IsTracingActive())
		{
			BridgeTrace.TurnOnTracing();
			BridgeJointPlacement.CancelSelection();
			GameToolMode.SetMode(GameToolModeType.BUILD);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
		else
		{
			BridgeTrace.TurnOffTracing();
			InterfaceAudio.Play("ui_menubar_gen_off");
		}
	}

	public void OnGrid()
	{
		OnGridSilent();
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnGridSilent()
	{
		Profiles.m_ActiveProfile.m_GridEnabled = true;
		Profiles.SaveActiveProfile();
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			GameGrid.m_Grid.SetActive(value: true);
		}
		m_GridButton.gameObject.SetActive(value: false);
		m_GridSelectedButton.gameObject.SetActive(value: true);
	}

	public void OnGridSelected()
	{
		OnGridSelectedSilent();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void OnGridSelectedSilent()
	{
		Profiles.m_ActiveProfile.m_GridEnabled = false;
		Profiles.SaveActiveProfile();
		GameGrid.m_Grid.SetActive(value: false);
		m_GridButton.gameObject.SetActive(value: true);
		m_GridSelectedButton.gameObject.SetActive(value: false);
	}

	public void OnAutoTriangulate()
	{
		OnAutoTriangulateSilent();
		if (m_AutoTriangulateHelpArrow.activeInHierarchy)
		{
			Profiles.m_ActiveProfile.m_DismissedAutoTriangulateHelpArrow = true;
			Profiles.SaveActiveProfile();
		}
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnAutoTriangulateSilent()
	{
		m_AutoTriangulateButton.gameObject.SetActive(value: false);
		m_AutoTriangulateSelectedButton.gameObject.SetActive(value: true);
		Profiles.m_ActiveProfile.m_AutoTriangulateEnabled = true;
		Profiles.SaveActiveProfile();
	}

	public void OnAutoTriangulateSelected()
	{
		OnAutoTriangulateSelectedSilent();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void OnAutoTriangulateSelectedSilent()
	{
		m_AutoTriangulateButton.gameObject.SetActive(value: true);
		m_AutoTriangulateSelectedButton.gameObject.SetActive(value: false);
		Profiles.m_ActiveProfile.m_AutoTriangulateEnabled = false;
		Profiles.SaveActiveProfile();
	}

	public void OnSnap()
	{
		OnSnapSilent();
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnSnapSilent()
	{
		m_SnapButton.gameObject.SetActive(value: false);
		m_SnapSelectedButton.gameObject.SetActive(value: true);
		Profiles.m_ActiveProfile.m_SnapEnabled = true;
		Profiles.SaveActiveProfile();
	}

	public void OnSnapSelected()
	{
		OnSnapSelectedSilent();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void OnSnapSelectedSilent()
	{
		m_SnapButton.gameObject.SetActive(value: true);
		m_SnapSelectedButton.gameObject.SetActive(value: false);
		Profiles.m_ActiveProfile.m_SnapEnabled = false;
		Profiles.SaveActiveProfile();
	}

	public void OnAutoDraw()
	{
		OnAutoDrawSilent();
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnAutoDrawSilent()
	{
		m_AutoDrawButton.gameObject.SetActive(value: false);
		m_AutoDrawSelectedButton.gameObject.SetActive(value: true);
		Profiles.m_ActiveProfile.m_AutoDrawEnabled = true;
		Profiles.SaveActiveProfile();
	}

	public void OnAutoDrawSelected()
	{
		OnAutoDrawSelectedSilent();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void OnAutoDrawSelectedSilent()
	{
		m_AutoDrawButton.gameObject.SetActive(value: true);
		m_AutoDrawSelectedButton.gameObject.SetActive(value: false);
		Profiles.m_ActiveProfile.m_AutoDrawEnabled = false;
		Profiles.SaveActiveProfile();
	}

	public void OnEdgeBisect()
	{
		OnEdgeBisectSilent();
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnEdgeBisectSilent()
	{
		m_EdgeBisectButton.gameObject.SetActive(value: false);
		m_EdgeBisectSelectedButton.gameObject.SetActive(value: true);
		Profiles.m_ActiveProfile.m_EdgeBisectEnabled = true;
		Profiles.SaveActiveProfile();
	}

	public void OnEdgeBisectSelected()
	{
		OnEdgeBisectSelectedSilent();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void OnEdgeBisectSelectedSilent()
	{
		m_EdgeBisectButton.gameObject.SetActive(value: true);
		m_EdgeBisectSelectedButton.gameObject.SetActive(value: false);
		Profiles.m_ActiveProfile.m_EdgeBisectEnabled = false;
		Profiles.SaveActiveProfile();
	}

	public void UpdateBuildUndoRedoButtons()
	{
		if (!Game.IsCurrentLevelTutorial())
		{
			m_UndoButton.interactable = BridgeUndo.CanUndo();
			m_RedoButton.interactable = BridgeRedo.CanRedo();
		}
	}

	public void OnUndo()
	{
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			if (BridgeTrace.IsFilling() || (bool)BridgeJointMovement.m_SelectedJoint)
			{
				InterfaceAudio.PlayErrorBeep();
				return;
			}
			if (!BridgeUndo.CanUndo())
			{
				InterfaceAudio.PlayErrorBeep();
				return;
			}
			Bridge.CancelSelection();
			BridgeUndo.Undo();
			BridgeTrace.m_JustFilled = false;
			InterfaceAudio.Play("ui_build_undo");
		}
		else if (!SandboxUndo.CanUndo())
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			EventEditor.DestroyPendingStage();
			SandboxSelectionSet.RevertSelectionSetToStartPositions();
			SandboxSelectionSet.CancelSelection();
			SandboxUndo.PrevSnapShot();
			InterfaceAudio.Play("ui_build_undo");
		}
	}

	public void OnRedo()
	{
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			if (BridgeTrace.IsFilling() || (bool)BridgeJointMovement.m_SelectedJoint)
			{
				InterfaceAudio.PlayErrorBeep();
				return;
			}
			if (!BridgeRedo.CanRedo())
			{
				InterfaceAudio.PlayErrorBeep();
				return;
			}
			Bridge.CancelSelection();
			BridgeRedo.Redo();
			InterfaceAudio.Play("ui_build_undo");
		}
		else if (!SandboxUndo.CanRedo())
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			EventEditor.DestroyPendingStage();
			SandboxSelectionSet.RevertSelectionSetToStartPositions();
			SandboxSelectionSet.CancelSelection();
			SandboxUndo.NextSnapShot();
			InterfaceAudio.Play("ui_build_undo");
		}
	}

	public void OnClear()
	{
		if (GameStateManager.GetState() == GameState.SIM || GameStateManager.GetPendingState() == GameState.SIM)
		{
			return;
		}
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			if (Sandbox.m_CurrentLayoutData.m_Bridge != null && Sandbox.m_CurrentLayoutData.m_Bridge.HasPrebuilts())
			{
				InterfaceAudio.Play("ui_menu_select");
				PopUpMessage.DisplayWarning(Localize.Get("POPUP_CLEAR_CONFIRM_PREBUILTS"), useYesNoLables: true, DoClear);
				return;
			}
			if (ClearWillChangeLevel())
			{
				InterfaceAudio.Play("ui_menu_select");
				PopUpMessage.DisplayWarning(Localize.Get("POPUP_CLEAR_CONFIRM"), useYesNoLables: true, DoClear);
				return;
			}
		}
		if (GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.DECOR)
		{
			InterfaceAudio.Play("ui_menu_select");
			PopUpMessage.DisplayWarning(Localize.Get("POPUP_NEW_SANDBOX_CONFIRM"), useYesNoLables: true, StartNewSandboxLayout);
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void DoClear()
	{
		HydraulicsController.Reset();
		Bridge.Clear();
		BridgeJoints.UnSplitAllJoints();
		SandboxLayout.DeserializeBridge(Sandbox.m_CurrentLayoutData.m_Bridge);
		Bridge.DestroyAllExceptPrebuilt();
		BridgeJoints.MakeDefaultColor();
		GameGrid.CenterOnTerrainEdge(TerrainIslands.GetLeftTerrain());
		GameUI.m_Instance.m_HydraulicsController.gameObject.SetActive(value: false);
		BridgeCheat.m_Cheated = Budget.m_UsingForcedUnlimitedBudget || Budget.m_UsingForcedUnlimitedMaterial;
	}

	private bool ClearWillChangeLevel()
	{
		if (BridgeJoints.GetNumActiveNonAnchorJoints() > 0 || BridgeEdges.GetNumActiveEdges() > 0 || BridgeTrace.IsTraceLinePlaced() || BridgeJoints.GetNumSplitJoints() > 0)
		{
			return true;
		}
		if (BridgePillars.GetNumActivePillars() > 0)
		{
			return true;
		}
		if (HydraulicsController.HasDataToClear())
		{
			return true;
		}
		return false;
	}

	private void StartNewSandboxLayout()
	{
		GameStateManager.SwitchToState(GameState.SANDBOX);
		Sandbox.StartNewSandbox(Theme.m_Instance.m_ThemeStub.m_ID);
	}

	private void UpdateAutoTriangulateHelpArrow()
	{
		bool active = GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null && Campaign.m_CurrentLevel.m_Id == "002" && !GameUI.m_Instance.m_LevelInfo.gameObject.activeInHierarchy && !Profiles.m_ActiveProfile.m_DismissedAutoTriangulateHelpArrow && Bridge.m_BuildMaterialType == BridgeMaterialType.WOOD && m_AutoTriangulateButton.gameObject.activeInHierarchy;
		m_AutoTriangulateHelpArrow.SetActive(active);
	}
}
