using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UI/Tutorial/Tutorial Base Container")]
public abstract class TutorialBaseContainer : MonoBehaviour
{
	public static bool Reloading = false;

	public Action onTutorialComplete;

	public bool resetIfNotCompleted;

	public bool includeInSandbox = true;

	[FormerlySerializedAs("zoneId")]
	public int sceneName = -1;

	public string machineToLoad;

	protected int lastLoadedScene = -2;

	protected TutorialStep[] steps = new TutorialStep[0];

	public static int[] indeces = new int[100];

	[HideInInspector]
	public BlockTabController blockTabController;

	[SerializeField]
	protected BlockType selectBlock;

	protected int currentStep;

	protected static bool completed = false;

	public abstract bool Setup(int currentScene);

	public abstract bool TutorialCompleted();

	protected abstract void PrepareSteps(bool forceActivate = false);

	public void Next(TutorialStep current)
	{
		if (sceneName >= indeces.Length)
		{
			return;
		}
		current.Close();
		currentStep = current.index + 1;
		bool flag = lastLoadedScene == -1 && !StatMaster.isMP && includeInSandbox;
		if (currentStep < steps.Length)
		{
			steps[currentStep].Open();
			if (!resetIfNotCompleted && !flag)
			{
				TutorialFileManager.SetTutorialState(base.gameObject.name, currentStep);
				if (sceneName > 0)
				{
					indeces[sceneName] = currentStep;
				}
			}
			return;
		}
		if (onTutorialComplete != null)
		{
			onTutorialComplete();
		}
		if (!flag)
		{
			TutorialFileManager.SetTutorialState(base.gameObject.name, currentStep);
			if (sceneName > 0)
			{
				indeces[sceneName] = currentStep;
			}
		}
	}

	public void Open()
	{
		if (currentStep < steps.Length)
		{
			steps[currentStep].Open();
		}
	}

	public void Close()
	{
		if (currentStep < steps.Length)
		{
			steps[currentStep].Close();
		}
	}

	protected IEnumerator LoadMachine()
	{
		while (Machine.Active() == null)
		{
			yield return null;
		}
		if (Machine.Active().BlockCount <= 1)
		{
			TextAsset t = Resources.Load<TextAsset>(Path.Combine("_TutorialMachineSaves", machineToLoad));
			MachineInfo info = MachineInfo.Decode(t.bytes);
			info.Author = "Spiderling";
			info.Type = MachineInfo.MachineType.Built;
			Machine.Active().LoadMachineInfo(info);
		}
		PrepareSteps();
	}

	protected IEnumerator ForceLoadMachine()
	{
		while (Machine.Active() == null)
		{
			yield return null;
		}
		if (Machine.Active().BlockCount <= 1)
		{
			TextAsset t = Resources.Load<TextAsset>(Path.Combine("_TutorialMachineSaves", machineToLoad));
			MachineInfo info = MachineInfo.Decode(t.bytes);
			info.Author = "Spiderling";
			info.Type = MachineInfo.MachineType.Built;
			Machine.Active().LoadMachineInfo(info);
		}
	}

	protected void SelectBlock()
	{
		if (selectBlock != BlockType.StartingBlock && !(blockTabController == null))
		{
			blockTabController.SelectBlock((int)selectBlock);
		}
	}

	public void Finish()
	{
		Close();
		if (sceneName > 0)
		{
			indeces[sceneName] = steps.Length;
		}
		else
		{
			completed = true;
		}
	}
}
