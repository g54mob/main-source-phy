using UnityEngine;
using UnityEngine.UI;

public class MenuUI : PineUIComponent
{
	public RectTransform root;

	public ES2CoverUI ES2Cover;

	public HostMiniPanelUI HostMiniPanel;

	public CanvasScaler Rooms;

	public LevelPickerUI Rooms_LevelPicker;

	public Image Rooms_AdvancedSearch;

	public Button Rooms_AdvancedSearch_FiltersBtn;

	public Text Rooms_AdvancedSearch_FiltersBtn_Text;

	public TagPickerUI TagPicker;

	public CanvasScaler Darkest;

	public LevelPickerUI Darkest_LevelPicker;

	public ClientCodeUI ClientCode;

	public HostOptionsUI HostOptions;

	public Credits Credits;

	public CustomizationUI Customization;

	public LoadingUI Loading;

	public OptionDialog OptionDialog;

	public RoomInfoPopup RoomInfoPopup;

	public InitialSetupUI InitialSetup;

	public InitialWelcomeMessageUI InitialWelcomeMessage;

	public CanvasGroup Fade;

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
			PineUI.addButtonListeners(Rooms_AdvancedSearch_FiltersBtn);
		}
	}
}
