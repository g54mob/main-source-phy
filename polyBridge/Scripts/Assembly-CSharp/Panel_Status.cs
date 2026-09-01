using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Status : MonoBehaviour
{
	public TextMeshProUGUI m_Text;

	public Button m_OK;

	public GameObject m_WaitingAnimation;

	private void Start()
	{
		m_OK.onClick.AddListener(OnOK);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	public void Open(string text)
	{
		m_Text.text = text;
		m_WaitingAnimation.SetActive(value: true);
		m_OK.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_OK"));
		base.gameObject.SetActive(value: true);
	}

	private void Update()
	{
		ProcessInput();
	}

	public void Close()
	{
		base.gameObject.SetActive(value: false);
	}

	public void Complete(string text)
	{
		m_Text.text = text;
		GameUI.m_Instance.m_Status.m_WaitingAnimation.SetActive(value: false);
		GameUI.m_Instance.m_Status.m_OK.gameObject.SetActive(value: true);
	}

	private void OnOK()
	{
		InterfaceAudio.Play("ui_menu_accept");
		Close();
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if ((Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)) && m_OK.gameObject.activeInHierarchy)
			{
				InterfaceAudio.Play("ui_menu_accept");
				Close();
			}
			if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH)) && m_OK.gameObject.activeInHierarchy)
			{
				InterfaceAudio.Play("ui_menu_accept");
				Close();
			}
		}
	}
}
