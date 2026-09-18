using System.Collections.Generic;
using UnityEngine;

public class PuzzleTextPanelUI : PanelUI<PuzzleTextPanelUI>
{
	[SerializeField]
	private List<PuzzleTextData> puzzleTextDatasList = new List<PuzzleTextData>();

	public void SetPanel(PuzzleTextTypes puzzleTextType)
	{
		foreach (PuzzleTextData puzzleTextDatas in puzzleTextDatasList)
		{
			puzzleTextDatas.textObject.SetActive(value: false);
		}
		foreach (PuzzleTextData puzzleTextDatas2 in puzzleTextDatasList)
		{
			if (puzzleTextDatas2.puzzleTextType == puzzleTextType)
			{
				puzzleTextDatas2.textObject.SetActive(value: true);
				return;
			}
		}
		Debug.LogError(puzzleTextType.ToString() + " is not avilable in PuzzleTextPanelUI");
	}

	public void BackButton()
	{
		UIBackKeyManager.BackUI();
	}
}
