using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DMNavigationGroup : MonoBehaviour
{
	public GameObject m_defaultSelection;

	[HideInInspector]
	public GameObject m_lastSelection;

	private CanvasGroup m_canvasGroup;

	private EventSystem m_eventSystem;

	public void EnableNavigation()
	{
		m_canvasGroup.interactable = true;
		Select();
	}

	public void DisableNavigation()
	{
		m_canvasGroup.interactable = false;
		m_lastSelection = m_eventSystem.currentSelectedGameObject;
	}

	private void Select()
	{
		if (PlayerActions.Instance.InputType == InputType.Controller)
		{
			if (m_lastSelection != null && m_lastSelection.activeSelf)
			{
				m_eventSystem.SetSelectedGameObject(m_lastSelection);
			}
			else
			{
				m_eventSystem.SetSelectedGameObject(m_defaultSelection);
			}
		}
	}
}
