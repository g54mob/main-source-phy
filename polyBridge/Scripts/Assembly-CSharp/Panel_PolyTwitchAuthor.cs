using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PolyTwitchAuthor : MonoBehaviour
{
	[Header("Movement")]
	public RectTransform m_PanelRectTransform;

	public float m_VerticalBuffer;

	[Header("Header")]
	public TextMeshProUGUI m_Author;

	public Button m_MuteButton;

	private PolyTwitchSuggestion m_CurrentSuggestion;

	private bool m_MovingPanel;

	private Vector3 m_OffsetFromPointer;

	private Vector2 m_AchoredPosWhenStartMoving;

	private void OnEnable()
	{
		m_MuteButton.onClick.AddListener(OnBanPlayer);
		m_MovingPanel = false;
		Update();
		ClampWindowY();
	}

	private void OnDisable()
	{
		m_MuteButton.onClick.RemoveAllListeners();
	}

	private void Update()
	{
		ProcessInput();
		if (m_MovingPanel)
		{
			MovePanelWithMouse();
		}
		if (m_CurrentSuggestion != null)
		{
			GameUI.SetAndEnableText(m_Author, m_CurrentSuggestion.GetDisplayName());
		}
		ClampWindowX();
		ClampWindowY();
	}

	private bool CanAdvanceToNextLevel()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			return Campaign.GetNextLayoutFilename() != string.Empty;
		}
		return false;
	}

	public void SetCurrentSuggestion(PolyTwitchSuggestion suggestion)
	{
		m_CurrentSuggestion = suggestion;
	}

	public bool IsMoving()
	{
		return m_MovingPanel;
	}

	private void ClampWindowX()
	{
		float x = GameUI.m_Instance.m_CanvasScaler.referenceResolution.x;
		float x2 = m_PanelRectTransform.sizeDelta.x;
		float x3 = Mathf.Clamp(m_PanelRectTransform.anchoredPosition.x, (0f - x) / 2f + x2 / 2f, x / 2f - x2 / 2f);
		m_PanelRectTransform.anchoredPosition = new Vector2(x3, m_PanelRectTransform.anchoredPosition.y);
	}

	private void ClampWindowY()
	{
		float canvasHeight = GetCanvasHeight();
		float y = m_PanelRectTransform.sizeDelta.y;
		float y2 = Mathf.Clamp(m_PanelRectTransform.anchoredPosition.y, 0f - (canvasHeight - y), 0f - m_VerticalBuffer);
		m_PanelRectTransform.anchoredPosition = new Vector2(m_PanelRectTransform.anchoredPosition.x, y2);
	}

	private float GetCanvasHeight()
	{
		float num = (float)Screen.width / (float)Screen.height;
		return GameUI.m_Instance.m_CanvasScaler.referenceResolution.x / num;
	}

	private bool PanelMoved()
	{
		if ((int)m_AchoredPosWhenStartMoving.x != (int)m_PanelRectTransform.anchoredPosition.x)
		{
			return true;
		}
		if ((int)m_AchoredPosWhenStartMoving.y != (int)m_PanelRectTransform.anchoredPosition.y)
		{
			return true;
		}
		return false;
	}

	private void OnBanPlayer()
	{
		if (m_CurrentSuggestion != null)
		{
			PolyTwitchBans.BanPlayer(m_CurrentSuggestion.m_Username, m_CurrentSuggestion.m_OwnerId);
		}
	}

	private void ProcessInput()
	{
		Vector3 mousePosition = GameInput.GetMousePosition();
		if (GameInput.GetMouseButtonJustPressed(0) && GameUI.PointerOver(typeof(Panel_PolyTwitchAuthor)))
		{
			Vector3 vector = m_PanelRectTransform.transform.position - mousePosition;
			m_OffsetFromPointer = new Vector2(vector.x, vector.y);
			m_AchoredPosWhenStartMoving = m_PanelRectTransform.anchoredPosition;
			m_MovingPanel = true;
		}
		if (m_MovingPanel && GameInput.GetMouseButtonJustReleased(0))
		{
			m_MovingPanel = false;
			Profiles.m_ActiveProfile.m_TwitchAuthorPanelPos = m_PanelRectTransform.anchoredPosition;
			Profiles.SaveActiveProfile();
		}
	}

	private void MovePanelWithMouse()
	{
		m_PanelRectTransform.transform.position = m_OffsetFromPointer + GameInput.GetMousePosition();
		ClampWindowX();
		ClampWindowY();
	}
}
