using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerUI : SceneSingleton<TutorialManagerUI>
{
	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private List<TutorialStepPanel> tutorialStepPanels;

	[SerializeField]
	private GameObject tutorialPanel;

	public void LoadTutorialSteps(TutorialGroup tutorialGroup)
	{
		SetActive(x: true);
		for (int i = 0; i < tutorialStepPanels.Count; i++)
		{
			tutorialStepPanels[i].gameObject.SetActive(i < tutorialGroup.tutorialChecks.Count);
			if (i < tutorialGroup.tutorialChecks.Count)
			{
				tutorialStepPanels[i].SetTutorialStep(tutorialGroup.tutorialChecks[i]);
			}
		}
	}

	public void CheckStep(TutorialStepType stepType)
	{
		foreach (TutorialStepPanel tutorialStepPanel in tutorialStepPanels)
		{
			if (tutorialStepPanel.IsStepType(stepType))
			{
				tutorialStepPanel.SetIsChecked();
			}
		}
	}

	public void HidePanel()
	{
		SetActive(x: false);
	}

	private void SetActive(bool x)
	{
		canvasGroup.interactable = x;
		canvasGroup.blocksRaycasts = x;
		canvasGroup.alpha = (x ? 1 : 0);
		tutorialPanel.SetActive(x);
	}
}
