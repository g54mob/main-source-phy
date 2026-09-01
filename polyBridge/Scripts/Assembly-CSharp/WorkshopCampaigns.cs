using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorkshopCampaigns
{
	public static Dictionary<string, WorkshopCampaign> m_Campaigns = new Dictionary<string, WorkshopCampaign>();

	public static string m_ActiveWorkshopCampaignModId;

	public static readonly int MAX_WORLD_PER_CAMPAIGN = 15;

	public static readonly int MAX_LEVELS_PER_WORLD = 16;

	public static readonly int MAX_DIFFICULTY = 7;

	public static readonly int MAX_WIN_MESSAGE_LENGTH = 1024;

	public static readonly Vector2 MIN_ICON_ANCHORED_POS = new Vector2(-125f, -210f);

	public static readonly Vector2 MAX_ICON_ANCHORED_POS = new Vector2(125f, 210f);

	public static void Add(string id, WorkshopCampaign campaign)
	{
		if (m_Campaigns.ContainsKey(id))
		{
			m_Campaigns[id] = campaign;
		}
		else
		{
			m_Campaigns.Add(id, campaign);
		}
	}

	public static WorkshopCampaign Get(string id)
	{
		if (m_Campaigns.ContainsKey(id))
		{
			return m_Campaigns[id];
		}
		return null;
	}

	public static WorkshopCampaign GetWithWorld(WorkshopCampaignWorld world)
	{
		foreach (KeyValuePair<string, WorkshopCampaign> campaign in m_Campaigns)
		{
			foreach (KeyValuePair<string, WorkshopCampaignWorld> world2 in campaign.Value.m_Worlds)
			{
				if (world2.Value.m_Id == world.m_Id)
				{
					return campaign.Value;
				}
			}
		}
		return null;
	}

	public static void ActivateWorkshopCampaignMod(string modId)
	{
		if (!(m_ActiveWorkshopCampaignModId == modId))
		{
			DeactivateCurrentWorkshopCampaignMod();
			if (!Mods.ModIsActive(modId))
			{
				Mods.ActivateMod(modId);
				Mods.RefreshMod(Mods.GetPathToMod(modId));
			}
			m_ActiveWorkshopCampaignModId = modId;
		}
	}

	public static void DeactivateCurrentWorkshopCampaignMod()
	{
		if (!string.IsNullOrEmpty(m_ActiveWorkshopCampaignModId))
		{
			Mods.DeactivateMod(m_ActiveWorkshopCampaignModId);
			Mods.RefreshAllMods(null);
		}
		m_ActiveWorkshopCampaignModId = string.Empty;
	}

	public static int GetNumCompletedLevels(string id)
	{
		Dictionary<string, CampaignLevelState> dictionary = CampaignProgressSerialize.LoadCampaignProgress(Profiles.GetActiveProfileName(), id + "." + WorkshopCampaignProgress.WORKSHOP_CAMPAIGN_PROGRESS_SUFFIX);
		if (dictionary == null)
		{
			return 0;
		}
		SteamItemInfo subscibedItem = Workshop.GetSubscibedItem(id);
		if (subscibedItem != null)
		{
			FileInfo[] luaFilesInMod = Mods.GetLuaFilesInMod(subscibedItem.m_InstallPath);
			if (luaFilesInMod != null && luaFilesInMod.Length != 0)
			{
				List<string> linesWithFunction = ModApi.GetLinesWithFunction(luaFilesInMod, "WorkshopCampaignAddLevelToWorld");
				HashSet<string> hashSet = new HashSet<string>();
				foreach (KeyValuePair<string, CampaignLevelState> item in dictionary)
				{
					if (!LinesContainId(linesWithFunction, item.Key))
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
			}
		}
		int num = 0;
		foreach (KeyValuePair<string, CampaignLevelState> item3 in dictionary)
		{
			if (item3.Value.m_Status == CampaignLevelStatus.PASS || item3.Value.m_Status == CampaignLevelStatus.UNDER_BUDGET || item3.Value.m_Status == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS)
			{
				num++;
			}
		}
		return num;
	}

	public static int GetNumLevels(string id)
	{
		SteamItemInfo subscibedItem = Workshop.GetSubscibedItem(id);
		if (subscibedItem == null)
		{
			return 0;
		}
		FileInfo[] luaFilesInMod = Mods.GetLuaFilesInMod(subscibedItem.m_InstallPath);
		if (luaFilesInMod != null && luaFilesInMod.Length != 0)
		{
			return ModApi.GetFunctionCount(luaFilesInMod, "OnModLoad", "WorkshopCampaignAddLevelToWorld");
		}
		return 0;
	}

	public static bool IsLevelATutorial(string levelId)
	{
		foreach (KeyValuePair<string, WorkshopCampaign> campaign in m_Campaigns)
		{
			foreach (KeyValuePair<string, WorkshopCampaignWorld> world in campaign.Value.m_Worlds)
			{
				if (world.Value.m_Tutorials.Contains(levelId))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool LinesContainId(List<string> lines, string id)
	{
		foreach (string line in lines)
		{
			if (line.IndexOf(id) > 0)
			{
				return true;
			}
		}
		return false;
	}
}
