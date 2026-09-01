using System;
using UnityEngine;

[AddComponentMenu("UI/Tutorial/Tutorial Dialogue")]
public class TutorialDialogue : MonoBehaviour
{
	public MinimiseTutorial minimizer;

	public string category = string.Empty;

	public int guidePage = 1;

	private bool closedByGuideBook;

	private bool wasPreviouslyOpened;

	private void Awake()
	{
		if (!OptionsMaster.BesiegeConfig.ShowTutorialWindows)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		if (!(GuideBook.Instance == null))
		{
			if (category != string.Empty)
			{
				GuideBook.SetPage(category, guidePage);
			}
			GuideBook instance = GuideBook.Instance;
			instance.OnBookOpened = (Action)Delegate.Combine(instance.OnBookOpened, new Action(OnGuideBookOpened));
			GuideBook instance2 = GuideBook.Instance;
			instance2.OnBookClosed = (Action)Delegate.Combine(instance2.OnBookClosed, new Action(OnGuideBookClosed));
		}
	}

	private void OnGuideBookClosed()
	{
		Open();
	}

	private void OnGuideBookOpened()
	{
		wasPreviouslyOpened = minimizer.openWindow.gameObject.activeSelf;
		closedByGuideBook = wasPreviouslyOpened;
		Close();
	}

	private void Close()
	{
		if ((bool)minimizer && minimizer.openWindow.gameObject.activeSelf)
		{
			minimizer.Minimise();
			closedByGuideBook = true;
		}
	}

	private void Open()
	{
		if (closedByGuideBook)
		{
			minimizer.Maximise();
			closedByGuideBook = false;
		}
	}
}
