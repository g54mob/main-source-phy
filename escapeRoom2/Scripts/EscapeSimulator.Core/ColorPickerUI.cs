using UnityEngine;
using UnityEngine.UI;

public class ColorPickerUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public ColorPicker ColorPicker;

	public Text ColorPicker_Title;

	public Button ColorPicker_QuitButton;

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
			PineUI.addButtonListeners(ColorPicker_QuitButton);
		}
	}
}
