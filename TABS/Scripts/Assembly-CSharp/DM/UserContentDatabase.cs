using System;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using LevelCreator;
using TFBGames;
using UnityEngine;

namespace DM
{
	public class UserContentDatabase
	{
		private enum AddResult
		{
			added = 0,
			overwritten = 1
		}

		private Dictionary<DatabaseID, UnitBlueprint> m_unitBlueprints = new Dictionary<DatabaseID, UnitBlueprint>();

		private Dictionary<DatabaseID, Faction> m_factions = new Dictionary<DatabaseID, Faction>();

		private Dictionary<DatabaseID, TABSCampaignAsset> m_campaigns = new Dictionary<DatabaseID, TABSCampaignAsset>();

		private Dictionary<DatabaseID, TABSCampaignLevelAsset> m_campaignLevels = new Dictionary<DatabaseID, TABSCampaignLevelAsset>();

		private Dictionary<DatabaseID, CustomMap> m_customMaps = new Dictionary<DatabaseID, CustomMap>();

		private AddResult Add<T>(T entity, Dictionary<DatabaseID, T> dictionary) where T : IDatabaseEntity
		{
			if (dictionary.ContainsKey(entity.Entity.GUID))
			{
				dictionary[entity.Entity.GUID] = entity;
				return AddResult.overwritten;
			}
			dictionary.Add(entity.Entity.GUID, entity);
			return AddResult.added;
		}

		public void ClearDatabase(WorkshopContentType contentType)
		{
			switch (contentType)
			{
			case WorkshopContentType.Unit:
				m_unitBlueprints.Clear();
				break;
			case WorkshopContentType.Layout:
			case WorkshopContentType.Battle:
				m_campaignLevels.Clear();
				break;
			case WorkshopContentType.Campaign:
				m_campaigns.Clear();
				break;
			case WorkshopContentType.Faction:
				m_factions.Clear();
				break;
			case WorkshopContentType.Map:
				m_customMaps.Clear();
				break;
			case WorkshopContentType.Any:
				m_unitBlueprints.Clear();
				m_campaignLevels.Clear();
				m_campaigns.Clear();
				m_customMaps.Clear();
				break;
			}
		}

		public void AddUnitBlueprint(UnitBlueprint unitBlueprint)
		{
			Add(unitBlueprint, m_unitBlueprints);
		}

		public void RemoveUnitBlueprint(DatabaseID id)
		{
			if (m_unitBlueprints.ContainsKey(id))
			{
				m_unitBlueprints.Remove(id);
				return;
			}
			Debug.LogWarningFormat("Failed to delete {0}, does not exist.", id.m_ID, id.m_modID);
		}

		public IEnumerable<UnitBlueprint> GetUnitBlueprints()
		{
			return m_unitBlueprints.Values;
		}

		public UnitBlueprint GetUnitBlueprint(DatabaseID id)
		{
			if (!m_unitBlueprints.ContainsKey(id))
			{
				return null;
			}
			return m_unitBlueprints[id];
		}

		public void AddFaction(Faction faction)
		{
			Add(faction, m_factions);
		}

		public void RemoveFaction(DatabaseID id)
		{
			if (m_factions.ContainsKey(id))
			{
				m_factions.Remove(id);
				return;
			}
			Debug.LogErrorFormat("Trying to remove non-existing user faction {0}.", id);
		}

		public IEnumerable<Faction> GetFactions()
		{
			return m_factions.Values;
		}

		public Faction GetFaction(DatabaseID id)
		{
			if (!m_factions.ContainsKey(id))
			{
				return null;
			}
			return m_factions[id];
		}

		public void AddCampaign(TABSCampaignAsset campaign)
		{
			Add(campaign, m_campaigns);
		}

		public void RemoveCampaign(DatabaseID id)
		{
			if (m_campaigns.ContainsKey(id))
			{
				m_campaigns.Remove(id);
				return;
			}
			Debug.LogErrorFormat("Trying to remove non-existing user campaign {0}.", id);
		}

		public IEnumerable<TABSCampaignAsset> GetCampaigns()
		{
			return m_campaigns.Values;
		}

		public TABSCampaignAsset GetCampaign(DatabaseID id)
		{
			if (!m_campaigns.ContainsKey(id))
			{
				return null;
			}
			return m_campaigns[id];
		}

		public void AddCampaignLevel(TABSCampaignLevelAsset campaignLevel)
		{
			if (Add(campaignLevel, m_campaignLevels) == AddResult.added)
			{
				return;
			}
			foreach (KeyValuePair<DatabaseID, TABSCampaignAsset> campaign in m_campaigns)
			{
				for (int i = 0; i < campaign.Value.LevelsInCampaign.Length; i++)
				{
					if (campaign.Value.LevelsInCampaign[i] != null && campaign.Value.LevelsInCampaign[i].Entity.GUID == campaignLevel.Entity.GUID)
					{
						campaign.Value.LevelsInCampaign[i] = campaignLevel;
					}
				}
			}
		}

		public void RemoveCampaignLevel(DatabaseID id, System.Action onDone)
		{
			if (m_campaignLevels.ContainsKey(id))
			{
				ProcessCampaignLevelRemoval(id, delegate
				{
					m_campaignLevels.Remove(id);
					onDone?.Invoke();
				});
			}
			else
			{
				Debug.LogErrorFormat("Trying to remove non-existing user campaign level {0}.", id);
				onDone?.Invoke();
			}
		}

		private void ProcessCampaignLevelRemoval(DatabaseID id, System.Action onDone)
		{
			List<CampaignChangeAsset> list = new List<CampaignChangeAsset>();
			foreach (TABSCampaignAsset campaign in GetCampaigns())
			{
				bool flag = false;
				if (campaign.LevelsInCampaign.Length == 0)
				{
					continue;
				}
				List<TABSCampaignLevelAsset> list2 = new List<TABSCampaignLevelAsset>(campaign.LevelsInCampaign);
				for (int i = 0; i < campaign.LevelsInCampaign.Length; i++)
				{
					TABSCampaignLevelAsset tABSCampaignLevelAsset = campaign.LevelsInCampaign[i];
					if (tABSCampaignLevelAsset == null)
					{
						list2.Remove(tABSCampaignLevelAsset);
					}
					else if (tABSCampaignLevelAsset != null && tABSCampaignLevelAsset.Entity.GUID == id)
					{
						list2.Remove(tABSCampaignLevelAsset);
						flag = true;
					}
				}
				if (list2.Count <= 0)
				{
					flag = false;
					list.Add(new CampaignChangeAsset
					{
						asset = campaign,
						markForDelete = true
					});
				}
				if (flag)
				{
					campaign.SetLevels(list2.ToArray());
					list.Add(new CampaignChangeAsset
					{
						asset = campaign,
						markForDelete = false
					});
				}
			}
			int count = list.Count;
			if (count <= 0)
			{
				onDone?.Invoke();
				return;
			}
			AsyncCounter counter = new AsyncCounter(count);
			for (int j = 0; j < count; j++)
			{
				CampaignChangeAsset campaignChangeAsset = list[j];
				if (campaignChangeAsset.markForDelete)
				{
					BattleCreatorSharedCommands.DeleteCampaign(campaignChangeAsset.asset, delegate
					{
						if (counter.OnAsyncDone())
						{
							onDone?.Invoke();
						}
					});
					continue;
				}
				CampaignHandler.OverwriteCampaign(campaignChangeAsset.asset.SerializeCampaign(), campaignChangeAsset.asset.FilePath, delegate
				{
					if (counter.OnAsyncDone())
					{
						onDone?.Invoke();
					}
				});
			}
		}

		public IEnumerable<TABSCampaignLevelAsset> GetCampaignLevels()
		{
			return m_campaignLevels.Values;
		}

		public TABSCampaignLevelAsset GetCampaignLevel(DatabaseID id)
		{
			if (!m_campaignLevels.ContainsKey(id))
			{
				return null;
			}
			return m_campaignLevels[id];
		}

		public void AddCustomMap(CustomMap customMap)
		{
			Add(customMap, m_customMaps);
		}

		public void RemoveCustomMap(DatabaseID id, System.Action onDone)
		{
			if (m_customMaps.ContainsKey(id))
			{
				m_customMaps.Remove(id);
			}
			ProcessCustomMapRemoval(id, onDone);
		}

		private void ProcessCustomMapRemoval(DatabaseID id, System.Action onDone)
		{
			if (id == default(DatabaseID))
			{
				onDone?.Invoke();
				return;
			}
			List<DatabaseID> list = new List<DatabaseID>();
			foreach (TABSCampaignLevelAsset campaignLevel in GetCampaignLevels())
			{
				if (campaignLevel != null && campaignLevel.CustomMap == id)
				{
					list.Add(campaignLevel.Entity.GUID);
				}
			}
			foreach (DatabaseID item in list)
			{
				string folderPath = CustomContentFilePaths.FilePathLayout + item;
				BattleCreatorSharedCommands.DeleteContentFolder(new CustomContentDataPackage(item, folderPath, ContentTypeFilter.Battles), null);
			}
			onDone?.Invoke();
		}

		public CustomMap GetCustomMap(DatabaseID id)
		{
			if (!m_customMaps.ContainsKey(id))
			{
				return null;
			}
			return m_customMaps[id];
		}

		public IEnumerable<CustomMap> GetCustomMaps()
		{
			return m_customMaps.Values;
		}
	}
}
