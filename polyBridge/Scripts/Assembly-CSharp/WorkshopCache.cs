using System.Collections.Generic;

public class WorkshopCache
{
	public WorkshopQueryFilter m_QueryFilter;

	public int m_TotalCount;

	public Dictionary<int, WorkshopItem[]> m_WorkshopPages = new Dictionary<int, WorkshopItem[]>();

	public Dictionary<string, WorkshopItem> m_WorkshopDict = new Dictionary<string, WorkshopItem>();

	public void Clear()
	{
		m_TotalCount = 0;
		m_WorkshopPages.Clear();
		m_WorkshopDict.Clear();
	}

	public WorkshopItem[] Get(WorkshopQueryFilter filter, int pageIndex)
	{
		if (m_QueryFilter.Matches(filter) && m_WorkshopPages.ContainsKey(pageIndex))
		{
			return m_WorkshopPages[pageIndex];
		}
		return null;
	}

	public WorkshopItem GetItem(string itemID)
	{
		if (!m_WorkshopDict.ContainsKey(itemID))
		{
			return null;
		}
		return m_WorkshopDict[itemID];
	}

	public void Add(WorkshopQueryFilter filter, int pageIndex, WorkshopItem[] items, int totalCount)
	{
		m_QueryFilter = filter;
		m_TotalCount = totalCount;
		if (m_WorkshopPages.ContainsKey(pageIndex))
		{
			m_WorkshopPages[pageIndex] = items;
		}
		else
		{
			m_WorkshopPages.Add(pageIndex, items);
		}
		foreach (WorkshopItem workshopItem in items)
		{
			string id = workshopItem.GetId();
			if (m_WorkshopDict.ContainsKey(id))
			{
				m_WorkshopDict[id] = workshopItem;
			}
			else
			{
				m_WorkshopDict.Add(id, workshopItem);
			}
		}
	}
}
