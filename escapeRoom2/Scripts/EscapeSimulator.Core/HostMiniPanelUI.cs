using UnityEngine;
using UnityEngine.UI;

public class HostMiniPanelUI : PineUIComponent
{
	public Canvas root;

	public Image Panel;

	public Image Panel_BG;

	public Image Panel_BG_Icon;

	public Image Panel_BG_Spinner_Spinner;

	public Text Panel_BG_Code;

	public Image InviteFriends;

	public Image InviteFriends_Image;

	public Text InviteFriends_Code;

	public Image LobbyCodeCopied;

	public Text LobbyCodeCopied_CopiedText;

	public PlayerUI PlayerUI;

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
		}
	}
}
