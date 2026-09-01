using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PolyTwitchMain : MonoBehaviour
{
	[Header("Movement")]
	public PointerEvents m_PointerEvents;

	public RectTransform m_PanelRectTransform;

	public GameObject m_Body;

	public RectTransform m_BodyRectTransform;

	public float m_VerticalBuffer;

	[Header("Header")]
	public RectTransform m_HeaderRectTransform;

	public Button m_HeaderButton;

	public Button m_NotificationsButton;

	public Button m_AutoplayButton;

	public Button m_MinMaxButton;

	public Button m_ResizeButton;

	public Image m_MinMaxButtonImage;

	public Sprite m_MinimizeSprite;

	public Sprite m_MaximizeSprite;

	public TextMeshProUGUI m_NotificationsButtonText;

	public Image m_BitsIndicatorImage;

	public Sprite[] m_BitsSpritesHighToLow;

	[Header("Buttons")]
	public Button m_SuggestionsButton;

	public Button m_SettingsButton;

	public Button m_BanListButton;

	public Button m_HistoryButton;

	[Header("Button Images")]
	public Image m_SuggestionsButtonImage;

	public Image m_SettingsButtonImage;

	public Image m_BanListButtonImage;

	public Image m_HistoryButtonImage;

	[Header("Sub Panels")]
	public Panel_PolyTwitchAutoPlay m_AutoPlayPanel;

	public Panel_PolyTwitchAuthor m_AuthorPanel;

	public Panel_PolyTwitchMain_Suggestions m_SuggestionsPanel;

	public Panel_PolyTwitchMain_Settings m_SettingsPanel;

	public Panel_PolyTwitchMain_BanList m_BanListPanel;

	public Panel_PolyTwitchMain_History m_HistoryPanel;

	[Header("Resize")]
	public float m_Height;

	public float m_MinHeight;

	public float m_MinHeightWithBits;

	public float m_DefaultWidth;

	public PointerEvents m_ReSizePointerEvents;

	public ToolTipText m_ResizeButtonToolTip;

	private bool m_Collapsed;

	private bool m_MovingPanel;

	private bool m_ResizingPanel;

	private float m_ResizeGripOffset;

	private Vector3 m_OffsetFromPointer;

	private Vector2 m_AchoredPosWhenStartMoving;

	private bool m_DisplayBridgePending;

	private int m_NumFramesToDisplayBridge;

	private bool m_PanelRestoreHeightPushed;

	private float m_PanelRestoreHeight;

	private bool m_RestoreSuggestionsOnUpdate;

	public void Start()
	{
		m_SuggestionsPanel.gameObject.SetActive(value: false);
		m_SettingsPanel.gameObject.SetActive(value: true);
		m_BanListPanel.gameObject.SetActive(value: false);
		m_HistoryPanel.gameObject.SetActive(value: false);
		m_NotificationsButton.onClick.AddListener(OnNewSuggestions);
		m_HeaderButton.onClick.AddListener(OnCollapse);
		m_MinMaxButton.onClick.AddListener(OnCollapse);
		m_SuggestionsButton.onClick.AddListener(OnSuggestions);
		m_SettingsButton.onClick.AddListener(OnSettings);
		m_BanListButton.onClick.AddListener(OnBanList);
		m_HistoryButton.onClick.AddListener(OnHistory);
	}

	public void OnEnable()
	{
		m_AchoredPosWhenStartMoving = m_PanelRectTransform.anchoredPosition;
		ClampWindowY();
	}

	public void OnDisable()
	{
	}

	public void Update()
	{
		ProcessInput();
		UpdateAutoPlay();
		UpdateHighlightedIcons();
		UpdateSuggestionNotificationIcon();
		UpdateBitsIndicator();
		if (m_MovingPanel)
		{
			MovePanelWithMouse();
		}
		if (m_ResizingPanel)
		{
			ResizePanelWithMouse();
			m_ResizeButtonToolTip.m_Text = string.Empty;
		}
		else
		{
			m_ResizeButtonToolTip.m_Text = "Resize";
		}
		m_ResizeButton.gameObject.SetActive(!m_Collapsed);
		if (m_DisplayBridgePending)
		{
			m_NumFramesToDisplayBridge--;
			if (m_NumFramesToDisplayBridge == 0)
			{
				Bridge.CancelSelection();
				GameUI.m_Instance.m_PolyTwitchBridge.gameObject.SetActive(value: true);
				InterfaceAudio.Play("ui_window_open");
				m_DisplayBridgePending = false;
			}
		}
		ClampHeight();
		ClampWindowX();
		ClampWindowY();
		if (m_RestoreSuggestionsOnUpdate)
		{
			m_RestoreSuggestionsOnUpdate = false;
			RestoreSuggestions();
		}
	}

	public void SetHeight(float height)
	{
		m_Height = Mathf.Max(GetMinHeightToUse(), height);
		if (!m_Collapsed)
		{
			m_PanelRectTransform.sizeDelta = new Vector2(m_PanelRectTransform.sizeDelta.x, height);
		}
	}

	public void OnLayoutLoaded()
	{
		ClearWindowMovement();
		if (PolyTwitch.m_StreamStarted)
		{
			BackUpSuggestions();
			m_RestoreSuggestionsOnUpdate = true;
		}
	}

	public void ClearWindowMovement()
	{
		m_MovingPanel = false;
	}

	public void DisplayBridgeAfterImageLoads()
	{
		m_DisplayBridgePending = true;
		m_NumFramesToDisplayBridge = 3;
	}

	public bool IsMoving()
	{
		return m_MovingPanel;
	}

	public bool IsDraggingScrollbar()
	{
		if (!m_SuggestionsPanel.IsDraggingScrollbar() && !m_BanListPanel.IsDraggingScrollbar())
		{
			return m_HistoryPanel.IsDraggingScrollbar();
		}
		return true;
	}

	public bool IsHovering()
	{
		return m_PointerEvents.m_IsHovering;
	}

	public bool IsResizingPanel()
	{
		return m_ResizingPanel;
	}

	public void UnCollapse()
	{
		m_PanelRectTransform.sizeDelta = new Vector2(m_DefaultWidth, m_PanelRestoreHeightPushed ? m_PanelRestoreHeight : m_Height);
		m_PanelRestoreHeightPushed = false;
		m_Body.gameObject.SetActive(value: true);
		m_MinMaxButtonImage.sprite = m_MinimizeSprite;
		m_MinMaxButton.GetComponent<ToolTipText>().m_LocalizationKey = ToolTipLocalizationKey.TOOLTIP_MINIMIZE;
		ClampWindowY();
		m_Collapsed = false;
	}

	public void Collapse()
	{
		m_PanelRestoreHeight = m_PanelRectTransform.sizeDelta.y;
		m_PanelRestoreHeightPushed = true;
		m_PanelRectTransform.sizeDelta = new Vector2(m_DefaultWidth, 30f);
		m_Body.gameObject.SetActive(value: false);
		m_MinMaxButtonImage.sprite = m_MaximizeSprite;
		m_MinMaxButton.GetComponent<ToolTipText>().m_LocalizationKey = ToolTipLocalizationKey.TOOLTIP_MAXIMIZE;
		m_Collapsed = true;
	}

	public void GoToSettingsTab()
	{
		OnSettings();
	}

	private void UpdateBitsIndicator()
	{
		if (m_BitsSpritesHighToLow.Length < 5)
		{
			Debug.LogError("Bits Indicator Sprites Missing");
			return;
		}
		int highestUnviewedBitCount = PolyTwitchSuggestions.GetHighestUnviewedBitCount();
		m_BitsIndicatorImage.gameObject.SetActive(value: true);
		if (highestUnviewedBitCount >= 10000)
		{
			m_BitsIndicatorImage.sprite = m_BitsSpritesHighToLow[0];
		}
		else if (highestUnviewedBitCount >= 5000)
		{
			m_BitsIndicatorImage.sprite = m_BitsSpritesHighToLow[1];
		}
		else if (highestUnviewedBitCount >= 1000)
		{
			m_BitsIndicatorImage.sprite = m_BitsSpritesHighToLow[2];
		}
		else if (highestUnviewedBitCount >= 100)
		{
			m_BitsIndicatorImage.sprite = m_BitsSpritesHighToLow[3];
		}
		else if (highestUnviewedBitCount >= 1)
		{
			m_BitsIndicatorImage.sprite = m_BitsSpritesHighToLow[4];
		}
		else
		{
			m_BitsIndicatorImage.gameObject.SetActive(value: false);
		}
	}

	private void OnNewSuggestions()
	{
		if (!PolyTwitch.m_IsTakingScreenshot)
		{
			PolyTwitchSuggestion oldestUnViewedSuggestion = PolyTwitchSuggestions.GetOldestUnViewedSuggestion();
			GameUI.m_Instance.m_PolyTwitchBridge.ViewSuggestion(oldestUnViewedSuggestion);
			GameUI.m_Instance.m_PolyTwitchMain.DisplayBridgeAfterImageLoads();
		}
	}

	private void OnCollapse()
	{
		if (!m_MovingPanel || !PanelMoved())
		{
			if (m_Collapsed)
			{
				UnCollapse();
				InterfaceAudio.Play("ui_menubar_gen_on");
			}
			else
			{
				Collapse();
				InterfaceAudio.Play("ui_menubar_gen_off");
			}
			Profiles.m_ActiveProfile.m_TwitchStreamerWindowCollapsed = !m_Body.gameObject.activeSelf;
			Profiles.SaveActiveProfile();
		}
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

	private void ProcessInput()
	{
		Vector3 mousePosition = GameInput.GetMousePosition();
		if (GameInput.GetMouseButtonJustPressed(0) && GameUI.PointerOver(typeof(Panel_PolyTwitchMainMoveRegion)))
		{
			Vector3 vector = m_PanelRectTransform.transform.position - mousePosition;
			m_OffsetFromPointer = new Vector2(vector.x, vector.y);
			m_AchoredPosWhenStartMoving = m_PanelRectTransform.anchoredPosition;
			m_MovingPanel = true;
		}
		if (m_MovingPanel && GameInput.GetMouseButtonJustReleased(0))
		{
			m_MovingPanel = false;
			Profiles.m_ActiveProfile.m_TwitchStreamerWindowPos = GameUI.m_Instance.m_PolyTwitchMain.m_PanelRectTransform.anchoredPosition;
			Profiles.SaveActiveProfile();
		}
		if (GameInput.GetMouseButtonJustPressed(0) && m_ReSizePointerEvents.m_IsHovering)
		{
			m_ResizeGripOffset = CalculateResizeGripOffset(GameInput.GetMousePosition());
			m_ResizingPanel = true;
		}
		if (m_ResizingPanel && GameInput.GetMouseButtonJustReleased(0))
		{
			m_ResizingPanel = false;
			Profiles.m_ActiveProfile.m_TwitchStreamerWindowHeight = m_PanelRectTransform.sizeDelta.y;
			Profiles.SaveActiveProfile();
		}
	}

	private void MovePanelWithMouse()
	{
		m_PanelRectTransform.transform.position = m_OffsetFromPointer + GameInput.GetMousePosition();
		ClampWindowX();
		ClampWindowY();
	}

	private void ResizePanelWithMouse()
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(m_PanelRectTransform, GameInput.GetMousePosition(), null, out var localPoint);
		float value = Mathf.Abs(localPoint.y) + m_ResizeGripOffset;
		float max = Mathf.Max(GetMinHeightToUse(), GetCanvasHeight() + m_PanelRectTransform.anchoredPosition.y);
		SetHeight(Mathf.Clamp(value, GetMinHeightToUse(), max));
	}

	private float CalculateResizeGripOffset(Vector2 mouseScreenPos)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(m_PanelRectTransform, mouseScreenPos, null, out var localPoint);
		return m_PanelRectTransform.sizeDelta.y + localPoint.y;
	}

	private void ClampWindowX()
	{
		float x = GameUI.m_Instance.m_RectTransform.sizeDelta.x;
		float x2 = m_PanelRectTransform.sizeDelta.x;
		float x3 = Mathf.Clamp(m_PanelRectTransform.anchoredPosition.x, (0f - x) / 2f + x2 / 2f, x / 2f - x2 / 2f);
		m_PanelRectTransform.anchoredPosition = new Vector2(x3, m_PanelRectTransform.anchoredPosition.y);
	}

	private void ClampWindowY()
	{
		float canvasHeight = GetCanvasHeight();
		float y = m_PanelRectTransform.sizeDelta.y;
		float y2 = Mathf.Clamp(m_PanelRectTransform.anchoredPosition.y, 0f - (canvasHeight - y), 0f);
		m_PanelRectTransform.anchoredPosition = new Vector2(m_PanelRectTransform.anchoredPosition.x, y2);
	}

	private void ClampHeight()
	{
		float y = m_PanelRectTransform.sizeDelta.y;
		float max = Mathf.Max(GetMinHeightToUse(), GetCanvasHeight() + m_PanelRectTransform.anchoredPosition.y, 0f);
		SetHeight(Mathf.Clamp(y, GetMinHeightToUse(), max));
	}

	private float GetCanvasHeight()
	{
		return GameUI.m_Instance.m_RectTransform.sizeDelta.y;
	}

	private void OnSuggestions()
	{
		m_SuggestionsPanel.gameObject.SetActive(value: true);
		m_SettingsPanel.gameObject.SetActive(value: false);
		m_BanListPanel.gameObject.SetActive(value: false);
		m_HistoryPanel.gameObject.SetActive(value: false);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnSettings()
	{
		m_SuggestionsPanel.gameObject.SetActive(value: false);
		m_SettingsPanel.gameObject.SetActive(value: true);
		m_BanListPanel.gameObject.SetActive(value: false);
		m_HistoryPanel.gameObject.SetActive(value: false);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnBanList()
	{
		m_SuggestionsPanel.gameObject.SetActive(value: false);
		m_SettingsPanel.gameObject.SetActive(value: false);
		m_BanListPanel.gameObject.SetActive(value: true);
		m_HistoryPanel.gameObject.SetActive(value: false);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnHistory()
	{
		m_SuggestionsPanel.gameObject.SetActive(value: false);
		m_SettingsPanel.gameObject.SetActive(value: false);
		m_BanListPanel.gameObject.SetActive(value: false);
		m_HistoryPanel.gameObject.SetActive(value: true);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void UpdateAutoPlay()
	{
		m_AutoPlayPanel.gameObject.SetActive(GameStateManager.GetState() == GameState.SIM && PolyTwitchAutoPlay.m_Running);
	}

	private void UpdateHighlightedIcons()
	{
		if (m_SuggestionsPanel.gameObject.activeInHierarchy)
		{
			HighlightIcon(m_SuggestionsButtonImage);
		}
		else if (m_SettingsPanel.gameObject.activeInHierarchy)
		{
			HighlightIcon(m_SettingsButtonImage);
		}
		else if (m_BanListPanel.gameObject.activeInHierarchy)
		{
			HighlightIcon(m_BanListButtonImage);
		}
		else if (m_HistoryPanel.gameObject.activeInHierarchy)
		{
			HighlightIcon(m_HistoryButtonImage);
		}
		else
		{
			HighlightIcon(null);
		}
	}

	private void HighlightIcon(Image image)
	{
		m_SuggestionsButtonImage.color = ((image == m_SuggestionsButtonImage) ? GameUI.m_Instance.m_GoldColor : Color.white);
		m_SettingsButtonImage.color = ((image == m_SettingsButtonImage) ? GameUI.m_Instance.m_GoldColor : Color.white);
		m_BanListButtonImage.color = ((image == m_BanListButtonImage) ? GameUI.m_Instance.m_GoldColor : Color.white);
		m_HistoryButtonImage.color = ((image == m_HistoryButtonImage) ? GameUI.m_Instance.m_GoldColor : Color.white);
	}

	private void UpdateSuggestionNotificationIcon()
	{
		int numberOfUnseenNotifications = PolyTwitchSuggestions.GetNumberOfUnseenNotifications();
		m_NotificationsButton.gameObject.SetActive(numberOfUnseenNotifications > 0);
		if (numberOfUnseenNotifications > 99)
		{
			m_NotificationsButtonText.text = "99+";
		}
		else
		{
			m_NotificationsButtonText.text = numberOfUnseenNotifications.ToString();
		}
	}

	private void BackUpSuggestions()
	{
		m_SuggestionsPanel.MoveAllSuggestionsToBackUp();
	}

	private void RestoreSuggestions()
	{
		if (Sandbox.m_CurrentLayoutData != null)
		{
			string layoutHash = Utils.MD5HashFor(Sandbox.m_CurrentLayoutData.SerializeWithoutBridgeBinary());
			m_SuggestionsPanel.RestoreSuggestionsFromBackUp(layoutHash);
		}
	}

	private float GetMinHeightToUse()
	{
		if (!PolyTwitch.CanUseBits())
		{
			return m_MinHeight;
		}
		return m_MinHeightWithBits;
	}
}
