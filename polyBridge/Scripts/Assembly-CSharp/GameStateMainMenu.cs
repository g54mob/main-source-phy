using System;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Poly.Base;
using Poly.Graphics;
using UnityEngine;

public class GameStateMainMenu
{
	public static bool m_LevelPassed;

	public static bool m_LevelFailed;

	private static float m_DisplayLevelEndDialogTimer;

	public static CampaignWorld m_World;

	public static int m_LevelIndexInWorld;

	public static string m_CurrentLayoutHash;

	public static string m_LoadCampaignPanelForWorldID;

	public static string m_ForceWorldID;

	private static bool m_RefreshedWeeklyChallenges;

	public static void Enter(GameState prevState)
	{
		if (LayoutResaver.m_Resaving)
		{
			LayoutResaver.End();
		}
		if (prevState != GameState.PRELOADING_LAYOUT_ASSETS)
		{
			m_World = GetWorldForMainMenu();
			m_LevelIndexInWorld = UnityEngine.Random.Range(0, m_World.m_MainMenuLevels.Length);
		}
		string layoutPath = m_World.m_MainMenuLevels[m_LevelIndexInWorld].GetLayoutPath();
		if (!Prefabs.m_Instance.IsLevelPreloaded(layoutPath))
		{
			GameStatePreloadingAssets.PreloadLevel(layoutPath, null, EnterFromPreloading);
			return;
		}
		LoadLayout();
		EnableUI();
		Theme.m_Instance.SetThemeVolume();
		Game.SetCameraCullingMasks(GameState.MAIN_MENU);
		SetWaterProperties();
		SetAmbientLightingColor();
		TriggerCallbackManager.OnEnterSim();
		Bridge.CancelSelection();
		BridgeTrace.Hide(hide: true);
		BuildZones.EnableSpriteRendering(enabled: false);
		CampaignTutorial.End();
		Checkpoints.EnterGameState(GameState.MAIN_MENU);
		ClipboardManager.ClearClipboard();
		CustomShapes.EnterSimMode();
		CustomShapes.HidePinsForStaticShapes();
		CustomShapes.HideExternalPins();
		CustomShapes.ShowAnchorMeshes(on: true);
		Decors.SetVisibility(GameState.MAIN_MENU);
		GameGrid.m_Grid.SetActive(value: false);
		GameUI.ClearMessages();
		GameUI.SetPointerMode(PointerMode.NORMAL);
		GameUI.m_Instance.m_Version.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_SandboxMenu.m_SandboxTabsPanel.m_SearchInputField.text = string.Empty;
		GameStateCommonInput.DisableMousePanIfButtonDown();
		Bridge.HideAllUI();
		BridgeEdges.SetDefaultColors();
		BridgeEdges.InitFX();
		BridgeJoints.MakeDefaultColor();
		BridgePillars.DisableOutlines();
		BridgePillars.InitFX();
		Decors.Hide(hide: false);
		GameRenderSettings.SetShadows((Profiles.m_ActiveProfile.m_ShadowResolution != ShadowResolution.OFF) ? true : false);
		SandboxItems.DisableFloatingText();
		SandboxItems.DisableOutlines();
		Cameras.EnableSky();
		Theme.m_Instance.EnableSimModeLighting();
		Theme.m_Instance.PositionWaterPlane();
		WaterLine.Enable(enable: false);
		MuteSFX();
		WaterBlocks.RefreshScale();
		WaterBlocks.EnableMeshRenderers(!SandboxSettings.m_NoWater);
		WorldBounds.Hide();
		Outlines.Disable();
		CuttingPlanes.m_Instance.PositionCuttingPlanes();
		Vehicles.EnableMeshRendering();
		ZedAxisVehicles.EnableMeshRendering();
		ZedAxisVehicles.Disable();
		ZedAxisVehicles.PositionAtStartingZ();
		ZedAxisVehicles.LinkToCuttingPlane(CuttingPlanes.m_Instance.m_North.gameObject, CuttingPlanes.m_Instance.m_South.gameObject);
		HeightFog.SetDirectionalLight(Theme.m_Instance.m_SunLight);
		HeightFog.Enable(!Theme.m_Instance.FogIsZeroHeight());
		TerrainIslands.HideSecondPassMeshRenderers(hide: true);
		TerrainIslands.StartParticleSystems();
		TerrainIslands.StartWaterFalls();
		TerrainLights.TurnOn(Profiles.m_ActiveProfile.m_TerrainLights);
		TerrainIslands.SetActiveBasedOnHiddenFlag();
		if (!SandboxSettings.m_HydraulicControllerEnabled)
		{
			BridgeEdges.ClampJointSelectorsToTwoWay();
		}
		Mods.DeactivateAutoLoadedMods();
		GameAchievements.InvalidateSpeedRunnerTimer();
		Game.SetTimeScale(BridgeSimSpeed.m_SimulationSpeedMultiplier);
		PolyTwitch.OnEnterMainMenuState();
		Cameras.m_AsyncCapture.DeleteAllReplayFrames();
		if (!string.IsNullOrEmpty(m_LoadCampaignPanelForWorldID))
		{
			CampaignWorld worldById = CampaignWorlds.m_Instance.GetWorldById(m_LoadCampaignPanelForWorldID);
			if ((bool)worldById && worldById.IsLocked())
			{
				GameUI.m_Instance.m_MainMenuNew.OpenCampaignPanelForDefaultLevel();
			}
			else
			{
				GameUI.m_Instance.m_Campaign.Open(Profiles.m_ActiveProfile.GetLastPlayedLevelIDForWorld(m_LoadCampaignPanelForWorldID), m_LoadCampaignPanelForWorldID);
			}
			m_LoadCampaignPanelForWorldID = string.Empty;
		}
		else
		{
			GameUI.m_Instance.m_MainMenuNew.Open();
		}
		Theme.m_Instance.EnableWaterPlane(on: true);
		WaterBlocks.EnableMeshRenderers(enable: true);
		WorkshopCampaigns.DeactivateCurrentWorkshopCampaignMod();
		LeaderboardReplay.SetActive(active: false);
		SandboxSettings.m_Unbreakable = true;
	}

	public static void Exit(GameState nextState)
	{
		Theme.m_Instance.StopAmbientAudio();
		TriggerCallbackManager.OnExitSim();
		BridgeRopes.DestroyAll();
		BridgeJoints.ForceStopFlashingOfJoints();
		BridgeJoints.m_FlashingJoints.Clear();
		CustomShapes.ShowAnchorMeshes(on: false);
		ZedAxisVehicles.UnlinkFromCuttingPlane();
		ZedAxisVehicles.Disable();
		MasterAudio.StopBus("Simulation");
		MasterAudio.StopBus("Sim - Vehicle");
		MasterAudio.StopBus("Sim - HydraulicLoop");
		UnMuteSFX();
		GameUI.m_Instance.m_Version.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_MainMenuNew.Close();
		Bridge.m_Simulating = false;
		SingletonBehaviour<GpuInstancer>.instance?.Reset();
		m_LoadCampaignPanelForWorldID = string.Empty;
		SandboxSettings.m_Unbreakable = false;
	}

	public static void UpdateManual()
	{
		Bridge.UpdateManual();
		CustomShapes.UpdateManual();
		Vehicles.UpdateManual();
		ZedAxisVehicles.UpdateManual();
		HeightFog.UpdateProperties();
		if (!m_RefreshedWeeklyChallenges && DateTime.Now.DayOfWeek == DayOfWeek.Monday && DateTime.Now.Minute > 2)
		{
			WeeklyChallenges.Download();
			m_RefreshedWeeklyChallenges = true;
		}
		if (!m_LevelPassed && !m_LevelFailed)
		{
			EvalulateIfLevelPassedOrFailed();
		}
		if (m_LevelPassed || m_LevelFailed)
		{
			m_DisplayLevelEndDialogTimer -= Time.deltaTime;
			if (m_DisplayLevelEndDialogTimer < 0f)
			{
				LoadNextLayout();
				LoadCustomCamera();
				WorldBounds.Hide();
				Bridge.HideAllUI();
				BridgePillars.DisableOutlines();
				SandboxItems.DisableFloatingText();
				SandboxItems.DisableOutlines();
				Cameras.EnableSky();
				CustomShapes.EnterSimMode();
				CustomShapes.HidePinsForStaticShapes();
				CustomShapes.HideExternalPins();
				CustomShapes.ShowAnchorMeshes(on: true);
				Decors.Hide(hide: false);
				Theme.m_Instance.EnableSimModeLighting();
				Theme.m_Instance.PositionWaterPlane();
				ZedAxisVehicles.Disable();
				ZedAxisVehicles.PositionAtStartingZ();
				ZedAxisVehicles.LinkToCuttingPlane(CuttingPlanes.m_Instance.m_North.gameObject, CuttingPlanes.m_Instance.m_South.gameObject);
				HeightFog.SetDirectionalLight(Theme.m_Instance.m_SunLight);
				HeightFog.Enable(!Theme.m_Instance.FogIsZeroHeight());
				TerrainIslands.HideSecondPassMeshRenderers(hide: true);
				TerrainIslands.StartParticleSystems();
				TerrainIslands.StartWaterFalls();
				TerrainLights.TurnOn(Profiles.m_ActiveProfile.m_TerrainLights);
				TerrainIslands.SetActiveBasedOnHiddenFlag();
				SandboxSettings.m_Unbreakable = true;
				m_DisplayLevelEndDialogTimer = float.MaxValue;
			}
		}
	}

	public static void LateUpdateManual()
	{
		CustomShapes.LateUpdateManual();
	}

	public static void FixedUpdateManual()
	{
		if (!Bridge.IsSimulating() && m_CurrentLayoutHash == Sandbox.m_CurrentLayoutHash)
		{
			Bridge.m_BridgeRestore = BridgeSave.Serialize();
			StartSimulation();
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
		}
	}

	public static void EnterFromPreloading(string layoutFilename, FileSlot slot)
	{
		GameStateManager.BashState(GameState.PRELOADING_LAYOUT_ASSETS);
		GameStateManager.SwitchToStateImmediate(GameState.MAIN_MENU);
		AudioMixerManager.AllowedToStartMusicFadeIn();
	}

	private static bool LoadLayout()
	{
		CampaignLevel campaignLevel = m_World.m_MainMenuLevels[m_LevelIndexInWorld];
		if (campaignLevel == null)
		{
			Debug.LogWarningFormat($"Could not find main menu level with index {m_LevelIndexInWorld}");
			return false;
		}
		string filename = campaignLevel.m_Filename;
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(Campaign.GetLevelsPath(campaignLevel.m_Id), filename);
		if (sandboxLayoutData == null)
		{
			Debug.LogWarningFormat("Could not load: {0}", filename);
			return false;
		}
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(campaignLevel.m_Id);
		string text = ((worldWithLevelId != null) ? worldWithLevelId.m_ThemePreloadStub.m_ID : sandboxLayoutData.m_ThemeStubId);
		if (string.IsNullOrEmpty(text))
		{
			Debug.LogWarningFormat("Theme for main menu level is null or empty");
			return false;
		}
		Sandbox.Clear();
		Sandbox.Load(text, sandboxLayoutData, loadBridge: true);
		PointsOfView.OnLayoutLoaded(string.Empty);
		LoadCustomCamera();
		SingletonBehaviour<GpuInstancer>.instance?.Reset();
		Sandbox.m_CurrentLayoutName = filename;
		if (Profiles.m_ActiveProfile.m_LastMainMenuThemeId != text)
		{
			Profiles.m_ActiveProfile.m_LastMainMenuThemeId = text;
			Profiles.SaveActiveProfile();
		}
		m_CurrentLayoutHash = Sandbox.m_CurrentLayoutHash;
		Prefabs.m_Instance.UnloadAssetsNotInLayout(campaignLevel.GetLayoutPath());
		PreloadNextLayout();
		return true;
	}

	private static void LoadNextLayout()
	{
		m_LevelIndexInWorld++;
		if (m_LevelIndexInWorld >= m_World.m_MainMenuLevels.Length)
		{
			m_LevelIndexInWorld = 0;
		}
		string layoutPath = m_World.m_MainMenuLevels[m_LevelIndexInWorld].GetLayoutPath();
		if (Prefabs.m_Instance.IsLevelPreloaded(layoutPath))
		{
			LoadLayout();
		}
		else
		{
			GameStatePreloadingAssets.PreloadLevel(layoutPath, null, EnterFromPreloading);
		}
	}

	private static void PreloadNextLayout()
	{
		int num = m_LevelIndexInWorld + 1;
		if (num >= m_World.m_MainMenuLevels.Length)
		{
			num = 0;
		}
		GameStatePreloadingAssets.PreloadLevelInBackground(m_World.m_MainMenuLevels[num].GetLayoutPath());
	}

	private static CampaignWorld GetWorldForMainMenu()
	{
		if (!string.IsNullOrEmpty(m_ForceWorldID))
		{
			CampaignWorld worldById = CampaignWorlds.m_Instance.GetWorldById(m_ForceWorldID);
			if (worldById != null)
			{
				return worldById;
			}
		}
		if (Campaign.HasCompletedAllLevels())
		{
			List<CampaignWorld> list = new List<CampaignWorld>();
			CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
			foreach (CampaignWorld campaignWorld in worlds)
			{
				if (campaignWorld.m_MainMenuLevels.Length != 0 && campaignWorld.m_ThemePreloadStub.m_ID != Profiles.m_ActiveProfile.m_LastMainMenuThemeId)
				{
					list.Add(campaignWorld);
				}
			}
			if (list.Count == 0)
			{
				list.Add(CampaignWorlds.m_Instance.m_Worlds[0]);
			}
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(Profiles.m_ActiveProfile.m_LastLoadedCampaignLevelId);
		if (levelFromId == null)
		{
			return CampaignWorlds.m_Instance.m_Worlds[0];
		}
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelFromId.m_Id);
		if (worldWithLevelId == null || worldWithLevelId.m_MainMenuLevels.Length == 0)
		{
			return CampaignWorlds.m_Instance.m_Worlds[0];
		}
		return worldWithLevelId;
	}

	private static void EnableUI()
	{
		GameUI.m_Instance.m_BottomBar.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_LiveStress.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.gameObject.SetActive(value: false);
		SandboxUI.DeActivateAllPanels();
	}

	private static void EvalulateIfLevelPassedOrFailed()
	{
		if (Vehicles.AllVehiclesHaveCollectedVictoryFlags())
		{
			m_LevelPassed = true;
			m_DisplayLevelEndDialogTimer = 5f;
		}
		else if ((bool)Vehicles.GetVehicleThatMeetsFailConditons())
		{
			m_DisplayLevelEndDialogTimer = 0.5f;
			m_LevelFailed = true;
		}
	}

	private static void StartSimulation()
	{
		TriggerCallbackManager.OnStartSimulation();
		Bridge.StartSimulation();
		Game.SetTimeScale(BridgeSimSpeed.m_SimulationSpeedMultiplier);
		VehicleAudio.S_AddedPitch = 0f;
		m_LevelPassed = false;
		m_LevelFailed = false;
	}

	private static void SetWaterProperties()
	{
		WaterBlocks.EnableWaves();
	}

	private static void SetAmbientLightingColor()
	{
		RenderSettings.ambientLight = Theme.m_Instance.m_ThemeStub.m_AmbientLightColor;
	}

	private static void MuteSFX()
	{
		AudioVolume.MuteSFX(on: true);
	}

	private static void UnMuteSFX()
	{
		AudioVolume.MuteSFX(on: false);
	}

	private static void LoadCustomCamera()
	{
		SandboxCustomCamera.TryLoad("MainMenu_" + m_World.m_Id);
	}
}
