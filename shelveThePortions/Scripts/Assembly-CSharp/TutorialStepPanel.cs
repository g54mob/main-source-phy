using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class TutorialStepPanel : MonoBehaviour
{
	[SerializeField]
	private GameObject tutorialCheckMark;

	[SerializeField]
	private LocalizationTextActivator tutorialText;

	[SerializeField]
	private List<ControlUIImage> inputImages;

	[Space]
	[SerializeField]
	[ReadOnly]
	private TutorialCheck currentStep;

	public void SetTutorialStep(TutorialCheck tutorialStep)
	{
		tutorialCheckMark.SetActive(tutorialStep.isCompleted);
		currentStep = tutorialStep;
		tutorialText.SetIDString(tutorialStep.tutorialStepID.ToString());
		for (int i = 0; i < inputImages.Count; i++)
		{
			inputImages[i].gameObject.SetActive(i < tutorialStep.input.Count);
			if (i < tutorialStep.input.Count)
			{
				inputImages[i].UpdateBindingDisplayUI(tutorialStep.input[i]);
			}
		}
	}

	public void SetIsChecked()
	{
		tutorialCheckMark.SetActive(value: true);
	}

	public bool IsStepType(TutorialStepType stepType)
	{
		return currentStep.tutorialStepID == stepType;
	}
}
