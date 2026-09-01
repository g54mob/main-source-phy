using System.Collections.Generic;
using UnityEngine;

public class CampaignProgress
{
	public Dictionary<string, CampaignLevelState> m_State = new Dictionary<string, CampaignLevelState>();

	public bool m_WinMessageHasBeenShown;

	public static int NUM_LEVELS_TO_UNLOCK_2STAR_WORLDS = 16;

	public static int NUM_LEVELS_TO_UNLOCK_3STAR_WORLDS = 10;

	public static int NUM_LEVELS_TO_UNLOCK_4STAR_WORLDS = 10;

	public static string CAMPAIGN_PROGRESS_FILENAME = "progress";

	public static bool Load()
	{
		Dictionary<string, CampaignLevelState> dictionary = CampaignProgressSerialize.LoadCampaignProgress(Profiles.GetActiveProfileName(), CAMPAIGN_PROGRESS_FILENAME);
		if (dictionary == null || dictionary.Count == 0)
		{
			return false;
		}
		Campaign.m_CampaignProgress.m_State = new Dictionary<string, CampaignLevelState>(dictionary);
		return true;
	}

	public static void Save()
	{
		if (Campaign.m_CampaignProgress != null && Campaign.m_CampaignProgress.m_State.Count > 0)
		{
			CampaignProgressSerialize.WriteCampaignProgress(Profiles.GetActiveProfileName(), CAMPAIGN_PROGRESS_FILENAME, Campaign.m_CampaignProgress);
		}
	}

	public CampaignProgress()
	{
	}

	public CampaignProgress(Dictionary<string, CampaignLevelState> progress)
	{
		m_State = new Dictionary<string, CampaignLevelState>(progress);
	}

	public CampaignLevelState GetCampaignLevelState(string levelId)
	{
		if (!m_State.ContainsKey(levelId))
		{
			return null;
		}
		return m_State[levelId];
	}

	public bool HasState(string levelId)
	{
		return m_State.ContainsKey(levelId);
	}

	public bool IsLocked(string levelId)
	{
		if (GetCampaignLevelState(levelId) == null)
		{
			return false;
		}
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelId);
		if ((bool)worldWithLevelId && worldWithLevelId.IsLocked())
		{
			return true;
		}
		return false;
	}

	public void SetStatus(string levelId, CampaignLevelStatus status)
	{
		if (!m_State.ContainsKey(levelId))
		{
			m_State.Add(levelId, new CampaignLevelState(0f, status));
		}
		else
		{
			m_State[levelId].m_Status = status;
		}
	}

	public int GetNumSecondsToComplete(string levelId)
	{
		if (!m_State.ContainsKey(levelId))
		{
			return 0;
		}
		return Mathf.RoundToInt(m_State[levelId].m_ElapsedSeconds);
	}

	public int GetNumCompletedLevels()
	{
		int num = 0;
		foreach (KeyValuePair<string, CampaignLevelState> item in m_State)
		{
			if (item.Value.m_Status == CampaignLevelStatus.PASS || item.Value.m_Status == CampaignLevelStatus.UNDER_BUDGET || item.Value.m_Status == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS)
			{
				num++;
			}
		}
		return num;
	}

	public bool HasCompletedAllLevels(int numLevelsInCampaign)
	{
		return GetNumCompletedLevels() == numLevelsInCampaign;
	}

	public CampaignLevelStatus GetLevelStatus(string levelId)
	{
		return GetCampaignLevelState(levelId)?.m_Status ?? CampaignLevelStatus.NONE;
	}

	public bool HasCompletedLevel(string levelId)
	{
		if (!m_State.ContainsKey(levelId))
		{
			return false;
		}
		CampaignLevelStatus status = m_State[levelId].m_Status;
		if (status != CampaignLevelStatus.PASS && status != CampaignLevelStatus.UNDER_BUDGET)
		{
			return status == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS;
		}
		return true;
	}

	public bool HasCompletedLevelUnderBudget(string levelId)
	{
		if (!m_State.ContainsKey(levelId))
		{
			return false;
		}
		CampaignLevelStatus status = m_State[levelId].m_Status;
		if (status != CampaignLevelStatus.UNDER_BUDGET)
		{
			return status == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS;
		}
		return true;
	}

	public bool HasCompletedLevelUnderBudgetNoBreaks(string levelId)
	{
		if (!m_State.ContainsKey(levelId))
		{
			return false;
		}
		return m_State[levelId].m_Status == CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS;
	}

	public void MarkLevelAsCompleted(string levelId, CampaignLevelStatus status)
	{
		if (!m_State.ContainsKey(levelId))
		{
			m_State.Add(levelId, new CampaignLevelState(0f, status));
		}
		else if (CampaignLevelState.StatusIsUpgrade(m_State[levelId].m_Status, status))
		{
			m_State[levelId].m_Status = status;
		}
	}

	public void UnlockNextLevel(CampaignLevel currentLevel)
	{
		CampaignLevel nextLevel = CampaignWorlds.m_Instance.GetNextLevel(currentLevel);
		if (nextLevel != null && IsLocked(nextLevel.m_Id))
		{
			SetStatus(nextLevel.m_Id, CampaignLevelStatus.NONE);
			Save();
		}
	}

	public void Reset()
	{
		m_State.Clear();
		m_WinMessageHasBeenShown = false;
	}
}
