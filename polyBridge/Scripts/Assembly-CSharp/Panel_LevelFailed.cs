using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_LevelFailed : MonoBehaviour
{
	public GameObject m_Root;

	public Banner m_Banner;

	public Panel_Replay m_ReplayPanel;

	public RectTransform m_LevelFailedRectTransform;

	public TextMeshProUGUI m_LevelNameText;

	public TextMeshProUGUI m_FailReasonText;

	public TextMeshProUGUI m_NoReplaysText;

	public ToolTipText m_ExitToolTipText;

	[Header("Buttons")]
	public Button m_ExitButton;

	public Button m_RetryButton;

	public Button m_ShareButton;

	private string m_SubstringToColor;

	private Color m_SubstringColor;

	private bool m_Open;

	private bool m_LoadMainMenuOnClose;

	private void Start()
	{
		m_ExitButton.onClick.AddListener(OnExit);
		m_RetryButton.onClick.AddListener(OnRetry);
		m_ShareButton.onClick.AddListener(OnShare);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		m_LevelFailedRectTransform.sizeDelta = new Vector2(m_LevelFailedRectTransform.sizeDelta.x, (CampaignTutorial.IsRunning() || !Profiles.m_ActiveProfile.m_Replays) ? 236 : 475);
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		ShowGamepadLegend();
		m_LoadMainMenuOnClose = false;
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
	}

	private void Update()
	{
		m_Root.SetActive(!GameUI.m_Instance.m_ShareReplay.gameObject.activeInHierarchy);
		ProcessInput();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	public void Open()
	{
		if (m_Open)
		{
			return;
		}
		base.gameObject.SetActive(value: true);
		SetExitButtonTooltip();
		bool flag = m_ReplayPanel.Show(!GameUI.m_Instance.m_PolyTwitchMain.m_AutoPlayPanel.gameObject.activeInHierarchy);
		m_ReplayPanel.gameObject.SetActive(flag);
		m_ReplayPanel.DisableTimelineMarkers();
		m_NoReplaysText.gameObject.SetActive(!flag);
		m_ShareButton.gameObject.SetActive(flag);
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null)
		{
			m_LevelNameText.text = Campaign.m_CurrentLevel.GetFullNameFormatted();
		}
		else
		{
			m_LevelNameText.text = Game.GetLevelTitle();
		}
		m_Banner.Refresh();
		if ((bool)m_RetryButton.GetComponent<TweenScale>())
		{
			if (Game.IsCurrentLevelTutorial())
			{
				m_RetryButton.GetComponent<TweenScale>().Play();
			}
			else
			{
				m_RetryButton.GetComponent<TweenScale>().Stop();
				m_RetryButton.GetComponent<TweenScale>().Reset();
			}
		}
		m_Open = true;
	}

	public void Close()
	{
		if (m_Open)
		{
			m_Open = false;
			m_ReplayPanel.Hide();
			base.gameObject.SetActive(value: false);
			if (m_LoadMainMenuOnClose)
			{
				GameStateManager.SwitchToState(GameState.MAIN_MENU);
			}
		}
	}

	public void SetFailReasonText(string text)
	{
		m_FailReasonText.text = text;
	}

	private void OnCancel()
	{
		GameUI.m_Instance.m_TopBar.OnExitSimSilent();
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void SetExitButtonTooltip()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			m_ExitToolTipText.m_LocalizationKey = ToolTipLocalizationKey.PAUSEMENU_EXIT_TO_CAMPAIGN_MENU;
		}
		else if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null && !WeeklyChallenges.IsAWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId()))
		{
			m_ExitToolTipText.m_LocalizationKey = ToolTipLocalizationKey.TOOLTIP_EXIT_TO_WORKSHOP;
		}
		else
		{
			m_ExitToolTipText.m_LocalizationKey = ToolTipLocalizationKey.TOOLTIP_MAIN_MENU;
		}
	}

	private void OnExit()
	{
		if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null && !WeeklyChallenges.IsAWeeklyChallenge(Workshop.m_LastPlayedWorkshopItem.GetId()))
		{
			ExitToWorkshop();
		}
		else if (GameManager.GetGameMode() == GameMode.SANDBOX && Sandbox.m_UnsavedChanges)
		{
			InterfaceAudio.Play("ui_window_open");
			PopUpMessage.DisplayWarning(Localize.Get("POPUP_EXIT_SANDBOX_LOSE_CHANGES"), useYesNoLables: true, ExitToMainMenu);
		}
		else
		{
			ExitToMainMenu();
		}
	}

	private void ExitToMainMenu()
	{
		m_LoadMainMenuOnClose = true;
		OnCancel();
	}

	private void ExitToWorkshop()
	{
		OnCancel();
		GameUI.m_Instance.m_Workshop.Open(WorkshopView.LEVELS_AND_CAMPAIGNS);
	}

	private void OnRetry()
	{
		OnCancel();
	}

	private void OnShare()
	{
		GameUI.m_Instance.m_ShareReplay.Show();
		InterfaceAudio.Play("ui_fail_share");
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (GameInput.JustPressed(BindingType.START_SIM) || Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				OnRetry();
			}
			else if (m_RetryButton.gameObject.activeInHierarchy && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
			{
				OnRetry();
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				OnShare();
			}
		}
	}

	private void ShowGamepadLegend()
	{
		if (Game.IsCurrentLevelTutorial() || !Profiles.m_ActiveProfile.m_Replays)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_RETRY"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("TOOLTIP_SHARE"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_RETRY"));
		}
	}
}
