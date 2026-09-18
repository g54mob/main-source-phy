using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class TutorialManager : SceneSingleton<TutorialManager>
{
	private TutorialManagerUI tutorialManagerUI;

	[ReadOnly]
	public bool isTutorialCompleted;

	[Space]
	[SerializeField]
	[ReadOnly]
	private int tutorialGroupIndex;

	[SerializeField]
	private List<TutorialGroup> tutorialGroupsList = new List<TutorialGroup>();

	[SerializeField]
	private float timeAfterLastCheckTohideUI = 2f;

	[SerializeField]
	private Animator doorAnimator;

	[SerializeField]
	private List<GameObject> staticDoorGameObjectsList = new List<GameObject>();

	[SerializeField]
	private List<GameObject> notStaticDoorGameObjectsList = new List<GameObject>();

	[Space]
	[SerializeField]
	private bool isDebugOn;

	private void Start()
	{
		tutorialManagerUI = SceneSingleton<TutorialManagerUI>.Instance;
	}

	public void StartTutorial()
	{
		foreach (GameObject staticDoorGameObjects in staticDoorGameObjectsList)
		{
			staticDoorGameObjects.SetActive(value: true);
		}
		foreach (GameObject notStaticDoorGameObjects in notStaticDoorGameObjectsList)
		{
			notStaticDoorGameObjects.SetActive(value: false);
		}
		isTutorialCompleted = false;
		tutorialGroupIndex = -1;
		LoadNextGroup(runTimer: false);
	}

	[Button]
	public void CompleteTutorial(bool showCutscene)
	{
		isTutorialCompleted = true;
		doorAnimator.SetTrigger("OpenDoor");
		foreach (GameObject staticDoorGameObjects in staticDoorGameObjectsList)
		{
			staticDoorGameObjects.SetActive(value: false);
		}
		foreach (GameObject notStaticDoorGameObjects in notStaticDoorGameObjectsList)
		{
			notStaticDoorGameObjects.SetActive(value: true);
		}
		if (SceneSingleton<CatsManager>.Instance.isCatOn)
		{
			SceneSingleton<CatPanelUI>.Instance.BackButton();
		}
		Singleton<GameAchievementsManager>.Instance.ActivateAchievement(AchievementTypes.Ach_Tutorial);
		if (showCutscene)
		{
			SceneSingleton<CutscenesManager>.Instance.StartTutorialEnd();
		}
		else
		{
			SaveSystem.SaveLevel();
		}
	}

	[Button]
	public void CompleteTutorialStep(TutorialStepType stepType)
	{
		if (isTutorialCompleted)
		{
			return;
		}
		if (isDebugOn)
		{
			Debug.Log("tutorial step: " + stepType);
		}
		TutorialCheck tutorialStep = GetTutorialStep(stepType);
		if (tutorialStep == null)
		{
			if (isDebugOn)
			{
				Debug.Log(stepType.ToString() + " step is not in the current checks");
			}
		}
		else if (!tutorialStep.isCompleted)
		{
			tutorialStep.isCompleted = true;
			tutorialManagerUI.CheckStep(stepType);
			if (tutorialGroupsList[tutorialGroupIndex].tutorialChecks.Contains(tutorialStep))
			{
				Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_TutorialStepCompleted);
			}
			if (AreAllStepsInGroupCompleted(tutorialGroupsList[tutorialGroupIndex]))
			{
				LoadNextGroup(runTimer: true);
			}
		}
	}

	private void LoadNextGroup(bool runTimer)
	{
		tutorialGroupIndex++;
		if (tutorialGroupIndex >= tutorialGroupsList.Count)
		{
			tutorialGroupIndex = -1;
			CompleteTutorial(showCutscene: true);
		}
		StopAllCoroutines();
		StartCoroutine(RefreshUI(runTimer));
	}

	private IEnumerator RefreshUI(bool runTimer)
	{
		if (runTimer)
		{
			yield return new WaitForSeconds(timeAfterLastCheckTohideUI);
		}
		if (tutorialGroupIndex == -1)
		{
			SceneSingleton<TutorialManagerUI>.Instance.HidePanel();
		}
		else if (AreAllStepsInGroupCompleted(tutorialGroupsList[tutorialGroupIndex]))
		{
			LoadNextGroup(runTimer: true);
		}
		else
		{
			tutorialManagerUI.LoadTutorialSteps(tutorialGroupsList[tutorialGroupIndex]);
		}
	}

	private TutorialCheck GetTutorialStep(TutorialStepType stepType)
	{
		foreach (TutorialGroup tutorialGroups in tutorialGroupsList)
		{
			foreach (TutorialCheck tutorialCheck in tutorialGroups.tutorialChecks)
			{
				if (tutorialCheck.tutorialStepID == stepType)
				{
					return tutorialCheck;
				}
			}
		}
		return null;
	}

	private bool AreAllStepsInGroupCompleted(TutorialGroup group)
	{
		foreach (TutorialCheck tutorialCheck in group.tutorialChecks)
		{
			if (!tutorialCheck.isCompleted)
			{
				return false;
			}
		}
		return true;
	}
}
