using UnityEngine;
using UnityEngine.UI;

public class CoverUI : PineUIComponent
{
	public Canvas root;

	public MenuNewsUI MenuNews;

	public CanvasGroup Coop;

	public Button Coop_CreateGame;

	public Text Coop_CreateGame_Text;

	public Button Coop_Join;

	public Text Coop_Join_Text;

	public Button Coop_PlayRandomMatch;

	public Text Coop_PlayRandomMatch_Text;

	public Text Coop_PlayRandomMatch_Text_1;

	public Button Coop_Back;

	public Text Coop_Back_Text;

	public CanvasGroup Main;

	public Button Main_Solo;

	public Text Main_Solo_Text;

	public Button Main_Coop;

	public Text Main_Coop_Text;

	public Button Main_Workshop;

	public Text Main_Workshop_Text;

	public Button Main_Options;

	public Text Main_Options_Text;

	public Button Main_Credits;

	public Text Main_Credits_Text;

	public Button Main_Discord;

	public Text Main_Discord_Text;

	public Image Main_Discord_Image;

	public Button Main_Quit;

	public Text Main_Quit_Text;

	public Button CustomizeParent_Customize;

	public Image CustomizeParent_Customize_CustomizeControllerHint;

	public Text CustomizeParent_Customize_Text;

	public Image Logo;

	public Button Logo_VersionBtn;

	public Text Logo_VersionBtn_Version;

	public Text PineSocials;

	public Image PineSocials_Image;

	public Button PineSocials_Image_Twitter;

	public Image PineSocials_Image_Twitter_Image;

	public Button PineSocials_Image_Facebook;

	public Image PineSocials_Image_Facebook_Image;

	public Button PineSocials_Image_Instagram;

	public Image PineSocials_Image_Instagram_Image;

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
			PineUI.addButtonListeners(Coop_CreateGame);
			PineUI.addButtonListeners(Coop_Join);
			PineUI.addButtonListeners(Coop_PlayRandomMatch);
			PineUI.addButtonListeners(Coop_Back);
			PineUI.addButtonListeners(Main_Solo);
			PineUI.addButtonListeners(Main_Coop);
			PineUI.addButtonListeners(Main_Workshop);
			PineUI.addButtonListeners(Main_Options);
			PineUI.addButtonListeners(Main_Credits);
			PineUI.addButtonListeners(Main_Discord);
			PineUI.addButtonListeners(Main_Quit);
			PineUI.addButtonListeners(CustomizeParent_Customize);
			PineUI.addButtonListeners(Logo_VersionBtn);
			PineUI.addButtonListeners(PineSocials_Image_Twitter);
			PineUI.addButtonListeners(PineSocials_Image_Facebook);
			PineUI.addButtonListeners(PineSocials_Image_Instagram);
		}
	}
}
