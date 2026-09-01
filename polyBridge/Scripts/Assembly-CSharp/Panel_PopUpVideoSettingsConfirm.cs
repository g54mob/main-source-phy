using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PopUpVideoSettingsConfirm : MonoBehaviour
{
	public TextMeshProUGUI m_TimerText;

	public Button m_CancelButton;

	public Button m_OkButton;

	private float NUM_SECONDS_TO_CONFIRM = 10f;

	private float m_SecondsLeft;

	private void Awake()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_OkButton.onClick.AddListener(OnOk);
	}

	private void OnEnable()
	{
		m_SecondsLeft = NUM_SECONDS_TO_CONFIRM;
		InterfaceAudio.Play("ui_menubar_gen_on");
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		InterfaceAudio.Play("ui_menubar_gen_off");
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
		m_SecondsLeft -= Time.unscaledDeltaTime;
		int num = Mathf.CeilToInt(m_SecondsLeft);
		if (num == 0)
		{
			GameUI.m_Instance.m_Settings.m_GraphicsPanel.Revert();
			Close();
		}
		m_TimerText.text = num.ToString();
	}

	private void OnCancel()
	{
		GameUI.m_Instance.m_Settings.m_GraphicsPanel.Revert();
		Close();
		InterfaceAudio.Play("ui_menu_cancel");
	}

	private void OnOk()
	{
		GameUI.m_Instance.m_Settings.m_GraphicsPanel.ApplyAfterConfirmation();
		Close();
		GameUI.m_Instance.m_Settings.Close();
		Profiles.SaveActiveProfile();
	}

	private void Close()
	{
		GameUI.m_Instance.m_PopUpVideoSettingsConfirm.gameObject.SetActive(value: false);
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			OnCancel();
		}
	}
}
