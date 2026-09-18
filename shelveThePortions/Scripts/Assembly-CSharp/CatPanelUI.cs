using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatPanelUI : SceneSingleton<CatPanelUI>
{
	[SerializeField]
	private CanvasGroup canvasGroup;

	[Space]
	[SerializeField]
	private GameObject potionsPanel;

	[SerializeField]
	private GameObject upgradePanel;

	[SerializeField]
	private GameObject kittenPanel;

	[Space]
	[SerializeField]
	private GameObject KittenPanelSelectedButton;

	[SerializeField]
	private GameObject noPotionsPanelSelectedButton;

	[SerializeField]
	private GameObject potionsPanelSelectedButton;

	[SerializeField]
	private GameObject upgradePanelSelectedButton;

	[SerializeField]
	private GameObject pointsAvilable_1;

	[SerializeField]
	private GameObject pointsAvilable_2;

	[Space]
	[SerializeField]
	private CanvasGroup NoSolutionPuzzlePanelCG;

	[SerializeField]
	private GameObject NoSolutionPuzzlePanelObject;

	[SerializeField]
	private GameObject puzzleSolutionPanelObject;

	[SerializeField]
	private List<Image> puzzleImagesList = new List<Image>();

	[Space]
	[SerializeField]
	private GameObject noPotionSelectedText;

	[SerializeField]
	private GameObject cantHightlightPuzzlePanel;

	[SerializeField]
	private CatButtonUI highlightPuzzleCatButtonUI;

	[SerializeField]
	private CatButtonUI revealSolutionCatButtonUI;

	[SerializeField]
	private CanvasGroup isHoldingPotionCanvasGroup;

	[Space]
	[SerializeField]
	private TextMeshProUGUI currentSkillPointsText;

	[SerializeField]
	private UpgradePanelUI carryUpgradePanelUI;

	[SerializeField]
	private UpgradePanelUI speedUpgradePanelUI;

	[SerializeField]
	private UpgradePanelUI sprintUpgradePanelUI;

	[SerializeField]
	private UpgradePanelUI assembleUpgradePanelUI;

	[SerializeField]
	private UpgradePanelUI highlightUpgradePanelUI;

	[SerializeField]
	private UpgradePanelUI shelveHighlightUpgradePanelUI;

	[Space(10f)]
	[SerializeField]
	private List<LocalizationTextActivator> catNames = new List<LocalizationTextActivator>();

	[Space(5f)]
	[SerializeField]
	private LocalizationTextActivator helpLocalizationTextActivator;

	[SerializeField]
	private List<string> helpLocalizationID = new List<string>();

	private int helpIndex = -1;

	[Space]
	[SerializeField]
	private LocalizationTextActivator noPotionLocalizationTextActivator;

	[SerializeField]
	private List<string> noPotionLocalizationID = new List<string>();

	private int noPotionIndex = -1;

	[Space]
	[SerializeField]
	private LocalizationTextActivator kittenLocalizationTextActivator;

	[SerializeField]
	private List<string> kittenLocalizationID = new List<string>();

	[Space]
	[SerializeField]
	private List<PettingPanelUI> pettingPanelUIsList = new List<PettingPanelUI>();

	private int kittenIndex = -1;

	private UpgradesManager upgradesManager;

	private CatsManager catsManager;

	private bool isPotionUIOn;

	private void Start()
	{
		upgradesManager = SceneSingleton<UpgradesManager>.Instance;
		catsManager = SceneSingleton<CatsManager>.Instance;
		helpLocalizationID = Randomizer.Randomize(helpLocalizationID);
		noPotionLocalizationID = Randomizer.Randomize(noPotionLocalizationID);
		EventManager.OnControlsChange += OnControlsChange;
		RefreshCatsText();
	}

	private void OnDisable()
	{
		EventManager.OnControlsChange -= OnControlsChange;
	}

	public void SetCatName(string ID)
	{
		foreach (LocalizationTextActivator catName in catNames)
		{
			catName.SetIDString(ID);
		}
	}

	public void UpdateUpgradeUI()
	{
		currentSkillPointsText.text = upgradesManager.aviliableSkillPoints.ToString();
		if (upgradesManager.IsCarryUpgradeMaxed())
		{
			carryUpgradePanelUI.SetPanelComplete("(" + upgradesManager.inventoryUpgradesLists[upgradesManager.currentInventoryUpgradeIndex] + ")");
		}
		else
		{
			carryUpgradePanelUI.SetPanel("(" + upgradesManager.inventoryUpgradesLists[upgradesManager.currentInventoryUpgradeIndex] + "->" + upgradesManager.inventoryUpgradesLists[upgradesManager.currentInventoryUpgradeIndex + 1] + ")", 1, upgradesManager.CanClickCarryUpgrade());
		}
		if (upgradesManager.IsSpeedUpgradeMaxed())
		{
			speedUpgradePanelUI.SetPanelComplete("(" + upgradesManager.speedUpgradesLists[upgradesManager.currentSpeedUpgradeIndex] + ")");
		}
		else
		{
			speedUpgradePanelUI.SetPanel("(" + upgradesManager.speedUpgradesLists[upgradesManager.currentSpeedUpgradeIndex] + "->" + upgradesManager.speedUpgradesLists[upgradesManager.currentSpeedUpgradeIndex + 1] + ")", 1, upgradesManager.CanClickSpeedUpgrade());
		}
		if (upgradesManager.IsSprintUpgradeMaxed())
		{
			sprintUpgradePanelUI.SetPanelComplete();
		}
		else
		{
			sprintUpgradePanelUI.SetPanel(2, upgradesManager.CanClickSprintUpgrade());
		}
		int completedShelvesCount = SceneSingleton<CabinetsManager>.Instance.GetCompletedShelvesCount();
		if (completedShelvesCount < upgradesManager.shelfsNeededToUnlockHighlight)
		{
			highlightUpgradePanelUI.SetShelvePanel(completedShelvesCount + "/" + upgradesManager.shelfsNeededToUnlockHighlight);
		}
		else if (upgradesManager.IsHighlightUpgradeMaxed())
		{
			highlightUpgradePanelUI.SetPanelComplete("(" + upgradesManager.highlightsCooldownLists[upgradesManager.currentHighlightIndex] + "s)");
		}
		else
		{
			highlightUpgradePanelUI.SetPanel("(" + upgradesManager.highlightsCooldownLists[upgradesManager.currentHighlightIndex] + "s->" + upgradesManager.highlightsCooldownLists[upgradesManager.currentHighlightIndex + 1] + "s)", 1, upgradesManager.CanClickHightlightUpgrade());
		}
		if (completedShelvesCount < upgradesManager.shelfsNeededToUnlockShelvesHighlight)
		{
			shelveHighlightUpgradePanelUI.SetShelvePanel(completedShelvesCount + "/" + upgradesManager.shelfsNeededToUnlockShelvesHighlight);
		}
		else if (upgradesManager.IsShelveHighlightUpgradeMaxed())
		{
			shelveHighlightUpgradePanelUI.SetPanelComplete("(" + upgradesManager.shelvesHighlightsCooldownLists[upgradesManager.currentShelvesHighlightIndex] + "s)");
		}
		else
		{
			shelveHighlightUpgradePanelUI.SetPanel("(" + upgradesManager.shelvesHighlightsCooldownLists[upgradesManager.currentShelvesHighlightIndex] + "s->" + upgradesManager.shelvesHighlightsCooldownLists[upgradesManager.currentShelvesHighlightIndex + 1] + "s)", 1, upgradesManager.CanClickShelveHightlightUpgrade());
		}
		if (completedShelvesCount < upgradesManager.shelfsNeededToUnlockAssemble)
		{
			assembleUpgradePanelUI.SetShelvePanel(completedShelvesCount + "/" + upgradesManager.shelfsNeededToUnlockAssemble);
		}
		else if (upgradesManager.IsAssembleUpgradeMaxed())
		{
			assembleUpgradePanelUI.SetPanelComplete("(" + upgradesManager.assemblesCountLists[upgradesManager.currentAssembleIndex] + ")\n(" + upgradesManager.assemblesCooldownLists[upgradesManager.currentAssembleIndex] + "s)");
		}
		else
		{
			assembleUpgradePanelUI.SetPanel("(" + upgradesManager.assemblesCountLists[upgradesManager.currentAssembleIndex] + "->" + upgradesManager.assemblesCountLists[upgradesManager.currentAssembleIndex + 1] + ")\n(" + upgradesManager.assemblesCooldownLists[upgradesManager.currentAssembleIndex] + "s->" + upgradesManager.assemblesCooldownLists[upgradesManager.currentAssembleIndex + 1] + "s)", 1, upgradesManager.CanClickAssembleUpgrade());
		}
		foreach (PettingPanelUI pettingPanelUIs in pettingPanelUIsList)
		{
			pettingPanelUIs.UpdatePanel();
		}
	}

	public void UnlockCarryUpgrade()
	{
		upgradesManager.UnlockCarryUpgrade();
		UpdateUpgradeUI();
	}

	public void UnlockSpeedUpgrade()
	{
		upgradesManager.UnlockSpeedUpgrade();
		UpdateUpgradeUI();
	}

	public void UnlockHighlightUpgrade()
	{
		upgradesManager.UnlockHighlightUpgrade();
		UpdateUpgradeUI();
	}

	public void UnlockShelveHighlightUpgrade()
	{
		upgradesManager.UnlockShelveHighlightUpgrade();
		UpdateUpgradeUI();
	}

	public void UnlockAssembleUpgrade()
	{
		upgradesManager.UnlockAssembleUpgrade();
		UpdateUpgradeUI();
	}

	public void UnlockSprintUpgrade()
	{
		upgradesManager.UnlockSprintUpgrade();
		UpdateUpgradeUI();
	}

	public void DisableHighlightPuzzleTimer()
	{
		highlightPuzzleCatButtonUI.UpdatePanel(upgradesManager.shelfsNeededToUnlockHighlightPuzzle, canHighlight: true);
	}

	public void DisableRevealSolutionTimer()
	{
		revealSolutionCatButtonUI.UpdatePanel(upgradesManager.shelfsNeededToUnlockRevealSolution, canHighlight: true);
	}

	public void UpdateHighlightPuzzleTimer(float percent, int secondsLeft)
	{
		if (potionsPanel.activeSelf)
		{
			highlightPuzzleCatButtonUI.UpdateTimer(percent, secondsLeft);
		}
	}

	public void UpdateRevealSolutionTimer(float percent, int secondsLeft)
	{
		if (potionsPanel.activeSelf)
		{
			revealSolutionCatButtonUI.UpdateTimer(percent, secondsLeft);
		}
	}

	public void SetKittenPanelUI()
	{
		potionsPanel.SetActive(value: false);
		upgradePanel.SetActive(value: false);
		kittenPanel.SetActive(value: true);
		isPotionUIOn = false;
		EventSystem.current.SetSelectedGameObject(KittenPanelSelectedButton);
		foreach (PettingPanelUI pettingPanelUIs in pettingPanelUIsList)
		{
			pettingPanelUIs.UpdatePanel();
		}
	}

	public void SetUpgradeUI()
	{
		canvasGroup.alpha = 1f;
		potionsPanel.SetActive(value: false);
		kittenPanel.SetActive(value: false);
		upgradePanel.SetActive(value: true);
		isPotionUIOn = false;
		EventSystem.current.SetSelectedGameObject(upgradePanelSelectedButton);
		foreach (PettingPanelUI pettingPanelUIs in pettingPanelUIsList)
		{
			pettingPanelUIs.UpdatePanel();
		}
	}

	private void OnControlsChange()
	{
		if (upgradePanel.activeSelf)
		{
			EventSystem.current.SetSelectedGameObject(upgradePanelSelectedButton);
		}
		if (potionsPanel.activeSelf)
		{
			EventSystem.current.SetSelectedGameObject(potionsPanelSelectedButton);
		}
		if (kittenPanel.activeSelf)
		{
			EventSystem.current.SetSelectedGameObject(KittenPanelSelectedButton);
		}
	}

	public void UpdatePotionUI(bool isInventoryEmpty, bool isHoldingPotion)
	{
		if (isPotionUIOn)
		{
			SetPotionUI(isInventoryEmpty, isHoldingPotion);
		}
	}

	public void SetPotionUI(bool isInventoryEmpty, bool isHoldingPotion)
	{
		isHoldingPotionCanvasGroup.interactable = isHoldingPotion;
		canvasGroup.alpha = 1f;
		upgradePanel.SetActive(value: false);
		kittenPanel.SetActive(value: false);
		potionsPanel.SetActive(value: true);
		isPotionUIOn = true;
		noPotionLocalizationTextActivator.gameObject.SetActive(isInventoryEmpty);
		helpLocalizationTextActivator.gameObject.SetActive(!isInventoryEmpty);
		pointsAvilable_1.SetActive(SceneSingleton<UpgradesManager>.Instance.aviliableSkillPoints > 0);
		pointsAvilable_2.SetActive(SceneSingleton<UpgradesManager>.Instance.aviliableSkillPoints > 0);
		noPotionSelectedText.SetActive(!isHoldingPotion && !isInventoryEmpty);
		if (isInventoryEmpty)
		{
			NoSolutionPuzzlePanelObject.SetActive(value: true);
			puzzleSolutionPanelObject.SetActive(value: false);
			NoSolutionPuzzlePanelCG.interactable = false;
		}
		else if (isHoldingPotion)
		{
			NoSolutionPuzzlePanelCG.interactable = true;
			SortingPuzzleType sortingPuzzleType = SceneSingleton<CharacterInventory>.Instance.GetSelectedPotion().sortingPuzzleType;
			if (SceneSingleton<HighlightManager>.Instance.CanHighlightPuzzle(sortingPuzzleType))
			{
				cantHightlightPuzzlePanel.SetActive(value: false);
				highlightPuzzleCatButtonUI.gameObject.SetActive(value: true);
			}
			else
			{
				cantHightlightPuzzlePanel.SetActive(value: true);
				highlightPuzzleCatButtonUI.gameObject.SetActive(value: false);
			}
			if (Singleton<PotionsDataHolder>.Instance.solvedPuzzlesList.Contains(sortingPuzzleType))
			{
				NoSolutionPuzzlePanelObject.SetActive(value: false);
				puzzleSolutionPanelObject.SetActive(value: true);
				foreach (Image puzzleImages in puzzleImagesList)
				{
					puzzleImages.gameObject.SetActive(value: false);
				}
				for (int i = 0; i < Singleton<PotionsDataHolder>.Instance.GetPotionPuzzleSpriteCount(sortingPuzzleType); i++)
				{
					puzzleImagesList[i].gameObject.SetActive(value: true);
					puzzleImagesList[i].sprite = Singleton<PotionsDataHolder>.Instance.GetPotionPuzzleSprite(sortingPuzzleType, i);
				}
			}
			else
			{
				NoSolutionPuzzlePanelObject.SetActive(value: true);
				puzzleSolutionPanelObject.SetActive(value: false);
			}
		}
		UpdateButtons();
		EventSystem.current.SetSelectedGameObject(potionsPanelSelectedButton);
		foreach (PettingPanelUI pettingPanelUIs in pettingPanelUIsList)
		{
			pettingPanelUIs.UpdatePanel();
		}
	}

	public void UpdateButtons()
	{
		highlightPuzzleCatButtonUI.UpdatePanel(upgradesManager.shelfsNeededToUnlockHighlightPuzzle, catsManager.CanHighlightPuzzle());
		revealSolutionCatButtonUI.UpdatePanel(upgradesManager.shelfsNeededToUnlockRevealSolution, catsManager.CanRevealSolution());
	}

	public void HighlightPuzzleButtonClicked()
	{
		catsManager.HighlightPuzzleButtonClicked();
		BackButton();
	}

	public void RevealSolutionButtonClicked()
	{
		catsManager.RevealSolutionButtonClicked();
		BackButton();
	}

	public void StartPetting()
	{
		catsManager.StartPetting();
	}

	public void StopPetting()
	{
		catsManager.StopPetting();
	}

	public void SetPettingHighlight(bool flag)
	{
		if (flag)
		{
			Singleton<CurserManager>.Instance.SetCurser(CurserTypes.Pet);
		}
		else
		{
			Singleton<CurserManager>.Instance.SetBaseCurserToNormal();
		}
		if (catsManager.currentCatController != null)
		{
			catsManager.currentCatController.SetPettingHighlight(flag);
		}
	}

	public void GoToPotionCamera()
	{
		canvasGroup.alpha = 0f;
		catsManager.GoToPotionCamera();
	}

	public void GotoUpgradesCamera()
	{
		UpdateUpgradeUI();
		canvasGroup.alpha = 0f;
		catsManager.GotoUpgradesCamera();
	}

	private void RefreshCatsText()
	{
		helpIndex++;
		if (helpIndex >= helpLocalizationID.Count)
		{
			helpIndex = 0;
			helpLocalizationID = Randomizer.Randomize(helpLocalizationID);
		}
		helpLocalizationTextActivator.SetIDString(helpLocalizationID[helpIndex]);
		noPotionIndex++;
		if (noPotionIndex >= noPotionLocalizationID.Count)
		{
			noPotionIndex = 0;
			noPotionLocalizationID = Randomizer.Randomize(noPotionLocalizationID);
		}
		noPotionLocalizationTextActivator.SetIDString(noPotionLocalizationID[noPotionIndex]);
		kittenIndex++;
		if (kittenIndex >= kittenLocalizationID.Count)
		{
			kittenIndex = 0;
			kittenLocalizationID = Randomizer.Randomize(kittenLocalizationID);
		}
		kittenLocalizationTextActivator.SetIDString(kittenLocalizationID[kittenIndex]);
	}

	public void DisableCatUI()
	{
		isPotionUIOn = false;
		SetPettingHighlight(flag: false);
		RefreshCatsText();
	}

	public void BackButton()
	{
		UIBackKeyManager.BackUI();
	}
}
