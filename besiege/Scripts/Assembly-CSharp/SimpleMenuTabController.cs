using UnityEngine;

public class SimpleMenuTabController : ClickBehaviour
{
	public static int currentTab;

	public int myID = -1;

	public GameObject simpleMenuOptionPosition;

	public bool lastActive;

	public JustAnotherScalingScript justAnotherScalingScript;

	private void OnEnable()
	{
		currentTab = 0;
		lastActive = currentTab != myID;
	}

	private void Update()
	{
		if ((currentTab == myID) ^ lastActive)
		{
			lastActive = currentTab == myID;
			if (lastActive)
			{
				simpleMenuOptionPosition.SetActive(true);
				HighlightThisTab();
			}
			else
			{
				simpleMenuOptionPosition.SetActive(false);
				UnHighlightThisTab();
			}
		}
	}

	private void HighlightThisTab()
	{
		justAnotherScalingScript.SetGoal(0.6f);
	}

	private void UnHighlightThisTab()
	{
		justAnotherScalingScript.SetGoal(0f);
	}

	private void OnMouseExit()
	{
		if (currentTab == myID)
		{
			justAnotherScalingScript.SetGoal(0.6f);
		}
		else
		{
			justAnotherScalingScript.SetGoal(0f);
		}
	}

	public override void OnCursorOver()
	{
		justAnotherScalingScript.SetGoal(1f);
		base.OnCursorOver();
	}

	public override void OnClicked()
	{
		currentTab = myID;
		justAnotherScalingScript.SetCurrent(0.1f);
	}
}
