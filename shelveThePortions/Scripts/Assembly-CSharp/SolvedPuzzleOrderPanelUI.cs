using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolvedPuzzleOrderPanelUI : MonoBehaviour
{
	public SortingPuzzleType sortingPuzzleType;

	[SerializeField]
	private List<Image> puzzleImagesList = new List<Image>();

	[Space]
	[SerializeField]
	private GameObject unSolvedPanel;

	[SerializeField]
	private GameObject solvedPanel;

	public void SetPuzzle(SortingPuzzleType sortingPuzzleType)
	{
		this.sortingPuzzleType = sortingPuzzleType;
		UpdatePuzzleSprites();
	}

	public void UpdatePuzzleSprites()
	{
		foreach (Image puzzleImages in puzzleImagesList)
		{
			puzzleImages.gameObject.SetActive(value: false);
		}
		for (int i = 0; i < Singleton<PotionsDataHolder>.Instance.GetPotionPuzzleSpriteCount(sortingPuzzleType); i++)
		{
			puzzleImagesList[i].gameObject.SetActive(value: true);
			if (SaveSystem.GetIsColorBlindSetting())
			{
				if (Singleton<PotionsDataHolder>.Instance.IsSortingPuzzleColorBlind(sortingPuzzleType))
				{
					puzzleImagesList[i].sprite = Singleton<PotionsDataHolder>.Instance.GetPotionPuzzleColorBlindSprite(sortingPuzzleType, i);
				}
				else
				{
					puzzleImagesList[i].sprite = Singleton<PotionsDataHolder>.Instance.GetPotionPuzzleSprite(sortingPuzzleType, i);
				}
			}
			else
			{
				puzzleImagesList[i].sprite = Singleton<PotionsDataHolder>.Instance.GetPotionPuzzleSprite(sortingPuzzleType, i);
			}
		}
	}

	public void SolvePuzzle()
	{
		unSolvedPanel.SetActive(value: false);
		solvedPanel.SetActive(value: true);
	}

	public void UnSolvePuzzle()
	{
		unSolvedPanel.SetActive(value: true);
		solvedPanel.SetActive(value: false);
	}
}
