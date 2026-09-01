using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ShareReplayStatus : MonoBehaviour
{
	public Action m_OKCallback;

	public Action m_CancelCallback;

	public TextMeshProUGUI m_Title;

	public TextMeshProUGUI m_Message;

	public Button m_OpenReplaysFolderButton;

	public Button m_OK;

	public Button m_Cancel;

	public GameObject m_WaitingAnimation;

	[NonSerialized]
	public string m_Fullpath;

	private void Start()
	{
		m_OK.onClick.AddListener(OnOK);
		m_Cancel.onClick.AddListener(OnCancel);
		m_OpenReplaysFolderButton.onClick.AddListener(OnOpenReplaysFolder);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	public void Open(string text, Action cancelCallback)
	{
		m_Title.text = text;
		m_WaitingAnimation.SetActive(value: true);
		m_OK.gameObject.SetActive(value: false);
		m_Cancel.gameObject.SetActive(value: true);
		m_Message.gameObject.SetActive(value: false);
		m_OpenReplaysFolderButton.gameObject.SetActive(value: false);
		m_CancelCallback = cancelCallback;
		GameUI.m_Instance.m_GamepadLegend.Save();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
		base.gameObject.SetActive(value: true);
	}

	private void Update()
	{
		ProcessInput();
		if (!m_OK.gameObject.activeInHierarchy && !m_Cancel.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_GamepadLegend.HideButtons();
		}
	}

	public void Close()
	{
		InterfaceAudio.Play("ui_window_close");
		base.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_GamepadLegend.Restore();
	}

	public void Complete(bool success, string title, string message, string fullpath, Action okCallback)
	{
		m_Title.text = title;
		m_Message.text = message;
		m_Fullpath = fullpath;
		m_OpenReplaysFolderButton.gameObject.SetActive(success && !Game.IsRunningOnSteamDeck() && !SteamUtils.IsSteamInBigPictureMode());
		m_WaitingAnimation.SetActive(value: false);
		m_OK.gameObject.SetActive(value: true);
		m_Cancel.gameObject.SetActive(value: false);
		m_Message.gameObject.SetActive(success);
		m_OKCallback = okCallback;
		if (m_OpenReplaysFolderButton.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.NORTH, Localize.Get("UI_OK"), GamepadButtonType.WEST, Localize.Get("UI_OPEN_REPLAYS_FOLDER"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_OK"));
		}
	}

	private void OnOK()
	{
		InterfaceAudio.Play("ui_menu_accept");
		Close();
		m_OKCallback?.Invoke();
	}

	private void OnCancel()
	{
		try
		{
			FFmpegCommands.Abort();
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception calling FFmpegCommands.Abort(): " + ex.Message);
		}
		Close();
		m_CancelCallback?.Invoke();
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (m_OK.gameObject.activeInHierarchy)
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH) || GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH))
			{
				OnOK();
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				OnOK();
			}
		}
		else if (m_Cancel.gameObject.activeInHierarchy)
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				OnCancel();
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				OnCancel();
			}
		}
		if (m_OpenReplaysFolderButton.gameObject.activeInHierarchy && GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			OnOpenReplaysFolder();
		}
	}

	private void OnOpenReplaysFolder()
	{
		InterfaceAudio.Play("ui_menubar_gen_on");
		Utils.OpenLocalPath(m_Fullpath);
	}
}
