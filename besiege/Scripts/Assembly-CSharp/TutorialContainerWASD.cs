using System;
using UnityEngine;

[AddComponentMenu("UI/Tutorial/Tutorial Container WASD")]
public class TutorialContainerWASD : TutorialBaseContainer
{
	public override bool TutorialCompleted()
	{
		if (steps.Length == 0)
		{
			steps = base.gameObject.GetComponents<TutorialStep>();
		}
		int val = ((sceneName > 0) ? TutorialBaseContainer.indeces[sceneName] : 0);
		currentStep = Math.Max(Math.Max(TutorialFileManager.GetTutorialState(base.gameObject.name), 0), val);
		if (currentStep >= steps.Length)
		{
			return true;
		}
		return false;
	}

	public override bool Setup(int currentScene)
	{
		lastLoadedScene = currentScene;
		bool flag = lastLoadedScene == -1 && !StatMaster.isMP && includeInSandbox;
		if (TutorialCompleted() || flag || currentScene != -10)
		{
			base.gameObject.SetActive(false);
			return false;
		}
		base.gameObject.SetActive(true);
		PrepareSteps();
		return true;
	}

	protected override void PrepareSteps(bool forceActivate = false)
	{
		blockTabController = UnityEngine.Object.FindObjectOfType<BlockTabController>();
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
