using UnityEngine;

public class Panel_CollapseBar : MonoBehaviour
{
	public PointerEvents m_CollapseBarPointerEvents;

	private bool m_IsMoving;

	private Vector3 m_MoveOffset;

	private Vector2 m_MoveStartPos;

	private Panel_EventEditor m_EventEditor;

	private bool m_IgnoreNextClick;

	private RectTransform m_CollapseBarRectTransform;

	private RectTransform m_EventEditorRectTransform;

	private void Start()
	{
		m_EventEditor = GameUI.m_Instance.m_EventEditor;
		m_CollapseBarPointerEvents.RegisterOnClickedDelegate(OnClickedCollapseBar);
		m_CollapseBarPointerEvents.RegisterOnUpDelegate(OnUpCollapseBar);
		m_CollapseBarPointerEvents.RegisterOnDownDelegate(OnDownCollapseBar);
		m_CollapseBarRectTransform = GetComponent<RectTransform>();
		m_EventEditorRectTransform = m_EventEditor.GetComponent<RectTransform>();
	}

	private void Update()
	{
	}

	private void OnDisable()
	{
		m_IsMoving = false;
	}

	public bool IsMoving()
	{
		return m_IsMoving;
	}

	private void OnClickedCollapseBar()
	{
		if (m_IgnoreNextClick)
		{
			m_IgnoreNextClick = false;
		}
		else if (m_EventEditor.m_CollapsePanel.m_CollapseState == PanelCollapseState.COLLAPSED)
		{
			m_EventEditor.UnCollapse(Profiles.m_ActiveProfile.m_EventEditorAnchorYNormalized);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
		else if (m_EventEditor.m_CollapsePanel.m_CollapseState == PanelCollapseState.UNCOLLAPSED)
		{
			m_EventEditor.Collapse();
			InterfaceAudio.Play("ui_menubar_gen_off");
		}
	}

	private void OnDownCollapseBar()
	{
	}

	private void OnUpCollapseBar()
	{
		if (m_IsMoving)
		{
			if (m_EventEditorRectTransform.anchoredPosition.y > m_CollapseBarRectTransform.sizeDelta.y)
			{
				Profiles.m_ActiveProfile.m_EventEditorAnchorYNormalized = Mathf.Clamp01(m_EventEditorRectTransform.anchoredPosition.y / EventEditor.MAX_ANCHOR_Y);
			}
			m_IsMoving = false;
		}
	}

	private void MoveWithPointer(Vector2 screenPos)
	{
		if (m_IsMoving)
		{
			float y = Mathf.Clamp((m_MoveOffset + GameInput.GetMousePosition()).y, EventEditor.MIN_ANCHOR_Y, GameUI.GetScreenYFromAnchor(EventEditor.MAX_ANCHOR_Y));
			m_EventEditor.transform.position = new Vector3(m_EventEditor.transform.position.x, y, m_EventEditor.transform.position.z);
			if (m_EventEditor.m_RootRectTransform.anchoredPosition.y < EventEditor.MIN_ANCHOR_Y)
			{
				m_EventEditor.m_RootRectTransform.anchoredPosition = new Vector2(m_EventEditor.m_RootRectTransform.anchoredPosition.x, EventEditor.MIN_ANCHOR_Y);
			}
			if (Mathf.FloorToInt(Mathf.Abs(m_MoveStartPos.y - m_EventEditor.transform.position.y)) > 1)
			{
				m_IgnoreNextClick = true;
			}
		}
	}
}
