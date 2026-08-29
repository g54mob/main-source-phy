using UnityEngine;

public class PineWorkshopUI : PineUIComponent
{
	public Canvas root;

	public LevelPickerUI LevelPicker;

	public object data;

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
