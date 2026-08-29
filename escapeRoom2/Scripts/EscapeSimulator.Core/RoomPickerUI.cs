using UnityEngine;
using UnityEngine.UI;

public class RoomPickerUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

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
			PineUI.addButtonListeners(Background);
		}
	}
}
