using ModIO.UI;
using UnityEngine;

public class DMTagToggleExplorer : MonoBehaviour, IExplorerViewElement
{
	[SerializeField]
	private string tagName;

	private StateToggleDisplay m_toggle;

	private ExplorerView m_view;

	public void SetExplorerView(ExplorerView view)
	{
		m_view = view;
	}

	public void ToggleTagFilter()
	{
		if (!(m_view == null))
		{
			if (m_toggle == null)
			{
				m_toggle = GetComponent<StateToggleDisplay>();
			}
			if (m_toggle.isOn)
			{
				m_view.defaultTab = tagName;
				m_view.AddTagToFilter(tagName);
			}
			else
			{
				m_view.RemoveTagFromFilter(tagName);
			}
		}
	}
}
