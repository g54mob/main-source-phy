using UnityEngine;
using UnityEngine.UI;

public class HostOptionsUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public CanvasGroup NetworkingProtocolDropdown;

	public Transform NetworkingProtocolDropdown_Placement;

	public Text NetworkingProtocolDropdown_Description;

	public CanvasGroup GameplayDropdown;

	public Transform GameplayDropdown_Placement;

	public Text GameplayDropdown_Description;

	public CanvasGroup DifficultyDropdown;

	public Transform DifficultyDropdown_Placement;

	public Text DifficultyDropdown_Description;

	public Button HostButton;

	public Text HostButton_Text;

	public Button UpdateButton;

	public Text UpdateButton_Text;

	public Image InviteFriendsControllerHint_Icon;

	public Text InviteFriendsControllerHint_Text;

	public Transform ControllerHints;

	public Image ControllerHints_confirm_HintImage;

	public Text ControllerHints_confirm_Text;

	public Image ControllerHints_cancel_HintImage;

	public Text ControllerHints_cancel_Text;

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
			PineUI.addButtonListeners(HostButton);
			PineUI.addButtonListeners(UpdateButton);
		}
	}
}
