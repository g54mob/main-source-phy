using System.Collections.Generic;
using Steamworks.Data;
using Steamworks.Ugc;
using UnityEngine;

public class WorkshopQueryFilter
{
	public List<string> m_RequiredTags = new List<string>();

	public List<string> m_ExcludedTags = new List<string>();

	public WorkshopSortOrder m_SortOrder;

	public WorkshopOverTimePeriod m_OverTimePeriod;

	public string m_SearchText;

	public ulong m_SteamIdAsUlong;

	public WorkshopQueryFilter()
	{
		m_SortOrder = WorkshopSortOrder.NONE;
		m_OverTimePeriod = WorkshopOverTimePeriod.NONE;
	}

	public WorkshopQueryFilter(WorkshopFilterBar filterBar, List<string> requiredTags, List<string> excludedTags)
	{
		Set(filterBar, requiredTags, excludedTags);
	}

	public void Set(WorkshopFilterBar filterBar, List<string> requiredTags, List<string> excludedTags)
	{
		m_SortOrder = filterBar.m_WorkshopSortOrder;
		m_OverTimePeriod = filterBar.m_WorkshopTimePeriod;
		m_SearchText = (filterBar.DoesManualSearch() ? string.Empty : filterBar.m_SearchText);
		m_SteamIdAsUlong = filterBar.m_SteamIdForSearch;
		m_RequiredTags.Clear();
		foreach (string requiredTag in requiredTags)
		{
			m_RequiredTags.Add(requiredTag);
		}
		m_ExcludedTags.Clear();
		foreach (string excludedTag in excludedTags)
		{
			m_ExcludedTags.Add(excludedTag);
		}
	}

	public bool Matches(WorkshopQueryFilter other)
	{
		if (m_SearchText != other.m_SearchText)
		{
			return false;
		}
		if (m_SortOrder != other.m_SortOrder)
		{
			return false;
		}
		if (m_OverTimePeriod != other.m_OverTimePeriod)
		{
			return false;
		}
		if (m_RequiredTags.Count != other.m_RequiredTags.Count)
		{
			return false;
		}
		if (m_SortOrder != WorkshopSortOrder.MOST_RECENTLY_PLAYED)
		{
			foreach (string requiredTag in m_RequiredTags)
			{
				if (!other.m_RequiredTags.Contains(requiredTag))
				{
					return false;
				}
			}
			if (m_ExcludedTags.Count != other.m_ExcludedTags.Count)
			{
				return false;
			}
			foreach (string excludedTag in m_ExcludedTags)
			{
				if (!other.m_ExcludedTags.Contains(excludedTag))
				{
					return false;
				}
			}
			if (m_SteamIdAsUlong != other.m_SteamIdAsUlong)
			{
				return false;
			}
		}
		return true;
	}

	public Query CreateQuery(WorkshopTab tab, int cachePageIndex)
	{
		Query all = Query.All;
		if (m_SortOrder == WorkshopSortOrder.MOST_RECENTLY_PLAYED)
		{
			all = AddSortToQuery(all, m_SteamIdAsUlong, m_SortOrder, m_OverTimePeriod).WithMetadata(b: true).WithLongDescription(b: true).WithOnlyIDs(b: false);
			int num = cachePageIndex * WorkshopCaches.NUM_ITEMS_PER_PAGE;
			if (tab == WorkshopTab.LEVELS && WorkshopRecentlyPlayed.m_Levels.Count - num > 0)
			{
				int num2 = Mathf.Min(num + WorkshopCaches.NUM_ITEMS_PER_PAGE, WorkshopRecentlyPlayed.m_Levels.Count - num);
				List<PublishedFileId> list = new List<PublishedFileId>();
				for (int i = num; i < num2; i++)
				{
					if (ulong.TryParse(WorkshopRecentlyPlayed.m_Levels[i], out var result))
					{
						list.Add(result);
					}
				}
				all = all.WithFileId(list.ToArray());
			}
			else if (tab == WorkshopTab.CAMPAIGNS && WorkshopRecentlyPlayed.m_Campaigns.Count - num > 0)
			{
				int num3 = Mathf.Min(num + WorkshopCaches.NUM_ITEMS_PER_PAGE, WorkshopRecentlyPlayed.m_Campaigns.Count - num);
				List<PublishedFileId> list2 = new List<PublishedFileId>();
				for (int j = num; j < num3; j++)
				{
					if (ulong.TryParse(WorkshopRecentlyPlayed.m_Campaigns[j], out var result2))
					{
						list2.Add(result2);
					}
				}
				all = all.WithFileId(list2.ToArray());
			}
			else
			{
				PublishedFileId[] files = new PublishedFileId[1];
				all = all.WithFileId(files);
			}
		}
		else
		{
			all = AddSortToQuery(all, m_SteamIdAsUlong, m_SortOrder, m_OverTimePeriod).WithMetadata(b: true).WithLongDescription(b: true).WithOnlyIDs(b: false);
			if (!m_RequiredTags.Contains(WorkshopTags.LEVEL_TAG) && !m_RequiredTags.Contains(WorkshopTags.MOD_TAG) && !m_RequiredTags.Contains(WorkshopTags.CAMPAIGN_TAG))
			{
				all = all.MatchAllTags().WithTag(WorkshopTags.LEVEL_TAG);
			}
			else
			{
				all = all.MatchAllTags();
				foreach (string requiredTag in m_RequiredTags)
				{
					all = all.WithTag(requiredTag);
				}
				foreach (string excludedTag in m_ExcludedTags)
				{
					all = all.WithoutTag(excludedTag);
				}
			}
			if (!string.IsNullOrEmpty(m_SearchText))
			{
				if (Workshop.TextIsWorkshopID(m_SearchText))
				{
					PublishedFileId[] array = new PublishedFileId[1] { default(PublishedFileId) };
					if (ulong.TryParse(m_SearchText, out var result3))
					{
						array[0].Value = result3;
						all = all.WithFileId(array);
					}
				}
				else
				{
					all = all.WhereSearchText(m_SearchText);
				}
			}
		}
		return all;
	}

	private Query AddSortToQuery(Query query, ulong steamId, WorkshopSortOrder sortOrder, WorkshopOverTimePeriod timePeriod)
	{
		if (steamId != 0L)
		{
			return query.RankedByPublicationDate().WhereUserPublished(steamId);
		}
		switch (sortOrder)
		{
		case WorkshopSortOrder.MOST_RECENT:
			return query.RankedByPublicationDate();
		case WorkshopSortOrder.MOST_LIKED:
			if (timePeriod == WorkshopOverTimePeriod.ALL_TIME)
			{
				return query.RankedByVote();
			}
			return query.RankedByTrend().WithTrendDays(GetTrendDays(timePeriod));
		case WorkshopSortOrder.MOST_SUBSCRIBED:
			return query.RankedByTotalUniqueSubscriptions();
		case WorkshopSortOrder.SUBSCRIBED_BY_ME:
			return query.RankedByPublicationDate().WhereUserSubscribed();
		case WorkshopSortOrder.CREATED_BY_ME:
			return query.RankedByTextSearch().WhereUserPublished();
		case WorkshopSortOrder.FAVORITED_BY_ME:
			return query.RankedByPublicationDate().WhereUserFavorited();
		case WorkshopSortOrder.CREATED_BY_FRIENDS:
			return query.CreatedByFriends();
		case WorkshopSortOrder.FAVORITED_BY_FRIENDS:
			return query.FavoritedByFriends();
		default:
			return query.RankedByPublicationDate();
		}
	}

	private int GetTrendDays(WorkshopOverTimePeriod timePeriod)
	{
		switch (timePeriod)
		{
		case WorkshopOverTimePeriod.TODAY:
			return 1;
		case WorkshopOverTimePeriod.PAST_WEEK:
			return 7;
		case WorkshopOverTimePeriod.PAST_MONTH:
			return 30;
		case WorkshopOverTimePeriod.PAST_YEAR:
			return 365;
		default:
			Debug.LogWarning($"Calling GetTrendDays with unsupported timePeriod '{timePeriod}'");
			return 10000;
		}
	}
}
