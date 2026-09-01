using ModIO.UI;
using UnityEngine;

public class DMSortModExplorer : MonoBehaviour, IExplorerViewElement
{
	[SerializeField]
	private ExplorerSortDropdownController.OptionData m_options;

	private ExplorerView m_view;

	public void SetExplorerView(ExplorerView view)
	{
		m_view = view;
	}

	public void SetExplorerViewSortMethod(bool enabled)
	{
		if (!(m_view == null) && enabled)
		{
			m_view.SetSortMethod(m_options.isAscending, m_options.fieldName);
		}
	}
}
