using System.Collections.Generic;

public class WorkshopCampaignProgress
{
	public static readonly string WORKSHOP_CAMPAIGN_PROGRESS_SUFFIX = "progress";

	public static bool Load(WorkshopCampaign workshopCampaign)
	{
		Dictionary<string, CampaignLevelState> dictionary = CampaignProgressSerialize.LoadCampaignProgress(Profiles.GetActiveProfileName(), workshopCampaign.GetId() + "." + WORKSHOP_CAMPAIGN_PROGRESS_SUFFIX);
		if (dictionary == null || dictionary.Count == 0)
		{
			return false;
		}
		HashSet<string> hashSet = new HashSet<string>();
		foreach (KeyValuePair<string, CampaignLevelState> item in dictionary)
		{
			if (!workshopCampaign.ContainsLevel(item.Key))
			{
				hashSet.Add(item.Key);
			}
		}
		foreach (string item2 in hashSet)
		{
			if (dictionary.ContainsKey(item2))
			{
				dictionary.Remove(item2);
			}
		}
		workshopCampaign.m_CampaignProgress.m_State = new Dictionary<string, CampaignLevelState>(dictionary);
		return true;
	}

	public static void Save(WorkshopCampaign workshopCampaign)
	{
		if (workshopCampaign.m_CampaignProgress != null && workshopCampaign.m_CampaignProgress.m_State.Count > 0)
		{
			CampaignProgressSerialize.WriteCampaignProgress(Profiles.GetActiveProfileName(), workshopCampaign.GetId() + "." + WORKSHOP_CAMPAIGN_PROGRESS_SUFFIX, workshopCampaign.m_CampaignProgress);
		}
	}
}
