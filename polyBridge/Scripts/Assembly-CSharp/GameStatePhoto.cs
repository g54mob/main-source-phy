using Poly.Game;
using Poly.Math;
using UnityEngine;

public class GameStatePhoto
{
	public static bool m_CameraInTransition;

	private static bool m_IsFirstTimeEnteringPhoto = true;

	private static string m_ItemID;

	public static void Enter(GameState prevState)
	{
		Theme.m_Instance.SetThemeVolume();
		if (!WorkshopPreview.m_IsTakingScreenshot)
		{
			EnableUI();
		}
		Game.SetCameraCullingMasks(GameState.SIM);
		ForceDisableBridgeParts(WorkshopSubmit.m_AutoPlay, WorkshopSubmit.m_ShowPrebuilds);
		SetWaterProperties();
		SetAmbientLightingColor();
		if (m_IsFirstTimeEnteringPhoto)
		{
			PointsOfView.m_PointsOfView[PointOfViewType.PHOTO].FrameObjects(Game.GetLevelId());
			m_IsFirstTimeEnteringPhoto = false;
		}
		PointsOfView.RotateTo(PointOfViewType.PHOTO, 0f);
		m_CameraInTransition = true;
		TriggerCallbackManager.OnEnterSim();
		GroupSelect.Cancel();
		Bridge.CancelSelection();
		BridgeTrace.Hide(hide: true);
		BuildZones.EnableSpriteRendering(enabled: false);
		Checkpoints.EnterGameState(GameState.PHOTO);
		ClipboardManager.ClearClipboard();
		CustomShapes.EnterSimMode();
		CustomShapes.HidePinsForStaticShapes();
		CustomShapes.HideExternalPins();
		Decors.SetVisibility(GameState.SIM);
		GameGrid.m_Grid.SetActive(value: false);
		GameUI.SetPointerMode(PointerMode.NORMAL);
		GameUI.m_Instance.m_TopBar.UpdateLevelNavButtons();
		GameStateCommonInput.DisableMousePanIfButtonDown();
		Bridge.HideAllUI();
		BridgeEdges.SetDefaultColors();
		BridgeEdges.InitFX();
		BridgeJoints.MakeDefaultColor();
		BridgeJoints.RefreshCaps();
		BridgePillars.DisableOutlines();
		BridgePillars.InitFX();
		CustomShapes.ShowAnchorMeshes(on: true);
		Decors.Hide(hide: false);
		GameRenderSettings.SetShadows((Profiles.m_ActiveProfile.m_ShadowResolution != ShadowResolution.OFF) ? true : false);
		SandboxItems.DisableFloatingText();
		SandboxItems.DisableOutlines();
		Cameras.EnableSky();
		Theme.m_Instance.EnableSimModeLighting();
		Theme.m_Instance.PositionWaterPlane();
		Outlines.Disable();
		Vehicles.TurnWheelFillMeshesOff();
		WaterBlocks.RefreshScale();
		WaterBlocks.EnableMeshRenderers(!SandboxSettings.m_NoWater);
		WorldBounds.Hide();
		WaterRulers.Disable();
		CuttingPlanes.m_Instance.PositionCuttingPlanes();
		Vehicles.EnableMeshRendering();
		ZedAxisVehicles.EnableMeshRendering();
		ZedAxisVehicles.Disable();
		ZedAxisVehicles.PositionAtCenterAndActivate();
		ZedAxisVehicles.LinkToCuttingPlane(CuttingPlanes.m_Instance.m_North.gameObject, CuttingPlanes.m_Instance.m_South.gameObject);
		GameStateBuild.ClearFirstBreakEdge();
		HeightFog.SetDirectionalLight(Theme.m_Instance.m_SunLight);
		HeightFog.Enable(!Theme.m_Instance.FogIsZeroHeight());
		TerrainIslands.HideSecondPassMeshRenderers(hide: true);
		TerrainIslands.SetActiveBasedOnHiddenFlag();
		TerrainIslands.StartParticleSystems();
		TerrainIslands.StartWaterFalls();
		TerrainLights.TurnOn(Profiles.m_ActiveProfile.m_TerrainLights);
		WaterLine.Enable(enable: false);
		AdjustTerrainIslandsToPreventZFighting();
		AdjustCustomShapesToPreventZFighting();
		MaybeHideTerrain();
		if (!SandboxSettings.m_HydraulicControllerEnabled)
		{
			BridgeEdges.ClampJointSelectorsToTwoWay();
		}
		if (!Game.m_TakingScreenshotForWorkshopSubmit)
		{
			GameUI.m_Instance.m_TopBar.m_SandboxUndoRedoPanel.SetActive(value: false);
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_TAKE_PHOTO"), GamepadButtonType.DPAD_LEFT, Localize.Get("BINDING_CYCLE_SIM_VIEW"), GamepadButtonType.NORTH, Localize.Get("UI_ROTATE_CAMERA_HOLD"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		}
		GameUI.m_Instance.m_TopBar.m_DecorViewToggle.gameObject.SetActive(value: false);
	}

	public static void Exit(GameState nextState)
	{
		if ((bool)CameraControl.instance)
		{
			CameraControl.instance.isSimActive = false;
		}
		PointsOfView.Set(PointOfViewType.PHOTO, PointsOfView.m_Pivot, Cameras.MainCamera().transform.position, Cameras.MainCamera().transform.rotation, Cameras.MainCamera().orthographicSize);
		Theme.m_Instance.StopAmbientAudio();
		Checkpoints.ResetScale();
		CustomShapes.ShowAnchorMeshes(on: false);
		BridgeRopes.DestroyAll();
		BridgeJoints.ForceStopFlashingOfJoints();
		BridgeJoints.m_FlashingJoints.Clear();
		ZedAxisVehicles.UnlinkFromCuttingPlane();
		ZedAxisVehicles.Disable();
		AudioEmitters.StopAll();
		WaterSplash.DestroyAll();
		GameUI.m_Instance.m_Recenter.gameObject.SetActive(value: false);
		if (nextState == GameState.BUILD || nextState == GameState.SANDBOX || nextState == GameState.DECOR)
		{
			BridgeJoints.DisableJointCaps();
			BridgeEdges.EnableJointCaps();
		}
		m_CameraInTransition = false;
		GameUI.m_Instance.m_TopBar.m_PhotoModeParent.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_MainMenuButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_BuildToolBar.m_TrashButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_BuildToolBar.m_UndoButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_BuildToolBar.m_RedoButton.gameObject.SetActive(value: true);
		if (nextState == GameState.SANDBOX || nextState == GameState.DECOR)
		{
			GameUI.m_Instance.m_TopBar.m_SandboxUndoRedoPanel.SetActive(value: true);
		}
		GameStateCommonInput.StopZooming();
		UndoForceDisabled();
		Game.m_TakingScreenshotForWorkshopSubmit = false;
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
		CameraRotate.m_Instance.UpdateManual();
		HeightFog.UpdateProperties();
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

	public static void LeavePhotoMode()
	{
		GameStateManager.SwitchToState(GameStateManager.GetPrevState());
		GameUI.m_Instance.m_WorkshopSubmit.OpenFromPhotoMode(m_ItemID);
	}

	public static void SetItemID(string itemID)
	{
		m_ItemID = itemID;
	}

	private static void EnableUI()
	{
		GameUI.m_Instance.m_TopBar.gameObject.SetActive(!GameUI.m_DisableHud && GameInput.GetActiveGameDevice() != GameDevice.Gamepad);
		GameUI.m_Instance.m_TopBar.m_PhotoModeParent.SetActive(value: true);
		GameUI.m_Instance.m_TopBar.m_CostAndBudget.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ButtonContainerSpeed.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ButtonContainerPauseResume.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_LevelInfo.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_LevelNavButtons.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ModeToggle.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_SimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ExitSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_PauseSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_UnPauseSimButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_HelpButton.interactable = false;
		GameUI.m_Instance.m_TopBar.m_GodModeParent.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ReplayButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_MainMenuButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_BottomBar.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_LiveStress.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_ShowDecorParent.SetActive(value: false);
		SandboxUI.DeActivateAllPanels();
	}

	public static void UpdateForCurrentDevice()
	{
		if (GameStateManager.GetState() == GameState.PHOTO)
		{
			GameUI.m_Instance.m_TopBar.gameObject.SetActive(!GameUI.m_DisableHud && GameInput.GetActiveGameDevice() != GameDevice.Gamepad);
		}
	}

	public static void ForceDisableBridgeParts(bool showBridge, bool showPrebuilds)
	{
		if (!showBridge)
		{
			if (showPrebuilds)
			{
				BridgeEdges.ForceDisableAllExceptPrebuilt();
				BridgePillars.ForceDisableAllExceptPrebuilt();
			}
			else
			{
				Cameras.MainCamera().cullingMask &= ~(Utils.JOINT_LAYER_MASK | Utils.EDGE_LAYER_MASK | Utils.PISTON_LAYER_MASK | Utils.BRIDGE_PILLAR_LAYER_MASK);
			}
		}
	}

	public static void UndoForceDisabled()
	{
		BridgeEdges.UndoForceDisabled();
		BridgePillars.UndoForceDisabled();
	}

	private static void DoActionsWhenTransitionCompleted()
	{
		if ((bool)CameraControl.instance && CameraControl.instance.enabled)
		{
			Bounds2 bounds = PointsOfView.CalcBoundsForNewCameraController();
			Bounds renderingBounds = PointsOfView.Calc3dBoundsForGameCamera();
			CameraControl.instance.isSimActive = true;
			CameraControl.instance.Init(bounds, renderingBounds);
		}
	}

	private static void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInput())
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			LeavePhotoMode();
		}
		else if (!GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy)
		{
			if (GameInput.JustPressed(BindingType.CYCLE_SIM_VIEW))
			{
				GameUI.m_Instance.m_SimToolBar.OnCycleView();
			}
			if (Input.GetKeyDown(KeyCode.Space) || GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH))
			{
				InterfaceAudio.Play("ui_take_photo");
				SandboxSettings.SaveThumbnailCamera(Cameras.MainCamera());
				WorkshopPreview.TakeScreenshot(WorkshopSubmit.m_AutoPlay, WorkshopSubmit.m_ShowPrebuilds);
				GameStateManager.SwitchToState(GameStateManager.GetPrevState());
				GameUI.m_Instance.m_WorkshopSubmit.OpenFromPhotoMode(m_ItemID);
			}
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
		RenderSettings.ambientLight = Theme.m_Instance.m_ThemeStub.m_AmbientLightColor;
	}
}
