using UnityEngine;
using UnityEngine.UI;

public class EditorUI : PineUIComponent
{
	public Canvas root;

	public Button Play;

	public Image Play_Icon;

	public Button Undo;

	public Image Undo_Icon;

	public Button Redo;

	public Image Redo_Icon;

	public Button PrefsToggle;

	public Image PrefsToggle_Icon;

	public RectTransform PrefsUI;

	public Button PrefsUI_Background;

	public Image PrefsUI_Panel;

	public Image Buttons;

	public Button Settings;

	public Image Settings_Icon;

	public Text Settings_Name;

	public Image Settings_Name_Selected;

	public Button Props;

	public Image Props_Icon;

	public Text Props_Name;

	public Image Props_Name_Selected;

	public Button Logic;

	public Image Logic_Icon;

	public Text Logic_Name;

	public Image Logic_Name_Selected;

	public Button Building;

	public Image Building_Icon;

	public Text Building_Name;

	public Image Building_Name_Selected;

	public Button Assets;

	public Image Assets_Icon;

	public Text Assets_Name;

	public Image Assets_Name_Selected;

	public Image BotBorder;

	public Image Picker;

	public RectTransform PropsHeader;

	public Button PropsHeader_Back;

	public Image PropsHeader_Back_Image;

	public Image PropsHeader_Filter;

	public Image PropsHeader_Filter_Icon;

	public InputField PropsHeader_Filter_Input;

	public Text PropsHeader_Filter_Input_Text;

	public Text PropsHeader_Filter_Input_Placeholder;

	public Button PropsHeader_Filter_SearchClearButton;

	public Image PropsHeader_Filter_SearchClearButton_X;

	public Image PropsHeader_Sizing;

	public Toggle PropsHeader_Sizing_Big;

	public Image PropsHeader_Sizing_Big_Dot;

	public Toggle PropsHeader_Sizing_Med;

	public Image PropsHeader_Sizing_Med_Dot;

	public Toggle PropsHeader_Sizing_Small;

	public Image PropsHeader_Sizing_Small_Dot;

	public HeaderWithBackUI PropsTagHeader;

	public ScrollRect PropsRect;

	public RectTransform PropsRect_Content;

	public Text PropsRect_Header;

	public TagButtonGroupUI PropsRect_TagButtonGroup;

	public RectTransform PropsRect_Spacer;

	public PropButtonGroupUI PropsRect_PropButtonGroup;

	public HeaderWithBackUI PropsRect_HeaderWithBack;

	public HierarchyItemUI PropsRect_HierarchyItem;

	public Image PropsRect_ES2ScrollbarVertical;

	public Image PropsRect_ES2ScrollbarVertical_BGImage;

	public Image PropsRect_ES2ScrollbarVertical_SlidingArea_Handle;

	public Image PropsRect_ES2ScrollbarVertical_SlidingArea_Handle_BGHandle;

	public RectTransform SettingsContent;

	public Button SettingsContent_Save;

	public Text SettingsContent_Save_Text;

	public Button SettingsContent_ShowInitialMenu;

	public Text SettingsContent_ShowInitialMenu_Text;

	public Button SettingsContent_Publish;

	public Text SettingsContent_Publish_Text;

	public Button SettingsContent_Walkthrough;

	public Text SettingsContent_Walkthrough_Text;

	public Button SettingsContent_OpenUGC;

	public Text SettingsContent_OpenUGC_Text;

	public Button SettingsContent_OpenRoomFile;

	public Text SettingsContent_OpenRoomFile_Text;

	public Button SettingsContent_ShowLevelCode;

	public Text SettingsContent_ShowLevelCode_Text;

	public Button SettingsContent_Import;

	public Text SettingsContent_Import_Text;

	public Button SettingsContent_Options;

	public Text SettingsContent_Options_Text;

	public Button SettingsContent_Discard;

	public Text SettingsContent_Discard_Text;

	public Button SettingsContent_ExitSave;

	public Text SettingsContent_ExitSave_Text;

	public RectTransform Info;

	public Text GameVersion;

	public Text RoomFolder;

	public Text PropCount;

	public EditorAssetBrowserUI EditorAssetBrowserUITABS;

	public Image HistoryProps;

	public PropButtonUI HistoryProps_PropButton;

	public Image Border;

	public Image ButtonsHide;

	public Image PickerHide;

	public Image HistoryPropsHide;

	public Image HiddenPropsPopup;

	public Text HiddenPropsPopup_Text;

	public Button HiddenPropsPopup_Unhide;

	public CanvasGroup SaveNotification;

	public Image SaveNotification_Icon;

	public Text SaveNotification_Text;

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
			PineUI.addButtonListeners(Play);
			PineUI.addButtonListeners(Undo);
			PineUI.addButtonListeners(Redo);
			PineUI.addButtonListeners(PrefsToggle);
			PineUI.addButtonListeners(Settings);
			PineUI.addButtonListeners(Props);
			PineUI.addButtonListeners(Logic);
			PineUI.addButtonListeners(Building);
			PineUI.addButtonListeners(Assets);
			PineUI.addButtonListeners(PropsHeader_Back);
			PineUI.addButtonListeners(PropsHeader_Filter_SearchClearButton);
			PineUI.addToggleListeners(PropsHeader_Sizing_Big);
			PineUI.addToggleListeners(PropsHeader_Sizing_Med);
			PineUI.addToggleListeners(PropsHeader_Sizing_Small);
			PineUI.addButtonListeners(SettingsContent_Save);
			PineUI.addButtonListeners(SettingsContent_ShowInitialMenu);
			PineUI.addButtonListeners(SettingsContent_Publish);
			PineUI.addButtonListeners(SettingsContent_Walkthrough);
			PineUI.addButtonListeners(SettingsContent_OpenUGC);
			PineUI.addButtonListeners(SettingsContent_OpenRoomFile);
			PineUI.addButtonListeners(SettingsContent_ShowLevelCode);
			PineUI.addButtonListeners(SettingsContent_Import);
			PineUI.addButtonListeners(SettingsContent_Options);
			PineUI.addButtonListeners(SettingsContent_Discard);
			PineUI.addButtonListeners(SettingsContent_ExitSave);
			PineUI.addButtonListeners(HiddenPropsPopup_Unhide);
		}
	}
}
