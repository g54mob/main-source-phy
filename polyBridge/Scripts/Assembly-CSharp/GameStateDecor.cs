using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Poly.Base;
using Poly.Game;
using Poly.Graphics;
using UnityEngine;
using Vectrosity;

public class GameStateDecor
{
	public static bool m_CameraInTransition;

	public static PointOfViewType m_PointOfViewType = PointOfViewType.DECOR_CENTER;

	private static bool m_ignoreNextSelection;

	private static Vector2 m_StartPanScreenPos;

	private static float m_StartPanTime;

	private static bool m_RestoreCamera;

	private const float BRIDGELINE_WIDTH = 10f;

	private static List<VectorLine> m_BridgeLines = new List<VectorLine>();

	private static List<string> m_RestoreSelectionGuids = new List<string>();

	public static void Enter(GameState prevState)
	{
		if (m_BridgeLines.Count == 0)
		{
			CreateBridgeLines();
		}
		else
		{
			UpdateBridgeLines();
		}
		SetBridgeLinesActive(active: false);
		Theme.m_Instance.SetThemeVolume();
		EnableUI();
		Game.SetCameraCullingMasks(GameState.DECOR);
		SetWaterProperties();
		if (m_PointOfViewType != PointOfViewType.DECOR_TOP && (prevState == GameState.SANDBOX || prevState == GameState.BUILD || (prevState == GameState.SIM && Profiles.m_ActiveProfile.m_LockBuildCamera)))
		{
			DoActionsWhenTransitionCompleted();
		}
		else if (m_RestoreCamera)
		{
			PointsOfView.RotateTo(PointOfViewType.DECOR_CUSTOM, GameSettings.TransitionTimeSeconds());
			m_CameraInTransition = true;
		}
		else
		{
			PointsOfView.SnapTo(m_PointOfViewType);
			DoActionsWhenTransitionCompleted();
		}
		GroupSelect.Cancel();
		Bridge.CancelSelection();
		BridgeTrace.Hide(hide: true);
		BuildZones.EnableSpriteRendering(enabled: false);
		Checkpoints.EnterGameState(GameState.DECOR);
		ClipboardManager.ClearClipboard();
		CustomShapes.EnterSimMode();
		CustomShapes.HidePinsForStaticShapes();
		CustomShapes.HideExternalPins();
		Decors.SetVisibility(GameState.SIM);
		GameUI.SetPointerMode(PointerMode.NORMAL);
		GameUI.m_Instance.m_TopBar.UpdateLevelNavButtons();
		GameStateCommonInput.DisableMousePanIfButtonDown();
		Bridge.HideAllUI();
		BridgeJoints.ChangeAnchorsLayer(Utils.JOINT_LAYER, Utils.DECOR_LAYER);
		BridgeJoints.DisableAnchorsCollision(disable: true);
		BridgeJoints.OverrideAnchorFX_Z(-10f);
		BridgeEdges.SetDefaultColors();
		BridgeEdges.InitFX();
		BridgeJoints.MakeDefaultColor();
		BridgeJoints.RefreshCaps();
		BridgePillars.DisableOutlines();
		BridgePillars.InitFX();
		CustomShapes.ShowAnchorMeshes(on: true);
		Decors.Hide(hide: false);
		SandboxItems.DisableFloatingText();
		SandboxItems.DisableOutlines();
		Cameras.DisableSky();
		Cameras.EnableBuildModeSky();
		Outlines.Disable();
		Vehicles.TurnWheelFillMeshesOff();
		WaterBlocks.RefreshScale();
		WaterBlocks.EnableMeshRenderers(!SandboxSettings.m_NoWater);
		WorldBounds.Hide();
		WaterLine.Enable(enable: false);
		CuttingPlanes.m_Instance.PositionCuttingPlanes();
		Vehicles.EnableMeshRendering();
		ZedAxisVehicles.PositionAtStartingZ();
		ZedAxisVehicles.LinkToCuttingPlane(CuttingPlanes.m_Instance.m_North.gameObject, CuttingPlanes.m_Instance.m_South.gameObject);
		ZedAxisVehicles.Enable();
		ZedAxisVehicles.Hide(hide: true);
		GameStateBuild.ClearFirstBreakEdge();
		TerrainIslands.SetActiveBasedOnHiddenFlag();
		TerrainIslands.StopParticleSystems();
		TerrainIslands.StopWaterFalls();
		TerrainLights.TurnOn(on: false);
		AdjustTerrainIslandsToPreventZFighting();
		AdjustCustomShapesToPreventZFighting();
		MaybeHideTerrain();
		if (!SandboxSettings.m_HydraulicControllerEnabled)
		{
			BridgeEdges.ClampJointSelectorsToTwoWay();
		}
		SandboxSelectionSet.SelectItemsMatchingGuids(m_RestoreSelectionGuids);
		Theme.m_Instance.EnableWaterPlane(on: false);
		WaterBlocks.EnableMeshRenderers(enable: false);
		Game.SetTimeScale(0f);
		GameUI.m_Instance.m_EventEditor.gameObject.SetActive(value: false);
	}

	public static void Exit(GameState nextState)
	{
		if (nextState == GameState.SIM)
		{
			Bridge.m_BridgeRestore = BridgeSave.Serialize();
		}
		if ((bool)CameraControl.instance)
		{
			CameraControl.instance.isSimActive = false;
		}
		m_RestoreCamera = nextState == GameState.SIM || nextState == GameState.SANDBOX || nextState == GameState.BUILD;
		PointsOfView.Set(PointOfViewType.DECOR_CUSTOM, PointsOfView.m_Pivot, Cameras.MainCamera().transform.position, Cameras.MainCamera().transform.rotation, Cameras.MainCamera().orthographicSize);
		GameUI.ClosePanelsWhenSwitchingModes();
		Theme.m_Instance.StopAmbientAudio();
		TriggerCallbackManager.OnExitSim();
		Checkpoints.ResetScale();
		CustomShapes.ShowAnchorMeshes(on: false);
		BridgeRopes.DestroyAll();
		BridgeJoints.ChangeAnchorsLayer(Utils.DECOR_LAYER, Utils.JOINT_LAYER);
		BridgeJoints.DisableAnchorsCollision(disable: false);
		BridgeJoints.RestoreAnchorFX_Z();
		BridgeJoints.ForceStopFlashingOfJoints();
		BridgeJoints.m_FlashingJoints.Clear();
		ZedAxisVehicles.UnlinkFromCuttingPlane();
		ZedAxisVehicles.Disable();
		ZedAxisVehicles.Hide(hide: false);
		AudioEmitters.StopAll();
		WaterSplash.DestroyAll();
		GameUI.m_Instance.m_Recenter.gameObject.SetActive(value: false);
		Cameras.DisableBuildModeSky();
		MasterAudio.StopBus("Simulation");
		MasterAudio.StopBus("Sim - Vehicle");
		MasterAudio.StopBus("Sim - HydraulicLoop");
		Cameras.PauseRecording();
		m_CameraInTransition = false;
		m_RestoreSelectionGuids.Clear();
		foreach (SandboxItem item in SandboxSelectionSet.m_Items)
		{
			m_RestoreSelectionGuids.Add(item.m_UndoGuid);
		}
		SandboxItems.CancelNewUnplacedItem();
		SandboxSelectionSet.RevertSelectionSetToStartPositions();
		SandboxSelectionSet.CancelSelection();
		if ((bool)GameUI.m_Instance)
		{
			GameUI.m_Instance.m_TopBar.m_PausedSim = false;
		}
		if (nextState == GameState.SANDBOX)
		{
			GameUI.m_Instance.m_EventEditor.gameObject.SetActive(value: true);
		}
		if (nextState != GameState.PHOTO)
		{
			GameUI.m_Instance.m_TopBar.m_SandboxUndoRedoPanel.SetActive(value: false);
			GameUI.m_Instance.m_TopBar.m_DecorViewToggle.gameObject.SetActive(value: false);
		}
		GameGrid.m_Grid.transform.rotation = Quaternion.identity;
		Cameras.ExitDecorMode();
		SetBridgeLinesActive(active: false);
		Bridge.m_Simulating = false;
		SingletonBehaviour<GpuInstancer>.instance?.Reset();
		GameStateCommonInput.StopZooming();
	}

	public static void UpdateManual()
	{
		if (m_CameraInTransition && !CameraInterpolate.IsActive())
		{
			m_CameraInTransition = false;
			DoActionsWhenTransitionCompleted();
		}
		SandboxUI.UpdateSandboxMenu(m_CameraInTransition);
		SandboxItems.UpdateManual();
		CustomShapes.MaybeDisableMeshRendering();
		HeightFog.UpdateProperties();
		if (!m_CameraInTransition && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			ProcessInput();
			GameStateCommonInput.Process();
			SandboxInput.UpdateContinuousHold();
		}
		float a = Vector3.Dot(Cameras.MainCamera().transform.forward, Vector3.forward);
		WaterLine.Enable(Mathf.Approximately(a, 1f));
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				joint.m_FX.gameObject.SetActive(Game.InDecorModeFrontView() && Mathf.Approximately(a, 1f));
			}
		}
		float a2 = Vector3.Dot(Cameras.MainCamera().transform.forward, -Vector3.up);
		SetBridgeLinesActive(Game.InDecorModeTopView() && Mathf.Approximately(a2, 1f));
		GameGrid.m_Grid.transform.rotation = Cameras.MainCamera().transform.rotation;
		UpdateGamepadLegend();
	}

	public static void LateUpdateManual()
	{
		if (!m_CameraInTransition)
		{
			CinemaCamera.UpdateManual();
		}
		CustomShapes.LateUpdateManual();
	}

	public static void FixedUpdateManual()
	{
	}

	public static bool CameraInTransition()
	{
		return m_CameraInTransition;
	}

	public static void OnLayoutLoaded()
	{
		m_RestoreCamera = false;
		m_RestoreSelectionGuids.Clear();
	}

	public static void Reset()
	{
		m_ignoreNextSelection = false;
	}

	public static void ForceIgnoreNextSelection()
	{
		m_ignoreNextSelection = true;
	}

	private static void EnableUI()
	{
		GameUI.m_Instance.m_TopBar.gameObject.SetActive(!GameUI.m_DisableHud);
		GameUI.m_Instance.m_TopBar.m_SandboxUndoRedoPanel.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_CostAndBudget.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ButtonContainerSpeed.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ButtonContainerPauseResume.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_LevelInfo.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_LevelNavButtons.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ModeToggle.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_DecorViewToggle.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_SimButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_ExitSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_PauseSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_UnPauseSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_HelpButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ReplayButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_GodModeParent.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ShowDecorParent.SetActive(value: false);
		GameUI.m_Instance.m_BottomBar.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_LiveStress.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Selection.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_Clipboard.gameObject.SetActive(value: false);
	}

	private static void DoActionsWhenTransitionCompleted()
	{
		SetAmbientLightingColor();
		Theme.m_Instance.DisableModeLighting();
		Main.m_Instance.m_PostFX.SetForDecor();
		Cameras.EnterDecorMode();
		Cameras.DecorCamera().cullingMask = Utils.DECOR_LAYER_MASK;
		Cameras.ForegroundCamera().cullingMask = 0;
		Cameras.MainCamera().cullingMask = 0;
		Cameras.RenderLastCamera().cullingMask = ~(Cameras.DecorCamera().cullingMask | Utils.TRANSPARENT_FX_LAYER_MASK | Utils.NO_RENDER_LAYER_MASK | Utils.BUILD_ZONE_LAYER_MASK);
		GameRenderSettings.SetShadows(on: false);
		HeightFog.Enable(on: false);
		GameGrid.m_Grid.SetActive(value: true);
		MaybeSubmitWorkshopLevel();
	}

	private static void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInput())
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameStateCommonInput.ProcessEscapeKeypress();
		}
		if (GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy)
		{
			return;
		}
		SandboxInput.m_ForceIgnoreGrid = (GameInput.IsDown(BindingType.MOVE_OFF_GRID) ? true : false);
		if (GameInput.IsDown(BindingType.MULTI_SELECT) && ActivePanels.m_Panels.Count == 0 && ClipboardManager.IsEmpty())
		{
			GameUI.SetPointerMode(PointerMode.SELECT_TOGGLE);
		}
		else
		{
			GameUI.SetPointerMode(PointerMode.SELECT);
		}
		if (GameInput.GetMouseButtonJustReleased(0))
		{
			ProcessLeftClickUp(GameInput.GetMousePosition());
		}
		if (GameInput.GetMouseButtonJustPressed(0))
		{
			ProcessLeftClickDown(GameInput.GetMousePosition());
		}
		if (GameInput.GetMouseButtonJustPressed(1))
		{
			ProcessRightClickDown(GameInput.GetMousePosition());
		}
		if (GameInput.GetMouseButtonJustReleased(1))
		{
			ProcessRightClickUp(GameInput.GetMousePosition());
		}
		if (GameInput.GetMouseButtonIsDown(0))
		{
			SandboxSelectionSet.MaybeMoveSelectionWithPointer();
		}
		if (GameInput.JustPressed(BindingType.START_SIM))
		{
			GameUI.m_Instance.m_TopBar.OnSim();
		}
		if (GameInput.JustPressed(BindingType.DELETE_SELECTION))
		{
			if (!SandboxSelectionSet.IsEmpty())
			{
				SandboxSelectionSet.Delete();
				InterfaceAudio.Play("ui_build_delete");
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GameInput.GetActiveGameDevice() == GameDevice.KeyboardAndMouse && GameInput.JustPressed(BindingType.CYCLE_SIM_VIEW) && !CameraInterpolate.IsActive())
		{
			GameUI.m_Instance.m_TopBar.m_DecorViewToggle.OnButton();
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
		if (GameInput.JustPressed(BindingType.SANDBOX_BUILD_SIM_CYCLE))
		{
			GameUI.m_Instance.m_TopBar.m_ModeToggle.OnButton();
		}
		if (!GameInput.IsDown(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL) && !GameInput.IsDown(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL))
		{
			SandboxSelectionSet.SetMovementConstraint(MovementConstraint.NONE);
		}
		if (GameInput.IsDown(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL))
		{
			SandboxSelectionSet.SetMovementConstraint(MovementConstraint.X_AXIS);
		}
		else if (GameInput.IsDown(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL))
		{
			SandboxSelectionSet.SetMovementConstraint(MovementConstraint.Y_AXIS);
		}
	}

	private static void DoMouseScrollWheel(float delta)
	{
		if (!Mathf.Approximately(delta, 0f))
		{
			float orthographicSize = Cameras.MainCamera().orthographicSize;
			Cameras.SetOrthographicSize(Mathf.Clamp((delta > 0f) ? (orthographicSize - 1f) : (orthographicSize + 1f), Game.MinOrthographicSize(), Game.MaxOrthographicSize()));
			Game.RefreshAfterOrthographicSizeChange();
		}
	}

	private static void AdjustTerrainIslandsToPreventZFighting()
	{
		int num = 2;
		foreach (TerrainIsland terrain in TerrainIslands.m_Terrains)
		{
			num = 2;
			foreach (TerrainIsland terrain2 in TerrainIslands.m_Terrains)
			{
				if (terrain != terrain2 && terrain.m_MeshRenderer.bounds.Intersects(terrain2.m_MeshRenderer.bounds) && Mathf.Approximately(terrain.m_MeshRenderer.transform.position.z, terrain2.m_MeshRenderer.transform.position.z))
				{
					float z = ((num % 2 == 0) ? (0.001f * (float)Mathf.FloorToInt((float)num / 2f)) : (-0.001f * (float)Mathf.FloorToInt((float)num / 2f)));
					terrain.m_MeshRenderer.transform.position += new Vector3(0f, 0f, z);
					num++;
				}
			}
		}
	}

	private static void AdjustCustomShapesToPreventZFighting()
	{
		int num = 2;
		foreach (CustomShape shape in CustomShapes.m_Shapes)
		{
			num = 2;
			foreach (CustomShape shape2 in CustomShapes.m_Shapes)
			{
				if (shape != shape2 && shape.m_PolygonCollider2D.bounds.Intersects(shape2.m_MeshRenderer.bounds) && Mathf.Approximately(shape.m_MeshRenderer.transform.position.z, shape2.m_MeshRenderer.transform.position.z))
				{
					if (num % 2 == 0)
					{
						shape.m_MeshRenderer.transform.position += new Vector3(0f, 0f, 0.001f * (float)Mathf.FloorToInt((float)num / 2f));
					}
					else
					{
						shape.m_MeshRenderer.transform.position -= new Vector3(0f, 0f, 0.001f * (float)Mathf.FloorToInt((float)num / 2f));
					}
					num++;
				}
			}
		}
	}

	private static void MaybeHideTerrain()
	{
		TerrainIslands.Hide(TerrainIslands.m_Hide);
		Rocks.Hide(TerrainIslands.m_Hide);
		Pillars.Hide(TerrainIslands.m_Hide);
	}

	private static void SetWaterProperties()
	{
		WaterBlocks.EnableWaves();
	}

	private static void SetAmbientLightingColor()
	{
		RenderSettings.ambientLight = PostFX.m_Instance.m_BuildAmbientLightColor;
	}

	private static bool CanSendCustomEventForCampaignLevel()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			return !Campaign.m_CampaignProgress.HasCompletedLevel(Campaign.m_CurrentLevel.m_Id);
		}
		return false;
	}

	private static void ProcessLeftClickUp(Vector2 screenPos)
	{
		if (SandboxSelectionSet.SelectionFollowsMouse())
		{
			SandboxSelectionSet.FinalizeMovement();
			BridgeEffects.PlayErrorEffectAtFirstIllegalNodePosition();
		}
		if ((bool)SandboxItems.m_NewUnPlacedItem)
		{
			SandboxItems.PlaceNewItem(SandboxItems.m_NewUnPlacedItem);
			SandboxItems.m_NewUnPlacedItem = null;
		}
		else if (!GameUI.IsPointerOverGameObject())
		{
			if (!m_ignoreNextSelection && !SandboxSelectionSet.SelectionFollowsMouse() && !GameUI.IsPointerOverGameObject())
			{
				SandboxItems.TrySelectItem(GameInput.GetMousePosition());
			}
			if (!m_ignoreNextSelection && !SandboxItems.GetItemUnderPos(GameInput.GetMousePosition()) && GameUI.GetPointerMode() != PointerMode.SELECT_TOGGLE)
			{
				SandboxSelectionSet.CancelSelection();
			}
		}
		SandboxSelectionSet.CancelSelectionFollowingMouse();
		Sandbox.m_AllowedToPanCamera = false;
		m_ignoreNextSelection = false;
	}

	private static void ProcessLeftClickDown(Vector2 screenPos)
	{
		SandboxItem itemUnderPos = SandboxItems.GetItemUnderPos(GameInput.GetMousePosition());
		if ((bool)itemUnderPos && itemUnderPos.IsLocked())
		{
			Sandbox.m_AllowedToPanCamera = true;
			return;
		}
		if ((bool)itemUnderPos && !SandboxSelectionSet.SelectionFollowsMouse() && !GameUI.IsPointerOverGameObject())
		{
			if (!SandboxSelectionSet.m_Items.Contains(itemUnderPos))
			{
				SandboxItems.TrySelectItem(GameInput.GetMousePosition());
				m_ignoreNextSelection = true;
			}
			else
			{
				GameUI.m_Instance.m_SandboxMenu.MaybeActivateEditSubmenu();
			}
		}
		if ((bool)itemUnderPos && SandboxSelectionSet.m_Items.Contains(itemUnderPos) && GameUI.GetPointerMode() != PointerMode.SELECT_TOGGLE && !GameUI.IsPointerOverGameObject())
		{
			SandboxSelectionSet.StartMoving(itemUnderPos);
		}
		else
		{
			Sandbox.m_AllowedToPanCamera = itemUnderPos == null || !GameUI.m_Instance.m_SandboxMenu.m_PointerEvents.m_IsHovering;
		}
		if (GameUI.m_Instance.m_SandboxMenu.m_PointerEvents.m_IsHovering)
		{
			m_ignoreNextSelection = true;
		}
	}

	private static void ProcessRightClickUp(Vector2 screenPos)
	{
		if (GroupSelect.IsActive())
		{
			if (GameUI.GetPointerMode() != PointerMode.SELECT_TOGGLE)
			{
				SandboxSelectionSet.CancelSelection();
			}
			SandboxSelectionSet.SelectAllDecorInRect((m_PointOfViewType == PointOfViewType.DECOR_TOP) ? GroupSelect.GetRectXZ() : GroupSelect.GetRect(), GameInput.MultiSelectIsDown());
			if (!SandboxSelectionSet.SelectionSetMatchesStoredGuids())
			{
				SandboxUndo.SnapShot();
			}
		}
		else if (!m_ignoreNextSelection && !SandboxSelectionSet.SelectionFollowsMouse() && !GameUI.IsPointerOverGameObject())
		{
			SandboxItems.TrySelectItem(GameInput.GetMousePosition());
		}
		m_ignoreNextSelection = false;
		GroupSelect.Cancel();
	}

	private static void ProcessRightClickDown(Vector2 screenPos)
	{
		m_StartPanScreenPos = GameInput.GetMousePosition();
		m_StartPanTime = Time.realtimeSinceStartup;
		if (SandboxSelectionSet.SelectionFollowsMouse())
		{
			SandboxSelectionSet.FinalizeMovement();
		}
		if (!GameUI.IsPointerOverGameObject() && !GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy && !SandboxSelectionSet.SelectionFollowsMouse() && GameToolMode.GetMode() != GameToolModeType.ERASE)
		{
			GroupSelect.Start(screenPos);
		}
		if (GameUI.GetPointerMode() != PointerMode.SELECT_TOGGLE)
		{
			SandboxItem itemUnderPos = SandboxItems.GetItemUnderPos(screenPos);
			if (!itemUnderPos || !SandboxSelectionSet.m_Items.Contains(itemUnderPos))
			{
				SandboxSelectionSet.CancelSelection();
			}
		}
	}

	private static void CreateBridgeLines()
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		Vector3 vector = new Vector3(0f, 0f, GameSettings.BridgeWidth() / 2f);
		m_BridgeLines.Add(CreateBridgeLine(leftTerrain.transform.position + vector, rightTerrain.transform.position + vector));
		m_BridgeLines.Add(CreateBridgeLine(leftTerrain.transform.position - vector, rightTerrain.transform.position - vector));
	}

	private static void UpdateBridgeLines()
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		Vector3 vector = new Vector3(0f, 0f, GameSettings.BridgeWidth() / 2f);
		m_BridgeLines[0].points3[0] = leftTerrain.transform.position + vector;
		m_BridgeLines[0].points3[1] = rightTerrain.transform.position + vector;
		m_BridgeLines[1].points3[0] = leftTerrain.transform.position - vector;
		m_BridgeLines[1].points3[1] = rightTerrain.transform.position - vector;
	}

	private static VectorLine CreateBridgeLine(Vector3 start, Vector3 end)
	{
		VectorLine vectorLine = new VectorLine("Bridge Line", new List<Vector3>(), GameUI.m_Instance.m_ChalkLine2D, 10f);
		if (vectorLine == null)
		{
			return null;
		}
		vectorLine.Draw3DAuto();
		vectorLine.points3.Add(start);
		vectorLine.points3.Add(end);
		vectorLine.layer = Utils.DECOR_LAYER;
		vectorLine.textureScale = 1f;
		vectorLine.color = Color.white;
		vectorLine.AddNormals();
		return vectorLine;
	}

	private static void SetBridgeLinesActive(bool active)
	{
		foreach (VectorLine bridgeLine in m_BridgeLines)
		{
			bridgeLine.active = active;
		}
	}

	public static void UpdateBridgeLinesWidth()
	{
		foreach (VectorLine bridgeLine in m_BridgeLines)
		{
			Outlines.UpdateWidthForOrthographicChange(bridgeLine, 10f);
		}
	}

	private static void MaybeSubmitWorkshopLevel()
	{
		if (WorkshopSubmit.m_RanSimulation)
		{
			if (WorkshopSubmit.m_SimulationPassed)
			{
				GameUI.m_Instance.m_WorkshopSubmit.SubmitAfterSimulation();
			}
			else
			{
				PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_WORKSHOP_SIM_FAIL"));
			}
			WorkshopSubmit.m_RanSimulation = false;
		}
	}

	private static void UpdateGamepadLegend()
	{
		if (ActivePanels.m_Panels.Count > 0 || m_CameraInTransition)
		{
			return;
		}
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		if (SandboxSelectionSet.IsEmpty())
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_GROUP_SELECT"));
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"));
			return;
		}
		if (SandboxSelectionSet.MultipleItemsSelected())
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_ALL, Localize.Get("UI_MOVE"));
			return;
		}
		if (SandboxSelectionSet.GetSelectedItem().m_Type == SandboxItemType.DECOR)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("UI_SANDBOX_DELETE"), GamepadButtonType.NORTH, Localize.Get("UI_SANDBOX_DUPLICATE"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
		}
		GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_ALL, Localize.Get("UI_MOVE"));
	}
}
