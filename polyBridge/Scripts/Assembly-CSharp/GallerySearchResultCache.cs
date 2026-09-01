using System.Collections.Generic;

public class GallerySearchResultCache
{
	public static int m_TotalCount;

	private static Dictionary<int, GallerySearchResult> m_Cache = new Dictionary<int, GallerySearchResult>();

	public static GallerySearchResult Get(int pageIndex)
	{
		if (!m_Cache.ContainsKey(pageIndex))
		{
			return null;
		}
		return m_Cache[pageIndex];
	}

	public static void Cache(int pageIndex, GallerySearchResult searchResult)
	{
		if (!m_Cache.ContainsKey(pageIndex))
		{
			m_Cache.Add(pageIndex, searchResult);
		}
		else
		{
			m_Cache[pageIndex] = searchResult;
		}
	}

	public static bool IsPageCached(int pageIndex)
	{
		return m_Cache.ContainsKey(pageIndex);
	}

	public static void Clear()
	{
		m_Cache.Clear();
	}
}
