using System.Collections.Generic;

public class WorkshopCampaignsLevelCache
{
	public static Dictionary<string, WorkshopCampaignLevel> m_Cache = new Dictionary<string, WorkshopCampaignLevel>();

	public static void Clear()
	{
		m_Cache.Clear();
	}

	public static WorkshopCampaignLevel Get(string id)
	{
		if (m_Cache.ContainsKey(id))
		{
			return m_Cache[id];
		}
		return null;
	}

	public static void Add(string id, WorkshopCampaignLevel level)
	{
		if (m_Cache.ContainsKey(id))
		{
			m_Cache[id] = level;
		}
		else
		{
			m_Cache.Add(id, level);
		}
	}
}
