using System;
using UnityEngine;

[AddComponentMenu("UI/Tutorial/Tutorial System")]
public class TutorialSystem : MonoBehaviour
{
	public float levelStartWait;

	public float openAnimationWait;

	public float animationDuration = 1f;

	public static float LevelStartWait = 0.5f;

	public static float OpenAnimWait = 0.5f;

	public static float AnimDuration = 0.5f;

	public TutorialBaseContainer[] tutorials = new TutorialBaseContainer[0];

	private Action lastAction;

	private static TutorialSystem instance;

	private static int lastLevel = -1;

	private static int lastStep;

	private bool initialised;

	public static TutorialSystem Instance
	{
		get
		{
			return instance;
		}
	}

	private void Awake()
	{
		instance = this;
		LevelStartWait = levelStartWait;
		OpenAnimWait = openAnimationWait;
		AnimDuration = animationDuration;
		ReferenceMaster.onLevelLoadComplete = (Action<int>)Delegate.Combine(ReferenceMaster.onLevelLoadComplete, new Action<int>(LevelLoaded));
		ReferenceMaster.onSceneLoaded = (Action)Delegate.Combine(ReferenceMaster.onSceneLoaded, new Action(SceneLoaded));
		ReferenceMaster.onLevelLoad = (Action)Delegate.Combine(ReferenceMaster.onLevelLoad, new Action(Close));
		ReferenceMaster.OnConnect += Connected;
		ReferenceMaster.onTutorialsToggled = (Action<bool>)Delegate.Combine(ReferenceMaster.onTutorialsToggled, new Action<bool>(Toggle));
	}

	private void Toggle(bool a)
	{
		if (a)
		{
			if (initialised)
			{
				tutorials[lastStep].Open();
			}
			else
			{
				LevelLoaded(lastLevel);
			}
		}
		else
		{
			Close();
		}
	}

	private void SceneLoaded()
	{
		if ((StatMaster.isMP || (bool)UnityEngine.Object.FindObjectOfType<NetworkScene>()) && StatMaster.isClient)
		{
		}
	}

	private void LevelLoaded(int index)
	{
		if (StatMaster.isMP)
		{
			return;
		}
		lastLevel = index;
		if (!OptionsMaster.BesiegeConfig.Tutorials)
		{
			return;
		}
		initialised = true;
		if (index == -1)
		{
			for (int i = 0; i < tutorials.Length; i++)
			{
				if (!tutorials[i].TutorialCompleted())
				{
					StartTutorials(i);
					break;
				}
			}
		}
		else
		{
			for (int j = 0; j < tutorials.Length; j++)
			{
				tutorials[j].Setup(index);
			}
		}
	}

	public static void StartCustomTutorial(string containerType)
	{
		if (!OptionsMaster.BesiegeConfig.Tutorials)
		{
			return;
		}
		if (instance == null)
		{
			Debug.LogError("missing tutorial system");
			return;
		}
		if (instance.tutorials == null)
		{
			Debug.LogError("missing tutorials");
			return;
		}
		for (int i = 0; i < instance.tutorials.Length; i++)
		{
			if (instance.tutorials[i].GetType().Name == containerType)
			{
				instance.tutorials[i].Setup(-10);
			}
		}
	}

	private void Connected()
	{
		TutorialBaseContainer.Reloading = true;
		LevelLoaded(-1);
		TutorialBaseContainer.Reloading = false;
	}

	private void StartTutorials(int i)
	{
		for (int j = 0; j < i; j++)
		{
			tutorials[j].Close();
		}
		int i2 = i;
		for (int k = i + 1; k < tutorials.Length; k++)
		{
			if (tutorials[i].sceneName == tutorials[k].sceneName)
			{
				tutorials[k - 1].Setup(-1);
				i2 = k;
			}
		}
		StartTutorial(i2);
	}

	private void StartTutorial(int i)
	{
		if (i - 1 > 0 && lastAction != null)
		{
			TutorialBaseContainer obj = tutorials[i - 1];
			obj.onTutorialComplete = (Action)Delegate.Remove(obj.onTutorialComplete, lastAction);
		}
		if (i < tutorials.Length)
		{
			lastStep = i;
			tutorials[i].Setup(-1);
			lastAction = delegate
			{
				StartTutorials(i + 1);
			};
			TutorialBaseContainer obj2 = tutorials[i];
			obj2.onTutorialComplete = (Action)Delegate.Combine(obj2.onTutorialComplete, lastAction);
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLevelLoadComplete = (Action<int>)Delegate.Remove(ReferenceMaster.onLevelLoadComplete, new Action<int>(LevelLoaded));
		ReferenceMaster.onSceneLoaded = (Action)Delegate.Remove(ReferenceMaster.onSceneLoaded, new Action(SceneLoaded));
		ReferenceMaster.onLevelLoad = (Action)Delegate.Remove(ReferenceMaster.onLevelLoad, new Action(Close));
		ReferenceMaster.OnConnect -= Connected;
		ReferenceMaster.onTutorialsToggled = (Action<bool>)Delegate.Remove(ReferenceMaster.onTutorialsToggled, new Action<bool>(Toggle));
	}

	public static void Close()
	{
		for (int i = 0; i < instance.tutorials.Length; i++)
		{
			instance.tutorials[i].Close();
		}
	}
}
