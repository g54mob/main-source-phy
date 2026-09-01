using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PopUpBindingConflict : MonoBehaviour
{
	public delegate void OnOkDelegate();

	public OnOkDelegate m_OnOkDelegate;

	public TextMeshProUGUI m_Line1;

	public TextMeshProUGUI m_Line2;

	public Button m_CancelButton;

	public Button m_OkButton;

	private void Awake()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_OkButton.onClick.AddListener(OnOk);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_menu_cancel");
		Close();
	}

	private void OnOk()
	{
		if (m_OnOkDelegate != null)
		{
			m_OnOkDelegate();
			InterfaceAudio.Play("ui_menu_accept");
		}
		Close();
	}

	private void Close()
	{
		GameUI.m_Instance.m_PopUpBindingConflict.gameObject.SetActive(value: false);
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			OnCancel();
		}
	}
}
