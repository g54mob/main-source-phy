using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Game
{
	public static readonly string ADMIN_URL = "http://ec2-18-190-28-71.us-east-2.compute.amazonaws.com";

	public static readonly string SERVICES_BASE_URL = "http://pb3services.com";

	public static readonly string CLOUDFLARE_GALLERY_URL = "https://pb3gallery.com/gallery/";

	public static readonly string CLOUDFLARE_LEADERBOARDS_URL = "https://pb3gallery.com/leaderboards/";

	public static readonly string AMAZON_S3_URL = "https://pb3game.s3.us-east-2.amazonaws.com/";

	public static readonly string ADMIN_DELETE_SCORE_URL = ADMIN_URL + ":5005/delete_score";

	public static readonly string ADMIN_BAN_URL = ADMIN_URL + ":5005/ban_and_delete_scores";

	public static readonly string ADMIN_ADD_TAG_URL = ADMIN_URL + ":5005/add_tag";

	public static readonly string ADMIN_REMOVE_TAG_URL = ADMIN_URL + ":5005/remove_tag";

	public static readonly string LEADERBOARD_UPLOAD_URL = SERVICES_BASE_URL + ":5001";

	public static readonly string GALLERY_UPLOAD_URL = SERVICES_BASE_URL + ":5004/upload";

	public static readonly string GALLERY_DELETE_URL = SERVICES_BASE_URL + ":5004/delete";

	public static readonly string GALLERY_SEARCH_URL = SERVICES_BASE_URL + ":5002";

	public static bool m_AllowShowLevelID = false;

	public static bool m_ForceSteamDeck;

	public static bool m_ForceMobile;

	public static readonly int DOWNLOAD_TIMEOUT_SECONDS = 30;

	public static readonly int DOWNLOAD_ATTEMPT_INTERVAL_SECONDS = 60;

	public static readonly HttpClient m_HttpClient = new HttpClient();

	public static bool m_ForceOffline;

	public static bool m_TakingScreenshotForAutoSave;

	public static bool m_TakingScreenshotForWorkshopSubmit;

	private static Dictionary<string, string> m_LevelChecksums = new Dictionary<string, string>();

	public static void AddLevelChecksum(string levelId, string checksum)
	{
		if (m_LevelChecksums.ContainsKey(levelId))
		{
			m_LevelChecksums[levelId] = checksum;
		}
		else
		{
			m_LevelChecksums.Add(levelId, checksum);
		}
	}

	public static string GetLevelCheckssum(string levelId)
	{
		if (m_LevelChecksums.ContainsKey(levelId))
		{
			return m_LevelChecksums[levelId];
		}
		return string.Empty;
	}

	public static void SetTimeScale(float timeScale)
	{
		if (!Mathf.Approximately(Time.timeScale, timeScale))
		{
			Time.timeScale = timeScale;
			bool pause = Mathf.Approximately(timeScale, 0f);
			TerrainIslands.PauseParticleSystems(pause);
			TerrainIslands.PauseWaterfalls(pause);
			TerrainIslands.UpdateWaterfallsInverseTimeScale(timeScale);
		}
	}

	public static bool BlockScoreUploadAndAchivementStats()
	{
		if (BridgeCheat.m_Cheated || Mods.m_IsUsingGameplayMod)
		{
			return true;
		}
		if (GameManager.GetGameSubMode() == GameSubMode.LEADERBOARD_REPLAY)
		{
			return true;
		}
		return false;
	}

	public static string GetLevelFilename()
	{
		if (GameManager.GetGameMode() == GameMode.SANDBOX)
		{
			return Path.ChangeExtension(Sandbox.m_CurrentLayoutName, SandboxLayout.SAVE_EXTENSION);
		}
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null)
		{
			return Path.ChangeExtension(Campaign.m_CurrentLevel.m_Filename, SandboxLayout.SAVE_EXTENSION);
		}
		return string.Empty;
	}

	public static bool InSandboxGodMode()
	{
		if (GameManager.GetGameMode() == GameMode.SANDBOX)
		{
			return Profiles.m_ActiveProfile.m_GodMode;
		}
		return false;
	}

	public static string GetLevelId()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			return Campaign.GetCurrentLevelId();
		}
		if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null)
		{
			return Workshop.m_LastPlayedWorkshopItem.GetId();
		}
		if (GameManager.GetGameMode() == GameMode.SANDBOX && Sandbox.m_CurrentLayoutData != null)
		{
			return Sandbox.m_CurrentLayoutData.m_Workshop.m_Id;
		}
		return string.Empty;
	}

	public static string GetWorldIdWithLevel(string levelId)
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelId);
		if (!(worldWithLevelId != null))
		{
			return string.Empty;
		}
		return worldWithLevelId.m_Id;
	}

	public static string GetLevelTitle()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			return Campaign.GetCurrentLayoutName();
		}
		if (string.IsNullOrEmpty(SandboxSettings.m_Title))
		{
			return Localize.Get("MAINMENU_SANDBOX");
		}
		return SandboxSettings.m_Title;
	}

	public static string GetLevelTitleWithoutPrefix()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			if (!(Campaign.m_CurrentLevel != null))
			{
				return string.Empty;
			}
			return Campaign.m_CurrentLevel.GetLocalizedDisplayNameWithoutPrefix();
		}
		return SandboxSettings.m_Title;
	}

	public static bool IsCurrentLevelTutorial()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null)
		{
			return Campaign.m_CurrentLevel.IsTutorial();
		}
		return false;
	}

	public static void RefreshAfterOrthographicSizeChange()
	{
		Bridge.RefreshZoomDependentVisibility();
		Outlines.RefreshAfterOrthographicSizeChange();
		Vehicles.UpdateWheelsLineWidth();
		WaterLine.RefreshAfterOrthographicSizeChange();
		WaterRulers.RefreshAfterOrthographicSizeChange();
	}

	public static void SetCameraCullingMasks(GameState gameState)
	{
		switch (gameState)
		{
		case GameState.BUILD:
			Cameras.RenderLastCamera().cullingMask = Utils.EDGE_LAYER_MASK | Utils.JOINT_LAYER_MASK | Utils.SPLIT_JOINT_NUMBER_LAYER_MASK | Utils.JOINT_SELECTOR_LAYER_MASK | Utils.RENDER_LAST_LAYER_MASK | Utils.PISTON_LAYER_MASK | Utils.PICKUP_BY_VEHICLE_LAYER_MASK | Utils.DEFAULT_LAYER_MASK | Utils.SPLINE_CONTROL_POINT_MASK | Utils.UI_LAYER_MASK | Utils.SPRING_LAYER_MASK;
			Cameras.ForegroundCamera().cullingMask = Utils.FOREGROUND_LAYER_MASK | Utils.SCENEGEO_LAYER_MASK | Utils.BRIDGE_PILLAR_LAYER_MASK | Utils.CUSTOM_SHAPE_LAYER_MASK;
			Cameras.MainCamera().cullingMask = Utils.TERRAIN_LAYER_MASK | Utils.SCENEGEOSTATIC_LAYER_MASK | Utils.SKY_LAYER_MASK | Utils.WATER_LAYER_MASK | Utils.DECOR_LAYER_MASK;
			Cameras.BuildZoneCamera().cullingMask = Utils.BUILD_ZONE_LAYER_MASK | Utils.VEHICLE_LAYER_MASK;
			Cameras.ReplayCamera().gameObject.SetActive(value: false);
			Cameras.SplashCamera().gameObject.SetActive(value: false);
			Cameras.ForegroundCamera().gameObject.SetActive(value: true);
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
			Cameras.SetForwardRenderer(URP.FORWARD_RENDERER);
			break;
		case GameState.SANDBOX:
			Cameras.RenderLastCamera().cullingMask = Utils.SPLIT_JOINT_NUMBER_LAYER_MASK | Utils.JOINT_SELECTOR_LAYER_MASK | Utils.RENDER_LAST_LAYER_MASK | Utils.PISTON_LAYER_MASK | Utils.VEHICLE_LAYER_MASK | Utils.PICKUP_BY_VEHICLE_LAYER_MASK | Utils.DEFAULT_LAYER_MASK | Utils.SPLINE_CONTROL_POINT_MASK | Utils.UI_LAYER_MASK | Utils.SPRING_LAYER_MASK;
			Cameras.ForegroundCamera().cullingMask = Utils.FOREGROUND_LAYER_MASK | Utils.SCENEGEO_LAYER_MASK | Utils.CUSTOM_SHAPE_LAYER_MASK;
			Cameras.MainCamera().cullingMask = Utils.TERRAIN_LAYER_MASK | Utils.SCENEGEOSTATIC_LAYER_MASK | Utils.SKY_LAYER_MASK | Utils.WATER_LAYER_MASK | Utils.BRIDGE_PILLAR_LAYER_MASK | Utils.EDGE_LAYER_MASK | Utils.JOINT_LAYER_MASK;
			Cameras.BuildZoneCamera().cullingMask = Utils.BUILD_ZONE_LAYER_MASK;
			Cameras.ReplayCamera().gameObject.SetActive(value: false);
			Cameras.SplashCamera().gameObject.SetActive(value: false);
			Cameras.ForegroundCamera().gameObject.SetActive(value: true);
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
			Cameras.SetForwardRenderer(URP.FORWARD_RENDERER);
			break;
		case GameState.DECOR:
			Cameras.ForegroundCamera().cullingMask = Utils.FOREGROUND_LAYER_MASK;
			Cameras.RenderLastCamera().cullingMask = Utils.RENDER_LAST_LAYER_MASK | Utils.UI_LAYER_MASK;
			Cameras.MainCamera().cullingMask = ~(Utils.FOREGROUND_LAYER_MASK | Utils.RENDER_LAST_LAYER_MASK | Utils.BRIDGE_PREVIEW_LAYER_MASK | Utils.NO_RENDER_LAYER_MASK);
			Cameras.BuildZoneCamera().cullingMask = Utils.BUILD_ZONE_LAYER_MASK;
			Cameras.ReplayCamera().gameObject.SetActive(value: false);
			Cameras.SplashCamera().gameObject.SetActive(value: false);
			Cameras.ForegroundCamera().gameObject.SetActive(value: true);
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
			Cameras.SetForwardRenderer(URP.FORWARD_RENDERER);
			break;
		default:
			Cameras.MainCamera().cullingMask = ~(Utils.FOREGROUND_LAYER_MASK | Utils.RENDER_LAST_LAYER_MASK | Utils.BRIDGE_PREVIEW_LAYER_MASK | Utils.NO_RENDER_LAYER_MASK);
			Cameras.ReplayCamera().cullingMask = ((gameState == GameState.SIM) ? Cameras.MainCamera().cullingMask : 0);
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
			Cameras.ForegroundCamera().gameObject.SetActive(value: false);
			if (gameState == GameState.SIM || gameState == GameState.MAIN_MENU || gameState == GameState.PHOTO)
			{
				Cameras.SetForwardRenderer(URP.FORWARD_RENDERER_SSAO);
				Cameras.SplashCamera().gameObject.SetActive(value: true);
			}
			else
			{
				Cameras.SetForwardRenderer(URP.FORWARD_RENDERER);
				Cameras.SplashCamera().gameObject.SetActive(value: false);
			}
			break;
		}
	}

	public static float MinOrthographicSize()
	{
		return GameSettings.MinOrthographicSize();
	}

	public static float MaxOrthographicSize()
	{
		return GameSettings.MaxOrthographicSize();
	}

	public static void SelectFirstValidMaterial()
	{
		if (Bridge.m_BuildMaterialType == BridgeMaterialType.INVALID || Budget.HasZeroBudget(Bridge.m_BuildMaterialType))
		{
			if (!Budget.HasZeroBudget(BridgeMaterialType.ROAD) && Bridge.m_BuildMaterialType != BridgeMaterialType.ROAD)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.ROAD;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.ROAD, animateTransition: false);
			}
			else if (!Budget.HasZeroBudget(BridgeMaterialType.REINFORCED_ROAD) && !SandboxSettings.m_NoReinforcedRoad && Bridge.m_BuildMaterialType != BridgeMaterialType.REINFORCED_ROAD)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.REINFORCED_ROAD;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.REINFORCED_ROAD, animateTransition: false);
			}
			else if (!Budget.HasZeroBudget(BridgeMaterialType.WOOD) && Bridge.m_BuildMaterialType != BridgeMaterialType.WOOD)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.WOOD;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.WOOD, animateTransition: false);
			}
			else if (!Budget.HasZeroBudget(BridgeMaterialType.STEEL) && Bridge.m_BuildMaterialType != BridgeMaterialType.STEEL)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.STEEL;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.STEEL, animateTransition: false);
			}
			else if (!Budget.HasZeroBudget(BridgeMaterialType.HYDRAULICS) && Bridge.m_BuildMaterialType != BridgeMaterialType.HYDRAULICS)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.HYDRAULICS;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.HYDRAULICS, animateTransition: false);
			}
			else if (!Budget.HasZeroBudget(BridgeMaterialType.ROPE) && Bridge.m_BuildMaterialType != BridgeMaterialType.ROPE)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.ROPE;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.ROPE, animateTransition: false);
			}
			else if (!Budget.HasZeroBudget(BridgeMaterialType.CABLE) && Bridge.m_BuildMaterialType != BridgeMaterialType.CABLE)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.CABLE;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.CABLE, animateTransition: false);
			}
			else if (!Budget.HasZeroBudget(BridgeMaterialType.SPRING) && Bridge.m_BuildMaterialType != BridgeMaterialType.SPRING)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.SPRING;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.SPRING, animateTransition: false);
			}
			else if (!Budget.HasZeroBudget(BridgeMaterialType.PILLAR) && Bridge.m_BuildMaterialType != BridgeMaterialType.PILLAR)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.PILLAR;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.PILLAR, animateTransition: false);
			}
			else if (Bridge.m_BuildMaterialType == BridgeMaterialType.INVALID)
			{
				Bridge.m_BuildMaterialType = BridgeMaterialType.ROAD;
				GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.ROAD, animateTransition: false);
			}
		}
	}

	public static bool InDecorModeTopView()
	{
		if (GameStateManager.GetState() == GameState.DECOR)
		{
			return GameStateDecor.m_PointOfViewType == PointOfViewType.DECOR_TOP;
		}
		return false;
	}

	public static bool InDecorModeFrontView()
	{
		if (GameStateManager.GetState() == GameState.DECOR)
		{
			if (GameStateDecor.m_PointOfViewType != PointOfViewType.DECOR_CENTER)
			{
				return GameStateDecor.m_PointOfViewType == PointOfViewType.DECOR_CUSTOM;
			}
			return true;
		}
		return false;
	}

	public static void DesaturateAllVehiclesFlagsAndCheckpointsExcept(string guid)
	{
		Vehicles.DesaturateAllExcept(guid);
		VehicleStopTriggers.DesaturateAllExceptForVehicle(guid);
		Checkpoints.DesaturateAllExceptForVehicle(guid);
	}

	public static void UnDesaturateAllVehiclesFlagsAndCheckpoints()
	{
		foreach (Vehicle vehicle in Vehicles.m_Vehicles)
		{
			if (vehicle.m_SandboxItem.m_Desaturated)
			{
				vehicle.m_SandboxItem.Desaturate(on: false);
			}
		}
		foreach (VehicleStopTrigger trigger in VehicleStopTriggers.m_Triggers)
		{
			if (trigger.m_SandboxItem.m_Desaturated)
			{
				trigger.m_SandboxItem.Desaturate(on: false);
			}
		}
		foreach (Checkpoint checkpoint in Checkpoints.m_Checkpoints)
		{
			if (checkpoint.m_SandboxItem.m_Desaturated)
			{
				checkpoint.m_SandboxItem.Desaturate(on: false);
				checkpoint.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y, 0f);
				checkpoint.SetOutlineColor();
			}
		}
	}

	public static void MaybeShowPauseOnBreakPopup()
	{
		try
		{
			string path = Path.Combine(Application.persistentDataPath, Profiles.ROOT_DIRECTORY_NAME, ".firstbreakwarning");
			if (!File.Exists(path))
			{
				PopUpMessage.DisplayInfoOkOnly(string.Format(Localize.Get("UI_FIRST_TIME_PAUSE_ON_BREAK"), Bindings.m_Bindings[BindingType.PAUSE_ON_BREAK].GetTooltipBindingString()));
				File.WriteAllText(path, "");
			}
		}
		catch (Exception ex)
		{
			Debug.Log("Exception in MaybeShowPauseOnBreakPopup: " + ex.Message);
		}
	}

	public static void DoQuickSave()
	{
		if (GameManager.GetGameMode() == GameMode.SANDBOX)
		{
			if (string.IsNullOrEmpty(Sandbox.m_CurrentLayoutName))
			{
				GameUI.m_Instance.m_SaveSandboxLayout.gameObject.SetActive(value: true);
				return;
			}
			SandboxItems.CancelMovementDueToModalMenuOpening();
			Sandbox.Save(Sandbox.m_CurrentLayoutName);
			GameUI.m_Instance.m_PauseMenu.CloseSilent();
		}
		else if (GameStateManager.GetState() == GameState.BUILD && !IsCurrentLevelTutorial() && !BridgeSaveSlots.TryQuickSave())
		{
			GameUI.m_Instance.m_TopBar.OnSaveAs();
		}
	}

	public static string GetLocalizedTargetFramerate(int vsyncInterval)
	{
		switch (vsyncInterval)
		{
		case 0:
			return Localize.Get("UI_NO_MAX_FRAMERATE");
		case 1:
			return Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value).ToString();
		case 2:
			return Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value / 2f).ToString();
		default:
			Debug.LogWarning($"Unexpected vsync interval: {vsyncInterval}");
			return Screen.currentResolution.refreshRateRatio.value.ToString();
		}
	}

	public static int GetDefaultVsyncInterval()
	{
		if (SystemInfo.systemMemorySize >= 16000)
		{
			return 1;
		}
		return 2;
	}

	public static void ForceIgnoreNextSelection()
	{
		if (GameStateManager.GetState() == GameState.DECOR)
		{
			GameStateDecor.ForceIgnoreNextSelection();
		}
		else if (GameStateManager.GetState() == GameState.BUILD)
		{
			GameStateBuild.ForceIgnoreNextSelection();
		}
		else if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			SandboxInput.ForceIgnoreNextSelection();
		}
	}

	public static bool IsSteamDeckOrMobile()
	{
		if (!IsRunningOnSteamDeck())
		{
			return IsMobile();
		}
		return true;
	}

	public static bool IsRunningOnSteamDeck()
	{
		if (m_ForceSteamDeck)
		{
			return true;
		}
		return SteamUtils.IsRunningOnSteamDeck();
	}

	public static bool IsMobile()
	{
		if (m_ForceMobile)
		{
			return true;
		}
		return false;
	}
}
