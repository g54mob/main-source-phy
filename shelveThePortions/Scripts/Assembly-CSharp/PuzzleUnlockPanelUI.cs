using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUnlockPanelUI : SceneSingleton<PuzzleUnlockPanelUI>
{
	[SerializeField]
	private GameObject panel;

	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private List<Image> puzzleImagesList = new List<Image>();

	private void Start()
	{
		panel.SetActive(value: false);
	}

	public void SetPuzzle(SortingPuzzleType sortingPuzzleType)
	{
		canvasGroup.alpha = 0f;
		panel.SetActive(value: false);
		panel.SetActive(value: true);
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
		StopAllCoroutines();
		StartCoroutine(DisableHighlightsCoroutine());
	}

	private IEnumerator DisableHighlightsCoroutine()
	{
		yield return new WaitForSeconds(4f);
		panel.SetActive(value: false);
	}
}
