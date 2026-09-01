using System.Collections.Generic;
using CloudinaryDotNet.Actions;
using UnityEngine;

public class GalleryCurate
{
	public static bool CURATE_MODE = false;

	private static int m_LastRandomIdx = -1;

	private static int m_SecondLastRandomIdx = -1;

	public static SearchResult GetExampleReplays(string levelID, int count)
	{
		SearchResult searchResult = new SearchResult();
		searchResult.Resources = new List<SearchResource>();
		for (int i = 0; i < count; i++)
		{
			SearchResource randomResource = GetRandomResource(levelID);
			if (randomResource != null)
			{
				searchResult.Resources.Add(randomResource);
			}
		}
		if (searchResult.Resources.Count == 0)
		{
			return null;
		}
		searchResult.TotalCount = searchResult.Resources.Count;
		return searchResult;
	}

	private static SearchResource GetRandomResource(string levelID)
	{
		List<GalleryCurateItem> pulicIDs = GetPulicIDs(levelID);
		if (pulicIDs == null || pulicIDs.Count == 0)
		{
			return null;
		}
		int count = pulicIDs.Count;
		int num = Random.Range(0, count);
		while (num == m_LastRandomIdx || num == m_SecondLastRandomIdx)
		{
			num = Random.Range(0, count);
		}
		m_SecondLastRandomIdx = m_LastRandomIdx;
		m_LastRandomIdx = num;
		return AllocateResource(pulicIDs[num]);
	}

	private static List<GalleryCurateItem> GetPulicIDs(string levelID)
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelID);
		if (worldWithLevelId == null)
		{
			return null;
		}
		switch (worldWithLevelId.m_Id)
		{
		case "001":
			if (!GalleryCurateDict_001.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_001.m_Items[levelID];
		case "002":
			if (!GalleryCurateDict_002.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_002.m_Items[levelID];
		case "003":
			if (!GalleryCurateDict_003.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_003.m_Items[levelID];
		case "004":
			if (!GalleryCurateDict_004.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_004.m_Items[levelID];
		case "005":
			if (!GalleryCurateDict_005.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_005.m_Items[levelID];
		case "006":
			if (!GalleryCurateDict_006.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_006.m_Items[levelID];
		case "007":
			if (!GalleryCurateDict_007.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_007.m_Items[levelID];
		case "008":
			if (!GalleryCurateDict_008.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_008.m_Items[levelID];
		case "009":
			if (!GalleryCurateDict_009.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_009.m_Items[levelID];
		case "010":
			if (!GalleryCurateDict_010.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_010.m_Items[levelID];
		case "011":
			if (!GalleryCurateDict_011.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_011.m_Items[levelID];
		case "012":
			if (!GalleryCurateDict_012.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_012.m_Items[levelID];
		case "013":
			if (!GalleryCurateDict_013.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_013.m_Items[levelID];
		case "014":
			if (!GalleryCurateDict_014.m_Items.ContainsKey(levelID))
			{
				return null;
			}
			return GalleryCurateDict_014.m_Items[levelID];
		default:
			return null;
		}
	}

	private static SearchResource AllocateResource(GalleryCurateItem item)
	{
		SearchResource searchResource = new SearchResource();
		searchResource.PublicId = item.m_ID;
		searchResource.Context = new Dictionary<string, string>
		{
			{ "LEVEL_ID", item.m_LevelID },
			{ "WORLD_ID", item.m_WorldID },
			{ "BUDGET", item.m_Budget },
			{ "MAX_STRESS_ENCODED", item.m_Stress }
		};
		searchResource.Tags = new string[1] { GalleryFilterParameters.CLOUDFLARE_TAG };
		return searchResource;
	}

	private static void AddReplaysToSearchResult(SearchResult searchResult, string levelID)
	{
		foreach (GalleryCurateItem pulicID in GetPulicIDs(levelID))
		{
			SearchResource item = AllocateResource(pulicID);
			searchResult.Resources.Add(item);
		}
	}
}
