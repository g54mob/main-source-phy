using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Settings : MonoBehaviour
{
	[Header("Top Buttons")]
	public Button m_TopRightCancel;

	[Header("Panels")]
	public Panel_SettingsControls m_ControlsPanel;

	public Panel_SettingsGamepad m_GamepadPanel;

	public Panel_SettingsGraphics m_GraphicsPanel;

	public Panel_SettingsAudio m_AudioPanel;

	public Panel_SettingsTwitch m_TwitchPanel;

	public Panel_SettingsOther m_OtherPanel;

	public Panel_SettingsReplays m_ReplaysPanel;

	[Header("Tab Buttons")]
	public SandboxTab m_ControlsTab;

	public SandboxTab m_GamepadTab;

	public SandboxTab m_GraphicsTab;

	public SandboxTab m_AudioTab;

	public SandboxTab m_OtherTab;

	public SandboxTab m_ReplaysTab;

	public SandboxTab m_TwitchTab;

	[Header("Colors")]
	public Color m_TabActiveColor;

	public Color m_TabInActiveColor;

	public Color m_RowEvenColor;

	public Color m_RowOddColor;

	public Color m_RowSelectedColor;

	[Header("Bottom Buttons")]
	public Button m_OK;

	public Button m_Cancel;

	private ProfileProxy m_ProfileProxyOnOpen;

	private ProfileProxyNoSync m_ProfileProxyNoSyncOnOpen;

	private int m_SelectedRowIndex;

	private List<SettingsRow> m_Rows = new List<SettingsRow>();

	private void Awake()
	{
		if (Game.IsRunningOnSteamDeck())
		{
			SelectTabSilent(m_GamepadPanel.gameObject);
		}
		else
		{
			SelectTabSilent(m_ControlsPanel.gameObject);
		}
	}

	private void Start()
	{
		m_ControlsTab.m_Button.onClick.AddListener(OnControlsButton);
		m_GamepadTab.m_Button.onClick.AddListener(OnGamepadButton);
		m_GraphicsTab.m_Button.onClick.AddListener(OnGraphicsButton);
		m_AudioTab.m_Button.onClick.AddListener(OnAudioButton);
		m_TwitchTab.m_Button.onClick.AddListener(OnTwitchButton);
		m_OtherTab.m_Button.onClick.AddListener(OnOtherButton);
		m_ReplaysTab.m_Button.onClick.AddListener(OnReplaysButton);
		m_OK.onClick.AddListener(OnOK);
		m_Cancel.onClick.AddListener(OnOK);
		m_TopRightCancel.onClick.AddListener(OnOK);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		if (Game.IsSteamDeckOrMobile() || SteamUtils.IsSteamInBigPictureMode())
		{
			m_ControlsTab.gameObject.SetActive(value: false);
			m_TwitchTab.gameObject.SetActive(value: false);
		}
		if (!Game.IsRunningOnSteamDeck())
		{
			GameUI.ApplyUIScaleMode(Profiles.m_ActiveProfile.m_UIScaleMode);
			GameUI.ApplyUIScaleFactor(Profiles.m_ActiveProfile.m_UIScaleFactor);
		}
		ShowGamepadLegend();
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		if (m_ControlsPanel.gameObject.activeInHierarchy || m_TwitchPanel.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"));
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_VERTICAL, Localize.Get("UI_SELECT_OPTION"), GamepadButtonType.DPAD_HORIZONTAL, Localize.Get("UI_CHANGE_OPTION"));
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("UI_RESET_TO_DEFAULTS"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
		}
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		m_TwitchPanel.UpdateManual();
		ProcessInput();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			if (m_GamepadPanel.gameObject.activeInHierarchy)
			{
				SetSelectedRowColor(m_GamepadPanel.m_Content, m_SelectedRowIndex);
			}
			else if (m_GraphicsPanel.gameObject.activeInHierarchy)
			{
				SetSelectedRowColor(m_GraphicsPanel.m_Content, m_SelectedRowIndex);
			}
			else if (m_AudioPanel.gameObject.activeInHierarchy)
			{
				SetSelectedRowColor(m_AudioPanel.m_Content, m_SelectedRowIndex);
			}
			else if (m_OtherPanel.gameObject.activeInHierarchy)
			{
				SetSelectedRowColor(m_OtherPanel.m_Content, m_SelectedRowIndex);
			}
			else if (m_ReplaysPanel.gameObject.activeInHierarchy)
			{
				SetSelectedRowColor(m_ReplaysPanel.m_Content, m_SelectedRowIndex);
			}
		}
		else if (m_GamepadPanel.gameObject.activeInHierarchy)
		{
			SetRowsColor(m_GamepadPanel.m_Content);
		}
		else if (m_GraphicsPanel.gameObject.activeInHierarchy)
		{
			SetRowsColor(m_GraphicsPanel.m_Content);
		}
		else if (m_AudioPanel.gameObject.activeInHierarchy)
		{
			SetRowsColor(m_AudioPanel.m_Content);
		}
		else if (m_OtherPanel.gameObject.activeInHierarchy)
		{
			SetRowsColor(m_OtherPanel.m_Content);
		}
		else if (m_ReplaysPanel.gameObject.activeInHierarchy)
		{
			SetRowsColor(m_ReplaysPanel.m_Content);
		}
	}

	public void Open()
	{
		m_ProfileProxyOnOpen = new ProfileProxy(Profiles.m_ActiveProfile, Profiles.CURRENT_VERSION);
		m_ProfileProxyNoSyncOnOpen = new ProfileProxyNoSync(Profiles.m_ActiveProfile, Profiles.CURRENT_VERSION);
		base.gameObject.SetActive(value: true);
		m_ControlsPanel.OnEnableManual();
		m_GamepadPanel.OnEnableManual();
		m_GraphicsPanel.OnEnableManual();
		m_AudioPanel.OnEnableManual();
		m_TwitchPanel.OnEnableManual();
		m_OtherPanel.OnEnableManual();
		m_ReplaysPanel.OnEnableManual();
		if (!m_ControlsTab.gameObject.activeInHierarchy && m_ControlsPanel.gameObject.activeSelf)
		{
			SelectTabSilent(m_GraphicsPanel.gameObject);
		}
	}

	public void OpenTwitchSettings()
	{
		Open();
		SelectTabSilent(m_TwitchPanel.gameObject);
	}

	public void Close()
	{
		m_ControlsPanel.OnDisableManual();
		m_GamepadPanel.OnDisableManual();
		m_GraphicsPanel.OnDisableManual();
		m_AudioPanel.OnDisableManual();
		m_TwitchPanel.OnDisableManual();
		m_OtherPanel.OnDisableManual();
		m_ReplaysPanel.OnDisableManual();
		InterfaceAudio.Play("ui_window_close");
		base.gameObject.SetActive(value: false);
		if (GameStateManager.GetState() == GameState.MAIN_MENU)
		{
			GameUI.m_Instance.m_MainMenuNew.Open();
		}
	}

	public void SetRowsColor(Transform content)
	{
		int num = 0;
		for (int i = 0; i < content.childCount; i++)
		{
			GameObject gameObject = content.GetChild(i).gameObject;
			if (!gameObject.activeSelf)
			{
				continue;
			}
			SettingsRow component = gameObject.GetComponent<SettingsRow>();
			if (component != null)
			{
				component.m_Background.color = ((num % 2 == 0) ? m_RowEvenColor : m_RowOddColor);
				if (component.m_Arrow != null)
				{
					component.m_Arrow.gameObject.SetActive(value: false);
				}
				num++;
			}
		}
	}

	public void SetSelectedRowColor(Transform content, int selectedIndex)
	{
		int num = 0;
		for (int i = 0; i < content.childCount; i++)
		{
			GameObject gameObject = content.GetChild(i).gameObject;
			if (!gameObject.activeInHierarchy)
			{
				continue;
			}
			SettingsRow component = gameObject.GetComponent<SettingsRow>();
			if (!(component != null))
			{
				continue;
			}
			if (num == selectedIndex)
			{
				component.m_Background.color = m_RowSelectedColor;
				if ((bool)component.m_Arrow)
				{
					component.m_Arrow.gameObject.SetActive(value: true);
				}
			}
			else
			{
				component.m_Background.color = ((num % 2 == 0) ? m_RowEvenColor : m_RowOddColor);
				if ((bool)component.m_Arrow)
				{
					component.m_Arrow.gameObject.SetActive(value: false);
				}
			}
			num++;
		}
	}

	private void OnControlsButton()
	{
		SelectTab(m_ControlsPanel.gameObject);
	}

	private void OnGamepadButton()
	{
		SelectTab(m_GamepadPanel.gameObject);
	}

	private void OnGraphicsButton()
	{
		SelectTab(m_GraphicsPanel.gameObject);
	}

	private void OnAudioButton()
	{
		SelectTab(m_AudioPanel.gameObject);
	}

	private void OnTwitchButton()
	{
		SelectTab(m_TwitchPanel.gameObject);
	}

	private void OnOtherButton()
	{
		SelectTab(m_OtherPanel.gameObject);
	}

	private void OnReplaysButton()
	{
		SelectTab(m_ReplaysPanel.gameObject);
	}

	public void OnOK()
	{
		m_ControlsPanel.Apply();
		m_GamepadPanel.Apply();
		m_AudioPanel.Apply(updateProfileOnly: false);
		m_GraphicsPanel.Apply(updateProfileOnly: false);
		m_TwitchPanel.Apply();
		m_OtherPanel.Apply();
		m_ReplaysPanel.Apply(updateProfileOnly: false);
		if (!Game.IsRunningOnSteamDeck() && m_GraphicsPanel.IsDirty())
		{
			SelectTab(m_GraphicsPanel.gameObject);
			Resolution resolutionFromSlider = m_GraphicsPanel.GetResolutionFromSlider();
			bool fullScreenFromToggle = m_GraphicsPanel.GetFullScreenFromToggle();
			Screen.SetResolution(resolutionFromSlider.width, resolutionFromSlider.height, fullScreenFromToggle);
			GameUI.m_Instance.m_PopUpVideoSettingsConfirm.gameObject.SetActive(value: true);
		}
		else
		{
			Close();
			InterfaceAudio.Play("ui_menu_accept");
		}
		Profiles.SaveActiveProfile();
	}

	private void SelectTab(GameObject panel)
	{
		SelectTabSilent(panel);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void SelectTabSilent(GameObject panel)
	{
		m_ControlsTab.m_Background.color = ((panel == m_ControlsPanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_GamepadTab.m_Background.color = ((panel == m_GamepadPanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_GraphicsTab.m_Background.color = ((panel == m_GraphicsPanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_AudioTab.m_Background.color = ((panel == m_AudioPanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_TwitchTab.m_Background.color = ((panel == m_TwitchPanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_OtherTab.m_Background.color = ((panel == m_OtherPanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_ReplaysTab.m_Background.color = ((panel == m_ReplaysPanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_ControlsTab.m_BackgroundRectTransform.offsetMin = ((panel == m_ControlsPanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_GamepadTab.m_BackgroundRectTransform.offsetMin = ((panel == m_GamepadPanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_GraphicsTab.m_BackgroundRectTransform.offsetMin = ((panel == m_GraphicsPanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_AudioTab.m_BackgroundRectTransform.offsetMin = ((panel == m_AudioPanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_TwitchTab.m_BackgroundRectTransform.offsetMin = ((panel == m_TwitchPanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_OtherTab.m_BackgroundRectTransform.offsetMin = ((panel == m_OtherPanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_ReplaysTab.m_BackgroundRectTransform.offsetMin = ((panel == m_ReplaysPanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_ControlsPanel.gameObject.SetActive((panel == m_ControlsPanel.gameObject) ? true : false);
		m_GamepadPanel.gameObject.SetActive((panel == m_GamepadPanel.gameObject) ? true : false);
		m_GraphicsPanel.gameObject.SetActive((panel == m_GraphicsPanel.gameObject) ? true : false);
		m_AudioPanel.gameObject.SetActive((panel == m_AudioPanel.gameObject) ? true : false);
		m_TwitchPanel.gameObject.SetActive((panel == m_TwitchPanel.gameObject) ? true : false);
		m_OtherPanel.gameObject.SetActive((panel == m_OtherPanel.gameObject) ? true : false);
		m_ReplaysPanel.gameObject.SetActive((panel == m_ReplaysPanel.gameObject) ? true : false);
		m_SelectedRowIndex = 0;
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			OnOK();
		}
		if (m_GraphicsPanel.gameObject.activeInHierarchy || m_AudioPanel.gameObject.activeInHierarchy || m_OtherPanel.isActiveAndEnabled || m_GamepadPanel.gameObject.activeInHierarchy || m_ReplaysPanel.gameObject.activeInHierarchy)
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP))
			{
				MoveSelectedRowUp();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN))
			{
				MoveSelectedRowDown();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_RIGHT))
			{
				ChangeOption(GamepadButtonType.DPAD_RIGHT);
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_LEFT))
			{
				ChangeOption(GamepadButtonType.DPAD_LEFT);
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH))
			{
				SettingsRow settingsRowUnderPointer = GameUI.GetSettingsRowUnderPointer();
				SettingsRow settingsRow = ((m_SelectedRowIndex >= 0) ? m_Rows[m_SelectedRowIndex] : null);
				if (settingsRowUnderPointer != null && settingsRowUnderPointer == settingsRow && settingsRowUnderPointer.m_SettingsRowType == SettingsRowType.TOGGLE && !GameUI.PointerOver(typeof(Toggle)))
				{
					settingsRowUnderPointer.m_Action?.Invoke(GamepadButtonType.SOUTH);
				}
			}
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			ResetToDefaults();
		}
		ProcessShoulderButtons();
	}

	private void ResetToDefaults()
	{
		if (m_ControlsPanel.gameObject.activeInHierarchy)
		{
			m_ControlsPanel.OnResetToDefaults();
		}
		else if (m_GamepadPanel.gameObject.activeInHierarchy)
		{
			m_GamepadPanel.OnResetToDefaults();
		}
		else if (m_GraphicsPanel.gameObject.activeInHierarchy)
		{
			m_GraphicsPanel.OnResetToDefaults();
		}
		else if (m_AudioPanel.gameObject.activeInHierarchy)
		{
			m_AudioPanel.OnResetToDefaults();
		}
		else if (m_OtherPanel.gameObject.activeInHierarchy)
		{
			m_OtherPanel.OnResetToDefaults();
		}
		else if (m_ReplaysPanel.gameObject.activeInHierarchy)
		{
			m_ReplaysPanel.OnResetToDefaults();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void ChangeOption(GamepadButtonType button)
	{
		if (m_SelectedRowIndex >= 0 && m_SelectedRowIndex < m_Rows.Count)
		{
			m_Rows[m_SelectedRowIndex].m_Action?.Invoke(button);
		}
	}

	public void ClearRows()
	{
		m_Rows.Clear();
	}

	public void AddRow(SettingsRow row)
	{
		m_Rows.Add(row);
	}

	private void MoveSelectedRowUp()
	{
		if (m_SelectedRowIndex <= 0)
		{
			m_SelectedRowIndex = 0;
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			m_SelectedRowIndex--;
			ForceGamepadCursorToSelecctedRow();
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void MoveSelectedRowDown()
	{
		if (m_SelectedRowIndex >= m_Rows.Count - 1)
		{
			m_SelectedRowIndex = m_Rows.Count - 1;
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			m_SelectedRowIndex++;
			ForceGamepadCursorToSelecctedRow();
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void ForceGamepadCursorToSelecctedRow()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedRowIndex != -1)
		{
			GameInput.SetVirtualMousePosition(m_Rows[m_SelectedRowIndex].m_Arrow.transform.position);
		}
	}

	private void ProcessShoulderButtons()
	{
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
		{
			if (Game.IsRunningOnSteamDeck())
			{
				if (m_ReplaysPanel.gameObject.activeInHierarchy)
				{
					SelectTab(m_OtherPanel.gameObject);
				}
				else if (m_OtherPanel.gameObject.activeInHierarchy)
				{
					SelectTab(m_AudioPanel.gameObject);
				}
				else if (m_AudioPanel.gameObject.activeInHierarchy)
				{
					SelectTab(m_GraphicsPanel.gameObject);
				}
				else if (m_GraphicsPanel.gameObject.activeInHierarchy)
				{
					SelectTab(m_GamepadPanel.gameObject);
				}
				else
				{
					InterfaceAudio.PlayErrorBeep();
				}
			}
			else if (m_TwitchPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_ReplaysPanel.gameObject);
			}
			else if (m_ReplaysPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_OtherPanel.gameObject);
			}
			else if (m_OtherPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_AudioPanel.gameObject);
			}
			else if (m_AudioPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_GraphicsPanel.gameObject);
			}
			else if (m_GraphicsPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_GamepadPanel.gameObject);
			}
			else if (m_GamepadPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_ControlsPanel.gameObject);
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		else
		{
			if (!GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
			{
				return;
			}
			if (Game.IsRunningOnSteamDeck())
			{
				if (m_GamepadPanel.gameObject.activeInHierarchy)
				{
					SelectTab(m_GraphicsPanel.gameObject);
				}
				else if (m_GraphicsPanel.gameObject.activeInHierarchy)
				{
					SelectTab(m_AudioPanel.gameObject);
				}
				else if (m_AudioPanel.gameObject.activeInHierarchy)
				{
					SelectTab(m_OtherPanel.gameObject);
				}
				else if (m_OtherPanel.gameObject.activeInHierarchy)
				{
					SelectTab(m_ReplaysPanel.gameObject);
				}
				else
				{
					InterfaceAudio.PlayErrorBeep();
				}
			}
			else if (m_ControlsPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_GamepadPanel.gameObject);
			}
			else if (m_GamepadPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_GraphicsPanel.gameObject);
			}
			else if (m_GraphicsPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_AudioPanel.gameObject);
			}
			else if (m_AudioPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_OtherPanel.gameObject);
			}
			else if (m_OtherPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_ReplaysPanel.gameObject);
			}
			else if (m_ReplaysPanel.gameObject.activeInHierarchy)
			{
				SelectTab(m_TwitchPanel.gameObject);
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
	}
}
