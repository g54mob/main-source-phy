using UnityEngine;

public class OutOfBoundsWarning : WarningPopupBase
{
	public GameObject outOfBoundsText;

	public GameObject inFloorText;

	public static OutOfBoundsWarning current;

	protected override void Awake()
	{
		base.Awake();
		current = this;
	}

	public void OutOfBounds()
	{
		if ((bool)outOfBoundsText)
		{
			outOfBoundsText.SetActive(true);
			inFloorText.SetActive(false);
		}
		else
		{
			Debug.LogWarning("missing two references in OutOfBoundsWarning");
		}
		ShowWarning();
	}

	public void InFloor()
	{
		if ((bool)outOfBoundsText)
		{
			outOfBoundsText.SetActive(false);
			inFloorText.SetActive(true);
		}
		else
		{
			Debug.LogWarning("missing two references in OutOfBoundsWarning");
		}
		ShowWarning();
	}
}
