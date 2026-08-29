using UnityEngine;
using UnityEngine.UI;

public class LevelPickerUI : PineUIComponent
{
	public RectTransform root;

	public ES2FrameController Frame;

	public Transform PackScroll;

	public Transform PackScroll_PackContent;

	public PackUI PackScroll_PackContent_Pack;

	public Image PackScroll_PackContent_Separator;

	public Image PackScroll_PackContent_Separator_Line;

	public Image PackScroll_ES2ScrollbarVertical;

	public Image PackScroll_ES2ScrollbarVertical_BGImage;

	public Image PackScroll_ES2ScrollbarVertical_SlidingArea_Handle;

	public Image PackScroll_ES2ScrollbarVertical_SlidingArea_Handle_BGHandle;

	public Image PackScroll_NotFocusedBG;

	public Transform Seperator;

	public VerticalLayoutGroup LevelSelection;

	public Transform LevelSelection_Search;

	public Transform LevelSelection_Search_SearchBg;

	public InputField LevelSelection_Search_SearchBg_SearchInputField;

	public Image LevelSelection_Search_SearchBg_Image;

	public Button LevelSelection_Search_BrowseButton;

	public Image LevelSelection_Search_BrowseButton_BrowsePlus;

	public ControllerButtonImage LevelSelection_Search_BrowseButton_BrowseControllerHint;

	public UndraggableRect LevelSelection_Scroll;

	public GridLayoutGroup LevelSelection_Scroll_LevelsContent_Panel;

	public LevelUI LevelSelection_Scroll_LevelsContent_Panel_Level;

	public LevelUI LevelSelection_Scroll_LevelsContent_Panel_DLCNotInstalled;

	public Text LevelSelection_Scroll_NoCustomLevelsInstalledLbl;

	public KeyboardUI LevelSelection_Scroll_Keyboard;

	public Text LevelSelection_Scroll_PortalDisclamer;

	public Text LevelSelection_Scroll_AmongusDisclamer;

	public Transform BotRight;

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
			PineUI.addButtonListeners(LevelSelection_Search_BrowseButton);
		}
	}
}
