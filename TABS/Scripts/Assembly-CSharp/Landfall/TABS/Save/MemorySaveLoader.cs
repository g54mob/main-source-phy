using System.Collections.Generic;
using DM;
using Landfall.TABS.Workshop;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Save
{
	public class MemorySaveLoader : ISaveLoaderService, IService
	{
		private TABSSave currentSave;

		private SecretUnlockConditions m_secretUnlockConditions;

		public bool IsReady => true;

		public MemorySaveLoader()
		{
			currentSave = new TABSSave();
			m_secretUnlockConditions = Resources.Load<SecretUnlockConditions>("SecretUnlockConditions");
		}

		public bool HasBeatenLevel(DatabaseID lvl, DatabaseID campaignID)
		{
			return currentSave.HasBeatenLevel(lvl, campaignID);
		}

		public void AddBeatenLevel(DatabaseID lvl, DatabaseID campaignID)
		{
			if (!(lvl == default(DatabaseID)))
			{
				Debug.Log("Adding Saved Level: " + lvl);
				currentSave.AddLevel(lvl, campaignID);
			}
		}

		public void ClearLevels(List<TABSCampaignAsset> availableCampaigns)
		{
			IPlayerPrefsPlatform service = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			service.SetInt("CompletedTutorial", 0);
			service.SetInt("CompletedPossessionTutorial", 0);
			foreach (TABSCampaignAsset availableCampaign in availableCampaigns)
			{
				ClearWinStreak(availableCampaign.Entity.GUID);
			}
		}

		public void ClearSecrets()
		{
			currentSave.ClearSecrets();
		}

		public bool HasUnlockedSecret(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return true;
			}
			return currentSave.HasUnlockedSecret(key);
		}

		public List<SecretUnlockCondition> UnlockSecret(string key)
		{
			currentSave.UnlockSecret(key);
			List<SecretUnlockCondition> list = new List<SecretUnlockCondition>();
			if (m_secretUnlockConditions != null)
			{
				list = m_secretUnlockConditions.CheckUnlockCondition(currentSave.UnlockedSecrets);
				foreach (SecretUnlockCondition item in list)
				{
					currentSave.UnlockSecret(item.m_unlock);
				}
			}
			return list;
		}

		public void AddWinStreakLevel(DatabaseID lvl, DatabaseID campaignID)
		{
			currentSave.AddWinStreakLevel(lvl, campaignID);
		}

		public void ClearWinStreak(DatabaseID campaignID)
		{
			currentSave.ClearWinStreak(campaignID);
		}

		public bool HasBeatenCampaignWithoutLosing(DatabaseID campaignID)
		{
			TABSCampaignLevelAsset[] levelsInCampaign = ContentDatabase.Instance().GetCampaign(campaignID).LevelsInCampaign;
			foreach (TABSCampaignLevelAsset tABSCampaignLevelAsset in levelsInCampaign)
			{
				if (!currentSave.HasLevelInWinStreak(tABSCampaignLevelAsset.Entity.GUID, campaignID))
				{
					return false;
				}
			}
			return true;
		}

		public bool LevelIsInWinStreak(DatabaseID lvl, DatabaseID campaignID)
		{
			return currentSave.HasLevelInWinStreak(lvl, campaignID);
		}

		public void AddNonPossessedLevel(DatabaseID lvl, DatabaseID campaignID)
		{
			currentSave.AddNonPossessedLevel(lvl, campaignID);
		}

		public void ClearNonPossessedCampaign(DatabaseID campaignID)
		{
			currentSave.ClearNonPossessedCampaign(campaignID);
		}

		public bool HasBeatenNonPossessedCamapaign(DatabaseID campaignID)
		{
			TABSCampaignLevelAsset[] levelsInCampaign = ContentDatabase.Instance().GetCampaign(campaignID).LevelsInCampaign;
			foreach (TABSCampaignLevelAsset tABSCampaignLevelAsset in levelsInCampaign)
			{
				if (!currentSave.HasLevelInNonPossessedCampaign(tABSCampaignLevelAsset.Entity.GUID, campaignID))
				{
					return false;
				}
			}
			return true;
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}

		public Faction[] GetFactionBar()
		{
			return currentSave.GetFactionBar();
		}

		public void SetFactionBar(Faction[] factions)
		{
			currentSave.SetFactionBar(factions);
		}
	}
}
