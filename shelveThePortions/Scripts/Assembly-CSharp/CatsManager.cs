using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class CatsManager : SceneSingleton<CatsManager>
{
	[SerializeField]
	private List<CatController> catControllersList = new List<CatController>();

	[ReadOnly]
	public CatController currentCatController;

	[SerializeField]
	[ReadOnly]
	private PotionController potionController;

	private CatPanelUI catPanelUI;

	private CharacterInventory characterInventory;

	public bool isCatOn;

	[ReadOnly]
	public int catsPettedCount;

	private float highlightPuzzleItemsCooldown = 300f;

	private float revealSolutionCooldown = 300f;

	public float currentHighlightPuzzleTimer;

	public float currentRevealSolutionTimer;

	private void Start()
	{
		catPanelUI = SceneSingleton<CatPanelUI>.Instance;
		characterInventory = SceneSingleton<CharacterInventory>.Instance;
	}

	public void LoadCatsManager(LevelSaveData levelSaveData)
	{
		currentHighlightPuzzleTimer = levelSaveData.highlightPuzzleTimer;
		currentRevealSolutionTimer = levelSaveData.revealSolutionTimer;
		catsPettedCount = levelSaveData.catsPettedCount;
		LoadCatsPettedCount(levelSaveData.isCatsPettedList);
	}

	public void InteractWithCat(CatController currentCatController)
	{
		this.currentCatController = currentCatController;
		catPanelUI.SetCatName(currentCatController.catNameID);
		EventManager.ActivateEvent(EventTypes.CatUILoaded);
		isCatOn = true;
		if (currentCatController.isKitten)
		{
			catPanelUI.SetKittenPanelUI();
		}
	}

	public void GoToPotionCamera()
	{
		currentCatController.GoToPotionCamera();
	}

	public void GotoUpgradesCamera()
	{
		SceneSingleton<HoveredPotionController>.Instance.SetIsCatPotionOn(flag: false);
		if (potionController != null)
		{
			MasterPool.ReturnToPoolTransform(potionController.gameObject, potionController.prefabType);
			potionController = null;
		}
		currentCatController.GoToUpgradeCamera();
	}

	public void UpdateHoldedPotion()
	{
		SceneSingleton<HoveredPotionController>.Instance.SetIsCatPotionOn(flag: true);
		if (characterInventory.IsInventoryEmpty())
		{
			catPanelUI.SetPotionUI(isInventoryEmpty: true, isHoldingPotion: false);
			return;
		}
		SceneSingleton<HoveredPotionController>.Instance.UpdateZoomedPotion();
		catPanelUI.SetPotionUI(isInventoryEmpty: false, characterInventory.IsHoldingPotion());
	}

	public void DisableCatUI()
	{
		SceneSingleton<HoveredPotionController>.Instance.SetIsCatPotionOn(flag: false);
		isCatOn = false;
		if (potionController != null)
		{
			MasterPool.ReturnToPoolTransform(potionController.gameObject, potionController.prefabType);
			potionController = null;
		}
		if (currentCatController != null)
		{
			currentCatController.StopPetting();
		}
		currentCatController = null;
	}

	public void UpdateUpgradeHighlightAllCats(bool isActive)
	{
		foreach (CatController catControllers in catControllersList)
		{
			catControllers.SetUpgradeHighlight(isActive);
		}
	}

	private void Update()
	{
		if (currentHighlightPuzzleTimer > 0f)
		{
			currentHighlightPuzzleTimer -= Time.deltaTime;
			if (currentHighlightPuzzleTimer < 0f)
			{
				currentHighlightPuzzleTimer = 0f;
				SceneSingleton<CatPanelUI>.Instance.DisableHighlightPuzzleTimer();
			}
			else
			{
				SceneSingleton<CatPanelUI>.Instance.UpdateHighlightPuzzleTimer(currentHighlightPuzzleTimer / currentHighlightPuzzleTimer, (int)currentHighlightPuzzleTimer);
			}
		}
		if (currentRevealSolutionTimer > 0f)
		{
			currentRevealSolutionTimer -= Time.deltaTime;
			if (currentRevealSolutionTimer < 0f)
			{
				currentRevealSolutionTimer = 0f;
				SceneSingleton<CatPanelUI>.Instance.DisableRevealSolutionTimer();
			}
			else
			{
				SceneSingleton<CatPanelUI>.Instance.UpdateRevealSolutionTimer(currentRevealSolutionTimer / revealSolutionCooldown, (int)currentRevealSolutionTimer);
			}
		}
	}

	public bool CanHighlightPuzzle()
	{
		return currentHighlightPuzzleTimer == 0f;
	}

	public bool CanRevealSolution()
	{
		return currentRevealSolutionTimer == 0f;
	}

	public void HighlightPuzzleButtonClicked()
	{
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Cat_Hint);
		SceneSingleton<HighlightManager>.Instance.HighlightPuzzle(characterInventory.GetSelectedPotion().sortingPuzzleType);
		currentHighlightPuzzleTimer = highlightPuzzleItemsCooldown;
	}

	public void RevealSolutionButtonClicked()
	{
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Cat_Hint);
		Singleton<PotionsDataHolder>.Instance.AddSolvedPuzzle(characterInventory.GetSelectedPotion().sortingPuzzleType);
		currentRevealSolutionTimer = revealSolutionCooldown;
		SceneSingleton<QuickPuzzleSolutionPanelUI>.Instance.UpdateQuickPuzzleSolution();
	}

	public void StartPetting()
	{
		if (currentCatController != null)
		{
			currentCatController.StartPetting();
		}
	}

	public void StopPetting()
	{
		if (currentCatController != null)
		{
			currentCatController.StopPetting();
		}
	}

	public void UpdatePettingCount()
	{
		catsPettedCount++;
		int num = 0;
		foreach (CatController catControllers in catControllersList)
		{
			if (catControllers.isCatPetted)
			{
				num++;
			}
		}
		if (num >= 1)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Cat_1);
		}
		if (num >= catControllersList.Count)
		{
			Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Cat_All);
		}
	}

	public void LoadCatsPettedCount(List<bool> catsPetted)
	{
		foreach (CatController catControllers in catControllersList)
		{
			catControllers.isCatPetted = false;
		}
		for (int i = 0; i < catsPetted.Count; i++)
		{
			if (i >= catControllersList.Count)
			{
				catControllersList[i].isCatPetted = catsPetted[i];
			}
		}
	}

	public List<bool> GetCatsPettedCount()
	{
		List<bool> list = new List<bool>();
		foreach (CatController catControllers in catControllersList)
		{
			list.Add(catControllers.isCatPetted);
		}
		return list;
	}
}
