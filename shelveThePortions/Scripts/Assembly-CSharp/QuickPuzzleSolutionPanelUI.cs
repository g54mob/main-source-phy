using UnityEngine;

public class QuickPuzzleSolutionPanelUI : SceneSingleton<QuickPuzzleSolutionPanelUI>
{
	[Header("Behaviour")]
	[SerializeField]
	private bool toggleMode;

	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private SolvedPuzzleOrderPanelUI solvedPuzzleOrderPanelUI;

	[SerializeField]
	private LocalizationTextActivator localizationTextActivator;

	private bool isPanelActive;

	private void Start()
	{
		UpdateIsToggle();
	}

	public void UpdateIsToggle()
	{
		toggleMode = SaveSystem.GetHoverSolutionToggleSetting();
		if (!toggleMode)
		{
			HidePanel();
		}
	}

	public void KeyDown()
	{
		if (toggleMode)
		{
			TogglePanel();
		}
		else
		{
			ShowPanel();
		}
	}

	public void KeyUp()
	{
		if (!toggleMode)
		{
			HidePanel();
		}
	}

	private void TogglePanel()
	{
		if (isPanelActive)
		{
			HidePanel();
		}
		else
		{
			ShowPanel();
		}
	}

	private void ShowPanel()
	{
		if (!isPanelActive)
		{
			isPanelActive = true;
			AlphaSystem.Alphalizer(canvasGroup, 1f, 0.5f, IgnoreTimeScale: true);
			UpdateQuickPuzzleSolution();
		}
	}

	public void UpdateQuickPuzzleSolution()
	{
		if (!isPanelActive)
		{
			return;
		}
		if (SceneSingleton<CharacterInventory>.Instance.IsHoldingPotion())
		{
			SortingPuzzleType sortingPuzzleType = SceneSingleton<CharacterInventory>.Instance.GetSelectedPotion().sortingPuzzleType;
			if (Singleton<PotionsDataHolder>.Instance.solvedPuzzlesList.Contains(sortingPuzzleType))
			{
				solvedPuzzleOrderPanelUI.SetPuzzle(sortingPuzzleType);
				solvedPuzzleOrderPanelUI.SolvePuzzle();
			}
			else
			{
				localizationTextActivator.SetIDString("Menu_NoPuzzleSolved");
				solvedPuzzleOrderPanelUI.UnSolvePuzzle();
			}
		}
		else
		{
			if (SceneSingleton<CharacterInventory>.Instance.itemControllersList.Count == 0)
			{
				localizationTextActivator.SetIDString("Menu_EmptyHand");
			}
			else
			{
				localizationTextActivator.SetIDString("Cat_NoPotionSelected");
			}
			solvedPuzzleOrderPanelUI.UnSolvePuzzle();
		}
	}

	private void HidePanel()
	{
		if (isPanelActive)
		{
			isPanelActive = false;
			AlphaSystem.Alphalizer(canvasGroup, 0f, 0.25f, IgnoreTimeScale: true);
		}
	}
}
