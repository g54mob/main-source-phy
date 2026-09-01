using UnityEngine.Serialization;

public class MinimiseTutorial : MinimiseWindow
{
	[FormerlySerializedAs("minimizer")]
	public bool showMinimiserButton = true;

	public bool showButtonOnGuide = true;

	protected virtual void Start()
	{
		if (showMinimiserButton)
		{
			GuideBook.SetTutorialCollapser(this);
		}
	}

	public override void Minimise()
	{
		if (openWindow.gameObject.activeSelf)
		{
			base.Minimise();
			if (showButtonOnGuide)
			{
				minimisedWindow.gameObject.SetActive(false);
				GuideBook.ShowTutorialCollapse(showMinimiserButton);
			}
		}
	}

	public override void Maximise()
	{
		if (!openWindow.gameObject.activeSelf)
		{
			base.Maximise();
			if (showButtonOnGuide)
			{
				minimisedWindow.gameObject.SetActive(false);
				GuideBook.ShowTutorialCollapse(showMinimiserButton && !openWindow.gameObject.activeSelf);
			}
		}
	}
}
