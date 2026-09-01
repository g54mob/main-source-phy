using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_LevelInfoLite : MonoBehaviour
{
	[Header("Panel")]
	public RectTransform m_Panel;

	public RectTransform m_Viewport;

	public RectTransform m_Content;

	public Panel_Stages m_Stages;

	public int m_MinPanelWidth;

	public int m_MaxPanelWidth;

	public int m_MinPanelHeight;

	public int m_MaxPanelHeight;

	public bool m_Embedded;

	[Header("Header")]
	public TextMeshProUGUI m_Title;

	public Button m_Cancel;

	private Vector2 m_DefaultAnchorPosition;

	private float m_ContentLastX;

	private float m_ContentLastY;

	private bool m_IsDraggingScrollbar;

	private int m_NumFramesUntilVisibile;

	private void Awake()
	{
		m_DefaultAnchorPosition = m_Panel.anchoredPosition;
	}

	private void Start()
	{
		m_Cancel.onClick.AddListener(OnCancel);
	}

	private void OnEnable()
	{
		SetTitle();
		if (!m_Embedded)
		{
			m_Panel.anchoredPosition = new Vector2(0f, 10000f);
			m_NumFramesUntilVisibile = 3;
		}
		m_ContentLastX = m_Content.anchoredPosition.y;
		m_ContentLastY = m_Content.anchoredPosition.y;
		UpdatePanelDimensions();
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		if (!m_Embedded)
		{
			m_NumFramesUntilVisibile--;
			if (m_NumFramesUntilVisibile <= 0)
			{
				base.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, (Game.IsCurrentLevelTutorial() && GameUI.m_Instance.m_CampaignTutorial.BottomPanelIsShowing()) ? (-210f) : (-83f));
				ProcessInput();
			}
		}
		UpdateScrollbarState();
		m_Cancel.interactable = !Game.IsCurrentLevelTutorial();
	}

	public bool IsDraggingScrollbar()
	{
		return m_IsDraggingScrollbar;
	}

	public void OnCancel()
	{
		if (!Game.IsCurrentLevelTutorial())
		{
			base.gameObject.SetActive(value: false);
			InterfaceAudio.Play("ui_menubar_gen_off");
		}
	}

	private void SetTitle()
	{
		m_Title.text = Localize.Get("UI_TIMELINE");
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && !Game.IsCurrentLevelTutorial() && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			OnCancel();
		}
	}

	public void UpdatePanelDimensions()
	{
		m_Stages.ForceRebuildLayout();
		float value = ComputePanelWidth();
		float value2 = ComputePanelHeight();
		float num = GameUI.m_Instance.m_TopBar.GetComponent<RectTransform>().rect.width - 2f * m_Panel.anchoredPosition.x;
		if (m_MaxPanelWidth > 0)
		{
			num = Mathf.Min(num, m_MaxPanelWidth);
		}
		m_Panel.sizeDelta = new Vector2(Mathf.Clamp(value, m_MinPanelWidth, num), Mathf.Clamp(value2, m_MinPanelHeight, m_MaxPanelHeight));
	}

	private float ComputePanelWidth()
	{
		return m_Stages.ComputeTimelinesWidth() + 55f;
	}

	private float ComputePanelHeight()
	{
		return Mathf.Abs(m_Viewport.offsetMax.y) + m_Stages.ComputeTimelinesHeight() + 5f;
	}

	private void UpdateScrollbarState()
	{
		if (Mathf.Abs(m_Content.anchoredPosition.y - m_ContentLastY) > 0.001f)
		{
			m_IsDraggingScrollbar = true;
		}
		if (Mathf.Abs(m_Content.anchoredPosition.x - m_ContentLastX) > 0.001f)
		{
			m_IsDraggingScrollbar = true;
		}
		m_ContentLastX = m_Content.anchoredPosition.x;
		m_ContentLastY = m_Content.anchoredPosition.y;
		if (m_IsDraggingScrollbar && GameInput.GetMouseButtonJustReleased(0))
		{
			m_IsDraggingScrollbar = false;
		}
	}
}
