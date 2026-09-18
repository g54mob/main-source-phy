using System.Collections.Generic;
using UnityEngine;

public class SolvedPuzzlesPanelUI : SceneSingleton<SolvedPuzzlesPanelUI>
{
	[SerializeField]
	private List<SolvedPuzzleOrderPanelUI> solvedPuzzleOrderPanelUIsList = new List<SolvedPuzzleOrderPanelUI>();

	protected override void Awake()
	{
		base.Awake();
		for (int i = 0; i < Singleton<PotionsDataHolder>.Instance.puzzlesUIList.Count; i++)
		{
			solvedPuzzleOrderPanelUIsList[i].SetPuzzle(Singleton<PotionsDataHolder>.Instance.puzzlesUIList[i]);
		}
	}

	public void LoadPuzzles()
	{
		foreach (SortingPuzzleType solvedPuzzles in Singleton<PotionsDataHolder>.Instance.solvedPuzzlesList)
		{
			SolvePuzzle(solvedPuzzles);
		}
		UpdateColorBlind();
	}

	public void UpdateColorBlind()
	{
		foreach (SolvedPuzzleOrderPanelUI solvedPuzzleOrderPanelUIs in solvedPuzzleOrderPanelUIsList)
		{
			solvedPuzzleOrderPanelUIs.UpdatePuzzleSprites();
		}
	}

	public void SolvePuzzle(SortingPuzzleType sortingPuzzleType)
	{
		foreach (SolvedPuzzleOrderPanelUI solvedPuzzleOrderPanelUIs in solvedPuzzleOrderPanelUIsList)
		{
			if (solvedPuzzleOrderPanelUIs.sortingPuzzleType == sortingPuzzleType)
			{
				solvedPuzzleOrderPanelUIs.SolvePuzzle();
			}
		}
	}
}
