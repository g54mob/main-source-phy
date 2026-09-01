using System.Collections.Generic;
using System.IO;
using CloudinaryDotNet.Actions;
using UnityEngine;

public static class Gallery
{
	public delegate void OnUploadReplayDelegate(string entryId, string failureMessage);

	public delegate void OnDeleteReplayDelegate(string failureMessage);

	public static OnUploadReplayDelegate m_OnUploadReplayCallback;

	public static OnDeleteReplayDelegate m_OnDeleteReplayCallback;

	public static int NUM_SLOTS_PER_PAGE = 15;

	public static int MAX_ENTRIES_FOR_SEARCH = 150;

	public static int VIDEO_PREVIEW_WIDTH = 512;

	public static int VIDEO_PREVIEW_HEIGHT = 288;

	public static Dictionary<string, string> m_NameOwnerIdMap = new Dictionary<string, string>();

	public static Dictionary<int, GallerySearchResultStatus> m_GallerySearchResultStatus = new Dictionary<int, GallerySearchResultStatus>();

	private static GalleryFilterParameters s_CurrentCacheFilter = null;

	private static float m_GalleryItemDeleteTime;

	public static void Init()
	{
		m_GalleryItemDeleteTime = float.MinValue;
	}

	public static void LaunchForCurrentLevel()
	{
		if (GameManager.GetGameMode() == GameMode.WORKSHOP)
		{
			GameUI.m_Instance.m_Gallery.OpenWorkshopItem(Workshop.m_LastPlayedWorkshopItem.GetTitle(), Workshop.m_LastPlayedWorkshopItem.GetId());
		}
		else if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			GameUI.m_Instance.m_Gallery.OpenCampaignLevel(Campaign.m_CurrentLevel);
		}
	}

	public static void ClearCache()
	{
		s_CurrentCacheFilter = null;
		GallerySearchResultCache.Clear();
	}

	public static void SetFilter(GalleryFilterParameters parameters)
	{
		if (s_CurrentCacheFilter == null || parameters.DifferentFrom(s_CurrentCacheFilter))
		{
			ClearCache();
			ClearGallerySearchStatus();
		}
		s_CurrentCacheFilter = parameters;
	}

	public static void DownloadAllAsync(int pageIndex, int maxResults)
	{
		CloudinaryManager.SearchAsync(pageIndex, SearchFail, SearchSuccess, maxResults, GetNextCursor(pageIndex - 1), s_CurrentCacheFilter.IsSortByBudget() ? "public_id" : string.Empty, s_CurrentCacheFilter.GetSortDirection(), GetSteamIds(), s_CurrentCacheFilter.m_WorldId, s_CurrentCacheFilter.m_LevelId, GetIncludeTags(s_CurrentCacheFilter), GetExcludeTags(s_CurrentCacheFilter));
	}

	public static string GetPreviewUrl(SearchResource searchResource)
	{
		if (HasTag(searchResource, GalleryFilterParameters.CLOUDFLARE_TAG))
		{
			return Game.CLOUDFLARE_GALLERY_URL + searchResource.PublicId + ".jpg";
		}
		return Path.ChangeExtension(searchResource.Url, ".jpg").Replace("upload/", "upload/so_0,q_auto/");
	}

	public static bool HasTag(SearchResource searchResource, string tag)
	{
		if (searchResource.Tags != null)
		{
			string[] tags = searchResource.Tags;
			for (int i = 0; i < tags.Length; i++)
			{
				if (tags[i] == tag)
				{
					return true;
				}
			}
		}
		return false;
	}

	private static List<string> GetSteamIds()
	{
		if (s_CurrentCacheFilter.m_Ownership == GalleryFilterParameters.OWNERSHIP_FRIENDS_TAG)
		{
			return SteamUtils.GetFriendSteamIds();
		}
		if (!string.IsNullOrEmpty(s_CurrentCacheFilter.m_Ownership))
		{
			return new List<string> { s_CurrentCacheFilter.m_Ownership };
		}
		return null;
	}

	private static string GetIncludeTags(GalleryFilterParameters filter)
	{
		string text = string.Empty;
		if (filter.m_ShowOnlyFeatured)
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.FEATURED_TAG : ("," + GalleryFilterParameters.FEATURED_TAG));
		}
		if (filter.m_UnderBudgetOnly)
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.UNDERBUDGET_TAG : ("," + GalleryFilterParameters.UNDERBUDGET_TAG));
		}
		if (filter.m_UnbreakingOnly)
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.UNBREAKING_TAG : ("," + GalleryFilterParameters.UNBREAKING_TAG));
		}
		if (filter.m_Curated)
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.CURATED_TAG : ("," + GalleryFilterParameters.CURATED_TAG));
		}
		return text;
	}

	private static string GetExcludeTags(GalleryFilterParameters filter)
	{
		string text = string.Empty;
		if (filter.m_ShowOnlyWins)
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.FAIL_TAG : ("," + GalleryFilterParameters.FAIL_TAG));
		}
		if (!filter.m_IncludeCheats)
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.CHEAT_TAG : ("," + GalleryFilterParameters.CHEAT_TAG));
		}
		return text;
	}

	private static string GetNextCursor(int previousPageIndex)
	{
		if (previousPageIndex >= 0)
		{
			GallerySearchResult gallerySearchResult = GallerySearchResultCache.Get(previousPageIndex);
			if (gallerySearchResult != null)
			{
				return gallerySearchResult.m_NextCursor;
			}
		}
		return string.Empty;
	}

	private static void SearchSuccess(SearchResult searchResult, int pageIndex)
	{
		if (searchResult == null || searchResult.Resources == null || searchResult.Resources.Count == 0)
		{
			GallerySearchResultCache.m_TotalCount = 0;
			SetGallerySearchResultStatus(pageIndex, GallerySearchResultStatus.ZERO_ITEMS);
			return;
		}
		GallerySearchResultCache.m_TotalCount = searchResult.TotalCount;
		if (searchResult.Resources.Count <= NUM_SLOTS_PER_PAGE)
		{
			GallerySearchResult searchResult2 = new GallerySearchResult(searchResult.Resources, searchResult.NextCursor);
			GallerySearchResultCache.Cache(pageIndex, searchResult2);
			if (GetGallerySearchResultStatus(pageIndex) != GallerySearchResultStatus.CACHED)
			{
				SetGallerySearchResultStatus(pageIndex, GallerySearchResultStatus.CACHED);
				GameUI.m_Instance.m_Gallery.Refresh(pageIndex);
			}
			return;
		}
		int num = Mathf.CeilToInt((float)searchResult.Resources.Count / (float)NUM_SLOTS_PER_PAGE);
		for (int i = 0; i < num; i++)
		{
			int index = i * NUM_SLOTS_PER_PAGE;
			int count = Mathf.Min(searchResult.Resources.Count - i * NUM_SLOTS_PER_PAGE, NUM_SLOTS_PER_PAGE);
			GallerySearchResult searchResult3 = new GallerySearchResult(searchResult.Resources.GetRange(index, count), (i == num - 1) ? searchResult.NextCursor : string.Empty);
			GallerySearchResultCache.Cache(pageIndex + i, searchResult3);
			if (GetGallerySearchResultStatus(pageIndex + i) != GallerySearchResultStatus.CACHED)
			{
				SetGallerySearchResultStatus(pageIndex + i, GallerySearchResultStatus.CACHED);
				GameUI.m_Instance.m_Gallery.Refresh(pageIndex + i);
			}
		}
	}

	private static void SearchFail(string errorMessage, int pageIndex)
	{
		GallerySearchResultCache.m_TotalCount = 0;
		SetGallerySearchResultStatus(pageIndex, GallerySearchResultStatus.FAILED_LOAD);
	}

	public static void ClearGallerySearchStatus()
	{
		m_GallerySearchResultStatus.Clear();
	}

	public static bool DownloadInProgress()
	{
		foreach (KeyValuePair<int, GallerySearchResultStatus> item in m_GallerySearchResultStatus)
		{
			if (item.Value == GallerySearchResultStatus.LOADING)
			{
				return true;
			}
		}
		return false;
	}

	public static void SetGallerySearchResultStatus(int pageIndex, GallerySearchResultStatus result)
	{
		if (m_GallerySearchResultStatus.ContainsKey(pageIndex))
		{
			m_GallerySearchResultStatus[pageIndex] = result;
		}
		else
		{
			m_GallerySearchResultStatus.Add(pageIndex, result);
		}
	}

	public static GallerySearchResultStatus GetGallerySearchResultStatus(int pageIndex)
	{
		if (m_GallerySearchResultStatus.ContainsKey(pageIndex))
		{
			return m_GallerySearchResultStatus[pageIndex];
		}
		return GallerySearchResultStatus.NONE;
	}

	public static void RequestPreviewsForPage(int pageIndex)
	{
		GallerySearchResult gallerySearchResult = GallerySearchResultCache.Get(pageIndex);
		if (gallerySearchResult != null && GalleryPreviewRequests.NumInQ() <= NUM_SLOTS_PER_PAGE)
		{
			gallerySearchResult.RequestPreviewImages();
		}
	}

	public static void RegisterDeleteItem()
	{
		m_GalleryItemDeleteTime = Time.time;
	}

	public static bool GalleryItemDeletedInLastMinutes(int minutes)
	{
		int num = minutes * 60;
		return Time.time - m_GalleryItemDeleteTime < (float)num;
	}
}
