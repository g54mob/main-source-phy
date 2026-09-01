using System.Collections.Generic;
using UnityEngine;

public class WorkshopQueryFilters
{
	private static readonly List<string> TAGLIST_EMPTY = new List<string>();

	private static readonly List<string> TAGLIST_LEVEL_ONLY = new List<string> { WorkshopTags.LEVEL_TAG };

	private static readonly List<string> TAGLIST_MOD_ONLY = new List<string> { WorkshopTags.MOD_TAG };

	private static List<string> m_TempIncludeTagList = new List<string>();

	private static List<string> m_TempExcludeTagList = new List<string>();

	public static WorkshopQueryFilter AllocateNewQuery(WorkshopFilterBar filterBar, WorkshopTab tab)
	{
		switch (tab)
		{
		case WorkshopTab.LEVELS:
			SetIncludeAndExcludeListsForLevel();
			return new WorkshopQueryFilter(filterBar, m_TempIncludeTagList, m_TempExcludeTagList);
		case WorkshopTab.CAMPAIGNS:
			SetIncludeAndExcludeListsForCampaign();
			return new WorkshopQueryFilter(filterBar, m_TempIncludeTagList, m_TempExcludeTagList);
		case WorkshopTab.MODS:
			SetIncludeAndExcludeListsForMod();
			return new WorkshopQueryFilter(filterBar, m_TempIncludeTagList, m_TempExcludeTagList);
		default:
			Debug.LogWarning($"Unexpected tab: `{tab}'");
			return null;
		}
	}

	public static void UpdateQuery(WorkshopQueryFilter filter, WorkshopFilterBar filterBar, WorkshopTab tab)
	{
		switch (tab)
		{
		case WorkshopTab.LEVELS:
			SetIncludeAndExcludeListsForLevel();
			filter.Set(filterBar, m_TempIncludeTagList, m_TempExcludeTagList);
			break;
		case WorkshopTab.CAMPAIGNS:
			SetIncludeAndExcludeListsForCampaign();
			filter.Set(filterBar, m_TempIncludeTagList, m_TempExcludeTagList);
			break;
		case WorkshopTab.MODS:
			SetIncludeAndExcludeListsForMod();
			filter.Set(filterBar, m_TempIncludeTagList, m_TempExcludeTagList);
			break;
		default:
			Debug.LogWarning($"Unexpected tab: `{tab}'");
			break;
		}
	}

	private static void SetIncludeAndExcludeListsForLevel()
	{
		m_TempIncludeTagList.Clear();
		m_TempIncludeTagList.Add(WorkshopTags.LEVEL_TAG);
		WorkshopTags.GetRequiredTags(WorkshopTagMode.LEVEL, m_TempIncludeTagList);
		m_TempExcludeTagList.Clear();
		WorkshopTags.GetExcludeTags(WorkshopTagMode.LEVEL, m_TempExcludeTagList);
	}

	private static void SetIncludeAndExcludeListsForCampaign()
	{
		m_TempIncludeTagList.Clear();
		m_TempIncludeTagList.Add(WorkshopTags.CAMPAIGN_TAG);
		m_TempExcludeTagList.Clear();
	}

	private static void SetIncludeAndExcludeListsForMod()
	{
		m_TempIncludeTagList.Clear();
		m_TempIncludeTagList.Add(WorkshopTags.MOD_TAG);
		WorkshopTags.GetRequiredTags(WorkshopTagMode.MOD, m_TempIncludeTagList);
		m_TempExcludeTagList.Clear();
		WorkshopTags.GetExcludeTags(WorkshopTagMode.MOD, m_TempExcludeTagList);
	}
}
