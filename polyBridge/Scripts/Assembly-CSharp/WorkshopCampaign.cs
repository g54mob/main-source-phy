using System.Collections.Generic;

public class WorkshopCampaign
{
	public string m_Id;

	public string m_WinMessage;

	public Dictionary<string, WorkshopCampaignWorld> m_Worlds = new Dictionary<string, WorkshopCampaignWorld>();

	public CampaignProgress m_CampaignProgress = new CampaignProgress();

	public WorkshopCampaign(string id, string winMessage)
	{
		m_Id = id;
		if (winMessage.Length >= WorkshopCampaigns.MAX_WIN_MESSAGE_LENGTH)
		{
			m_WinMessage = winMessage.Substring(0, WorkshopCampaigns.MAX_WIN_MESSAGE_LENGTH);
		}
		else
		{
			m_WinMessage = winMessage;
		}
	}

	public string GetId()
	{
		return m_Id;
	}

	public string GetModName()
	{
		string text = m_Id;
		if (Workshop.GetSubscibedItem(text) != null)
		{
			text = Workshop.GetSubscibedItem(text).m_Title;
		}
		return text;
	}

	public bool HasCompletedAllLevels()
	{
		return m_CampaignProgress.HasCompletedAllLevels(GetNumLevels());
	}

	public void AddWorld(string id, WorkshopCampaignWorld world)
	{
		if (m_Worlds.ContainsKey(id))
		{
			m_Worlds[id] = world;
			return;
		}
		m_Worlds.Add(id, world);
		world.m_Index = m_Worlds.Count - 1;
	}

	public WorkshopCampaignWorld GetWorld(string id)
	{
		if (m_Worlds.ContainsKey(id))
		{
			return m_Worlds[id];
		}
		return null;
	}

	public WorkshopCampaignWorld GetWorldWithIndex(int index)
	{
		foreach (WorkshopCampaignWorld value in m_Worlds.Values)
		{
			if (value.m_Index == index)
			{
				return value;
			}
		}
		return null;
	}

	public WorkshopCampaignWorld GetWorldWithLevelId(string levelId)
	{
		foreach (WorkshopCampaignWorld value in m_Worlds.Values)
		{
			if (value.m_LevelIds.Contains(levelId))
			{
				return value;
			}
		}
		return null;
	}

	public string GetNextLevelId()
	{
		if (Workshop.m_LastPlayedWorkshopItem == null)
		{
			return string.Empty;
		}
		string id = Workshop.m_LastPlayedWorkshopItem.GetId();
		WorkshopCampaignWorld worldWithLevelId = GetWorldWithLevelId(id);
		if (worldWithLevelId == null)
		{
			return string.Empty;
		}
		int num = worldWithLevelId.m_LevelIds.IndexOf(id);
		if (num < 0)
		{
			return string.Empty;
		}
		if (num + 1 < worldWithLevelId.m_LevelIds.Count)
		{
			return worldWithLevelId.m_LevelIds[num + 1];
		}
		WorkshopCampaignWorld worldWithIndex = GetWorldWithIndex(worldWithLevelId.m_Index + 1);
		if (worldWithIndex == null)
		{
			return string.Empty;
		}
		if (worldWithIndex.m_LevelIds.Count > 0)
		{
			return worldWithIndex.m_LevelIds[0];
		}
		return string.Empty;
	}

	public void ClearUnlimitedBudgetAndMaterialFlags()
	{
		foreach (KeyValuePair<string, WorkshopCampaignWorld> world in m_Worlds)
		{
			foreach (string levelId in world.Value.m_LevelIds)
			{
				WorkshopCampaignLevel workshopCampaignLevel = WorkshopCampaignsLevelCache.Get(levelId);
				if (workshopCampaignLevel != null)
				{
					workshopCampaignLevel.m_UnlimitedBudget = false;
					workshopCampaignLevel.m_UnlimitedMaterial = false;
				}
			}
		}
	}

	public int GetNumLevels()
	{
		int num = 0;
		foreach (KeyValuePair<string, WorkshopCampaignWorld> world in m_Worlds)
		{
			num += world.Value.m_LevelIds.Count;
		}
		return num;
	}

	public string GetTitle()
	{
		if (Workshop.m_SubscribedItems.ContainsKey(m_Id))
		{
			return Workshop.m_SubscribedItems[m_Id].m_Title;
		}
		return Localize.Get("UI_SANDBOX_WORLD_SELECTION");
	}

	public bool ContainsLevel(string levelId)
	{
		foreach (KeyValuePair<string, WorkshopCampaignWorld> world in m_Worlds)
		{
			if (world.Value.m_LevelIds.Contains(levelId))
			{
				return true;
			}
		}
		return false;
	}
}
