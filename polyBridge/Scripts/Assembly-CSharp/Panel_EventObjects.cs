using UnityEngine;
using UnityEngine.UI;

public class Panel_EventObjects : MonoBehaviour
{
	public Button m_EventObjects;

	public ScrollRect m_ScrollRect;

	public GameObject m_ScrollHandle;

	private void OnEnable()
	{
		if ((bool)m_EventObjects)
		{
			m_EventObjects.onClick.AddListener(OnEventObjects);
		}
	}

	private void OnDisable()
	{
		if ((bool)m_EventObjects)
		{
			m_EventObjects.onClick.RemoveAllListeners();
		}
	}

	public void Update()
	{
		m_ScrollHandle.gameObject.SetActive(m_ScrollRect.enabled);
	}

	public void OnEventObjects()
	{
		if (GameUI.m_Instance.m_EventEditor.m_CollapsePanel.m_CollapseState == PanelCollapseState.COLLAPSED)
		{
			GameUI.m_Instance.m_EventEditor.UnCollapse(Profiles.m_ActiveProfile.m_EventEditorAnchorYNormalized);
		}
		else if (GameUI.m_Instance.m_EventEditor.m_CollapsePanel.m_CollapseState == PanelCollapseState.UNCOLLAPSED)
		{
			GameUI.m_Instance.m_EventEditor.Collapse();
		}
	}
}
