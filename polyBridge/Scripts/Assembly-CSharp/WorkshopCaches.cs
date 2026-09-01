using System;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopCaches : MonoBehaviour
{
	public static Dictionary<WorkshopTab, WorkshopCache> m_Caches = new Dictionary<WorkshopTab, WorkshopCache>();

	public static readonly int NUM_ITEMS_PER_PAGE = 50;

	public static void Init()
	{
		m_Caches.Add(WorkshopTab.LEVELS, new WorkshopCache());
		m_Caches.Add(WorkshopTab.CAMPAIGNS, new WorkshopCache());
		m_Caches.Add(WorkshopTab.MODS, new WorkshopCache());
	}

	public static WorkshopItem GetItem(WorkshopTab tab, string itemID)
	{
		return m_Caches[tab].GetItem(itemID);
	}

	public static bool FilterMatches(WorkshopTab tab, WorkshopQueryFilter filter)
	{
		if (m_Caches[tab].m_QueryFilter != null)
		{
			return m_Caches[tab].m_QueryFilter.Matches(filter);
		}
		return false;
	}

	public static void Clear(WorkshopTab tab)
	{
		m_Caches[tab].Clear();
	}

	public static int GetCachePageIndexNeeded(WorkshopTab tab, int startIndex, int numItems)
	{
		int num = Mathf.FloorToInt(startIndex / NUM_ITEMS_PER_PAGE);
		int result = Mathf.FloorToInt((startIndex + numItems - 1) / NUM_ITEMS_PER_PAGE);
		if (!m_Caches[tab].m_WorkshopPages.ContainsKey(num))
		{
			return num;
		}
		return result;
	}

	public static bool ItemRangeInCache(WorkshopTab tab, WorkshopQueryFilter filter, int startIndex, int numItems)
	{
		if (!FilterMatches(tab, filter))
		{
			return false;
		}
		if (startIndex + numItems > m_Caches[tab].m_TotalCount)
		{
			numItems = m_Caches[tab].m_TotalCount - startIndex;
		}
		int key = Mathf.FloorToInt(startIndex / NUM_ITEMS_PER_PAGE);
		int key2 = Mathf.FloorToInt((startIndex + numItems - 1) / NUM_ITEMS_PER_PAGE);
		if (m_Caches[tab].m_WorkshopPages.ContainsKey(key))
		{
			return m_Caches[tab].m_WorkshopPages.ContainsKey(key2);
		}
		return false;
	}

	public static int GetTotalCount(WorkshopTab tab)
	{
		return m_Caches[tab].m_TotalCount;
	}

	public static void ForceUpdateTotalCount(WorkshopTab tab)
	{
		m_Caches[tab].m_TotalCount = 0;
		for (int i = 0; m_Caches[tab].m_WorkshopPages.ContainsKey(i); i++)
		{
			m_Caches[tab].m_TotalCount += m_Caches[tab].m_WorkshopPages[i].Length;
		}
	}

	public static WorkshopItem[] GetAllFiltered(WorkshopTab tab, string searchText)
	{
		List<WorkshopItem> list = new List<WorkshopItem>();
		string value = searchText.ToLower();
		for (int i = 0; m_Caches[tab].m_WorkshopPages.ContainsKey(i); i++)
		{
			WorkshopItem[] array = m_Caches[tab].m_WorkshopPages[i];
			foreach (WorkshopItem workshopItem in array)
			{
				if (searchText == string.Empty || workshopItem.GetId() == searchText || workshopItem.m_SteamItem.Title.ToLower().Contains(value) || workshopItem.m_SteamItem.Description.ToLower().Contains(value))
				{
					list.Add(workshopItem);
				}
			}
		}
		return list.ToArray();
	}

	public static WorkshopItem[] Get(WorkshopTab tab, WorkshopQueryFilter filter, int startIndex, int numItems)
	{
		if (filter != null && !ItemRangeInCache(tab, filter, startIndex, numItems))
		{
			return null;
		}
		int key = Mathf.FloorToInt(startIndex / NUM_ITEMS_PER_PAGE);
		int key2 = Mathf.FloorToInt((startIndex + numItems - 1) / NUM_ITEMS_PER_PAGE);
		WorkshopItem[] array = m_Caches[tab].m_WorkshopPages[key];
		WorkshopItem[] array2 = m_Caches[tab].m_WorkshopPages[key2];
		int num = startIndex % NUM_ITEMS_PER_PAGE;
		int num2 = Mathf.Min(num + numItems);
		int num3 = Mathf.Min(num + numItems, array.Length);
		int num4 = ((num2 >= NUM_ITEMS_PER_PAGE) ? (num2 - NUM_ITEMS_PER_PAGE) : 0);
		int num5 = num3 - num + num4;
		if (num5 <= 0)
		{
			return null;
		}
		WorkshopItem[] array3 = new WorkshopItem[num5];
		try
		{
			int num6 = 0;
			for (int i = num; i < num3; i++)
			{
				array3[num6++] = array[i];
			}
			for (int j = 0; j < num4; j++)
			{
				array3[num6++] = array2[j];
			}
			return array3;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static void Add(WorkshopTab tab, WorkshopQueryFilter filter, int pageIndex, WorkshopItem[] items, int totalCount)
	{
		m_Caches[tab].Add(filter, pageIndex, items, totalCount);
	}
}
