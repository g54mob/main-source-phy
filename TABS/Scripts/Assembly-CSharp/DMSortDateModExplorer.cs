using ModIO;
using ModIO.UI;
using UnityEngine;

public class DMSortDateModExplorer : MonoBehaviour, IExplorerViewElement
{
	[SerializeField]
	private int periodSeconds;

	[SerializeField]
	private int roundingSeconds;

	private ExplorerView m_view;

	public void SetExplorerView(ExplorerView view)
	{
		m_view = view;
	}

	public void UpdateExplorerDateSortFilter(bool enabled)
	{
		if (!(m_view == null) && enabled)
		{
			int num = -1;
			num = ServerTimeStamp.Now - periodSeconds;
			int num2 = num % roundingSeconds;
			num -= num2;
			MinimumFilter<int> minimumFilter = null;
			if (num > 0)
			{
				minimumFilter = new MinimumFilter<int>(0)
				{
					minimum = num,
					isInclusive = false
				};
			}
			m_view.SetFieldFilters("date_live", minimumFilter);
		}
	}
}
