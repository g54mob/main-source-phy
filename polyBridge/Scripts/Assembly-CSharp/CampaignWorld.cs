using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CampaignWorld", menuName = "Game/CampaignWorld", order = 4)]
public class CampaignWorld : ScriptableObject
{
	public string m_Id;

	public string m_Prefix;

	public int m_NumStars;

	public ThemePreloadStub m_ThemePreloadStub;

	public bool m_AdvanceToNextWorldAutomatically;

	[Header("Levels")]
	public CampaignLevel[] m_Levels;

	public CampaignLevel[] m_MainMenuLevels;

	[Header("Achievement")]
	public GameAchievement m_BeatWorldAchievement;

	[NonSerialized]
	public string m_DisplayNameLocID;

	[NonSerialized]
	public string m_DescriptionLocID;

	public void OnEnable()
	{
		m_DisplayNameLocID = "WORLD_" + m_Id;
		m_DescriptionLocID = "WORLD_" + m_Id + "_DESC";
	}

	public bool HasPassedAllLevels()
	{
		CampaignLevel[] levels = m_Levels;
		foreach (CampaignLevel campaignLevel in levels)
		{
			if (!Campaign.m_CampaignProgress.HasCompletedLevel(campaignLevel.m_Id))
			{
				return false;
			}
		}
		return true;
	}

	public int GetNumPassedLevels()
	{
		int num = 0;
		CampaignLevel[] levels = m_Levels;
		foreach (CampaignLevel campaignLevel in levels)
		{
			if (Campaign.m_CampaignProgress.HasCompletedLevel(campaignLevel.m_Id))
			{
				num++;
			}
		}
		return num;
	}

	public bool Is100PercentComplete()
	{
		CampaignLevel[] levels = m_Levels;
		foreach (CampaignLevel campaignLevel in levels)
		{
			if (!Campaign.m_CampaignProgress.HasCompletedLevelUnderBudgetNoBreaks(campaignLevel.m_Id))
			{
				return false;
			}
		}
		return true;
	}

	public float GetPercentComplete()
	{
		int num = 0;
		CampaignLevel[] levels = m_Levels;
		foreach (CampaignLevel campaignLevel in levels)
		{
			CampaignLevelState campaignLevelState = Campaign.m_CampaignProgress.GetCampaignLevelState(campaignLevel.m_Id);
			if (campaignLevelState != null)
			{
				num += GetNumPartsComplete(campaignLevelState.m_Status);
			}
		}
		return Mathf.Clamp(100f * (float)num / (float)(m_Levels.Length * 3), 0f, 100f);
	}

	public bool IsUnlockedByDefault()
	{
		return m_NumStars < 2;
	}

	public bool IsLocked()
	{
		return !IsUnLocked();
	}

	public bool IsUnLocked()
	{
		if (IsUnlockedByDefault())
		{
			return true;
		}
		if (m_NumStars == 2)
		{
			return CampaignWorlds.m_Instance.CompletedLevelCountAtStarLevel(m_NumStars - 1, CampaignProgress.NUM_LEVELS_TO_UNLOCK_2STAR_WORLDS);
		}
		if (m_NumStars == 3)
		{
			return CampaignWorlds.m_Instance.CompletedLevelCountAtStarLevel(m_NumStars - 1, CampaignProgress.NUM_LEVELS_TO_UNLOCK_3STAR_WORLDS);
		}
		if (m_NumStars == 4)
		{
			return CampaignWorlds.m_Instance.CompletedLevelCountAtStarLevel(m_NumStars - 1, CampaignProgress.NUM_LEVELS_TO_UNLOCK_4STAR_WORLDS);
		}
		if (m_NumStars == 5)
		{
			if (!Profiles.m_ActiveProfile.m_FiveStarUnlocks.Contains(m_Id))
			{
				if (CampaignWorlds.m_Instance.CompletedAllLevelsAtStarLevel(1) && CampaignWorlds.m_Instance.CompletedAllLevelsAtStarLevel(2) && CampaignWorlds.m_Instance.CompletedAllLevelsAtStarLevel(3))
				{
					return CampaignWorlds.m_Instance.CompletedAllLevelsAtStarLevel(4);
				}
				return false;
			}
			return true;
		}
		Debug.LogWarning($"Unexpected number of stars when checking if world IsUnlocked: {m_NumStars}");
		return false;
	}

	public bool IsSecretWorld()
	{
		return m_NumStars == 5;
	}

	public string GetDisplayName()
	{
		return Localize.Get(m_DisplayNameLocID);
	}

	public string GetDescription()
	{
		return Localize.Get(m_DescriptionLocID);
	}

	public ThemePreloadStub GetThemePreloadStub()
	{
		return m_ThemePreloadStub;
	}

	public bool ContainsLevel(string levelID)
	{
		for (int i = 0; i < m_Levels.Length; i++)
		{
			if (m_Levels[i].m_Id == levelID)
			{
				return true;
			}
		}
		return false;
	}

	public int GetLevelIndexExTutorials(string levelID)
	{
		int num = 0;
		for (int i = 0; i < m_Levels.Length; i++)
		{
			if (m_Levels[i].m_Id == levelID)
			{
				return num;
			}
			if (!m_Levels[i].IsTutorial())
			{
				num++;
			}
		}
		return -1;
	}

	private int GetNumPartsComplete(CampaignLevelStatus status)
	{
		return status switch
		{
			CampaignLevelStatus.PASS => 1, 
			CampaignLevelStatus.UNDER_BUDGET => 2, 
			CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS => 3, 
			_ => 0, 
		};
	}
}
