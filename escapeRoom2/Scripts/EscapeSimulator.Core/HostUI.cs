using UnityEngine;
using UnityEngine.UI;

public class HostUI : PineUIComponent
{
	public CanvasScaler root;

	public Text LobbyInfo;

	public Text HintHost;

	public Transform HostPanel_InviteFriendsBg;

	public Button HostPanel_InviteFriendsBg_InviteFriends;

	public Text HostPanel_InviteFriendsBg_InviteFriends_Text;

	public Transform HostPanel_InviteFriendsControllerHint;

	public Image HostPanel_InviteFriendsControllerHint_Icon;

	public Text HostPanel_InviteFriendsControllerHint_Text;

	public Transform HostPanel_RoomIdBg;

	public Image HostPanel_RoomIdBg_RoomId;

	public Text HostPanel_RoomIdBg_RoomId_RoomId;

	public Button HostPanel_RoomIdBg_RoomId_CopyBtn;

	public Image HostPanel_RoomIdBg_RoomId_CopyBtn_Text;

	public Button HostPanel_RoomIdBg_RoomId_BtnShowCode;

	public Image HostPanel_RoomIdBg_RoomId_BtnShowCode_Text;

	public PlayerUI HostPanel_PlayerUI;

	public LevelPickerUI HostPanel_LevelPicker;

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
			PineUI.addButtonListeners(HostPanel_InviteFriendsBg_InviteFriends);
			PineUI.addButtonListeners(HostPanel_RoomIdBg_RoomId_CopyBtn);
			PineUI.addButtonListeners(HostPanel_RoomIdBg_RoomId_BtnShowCode);
		}
	}
}
