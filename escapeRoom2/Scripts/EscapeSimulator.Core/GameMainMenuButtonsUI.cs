using UnityEngine.UI;

public class GameMainMenuButtonsUI : PineUIComponent
{
	public Image root;

	public Button ButtonWalkthrough;

	public Text ButtonWalkthrough_Label;

	public Image ButtonWalkthrough_SelectionIndicator;

	public Button ButtonResume;

	public Text ButtonResume_Label;

	public Image ButtonResume_SelectionIndicator;

	public Button ButtonHelp;

	public Text ButtonHelp_Label;

	public Text ButtonHelp_Container_DummySpacerLabelBecauseTweenState;

	public Image ButtonHelp_Container_ExternalLink;

	public Image ButtonHelp_SelectionIndicator;

	public Button ButtonInvite;

	public Text ButtonInvite_Label;

	public Text ButtonInvite_Label_code;

	public Image ButtonInvite_SelectionIndicator;

	public Button ButtonOptions;

	public Text ButtonOptions_Label;

	public Image ButtonOptions_SelectionIndicator;

	public Button ButtonRestart;

	public Text ButtonRestart_Label;

	public Image ButtonRestart_SelectionIndicator;

	public Button ButtonResync;

	public Text ButtonResync_Label;

	public Image ButtonResync_SelectionIndicator;

	public Image Spacer;

	public Button ButtonMenu;

	public Text ButtonMenu_Label;

	public Image ButtonMenu_SelectionIndicator;

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
			PineUI.addButtonListeners(ButtonWalkthrough);
			PineUI.addButtonListeners(ButtonResume);
			PineUI.addButtonListeners(ButtonHelp);
			PineUI.addButtonListeners(ButtonInvite);
			PineUI.addButtonListeners(ButtonOptions);
			PineUI.addButtonListeners(ButtonRestart);
			PineUI.addButtonListeners(ButtonResync);
			PineUI.addButtonListeners(ButtonMenu);
		}
	}
}
