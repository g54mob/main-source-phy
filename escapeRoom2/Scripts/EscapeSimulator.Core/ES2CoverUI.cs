using UnityEngine;
using UnityEngine.UI;

public class ES2CoverUI : PineUIComponent
{
	public Canvas root;

	public Image Logo;

	public Button Logo_Version;

	public Text Logo_Version_VersionLbl;

	public Image Logo_Version_SelectionIndicator;

	public Image MainButtons;

	public Button MainButtons_ButtonPlay;

	public Text MainButtons_ButtonPlay_Label;

	public Image MainButtons_ButtonPlay_SelectionIndicator;

	public Button MainButtons_ButtonBuild;

	public Text MainButtons_ButtonBuild_Label;

	public Image MainButtons_ButtonBuild_SelectionIndicator;

	public Button MainButtons_ButtonOptions;

	public Text MainButtons_ButtonOptions_Label;

	public Image MainButtons_ButtonOptions_SelectionIndicator;

	public Button MainButtons_ButtonCredits;

	public Text MainButtons_ButtonCredits_Label;

	public Image MainButtons_ButtonCredits_SelectionIndicator;

	public Button MainButtons_ButtonDiscord;

	public Text MainButtons_ButtonDiscord_Label;

	public Image MainButtons_ButtonDiscord_SelectionIndicator;

	public Image MainButtons_ButtonDiscord_Image;

	public Image MainButtons_Spacer;

	public Button MainButtons_ButtonQuit;

	public Text MainButtons_ButtonQuit_Label;

	public Image MainButtons_ButtonQuit_SelectionIndicator;

	public MenuNewsUI MenuNews;

	public Image PlayButtons;

	public Button PlayButtons_ButtonSolo;

	public Text PlayButtons_ButtonSolo_Label;

	public Image PlayButtons_ButtonSolo_SelectionIndicator;

	public Button PlayButtons_ButtonHost;

	public Text PlayButtons_ButtonHost_Label;

	public Image PlayButtons_ButtonHost_SelectionIndicator;

	public Button PlayButtons_ButtonJoin;

	public Text PlayButtons_ButtonJoin_Label;

	public Image PlayButtons_ButtonJoin_SelectionIndicator;

	public Image PlayButtons_Spacer;

	public Button PlayButtons_ButtonBack;

	public Text PlayButtons_ButtonBack_Label;

	public Image PlayButtons_ButtonBack_SelectionIndicator;

	public Image OverlayFadeIn;

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
			PineUI.addButtonListeners(Logo_Version);
			PineUI.addButtonListeners(MainButtons_ButtonPlay);
			PineUI.addButtonListeners(MainButtons_ButtonBuild);
			PineUI.addButtonListeners(MainButtons_ButtonOptions);
			PineUI.addButtonListeners(MainButtons_ButtonCredits);
			PineUI.addButtonListeners(MainButtons_ButtonDiscord);
			PineUI.addButtonListeners(MainButtons_ButtonQuit);
			PineUI.addButtonListeners(PlayButtons_ButtonSolo);
			PineUI.addButtonListeners(PlayButtons_ButtonHost);
			PineUI.addButtonListeners(PlayButtons_ButtonJoin);
			PineUI.addButtonListeners(PlayButtons_ButtonBack);
		}
	}
}
