using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMessage : MonoBehaviour
{
	public static readonly float DEFAULT_DURATION_SECONDS = 2f;

	public TextMeshProUGUI m_Message;

	private float m_MessageTimer;

	private bool m_FadeOut;

	private string m_PinnedMessage;

	private ScreenMessageLocation m_Location;

	private Image m_Background;

	private RectTransform m_RectTransform;

	private readonly float DEFAULT_BACKGROUND_ALPHA = 0.5882353f;

	private void Awake()
	{
		m_RectTransform = GetComponent<RectTransform>();
	}

	private void Update()
	{
		UpdateMessageText();
		UpdateMessageScreenPos();
	}

	private void UpdateMessageScreenPos()
	{
		if (m_Location == ScreenMessageLocation.TOP_CENTER && (bool)m_RectTransform)
		{
			if (GameManager.GetGameMode() == GameMode.SANDBOX && GameStateManager.GetState() != GameState.SIM)
			{
				m_RectTransform.anchoredPosition = new Vector2(0f, GameUI.m_Instance.m_PointerToolTip.gameObject.activeInHierarchy ? (-147f) : (-110f));
			}
			else
			{
				m_RectTransform.anchoredPosition = new Vector2(0f, GameUI.m_Instance.m_PointerToolTip.gameObject.activeInHierarchy ? (-92f) : (-55f));
			}
		}
	}

	public void ShowMessage(ScreenMessageLocation location, string text, float durationSeconds)
	{
		m_Message.text = text;
		m_MessageTimer = durationSeconds;
		m_FadeOut = durationSeconds > 1.01f;
		m_Location = location;
		UpdateMessageScreenPos();
		base.gameObject.SetActive(value: true);
	}

	public void ClearMessage()
	{
		base.gameObject.SetActive(value: false);
		m_MessageTimer = 0f;
	}

	public bool MessageIsPinned()
	{
		return !string.IsNullOrEmpty(m_PinnedMessage);
	}

	public void PinMessage(string text)
	{
		m_PinnedMessage = text;
		m_Message.alpha = 1f;
		if (m_Background == null)
		{
			m_Background = GetComponent<Image>();
		}
		m_Background.color = new Color(0f, 0f, 0f, DEFAULT_BACKGROUND_ALPHA);
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: true);
			m_Message.text = string.Empty;
		}
	}

	public void UnpinMessage()
	{
		m_PinnedMessage = string.Empty;
		m_MessageTimer = 0f;
	}

	private void UpdateMessageText()
	{
		if (m_Background == null)
		{
			m_Background = GetComponent<Image>();
		}
		if (m_MessageTimer > 0f)
		{
			m_MessageTimer -= Time.unscaledDeltaTime;
			if (m_FadeOut)
			{
				m_Message.alpha = Mathf.Clamp01(m_MessageTimer);
				m_Background.color = new Color(0f, 0f, 0f, m_Message.alpha * DEFAULT_BACKGROUND_ALPHA);
			}
			if (!(m_MessageTimer < 0f))
			{
				return;
			}
			base.gameObject.SetActive(value: false);
		}
		if (!string.IsNullOrEmpty(m_PinnedMessage))
		{
			base.gameObject.SetActive(value: true);
			m_Message.text = m_PinnedMessage;
			m_Message.alpha = 1f;
			m_Background.color = new Color(0f, 0f, 0f, DEFAULT_BACKGROUND_ALPHA);
		}
		else
		{
			m_Message.text = string.Empty;
			base.gameObject.SetActive(value: false);
		}
	}
}
