using UnityEngine;
using UnityEngine.UI;

public class ES2HostOptionsUI : PineUIComponent
{
	public Canvas root;

	public ES2FrameController Frame;

	public Image Panel;

	public Image Panel_CodeTitle;

	public Text Panel_CodeTitle_Title;

	public Text Panel_CodeTitle_Text;

	public Button Panel_Button;

	public Text Panel_Button_Text;

	public Image Panel_Button_Text_Image;

	public Image Panel_InviteTitle;

	public Text Panel_InviteTitle_Title;

	public Text Panel_InviteTitle_Text;

	public UndraggableRect Panel_ScrollView;

	public Image Panel_ScrollView_Viewport;

	public PlayerInviteItemUI Panel_ScrollView_Viewport_Content_PlayerInviteItem;

	public Image Panel_ScrollView_ES2ScrollbarVertical;

	public Image Panel_ScrollView_ES2ScrollbarVertical_BGImage;

	public Image Panel_ScrollView_ES2ScrollbarVertical_SlidingArea_Handle;

	public Image Panel_ScrollView_ES2ScrollbarVertical_SlidingArea_Handle_BGHandle;

	public Image Gradient;

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
			PineUI.addButtonListeners(Panel_Button);
		}
	}
}
