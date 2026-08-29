using UnityEngine;
using UnityEngine.UI;

public class EditorSoundPickerUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Image Popup;

	public Image Header;

	public ToggleGroup Header_Tabs;

	public Toggle Header_Tabs_Presets;

	public Image Header_Tabs_Presets_Checkmark;

	public Text Header_Tabs_Presets_Text;

	public Toggle Header_Tabs_Custom;

	public Button Header_Tabs_Custom_Checkmark;

	public Text Header_Tabs_Custom_Text;

	public Image Header_Tabs_botLine;

	public Button Header_Options_Apply;

	public Image Header_Options_Apply_Icon;

	public Image ScrollRect;

	public Image Viewport;

	public RectTransform Content;

	public PickSoundItemUI AddNewSound;

	public PickSoundItemUI PickSoundItem;

	public Image ES2ScrollbarVertical;

	public Image ES2ScrollbarVertical_BGImage;

	public Image ES2ScrollbarVertical_SlidingArea_Handle;

	public Image ES2ScrollbarVertical_SlidingArea_Handle_BGHandle;

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
			PineUI.addToggleListeners(Header_Tabs_Presets);
			PineUI.addToggleListeners(Header_Tabs_Custom);
			PineUI.addButtonListeners(Header_Tabs_Custom_Checkmark);
			PineUI.addButtonListeners(Header_Options_Apply);
		}
	}
}
