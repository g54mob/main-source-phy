using System;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Poly.Base;
using Poly.Determinism;
using Poly.Game;
using Poly.Graphics;
using Poly.Math;
using UnityEngine;

public class GameStateSim
{
	public static bool m_SkipBridgeRestoreOnExit;

	public static bool m_LevelPassed;

	public static bool m_LevelFailed;

	public static bool m_LevelHung;

	public static int m_Budget;

	public static int m_BudgetUsed;

	public static float m_MassUsed;

	public static int m_NumBridgeBreaks;

	public static bool m_CameraInTransition;

	public static bool m_CapturingReplayForSolution;

	public static float m_ElapsedSeconds;

	public static float LEVEL_PASSED_DIALOG_DELAY_SECONDS = 2f;

	public static float LEVEL_FAIL_DIALOG_DELAY_SECONDS = 1.75f;

	private static bool m_RestoreCamera;

	private static bool m_RecordingReplayStarted;

	private static float m_DisplayLevelEndDialogTimer;

	private static float m_TimeSinceFailOrPass;

	public static void Enter(GameState prevState)
	{
		Theme.m_Instance.SetThemeVolume();
		m_LevelFailed = false;
		m_LevelPassed = false;
		m_LevelHung = false;
		m_ElapsedSeconds = 0f;
		EnableUI();
		Game.SetCameraCullingMasks(GameState.SIM);
		SetWaterProperties();
		SetCameraBackgroundColor();
		SetAmbientLightingColor();
		StressSamples.Reset();
		if (Profiles.m_ActiveProfile.m_LockBuildCamera)
		{
			DoActionsWhenTransitionCompleted();
			GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CENTER);
		}
		else if (m_CapturingReplayForSolution)
		{
			PointsOfView.m_PointsOfView[DumpReplays.m_PointOfViewType].FrameObjects(Game.GetLevelId());
			PointsOfView.RotateTo(DumpReplays.m_PointOfViewType, 0f);
			m_CameraInTransition = false;
		}
		else if (Game.m_TakingScreenshotForAutoSave && !DumpPreviewImages.m_Dumping)
		{
			PointsOfView.m_PointsOfView[PointOfViewType.BUILD].FrameObjects(Game.GetLevelId());
			PointsOfView.RotateTo(PointOfViewType.BUILD, 0f);
			m_CameraInTransition = false;
		}
		else if (Game.m_TakingScreenshotForAutoSave || CinemaCamera.Activated())
		{
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_LEFT].FrameObjects(Game.GetLevelId());
			PointsOfView.RotateTo(PointOfViewType.SIM_LEFT, 0f);
			m_CameraInTransition = false;
		}
		else if (LayoutValidator.m_Validating)
		{
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_CENTER_PITCHED_DOWN].FrameObjects(Game.GetLevelId());
			PointsOfView.RotateTo(PointOfViewType.SIM_CENTER_PITCHED_DOWN, 0f);
			m_CameraInTransition = false;
		}
		else
		{
			if (Game.IsCurrentLevelTutorial())
			{
				PointsOfView.RotateTo(PointOfViewType.SIM_RIGHT, GameSettings.TransitionTimeSeconds());
				GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_RIGHT);
			}
			else
			{
				float durationSeconds = GameSettings.TransitionTimeSeconds();
				if (Profiles.m_ActiveProfile.m_FollowCar && VehicleFollow.GetVehicleBeingFollowed() != null)
				{
					VehicleFollow.Reset();
					Vehicle vehicleBeingFollowedFirst = VehicleFollow.GetVehicleBeingFollowedFirst();
					if (vehicleBeingFollowedFirst != null)
					{
						if (VehicleFollow.m_LastVehicleOffsetFromCamera.magnitude > 0.01f)
						{
							VehicleFollow.StartVehicleFollow(vehicleBeingFollowedFirst);
						}
						else
						{
							Cameras.MainCamera().transform.position = PointsOfView.GetPointOfView(PointOfViewType.SIM_LEFT).m_Pos;
							Cameras.MainCamera().transform.rotation = PointsOfView.GetPointOfView(PointOfViewType.SIM_LEFT).m_Rot;
							VehicleFollow.StartVehicleFollowFromDefaultOffset(vehicleBeingFollowedFirst, VehicleFollow.VEHICLE_FOLLOW_DEFAULT_ORTHOGRAHPHIC_SIZE);
						}
					}
				}
				else if (!m_RestoreCamera)
				{
					if (Profiles.m_ActiveProfile.m_PointOfViewType == PointOfViewType.SIM_CUSTOM)
					{
						Profiles.m_ActiveProfile.m_PointOfViewType = PointOfViewType.SIM_RIGHT;
					}
					PointsOfView.m_PointsOfView[Profiles.m_ActiveProfile.m_PointOfViewType].FrameObjects(Game.GetLevelId());
					GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(Profiles.m_ActiveProfile.m_PointOfViewType);
					PointsOfView.RotateTo(Profiles.m_ActiveProfile.m_PointOfViewType, durationSeconds);
				}
				else
				{
					PointOfView pointOfView = PointsOfView.GetPointOfView(PointOfViewType.SIM_CUSTOM);
					if (PointOfViewMatchesPreset(pointOfView, PointsOfView.GetPointOfView(PointOfViewType.SIM_CENTER)))
					{
						ForceView(PointOfViewType.SIM_CENTER);
					}
					else if (PointOfViewMatchesPreset(pointOfView, PointsOfView.GetPointOfView(PointOfViewType.SIM_CENTER_PITCHED_DOWN)))
					{
						ForceView(PointOfViewType.SIM_CENTER_PITCHED_DOWN);
					}
					else if (PointOfViewMatchesPreset(pointOfView, PointsOfView.GetPointOfView(PointOfViewType.SIM_RIGHT)))
					{
						ForceView(PointOfViewType.SIM_RIGHT);
					}
					else if (PointOfViewMatchesPreset(pointOfView, PointsOfView.GetPointOfView(PointOfViewType.SIM_LEFT)))
					{
						ForceView(PointOfViewType.SIM_LEFT);
					}
					else if (pointOfView.Is2D())
					{
						GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CENTER);
					}
					else
					{
						GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CUSTOM);
					}
					PointsOfView.RotateTo(m_RestoreCamera ? PointOfViewType.SIM_CUSTOM : Profiles.m_ActiveProfile.m_PointOfViewType, durationSeconds);
				}
			}
			m_CameraInTransition = true;
		}
		TriggerCallbackManager.OnEnterSim();
		GroupSelect.Cancel();
		Bridge.CancelSelection();
		BridgeTrace.Hide(hide: true);
		BuildZones.EnableSpriteRendering(enabled: false);
		Checkpoints.EnterGameState(GameState.SIM);
		ClipboardManager.ClearClipboard();
		CustomShapes.EnterSimMode();
		CustomShapes.HidePinsForStaticShapes();
		CustomShapes.HideExternalPins();
		Decors.SetVisibility(GameState.SIM);
		GameGrid.m_Grid.SetActive(value: false);
		GameUI.ClearMessages();
		GameUI.SetPointerMode(PointerMode.NORMAL);
		GameUI.m_Instance.m_TopBar.UpdateLevelNavButtons();
		GameUI.m_Instance.m_TopBar.UpdateBridgeCost();
		GameStateCommonInput.DisableMousePanIfButtonDown();
		Bridge.HideAllUI();
		BridgeEdges.SetDefaultColors();
		BridgeEdges.InitFX();
		BridgeEdges.MarkPrebuiltEdgesToExcludeFromMaxStressCalculation();
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
		WaterLine.Enable(enable: false);
		if (!Game.m_TakingScreenshotForAutoSave)
		{
			Theme.m_Instance.PlayAmbientAudio();
			AudioEmitters.PlayAll();
		}
		Outlines.Disable();
		Vehicles.TurnWheelFillMeshesOff();
		WaterBlocks.RefreshScale();
		WaterBlocks.EnableMeshRenderers(!SandboxSettings.m_NoWater);
		WorldBounds.Hide();
		CuttingPlanes.m_Instance.PositionCuttingPlanes();
		Vehicles.EnableMeshRendering();
		ZedAxisVehicles.EnableMeshRendering();
		ZedAxisVehicles.Disable();
		ZedAxisVehicles.PositionAtStartingZ();
		ZedAxisVehicles.LinkToCuttingPlane(CuttingPlanes.m_Instance.m_North.gameObject, CuttingPlanes.m_Instance.m_South.gameObject);
		PolyTwitch.MarkLastLoadedSuggestionAsSimulated(PolyTwitch.m_BridgeHashForSimulation);
		PolyTwitchAutoPlay.EnterSimulation();
		GameStateBuild.ClearFirstBreakEdge();
		HeightFog.SetDirectionalLight(Theme.m_Instance.m_SunLight);
		HeightFog.Enable(!Theme.m_Instance.FogIsZeroHeight());
		TerrainIslands.HideSecondPassMeshRenderers(hide: true);
		TerrainIslands.SetActiveBasedOnHiddenFlag();
		TerrainIslands.StartParticleSystems();
		TerrainIslands.StartWaterFalls();
		TerrainLights.TurnOn(Profiles.m_ActiveProfile.m_TerrainLights);
		if (Game.m_TakingScreenshotForAutoSave || DumpPreviewImages.m_Dumping || DumpReplays.m_Dumping)
		{
			HeightFog.UpdateProperties();
			HeightFog.ManualUpdate();
		}
		AdjustTerrainIslandsToPreventZFighting();
		AdjustCustomShapesToPreventZFighting();
		MaybeHideTerrain();
		if (!SandboxSettings.m_HydraulicControllerEnabled)
		{
			BridgeEdges.ClampJointSelectorsToTwoWay();
		}
		m_RecordingReplayStarted = false;
		BridgeSimSpeed.SetTimeScaleForSimulation();
		if (SandboxSettings.m_NoWater)
		{
			Theme.m_Instance.EnableWaterPlane(on: false);
			WaterBlocks.EnableMeshRenderers(enable: false);
		}
		else
		{
			Theme.m_Instance.EnableWaterPlane(on: true);
			WaterBlocks.EnableMeshRenderers(enable: true);
		}
		ShowGamepadButtons();
		if (!Game.m_TakingScreenshotForAutoSave && !m_CapturingReplayForSolution && !LayoutValidator.m_Validating && !GameManager.IsSteamOffline() && GameLeaderboards.CurrentLevelAllowsLeaderboards())
		{
			SteamLeaderboards.CacheLeaderboards(Game.GetLevelId());
		}
	}

	public static void Exit(GameState nextState)
	{
		if ((bool)CameraControl.instance)
		{
			CameraControl.instance.isSimActive = false;
		}
		if (!Game.m_TakingScreenshotForAutoSave && !m_CapturingReplayForSolution && !LayoutValidator.m_Validating)
		{
			m_RestoreCamera = true;
			if (VehicleFollow.EnabledWithVehiclesInLevel())
			{
				Vehicle vehicleBeingFollowed = VehicleFollow.GetVehicleBeingFollowed();
				if (vehicleBeingFollowed != null)
				{
					Vector2 vector = Utils.V3toV2(vehicleBeingFollowed.m_SpawnPos) - VehicleFollow.GetVehicleFollowOffset();
					PointsOfView.Set(pos: new Vector3(vector.x, vector.y, Cameras.MainCamera().transform.position.z), type: PointOfViewType.SIM_CUSTOM, pivot: PointsOfView.m_Pivot, rot: Cameras.MainCamera().transform.rotation, orthographicSize: Cameras.MainCamera().orthographicSize);
				}
			}
			else
			{
				PointsOfView.Set(PointOfViewType.SIM_CUSTOM, PointsOfView.m_Pivot, Cameras.MainCamera().transform.position, Cameras.MainCamera().transform.rotation, Cameras.MainCamera().orthographicSize);
			}
		}
		if (!Game.m_TakingScreenshotForAutoSave)
		{
			GameUI.ClosePanelsWhenSwitchingModes();
		}
		GameUI.m_Instance.m_SimToolBar.gameObject.SetActive(value: false);
		Theme.m_Instance.StopAmbientAudio();
		m_DisplayLevelEndDialogTimer = float.MaxValue;
		Game.SetTimeScale(1f);
		TriggerCallbackManager.OnExitSim();
		Checkpoints.ResetScale();
		CustomShapes.ShowAnchorMeshes(on: false);
		BridgeEffects.StopErrorFX();
		BridgeRopes.DestroyAll();
		BridgeJoints.ForceStopFlashingOfJoints();
		BridgeJoints.m_FlashingJoints.Clear();
		ZedAxisVehicles.UnlinkFromCuttingPlane();
		ZedAxisVehicles.Disable();
		PolyTwitchAutoPlay.Stop();
		AudioEmitters.StopAll();
		WaterSplash.DestroyAll();
		GameUI.m_Instance.m_Recenter.gameObject.SetActive(value: false);
		TerrainIslands.UndoAdjustmentForTerrainVisualHeight();
		WaterLine.Enable(enable: false);
		WorkshopSubmit.m_RunSimulationBeforeSubmit = false;
		SingletonBehaviour<GpuInstancer>.instance?.Reset();
		if (!m_SkipBridgeRestoreOnExit && (nextState == GameState.BUILD || nextState == GameState.SANDBOX || nextState == GameState.DECOR))
		{
			Bridge.RevertToSavedBridge(Bridge.m_BridgeRestore);
			BridgeJoints.DisableJointCaps();
			BridgeEdges.EnableJointCaps();
			Sandbox.Restore();
		}
		MasterAudio.StopBus("Simulation");
		MasterAudio.StopBus("Sim - Vehicle");
		MasterAudio.StopBus("Sim - HydraulicLoop");
		Cameras.PauseRecording();
		m_SkipBridgeRestoreOnExit = false;
		m_CapturingReplayForSolution = false;
		m_CameraInTransition = false;
		if ((bool)GameUI.m_Instance)
		{
			GameUI.m_Instance.m_TopBar.m_PausedSim = false;
			GameUI.m_Instance.m_GamepadLegend.HideButtons();
		}
		GameRenderSettings.SetShadows_OverrideDistance(Profiles.m_ActiveProfile.m_ShadowResolution != ShadowResolution.OFF, 200f);
		GameStateCommonInput.StopZooming();
		Bridge.m_Simulating = false;
		Cameras.MainCamera().orthographic = true;
		Cameras.ReplayCamera().orthographic = true;
	}

	public static void UpdateManual()
	{
		if (Game.m_TakingScreenshotForAutoSave)
		{
			return;
		}
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
		else if (m_CameraInTransition && GameInput.JustPressed(BindingType.PAUSE_SIM))
		{
			GameUI.m_Instance.m_TopBar.TogglePauseSim();
		}
		BridgeEdges.UpdateStressColor();
		if (Bridge.IsSimulating())
		{
			BridgePhysics.UpdateCurrentTime();
			Vehicles.UpdateManual();
			ZedAxisVehicles.UpdateManual();
			CustomShapes.UpdateManual();
			Bridge.UpdateManual();
			VehicleFollow.UpdateManual();
			if (!GameManager.IsPaused())
			{
				m_ElapsedSeconds += Time.unscaledDeltaTime;
				if (BridgeSimSpeed.GetTimeScaleForDisplay() > 5.01f)
				{
					GameAchievements.InvalidateSpeedRunnerTimer();
				}
			}
		}
		CameraRotate.m_Instance.UpdateManual();
		HeightFog.UpdateProperties();
		if (ActivePanels.None())
		{
			ShowGamepadButtons();
		}
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
		if (!Bridge.IsSimulating() && !m_CameraInTransition && GameStateManager.GetPendingState() == GameState.INVALID)
		{
			m_MassUsed = BridgeJoints.Mass() + BridgeEdges.Mass();
			m_BudgetUsed = Mathf.RoundToInt(Budget.CalculateBridgeCost());
			m_Budget = Budget.m_CashBudget;
			BridgeCheat.CheckForCheating(Sandbox.m_CurrentLayoutData, Bridge.m_BridgeRestore, Game.GetLevelId());
			StartSimulation();
		}
		if (!m_CameraInTransition && !m_RecordingReplayStarted && Profiles.m_ActiveProfile.m_Replays && Cameras.m_AsyncCapture.m_Initialized && !Game.IsCurrentLevelTutorial())
		{
			Cameras.StartRecording();
			m_RecordingReplayStarted = true;
		}
		if (Bridge.IsSimulating())
		{
			TriggerCallbackManager.SortAndProcessTriggerEvents();
			EventTimelines.FixedUpdate_Manual();
			Bridge.FixedUpdateManual();
			Vehicles.FixedUpdateManual();
			CustomShapes.FixedUpdateManual();
			ZedAxisVehicles.FixedUpdateManual();
			StressSamples.FixedUpdateManual();
			if (Main.m_Instance.m_World.frameCount == 1)
			{
				TerrainIslands.MaybeAdjustTerrainVisualHeight();
			}
		}
		if (!m_LevelPassed && !m_LevelFailed && Bridge.IsSimulating())
		{
			EvalulateIfLevelPassedOrFailed(Game.GetLevelId());
		}
		if (m_LevelPassed || m_LevelFailed)
		{
			m_TimeSinceFailOrPass += Time.fixedUnscaledDeltaTime;
			if (m_TimeSinceFailOrPass > Replays.RECORD_TIME_AFTER_PASS_OR_FAIL)
			{
				Cameras.PauseRecording();
				m_TimeSinceFailOrPass = float.MinValue;
			}
			if (!Mathf.Approximately(Time.timeScale, 0f))
			{
				m_DisplayLevelEndDialogTimer -= Mathf.Min(Time.fixedUnscaledDeltaTime, 1f / 30f);
			}
			if (m_DisplayLevelEndDialogTimer < 0f)
			{
				DoLevelPassOrFailActions(Game.GetLevelId());
			}
		}
		else if (!m_LevelHung)
		{
			CheckForHungLevel();
		}
	}

	public static bool CameraInTransition()
	{
		return m_CameraInTransition;
	}

	public static void OnLayoutLoaded()
	{
		m_RestoreCamera = false;
	}

	public static void ResumeReplayRecording()
	{
		m_RecordingReplayStarted = false;
	}

	public static void Pause()
	{
		Game.SetTimeScale(0f);
	}

	public static void UnPause()
	{
		if (!GameUI.m_Instance.m_TopBar.m_PausedSim)
		{
			Game.SetTimeScale(BridgeSimSpeed.GetTimeScaleForSimulation());
		}
	}

	public static bool IsSimulatingWithoutPassOrFail()
	{
		if (!m_LevelPassed && !m_LevelFailed)
		{
			return Bridge.IsSimulating();
		}
		return false;
	}

	public static void LevelSuccessImmediate()
	{
		m_LevelPassed = true;
		m_TimeSinceFailOrPass = 0f;
		m_DisplayLevelEndDialogTimer = 0.1f;
		if (m_DisplayLevelEndDialogTimer > 0f)
		{
			try
			{
				if (GameManager.GameModeIsCampaignOrWorkshop() && !Game.IsCurrentLevelTutorial())
				{
					GameUI.m_Instance.m_LevelComplete.m_GalleryPanel.Download(Game.GetLevelId());
				}
				else
				{
					GameUI.m_Instance.m_LevelComplete.m_GalleryPanel.m_WaitAnimation.SetActive(value: false);
					GameUI.m_Instance.m_LevelComplete.m_GalleryPanel.m_NoReplaysText.gameObject.SetActive(value: false);
				}
			}
			catch (Exception ex)
			{
				Debug.LogFormat("Got exception trying to start Replay download: " + ex.Message);
			}
		}
		Campaign.SaveLastSolvedCampaignLevelId();
	}

	public static void LevelFailImmediate(string failReasonText)
	{
		m_LevelFailed = true;
		m_TimeSinceFailOrPass = 0f;
		DeterminismLog.LogEvent(null, Poly.Determinism.EventType.LevelFail);
		m_DisplayLevelEndDialogTimer = 0.1f;
		GameUI.m_Instance.m_LevelFailed.SetFailReasonText(failReasonText);
	}

	private static void EnableUI()
	{
		GameUI.m_Instance.m_TopBar.EnableForSim();
		GameUI.m_Instance.m_SimToolBar.EnableForSim();
		GameUI.m_Instance.m_BottomBar.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_LiveStress.gameObject.SetActive(!GameUI.m_DisableHud);
		GameUI.m_Instance.m_LiveStress.m_StressLabel.text = "0%";
		SandboxUI.DeActivateAllPanels();
	}

	private static void DoActionsWhenTransitionCompleted()
	{
		if (Cameras.In2DMode())
		{
			GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CENTER);
		}
		GameUI.m_Instance.m_LevelComplete.m_GalleryPanel.MaybeCacheGallerySearchResult(Game.GetLevelId());
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
		GameStateCommonInput.ProcessSimSpeedInput();
		if (GameInput.JustPressed(BindingType.START_SIM) && GameUI.m_Instance.m_TopBar.m_ExitSimButton.interactable)
		{
			GameUI.m_Instance.m_TopBar.OnClickExitSim();
		}
		if (GameInput.JustPressed(BindingType.PAUSE_SIM))
		{
			GameUI.m_Instance.m_TopBar.TogglePauseSim();
		}
		if (GameInput.JustPressed(BindingType.CYCLE_SIM_VIEW))
		{
			if (Profiles.m_ActiveProfile.m_LockBuildCamera)
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				GameUI.m_Instance.m_SimToolBar.OnCycleView();
			}
		}
		if (GameInput.JustPressed(BindingType.CYCLE_FOLLOW_CAR))
		{
			if (Game.IsCurrentLevelTutorial())
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				VehicleFollow.CycleNextVehicle();
			}
		}
		if (GameInput.JustPressed(BindingType.FOLLOW_CAR))
		{
			if (Game.IsCurrentLevelTutorial())
			{
				InterfaceAudio.PlayErrorBeep();
			}
			else
			{
				VehicleFollow.Toggle();
				Profiles.SaveActiveProfile();
				GameUI.m_Instance.m_SimToolBar.UpdateFollowCarIcon();
			}
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
		{
			GameUI.m_Instance.m_TopBar.OnSlower();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
		{
			GameUI.m_Instance.m_TopBar.OnFaster();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP))
		{
			GameUI.ToggleLevelInfoPanel();
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

	private static void StartSimulation()
	{
		TriggerCallbackManager.OnStartSimulation();
		Bridge.StartSimulation();
		VehicleFollow.Reset();
		m_LevelPassed = false;
		m_LevelFailed = false;
		m_LevelHung = false;
		m_NumBridgeBreaks = 0;
		m_ElapsedSeconds = 0f;
		m_TimeSinceFailOrPass = 0f;
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && !GameAchievements.HasUnlocked(GameAchievement.Fun_NeverGoingToGiveYouUp))
		{
			string currentLevelId = Campaign.GetCurrentLevelId();
			string mostRecentSimChecksum = Profiles.m_ActiveProfile.GetMostRecentSimChecksum(currentLevelId);
			string text = Checksum.Generate(Bridge.m_BridgeRestore.SerializeBinary());
			if (text != mostRecentSimChecksum)
			{
				Profiles.m_ActiveProfile.SetMostRecentSimChecksum(currentLevelId, text);
				Profiles.SaveActiveProfile();
			}
		}
		if ((bool)CameraControl.instance && CameraControl.instance.enabled)
		{
			Bounds2 bounds = PointsOfView.CalcBoundsForNewCameraController();
			Bounds renderingBounds = PointsOfView.Calc3dBoundsForGameCamera();
			CameraControl.instance.isSimActive = true;
			CameraControl.instance.Init(bounds, renderingBounds);
		}
	}

	private static void EvalulateIfLevelPassedOrFailed(string levelID)
	{
		if (Vehicles.AllVehiclesHaveCollectedVictoryFlags())
		{
			m_LevelPassed = true;
			m_TimeSinceFailOrPass = 0f;
			m_DisplayLevelEndDialogTimer = GetLevelEndDelaySeconds(pass: true);
			if (m_DisplayLevelEndDialogTimer > 0f)
			{
				try
				{
					if (GameManager.GameModeIsCampaignOrWorkshop() && !Game.IsCurrentLevelTutorial())
					{
						GameUI.m_Instance.m_LevelComplete.m_GalleryPanel.Download(Game.GetLevelId());
					}
					else
					{
						GameUI.m_Instance.m_LevelComplete.m_GalleryPanel.m_WaitAnimation.SetActive(value: false);
						GameUI.m_Instance.m_LevelComplete.m_GalleryPanel.m_NoReplaysText.gameObject.SetActive(value: false);
					}
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Got exception trying to start Replay download: " + ex.Message);
				}
			}
			Campaign.SaveLastSolvedCampaignLevelId();
			return;
		}
		Vehicle vehicleThatMeetsFailConditons = Vehicles.GetVehicleThatMeetsFailConditons();
		if (!vehicleThatMeetsFailConditons)
		{
			return;
		}
		m_LevelFailed = true;
		m_TimeSinceFailOrPass = 0f;
		DeterminismLog.LogEvent(null, Poly.Determinism.EventType.LevelFail);
		m_DisplayLevelEndDialogTimer = GetLevelEndDelaySeconds(pass: false);
		SetLevelFailedReasonText(vehicleThatMeetsFailConditons);
		try
		{
			if (!GameManager.GameModeIsCampaignOrWorkshop() || Game.IsCurrentLevelTutorial())
			{
				return;
			}
			if (!GameAchievements.m_LevelsFailed.Contains(levelID))
			{
				GameAchievements.m_LevelsFailed.Add(levelID);
				GameAchievements.SaveFailedLevels();
			}
			if (!GameAchievements.HasUnlocked(GameAchievement.Fun_NeverGoingToGiveYouUp))
			{
				string mostRecentSimChecksum = Profiles.m_ActiveProfile.GetMostRecentSimChecksum(levelID);
				if (!GameAchievements.m_FailedSimChecksums.ContainsKey(levelID))
				{
					GameAchievements.m_FailedSimChecksums.Add(levelID, new HashSet<string> { mostRecentSimChecksum });
					GameAchievements.SaveFailedSimChecksums();
				}
				else if (!string.IsNullOrEmpty(mostRecentSimChecksum))
				{
					GameAchievements.m_FailedSimChecksums[levelID].Add(mostRecentSimChecksum);
					GameAchievements.SaveFailedSimChecksums();
				}
			}
			if (GameAchievements.m_FailedSimChecksums.ContainsKey(levelID) && GameAchievements.m_FailedSimChecksums[levelID].Count >= GameAchievements.NUM_FAILURES_TO_TRIGGER_NEVER_GONNA_GIVE_YOU_UP)
			{
				GameAchievements.UnlockAchievement(GameAchievement.Fun_NeverGoingToGiveYouUp);
			}
		}
		catch (Exception ex2)
		{
			Debug.LogFormat("Caught exception trying to do level complete achivement checks: " + ex2.Message);
		}
	}

	private static void CheckForHungLevel()
	{
		foreach (EventTimeline timeline in EventTimelines.m_Timelines)
		{
			if (!timeline.m_ActiveStage || !timeline.m_ActiveStage.IsHung())
			{
				continue;
			}
			Vehicle hungVehicle = timeline.m_ActiveStage.GetHungVehicle();
			if ((bool)hungVehicle && !GameUI.m_Instance.m_PopUpTwoChoices.gameObject.activeInHierarchy)
			{
				m_LevelHung = true;
				if (PolyTwitchAutoPlay.m_Running)
				{
					PopUpTwoChoices.Display(string.Format(Localize.Get("POPUP_STUCK_POLYTWITCH"), GetVehicleNameColorized(hungVehicle)), Localize.Get("POPUP_STUCK_POLYTWITCH_SKIP"), Localize.Get("POPUP_STUCK_POLYTWITCH_CONTINUE"), OnSkipToNextBridge, OnContinueSimulation);
					GameUI.m_Instance.m_PopUpTwoChoices.m_ChoiceAShortcut = BindingType.START_SIM;
					PolyTwitchAutoPlay.StartStuckRetryCountdown();
					InterfaceAudio.Play("ui_window_open");
				}
				break;
			}
		}
	}

	private static void OnSkipToNextBridge()
	{
		GameUI.m_Instance.m_PolyTwitchMain.m_AutoPlayPanel.SkipToNextBridge();
	}

	private static void OnRetry()
	{
		GameUI.m_Instance.m_TopBar.OnExitSim();
	}

	private static void OnContinueSimulation()
	{
		foreach (EventTimeline timeline in EventTimelines.m_Timelines)
		{
			if ((bool)timeline.m_ActiveStage && timeline.m_ActiveStage.IsHung())
			{
				timeline.m_ActiveStage.m_SkipHungCheck = true;
			}
		}
	}

	private static void LaunchLevelEndDialog()
	{
		if (m_LevelPassed)
		{
			GameUI.m_Instance.m_LevelComplete.Open();
		}
		else if (m_LevelFailed)
		{
			GameUI.m_Instance.m_LevelFailed.Open();
		}
		InterfaceAudio.Play("ui_window_open");
	}

	private static bool ShouldUploadScoreToLeaderboards(int budgetUsed)
	{
		if (GameManager.IsSteamOffline())
		{
			return false;
		}
		if (!GameLeaderboards.CurrentLevelAllowsUploadToLeaderboards())
		{
			return false;
		}
		if (Game.BlockScoreUploadAndAchivementStats())
		{
			return false;
		}
		if (!BridgeCheat.BridgeCostValid(budgetUsed))
		{
			return false;
		}
		if (Profiles.m_ActiveProfile.m_LeaderboardsNoSubmit)
		{
			return false;
		}
		if (LeaderboardReplay.IsActive())
		{
			return false;
		}
		return true;
	}

	private static void UploadScoreToLeaderboards(string levelId, int score, float maxStressNormalized, bool didBreak, BridgeSaveData bridgeSaveData)
	{
		if (string.IsNullOrEmpty(levelId))
		{
			Debug.LogWarningFormat("Invalid level id when trying to upload score to leaderboards");
		}
		else if (!GameManager.IsSteamOffline())
		{
			bool underBudget = score <= Mathf.RoundToInt(Budget.m_CashBudget);
			SteamLeaderboardsUpload.UploadLeaderboardScore(levelId, score, maxStressNormalized, didBreak, underBudget, bridgeSaveData, Profiles.m_ActiveProfile.m_LeaderboardsFilter, GameLeaderboards.UploadScoreComplete);
		}
	}

	private static void DoLevelPassOrFailActions(string levelID)
	{
		Game.IsCurrentLevelTutorial();
		bool flag = false;
		if (!WorkshopSubmit.m_RunSimulationBeforeSubmit && !m_CapturingReplayForSolution && !LayoutValidator.m_Validating && !LeaderboardReplay.IsActive())
		{
			flag = PolyTwitch.m_StreamStarted && PolyTwitch.m_LastLoadedSuggestion != null && PolyTwitch.m_LastLoadedSuggestion.m_BridgeHash == PolyTwitch.m_BridgeHashForSimulation;
			if (m_LevelPassed && GameManager.GameModeIsCampaignOrWorkshop() && !flag && !Game.BlockScoreUploadAndAchivementStats())
			{
				Campaign.UpdateReservedSaves(m_NumBridgeBreaks, StressSamples.m_MaxStressNormalized);
			}
			if (m_LevelPassed && !Game.BlockScoreUploadAndAchivementStats())
			{
				CampaignLevelStatus levelStatusFromSimulationResults = GetLevelStatusFromSimulationResults();
				if (GameManager.GetGameMode() == GameMode.CAMPAIGN && (bool)Campaign.m_CurrentLevel)
				{
					Campaign.m_CampaignProgress.MarkLevelAsCompleted(Campaign.m_CurrentLevel.m_Id, levelStatusFromSimulationResults);
					Campaign.m_CampaignProgress.UnlockNextLevel(Campaign.m_CurrentLevel);
					CampaignProgress.Save();
					CampaignWorlds.m_Instance.MaybeUpdateFiveStarUnlocks();
				}
				else if (GameManager.GetGameMode() == GameMode.WORKSHOP && !string.IsNullOrEmpty(WorkshopCampaigns.m_ActiveWorkshopCampaignModId))
				{
					WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(WorkshopCampaigns.m_ActiveWorkshopCampaignModId);
					if (workshopCampaign != null)
					{
						bool num = workshopCampaign.HasCompletedAllLevels();
						WorkshopCampaignProgress.Load(workshopCampaign);
						workshopCampaign.m_CampaignProgress.MarkLevelAsCompleted(Game.GetLevelId(), levelStatusFromSimulationResults);
						if (!num && workshopCampaign.HasCompletedAllLevels())
						{
							PopUpMessage.DisplayWithTitle(workshopCampaign.GetModName(), workshopCampaign.m_WinMessage, null);
						}
						WorkshopCampaignProgress.Save(workshopCampaign);
					}
				}
			}
			if (m_LevelPassed && GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null && WeeklyChallenges.IsAWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId()))
			{
				CampaignLevelStatus levelStatusFromSimulationResults2 = GetLevelStatusFromSimulationResults();
				WeeklyChallengesProgress.UpdateProgress(Workshop.m_LastPlayedWorkshopItem.GetId(), levelStatusFromSimulationResults2);
			}
			PolyTwitch.SetStatusForLastLoadedSuggestion(m_LevelPassed ? PolyTwitchSuggestionStatus.PASSED : PolyTwitchSuggestionStatus.FAILED, PolyTwitch.m_BridgeHashForSimulation);
		}
		m_DisplayLevelEndDialogTimer = float.MaxValue;
		Vehicles.TurnOffMotorForAll();
		if (WorkshopSubmit.m_RunSimulationBeforeSubmit)
		{
			WorkshopSubmit.m_SimulationPassed = m_LevelPassed;
			WorkshopSubmit.m_RunSimulationBeforeSubmit = false;
			GameStateManager.SwitchToState(GameStateManager.GetPrevState());
		}
		else if (!m_CapturingReplayForSolution && !LayoutValidator.m_Validating)
		{
			if (PolyTwitch.m_StreamStarted)
			{
				Campaign.SaveLastSolvedCampaignLevelId();
				LaunchLevelEndDialog();
				if (m_LevelPassed && GameManager.GameModeIsCampaignOrWorkshop() && GameLeaderboards.CurrentLevelAllowsLeaderboards())
				{
					if (ShouldUploadScoreToLeaderboards(m_BudgetUsed) && !flag)
					{
						UploadScoreToLeaderboards(levelID, m_BudgetUsed, StressSamples.m_MaxStressNormalized, m_NumBridgeBreaks > 0, Bridge.m_BridgeRestore);
					}
					else
					{
						GameUI.m_Instance.m_LevelComplete.m_LeaderboardPanel.OnRefresh();
					}
				}
			}
			else
			{
				LaunchLevelEndDialog();
				if (m_LevelPassed && GameManager.GameModeIsCampaignOrWorkshop() && GameLeaderboards.CurrentLevelAllowsLeaderboards())
				{
					if (ShouldUploadScoreToLeaderboards(m_BudgetUsed))
					{
						UploadScoreToLeaderboards(levelID, m_BudgetUsed, StressSamples.m_MaxStressNormalized, m_NumBridgeBreaks > 0, Bridge.m_BridgeRestore);
					}
					else
					{
						GameUI.m_Instance.m_LevelComplete.m_LeaderboardPanel.OnRefresh();
					}
				}
			}
		}
		GameUI.m_Instance.m_LevelInfoLite.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_LiveStress.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_SimToolBar.gameObject.SetActive(value: false);
		Cameras.PauseRecording();
		if (!WorkshopSubmit.m_RunSimulationBeforeSubmit && !flag && GameManager.GameModeIsCampaignOrWorkshop() && !Game.IsCurrentLevelTutorial() && !DumpReplays.m_Dumping)
		{
			GameAchievements.DoEndLevelActions(GameManager.GetGameMode(), Game.GetLevelId(), m_LevelPassed, m_LevelFailed);
		}
	}

	private static CampaignLevelStatus GetLevelStatusFromSimulationResults()
	{
		bool flag = m_BudgetUsed <= Mathf.RoundToInt(Budget.m_CashBudget);
		if (m_LevelPassed && m_NumBridgeBreaks == 0 && flag)
		{
			return CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS;
		}
		if (m_LevelPassed && flag)
		{
			return CampaignLevelStatus.UNDER_BUDGET;
		}
		if (m_LevelPassed)
		{
			return CampaignLevelStatus.PASS;
		}
		return CampaignLevelStatus.NONE;
	}

	private static void SetLevelFailedReasonText(Vehicle vehicle)
	{
		string empty = string.Empty;
		string vehicleNameColorized = GetVehicleNameColorized(vehicle);
		empty = (vehicle.WheelsUnderWater() ? string.Format(Localize.Get("UI_LEVEL_FAILURE_REASON_UNDER_WATER"), vehicleNameColorized) : ((!(vehicle.transform.position.y < Vehicles.VEHICLE_FAIL_Y_POS) && vehicle.m_isRenderingEnabled) ? string.Empty : string.Format(Localize.Get("UI_LEVEL_FAILURE_REASON_UNDER_WORLD"), vehicleNameColorized)));
		GameUI.m_Instance.m_LevelFailed.SetFailReasonText(empty);
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

	private static string GetVehicleNameColorized(Vehicle vehicle)
	{
		return $"{Utils.ColorToHex(Color.white)}{Localize.Get(vehicle.m_Stub.m_DisplayNameLocID)} {vehicle.GetTextMeshString()}</color>";
	}

	private static void SetWaterProperties()
	{
		WaterBlocks.EnableWaves();
	}

	private static void SetCameraBackgroundColor()
	{
		Cameras.MainCamera().backgroundColor = GameUI.m_Instance.m_SimModeBackgroundColor;
		Cameras.ReplayCamera().backgroundColor = GameUI.m_Instance.m_SimModeBackgroundColor;
	}

	private static void SetAmbientLightingColor()
	{
		RenderSettings.ambientLight = Theme.m_Instance.m_ThemeStub.m_AmbientLightColor;
	}

	private static float GetLevelEndDelaySeconds(bool pass)
	{
		if (!pass)
		{
			return LEVEL_FAIL_DIALOG_DELAY_SECONDS;
		}
		return LEVEL_PASSED_DIALOG_DELAY_SECONDS;
	}

	private static void ForceView(PointOfViewType pointOfViewType)
	{
		Profiles.m_ActiveProfile.m_PointOfViewType = pointOfViewType;
		PointsOfView.m_PointsOfView[PointOfViewType.SIM_CUSTOM].CopyFrom(PointsOfView.m_PointsOfView[pointOfViewType]);
		GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(pointOfViewType);
	}

	private static bool PointOfViewMatchesPreset(PointOfView pointOfView, PointOfView presetPointOfView)
	{
		float num = Vector3.Distance(pointOfView.m_Rot.eulerAngles, presetPointOfView.m_Rot.eulerAngles);
		float num2 = Mathf.Abs(pointOfView.m_OrthographicsSize - presetPointOfView.m_OrthographicsSize);
		float num3 = Vector3.Distance(pointOfView.m_Pos, presetPointOfView.m_Pos);
		float num4 = 0.0101f;
		if (num > num4)
		{
			return false;
		}
		if (num2 > num4)
		{
			return false;
		}
		if (num3 > num4)
		{
			return false;
		}
		return true;
	}

	private static void ShowGamepadButtons()
	{
		if (CampaignTutorial.IsRunning())
		{
			GameUI.m_Instance.m_GamepadLegend.HideButtons();
			return;
		}
		if (GameUI.m_Instance.m_LevelInfoLite.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("UI_CLOSE"), GamepadButtonType.NORTH, Localize.Get("UI_ROTATE_CAMERA_HOLD"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("BINDING_STRESS_VIS"), GamepadButtonType.DPAD_UP, Localize.Get("UI_TIMELINE"), GamepadButtonType.NORTH, Localize.Get("UI_ROTATE_CAMERA_HOLD"));
		}
		GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("UI_SIM_SPEED"), GamepadButtonType.DPAD_LEFT, Localize.Get("BINDING_CYCLE_SIM_VIEW"));
	}
}
