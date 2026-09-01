using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class GameStateBuild
{
	public static bool m_ClearBridgeOnEnter;

	public static bool m_CameraInTransition;

	public static bool m_LaunchReplayOnEnter;

	public static bool m_LoadNextLevelOnEnter;

	public static PolyTwitchSuggestion m_LoadSuggestionOnEnter;

	public static PolyTwitchAutoSave m_LoadAutoSaveOnEnter;

	public static SandboxItem m_HoverSandboxItem;

	public static SandboxItem m_PrevHoverSandboxItem;

	public static readonly float HOVER_EDGE_DELAY_SECONDS = 0.025f;

	public static BridgeEdge m_HoverEdge;

	public static float m_HoverEdgeSeconds;

	public static BridgeEdge m_HoverLockedEdge;

	public static BridgeSpringSlider m_HoverBridgeSpringSlider;

	public static PistonSlider m_HoverPistonSlider;

	public static bool m_ShowLevelInfoPanelOnEnter;

	private static bool m_ignoreNextSelection;

	private static bool m_RestoreCamera;

	private static BridgeEdgeProxy m_FirstBreakEdgeProxy;

	private static BridgeEdge m_FirstBreakEdge;

	private static readonly float FIRSTBREAK_BOX_WIDTH = 1f;

	private static VectorLine m_SelectionBox;

	private static readonly float HOVER_PERSIST_SECONDS = 0.2f;

	private static readonly float MIN_HOVER_SECONDS = 0.2f;

	private static float m_HoverPersistExpireTime;

	private static float m_HoverStartTime;

	public static void Enter(GameState prevState)
	{
		if (m_LoadNextLevelOnEnter)
		{
			m_LoadNextLevelOnEnter = false;
			Campaign.LoadNextLevel();
		}
		if (m_LoadSuggestionOnEnter != null)
		{
			GameUI.m_Instance.m_PolyTwitchBridge.MaybeAutoSaveCurrentBridge();
			Bridge.ClearAndLoad(m_LoadSuggestionOnEnter.m_BridgeSaveData);
			PolyTwitch.m_LastLoadedSuggestion = m_LoadSuggestionOnEnter;
			m_LoadSuggestionOnEnter = null;
		}
		if (m_LoadAutoSaveOnEnter != null)
		{
			GameUI.m_Instance.m_PolyTwitchMain.m_HistoryPanel.SelectAutoSave(m_LoadAutoSaveOnEnter);
			Bridge.ClearAndLoad(m_LoadAutoSaveOnEnter.m_BridgeSaveData);
			m_LoadAutoSaveOnEnter = null;
		}
		GameUI.ClearMessages();
		EnableUI();
		SetCameraBackgroundColor();
		Outlines.UpdateOutlinesForStateChange(GameState.BUILD);
		Bridge.CancelSelection();
		BridgeShadow.Hide(hide: false);
		Checkpoints.EnterGameState(GameState.BUILD);
		Vehicles.TurnWheelFillMeshesOn();
		ZedAxisVehicles.PositionAtStartingZ();
		BridgeEdges.InitFX();
		BridgePillars.InitFX();
		CustomShapes.ShowAnchorMeshes(on: false);
		Decors.SetVisibility(GameState.BUILD);
		GameUI.SetPointerMode(PointerMode.NORMAL);
		GameUI.m_Instance.m_BottomBar.UpdateIconDucking();
		GameUI.m_Instance.m_BottomBar.SetMaterialIconsAlpha();
		GameUI.m_Instance.m_PolyTwitchMain.ClearWindowMovement();
		GameUI.m_Instance.m_TopBar.UpdateLevelNavButtons();
		GameUI.m_Instance.m_BuildToolBar.m_TraceToolPanel.OnEnterBuildMode();
		GameGrid.CenterOnTerrainEdge(TerrainIslands.GetLeftTerrain());
		GameStateCommonInput.DisableMousePanIfButtonDown();
		WorldBounds.Hide();
		HydraulicsController.RestoreDisableNewAdditionsState();
		Vehicles.DisableOutlines();
		Vehicles.EnableMeshRendering();
		VehicleStopTriggers.EnableHotspotColliders(on: true);
		TerrainIslands.SetActiveBasedOnHiddenFlag();
		TerrainIslands.ShrinkForSandboxMode(shrink: false);
		BridgeUndo.ClearClipboardSaveDataFromStack();
		BridgeRedo.ClearClipboardSaveDataFromStack();
		GameToolMode.SetMode(GameToolModeType.BUILD);
		m_ignoreNextSelection = false;
		m_CameraInTransition = false;
		if (!SandboxSettings.m_HydraulicControllerEnabled)
		{
			BridgeEdges.ClampJointSelectorsToTwoWay();
		}
		if (prevState == GameState.SANDBOX || Game.m_TakingScreenshotForAutoSave || (prevState == GameState.DECOR && GameStateDecor.m_PointOfViewType != PointOfViewType.DECOR_TOP) || (prevState == GameState.SIM && Profiles.m_ActiveProfile.m_LockBuildCamera && !Profiles.m_ActiveProfile.m_FollowCar))
		{
			if (Profiles.m_ActiveProfile.m_LockBuildCamera && !Cameras.In2DMode())
			{
				PointsOfView.SnapTo(PointOfViewType.BUILD);
			}
			DoActionsWhenTransitionCompleted();
		}
		else if (m_RestoreCamera)
		{
			PointsOfView.RotateTo(PointOfViewType.BUILD_CUSTOM, (prevState == GameState.SIM && !Profiles.m_ActiveProfile.m_LockBuildCamera) ? GameSettings.TransitionTimeSeconds() : 0f);
			m_CameraInTransition = true;
		}
		else
		{
			PointsOfView.SnapTo(PointOfViewType.BUILD);
			DoActionsWhenTransitionCompleted();
		}
		switch (prevState)
		{
		case GameState.SANDBOX:
			UpdatePolygonShapes();
			break;
		case GameState.SIM:
			foreach (Vehicle vehicle in Vehicles.m_Vehicles)
			{
				vehicle.SyncPositionAndRotation();
			}
			break;
		}
		if (m_LaunchReplayOnEnter)
		{
			GameUI.m_Instance.m_ShareReplay.Show();
			m_LaunchReplayOnEnter = false;
		}
		if (Game.IsCurrentLevelTutorial())
		{
			if (!CampaignTutorial.m_Completed && !CampaignTutorial.m_ResumeWhenEnteringBuildMode && !DumpPreviewImages.m_Dumping && !DumpReplays.m_Dumping)
			{
				CampaignTutorial.Start(Campaign.m_CurrentLevel.GetCampaignTutorialType());
			}
			CampaignTutorial.m_ResumeWhenEnteringBuildMode = false;
			GameUI.m_Instance.m_GamepadLegend.HideButtons();
		}
		else if (ShouldShowLevelInfoPanelOnEnter())
		{
			ShowLevelInfoPanel();
			m_ShowLevelInfoPanelOnEnter = false;
		}
		Time.timeScale = 0f;
		m_HoverSandboxItem = null;
		m_HoverStartTime = float.MaxValue;
	}

	public static void Exit(GameState nextState)
	{
		if (BridgeJointMovement.m_SelectedJoint != null)
		{
			BridgeJointMovement.FinalizeMovement();
		}
		if (GameToolMode.GetMode() == GameToolModeType.ERASE)
		{
			BridgeActions.FlushRecording();
		}
		HydraulicsController.SaveDisableNewAdditionsState();
		BridgePillarPlacement.CancelPlacementAndSelectPreviousMaterialSilent();
		BridgePillarMovement.CancelMovement();
		if (nextState == GameState.SIM && !Game.m_TakingScreenshotForAutoSave)
		{
			Bridge.m_BridgeRestore = BridgeSave.Serialize();
			PolyTwitchAutoPlay.MaybeLoadForSimulation();
		}
		TerrainIslands.EnableCollisionMeshRenderer(on: false);
		BridgeTrace.ClearDraggingHandles();
		BridgeTrace.TurnOffTracing();
		BridgeShadow.Hide(hide: true);
		BridgePillarDistanceMarkers.HideAll(hide: true);
		ClipboardManager.ClearClipboard();
		Pistons.ForceSliderRelease();
		BridgeSprings.ForceSliderRelease();
		BridgeJointSelectors.CancelCircle();
		BridgeEffects.StopErrorFX();
		Theme.m_Instance.m_BuildZoneDuck.SetActive(value: false);
		VehicleStopTriggers.EnableHotspotColliders(on: false);
		SandboxItemsUpdateShaderProperties(buildMode: false);
		m_RestoreCamera = nextState == GameState.SIM;
		PointsOfView.Set(PointOfViewType.BUILD_CUSTOM, PointsOfView.m_Pivot, Cameras.MainCamera().transform.position, Cameras.MainCamera().transform.rotation, Cameras.MainCamera().orthographicSize);
		GameUI.m_Instance.m_TraceLineToolTip.Disable();
		GameUI.m_Instance.m_Recenter.gameObject.SetActive(value: false);
		if (!WorkshopPreview.m_IsTakingScreenshot && !Game.m_TakingScreenshotForAutoSave)
		{
			GameUI.m_Instance.m_BuildToolBar.gameObject.SetActive(value: false);
			GameUI.ClosePanelsWhenSwitchingModes();
		}
		GameUI.m_Instance.m_PointerToolTip.gameObject.SetActive(value: false);
		Game.UnDesaturateAllVehiclesFlagsAndCheckpoints();
		DestroyFirstBreakBox();
		Cameras.DisableBuildModeSky();
		WaterLine.Enable(enable: false);
		Cameras.AbortRecording();
		GameToolMode.SetMode(GameToolModeType.BUILD);
		if ((bool)BridgeJointPlacement.m_SnapToJoint)
		{
			BridgeJointPlacement.m_SnapToJoint.m_SnapToFX.SetActive(value: false);
		}
		if (nextState != GameState.INVALID && !Game.m_TakingScreenshotForAutoSave && !WorkshopPreview.m_IsTakingScreenshot)
		{
			GameManager.AutoSave(nextState);
		}
		m_ShowLevelInfoPanelOnEnter = false;
		m_CameraInTransition = false;
		m_HoverSandboxItem = null;
		m_PrevHoverSandboxItem = null;
		GameStateCommonInput.StopZooming();
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		if (Cameras.m_Instance != null)
		{
			Cameras.m_Instance.m_Decor.gameObject.SetActive(value: false);
			Cameras.m_Instance.m_Decor.cullingMask = Utils.DECOR_LAYER_MASK;
		}
	}

	public static void UpdateManual()
	{
		if (m_CameraInTransition && !CameraInterpolate.IsActive())
		{
			m_CameraInTransition = false;
			DoActionsWhenTransitionCompleted();
		}
		if (!m_CameraInTransition && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			ProcessInput();
			GameStateCommonInput.Process();
		}
		if (BridgeSelectionSet.IsEmpty() && Input.GetKeyDown(KeyCode.Escape))
		{
			GameStateCommonInput.ProcessEscapeKeypress();
		}
		if (!Game.IsCurrentLevelTutorial() && Bridge.m_BuildMaterialType == BridgeMaterialType.INVALID)
		{
			Game.SelectFirstValidMaterial();
		}
		if (Bridge.m_BuildMaterialType == BridgeMaterialType.PILLAR)
		{
			if ((bool)BridgePillars.GetBridgePillarAtScreenPos(GameInput.GetMousePosition()))
			{
				BridgePillarPlacement.CancelPlacement();
			}
			else
			{
				BridgePillarPlacement.ShowClipboardPillar(GameInput.GetMousePosition());
			}
		}
		else
		{
			BridgePillarPlacement.CancelPlacement();
		}
		Bridge.UpdateManual();
		BridgeJointSelectors.UpdateManual();
		BridgePillarPlacement.UpdateManual(GameInput.GetMousePosition());
		BridgePillarMovement.UpdateManual(GameInput.GetMousePosition());
		BridgePillars.UpdateManual();
		ClipboardManager.UpdateManual();
		GameToolMode.UpdateManual();
		EventTimelines.UpdateManual();
		Budget.UpdateManual();
		if (!m_CameraInTransition)
		{
			Outlines.ManualUpdate();
		}
		UpdateHoverItems();
		UpdateHoverEdge();
		UpdateHoverSliders();
		UpdateHoverVisibility();
		UpdateFirstBreak();
	}

	public static void LateUpdateManual()
	{
		SandboxItems.UpdateFloatingText();
		SandboxItems.UpdateFloatingTextFocus();
		BridgeJointSelectors.LateUpdateManual();
		CampaignTutorial.LateUpdateManual();
		CustomShapes.LateUpdateManual();
	}

	public static void FixedUpdateManual()
	{
		Bridge.FixedUpdateManual();
	}

	public static void OnLayoutLoaded()
	{
		TerrainIslands.ShrinkForSandboxMode(shrink: false);
		UpdatePolygonShapes();
		GameUI.m_Instance.m_BottomBar.SetMaterialIconsAlpha();
		if (GameStateManager.GetState() != GameState.BUILD)
		{
			m_ShowLevelInfoPanelOnEnter = true;
		}
		m_RestoreCamera = false;
	}

	public static void ShowLevelInfoPanel()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN || GameManager.GetGameMode() == GameMode.WORKSHOP)
		{
			GameUI.m_Instance.m_LevelInfo.Open();
		}
	}

	public static bool AllowedToPanCameraWithMouse()
	{
		if (GameInput.GetActiveGameDevice() != GameDevice.KeyboardAndMouse)
		{
			return false;
		}
		if (BridgeTrace.IsTracingActive() && BridgeTrace.m_TracingFollowsMouse)
		{
			return true;
		}
		if (((bool)BridgeJointPlacement.m_SelectedJoint && !GameInput.IsDown(BindingType.PAN_WITH_MOUSE)) || (bool)BridgeJointMovement.m_SelectedJoint)
		{
			return false;
		}
		if (BridgePillarPlacement.InPlacementMode() || BridgePillarMovement.IsMovingSelectionSet())
		{
			return false;
		}
		return true;
	}

	public static void ClearFirstBreakEdge()
	{
		m_FirstBreakEdgeProxy = null;
		m_FirstBreakEdge = null;
		DestroyFirstBreakBox();
	}

	public static void SetFirstBreakEdge(BridgeEdgeProxy proxy)
	{
		m_FirstBreakEdgeProxy = proxy;
	}

	public static bool FirstBreakHasBeenSet()
	{
		return m_FirstBreakEdgeProxy != null;
	}

	public static void ClearFirstBreakAttachedToJoint(string jointGuid)
	{
		if ((bool)m_FirstBreakEdge && (m_FirstBreakEdge.m_JointA.m_Guid == jointGuid || m_FirstBreakEdge.m_JointB.m_Guid == jointGuid))
		{
			m_FirstBreakEdge.SetStressColor(0f);
			ClearFirstBreakEdge();
		}
	}

	public static bool MaybeShowFirstBreakToolTip()
	{
		if (GameStateManager.GetState() != GameState.BUILD)
		{
			return false;
		}
		if (!m_FirstBreakEdge)
		{
			return false;
		}
		Ray ray = Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition());
		if (Physics.Raycast(ray, out var _, float.MaxValue, Utils.JOINT_HOTSPOT_LAYER_MASK))
		{
			return false;
		}
		if (Pistons.MouseIsOverPistonSlider() || BridgeSprings.MouseIsOverSpringSlider())
		{
			return false;
		}
		int num = Physics.RaycastNonAlloc(ray, Utils.m_RaycastHits, float.MaxValue, Utils.EDGE_LAYER_MASK);
		for (int i = 0; i < num; i++)
		{
			if (Utils.m_RaycastHits[i].transform.parent.GetComponent<BridgeEdge>() == m_FirstBreakEdge)
			{
				GameUI.ToolTipEnable(string.Format("{0}\n({1})", Localize.Get("UI_FIRST_BREAK"), BridgeMaterials.GetLocalizedMaterialDisplayName(m_FirstBreakEdge.m_Material.m_MaterialType)), null);
				return true;
			}
		}
		return false;
	}

	public static void ShowFirstBreak()
	{
		if (m_FirstBreakEdgeProxy != null && !string.IsNullOrEmpty(m_FirstBreakEdgeProxy.m_NodeA_Guid) && !string.IsNullOrEmpty(m_FirstBreakEdgeProxy.m_NodeB_Guid) && !Game.IsCurrentLevelTutorial() && Profiles.m_ActiveProfile.m_FirstBreakEnabled)
		{
			if (!string.IsNullOrEmpty(m_FirstBreakEdgeProxy.m_Guid))
			{
				m_FirstBreakEdge = BridgeEdges.FindByGuid(m_FirstBreakEdgeProxy.m_Guid);
			}
			else
			{
				m_FirstBreakEdge = BridgeEdges.FindEnabledEdgeByJointGuids(m_FirstBreakEdgeProxy.m_NodeA_Guid, m_FirstBreakEdgeProxy.m_NodeB_Guid, m_FirstBreakEdgeProxy.m_Material);
			}
			if ((bool)m_FirstBreakEdge && m_SelectionBox == null)
			{
				CreateFirstBreakBox(m_FirstBreakEdge);
			}
		}
	}

	public static void DestroyFirstBreakBox()
	{
		if (m_SelectionBox != null)
		{
			VectorLine.Destroy(ref m_SelectionBox);
			m_SelectionBox = null;
		}
	}

	public static void InitializeWaterForBuildMode()
	{
		WaterBlocks.DisableWaves();
		WaterBlocks.EnableMeshRenderers(enable: false);
		WaterLine.Generate();
		WaterLine.Enable(!SandboxSettings.m_NoWater);
	}

	public static bool VehicleHasHoverFocus(Vehicle vehicle)
	{
		if (m_HoverSandboxItem == vehicle.m_SandboxItem)
		{
			_ = Time.unscaledTime;
			_ = m_HoverStartTime;
			if (TimeHoveringSeconds() >= MIN_HOVER_SECONDS)
			{
				return true;
			}
		}
		return false;
	}

	public static bool CustomShapeHasHoverFocus(CustomShape customShape)
	{
		if (m_HoverSandboxItem == customShape.m_SandboxItem && TimeHoveringSeconds() >= MIN_HOVER_SECONDS)
		{
			return true;
		}
		return false;
	}

	public static float TimeHoveringSeconds()
	{
		if (!(m_HoverSandboxItem == null))
		{
			return Time.unscaledTime - m_HoverStartTime;
		}
		return 0f;
	}

	public static bool HoveringPastThreshold(SandboxItem item)
	{
		if (m_HoverSandboxItem != item)
		{
			return false;
		}
		return TimeHoveringSeconds() > MIN_HOVER_SECONDS;
	}

	public static bool SnapCursorEnabled()
	{
		if (Profiles.m_ActiveProfile.m_SnapEnabled)
		{
			return GameInput.GetActiveGameDevice() == GameDevice.Gamepad;
		}
		return false;
	}

	public static void ForceIgnoreNextSelection()
	{
		m_ignoreNextSelection = true;
	}

	private static void EnableUI()
	{
		GameUI.m_Instance.m_TopBar.gameObject.SetActive(!GameUI.m_DisableHud);
		GameUI.m_Instance.m_TopBar.m_CostAndBudget.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_ButtonContainerSpeed.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_ButtonContainerPauseResume.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_LevelInfo.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_ModeToggle.gameObject.SetActive(GameManager.GetGameMode() == GameMode.SANDBOX);
		GameUI.m_Instance.m_TopBar.m_LevelNavButtons.SetActive(GameManager.GetGameMode() == GameMode.CAMPAIGN);
		GameUI.m_Instance.m_TopBar.m_SimButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_ExitSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_PauseSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_UnPauseSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_HelpButton.interactable = true;
		GameUI.m_Instance.m_TopBar.m_HelpButton.gameObject.SetActive(!Game.IsCurrentLevelTutorial() && (GameManager.GetGameMode() == GameMode.CAMPAIGN || GameManager.GetGameMode() == GameMode.WORKSHOP));
		GameUI.m_Instance.m_TopBar.m_GodModeParent.SetActive(GameManager.GetGameMode() == GameMode.SANDBOX);
		GameUI.m_Instance.m_TopBar.m_ShowDecorParent.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ReplayButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_Replays && !Game.IsCurrentLevelTutorial());
		if (GameUI.m_Instance.m_TopBar.m_ModeToggle.gameObject.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_TopBar.m_ModeToggle.SetStateImmediate(ToggleSliderState.ON);
		}
		GameUI.m_Instance.m_BuildToolBar.gameObject.SetActive(!GameUI.m_DisableHud);
		GameUI.m_Instance.m_BuildToolBar.m_GridButton.gameObject.SetActive(!Profiles.m_ActiveProfile.m_GridEnabled);
		GameUI.m_Instance.m_BuildToolBar.m_GridSelectedButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_GridEnabled);
		GameUI.m_Instance.m_BuildToolBar.m_AutoTriangulateButton.gameObject.SetActive(!Profiles.m_ActiveProfile.m_AutoTriangulateEnabled);
		GameUI.m_Instance.m_BuildToolBar.m_AutoTriangulateSelectedButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_AutoTriangulateEnabled);
		GameUI.m_Instance.m_BuildToolBar.m_EdgeBisectButton.gameObject.SetActive(!Profiles.m_ActiveProfile.m_EdgeBisectEnabled);
		GameUI.m_Instance.m_BuildToolBar.m_EdgeBisectSelectedButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_EdgeBisectEnabled);
		GameUI.m_Instance.m_BuildToolBar.m_SnapButton.gameObject.SetActive(!Profiles.m_ActiveProfile.m_SnapEnabled);
		GameUI.m_Instance.m_BuildToolBar.m_SnapSelectedButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_SnapEnabled);
		GameUI.m_Instance.m_BuildToolBar.m_AutoTriangulateButton.interactable = true;
		GameUI.m_Instance.m_BuildToolBar.m_AutoTriangulateSelectedButton.interactable = true;
		GameUI.m_Instance.m_BuildToolBar.m_EdgeBisectButton.interactable = true;
		GameUI.m_Instance.m_BuildToolBar.m_EdgeBisectSelectedButton.interactable = true;
		GameUI.m_Instance.m_BuildToolBar.m_SnapButton.interactable = true;
		GameUI.m_Instance.m_BuildToolBar.m_SnapSelectedButton.interactable = true;
		GameUI.m_Instance.m_BuildToolBar.m_GridButton.interactable = true;
		GameUI.m_Instance.m_BuildToolBar.m_GridSelectedButton.interactable = true;
		GameUI.m_Instance.m_BuildToolBar.m_TrashButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_BuildToolBar.m_TrashButton.interactable = true;
		GameUI.m_Instance.m_BuildToolBar.m_UndoButton.interactable = false;
		GameUI.m_Instance.m_BuildToolBar.m_RedoButton.interactable = false;
		GameUI.m_Instance.m_BottomBar.gameObject.SetActive(!GameUI.m_DisableHud && !GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy);
		GameUI.m_Instance.m_BottomBar.SelectMaterial(Bridge.m_BuildMaterialType, animateTransition: false);
		GameUI.m_Instance.m_BottomBar.m_HydraulicController.transform.parent.gameObject.SetActive(SandboxSettings.m_HydraulicControllerEnabled && HydraulicsPhases.m_Phases.Count > 0);
		GameUI.m_Instance.m_TraceTool.m_RolloutPanel.SetActive(value: false);
		GameUI.m_Instance.m_Selection.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Clipboard.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_LiveStress.gameObject.SetActive(value: false);
		SandboxUI.DeActivateAllPanels();
		Budget.UpdateManual();
		GameUI.m_Instance.m_BottomBar.RefreshLimits();
		GameUI.m_Instance.m_BottomBar.m_PanelResizeHorizontal.ForceUpdate();
	}

	private static void DoActionsWhenTransitionCompleted()
	{
		Main.m_Instance.m_PostFX.SetForBuildMode();
		Game.SetCameraCullingMasks(GameState.BUILD);
		TerrainIslands.HideSecondPassMeshRenderers(hide: false);
		TerrainIslands.EnableCollisionMeshRenderer(on: true);
		TerrainIslands.StopParticleSystems();
		TerrainIslands.StopWaterFalls();
		TerrainLights.TurnOn(on: false);
		CustomShapes.ShowAllPins();
		GameGrid.m_Grid.SetActive(Profiles.m_ActiveProfile.m_GridEnabled);
		BridgeTrace.Hide(hide: false);
		BridgeJoints.MakeDefaultColor();
		Theme.m_Instance.EnableBuildModeLighting();
		Theme.m_Instance.m_BuildZoneDuck.SetActive(BuildZones.GetActiveCount() > 0);
		SetAmbientLightingColor();
		GameRenderSettings.SetShadows(on: false);
		WaterBlocks.EnableMeshRenderers(enable: false);
		InitializeWaterForBuildMode();
		ShowFirstBreak();
		Bridge.UnHideAllUI();
		BridgePillars.EnableOutlines();
		Cameras.DisableSky();
		Cameras.EnableBuildModeSky();
		Checkpoints.SetOutlineColor();
		HeightFog.Enable(on: false);
		ZedAxisVehicles.EnableOutlineMeshRendering();
		ZedAxisVehicles.Enable();
		Vehicles.ShowCenterOfMass(Sandbox.m_ShowVehicleCenterOfMass);
		SandboxItemsUpdateShaderProperties(buildMode: true);
		if (GameStateManager.GetPrevState() != GameState.SANDBOX)
		{
			SandboxItems.EnableOutlines();
			Vehicles.DisableOutlines();
			Vehicles.EnableMeshRendering();
		}
		SandboxItemsEnterBuildMode();
		Camera.main.transform.rotation = Quaternion.identity;
	}

	private static void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInput())
		{
			GameUI.SetPointerMode(PointerMode.NORMAL);
			return;
		}
		if (GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy)
		{
			GameUI.SetPointerMode(PointerMode.NORMAL);
			return;
		}
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			GameUI.SetPointerMode(PointerMode.NORMAL);
			return;
		}
		if (GameInput.JustPressedRaw((GameInput.GetActiveGameDevice() != GameDevice.Gamepad) ? BindingType.DRAW_BUILD : BindingType.ERASE) && GameToolMode.GetMode() == GameToolModeType.ERASE)
		{
			Bridge.InitPreviousErasePos(GameInput.GetMousePosition());
			BridgeActions.FlushRecording();
		}
		if (GameInput.JustReleased((GameInput.GetActiveGameDevice() != GameDevice.Gamepad) ? BindingType.DRAW_BUILD : BindingType.ERASE) && GameToolMode.GetMode() == GameToolModeType.ERASE)
		{
			BridgeActions.FlushRecording();
		}
		if (GameInput.IsDown((GameInput.GetActiveGameDevice() != GameDevice.Gamepad) ? BindingType.DRAW_BUILD : BindingType.ERASE) && GameToolMode.GetMode() == GameToolModeType.ERASE && !BridgeTrace.IsFilling())
		{
			Bridge.Erase(GameInput.GetMousePosition());
		}
		if (GameInput.JustReleased(BindingType.DRAW_BUILD) && GameToolMode.GetMode() == GameToolModeType.MOVE && BridgeJointMovement.m_CancelMoveModeOnRelease)
		{
			GameToolMode.SetMode(GameToolModeType.BUILD);
			BridgeJointMovement.m_CancelMoveModeOnRelease = false;
		}
		if (!BridgeSelectionSet.IsEmpty())
		{
			ProcessSelectionSetInput();
		}
		if (ClipboardManager.ReadyToPaste() && !Game.IsCurrentLevelTutorial())
		{
			ProcessClipboardInput();
		}
		ProcessMaterialsInput();
		GameStateCommonInput.ProcessSimSpeedInput();
		if (GameInput.JustPressed(BindingType.MULTI_SELECT))
		{
			BridgeJointPlacement.CancelSelection();
			InterfaceAudio.Play("ui_build_select");
		}
		if (GameInput.JustPressed(BindingType.GRID) || (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT) && !Game.IsCurrentLevelTutorial()))
		{
			if (Profiles.m_ActiveProfile.m_GridEnabled)
			{
				GameUI.m_Instance.m_BuildToolBar.OnGridSelected();
			}
			else
			{
				GameUI.m_Instance.m_BuildToolBar.OnGrid();
			}
		}
		bool flag = false;
		if (GameInput.JustPressed(BindingType.TRACE_START) || (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP) && !Game.IsCurrentLevelTutorial() && !GameUI.m_Instance.m_Selection.gameObject.activeInHierarchy && BridgeSelectionSet.IsEmpty() && !ClipboardManager.ReadyToPaste()))
		{
			if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				GameUI.m_Instance.m_BuildToolBar.OnTraceTool();
				flag = BridgeTrace.IsTracingActive();
			}
		}
		if (!flag && (GameInput.JustPressed(BindingType.TRACE_CLEAR) || GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP)))
		{
			GameUI.m_Instance.m_TraceTool.OnClear();
		}
		if ((GameInput.JustPressed(BindingType.TRACE_FILL) || GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN)) && GameUI.m_Instance.m_TraceTool.m_Fill.interactable)
		{
			if (BridgeTrace.IsTraceLinePlaced() && Bridge.m_BuildMaterialType != BridgeMaterialType.PILLAR)
			{
				GameUI.m_Instance.m_TraceTool.OnFill();
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GameInput.JustPressed(BindingType.TRACE_SHAPE))
		{
			if (GameUI.m_Instance.m_TraceTool.m_Shape.gameObject.activeInHierarchy && GameUI.m_Instance.m_TraceTool.m_Shape.interactable)
			{
				GameUI.m_Instance.m_TraceTool.OnShape();
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT) && GameUI.m_Instance.m_TraceTool.m_Shape.gameObject.activeInHierarchy && GameUI.m_Instance.m_TraceTool.m_Shape.interactable)
		{
			GameUI.m_Instance.m_TraceTool.OnShape();
		}
		if (GameInput.JustPressed(BindingType.FLIP_HORIZONTAL) && !ClipboardManager.ReadyToPaste())
		{
			if (BridgeTrace.IsTracingActive() && BridgeTrace.IsTraceLinePlaced())
			{
				GameUI.m_Instance.m_TraceTool.OnFlip();
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GameInput.JustPressed(BindingType.TRACE_LOCK_TANGENTS))
		{
			if (GameUI.m_Instance.m_TraceTool.m_TangentsLocked.gameObject.activeInHierarchy && GameUI.m_Instance.m_TraceTool.m_TangentsLocked.interactable)
			{
				GameUI.m_Instance.m_TraceTool.OnTangentsLocked();
			}
			else if (GameUI.m_Instance.m_TraceTool.m_TangentsFree.gameObject.activeInHierarchy && GameUI.m_Instance.m_TraceTool.m_TangentsFree.interactable)
			{
				GameUI.m_Instance.m_TraceTool.OnTangentsFree();
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GameInput.JustPressed(BindingType.TRACE_SNAP_TANGENTS))
		{
			if (GameUI.m_Instance.m_TraceTool.m_Grid.gameObject.activeInHierarchy && GameUI.m_Instance.m_TraceTool.m_Grid.interactable)
			{
				GameUI.m_Instance.m_TraceTool.OnGrid();
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GameInput.JustPressed(BindingType.SPLIT_JOINT))
		{
			bool flag2 = false;
			if ((bool)BridgeJointPlacement.m_HoverJoint)
			{
				BridgeJointPlacement.ProcessDoubleClickOnJoint(BridgeJointPlacement.m_HoverJoint);
				flag2 = true;
			}
			else
			{
				foreach (BridgeJoint joint in BridgeSelectionSet.m_Joints)
				{
					if (joint.gameObject.activeInHierarchy)
					{
						BridgeJointPlacement.ProcessDoubleClickOnJoint(joint);
						flag2 = true;
					}
				}
			}
			if (!flag2)
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GameInput.JustPressed(BindingType.START_SIM) && GameUI.m_Instance.m_TopBar.m_SimButton.interactable)
		{
			GameUI.m_Instance.m_TopBar.OnSim();
		}
		if (GameInput.JustPressed(BindingType.SANDBOX_BUILD_SIM_CYCLE) && GameManager.GetGameMode() == GameMode.SANDBOX)
		{
			GameUI.m_Instance.m_TopBar.m_ModeToggle.OnButton();
		}
		if (GameInput.JustPressed(BindingType.AUTO_TRIANGULATE) || (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT) && !Game.IsCurrentLevelTutorial() && BridgeSelectionSet.IsEmpty() && !ClipboardManager.ReadyToPaste() && !BridgeTrace.IsTracingActive() && !BridgeTrace.IsTraceLinePlaced()))
		{
			if (Profiles.m_ActiveProfile.m_AutoTriangulateEnabled)
			{
				GameUI.m_Instance.m_BuildToolBar.OnAutoTriangulateSelected();
			}
			else
			{
				GameUI.m_Instance.m_BuildToolBar.OnAutoTriangulate();
			}
		}
		if (GameInput.JustPressed(BindingType.EDGE_BISECT))
		{
			if (Profiles.m_ActiveProfile.m_EdgeBisectEnabled)
			{
				GameUI.m_Instance.m_BuildToolBar.OnEdgeBisectSelected();
			}
			else
			{
				GameUI.m_Instance.m_BuildToolBar.OnEdgeBisect();
			}
		}
		if (GameUI.IsPointerOverGameObject())
		{
			GameUI.SetPointerMode(PointerMode.NORMAL);
		}
		else if (GameToolMode.GetMode() == GameToolModeType.MOVE)
		{
			if ((bool)BridgeJointPlacement.m_SelectedJoint)
			{
				BridgeJointPlacement.CancelSelection();
			}
			GameUI.SetPointerMode(PointerMode.MOVE);
		}
		else if (GameToolMode.GetMode() == GameToolModeType.ERASE && !BridgeTrace.IsFilling())
		{
			GameUI.SetPointerMode(PointerMode.ERASE);
		}
		else if (GameInput.IsDown(BindingType.MULTI_SELECT) && ActivePanels.m_Panels.Count == 0 && ClipboardManager.IsEmpty())
		{
			GameUI.SetPointerMode(PointerMode.SELECT_TOGGLE);
		}
		else
		{
			GameUI.SetPointerMode(PointerMode.NORMAL);
		}
		if (GameInput.JustPressed(BindingType.SELECT_INTERRUPT) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			BridgeJointPlacement.ClearDoubleClickTimer();
			if (CanProcessBuildAction())
			{
				_ = GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy;
			}
		}
		if (GameInput.JustPressed(BindingType.DRAW_BUILD) && CanProcessBuildAction())
		{
			Bridge.ProcessBuildAction();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH) && CanProcessBuildAction() && !ClipboardManager.ReadyToPaste() && !CampaignTutorial.IsRunning())
		{
			if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy || (!BridgeJointPlacement.m_SnapToJoint && !BridgeJointPlacement.m_HoverJoint))
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				GameToolMode.MoveModeActivate(on: true);
				if (SnapCursorEnabled() && GameToolMode.GetMode() == GameToolModeType.MOVE)
				{
					BridgeJointPlacement.SnapCursorToClosestNode();
					BridgeJointPlacement.UpdateHoverJoint();
				}
				Bridge.ProcessBuildAction();
			}
		}
		if (GameInput.JustReleased(BindingType.DRAW_BUILD) && CanProcessBuildAction())
		{
			if (BridgeTrace.m_TracingFollowsMouse)
			{
				BridgeTrace.ProcessButtonUp();
			}
			else if (BridgePillarPlacement.InPlacementMode())
			{
				BridgePillarPlacement.ProcessButtonUp(GameInput.GetMousePosition());
			}
		}
		if (GameInput.JustPressed(BindingType.SELECT_INTERRUPT) && !GameUI.IsPointerOverGameObject() && GameUI.GetPointerMode() == PointerMode.MOVE)
		{
			BridgeJointMovement.ProcessClick(GameInput.GetMousePosition());
			m_ignoreNextSelection = true;
		}
		if ((GameInput.JustPressed(BindingType.SELECT_INTERRUPT) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)) && GameToolMode.GetMode() != GameToolModeType.MOVE)
		{
			if ((bool)BridgeJointPlacement.m_SelectedJoint)
			{
				BridgeJointPlacement.CancelSelection();
				m_ignoreNextSelection = true;
			}
			else if (!GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy && !CampaignTutorial.BlockGroupSelect() && !SandboxSelectionSet.SelectionFollowsMouse() && GameToolMode.GetMode() != GameToolModeType.ERASE)
			{
				GroupSelect.Start(GameInput.GetMousePosition());
			}
		}
		if ((GameInput.JustPressed(BindingType.SELECT_INTERRUPT) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)) && GameToolMode.GetMode() != GameToolModeType.MOVE)
		{
			if ((bool)BridgeJointPlacement.m_SelectedJoint)
			{
				BridgeJointPlacement.CancelSelection();
				m_ignoreNextSelection = true;
			}
			else if (AllowedToGroupSelect())
			{
				GroupSelect.Start(GameInput.GetMousePosition());
			}
		}
		if (GameInput.JustReleased(BindingType.SELECT_INTERRUPT) || GamepadManager.ButtonJustReleased(GamepadButtonType.EAST))
		{
			if (m_ignoreNextSelection)
			{
				m_ignoreNextSelection = false;
			}
			else if (GameToolMode.GetMode() != GameToolModeType.ERASE)
			{
				if (GroupSelect.IsActive())
				{
					if (!GameInput.MultiSelectIsDown())
					{
						BridgeSelectionSet.CancelSelection();
					}
					BridgeSelectionSet.SelectAllInRect(GroupSelect.GetRect(), GameInput.MultiSelectIsDown());
					if (!BridgeSelectionSet.IsEmpty())
					{
						InterfaceAudio.Play("ui_build_select");
					}
				}
				else
				{
					Bridge.ProcessSelectAction();
				}
			}
			GroupSelect.Cancel();
		}
		if (GameInput.JustPressed(BindingType.HELP) && !Game.IsCurrentLevelTutorial())
		{
			GameUI.m_Instance.m_Help.Show();
		}
		if (GameInput.JustPressed(BindingType.SELECT_TOGGLE))
		{
			GameUI.m_Instance.m_BuildToolBar.OnSelect();
		}
		if (GameInput.JustPressed(BindingType.MOVE_TOGGLE))
		{
			GameUI.m_Instance.m_BuildToolBar.OnMove();
		}
		if (GameInput.JustPressed(BindingType.ERASE_TOGGLE))
		{
			GameUI.m_Instance.m_BuildToolBar.OnErase();
		}
		if (GameInput.JustPressed(BindingType.NUDGE_HYDRO_UP) || KeyboardRepeater.JustRepeated(Bindings.GetBinding(BindingType.NUDGE_HYDRO_UP).m_KeyCode) || KeyboardRepeater.JustRepeated(Bindings.GetBinding(BindingType.NUDGE_HYDRO_UP).m_AltKeyCode))
		{
			NudgeSelectedHydrosUp();
		}
		if (GameInput.JustPressed(BindingType.NUDGE_HYDRO_DOWN) || KeyboardRepeater.JustRepeated(Bindings.GetBinding(BindingType.NUDGE_HYDRO_DOWN).m_KeyCode) || KeyboardRepeater.JustRepeated(Bindings.GetBinding(BindingType.NUDGE_HYDRO_DOWN).m_AltKeyCode))
		{
			NudgeSelectedHydrosDown();
		}
		if (!ClipboardManager.ReadyToPaste())
		{
			if ((!BridgeTrace.IsTraceLinePlaced() || !GameUI.m_Instance.m_TraceTool.m_Fill.interactable) && GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN) && !Game.IsCurrentLevelTutorial() && BridgeSelectionSet.IsEmpty())
			{
				if (Profiles.m_ActiveProfile.m_SnapEnabled)
				{
					GameUI.m_Instance.m_BuildToolBar.OnSnapSelected();
				}
				else
				{
					GameUI.m_Instance.m_BuildToolBar.OnSnap();
				}
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
			{
				GameUI.m_Instance.m_BottomBar.CyclePrev();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
			{
				GameUI.m_Instance.m_BottomBar.CycleNext();
			}
		}
		if (!BridgeSelectionSet.IsEmpty() && GameUI.m_Instance.m_Selection.gameObject.activeInHierarchy)
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN))
			{
				GameUI.m_Instance.m_Selection.OnDelete();
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP))
			{
				GameUI.m_Instance.m_Selection.OnCopy();
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT))
			{
				GameUI.m_Instance.m_Selection.OnCut();
			}
		}
	}

	private static bool AllowedToGroupSelect()
	{
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (CampaignTutorial.BlockGroupSelect())
		{
			return false;
		}
		if (SandboxSelectionSet.SelectionFollowsMouse())
		{
			return false;
		}
		if (GameUI.m_Instance.m_SandboxEditBuildZone.gameObject.activeInHierarchy && GameUI.m_Instance.m_SandboxEditBuildZone.IsEditing())
		{
			return false;
		}
		return true;
	}

	public static bool CanProcessBuildAction()
	{
		if (GameUI.IsPointerOverGameObject())
		{
			return false;
		}
		if (BridgeTrace.m_ArcTracer.HandlesVisible() && BridgeTrace.m_ArcTracer.PointerOverArcHandle(GameInput.GetMousePosition()) != null)
		{
			return false;
		}
		if (Pistons.MouseIsOverPistonSlider() && BridgeSprings.MouseIsOverSpringSlider())
		{
			return false;
		}
		return true;
	}

	private static void ProcessSelectionSetInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!Game.IsCurrentLevelTutorial())
			{
				Bridge.CancelSelection();
				ClipboardManager.ClearClipboard();
			}
			return;
		}
		if (GameInput.JustPressed(BindingType.DELETE_SELECTION))
		{
			GameUI.m_Instance.m_Selection.OnDelete();
		}
		if (GameInput.JustPressed(BindingType.COPY_SELECTION))
		{
			if (BridgeSelectionSet.OnlyContainsJoints())
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				GameUI.m_Instance.m_Selection.OnCopy();
			}
		}
		if (GameInput.JustPressed(BindingType.CUT_SELECTION))
		{
			if (BridgeSelectionSet.OnlyContainsJoints() || Game.IsCurrentLevelTutorial())
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				GameUI.m_Instance.m_Selection.OnCut();
			}
		}
	}

	private static void ProcessClipboardInput()
	{
		if (GameInput.JustPressed(BindingType.FLIP_HORIZONTAL) || GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT))
		{
			GameUI.m_Instance.m_Clipboard.OnFlipHoriz();
		}
		if (GameInput.JustPressed(BindingType.FLIP_VERTICAL) || GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP))
		{
			if (ClipboardManager.GetBridgePillarCount() > 0)
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				GameUI.m_Instance.m_Clipboard.OnFlipVert();
			}
		}
	}

	private static void ProcessMaterialsInput()
	{
		if (GameInput.JustPressed(BindingType.SELECT_ROAD))
		{
			TrySelectRoad();
		}
		else if (GameInput.JustPressed(BindingType.SELECT_WOOD))
		{
			TrySelectWood();
		}
		else if (GameInput.JustPressed(BindingType.SELECT_STEEL))
		{
			TrySelectSteel();
		}
		else if (GameInput.JustPressed(BindingType.SELECT_HYDRAULICS))
		{
			TrySelectHydraulics();
		}
		else if (GameInput.JustPressed(BindingType.SELECT_ROPE))
		{
			TrySelectRope();
		}
		else if (GameInput.JustPressed(BindingType.SELECT_CABLE))
		{
			TrySelectCable();
		}
		else if (GameInput.JustPressed(BindingType.SELECT_SPRING))
		{
			TrySelectSpring();
		}
		else if (GameInput.JustPressed(BindingType.SELECT_PILLAR))
		{
			TrySelectPillar();
		}
	}

	private static void TrySelectRoad()
	{
		if (Budget.m_RoadBudget == 0 || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			GameUI.m_Instance.m_BottomBar.OnMaterial(BridgeMaterialType.ROAD);
		}
	}

	private static void TrySelectWood()
	{
		if (!Budget.m_AllowWood || Budget.m_WoodBudget == 0 || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			GameUI.m_Instance.m_BottomBar.OnMaterial(BridgeMaterialType.WOOD);
		}
	}

	private static void TrySelectSteel()
	{
		if (!Budget.m_AllowSteel || Budget.m_SteelBudget == 0 || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			GameUI.m_Instance.m_BottomBar.OnMaterial(BridgeMaterialType.STEEL);
		}
	}

	private static void TrySelectHydraulics()
	{
		if (!Budget.m_AllowHydraulic || Budget.m_HydraulicBudget == 0 || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			GameUI.m_Instance.m_BottomBar.OnMaterial(BridgeMaterialType.HYDRAULICS);
		}
	}

	private static void TrySelectRope()
	{
		if (!Budget.m_AllowRope || Budget.m_RopeBudget == 0 || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			GameUI.m_Instance.m_BottomBar.OnMaterial(BridgeMaterialType.ROPE);
		}
	}

	private static void TrySelectCable()
	{
		if (!Budget.m_AllowCable || Budget.m_CableBudget == 0 || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			GameUI.m_Instance.m_BottomBar.OnMaterial(BridgeMaterialType.CABLE);
		}
	}

	private static void TrySelectSpring()
	{
		if (!Budget.m_AllowSpring || Budget.m_SpringBudget == 0 || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			GameUI.m_Instance.m_BottomBar.OnMaterial(BridgeMaterialType.SPRING);
		}
	}

	private static void TrySelectPillar()
	{
		if (!Budget.m_AllowPillar || Budget.m_PillarBudget == 0 || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		if (Bridge.m_BuildMaterialType != BridgeMaterialType.PILLAR && Bridge.m_BuildMaterialType != BridgeMaterialType.INVALID)
		{
			BridgePillarPlacement.m_PreviousSelectedBridgeMaterialType = Bridge.m_BuildMaterialType;
		}
		GameUI.m_Instance.m_BottomBar.OnMaterial(BridgeMaterialType.PILLAR);
	}

	private static void UpdatePolygonShapes()
	{
		TerrainIslands.UpdatePolygonShapes();
		Vehicles.UpdatePolygonShapes();
		ZedAxisVehicles.UpdatePolygonShapes();
		Rocks.UpdatePolygonShapes();
		FlyingObjects.UpdatePolygonShapes();
		CustomShapes.UpdatePolygonShapes();
	}

	private static void SetCameraBackgroundColor()
	{
		Cameras.MainCamera().backgroundColor = GameUI.m_Instance.m_BuildModeBackgroundColor;
	}

	private static void SetAmbientLightingColor()
	{
		RenderSettings.ambientLight = PostFX.m_Instance.m_BuildAmbientLightColor;
	}

	private static void UpdateHoverItems()
	{
		SandboxItem hoverSandboxItem = m_HoverSandboxItem;
		if (SuppressHover())
		{
			m_HoverSandboxItem = null;
		}
		else
		{
			m_HoverSandboxItem = SandboxItems.GetItemUnderPos(GameInput.GetMousePosition());
			EventUnit hoverIcon = GameUI.m_Instance.m_HydraulicsController.GetHoverIcon();
			if ((bool)hoverIcon && (bool)hoverIcon.GetVehicle())
			{
				m_HoverSandboxItem = hoverIcon.GetVehicle().m_SandboxItem;
			}
		}
		if (m_HoverSandboxItem != hoverSandboxItem)
		{
			m_HoverStartTime = Time.unscaledTime;
		}
	}

	private static bool SuppressHover()
	{
		if (GameStateManager.GetState() != GameState.BUILD || GameUI.IsPointerOverGameObject())
		{
			return true;
		}
		if (GameUI.SaveLoadPanelIsActive() || GameUI.LevelEndPanelIsActive())
		{
			return true;
		}
		if (BridgeTrace.IsDraggingHandles() || BridgeTrace.TracingFollowsMouse())
		{
			return true;
		}
		if ((bool)BridgeJointPlacement.m_SelectedJoint)
		{
			return true;
		}
		if ((bool)BridgeJointPlacement.m_HoverJoint)
		{
			return true;
		}
		return false;
	}

	private static void UpdateHoverEdge()
	{
		Ray ray = Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition());
		BridgeEdge hoverEdge = m_HoverEdge;
		if (!Physics.Raycast(ray, out var _, float.MaxValue, Utils.JOINT_LAYER_MASK))
		{
			m_HoverEdge = BridgeEdges.GetEdgeUnderRay(ray);
		}
		if (hoverEdge == m_HoverEdge)
		{
			m_HoverEdgeSeconds += Time.unscaledDeltaTime;
		}
		else
		{
			m_HoverEdgeSeconds = 0f;
		}
	}

	private static void UpdateHoverSliders()
	{
		if (!Utils.GetClosestRaycastHit(GameInput.GetMousePosition(), Utils.JOINT_SELECTOR_LAYER_MASK))
		{
			m_HoverBridgeSpringSlider = BridgeSprings.GetSpringSliderUnderMouseSkipJointSelectorCheck();
			m_HoverPistonSlider = Pistons.GetPistonSliderUnderMouseSkipJointSelectorCheck();
			m_HoverLockedEdge = BridgeEdges.GetLockIconUnderMouseSkipJointSelectorCheck();
		}
	}

	private static void CreateFirstBreakBox(BridgeEdge edge)
	{
		m_SelectionBox = new VectorLine("FirstBreak", new List<Vector3>(8), null, FIRSTBREAK_BOX_WIDTH, LineType.Discrete, Joins.Weld);
		m_SelectionBox.material = GameUI.m_Instance.m_UnmaskedVectrosityMaterial;
		m_SelectionBox.Draw3DAuto();
		Vector3 bottomLeft = new Vector3((edge.m_Material.m_MaterialType == BridgeMaterialType.WOOD) ? (-1f) : (-0.5f), -0.2f, -5f);
		Vector3 topRight = new Vector3((edge.m_Material.m_MaterialType == BridgeMaterialType.WOOD) ? 1f : 0.5f, 0.2f, -5f);
		m_SelectionBox.MakeRect(bottomLeft, topRight);
		m_SelectionBox.drawTransform = edge.m_MeshRenderer.transform;
		m_SelectionBox.layer = Utils.RENDER_LAST_LAYER;
		m_SelectionBox.textureScale = 1f;
		m_SelectionBox.color = Color.red;
		UpdateFirstBreak();
	}

	private static void UpdateFirstBreak()
	{
		if (!m_FirstBreakEdge)
		{
			DestroyFirstBreakBox();
		}
		else if (!m_FirstBreakEdge.gameObject.activeInHierarchy)
		{
			DestroyFirstBreakBox();
		}
		else if (m_SelectionBox != null)
		{
			Outlines.UpdateWidthForOrthographicChange(m_SelectionBox, FIRSTBREAK_BOX_WIDTH);
		}
	}

	private static void UpdateHoverVisibility()
	{
		if (IsHoveringOverVehicleFlagOrCheckpoint() && !BridgeJointPlacement.m_SelectedJoint && !Game.IsCurrentLevelTutorial())
		{
			DesaturateUnHoveredVehiclesFlagsAndCheckpoints();
		}
		else if (Time.unscaledTime > m_HoverPersistExpireTime)
		{
			Game.UnDesaturateAllVehiclesFlagsAndCheckpoints();
			m_HoverPersistExpireTime = float.MaxValue;
		}
		m_PrevHoverSandboxItem = m_HoverSandboxItem;
	}

	private static void DesaturateUnHoveredVehiclesFlagsAndCheckpoints()
	{
		if (!(m_HoverSandboxItem == null) && !(Time.unscaledTime - m_HoverStartTime < MIN_HOVER_SECONDS))
		{
			Vehicle vehicle = null;
			if (m_HoverSandboxItem.m_Type == SandboxItemType.VEHICLE)
			{
				vehicle = m_HoverSandboxItem.GetComponent<Vehicle>();
			}
			else if (m_HoverSandboxItem.m_Type == SandboxItemType.VEHICLE_STOP_TRIGGER)
			{
				vehicle = Vehicles.FindByGuid(m_HoverSandboxItem.GetComponent<VehicleStopTrigger>().m_VehicleGuid);
			}
			else if (m_HoverSandboxItem.m_Type == SandboxItemType.CHECKPOINT)
			{
				vehicle = Vehicles.FindByGuid(m_HoverSandboxItem.GetComponent<Checkpoint>().m_VehicleGuid);
			}
			if (!(vehicle == null))
			{
				Game.DesaturateAllVehiclesFlagsAndCheckpointsExcept(vehicle.m_Guid);
				m_HoverPersistExpireTime = Time.unscaledTime + HOVER_PERSIST_SECONDS;
			}
		}
	}

	private static bool IsHoveringOverVehicleFlagOrCheckpoint()
	{
		if (m_HoverSandboxItem == null)
		{
			return false;
		}
		if (m_HoverSandboxItem.m_Type != SandboxItemType.VEHICLE && m_HoverSandboxItem.m_Type != SandboxItemType.VEHICLE_STOP_TRIGGER)
		{
			return m_HoverSandboxItem.m_Type == SandboxItemType.CHECKPOINT;
		}
		return true;
	}

	private static bool ShouldShowLevelInfoPanelOnEnter()
	{
		if (!m_ShowLevelInfoPanelOnEnter)
		{
			return false;
		}
		if (DumpPreviewImages.m_Dumping || DumpReplays.m_Dumping)
		{
			return false;
		}
		if (PolyTwitch.m_StreamStarted && Profiles.m_ActiveProfile.m_TwitchAutoPlay)
		{
			return false;
		}
		return true;
	}

	private static void SandboxItemsEnterBuildMode()
	{
		BuildZones.EnterBuildMode();
		CustomShapes.EnterBuildMode();
		Platforms.EnterBuildMode();
		FlyingObjects.EnterBuildMode();
		Ramps.EnterBuildMode();
	}

	private static void SandboxItemsUpdateShaderProperties(bool buildMode)
	{
		CustomShapes.UpdateShaderProperties(buildMode);
		Pillars.UpdateShaderProperties(buildMode);
		Platforms.UpdateShaderProperties(buildMode);
		FlyingObjects.UpdateShaderProperties(buildMode);
		Ramps.UpdateShaderProperties(buildMode);
		Rocks.UpdateShaderProperties(buildMode);
		Decors.UpdateShaderProperties(buildMode, CuttingPlanes.m_Instance.m_Floor);
		TerrainIslands.UpdateShaderProperties(buildMode, CuttingPlanes.m_Instance.m_Floor);
	}

	private static void NudgeSelectedHydrosUp()
	{
		NudgeSelectedHydros(0.01f, 0.1f, 1f);
	}

	private static void NudgeSelectedHydrosDown()
	{
		NudgeSelectedHydros(-0.01f, -0.1f, 0f);
	}

	private static void NudgeSelectedHydros(float hydroDelta, float springsDelta, float limit)
	{
		int num = 0;
		BridgeActions.StartRecording();
		List<BridgeEdge> list = new List<BridgeEdge>(BridgeSelectionSet.m_Edges);
		if (GameInput.IsDown(BindingType.SHOW_ALL_TOOLTIPS))
		{
			foreach (BridgeEdge edge in BridgeEdges.m_Edges)
			{
				bool flag = SandboxSettings.m_SpringAdjustmentsAllowed && edge.IsSpring();
				if (edge.gameObject.activeInHierarchy && (edge.IsPiston() || flag) && !list.Contains(edge) && !edge.IsLocked())
				{
					list.Add(edge);
				}
			}
		}
		if (m_HoverEdge != null && m_HoverEdge.gameObject.activeInHierarchy && (m_HoverEdge.IsPiston() || (SandboxSettings.m_SpringAdjustmentsAllowed && m_HoverEdge.IsSpring())) && !list.Contains(m_HoverEdge) && !m_HoverEdge.IsLocked())
		{
			list.Add(m_HoverEdge);
		}
		foreach (BridgeEdge item in list)
		{
			if (item.IsPiston())
			{
				Piston pistonOnEdge = Pistons.GetPistonOnEdge(item);
				if ((bool)pistonOnEdge)
				{
					float normalizedValue = pistonOnEdge.m_Slider.GetNormalizedValue();
					float num2 = Mathf.Clamp01(normalizedValue + hydroDelta);
					if (!Mathf.Approximately(Mathf.Abs(normalizedValue - num2), 0f))
					{
						pistonOnEdge.m_Slider.SetNormalizedValue(num2);
						pistonOnEdge.m_Slider.SetVisibilityExpireTime();
						BridgeActions.TranslatePistonSlider(pistonOnEdge, num2 - normalizedValue);
						num++;
					}
				}
			}
			if (SandboxSettings.m_SpringAdjustmentsAllowed && item.IsSpring() && item.m_SpringCoilVisualization != null)
			{
				float normalizedValue2 = item.m_SpringCoilVisualization.m_Slider.GetNormalizedValue();
				float num3 = Mathf.Clamp01(normalizedValue2 + springsDelta / 2f);
				if (!Mathf.Approximately(Mathf.Abs(normalizedValue2 - num3), 0f))
				{
					item.m_SpringCoilVisualization.m_Slider.SetNormalizedValue(num3);
					item.m_SpringCoilVisualization.m_Slider.SetVisibilityExpireTime();
					BridgeActions.TranslateSpringSlider(item.m_SpringCoilVisualization.m_Slider.m_BridgeSpring, num3 - normalizedValue2);
					item.m_SpringCoilVisualization.RefreshVisualization();
					num++;
				}
			}
		}
		if (num > 0)
		{
			InterfaceAudio.Play("ui_menu_select");
			BridgeActions.FlushRecording();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
			BridgeActions.CancelRecording();
		}
	}
}
