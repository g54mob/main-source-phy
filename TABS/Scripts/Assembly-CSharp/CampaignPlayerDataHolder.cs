using System.Linq;
using DM;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using LevelCreator;
using UnityEngine;

public static class CampaignPlayerDataHolder
{
	private static CustomMap storedCustomMap;

	private static int _LevelIndex;

	private static bool _saveGameWhenCompleteLevel;

	public static GameModeState CurrentGameModeState { get; private set; }

	public static DatabaseID GetCurrentCampaignID => SelectedCampaign.Entity.GUID;

	public static TABSCampaignAsset SelectedCampaign { get; private set; }

	public static bool AutoLoadNextLevel { get; private set; }

	public static bool PlayingSingleBattles { get; private set; }

	public static CustomMap StoredCustomMap
	{
		get
		{
			return storedCustomMap;
		}
		set
		{
			storedCustomMap = value;
		}
	}

	public static TABSCampaignLevelAsset[] GetLevelsInCurrentCampaign()
	{
		return SelectedCampaign.LevelsInCampaign;
	}

	public static TABSCampaignLevelAsset GetCurrentLevel()
	{
		if (SelectedCampaign == null)
		{
			Debug.LogError("CampaignPlayerDataHolder: Selected Campaign is null.");
		}
		return SelectedCampaign.LevelsInCampaign[_LevelIndex];
	}

	public static TABSCampaignLevelAsset GetPreviousLevel()
	{
		if (_LevelIndex == 0)
		{
			return null;
		}
		return SelectedCampaign.LevelsInCampaign[_LevelIndex - 1];
	}

	private static void NextLevel()
	{
		_LevelIndex++;
		if (_LevelIndex < SelectedCampaign.LevelsInCampaign.Length && !(GetCurrentLevel() != null) && !ContentDatabase.Instance().HasCampaignLevel(GetCurrentLevel().Entity.GUID))
		{
			Debug.Log("Next Campaign Level is not loaded, skipping that");
			NextLevel();
		}
	}

	public static void StartedPlayingBattle(TABSCampaignLevelAsset newBattle)
	{
		if (newBattle == null)
		{
			return;
		}
		TABSCampaignAsset tABSCampaignAsset = new TABSCampaignAsset();
		tABSCampaignAsset.SetData("LABEL_CUSTOM_BATTLES_CAMPAIGN_TITLE", ContentDatabase.Instance().GetUserCampaignLevels().ToArray());
		int startIndex = 0;
		for (int i = 0; i < tABSCampaignAsset.LevelsInCampaign.Length; i++)
		{
			TABSCampaignLevelAsset tABSCampaignLevelAsset = tABSCampaignAsset.LevelsInCampaign[i];
			if (tABSCampaignLevelAsset != null && tABSCampaignLevelAsset.Entity != null && tABSCampaignLevelAsset.Entity.GUID == newBattle.Entity.GUID)
			{
				startIndex = i;
				break;
			}
		}
		StartedPlayingNewCampaign(tABSCampaignAsset, startIndex);
		PlayingSingleBattles = true;
	}

	public static void StartedPlayingNewCampaign(TABSCampaignAsset newCampaign, int startIndex, bool autoLoadNextLevel = true, bool saveGameWhenCompleteLevel = true)
	{
		AutoLoadNextLevel = autoLoadNextLevel;
		_saveGameWhenCompleteLevel = saveGameWhenCompleteLevel;
		CurrentGameModeState = GameModeState.Campaign;
		SelectedCampaign = newCampaign;
		_LevelIndex = startIndex;
		ServiceLocator.GetService<GameModeService>().SetGameMode<CampaignGameMode>();
		PlayingSingleBattles = false;
	}

	public static void StartedPlayingSandbox()
	{
		CurrentGameModeState = GameModeState.Sandbox;
	}

	public static void StartedPlayingLocalMultiplayer()
	{
		CurrentGameModeState = GameModeState.LocalMultiplayer;
	}

	public static void StartedPlayingOnlineMultiplayer()
	{
		CurrentGameModeState = GameModeState.OnlineMultiplayer;
	}

	public static void StartedPlayingTest()
	{
		CurrentGameModeState = GameModeState.Test;
	}

	public static void BackToMenu()
	{
		CurrentGameModeState = GameModeState.Menu;
	}

	public static void SetToNone()
	{
		CurrentGameModeState = GameModeState.None;
	}

	public static void CompletedCurrentLevel()
	{
		if (_saveGameWhenCompleteLevel)
		{
			TABSCampaignLevelAsset currentLevel = GetCurrentLevel();
			Debug.Log("Completed Level: " + currentLevel.Entity.Name + " : " + currentLevel.Entity.GUID);
			ISaveLoaderService service = ServiceLocator.GetService<ISaveLoaderService>();
			service.AddBeatenLevel(currentLevel.Entity.GUID, SelectedCampaign.Entity.GUID);
			service.AddWinStreakLevel(currentLevel.Entity.GUID, SelectedCampaign.Entity.GUID);
			service.AddNonPossessedLevel(currentLevel.Entity.GUID, SelectedCampaign.Entity.GUID);
			CheckAchievements();
		}
	}

	public static void LoadNextCampaignLevel()
	{
		NextLevel();
		if (_LevelIndex >= SelectedCampaign.LevelsInCampaign.Length)
		{
			IntroSequence.justBeatCampaignInfo.ThankYouTitle = SelectedCampaign.CampaignInfo.ThankYouTitle;
			IntroSequence.justBeatCampaignInfo.ThankYouText = SelectedCampaign.CampaignInfo.ThankYouText;
			IntroSequence.justBeatCampaignInfo.Description = SelectedCampaign.CampaignInfo.Description;
			IntroSequence.justBeatCampaignInfo.ModID = SelectedCampaign.ModID;
			TABSSceneManager.LoadMainMenu();
		}
		else
		{
			TABSCampaignLevelAsset currentLevel = GetCurrentLevel();
			if (currentLevel.CustomMap != default(DatabaseID))
			{
				storedCustomMap = ContentDatabase.Instance().GetUserMap(currentLevel.CustomMap);
			}
			TABSSceneManager.LoadMap(GetCurrentLevel().MapAsset);
		}
	}

	public static void LoadCampaignLevelWithIndex(int index)
	{
		_LevelIndex = index;
		if (_LevelIndex >= SelectedCampaign.LevelsInCampaign.Length)
		{
			Debug.LogError("Index Is out of range of the current campaign");
			_LevelIndex = SelectedCampaign.LevelsInCampaign.Length - 1;
		}
		TABSSceneManager.LoadMap(GetCurrentLevel().MapAsset);
	}

	public static void CheckAchievements()
	{
		AchievementService service = ServiceLocator.GetService<AchievementService>();
		ISaveLoaderService saveService = ServiceLocator.GetService<ISaveLoaderService>();
		bool beatenAllCampaigns = true;
		if (HasBeatenCampaign(1109072765))
		{
			service.UnlockAchievement("FINISH_FANTASY_EVIL");
		}
		if (HasBeatenCampaign(1001262567))
		{
			service.UnlockAchievement("FINISH_FANTASY_GOOD");
		}
		if (HasBeatenCampaign(1439069936))
		{
			service.UnlockAchievement("FINISH_WILD_WEST");
		}
		if (HasBeatenCampaign(522556719))
		{
			service.UnlockAchievement("FINISH_SPOOKY");
		}
		if (HasBeatenCampaign(324659725))
		{
			service.UnlockAchievement("FINISH_PIRATE");
		}
		if (HasBeatenCampaign(465041334))
		{
			service.UnlockAchievement("FINISH_RENAISSANCE");
		}
		if (HasBeatenCampaign(1681554695))
		{
			service.UnlockAchievement("FINISH_DYNASTY");
		}
		if (HasBeatenCampaign(1972502960))
		{
			service.UnlockAchievement("FINISH_THE_CHALLENGE");
		}
		if (HasBeatenCampaign(1759214182))
		{
			service.UnlockAchievement("FINISH_THE_ADVENTURE");
		}
		if (HasBeatenCampaign(421185042))
		{
			service.UnlockAchievement("FINISH_THE_INTRODUCTION");
		}
		if (HasBeatenCampaign(1621266175))
		{
			service.UnlockAchievement("FINISH_LEGACY");
		}
		if (HasBeatenCampaign(1910922848))
		{
			service.UnlockAchievement("FINISH_SIMULATION");
		}
		if (beatenAllCampaigns)
		{
			service.UnlockAchievement("FINISH_ALL");
		}
		bool finishedAllCampaignsWithoutLosing = true;
		if (FinishedCampaignWithoutLosing(1109072765))
		{
			service.UnlockAchievement("FINISH_FANTASY_EVIL_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(1001262567))
		{
			service.UnlockAchievement("FINISH_FANTASY_GOOD_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(1439069936))
		{
			service.UnlockAchievement("FINISH_WILD_WEST_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(522556719))
		{
			service.UnlockAchievement("FINISH_SPOOKY_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(324659725))
		{
			service.UnlockAchievement("FINISH_PIRATE_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(465041334))
		{
			service.UnlockAchievement("FINISH_RENAISSANCE_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(1681554695))
		{
			service.UnlockAchievement("FINISH_DYNASTY_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(1972502960))
		{
			service.UnlockAchievement("FINISH_THE_CHALLENGE_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(1759214182))
		{
			service.UnlockAchievement("FINISH_THE_ADVENTURE_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(421185042))
		{
			service.UnlockAchievement("FINISH_THE_INTRODUCTION_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(1621266175))
		{
			service.UnlockAchievement("FINISH_LEGACY_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutLosing(1910922848))
		{
			service.UnlockAchievement("FINISH_SIMULATION_NO_DEFEAT");
		}
		if (finishedAllCampaignsWithoutLosing)
		{
			service.UnlockAchievement("FINISH_ALL_NO_DEFEAT");
		}
		if (FinishedCampaignWithoutPossessing(1109072765) || FinishedCampaignWithoutPossessing(1001262567) || FinishedCampaignWithoutPossessing(1439069936) || FinishedCampaignWithoutPossessing(522556719) || FinishedCampaignWithoutPossessing(324659725) || FinishedCampaignWithoutPossessing(465041334) || FinishedCampaignWithoutPossessing(1681554695) || FinishedCampaignWithoutPossessing(1972502960) || FinishedCampaignWithoutPossessing(1759214182) || FinishedCampaignWithoutPossessing(421185042) || FinishedCampaignWithoutPossessing(1621266175) || FinishedCampaignWithoutPossessing(1910922848))
		{
			service.UnlockAchievement("FINISH_NO_POSSESS");
		}
		bool FinishedCampaignWithoutLosing(int campaignId)
		{
			if (campaignId != 0)
			{
				bool num = saveService.HasBeatenCampaignWithoutLosing(new DatabaseID(-1, campaignId));
				if (!num)
				{
					finishedAllCampaignsWithoutLosing = false;
				}
				return num;
			}
			return false;
		}
		bool FinishedCampaignWithoutPossessing(int campaignId)
		{
			if (campaignId != 0)
			{
				bool num = saveService.HasBeatenNonPossessedCamapaign(new DatabaseID(-1, campaignId));
				if (!num)
				{
					finishedAllCampaignsWithoutLosing = false;
				}
				return num;
			}
			return false;
		}
		bool HasBeatenCampaign(int campaignId)
		{
			TABSCampaignAsset campaign = ContentDatabase.Instance().GetCampaign(new DatabaseID(-1, campaignId));
			if (campaign == null)
			{
				return false;
			}
			TABSCampaignLevelAsset[] levelsInCampaign = campaign.LevelsInCampaign;
			foreach (TABSCampaignLevelAsset tABSCampaignLevelAsset in levelsInCampaign)
			{
				if (!saveService.HasBeatenLevel(tABSCampaignLevelAsset.Entity.GUID, campaign.Entity.GUID))
				{
					beatenAllCampaigns = false;
					return false;
				}
			}
			return true;
		}
	}
}
