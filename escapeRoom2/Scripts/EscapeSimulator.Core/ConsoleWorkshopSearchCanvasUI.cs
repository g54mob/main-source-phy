using UnityEngine;
using UnityEngine.UI;

public class ConsoleWorkshopSearchCanvasUI : PineUIComponent
{
	public Canvas root;

	public Button PrevHint;

	public Image smallDotLeft;

	public Image dot;

	public Image smallDotRight;

	public Button NextHint;

	public Button Back;

	public Image BackControllerHint;

	public Image DownloadPlayControllerHint;

	public Button Play;

	public Button Install;

	public InputField LevelSelectorHeader_Search_SearchInputField;

	public Dropdown LevelSelectorHeader_SortDropdown;

	public Dropdown LevelSelectorHeader_TimeDropdown;

	public Button LevelSelectorHeader_BrowseButton;

	public Image LevelsScrollView;

	public ToggleGroup LevelsScrollView_Content;

	public Toggle LevelsScrollView_Content_Level;

	public Image LevelsScrollView_ScrollbarVertical;

	public MenuErrorPopupUI MenuErrorPopupUI;

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
			PineUI.addButtonListeners(PrevHint);
			PineUI.addButtonListeners(NextHint);
			PineUI.addButtonListeners(Back);
			PineUI.addButtonListeners(Play);
			PineUI.addButtonListeners(Install);
			PineUI.addDropdownListeners(LevelSelectorHeader_SortDropdown);
			PineUI.addDropdownListeners(LevelSelectorHeader_TimeDropdown);
			PineUI.addButtonListeners(LevelSelectorHeader_BrowseButton);
			PineUI.addToggleListeners(LevelsScrollView_Content_Level);
		}
	}
}
