using UnityEngine;

public class GameManager : SceneSingleton<GameManager>
{
	[SerializeField]
	private bool dontLoadPotions;

	[SerializeField]
	private bool dontLoadPuzzles;

	[SerializeField]
	private bool skipTutorial;

	public bool isLoadingGame;

	public bool isGameEnded;

	private void Start()
	{
		LoadLevel();
		if (PlayerPrefs.GetInt("OpenMainMenu") == 1)
		{
			EventManager.ActivateEvent(EventTypes.MainMenuLoaded);
		}
		else
		{
			ContinueGame();
		}
	}

	private void LoadLevel()
	{
		if (!SaveSystem.disableSavingSystem)
		{
			isLoadingGame = true;
			SaveSystem.LoadLevelSaveData();
			isGameEnded = SaveSystem.currentlevelSaveData.isGameEnded;
			Singleton<PotionsDataHolder>.Instance.solvedPuzzlesList.Clear();
			Singleton<PotionsDataHolder>.Instance.solvedPuzzlesList.AddRange(SaveSystem.currentlevelSaveData.solvedSortingPuzzleTypesList);
			SceneSingleton<SolvedPuzzlesPanelUI>.Instance.LoadPuzzles();
			SceneSingleton<GameTimeManager>.Instance.LoadTimer();
			SceneSingleton<PotionsManager>.Instance.LoadPotions(SaveSystem.currentlevelSaveData.potionSaveDatasList);
			if (SceneSingleton<PuzzlesManager>.Instance != null)
			{
				SceneSingleton<PuzzlesManager>.Instance.LoadData(SaveSystem.currentlevelSaveData.puzzleSaveData);
			}
			SceneSingleton<CharacterInventory>.Instance.RefreshItemArt(isLoading: true);
			SceneSingleton<CabinetsManager>.Instance.CheckShelves();
			SceneSingleton<UpgradesManager>.Instance.LoadUpgradeManager(SaveSystem.currentlevelSaveData);
			SceneSingleton<CatsManager>.Instance.LoadCatsManager(SaveSystem.currentlevelSaveData);
			SceneSingleton<AbilitiesManager>.Instance.LoadData();
			isLoadingGame = false;
		}
	}

	public void ResetAllPotionsItemsToStartingPostions()
	{
		SceneSingleton<PotionsManager>.Instance.ResetAllPotionsItemsToStartingPostions();
		SceneSingleton<PuzzlesManager>.Instance.ResetAllPotionsItemsToStartingPostions();
	}

	private void ResetandStartNewGame()
	{
		Singleton<PotionsDataHolder>.Instance.solvedPuzzlesList.Clear();
		SaveSystem.DeleteSavedLevel();
		UIBackKeyManager.ClearQueue();
		PlayerPrefs.SetInt("OpenMainMenu", 0);
		Singleton<ScenesManager>.Instance.GoToGameScene();
	}

	public void EndGame()
	{
		if (!isGameEnded)
		{
			isGameEnded = true;
			UpdateAchievement();
			SaveSystem.SaveLevel();
			EventManager.ActivateEvent(EventTypes.GameEnd);
		}
	}

	private void UpdateAchievement()
	{
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Shelf_All);
		if (SceneSingleton<AbilitiesManager>.Instance.abilitiesUsed == 0)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Finish_NoAbilities);
		}
	}

	public void StartNewGame()
	{
		if (SaveSystem.IsThereASavedLevel())
		{
			SceneSingleton<UIManager>.Instance.ShowConfirmationPanelUI("Menu_Overwrite", ResetandStartNewGame);
		}
		else
		{
			ContinueGame();
		}
	}

	public void ContinueGame()
	{
		EventManager.ActivateEvent(EventTypes.GameStart);
		if (SaveSystem.IsThereASavedLevel())
		{
			SceneSingleton<TutorialManager>.Instance.CompleteTutorial(showCutscene: false);
		}
		else if (skipTutorial)
		{
			SceneSingleton<TutorialManager>.Instance.CompleteTutorial(showCutscene: false);
		}
		else
		{
			SceneSingleton<TutorialManager>.Instance.StartTutorial();
			SceneSingleton<UIManager>.Instance.ShowPuzzleTextPanel(PuzzleTextTypes.Conversation_Start);
		}
		SceneSingleton<AbilitiesPanelUI>.Instance.LoadAbilitiesPanelUI();
	}

	public void PauseGame()
	{
		EventManager.ActivateEvent(EventTypes.GamePause);
		UIBackKeyManager.ClearQueue();
		UIBackKeyManager.AddOnBackKeyPressed(ResumeGame);
	}

	public void ResumeGame()
	{
		EventManager.ActivateEvent(EventTypes.GameResume);
		UIBackKeyManager.ClearQueue();
		UIBackKeyManager.AddOnBackKeyPressed(PauseGame);
	}

	public void GoBackToMainMenu()
	{
		SaveSystem.SaveLevel();
		UIBackKeyManager.ClearQueue();
		PlayerPrefs.SetInt("OpenMainMenu", 1);
		Singleton<ScenesManager>.Instance.GoToGameScene();
	}
}
