using System;
using System.Collections.Generic;
using System.Linq;
using DM;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Save
{
	[Serializable]
	public class TABSSave
	{
		[Serializable]
		public class SingleCampaignProgress
		{
			public DatabaseID CampaignGUID;

			public List<DatabaseID> CompletedLevels;
		}

		public Dictionary<Guid, List<Guid>> m_LevelDictionary;

		private Dictionary<DatabaseID, List<DatabaseID>> m_CompletedLevels = new Dictionary<DatabaseID, List<DatabaseID>>();

		private Dictionary<DatabaseID, List<DatabaseID>> m_CampaignLevelStreak = new Dictionary<DatabaseID, List<DatabaseID>>();

		private Dictionary<DatabaseID, List<DatabaseID>> m_CampaignNonPossessed = new Dictionary<DatabaseID, List<DatabaseID>>();

		private List<string> m_UnlockedSecrets = new List<string>();

		private DatabaseID[] m_FactionsOnBar;

		private Dictionary<TABSAchievement, int> m_achievementProgressDictionary = new Dictionary<TABSAchievement, int>();

		private List<SingleCampaignProgress> CompletedLevels { get; set; }

		public List<string> UnlockedSecrets => m_UnlockedSecrets;

		public Dictionary<TABSAchievement, int> AchievementProgressDictionary => m_achievementProgressDictionary;

		public void ClearSave()
		{
			ClearLevels();
			ClearSecrets();
		}

		public void ClearLevels()
		{
			if (m_CompletedLevels == null)
			{
				m_CompletedLevels = new Dictionary<DatabaseID, List<DatabaseID>>();
			}
			m_CompletedLevels.Clear();
		}

		public void ClearSecrets()
		{
			if (m_UnlockedSecrets == null)
			{
				m_UnlockedSecrets = new List<string>();
			}
			m_UnlockedSecrets.Clear();
		}

		public void AddLevel(DatabaseID id, DatabaseID campaignID)
		{
			if (m_CompletedLevels == null)
			{
				m_CompletedLevels = new Dictionary<DatabaseID, List<DatabaseID>>();
			}
			if (!m_CompletedLevels.ContainsKey(campaignID))
			{
				m_CompletedLevels.Add(campaignID, new List<DatabaseID>());
			}
			List<DatabaseID> list = m_CompletedLevels[campaignID];
			if (!list.Contains(id))
			{
				list.Add(id);
			}
			m_CompletedLevels[campaignID] = list;
		}

		public bool HasBeatenLevel(DatabaseID id, DatabaseID campaignID)
		{
			if (campaignID == new DatabaseID(-1337, 1337))
			{
				return true;
			}
			if (m_CompletedLevels == null || m_CompletedLevels.Count < 1)
			{
				return false;
			}
			if (!m_CompletedLevels.ContainsKey(campaignID))
			{
				return false;
			}
			return m_CompletedLevels[campaignID].Contains(id);
		}

		public void AddWinStreakLevel(DatabaseID id, DatabaseID campaignID)
		{
			if (m_CampaignLevelStreak == null)
			{
				m_CampaignLevelStreak = new Dictionary<DatabaseID, List<DatabaseID>>();
			}
			if (!m_CampaignLevelStreak.ContainsKey(campaignID))
			{
				m_CampaignLevelStreak.Add(campaignID, new List<DatabaseID>());
			}
			List<DatabaseID> list = m_CampaignLevelStreak[campaignID];
			if (!list.Contains(id))
			{
				list.Add(id);
			}
			m_CampaignLevelStreak[campaignID] = list;
		}

		public void ClearWinStreak(DatabaseID campaignID)
		{
			if (m_CampaignLevelStreak != null && m_CampaignLevelStreak.ContainsKey(campaignID))
			{
				m_CampaignLevelStreak[campaignID] = new List<DatabaseID>();
			}
		}

		public bool HasLevelInWinStreak(DatabaseID id, DatabaseID campaignID)
		{
			if (m_CampaignLevelStreak == null || m_CampaignLevelStreak.Count < 1)
			{
				return false;
			}
			if (!m_CampaignLevelStreak.ContainsKey(campaignID))
			{
				return false;
			}
			return m_CampaignLevelStreak[campaignID].Contains(id);
		}

		public void AddNonPossessedLevel(DatabaseID id, DatabaseID campaignID)
		{
			if (m_CampaignNonPossessed == null)
			{
				m_CampaignNonPossessed = new Dictionary<DatabaseID, List<DatabaseID>>();
			}
			if (!m_CampaignNonPossessed.ContainsKey(campaignID))
			{
				m_CampaignNonPossessed.Add(campaignID, new List<DatabaseID>());
			}
			List<DatabaseID> list = m_CampaignNonPossessed[campaignID];
			if (!list.Contains(id))
			{
				list.Add(id);
			}
			m_CampaignNonPossessed[campaignID] = list;
		}

		public void ClearNonPossessedCampaign(DatabaseID campaignID)
		{
			if (m_CampaignNonPossessed != null && m_CampaignNonPossessed.ContainsKey(campaignID))
			{
				m_CampaignNonPossessed[campaignID] = new List<DatabaseID>();
			}
		}

		public bool HasLevelInNonPossessedCampaign(DatabaseID id, DatabaseID campaignID)
		{
			if (m_CampaignNonPossessed == null || m_CampaignNonPossessed.Count < 1)
			{
				return false;
			}
			if (!m_CampaignNonPossessed.ContainsKey(campaignID))
			{
				return false;
			}
			return m_CampaignNonPossessed[campaignID].Contains(id);
		}

		public void UnlockSecret(string id)
		{
			if (m_UnlockedSecrets == null)
			{
				m_UnlockedSecrets = new List<string>();
			}
			if (!m_UnlockedSecrets.Contains(id))
			{
				m_UnlockedSecrets.Add(id);
			}
		}

		public bool HasUnlockedSecret(string id)
		{
			if (m_UnlockedSecrets == null)
			{
				m_UnlockedSecrets = new List<string>();
				return false;
			}
			return m_UnlockedSecrets.Contains(id);
		}

		private bool HasCampaign(DatabaseID id)
		{
			if (m_CompletedLevels == null)
			{
				m_CompletedLevels = new Dictionary<DatabaseID, List<DatabaseID>>();
				return false;
			}
			foreach (DatabaseID key in m_CompletedLevels.Keys)
			{
				if (key == id)
				{
					return true;
				}
			}
			return false;
		}

		public void UpdateSavedProgressAchievement(TABSAchievement achievement, int value, Action<TABSAchievement, int> callback, bool forceProgress = false)
		{
			if (achievement == null || value <= 0)
			{
				return;
			}
			if (m_achievementProgressDictionary == null)
			{
				m_achievementProgressDictionary = new Dictionary<TABSAchievement, int>();
			}
			if (!m_achievementProgressDictionary.ContainsKey(achievement))
			{
				m_achievementProgressDictionary.Add(achievement, 0);
			}
			if (!achievement.Data.IsProgressAchievement)
			{
				return;
			}
			int num = m_achievementProgressDictionary[achievement];
			if (num < achievement.Data.MaxValue || forceProgress)
			{
				int num2 = num + value;
				if (num2 >= achievement.Data.MaxValue)
				{
					int min = num2;
					num2 = Mathf.Clamp(num2, min, achievement.Data.MaxValue);
				}
				m_achievementProgressDictionary[achievement] = num2;
				callback?.Invoke(achievement, num2);
			}
		}

		public void Merge(TABSSave save)
		{
			if (save.m_CompletedLevels == null)
			{
				save.m_CompletedLevels = new Dictionary<DatabaseID, List<DatabaseID>>();
			}
			if (m_CompletedLevels == null)
			{
				m_CompletedLevels = new Dictionary<DatabaseID, List<DatabaseID>>();
			}
			if (m_achievementProgressDictionary == null)
			{
				m_achievementProgressDictionary = new Dictionary<TABSAchievement, int>();
			}
			foreach (KeyValuePair<DatabaseID, List<DatabaseID>> completedLevel in save.m_CompletedLevels)
			{
				if (!m_CompletedLevels.ContainsKey(completedLevel.Key))
				{
					m_CompletedLevels.Add(completedLevel.Key, new List<DatabaseID>());
				}
				foreach (DatabaseID item in completedLevel.Value)
				{
					if (!m_CompletedLevels[completedLevel.Key].Contains(item))
					{
						m_CompletedLevels[completedLevel.Key].Add(item);
					}
				}
			}
		}

		public void Upgrade()
		{
			if (m_LevelDictionary == null || m_LevelDictionary.Count == 0)
			{
				return;
			}
			if (m_CompletedLevels == null)
			{
				m_CompletedLevels = new Dictionary<DatabaseID, List<DatabaseID>>();
			}
			if (m_achievementProgressDictionary == null)
			{
				m_achievementProgressDictionary = new Dictionary<TABSAchievement, int>();
			}
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			if (contentDatabase == null)
			{
				return;
			}
			UpgradeDataAsset upgradeDataAsset = contentDatabase.GetUpgradeDataAsset();
			if (upgradeDataAsset == null)
			{
				return;
			}
			foreach (Guid key in m_LevelDictionary.Keys)
			{
				DatabaseID databaseID = upgradeDataAsset.GetDatabaseID(key);
				if (databaseID == default(DatabaseID))
				{
					continue;
				}
				List<DatabaseID> list = new List<DatabaseID>();
				if (m_CompletedLevels.ContainsKey(databaseID))
				{
					continue;
				}
				List<Guid> list2 = m_LevelDictionary[key];
				for (int i = 0; i < list2.Count; i++)
				{
					DatabaseID databaseID2 = upgradeDataAsset.GetDatabaseID(list2[i]);
					if (databaseID2 != default(DatabaseID))
					{
						list.Add(databaseID2);
					}
				}
				m_CompletedLevels.Add(databaseID, list);
			}
		}

		public Faction[] GetFactionBar()
		{
			ContentDatabase contentDatabase = ContentDatabase.Instance();
			if (m_FactionsOnBar == null || m_FactionsOnBar.Length == 0)
			{
				Faction[] array = contentDatabase.GetDefaultHotbarFactions().ToArray();
				Debug.Log("FACTION BAR DATA NOT FOUND. GENERATING DEFAULT DATA");
				m_FactionsOnBar = new DatabaseID[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					m_FactionsOnBar[i] = array[i].Entity.GUID;
				}
			}
			List<Faction> list = new List<Faction>();
			for (int j = 0; j < m_FactionsOnBar.Length; j++)
			{
				Faction faction = contentDatabase.GetFaction(m_FactionsOnBar[j]);
				if ((bool)faction)
				{
					list.Add(faction);
				}
			}
			return list.ToArray();
		}

		internal void SetFactionBar(Faction[] factions)
		{
			if (factions == null || factions.Length == 0)
			{
				m_FactionsOnBar = new DatabaseID[0];
				return;
			}
			m_FactionsOnBar = new DatabaseID[factions.Length];
			for (int i = 0; i < factions.Length; i++)
			{
				m_FactionsOnBar[i] = factions[i].Entity.GUID;
			}
		}
	}
}
