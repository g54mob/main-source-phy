using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour
{
	public int activeScreenIndex;

	public DynamicText numberLabel;

	private List<Transform> childObjs = new List<Transform>();

	private static int compareTransformByName(Transform a, Transform b)
	{
		return a.name.CompareTo(b.name);
	}

	private void Start()
	{
		childObjs.Clear();
		foreach (Transform item in base.transform)
		{
			childObjs.Add(item);
		}
		childObjs.Sort(compareTransformByName);
		setScreen(activeScreenIndex);
		Screen.orientation = ScreenOrientation.AutoRotation;
	}

	private void setScreen(int newIndex)
	{
		for (int i = 0; i < childObjs.Count; i++)
		{
			Transform transform = childObjs[i];
			transform.gameObject.SetActive(false);
		}
		activeScreenIndex = newIndex;
		childObjs[activeScreenIndex].gameObject.SetActive(true);
		if (numberLabel != null)
		{
			numberLabel.SetText((activeScreenIndex + 1).ToString());
		}
	}

	private void nextScreen()
	{
		int num = Mathf.Clamp(activeScreenIndex + 1, 0, childObjs.Count - 1);
		if (num != activeScreenIndex)
		{
			setScreen(num);
		}
	}

	private void previousScreen()
	{
		int num = Mathf.Clamp(activeScreenIndex - 1, 0, childObjs.Count - 1);
		if (num != activeScreenIndex)
		{
			setScreen(num);
		}
	}
}
