using System.Collections.Generic;
using UnityEngine;

public class CampaignWorlds : MonoBehaviour
{
	public CampaignWorld[] m_Worlds;

	public static readonly string WORLD_ID_ALL = "ALL";

	public static readonly string CYBER_WORLD_ID = "010";

	public static CampaignWorlds m_Instance;

	public static List<CampaignLevel> m_AllLevels = new List<CampaignLevel>();

	public static Dictionary<string, int> m_LevelCountDictionary = new Dictionary<string, int>();

	public static Dictionary<string, CampaignLevel> m_LevelDictionary = new Dictionary<string, CampaignLevel>();

	public static Dictionary<string, CampaignLevel> m_MainMenuLevelDictionary = new Dictionary<string, CampaignLevel>();

	private void Awake()
	{
		m_Instance = this;
	}

	public static void Init()
	{
		InitAllLevelsList();
		InitLevelCountDictionary();
		m_Instance.RefreshCachedCampaignLevelStrings();
	}

	public CampaignLevel GetNextLevel(CampaignLevel currentLevel)
	{
		if (currentLevel == null)
		{
			return null;
		}
		if (m_AllLevels.Contains(currentLevel))
		{
			int num = m_AllLevels.IndexOf(currentLevel);
			CampaignLevel campaignLevel = ((num < m_AllLevels.Count - 1) ? m_AllLevels[num + 1] : null);
			if (campaignLevel != null && campaignLevel.IsSecretWorldLevel() && !GameManager.IsSecretWorldUnlocked())
			{
				campaignLevel = null;
			}
			return campaignLevel;
		}
		return null;
	}

	public CampaignLevel GetPrevLevel(CampaignLevel currentLevel)
	{
		if (currentLevel == null)
		{
			return null;
		}
		if (m_AllLevels.Contains(currentLevel))
		{
			int num = m_AllLevels.IndexOf(currentLevel);
			if (num <= 0)
			{
				return null;
			}
			return m_AllLevels[num - 1];
		}
		return null;
	}

	public CampaignWorld GetWorldById(string id)
	{
		CampaignWorld[] worlds = m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (campaignWorld.m_Id == id)
			{
				return campaignWorld;
			}
		}
		return null;
	}

	public int GetNumLevels(bool includeSecret)
	{
		int num = 0;
		CampaignWorld[] worlds = m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (!campaignWorld.IsSecretWorld() || includeSecret)
			{
				num += m_LevelCountDictionary[campaignWorld.m_Id];
			}
		}
		return num;
	}

	public int GetNumLevelsInWorld(string id)
	{
		if (m_LevelCountDictionary.ContainsKey(id))
		{
			return m_LevelCountDictionary[id];
		}
		return 0;
	}

	public void ClearUnlimitedBudgetAndMaterialFlags()
	{
		CampaignWorld[] worlds = m_Worlds;
		for (int i = 0; i < worlds.Length; i++)
		{
			CampaignLevel[] levels = worlds[i].m_Levels;
			foreach (CampaignLevel obj in levels)
			{
				obj.m_UnlimitedBudget = false;
				obj.m_UnlimitedMaterial = false;
			}
		}
	}

	public CampaignWorld GetWorldForAnyLevel(string levelId)
	{
		if (string.IsNullOrEmpty(levelId))
		{
			return null;
		}
		if (m_LevelDictionary.ContainsKey(levelId))
		{
			return GetWorldWithLevelId(levelId);
		}
		if (m_MainMenuLevelDictionary.ContainsKey(levelId))
		{
			return GetWorldWithMainMenuLevelId(levelId);
		}
		return null;
	}

	public CampaignLevel GetLevelFromId(string levelId)
	{
		if (string.IsNullOrEmpty(levelId))
		{
			return null;
		}
		if (!m_LevelDictionary.ContainsKey(levelId))
		{
			return null;
		}
		return m_LevelDictionary[levelId];
	}

	public CampaignLevel GetMainMenuLevelFromId(string levelId)
	{
		if (string.IsNullOrEmpty(levelId))
		{
			return null;
		}
		if (!m_MainMenuLevelDictionary.ContainsKey(levelId))
		{
			return null;
		}
		return m_MainMenuLevelDictionary[levelId];
	}

	public CampaignWorld GetWorldWithLevelId(string levelId)
	{
		if (string.IsNullOrEmpty(levelId))
		{
			return null;
		}
		CampaignLevel levelFromId = GetLevelFromId(levelId);
		if (levelFromId == null)
		{
			return null;
		}
		return GetWorldById(levelFromId.m_WorldId);
	}

	public CampaignWorld GetWorldWithMainMenuLevelId(string levelId)
	{
		if (string.IsNullOrEmpty(levelId))
		{
			return null;
		}
		CampaignLevel mainMenuLevelFromId = GetMainMenuLevelFromId(levelId);
		if (mainMenuLevelFromId == null)
		{
			return null;
		}
		return GetWorldById(mainMenuLevelFromId.m_WorldId);
	}

	public void SetDefaultProgress()
	{
		Campaign.m_CampaignProgress.Reset();
		CampaignWorld[] worlds = m_Worlds;
		for (int i = 0; i < worlds.Length; i++)
		{
			CampaignLevel[] levels = worlds[i].m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				Campaign.m_CampaignProgress.SetStatus(campaignLevel.m_Id, CampaignLevelStatus.NONE);
			}
		}
		Campaign.m_CampaignProgress.SetStatus(m_Worlds[0].m_Levels[0].m_Id, CampaignLevelStatus.NONE);
	}

	public bool IsLevelLastInWorld(string levelId)
	{
		CampaignWorld worldWithLevelId = GetWorldWithLevelId(levelId);
		if (worldWithLevelId == null)
		{
			return false;
		}
		return worldWithLevelId.m_Levels[worldWithLevelId.m_Levels.Length - 1].m_Id == levelId;
	}

	public bool IsMainMenuLevel(string levelId)
	{
		return m_MainMenuLevelDictionary.ContainsKey(levelId);
	}

	public bool CompletedLevelCountAtStarLevel(int numStars, int levelCount)
	{
		int num = 0;
		CampaignWorld[] worlds = m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (campaignWorld.m_NumStars != numStars)
			{
				continue;
			}
			CampaignLevel[] levels = campaignWorld.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (Campaign.m_CampaignProgress.HasCompletedLevel(campaignLevel.m_Id))
				{
					num++;
				}
			}
		}
		return num >= levelCount;
	}

	public bool CompletedAllLevelsAtStarLevel(int numStars)
	{
		CampaignWorld[] worlds = m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (campaignWorld.m_NumStars != numStars)
			{
				continue;
			}
			CampaignLevel[] levels = campaignWorld.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (!Campaign.m_CampaignProgress.HasCompletedLevel(campaignLevel.m_Id))
				{
					return false;
				}
			}
		}
		return true;
	}

	public void MaybeUpdateFiveStarUnlocks()
	{
		bool flag = false;
		CampaignWorld[] worlds = m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (campaignWorld.m_NumStars == 5 && campaignWorld.IsUnLocked() && !Profiles.m_ActiveProfile.m_FiveStarUnlocks.Contains(campaignWorld.m_Id))
			{
				Profiles.m_ActiveProfile.m_FiveStarUnlocks.Add(campaignWorld.m_Id);
				flag = true;
			}
		}
		if (flag)
		{
			Profiles.SaveActiveProfile();
		}
	}

	public ThemePreloadStub GetPrelodStubForWorldPrefix(string prefix)
	{
		CampaignWorld[] worlds = m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (campaignWorld.m_Prefix == prefix)
			{
				return campaignWorld.m_ThemePreloadStub;
			}
		}
		return null;
	}

	public void RefreshCachedCampaignLevelStrings()
	{
		foreach (CampaignLevel allLevel in m_AllLevels)
		{
			allLevel.RefreshCachedStrings();
		}
	}

	private static void InitAllLevelsList()
	{
		for (int i = 0; i < m_Instance.m_Worlds.Length; i++)
		{
			CampaignWorld campaignWorld = m_Instance.m_Worlds[i];
			int num = 0;
			for (int j = 0; j < campaignWorld.m_Levels.Length; j++)
			{
				CampaignLevel campaignLevel = campaignWorld.m_Levels[j];
				m_AllLevels.Add(campaignLevel);
				if (m_LevelDictionary.ContainsKey(campaignLevel.m_Id))
				{
					Debug.LogWarningFormat("Duplicate Key For: {0} {1}", campaignLevel.GetLocalizedDisplayNameWithPrefix(), m_LevelDictionary[campaignLevel.m_Id].m_DisplayNameLocID);
					continue;
				}
				campaignLevel.m_WorldId = campaignWorld.m_Id;
				if (campaignLevel.IsTutorial())
				{
					campaignLevel.m_NumberPrefix = string.Format("{0}-{1}", campaignWorld.m_Prefix, "T");
					num = -1;
				}
				else
				{
					campaignLevel.m_NumberPrefix = string.Format("{0}-{1}", campaignWorld.m_Prefix, (j + 1 + num).ToString("D2"));
				}
				m_LevelDictionary.Add(campaignLevel.m_Id, campaignLevel);
			}
			for (int k = 0; k < campaignWorld.m_MainMenuLevels.Length; k++)
			{
				CampaignLevel campaignLevel2 = campaignWorld.m_MainMenuLevels[k];
				if (m_MainMenuLevelDictionary.ContainsKey(campaignLevel2.m_Id))
				{
					Debug.LogWarningFormat("Duplicate Key For: {0} {1}", campaignLevel2.GetLocalizedDisplayNameWithPrefix(), m_MainMenuLevelDictionary[campaignLevel2.m_Id].m_DisplayNameLocID);
					continue;
				}
				campaignLevel2.m_WorldId = campaignWorld.m_Id;
				campaignLevel2.m_NumberPrefix = string.Format("{0}-{1}", campaignWorld.m_Prefix, (k + 1).ToString("D2"));
				m_MainMenuLevelDictionary.Add(campaignLevel2.m_Id, campaignLevel2);
				if (!m_LevelDictionary.ContainsKey(campaignLevel2.m_Id))
				{
					m_LevelDictionary.Add(campaignLevel2.m_Id, campaignLevel2);
				}
			}
		}
	}

	private static void InitLevelCountDictionary()
	{
		CampaignWorld[] worlds = m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			m_LevelCountDictionary.Add(campaignWorld.m_Id, campaignWorld.m_Levels.Length);
		}
	}
}
