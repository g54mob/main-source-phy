using UnityEngine;

[AddComponentMenu("UI/Tutorial/Tutorial Container")]
public class TutorialContainer : TutorialBaseContainer
{
	public override bool TutorialCompleted()
	{
		if (LEVELLORD.levelsComplete[sceneName - 1] == 1)
		{
			return true;
		}
		if (steps.Length == 0)
		{
			steps = base.gameObject.GetComponents<TutorialStep>();
		}
		currentStep = ((sceneName > 0) ? TutorialBaseContainer.indeces[sceneName] : 0);
		if (currentStep >= steps.Length)
		{
			return true;
		}
		return false;
	}

	public override bool Setup(int currentScene)
	{
		if (lastLoadedScene == currentScene)
		{
			if (TutorialBaseContainer.Reloading && !StatMaster.isMP && !string.IsNullOrEmpty(machineToLoad))
			{
				StartCoroutine(ForceLoadMachine());
			}
			return false;
		}
		lastLoadedScene = currentScene;
		if (TutorialCompleted())
		{
			base.gameObject.SetActive(false);
			return false;
		}
		bool flag = lastLoadedScene == -1 && !StatMaster.isMP && includeInSandbox;
		if (sceneName == currentScene || flag)
		{
			base.gameObject.SetActive(true);
			if (!string.IsNullOrEmpty(machineToLoad))
			{
				StartCoroutine(LoadMachine());
			}
			else
			{
				PrepareSteps();
			}
			return true;
		}
		base.gameObject.SetActive(false);
		return false;
	}

	protected override void PrepareSteps(bool forceActivate = false)
	{
		blockTabController = Object.FindObjectOfType<BlockTabController>();
		steps = base.gameObject.GetComponents<TutorialStep>();
		bool isSandbox = lastLoadedScene == -1;
		for (int i = 0; i < steps.Length; i++)
		{
			steps[i].Prepare(this, i, isSandbox);
		}
		currentStep = ((sceneName > 0) ? TutorialBaseContainer.indeces[sceneName] : 0);
		if (currentStep < steps.Length)
		{
			steps[currentStep].Open();
		}
		SelectBlock();
	}
}
