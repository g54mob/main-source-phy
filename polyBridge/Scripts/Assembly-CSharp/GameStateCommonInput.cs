using Poly.Game;
using UnityEngine;

public class GameStateCommonInput
{
	public static bool m_DebugPan;

	public static bool m_PanDisabledWaitingForButtonUp;

	public static float ZOOM_INTERVAL_SECS = 0.125f;

	private static Vector3 m_InitialClickPositionForPan;

	internal static bool m_RefreshClickPositionForPan = true;

	private static float ZOOM_HOLD_INTERVAL_SECS = 0.15f;

	private static float m_NextAllowedZoomInTime = 0f;

	private static float m_NextAllowedZoomOutTime = 0f;

	private static float m_SimSpeedHeldDownTimeSeconds;

	private static float m_SimSpeedNextTickTime;

	private static Vector3 m_StartCameraPosZoom;

	private static Vector3 m_TargetCameraPosZoom;

	private static float m_StartOrthoSize;

	private static float m_TargetOrthoSize;

	private static float m_ElapsedOrthoSeconds;

	private static bool m_InterpolateToTargetOrthoSize;

	public static void StopZooming()
	{
		m_InterpolateToTargetOrthoSize = false;
	}

	public static void Process()
	{
		DoKeyboardProcessing();
		if (AllowedToPanCamera(withMouse: false) && GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			PanWithGamepad();
		}
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			if (GamepadManager.ButtonIsDown(GamepadButtonType.TRIGGER_LEFT))
			{
				DoMouseScrollWheel(0f - Time.unscaledDeltaTime);
			}
			else if (GamepadManager.ButtonIsDown(GamepadButtonType.TRIGGER_RIGHT))
			{
				DoMouseScrollWheel(Time.unscaledDeltaTime);
			}
		}
		else if (!GameInput.MousePointerOutsideGame())
		{
			DoMouseScrollWheel(Input.GetAxis("Mouse ScrollWheel"));
		}
		if ((!GameInput.IsDown(BindingType.DRAW_BUILD) || GameToolMode.GetMode() != GameToolModeType.BUILD) && !GameInput.IsDown(BindingType.PAN_WITH_MOUSE))
		{
			m_PanDisabledWaitingForButtonUp = false;
		}
		if (ShouldPan())
		{
			if (m_RefreshClickPositionForPan)
			{
				m_InitialClickPositionForPan = Cameras.MainCamera().ScreenToWorldPoint(GameInput.GetMousePosition());
				m_RefreshClickPositionForPan = false;
			}
			PanWithMouse();
		}
		else
		{
			m_RefreshClickPositionForPan = true;
		}
		UpdateToDesiredZoom();
	}

	public static bool ShouldPan()
	{
		if (AllowedToPanCamera(withMouse: true) && GameStateManager.AllowedToPanCameraWithMouse())
		{
			if (!GameInput.IsDown(BindingType.DRAW_BUILD) || GameToolMode.GetMode() != GameToolModeType.BUILD)
			{
				return GameInput.IsDown(BindingType.PAN_WITH_MOUSE);
			}
			return true;
		}
		return false;
	}

	public static void DisableMousePanIfButtonDown()
	{
		if (GameInput.IsDown(BindingType.DRAW_BUILD) || GameInput.IsDown(BindingType.PAN_WITH_MOUSE))
		{
			m_PanDisabledWaitingForButtonUp = true;
		}
	}

	public static bool IgnoreKeyboardInputForPanel(GameObject panel)
	{
		if (!ActivePanels.IsTopPanel(panel))
		{
			return true;
		}
		if (GameStateSandbox.m_CameraInTransition || GameStateBuild.m_CameraInTransition || GameStateSim.m_CameraInTransition)
		{
			return true;
		}
		return uConsole.IsOn();
	}

	public static bool IgnoreKeyboardInput()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && GamepadVirtualKeyboard.m_Active)
		{
			return true;
		}
		if (GameStateSandbox.m_CameraInTransition || GameStateBuild.m_CameraInTransition || GameStateSim.m_CameraInTransition)
		{
			return true;
		}
		if (GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy)
		{
			return true;
		}
		if (GameUI.m_Instance.m_LevelInfo.gameObject.activeInHierarchy)
		{
			return true;
		}
		if (GameUI.m_Instance.m_Workshop.gameObject.activeInHierarchy)
		{
			return true;
		}
		if (GameUI.m_Instance.m_LevelComplete.gameObject.activeInHierarchy)
		{
			return true;
		}
		if (GameUI.m_Instance.m_LevelFailed.gameObject.activeInHierarchy)
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_Status.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_PopUpMessage.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_PopUpMessage.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_PopUpInputField.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_PopUpTwoChoices.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_PopUpBinding.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_WorkshopSubmit.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_CustomShapeReset.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_ShareReplay.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_ShareReplayStatus.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_LoadSandboxLayout.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_CustomShapesLibrary.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_SaveSandboxLayout.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_LoadBridge.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_SaveBridge.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_Help.gameObject))
		{
			return true;
		}
		if (ActivePanels.IsTopPanel(GameUI.m_Instance.m_GamepadHelp.gameObject))
		{
			return true;
		}
		if (SandboxInputFields.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShape.ColorPickerHasInputFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxMultiSelect.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditAnchor.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShape.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditFlyingObject.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditCheckpoint.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditPlatform.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditRamp.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditRock.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditPillar.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditTerrain.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditVehicle.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditBuildZone.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxEditDecor.m_SandboxNudge.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_ProfileSelect.m_ProfileEdit.InputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.m_Instance.m_SandboxMenu.m_SandboxTabsPanel.SearchInputFieldHasFocus())
		{
			return true;
		}
		if (GameUI.IsScreenDucked())
		{
			return true;
		}
		return uConsole.IsOn();
	}

	public static void ProcessEscapeKeypress()
	{
		if (GameStateManager.GetPendingState() != GameState.INVALID)
		{
			return;
		}
		if (GameUI.m_Instance.m_Selection.gameObject.activeInHierarchy)
		{
			BridgeSelectionSet.CancelSelection();
		}
		else if (BridgePillarPlacement.InPlacementMode())
		{
			BridgePillarPlacement.CancelPlacementAndSelectPreviousMaterial();
		}
		else
		{
			if (ActivePanels.m_Panels.Count != 0 && !CampaignTutorial.IsRunning())
			{
				return;
			}
			if ((bool)BridgeJointPlacement.m_SelectedJoint)
			{
				BridgeJointPlacement.CancelSelection();
				return;
			}
			if ((bool)BridgeJointMovement.m_SelectedJoint)
			{
				BridgeJointMovement.CancelSelection();
				return;
			}
			if (ClipboardManager.ReadyToPaste() && !CampaignTutorial.IsRunning())
			{
				ClipboardManager.ClearClipboard();
				return;
			}
			if (!GameUI.HudIsActive())
			{
				GameUI.EnableHud(on: true);
			}
			if (GameStateManager.GetPendingState() != GameState.SIM)
			{
				GameUI.m_Instance.m_PauseMenu.gameObject.SetActive(value: true);
				InterfaceAudio.Play("ui_window_open");
			}
		}
	}

	public static void ProcessSimSpeedInput()
	{
		if (GameInput.JustPressed(BindingType.DECREASE_SIM_SPEED))
		{
			GameUI.m_Instance.m_TopBar.OnSlower();
			m_SimSpeedHeldDownTimeSeconds = 0f;
			m_SimSpeedNextTickTime = 0f;
		}
		if (GameInput.JustPressed(BindingType.INCREASE_SIM_SPEED))
		{
			GameUI.m_Instance.m_TopBar.OnFaster();
			m_SimSpeedHeldDownTimeSeconds = 0f;
			m_SimSpeedNextTickTime = 0f;
		}
		UpdateSimSpeedContinuousHold();
	}

	public static bool AllowedToPanCamera(bool withMouse)
	{
		if (VehicleFollow.EnabledWithVehiclesInLevel() && !Profiles.m_ActiveProfile.m_LockBuildCamera && GameStateManager.GetState() == GameState.SIM)
		{
			return false;
		}
		if (GameUI.m_Instance.m_TopBar.m_BridgeSimSpeedSlider.IsDragging())
		{
			MaybeShowNoPanReason("NO PAN REASON: Dragging Bridge Sim Speed Slider");
			return false;
		}
		if (CampaignTutorial.IsRunning())
		{
			MaybeShowNoPanReason("NO PAN REASON: Tutorial Active");
			return false;
		}
		if (GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy)
		{
			MaybeShowNoPanReason("NO PAN REASON: Pause Menu active");
			return false;
		}
		if (withMouse)
		{
			if (GameUI.IsPointerOverGameObject())
			{
				MaybeShowNoPanReason("NO PAN REASON: Pointer over UI");
				return false;
			}
			if (m_PanDisabledWaitingForButtonUp)
			{
				MaybeShowNoPanReason("NO PAN REASON: Waiting for button up");
				return false;
			}
		}
		if ((bool)SandboxItems.m_NewUnPlacedItem)
		{
			MaybeShowNoPanReason("NO PAN REASON: Unplaced sandbox item");
			return false;
		}
		if (EventEditor.IsIconMoving() || EventEditor.IsPanning() || EventEditor.IsMovingCollapseBar())
		{
			MaybeShowNoPanReason("NO PAN REASON: Event Editor icon moving/panning or moving collapse bar");
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditRamp.IsMovingControlPoint())
		{
			MaybeShowNoPanReason("NO PAN REASON: Moving Spline Point");
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShapeTools.IsMovingVert() || GameUI.m_Instance.m_SandboxEditCustomShapeTools.IsMovingPin() || GameUI.m_Instance.m_SandboxEditCustomShapeTools.IsMovingAnchor())
		{
			MaybeShowNoPanReason("NO PAN REASON: Moving Custom Shape vert/pin/anchor");
			return false;
		}
		if (Pistons.MouseIsOverPistonSlider() || (bool)Pistons.m_SliderFollowingMouse)
		{
			MaybeShowNoPanReason("NO PAN REASON: moving piston slider");
			return false;
		}
		if (BridgeSprings.MouseIsOverSpringSlider() || (bool)BridgeSprings.m_SliderFollowingMouse)
		{
			MaybeShowNoPanReason("NO PAN REASON: moving spring slider");
			return false;
		}
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy && GameUI.m_Instance.m_HydraulicsController.IsDraggingScrollbar())
		{
			MaybeShowNoPanReason("NO PAN REASON: scrolling hydraulics controller");
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShape.gameObject.activeInHierarchy && GameUI.m_Instance.m_SandboxEditCustomShape.IsDraggingScrollbar())
		{
			MaybeShowNoPanReason("NO PAN REASON: scrolling custom shape");
			return false;
		}
		if (GameUI.m_Instance.m_PolyTwitchMain.gameObject.activeInHierarchy && GameUI.m_Instance.m_PolyTwitchMain.IsDraggingScrollbar())
		{
			MaybeShowNoPanReason("NO PAN REASON: dragging scrollbar polytwitch");
			return false;
		}
		if (GameUI.SaveLoadPanelIsActive() || GameUI.LevelEndPanelIsActive())
		{
			MaybeShowNoPanReason("NO PAN REASON: SaveLoad or LevelEnd panel active");
			return false;
		}
		if (GameUI.m_Instance.m_TraceTool.IsFillSliderScrolling())
		{
			MaybeShowNoPanReason("NO PAN REASON: Fill Slider Scrolling");
			return false;
		}
		if (GameUI.m_Instance.m_PolyTwitchMain.IsMoving() || GameUI.m_Instance.m_PolyTwitchMain.m_AuthorPanel.IsMoving())
		{
			MaybeShowNoPanReason("NO PAN REASON: Moving PolyTwitch window");
			return false;
		}
		if (GameUI.m_Instance.m_PolyTwitchMain.m_SettingsPanel.IsSliderScrolling() || GameUI.m_Instance.m_PolyTwitchMain.IsResizingPanel())
		{
			MaybeShowNoPanReason("NO PAN REASON: Resizing PolyTwitch window");
			return false;
		}
		if (BridgeTrace.IsDraggingHandles())
		{
			MaybeShowNoPanReason("NO PAN REASON: Dragging handles");
			return false;
		}
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			MaybeShowNoPanReason("NO PAN REASON: Gallery active");
			return false;
		}
		if (GameUI.m_Instance.m_Campaign.gameObject.activeInHierarchy)
		{
			MaybeShowNoPanReason("NO PAN REASON: campaign UI active");
			return false;
		}
		if (GameUI.PointerOver(typeof(Panel_Stages)))
		{
			MaybeShowNoPanReason("NO PAN REASON: over Panel_Stages");
			return false;
		}
		if (GameUI.m_Instance.m_LevelInfoLite.gameObject.activeInHierarchy && GameUI.m_Instance.m_LevelInfoLite.IsDraggingScrollbar())
		{
			MaybeShowNoPanReason("NO PAN REASON: scrolling level info lite");
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditBuildZone.gameObject.activeInHierarchy && GameUI.m_Instance.m_SandboxEditBuildZone.IsEditing())
		{
			MaybeShowNoPanReason("NO PAN REASON: Editing build zone");
			return false;
		}
		if (EventEditor.IsIconMoving() || EventEditor.IsStageMoving())
		{
			MaybeShowNoPanReason("NO PAN REASON: Event Editor icon or stage moving");
			return false;
		}
		return true;
	}

	private static void PanWithGamepad()
	{
		Vector2 rightStick = GamepadManager.m_RightStick;
		rightStick *= Time.unscaledDeltaTime * GamepadManager.GetCursorPanSpeed();
		if (GamepadManager.m_VirtualMouseUI.CursorPeggedToLeftSideOfScreen() && GamepadManager.m_LeftStick.x < -0.01f)
		{
			rightStick.x += GamepadManager.m_LeftStick.x * Time.unscaledDeltaTime * GamepadManager.GetCursorPanSpeed();
		}
		if (GamepadManager.m_VirtualMouseUI.CursorPeggedToRightSideOfScreen() && GamepadManager.m_LeftStick.x > 0.01f)
		{
			rightStick.x += GamepadManager.m_LeftStick.x * Time.unscaledDeltaTime * GamepadManager.GetCursorPanSpeed();
		}
		if (GamepadManager.m_VirtualMouseUI.CursorPeggedToBottomOfScreen() && GamepadManager.m_LeftStick.y < -0.01f)
		{
			rightStick.y += GamepadManager.m_LeftStick.y * Time.unscaledDeltaTime * GamepadManager.GetCursorPanSpeed();
		}
		if (GamepadManager.m_VirtualMouseUI.CursorPeggedToTopOfScreen() && GamepadManager.m_LeftStick.y > 0.01f)
		{
			rightStick.y += GamepadManager.m_LeftStick.y * Time.unscaledDeltaTime * GamepadManager.GetCursorPanSpeed();
		}
		Cameras.MainCamera().transform.Translate(rightStick);
		if (rightStick != Vector2.zero)
		{
			if (GameStateManager.GetState() == GameState.SIM && !Cameras.In2DMode())
			{
				GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CUSTOM);
			}
			PointsOfView.PanPivot(Cameras.MainCamera().ScreenToWorldPoint(GameInput.GetMousePosition()), force: true);
		}
	}

	private static void PanWithKeyboard()
	{
		Vector2 zero = Vector2.zero;
		if (GameInput.IsDown(BindingType.PAN_CAMERA_UP))
		{
			zero.y += GameSettings.PanCameraSpeedY();
		}
		if (GameInput.IsDown(BindingType.PAN_CAMERA_LEFT))
		{
			zero.x += 0f - GameSettings.PanCameraSpeedX();
		}
		if (GameInput.IsDown(BindingType.PAN_CAMERA_DOWN))
		{
			zero.y += 0f - GameSettings.PanCameraSpeedY();
		}
		if (GameInput.IsDown(BindingType.PAN_CAMERA_RIGHT))
		{
			zero.x += GameSettings.PanCameraSpeedX();
		}
		zero *= Time.unscaledDeltaTime * Profiles.m_ActiveProfile.m_CameraPanSpeedNormalized;
		Cameras.MainCamera().transform.Translate(zero);
		if (zero != Vector2.zero)
		{
			if (GameStateManager.GetState() == GameState.SIM && !Cameras.In2DMode())
			{
				GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CUSTOM);
			}
			PointsOfView.PanPivot(Cameras.MainCamera().ScreenToWorldPoint(GameInput.GetMousePosition()), force: true);
		}
	}

	private static void MaybeShowNoPanReason(string text)
	{
		if (m_DebugPan)
		{
			Debug.LogFormat(text);
		}
	}

	private static bool AllowedToZoomCamera()
	{
		if (CampaignTutorial.IsRunning())
		{
			return false;
		}
		if (GameUI.m_Instance.m_LevelInfo.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameUI.SaveLoadPanelIsActive() || GameUI.LevelEndPanelIsActive())
		{
			return false;
		}
		if (GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameUI.m_Instance.m_Settings.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameUI.m_Instance.m_ProfileSelect.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (EventEditor.PointerOverEventStages())
		{
			return false;
		}
		if (EventEditor.PointerOverEventObjects())
		{
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShape.HasScrollFocus())
		{
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShape.CustomShapeTextureDropDownHasScrollFocus())
		{
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditVehicle.VehicleTypeDropDownHasScrollFocus())
		{
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditZedAxisVehicle.VehicleTypeDropDownHasScrollFocus())
		{
			return false;
		}
		if (GameUI.m_Instance.m_WorkshopSubmit.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameUI.m_Instance.m_CustomShapeReset.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameUI.m_Instance.m_PolyTwitchMain.IsHovering())
		{
			return false;
		}
		if (GameUI.m_Instance.m_Campaign.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameUI.PointerOver(typeof(Panel_SandboxMenu)))
		{
			return false;
		}
		if (GameUI.PointerOver(typeof(Panel_Stages)))
		{
			return false;
		}
		if (GameUI.m_Instance.m_Workshop.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameUI.m_Instance.m_WeeklyChallenges.gameObject.activeInHierarchy)
		{
			return false;
		}
		return true;
	}

	private static void DoMouseScrollWheel(float delta)
	{
		if (!Mathf.Approximately(delta, 0f) && AllowedToZoomCamera())
		{
			DoZoom(delta);
		}
	}

	private static float ApplyScalingFromPolyBridge1(float increment, float currentSize)
	{
		increment = ((GameInput.GetActiveGameDevice() != GameDevice.Gamepad) ? (increment * 6f) : (increment * 6f));
		increment *= currentSize / 4f;
		return increment;
	}

	private static void UpdateSimSpeedContinuousHold()
	{
		m_SimSpeedHeldDownTimeSeconds += Time.unscaledDeltaTime;
		m_SimSpeedNextTickTime += Time.unscaledDeltaTime;
		if (m_SimSpeedHeldDownTimeSeconds > 0.3f && m_SimSpeedNextTickTime > 0.1f)
		{
			if (GameInput.IsDown(BindingType.DECREASE_SIM_SPEED))
			{
				GameUI.m_Instance.m_TopBar.OnSlower();
			}
			if (GameInput.IsDown(BindingType.INCREASE_SIM_SPEED))
			{
				GameUI.m_Instance.m_TopBar.OnFaster();
			}
			m_SimSpeedNextTickTime = Mathf.Min(m_SimSpeedNextTickTime - 0.1f, 0.1f);
		}
	}

	private static void DoKeyboardProcessing()
	{
		if (AllowedToPanCamera(withMouse: false) && !uConsole.IsOn() && SandboxSelectionSet.IsEmpty())
		{
			PanWithKeyboard();
		}
		bool flag = GameInput.GetActiveGameDevice() == GameDevice.Gamepad && GameStateManager.GetState() != GameState.SIM;
		if (GameInput.JustPressed(BindingType.PAUSE_ON_BREAK) && !Input.GetKey(KeyCode.LeftControl) && !flag)
		{
			if (Profiles.m_ActiveProfile.m_PauseOnBreak)
			{
				GameUI.m_Instance.m_SimToolBar.OnPauseOnBreakSelected();
				GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("UI_PAUSE_ON_BREAK_DISABLED"), ScreenMessage.DEFAULT_DURATION_SECONDS);
			}
			else
			{
				GameUI.m_Instance.m_SimToolBar.OnPauseOnBreak();
				GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("UI_PAUSE_ON_BREAK_ENABLED"), ScreenMessage.DEFAULT_DURATION_SECONDS);
			}
		}
		if (GameStateManager.GetState() == GameState.SIM && GameInput.JustPressed(BindingType.STRESS_VIS))
		{
			if (GameUI.m_Instance.m_LevelInfoLite.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_LevelInfoLite.OnCancel();
			}
			else if (Profiles.m_ActiveProfile.m_StressViewEnabled)
			{
				GameUI.m_Instance.m_SimToolBar.OnStressSelected();
			}
			else
			{
				GameUI.m_Instance.m_SimToolBar.OnStress();
			}
		}
		bool flag2 = GameInput.GetActiveGameDevice() == GameDevice.Gamepad && GameStateManager.GetState() != GameState.SIM;
		if (GameInput.JustPressed(BindingType.LEVEL_INFO) && !flag2)
		{
			GameUI.ToggleLevelInfoPanel();
		}
		if (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.DECOR)
		{
			if (GameInput.JustPressed(BindingType.UNDO) && (GameToolMode.GetMode() != GameToolModeType.ERASE || !GameInput.IsDown((GameInput.GetActiveGameDevice() != GameDevice.Gamepad) ? BindingType.DRAW_BUILD : BindingType.ERASE)))
			{
				GameUI.m_Instance.m_BuildToolBar.OnUndo();
			}
			if (GameInput.JustPressed(BindingType.REDO) && (GameToolMode.GetMode() != GameToolModeType.ERASE || !GameInput.IsDown((GameInput.GetActiveGameDevice() != GameDevice.Gamepad) ? BindingType.DRAW_BUILD : BindingType.ERASE)))
			{
				GameUI.m_Instance.m_BuildToolBar.OnRedo();
			}
			if (GameInput.JustPressed(BindingType.QUICKSAVE))
			{
				Game.DoQuickSave();
			}
			if (GameInput.JustPressed(BindingType.SAVE))
			{
				if (!CanSaveLoad())
				{
					InterfaceAudio.PlayErrorBeep();
				}
				else
				{
					GameUI.m_Instance.m_TopBar.OnSaveAs();
				}
			}
			if (GameInput.JustPressed(BindingType.LOAD))
			{
				if (!CanSaveLoad())
				{
					InterfaceAudio.PlayErrorBeep();
				}
				else
				{
					GameUI.m_Instance.m_TopBar.OnLoad();
				}
			}
		}
		if (GameInput.JustPressed(BindingType.TOGGLE_HUD))
		{
			GameUI.ToggleHud();
		}
		if (GameInput.IsDown(BindingType.ZOOM_IN) && Time.realtimeSinceStartup > m_NextAllowedZoomInTime)
		{
			m_NextAllowedZoomInTime = Time.realtimeSinceStartup + ZOOM_HOLD_INTERVAL_SECS;
			DoZoom(0.1f);
		}
		if (GameInput.IsDown(BindingType.ZOOM_OUT) && Time.realtimeSinceStartup > m_NextAllowedZoomOutTime)
		{
			m_NextAllowedZoomOutTime = Time.realtimeSinceStartup + ZOOM_HOLD_INTERVAL_SECS;
			DoZoom(-0.1f);
		}
		if (GameInput.JustPressed(BindingType.LOCK_2D))
		{
			Profiles.m_ActiveProfile.m_LockBuildCamera = !Profiles.m_ActiveProfile.m_LockBuildCamera;
			Profiles.SaveActiveProfile();
			if (Profiles.m_ActiveProfile.m_LockBuildCamera && GameStateManager.GetState() == GameState.SIM)
			{
				PointsOfView.SnapTo(PointOfViewType.SIM_CENTER);
				GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CENTER);
			}
			if (Profiles.m_ActiveProfile.m_LockBuildCamera)
			{
				GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("UI_LOCK_2D_CAMERA_ON"), ScreenMessage.DEFAULT_DURATION_SECONDS);
			}
			else
			{
				GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("UI_LOCK_2D_CAMERA_OFF"), ScreenMessage.DEFAULT_DURATION_SECONDS);
			}
		}
	}

	private static bool CanSaveLoad()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel.IsTutorial())
		{
			return false;
		}
		return ActivePanels.None();
	}

	private static void MoveOrthoCamera(float xdiff, float ydiff)
	{
		if (Cameras.MainCamera() != null)
		{
			float x = Cameras.MainCamera().transform.position.x - xdiff;
			float y = Cameras.MainCamera().transform.position.y - ydiff;
			Cameras.MainCamera().transform.position = new Vector3(x, y, Cameras.MainCamera().transform.position.z);
		}
	}

	private static void PanWithMouse()
	{
		PointsOfView.PanPivot(m_InitialClickPositionForPan);
		float f = ((GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? GamepadManager.m_LeftStick.x : Input.GetAxis("Mouse X"));
		float f2 = ((GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? GamepadManager.m_LeftStick.y : Input.GetAxis("Mouse Y"));
		if (Mathf.Abs(f) > GameSettings.PasteCancelThresholdX() || Mathf.Abs(f2) > GameSettings.PasteCancelThresholdY())
		{
			if (ClipboardManager.ReadyToPaste() && GameInput.IsDown(BindingType.DRAW_BUILD))
			{
				ClipboardManager.IgnoreNextPaste();
			}
			if (GameStateManager.GetState() == GameState.DECOR)
			{
				GameStateDecor.ForceIgnoreNextSelection();
			}
			else if (GameStateManager.GetState() == GameState.SANDBOX)
			{
				SandboxInput.ForceIgnoreNextSelection();
			}
		}
	}

	private static void UpdateToDesiredZoom()
	{
		float orthographicSize = Cameras.MainCamera().orthographicSize;
		if (!m_InterpolateToTargetOrthoSize || Mathf.Approximately(orthographicSize, m_TargetOrthoSize))
		{
			m_InterpolateToTargetOrthoSize = false;
			return;
		}
		float num = Mathf.Abs(orthographicSize - m_TargetOrthoSize);
		Mathf.Max(10f, num * 4f);
		m_ElapsedOrthoSeconds += Time.unscaledDeltaTime;
		float num2 = Mathf.Clamp01(m_ElapsedOrthoSeconds / ZOOM_INTERVAL_SECS);
		orthographicSize = ((!(num2 > 0.5f)) ? Mathf.Lerp(m_StartOrthoSize, m_TargetOrthoSize, num2) : Mathf.SmoothStep(m_StartOrthoSize, m_TargetOrthoSize, num2));
		Cameras.SetOrthographicSize(orthographicSize);
		Game.RefreshAfterOrthographicSizeChange();
		if (Mathf.Approximately(m_StartOrthoSize, m_TargetOrthoSize))
		{
			m_InterpolateToTargetOrthoSize = false;
			PointsOfView.UpdatePivotBasedOnCamera();
		}
		if (GameStateManager.GetState() == GameState.SIM && !Cameras.In2DMode())
		{
			GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CUSTOM);
		}
		foreach (SandboxItem item in SandboxSelectionSet.m_Items)
		{
			item.SetOffsetFromPointer(GameInput.GetMousePosition());
		}
		if ((bool)CameraControl.instance && CameraControl.instance.isSimActive)
		{
			CameraControl.instance.ZoomUpdate();
			CameraControl.RegisterTransformUpdate();
		}
	}

	private static void DoZoom(float delta)
	{
		m_StartOrthoSize = Cameras.MainCamera().orthographicSize;
		m_ElapsedOrthoSeconds = 0f;
		GamepadManager.GetZoomSpeed();
		float increment = ((GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? (delta * GamepadManager.GetZoomSpeed()) : (delta * 6.65f * Profiles.m_ActiveProfile.m_MouseWheelSpeedNormalized));
		increment = ApplyScalingFromPolyBridge1(increment, m_StartOrthoSize);
		float value = m_StartOrthoSize - increment;
		if (m_InterpolateToTargetOrthoSize)
		{
			value = m_TargetOrthoSize - increment;
		}
		m_TargetOrthoSize = Mathf.Clamp(value, Game.MinOrthographicSize(), Game.MaxOrthographicSize());
		if (!Mathf.Approximately(m_StartOrthoSize, m_TargetOrthoSize) && (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX) && Cameras.GetOrthographicSize() > Game.MinOrthographicSize() + 0.001f)
		{
			float value2 = 1f / m_StartOrthoSize * increment;
			Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition());
			m_StartCameraPosZoom = Cameras.MainCamera().transform.position;
			m_TargetCameraPosZoom = Cameras.MainCamera().transform.position + (worldPointFromScreenPos - new Vector3(Cameras.MainCamera().transform.position.x, Cameras.MainCamera().transform.position.y, 0f)) * Mathf.Clamp01(value2);
		}
		m_InterpolateToTargetOrthoSize = true;
	}
}
