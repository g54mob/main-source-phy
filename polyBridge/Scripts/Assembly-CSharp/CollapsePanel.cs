using UnityEngine;

public class CollapsePanel : MonoBehaviour
{
	public delegate void OnCompleteDelegate();

	public PanelCollapseState m_CollapseStateStart;

	public float m_CollapseSeconds;

	public float m_CollapseScreenWidthNormalized;

	public float m_CollapseScreenAnchorYNormalized;

	public PanelCollapseState m_CollapseState;

	public OnCompleteDelegate m_OnCollapseCompleteDelegate;

	public OnCompleteDelegate m_OnUnCollapseCompleteDelegate;

	private float m_ElapsedCollapseSeconds;

	private RectTransform m_RectTransform;

	private Vector2 m_AnchorOffsetStart;

	private bool m_Initialized;

	private void Awake()
	{
		m_RectTransform = GetComponent<RectTransform>();
		m_AnchorOffsetStart = m_RectTransform.anchoredPosition;
		m_CollapseState = m_CollapseStateStart;
		m_Initialized = true;
	}

	private void Update()
	{
		if (!m_Initialized)
		{
			return;
		}
		PanelCollapseState collapseState = m_CollapseState;
		if (m_CollapseState != PanelCollapseState.COLLAPSING && m_CollapseState != PanelCollapseState.UNCOLLAPSING)
		{
			return;
		}
		m_ElapsedCollapseSeconds += Time.unscaledDeltaTime;
		float num = Mathf.Clamp01(m_ElapsedCollapseSeconds / m_CollapseSeconds);
		float num2 = Mathf.SmoothStep(0f, Mathf.RoundToInt(m_CollapseScreenWidthNormalized * (float)Screen.width), InterpolateAwayFromInitialState() ? num : (1f - num));
		float num3 = Mathf.SmoothStep(EventEditor.MIN_ANCHOR_Y, Mathf.RoundToInt(m_CollapseScreenAnchorYNormalized * EventEditor.MAX_ANCHOR_Y), InterpolateAwayFromInitialState() ? num : (1f - num));
		m_RectTransform.anchoredPosition = new Vector3(m_AnchorOffsetStart.x + num2, m_AnchorOffsetStart.y + num3);
		if (Mathf.Approximately(num, 1f))
		{
			m_CollapseState = ((m_CollapseState == PanelCollapseState.COLLAPSING) ? PanelCollapseState.COLLAPSED : PanelCollapseState.UNCOLLAPSED);
			if (m_CollapseState == PanelCollapseState.COLLAPSED && collapseState != PanelCollapseState.COLLAPSED && m_OnCollapseCompleteDelegate != null)
			{
				m_OnCollapseCompleteDelegate();
			}
			if (m_CollapseState == PanelCollapseState.UNCOLLAPSED && collapseState != PanelCollapseState.UNCOLLAPSED && m_OnUnCollapseCompleteDelegate != null)
			{
				m_OnUnCollapseCompleteDelegate();
			}
		}
	}

	public void Collapse()
	{
		if (m_CollapseState != PanelCollapseState.COLLAPSING && m_CollapseState != PanelCollapseState.COLLAPSED)
		{
			m_CollapseState = PanelCollapseState.COLLAPSING;
			m_ElapsedCollapseSeconds = 0f;
		}
	}

	public void UnCollapse()
	{
		if (m_CollapseState != PanelCollapseState.UNCOLLAPSING && m_CollapseState != PanelCollapseState.UNCOLLAPSED)
		{
			m_CollapseState = PanelCollapseState.UNCOLLAPSING;
			m_ElapsedCollapseSeconds = 0f;
		}
	}

	public PanelCollapseState GetState()
	{
		return m_CollapseState;
	}

	public bool IsMoving()
	{
		if (m_CollapseState != PanelCollapseState.COLLAPSING)
		{
			return m_CollapseState == PanelCollapseState.UNCOLLAPSING;
		}
		return true;
	}

	public void ForceUpdate()
	{
		m_ElapsedCollapseSeconds = 10f;
		Update();
	}

	private bool InterpolateAwayFromInitialState()
	{
		if (m_CollapseStateStart == PanelCollapseState.UNCOLLAPSED && m_CollapseState == PanelCollapseState.COLLAPSING)
		{
			return true;
		}
		if (m_CollapseStateStart == PanelCollapseState.COLLAPSED && m_CollapseState == PanelCollapseState.UNCOLLAPSING)
		{
			return true;
		}
		return false;
	}
}
