using System.IO;
using UnityEngine;

public class Campaign
{
	public static CampaignLevel m_CurrentLevel;

	public static CampaignLevel m_LevelBeingPreloaded;

	public static CampaignProgress m_CampaignProgress = new CampaignProgress();

	public static void Init()
	{
		CampaignTutorial.Init();
		CampaignWorlds.m_Instance.SetDefaultProgress();
	}

	public static void UpdateManual()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			CampaignTutorial.UpdateManual();
			MaybeIncrementElapsedSeconds();
		}
	}

	public static bool LoadNextLevel()
	{
		CampaignLevel nextLevel = CampaignWorlds.m_Instance.GetNextLevel(m_CurrentLevel);
		if (nextLevel == null)
		{
			return false;
		}
		if (m_CampaignProgress.IsLocked(nextLevel.m_Id))
		{
			return false;
		}
		GameStatePreloadingAssets.PreloadLevel(nextLevel.GetLayoutPath(), null, DoneLevelPreload);
		return true;
	}

	public static void LoadPreviousLevel()
	{
		CampaignLevel prevLevel = CampaignWorlds.m_Instance.GetPrevLevel(m_CurrentLevel);
		if (!(prevLevel == null))
		{
			GameStatePreloadingAssets.PreloadLevel(prevLevel.GetLayoutPath(), null, DoneLevelPreload);
		}
	}

	public static bool LoadLevel(CampaignLevel level)
	{
		if (!LoadLayout(level))
		{
			return false;
		}
		m_CurrentLevel = level;
		if (m_CurrentLevel.IsTutorial())
		{
			CampaignTutorial.m_Completed = false;
		}
		if (!string.IsNullOrEmpty(GetCurrentLayoutName()) && !m_CurrentLevel.IsTutorial())
		{
			ShowLevelName();
		}
		GameUI.m_Instance.m_TopBar.UpdateLevelNavButtons();
		if (!DumpPreviewImages.m_Dumping && !DumpReplays.m_Dumping)
		{
			bool flag = false;
			if (!Profiles.m_ActiveProfile.LastPlayedLevelIDAlreadyStored(m_CurrentLevel.m_Id))
			{
				Profiles.m_ActiveProfile.SetLastPlayedLevelIDForWorld(m_CurrentLevel.m_Id);
				flag = true;
			}
			if (Profiles.m_ActiveProfile.m_LastLoadedCampaignLevelId != m_CurrentLevel.m_Id)
			{
				Profiles.m_ActiveProfile.m_LastLoadedCampaignLevelId = m_CurrentLevel.m_Id;
				flag = true;
			}
			if (flag)
			{
				Profiles.SaveActiveProfile();
			}
			Prefabs.m_Instance.UnloadAssetsNotInLayout(level.GetLayoutPath());
			PreloadNextLevelInBackground();
		}
		return true;
	}

	public static bool LoadLayout(CampaignLevel level)
	{
		SandboxLayoutData sandboxLayoutData = SandboxLayout.Load(GetLevelsPath(level.m_Id), level.m_Filename);
		if (sandboxLayoutData == null)
		{
			Debug.LogWarningFormat("Could not load: {0}", level.m_Filename);
			return false;
		}
		Game.AddLevelChecksum(level.m_Id, sandboxLayoutData.GenerateChecksum());
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(level.m_Id);
		string text = ((worldWithLevelId != null) ? worldWithLevelId.m_ThemePreloadStub.m_ID : sandboxLayoutData.m_ThemeStubId);
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		BridgeSaveSlots.ClearSlots();
		BridgeSaveSlots.LoadSlots(Path.GetFileNameWithoutExtension(level.m_Filename), Profiles.GetActiveProfileName());
		Sandbox.Clear();
		BridgeSaveSlotData autoSave = BridgeSaveSlots.GetAutoSave();
		Sandbox.Load(text, sandboxLayoutData, loadBridge: false);
		PointsOfView.OnLayoutLoaded(level.m_Id);
		if (level.m_Id == "001")
		{
			GameAchievements.StartSpeedRunnerTimer();
		}
		if (Profiles.m_ActiveProfile.m_AutomatiallyLoadAutoSave && autoSave != null && !level.IsTutorial() && !DumpPreviewImages.m_Dumping && !DumpReplays.m_Dumping)
		{
			BridgeSaveData bridgeSaveData = Bridge.ClearAndLoadBinary(autoSave.m_Bridge);
			if (bridgeSaveData != null)
			{
				BridgeCheat.CheckForCheating(Sandbox.m_CurrentLayoutData, bridgeSaveData, level.m_Id);
				Bridge.Sanitize();
			}
			Budget.MaybeApplyForcedBudgets(autoSave.m_UsingUnlimitedBudget, autoSave.m_UsingUnlimitedMaterials);
			GameAchievements.InvalidateSpeedRunnerTimer();
		}
		return true;
	}

	public static string GetCurrentLayoutFilename()
	{
		if (!(m_CurrentLevel != null))
		{
			return string.Empty;
		}
		return m_CurrentLevel.m_Filename;
	}

	public static string GetCurrentLayoutName()
	{
		if (!(m_CurrentLevel != null))
		{
			return string.Empty;
		}
		return m_CurrentLevel.GetLocalizedDisplayNameWithPrefix();
	}

	public static string GetCurrentLevelId()
	{
		if (!(m_CurrentLevel != null))
		{
			return string.Empty;
		}
		return m_CurrentLevel.m_Id;
	}

	public static string GetPreviousLayoutName()
	{
		CampaignLevel prevLevel = CampaignWorlds.m_Instance.GetPrevLevel(m_CurrentLevel);
		if (!(prevLevel != null) || m_CampaignProgress.IsLocked(prevLevel.m_Id))
		{
			return string.Empty;
		}
		return prevLevel.GetLocalizedDisplayNameWithPrefix();
	}

	public static string GetNextLayoutFilename()
	{
		CampaignLevel nextLevel = CampaignWorlds.m_Instance.GetNextLevel(m_CurrentLevel);
		if (!(nextLevel != null) || m_CampaignProgress.IsLocked(nextLevel.m_Id))
		{
			return string.Empty;
		}
		return nextLevel.m_Filename;
	}

	public static string GetLevelsPath(string levelID)
	{
		if (CampaignWorlds.m_Instance.IsMainMenuLevel(levelID))
		{
			return Path.Combine(Application.streamingAssetsPath, "MainMenuLevels");
		}
		return Path.Combine(Application.streamingAssetsPath, "Levels");
	}

	public static bool HasCompletedAllLevels()
	{
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		for (int i = 0; i < worlds.Length; i++)
		{
			CampaignLevel[] levels = worlds[i].m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (!m_CampaignProgress.HasCompletedLevel(campaignLevel.m_Id))
				{
					return false;
				}
			}
		}
		return true;
	}

	public static bool HasCompletedAllNonSecretLevels()
	{
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (campaignWorld.IsSecretWorld())
			{
				continue;
			}
			CampaignLevel[] levels = campaignWorld.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (!m_CampaignProgress.HasCompletedLevel(campaignLevel.m_Id))
				{
					return false;
				}
			}
		}
		return true;
	}

	public static bool HasStartedCampaign()
	{
		return m_CampaignProgress.m_State.Count > 0;
	}

	public static void UpdateReservedSaves(int numBreaks, float maxStressNormalized)
	{
		string directoryForSaveSlot = BridgeSaveSlots.GetDirectoryForSaveSlot();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		int num = GameLeaderboards.ConvertStressToScore(maxStressNormalized);
		if (Budget.IsUnderBudget(Budget.m_BridgeCost) && num < BridgeSaveSlots.GetEncodedLowestStressForSlotID(3))
		{
			flag = !BridgeSaveSlots.SaveReserved(directoryForSaveSlot, ReservedSlot.LOWEST_STRESS, GameState.SIM);
		}
		if (numBreaks == 0)
		{
			if (Mathf.RoundToInt(Budget.m_BridgeCost) < BridgeSaveSlots.GetBudgetForReservedSlot(ReservedSlot.BUDGET_PERFECTION))
			{
				flag3 = !BridgeSaveSlots.SaveReserved(directoryForSaveSlot, ReservedSlot.BUDGET_PERFECTION, GameState.SIM);
				if (BridgeSaveSlots.GetBudgetForReservedSlot(ReservedSlot.BUDGET_PERFECTION) <= BridgeSaveSlots.GetBudgetForReservedSlot(ReservedSlot.BUDGET))
				{
					BridgeSaveSlots.DeleteReserved(directoryForSaveSlot, ReservedSlot.BUDGET);
				}
			}
		}
		else if (Mathf.RoundToInt(Budget.m_BridgeCost) < BridgeSaveSlots.GetBudgetForReservedSlot(ReservedSlot.BUDGET_PERFECTION) && Mathf.RoundToInt(Budget.m_BridgeCost) < BridgeSaveSlots.GetBudgetForReservedSlot(ReservedSlot.BUDGET))
		{
			flag2 = !BridgeSaveSlots.SaveReserved(directoryForSaveSlot, ReservedSlot.BUDGET, GameState.SIM);
		}
		if (flag)
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("WARN_LOWEST_STRESS_SAVE_FAIL"));
		}
		else if (flag2)
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("WARN_BUDGET_SAVE_FAIL"));
		}
		else if (flag3)
		{
			PopUpMessage.DisplayErrorOkOnly(Localize.Get("WARN_NOBREAKS_BUDGET_SAVE_FAIL"));
		}
	}

	public static void DonePreloadFromMainMenu(string layoutFilename, FileSlot slot)
	{
		if (LoadLevel(m_LevelBeingPreloaded))
		{
			GameManager.SetGameMode(GameMode.CAMPAIGN, GameSubMode.NONE);
			GameStateManager.SwitchToState(GameState.BUILD);
			GameUI.m_Instance.m_Campaign.Close();
		}
		else
		{
			PopUpMessage.DisplayErrorOkOnly($"Failed to load {m_LevelBeingPreloaded.GetLocalizedDisplayNameWithPrefix()}");
		}
		BridgeCheat.m_ForceUnlimitedBudget = false;
		BridgeCheat.m_ForceUnlimitedMaterial = false;
	}

	public static void ShowLevelName()
	{
		string currentLayoutName = GetCurrentLayoutName();
		GameUI.ShowMessage(ScreenMessageLocation.TOP_LEFT, currentLayoutName, 5f);
	}

	public static string GetLayoutFullPathFromId(string id)
	{
		return Path.Combine(GetLevelsPath(id), id + ".layout");
	}

	public static string FormatDifficultyLabel(int numStars)
	{
		string text = Localize.Get("UI_WORKSHOP_DIFFICULTY") + ": ";
		for (int i = 0; i < numStars; i++)
		{
			text += "<sprite name=Map_Sheep>";
		}
		return text;
	}

	public static string FormatUnlockHelp(int numStars)
	{
		string text = string.Empty;
		for (int i = 0; i < numStars - 1; i++)
		{
			text += "<sprite name=Map_Sheep>";
		}
		int num = CampaignProgress.NUM_LEVELS_TO_UNLOCK_2STAR_WORLDS;
		switch (numStars)
		{
		case 3:
			num = CampaignProgress.NUM_LEVELS_TO_UNLOCK_3STAR_WORLDS;
			break;
		case 4:
			num = CampaignProgress.NUM_LEVELS_TO_UNLOCK_4STAR_WORLDS;
			break;
		}
		return Localize.Get("POPUP_UNLOCK_WORLD_HELP", num.ToString(), text);
	}

	public static void SaveLastSolvedCampaignLevelId()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && m_CurrentLevel != null && Profiles.m_ActiveProfile.m_LastSolvedCampaignLevelId != m_CurrentLevel.m_Id)
		{
			Profiles.m_ActiveProfile.m_LastSolvedCampaignLevelId = m_CurrentLevel.m_Id;
			Profiles.SaveActiveProfile();
		}
	}

	public static void ClearLastSolvedCampaignLevelId()
	{
		if (!string.IsNullOrEmpty(Profiles.m_ActiveProfile.m_LastSolvedCampaignLevelId))
		{
			Profiles.m_ActiveProfile.m_LastSolvedCampaignLevelId = string.Empty;
			Profiles.SaveActiveProfile();
		}
	}

	public static int GetNumLevels()
	{
		int num = 0;
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			num += campaignWorld.m_Levels.Length;
		}
		return num;
	}

	private static void PreloadNextLevelInBackground()
	{
		CampaignLevel nextLevel = CampaignWorlds.m_Instance.GetNextLevel(m_CurrentLevel);
		if (nextLevel != null)
		{
			GameStatePreloadingAssets.PreloadLevelInBackground(nextLevel.GetLayoutPath());
		}
		if (GameUI.m_Instance.m_TopBar.m_LevelNavButtons.activeInHierarchy)
		{
			CampaignLevel prevLevel = CampaignWorlds.m_Instance.GetPrevLevel(m_CurrentLevel);
			if (prevLevel != null)
			{
				GameStatePreloadingAssets.PreloadLevelInBackground(prevLevel.GetLayoutPath());
			}
		}
	}

	private static void PostLoadStateTransition()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			GameStateSim.m_SkipBridgeRestoreOnExit = true;
		}
		if (GameStateManager.GetState() != GameState.BUILD)
		{
			GameStateManager.SwitchToState(GameState.BUILD);
		}
	}

	private static void MaybeIncrementElapsedSeconds()
	{
		if (!GameManager.IsPaused() && (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SIM) && !m_CampaignProgress.HasCompletedLevel(m_CurrentLevel.m_Id))
		{
			CampaignLevelState campaignLevelState = m_CampaignProgress.GetCampaignLevelState(m_CurrentLevel.m_Id);
			if (campaignLevelState != null)
			{
				campaignLevelState.m_ElapsedSeconds += Time.unscaledDeltaTime;
			}
		}
	}

	private static void DoneLevelPreload(string layoutPath, FileSlot slot)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(layoutPath);
		CampaignLevel levelFromId = CampaignWorlds.m_Instance.GetLevelFromId(fileNameWithoutExtension);
		if (levelFromId == null)
		{
			Debug.Log("Could not load campaign level with id '" + fileNameWithoutExtension + "'");
		}
		else if (LoadLevel(levelFromId))
		{
			if (GameStateManager.GetState() == GameState.BUILD)
			{
				GameStateBuild.Exit(GameState.INVALID);
				GameStateBuild.Enter(GameState.INVALID);
			}
			PostLoadStateTransition();
		}
	}
}
