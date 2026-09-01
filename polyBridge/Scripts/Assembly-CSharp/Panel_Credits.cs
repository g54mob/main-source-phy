using UnityEngine;
using UnityEngine.UI;

public class Panel_Credits : MonoBehaviour
{
	public Button m_CancelButton;

	public RectTransform m_CreditsPage;

	public int m_StartCreditsY;

	public int m_EndCreditsY;

	public int m_ScrollSpeed;

	private float m_CreditsPageY;

	private void Start()
	{
		m_CancelButton.onClick.AddListener(Close);
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
		InterfaceAudio.Play("ui_window_open");
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
	}

	private void Update()
	{
		ProcessInput();
		ScrollCredits();
	}

	public void Open()
	{
		base.gameObject.SetActive(value: true);
		m_CreditsPage.anchoredPosition = new Vector2(0f, m_StartCreditsY);
		m_CreditsPageY = m_StartCreditsY;
	}

	private void Close()
	{
		base.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_MainMenuNew.Open();
		InterfaceAudio.Play("ui_window_close");
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			Close();
		}
	}

	private void ScrollCredits()
	{
		m_CreditsPageY += (float)m_ScrollSpeed * Time.unscaledDeltaTime;
		m_CreditsPage.anchoredPosition = new Vector2(0f, m_CreditsPageY);
		if (m_CreditsPageY > (float)m_EndCreditsY)
		{
			m_CreditsPageY = m_StartCreditsY;
		}
	}
}
