using UnityEngine;
using UnityEngine.UI;

public class ChatView : CanvasInputView, IChatView
{
	public bool TestMode;

	public Color[] ChatTeamColors;

	[SerializeField]
	private Button inviteFriendButton;

	[SerializeField]
	private GameObject machineInfoHUD;

	[SerializeField]
	private GameObject scoreboard;

	[SerializeField]
	private TextMesh unreadMessagesText;

	[SerializeField]
	private GameObject unreadMessagesCircle;

	[SerializeField]
	private UIButtonExtended chatButton;

	[SerializeField]
	private Button closeButton;

	[SerializeField]
	private Button chatModeButton;

	private Text chatModeButtonText;

	private IChatController chatController;

	protected override bool toggleViewKey
	{
		get
		{
			return InputManager.ToggleChat();
		}
	}

	public void ChangeChatModeText(string chatModeText, bool showButton)
	{
		chatModeButtonText.text = chatModeText;
		chatModeButton.gameObject.SetActive(showButton);
	}

	protected override void Initialize()
	{
		if (!StatMaster.IsLevelEditorOnly)
		{
			hasToggleViewKey = true;
			chatModeButton.onClick.AddListener(OnChatModeButtonClicked);
			chatModeButtonText = chatModeButton.GetComponentInChildren<Text>();
			closeButton.onClick.AddListener(OnCloseButtonClicked);
			if (chatButton != null)
			{
				chatButton.Click += OnChatButtonClicked;
			}
			if (!SteamManager.Initialized)
			{
				inviteFriendButton.gameObject.SetActive(false);
			}
			else
			{
				inviteFriendButton.onClick.AddListener(OnInviteFriendButtonClicked);
			}
			controller = new ChatController();
			controller.Initialize(this);
			chatController = (IChatController)controller;
		}
	}

	private void OnInviteFriendButtonClicked()
	{
		chatController.OpenInviteFriendScreen();
	}

	private void OnChatButtonClicked()
	{
		if (!machineInfoHUD.activeSelf && !scoreboard.activeSelf)
		{
			SetVisibility(true);
		}
	}

	private void OnCloseButtonClicked()
	{
		SetVisibility(false);
	}

	private void OnChatModeButtonClicked()
	{
		chatController.ToggleChatMode();
	}

	protected override void LateUpdate()
	{
		if (!StatMaster.IsLevelEditorOnly)
		{
			base.LateUpdate();
			if (!TestMode && (machineInfoHUD.activeSelf || scoreboard.activeSelf))
			{
				SetVisibility(false);
			}
			if (viewContainer.activeSelf)
			{
			}
		}
	}

	public override void SetVisibility(bool visible)
	{
		if (!StatMaster.IsLevelEditorOnly)
		{
			base.SetVisibility(visible);
			if (visible)
			{
				ScrollToBottom();
			}
		}
	}

	public void ChangeUnreadText(string unreadText, bool showText)
	{
		if (!(unreadMessagesCircle == null) && !(unreadMessagesText == null))
		{
			unreadMessagesCircle.SetActive(showText);
			unreadMessagesText.text = unreadText;
		}
	}

	protected override void OnDestroy()
	{
		if (!StatMaster.IsLevelEditorOnly)
		{
			base.OnDestroy();
			closeButton.onClick.RemoveListener(OnCloseButtonClicked);
			chatButton.Click -= OnChatButtonClicked;
			inviteFriendButton.onClick.RemoveListener(OnInviteFriendButtonClicked);
		}
	}
}
