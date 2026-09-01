using System.Collections.Generic;
using UnityEngine;

public class GameStateSandbox
{
	public static bool m_CameraInTransition;

	private static readonly float GAME_GRID_Z_POS = 51f;

	private static bool m_RestoreCamera;

	private static GameState m_PrevStateOnEnter;

	private static List<string> m_RestoreSelectionGuids = new List<string>();

	public static void Enter(GameState prevState)
	{
		if (prevState != GameState.PHOTO)
		{
			GameUI.ClearMessages();
		}
		m_PrevStateOnEnter = prevState;
		if (TerrainIslands.m_Terrains.Count == 0)
		{
			string randomNonLegacyAddressableName = ThemeStubs.m_Instance.GetRandomNonLegacyAddressableName();
			if (Prefabs.AsyncPrefabExists(randomNonLegacyAddressableName))
			{
				Sandbox.StartNewSandbox(ThemeStubs.m_Instance.GetIdFromName(randomNonLegacyAddressableName));
				EnterWithPreloadedTheme(spawnRandomVehicle: true);
			}
			else
			{
				GameStatePreloadingAssets.PreloadTheme(randomNonLegacyAddressableName, null, PreloadThemeCallback);
			}
		}
		else
		{
			EnterWithPreloadedTheme(spawnRandomVehicle: false);
		}
	}

	private static void PreloadThemeCallback(string themeAdressableName, FileSlot slot)
	{
		Sandbox.StartNewSandbox(ThemeStubs.m_Instance.GetIdFromName(themeAdressableName));
		EnterWithPreloadedTheme(spawnRandomVehicle: true);
	}

	public static void EnterWithPreloadedTheme(bool spawnRandomVehicle)
	{
		Bridge.CancelSelection();
		BridgeTrace.Hide(hide: true);
		Bridge.UpdateManual();
		BridgeEdges.InitFX();
		BridgePillars.InitFX();
		BridgePillars.DisableOutlines();
		Budget.UpdateBridgeCost();
		Checkpoints.EnterGameState(GameState.SANDBOX);
		ClipboardManager.ClearClipboard();
		SandboxInput.Reset();
		if (!WorkshopPreview.m_IsTakingScreenshot)
		{
			SandboxUI.EnableUI();
		}
		GameUI.SetPointerMode(PointerMode.NORMAL);
		GameUI.m_Instance.m_PolyTwitchMain.ClearWindowMovement();
		GameUI.m_Instance.m_TopBar.OnSandboxEnter();
		GameGrid.SetZPos(GAME_GRID_Z_POS);
		GameGrid.CenterOnTerrainEdge(TerrainIslands.GetLeftTerrain());
		GameGrid.SetGridLayer(Utils.TERRAIN_LAYER);
		GameStateCommonInput.DisableMousePanIfButtonDown();
		Pistons.DisablePinions();
		SetCameraBackgroundColor();
		Outlines.UpdateOutlinesForStateChange(GameState.SANDBOX);
		TerrainIslands.ShrinkForSandboxMode(shrink: true);
		Checkpoints.EnableHotspotColliders(on: true);
		VehicleStopTriggers.EnableHotspotColliders(on: true);
		Vehicles.ShowCenterOfMass(Sandbox.m_ShowVehicleCenterOfMass);
		if (m_PrevStateOnEnter == GameState.BUILD)
		{
			DoActionsRegardlessOfTransition();
			BridgeEdges.HideJointSelectorUI();
			return;
		}
		if (Profiles.m_ActiveProfile.m_LockBuildCamera && m_PrevStateOnEnter == GameState.SIM && !Profiles.m_ActiveProfile.m_FollowCar)
		{
			DoActionsWhenTransitionCompleted();
		}
		else if (m_RestoreCamera)
		{
			if (m_PrevStateOnEnter == GameState.DECOR && GameStateDecor.m_PointOfViewType != PointOfViewType.DECOR_TOP)
			{
				PointsOfView.SnapTo(PointOfViewType.DECOR_CUSTOM);
			}
			else
			{
				float durationSeconds = ((m_PrevStateOnEnter == GameState.DECOR || WorkshopPreview.m_IsTakingScreenshot) ? 0f : GameSettings.TransitionTimeSeconds());
				PointsOfView.RotateTo(PointOfViewType.BUILD_CUSTOM, durationSeconds);
			}
			m_CameraInTransition = true;
		}
		else
		{
			PointsOfView.SnapTo(PointOfViewType.BUILD);
			DoActionsWhenTransitionCompleted();
		}
		if (GameUI.m_Instance.m_SandboxMenu.m_SandboxTabsPanel.DecorIsActiveTab())
		{
			GameUI.m_Instance.m_SandboxMenu.m_SandboxTabsPanel.SelectDefaultTab();
		}
		SandboxSelectionSet.SelectItemsMatchingGuids(m_RestoreSelectionGuids);
		GameGrid.m_Grid.transform.rotation = Quaternion.identity;
		Game.SetTimeScale(0f);
		if (spawnRandomVehicle && Sandbox.m_SpawnRandomVehicle)
		{
			Sandbox.CreateDefaultVehicle();
		}
		UpdateMainCameraDecorMask();
		m_PrevStateOnEnter = GameState.INVALID;
	}

	public static void Exit(GameState nextState)
	{
		Bridge.Sanitize();
		if (nextState == GameState.SIM)
		{
			Bridge.m_BridgeRestore = BridgeSave.Serialize();
			PolyTwitchAutoPlay.MaybeLoadForSimulation();
		}
		SandboxItems.CancelNewUnplacedItem();
		SandboxItems.DestroyImposters();
		SandboxSelectionSet.RevertSelectionSetToStartPositions();
		m_RestoreSelectionGuids.Clear();
		foreach (SandboxItem item in SandboxSelectionSet.m_Items)
		{
			m_RestoreSelectionGuids.Add(item.m_UndoGuid);
		}
		SandboxSelectionSet.CancelSelection();
		GameUI.m_Instance.m_SandboxMenu.ActivateSandboxSubMenu(null);
		m_RestoreCamera = nextState == GameState.SIM || nextState == GameState.DECOR;
		PointsOfView.Set(PointOfViewType.BUILD_CUSTOM, PointsOfView.m_Pivot, Cameras.MainCamera().transform.position, Cameras.MainCamera().transform.rotation, Cameras.MainCamera().orthographicSize);
		if (!WorkshopPreview.m_IsTakingScreenshot)
		{
			WaterRulers.Disable();
			GameUI.m_Instance.m_TopBar.m_MessageTopLeft.UnpinMessage();
		}
		GameUI.m_Instance.m_Recenter.gameObject.SetActive(value: false);
		if (nextState != GameState.PHOTO)
		{
			GameUI.m_Instance.m_TopBar.m_SandboxUndoRedoPanel.SetActive(value: false);
		}
		GameUI.ClosePanelsWhenSwitchingModes();
		GameGrid.SetZPos(0f);
		GameGrid.SetGridLayer(Utils.RENDER_LAST_LAYER);
		BridgeEffects.StopErrorFX();
		BuildZones.EnableSpriteRendering(enabled: false);
		TerrainIslands.ShrinkForSandboxMode(shrink: false);
		TerrainIslands.ClearDisplayVariantTimer();
		TerrainIslands.EnableCollisionMeshRenderer(on: false);
		EventEditor.ExitSandbox();
		Checkpoints.EnableHotspotColliders(on: false);
		VehicleStopTriggers.EnableHotspotColliders(on: false);
		Vehicles.TurnOffWheelsLine();
		Vehicles.ShowCenterOfMass(on: false);
		EnableSandboxItemRendering();
		WaterBlocks.RebuildMesh();
		WaterLine.Generate();
		Cameras.AbortRecording();
		DisableSandboxBackground();
		CustomShapes.UpdateSpawnTransform();
		Vehicles.UpdateSpawnTransform();
		ZedAxisVehicles.UpdateSpawnTransform();
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
		GameStateCommonInput.StopZooming();
		Sandbox.m_AllowedToPanCamera = false;
		m_CameraInTransition = false;
	}

	private static void EnableSandboxItemRendering()
	{
		BuildZones.EnableSpriteRendering(enabled: true);
		Checkpoints.EnableMeshRendering();
		CustomShapes.EnableMeshRendering();
		FlyingObjects.EnableMeshRendering();
		Pillars.EnableMeshRendering();
		Platforms.EnableMeshRendering();
		Ramps.EnableMeshRendering();
		Rocks.EnableMeshRendering();
		TerrainIslands.EnableMeshRendering();
		Vehicles.EnableMeshRendering();
		VehicleStopTriggers.EnableMeshRendering();
	}

	public static void UpdateManual()
	{
		if (!WorkshopPreview.m_IsTakingScreenshot)
		{
			if (m_CameraInTransition && !CameraInterpolate.IsActive())
			{
				m_CameraInTransition = false;
				DoActionsWhenTransitionCompleted();
			}
			SandboxUI.UpdateSandboxMenu(m_CameraInTransition);
			Sandbox.UpdateManual();
			EventEditor.UpdateManual();
			BridgeEdges.UpdateManualOutsideSim();
			Budget.UpdateManual();
			CustomShapes.MaybeDisableMeshRendering();
			Vehicles.ShowVehicleWheelsLine();
			if (SandboxSettings.m_NoWater)
			{
				WaterRulers.UpdateManual(Mathf.Max(WaterBlocks.MIN_HEIGHT, WaterBlocks.GetMaxHeight() - 2f));
			}
			else
			{
				WaterRulers.UpdateManual(WaterBlocks.GetHeight());
			}
			if (!string.IsNullOrEmpty(Sandbox.m_CurrentLayoutName))
			{
				GameUI.m_Instance.m_TopBar.m_MessageTopLeft.PinMessage(Sandbox.m_CurrentLayoutName);
			}
			if (!GameUI.PopupIsActive())
			{
				UpdateGamepadLegend();
			}
		}
	}

	public static void LateUpdateManual()
	{
		Sandbox.LateUpdateManual();
		if (!m_CameraInTransition)
		{
			Outlines.ManualUpdate();
		}
	}

	public static void FixedUpdateManual()
	{
	}

	public static void OnLayoutLoaded()
	{
		m_RestoreCamera = false;
		m_RestoreSelectionGuids.Clear();
		Decors.SetVisibility(GameState.SANDBOX);
	}

	public static bool AllowedToPanCameraWithMouse()
	{
		if (GameInput.GetActiveGameDevice() != GameDevice.KeyboardAndMouse)
		{
			return false;
		}
		if (Prefabs.AsyncLoadInProgress())
		{
			return false;
		}
		if (Sandbox.m_AllowedToPanCamera || GameInput.IsDown(BindingType.PAN_WITH_MOUSE))
		{
			return !SandboxSelectionSet.SelectionFollowsMouse();
		}
		return false;
	}

	public static void ObjectsEnterSandboxMode()
	{
		CustomShapes.EnterSandboxMode();
		BuildZones.EnterSandboxMode();
		Platforms.EnterSandboxMode();
		Ramps.EnterSandboxMode();
		Vehicles.EnterSandboxMode();
		ZedAxisVehicles.EnterSandboxMode();
		WaterLine.Enable(enable: false);
	}

	public static void UpdateMainCameraDecorMask()
	{
		if (Profiles.m_ActiveProfile.m_ShowDecor)
		{
			Cameras.m_Instance.m_Main.cullingMask |= Utils.DECOR_LAYER_MASK;
		}
		else
		{
			Cameras.m_Instance.m_Main.cullingMask &= ~Utils.DECOR_LAYER_MASK;
		}
	}

	private static void DoActionsWhenTransitionCompleted()
	{
		DoActionsRegardlessOfTransition();
		BridgeJoints.UnHideAllUI();
		CustomShapes.ShowAllPins();
		HeightFog.Enable(on: false);
		TerrainLights.TurnOn(on: false);
		TerrainIslands.StopParticleSystems();
		TerrainIslands.StopWaterFalls();
		MaybeSubmitWorkshopLevel();
	}

	private static void DoActionsRegardlessOfTransition()
	{
		SetAmbientLightingColor();
		Main.m_Instance.m_PostFX.SetForSandbox();
		Game.SetCameraCullingMasks(GameState.SANDBOX);
		UpdateMainCameraDecorMask();
		GameUI.m_Instance.m_SandboxMenu.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_EventEditor.gameObject.SetActive(value: true);
		if (GameUI.m_Instance.m_SandboxEditRamp.IsEditingSplinePoints())
		{
			GameUI.m_Instance.m_SandboxEditRamp.ExitSplineEditMode();
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxEditCustomShapeTools.ExitCustomShapeEditToolsMode();
		}
		TerrainIslands.EnableCollisionMeshRenderer(on: true);
		GameGrid.m_Grid.SetActive(value: true);
		GameRenderSettings.SetShadows(on: false);
		BridgeJoints.MakeGreyScale();
		BridgeJoints.HideSplitUI();
		BridgeJoints.HideHoverUI();
		BridgeJoints.DisableJointCaps();
		Checkpoints.DisableMeshes();
		Decors.SetVisibility(GameState.SANDBOX);
		Pistons.HideAllUI();
		BridgeSprings.HideAllUI();
		Cameras.DisableSky();
		TerrainIslands.SetActiveBasedOnHiddenFlag();
		Theme.m_Instance.EnableSandboxModeLighting();
		WorldBounds.Show();
		ZedAxisVehicles.Enable();
		WaterBlocks.RefreshScale();
		WaterBlocks.EnableMeshRenderers(enable: false);
		SandboxItems.EnableOutlines();
		ObjectsEnterSandboxMode();
		EnableSandboxBackground();
	}

	private static void SetCameraBackgroundColor()
	{
		Cameras.MainCamera().backgroundColor = GameUI.m_Instance.m_BlueprintBackgroundColor;
	}

	private static void SetAmbientLightingColor()
	{
		RenderSettings.ambientLight = PostFX.m_Instance.m_SandboxAmbientLightColor;
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

	private static void EnableSandboxBackground()
	{
		if (Cameras.m_Instance != null)
		{
			Cameras.m_Instance.m_SandboxSky.gameObject.SetActive(value: true);
		}
	}

	private static void DisableSandboxBackground()
	{
		if (Cameras.m_Instance != null)
		{
			Cameras.m_Instance.m_SandboxSky.gameObject.SetActive(value: false);
		}
	}

	private static void UpdateGamepadLegend()
	{
		if (m_CameraInTransition)
		{
			return;
		}
		if (!BridgeSelectionSet.IsEmpty() && SandboxSelectionSet.IsEmpty())
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.WEST, Localize.Get("TOOLTIP_PREBUILD_SOFTLOCK"), GamepadButtonType.NORTH, Localize.Get("TOOLTIP_PREBUILD_LOCK"), GamepadButtonType.DPAD_UP, Localize.Get("TOOLTIP_PREBUILD_UNLOCK"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			return;
		}
		if (SandboxSelectionSet.IsEmpty())
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_GROUP_SELECT"));
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"));
			return;
		}
		if (SandboxSelectionSet.MultipleItemsSelected())
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("UI_SANDBOX_DUPLICATE"), GamepadButtonType.WEST, Localize.Get("UI_SANDBOX_DELETE"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			if (SandboxSelectionSet.HasAtLeastOneMovableItem())
			{
				if (SandboxSelectionSet.AllMovableItemsAreWater())
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_VERTICAL, Localize.Get("UI_SANDBOX_HEIGHT"));
				}
				else
				{
					GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_ALL, Localize.Get("UI_MOVE"));
				}
			}
			else
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"));
			}
			return;
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			return;
		}
		SandboxItem selectedItem = SandboxSelectionSet.GetSelectedItem();
		switch (SandboxSelectionSet.GetSelectedItem().m_Type)
		{
		case SandboxItemType.ANCHOR:
		case SandboxItemType.VEHICLE:
		case SandboxItemType.PLATFORM:
		case SandboxItemType.RAMP:
		case SandboxItemType.FLYING_OBJECT:
		case SandboxItemType.ROCK:
		case SandboxItemType.ZED_AXIS_VEHICLE:
		case SandboxItemType.CUSTOM_SHAPE:
		case SandboxItemType.BUILD_ZONE:
		case SandboxItemType.PILLAR:
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("UI_SANDBOX_DUPLICATE"), GamepadButtonType.WEST, Localize.Get("UI_SANDBOX_DELETE"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			break;
		case SandboxItemType.CHECKPOINT:
		case SandboxItemType.HYDRAULICS_PHASE:
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("UI_SANDBOX_DELETE"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			break;
		case SandboxItemType.TERRAIN:
			if (selectedItem.GetComponent<TerrainIsland>().m_TerrainIslandType == TerrainIslandType.Middle)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("UI_SANDBOX_DUPLICATE"), GamepadButtonType.WEST, Localize.Get("UI_SANDBOX_DELETE"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			}
			else
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			}
			break;
		default:
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
			break;
		}
		if (selectedItem.IsMoveable())
		{
			if (selectedItem.m_Type == SandboxItemType.WATER)
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_VERTICAL, Localize.Get("UI_SANDBOX_HEIGHT"));
			}
			else
			{
				GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_ALL, Localize.Get("UI_MOVE"));
			}
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"));
		}
	}
}
