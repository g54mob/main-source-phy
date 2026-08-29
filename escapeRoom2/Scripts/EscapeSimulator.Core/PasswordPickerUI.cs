using UnityEngine;

public class PasswordPickerUI : PineUIComponent
{
	public RectTransform root;

	public PassEntryUI PassEntry;

	public RectTransform SpacerEntry;

	private bool isInitialized;

	protected override void Awake()
	{
		init();
	}

	public void init()
	{
		if (!isInitialized)
		{
			isInitialized = true;
		}
	}
}
