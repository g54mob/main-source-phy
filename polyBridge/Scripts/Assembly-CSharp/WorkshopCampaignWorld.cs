using System.Collections.Generic;
using UnityEngine;

public class WorkshopCampaignWorld
{
	public string m_Id;

	public string m_DisplayName;

	public string m_Prefix;

	public int m_NumStars;

	public int m_Index;

	public string m_Subtitle;

	public bool m_UseCustomPosition;

	public Sprite m_IconSprite;

	public Sprite m_IconSpriteSelected;

	public Vector2 m_IconPosition;

	public List<string> m_LevelIds = new List<string>();

	public HashSet<string> m_Tutorials = new HashSet<string>();

	public WorkshopCampaignWorld(string id, string displayName, string prefix, string subtitle, int numStars)
	{
		m_Id = id;
		m_DisplayName = displayName;
		m_Prefix = prefix;
		m_Subtitle = subtitle;
		m_NumStars = numStars;
	}

	public WorkshopCampaignLevel GetLevel(string levelid)
	{
		return WorkshopCampaignsLevelCache.Get(levelid);
	}

	public string GetLevelPrefix(string levelid)
	{
		string text = m_Prefix;
		int levelIndex = GetLevelIndex(levelid);
		if (levelIndex >= 0)
		{
			if (m_Tutorials.Count > 0)
			{
				int num = 0;
				for (int i = 0; i < m_LevelIds.Count && !(m_LevelIds[i] == levelid); i++)
				{
					if (m_Tutorials.Contains(m_LevelIds[i]))
					{
						num++;
					}
				}
				text = ((!m_Tutorials.Contains(levelid)) ? (text + "-" + (levelIndex + 1 - num).ToString("00")) : (text + "-T"));
			}
			else
			{
				text = text + "-" + (levelIndex + 1).ToString("00");
			}
		}
		return text;
	}

	public string GetFormattedLevelNameWithPrefix(WorkshopItem level)
	{
		if (level != null)
		{
			return GameUI.GOLD_COLOR_HEX_TAG + GetLevelPrefix(level.GetId()) + " <#FFFFFF>" + level.GetTitle();
		}
		return string.Empty;
	}

	public int GetLevelIndex(string levelid)
	{
		for (int i = 0; i < m_LevelIds.Count; i++)
		{
			if (m_LevelIds[i] == levelid)
			{
				return i;
			}
		}
		return -1;
	}

	public int GetNumPassedLevels()
	{
		WorkshopCampaign withWorld = WorkshopCampaigns.GetWithWorld(this);
		if (withWorld == null)
		{
			return 0;
		}
		int num = 0;
		foreach (string levelId in m_LevelIds)
		{
			if (withWorld.m_CampaignProgress.HasCompletedLevel(levelId))
			{
				num++;
			}
		}
		return num;
	}

	public int GetNumPassedLevelsExTutorial()
	{
		WorkshopCampaign withWorld = WorkshopCampaigns.GetWithWorld(this);
		if (withWorld == null)
		{
			return 0;
		}
		int num = 0;
		foreach (string levelId in m_LevelIds)
		{
			if (!m_Tutorials.Contains(levelId) && withWorld.m_CampaignProgress.HasCompletedLevel(levelId))
			{
				num++;
			}
		}
		return num;
	}

	public int GetNumLevelsExTutorial()
	{
		int num = 0;
		foreach (string levelId in m_LevelIds)
		{
			if (!m_Tutorials.Contains(levelId))
			{
				num++;
			}
		}
		return num;
	}

	public int GetNumLevels()
	{
		if (m_LevelIds == null)
		{
			return 0;
		}
		return m_LevelIds.Count;
	}

	public WorkshopCampaignLevel GetFirstLevel()
	{
		foreach (string levelId in m_LevelIds)
		{
			WorkshopCampaignLevel workshopCampaignLevel = WorkshopCampaignsLevelCache.Get(levelId);
			if (workshopCampaignLevel != null)
			{
				return workshopCampaignLevel;
			}
		}
		return null;
	}

	public bool Is100PercentComplete()
	{
		WorkshopCampaign withWorld = WorkshopCampaigns.GetWithWorld(this);
		if (withWorld == null)
		{
			return false;
		}
		foreach (string levelId in m_LevelIds)
		{
			if (!withWorld.m_CampaignProgress.HasCompletedLevelUnderBudgetNoBreaks(levelId))
			{
				return false;
			}
		}
		return true;
	}
}
