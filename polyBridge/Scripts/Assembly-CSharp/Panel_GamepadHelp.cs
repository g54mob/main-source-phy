using UnityEngine;
using UnityEngine.UI;

public class Panel_GamepadHelp : MonoBehaviour
{
	public RectTransform m_Root;

	public Button m_CancelButton;

	[Header("Panels")]
	public GameObject m_SandboxModePanel;

	public GameObject m_BuildModePanel;

	public GameObject m_SimModePanel;

	[Header("Tab Buttons")]
	public SandboxTab m_SandboxModeButton;

	public SandboxTab m_BuildModeButton;

	public SandboxTab m_SimModeButton;

	[Header("Colors")]
	public Color m_TabActiveColor;

	public Color m_TabInActiveColor;

	public void Start()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_SandboxModeButton.m_Button.onClick.AddListener(OnSandboxModeButton);
		m_BuildModeButton.m_Button.onClick.AddListener(OnBuildModeButton);
		m_SimModeButton.m_Button.onClick.AddListener(OnSimModeButton);
	}

	private void Update()
	{
		ProcessInput();
	}

	public void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"));
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
		m_Root.anchoredPosition = new Vector2(0f, Game.IsRunningOnSteamDeck() ? (-10) : (-10));
	}

	public void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	public void Show()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: true);
			switch (GameStateManager.GetState())
			{
			case GameState.SANDBOX:
				SelectTabSilent(m_SandboxModePanel);
				break;
			case GameState.SIM:
				SelectTabSilent(m_SimModePanel);
				break;
			default:
				SelectTabSilent(m_BuildModePanel);
				break;
			}
		}
	}

	public void Close()
	{
		base.gameObject.SetActive(value: false);
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Game.ForceIgnoreNextSelection();
			}
			OnCancel();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
		{
			if (m_BuildModePanel.activeInHierarchy)
			{
				OnSandboxModeButton();
			}
			else if (m_SimModePanel.activeInHierarchy)
			{
				OnBuildModeButton();
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
		{
			if (m_SandboxModePanel.activeInHierarchy)
			{
				OnBuildModeButton();
			}
			else if (m_BuildModePanel.activeInHierarchy)
			{
				OnSimModeButton();
			}
			else
			{
				InterfaceAudio.PlayErrorBeep();
			}
		}
	}

	private void OnSandboxModeButton()
	{
		SelectTab(m_SandboxModePanel.gameObject);
	}

	private void OnBuildModeButton()
	{
		SelectTab(m_BuildModePanel.gameObject);
	}

	private void OnSimModeButton()
	{
		SelectTab(m_SimModePanel.gameObject);
	}

	private void SelectTab(GameObject panel)
	{
		SelectTabSilent(panel);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void SelectTabSilent(GameObject panel)
	{
		m_SandboxModeButton.m_Background.color = ((panel == m_SandboxModePanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_BuildModeButton.m_Background.color = ((panel == m_BuildModePanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_SimModeButton.m_Background.color = ((panel == m_SimModePanel.gameObject) ? m_TabActiveColor : m_TabInActiveColor);
		m_SandboxModeButton.m_BackgroundRectTransform.offsetMin = ((panel == m_SandboxModePanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_BuildModeButton.m_BackgroundRectTransform.offsetMin = ((panel == m_BuildModePanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_SimModeButton.m_BackgroundRectTransform.offsetMin = ((panel == m_SimModePanel.gameObject) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_SandboxModePanel.gameObject.SetActive((panel == m_SandboxModePanel.gameObject) ? true : false);
		m_BuildModePanel.gameObject.SetActive((panel == m_BuildModePanel.gameObject) ? true : false);
		m_SimModePanel.gameObject.SetActive((panel == m_SimModePanel.gameObject) ? true : false);
	}
}
