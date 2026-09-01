using System;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using Landfall.TABS.Workshop;
using LevelCreator;
using ModIO;
using UnityEngine;

namespace DM
{
	public class ContentDatabase
	{
		private static ContentDatabase m_instance;

		public AssetLoader AssetLoader { get; private set; }

		public LandfallContentDatabase LandfallContentDatabase { get; private set; }

		public UserContentDatabase UserContentDatabase { get; private set; }

		private ContentDatabase()
		{
			NonStreamableDatabaseAssets nonStreamableDatabaseAssets = BuildDatabase.GetNonStreamableDatabaseAssets();
			AssetLoader = new AssetLoader(nonStreamableDatabaseAssets.BuildDateTimeString, nonStreamableDatabaseAssets.Objects);
			LandfallContentDatabase = new LandfallContentDatabase(nonStreamableDatabaseAssets, AssetLoader);
			UserContentDatabase = new UserContentDatabase();
			Debug.Log("ContentDatabase constructed");
		}

		public static ContentDatabase Instance()
		{
			if (m_instance == null)
			{
				m_instance = new ContentDatabase();
			}
			return m_instance;
		}

		public GameObject GetGameObject(DatabaseID databaseId)
		{
			return AssetLoader.GetAsset<GameObject>(databaseId);
		}

		public GameObject[] GetGameObjects(DatabaseID[] databaseIds)
		{
			return databaseIds.Select(AssetLoader.GetAsset<GameObject>).ToArray();
		}

		private IEnumerable<T> FilteredOnNamePart<T>(IEnumerable<T> databaseEntities, string namePart, bool onlyExactNameMatch) where T : IDatabaseEntity
		{
			string lowercaseNamePart = namePart?.ToLower();
			if (onlyExactNameMatch)
			{
				return databaseEntities.Where(delegate(T databaseEntity)
				{
					string name = databaseEntity.Entity.Name;
					return name != null && name.ToLower()?.Equals(lowercaseNamePart) == true;
				});
			}
			if (string.IsNullOrEmpty(lowercaseNamePart))
			{
				return databaseEntities;
			}
			return databaseEntities.Where(delegate(T databaseEntity)
			{
				string name = databaseEntity.Entity.Name;
				return name != null && name.ToLower()?.Contains(lowercaseNamePart) == true;
			});
		}

		public void ClearUserContent(WorkshopContentType contentType)
		{
			UserContentDatabase.ClearDatabase(contentType);
		}

		public IEnumerable<MapAsset> GetAllMapAssetsOrdered()
		{
			return LandfallContentDatabase.GetMapAssetsOrdered();
		}

		public MapAsset GetMapAsset(DatabaseID id)
		{
			return LandfallContentDatabase.GetMapAsset(id);
		}

		public int GetMapAssetCount()
		{
			return GetAllMapAssetsOrdered().Count();
		}

		public MapAsset GetMapAssetByIndex(int index)
		{
			return LandfallContentDatabase.GetMapAssetByIndex(index);
		}

		public IEnumerable<MapAsset> GetMapAssetsByType(MapAsset.MapType mapType, bool onlyUnlocked)
		{
			return from mapAsset in GetAllMapAssetsOrdered()
				where mapAsset.m_MapType == mapType
				where !onlyUnlocked || !mapAsset.m_isSecret || ServiceLocator.GetService<ISaveLoaderService>().HasUnlockedSecret(mapAsset.Entity.UnlockKey)
				select mapAsset;
		}

		public MapAsset GetMapAssetByTypeAndMapIndex(MapAsset.MapType mapType, int mapIndex)
		{
			return (from map in GetMapAssetsByType(mapType, onlyUnlocked: false)
				where map.m_mapIndex == mapIndex
				select map).FirstOrDefault();
		}

		public void AddUserUnitBlueprint(UnitBlueprint unitBlueprint)
		{
			UserContentDatabase.AddUnitBlueprint(unitBlueprint);
		}

		public void RemoveUserUnitBlueprintAndEmptyFactionsCreated(DatabaseID id)
		{
			UnitBlueprint unitBlueprint = GetUnitBlueprint(id);
			List<DatabaseID> list = new List<DatabaseID>();
			foreach (Faction item in GetFactionsByUnitBlueprint(id))
			{
				Debug.Log("removing unit \"" + unitBlueprint.Name + "\" from faction \"" + item.name + "\"");
				item.RemoveUnit(unitBlueprint);
				if (item.Units.Length == 0)
				{
					list.Add(item.Entity.GUID);
				}
			}
			foreach (DatabaseID item2 in list)
			{
				string folderPath = CustomContentFilePaths.FilePathFaction + item2;
				BattleCreatorSharedCommands.DeleteContentFolder(new CustomContentDataPackage(item2, folderPath, ContentTypeFilter.Factions), null);
			}
			UserContentDatabase.RemoveUnitBlueprint(id);
		}

		public IEnumerable<UnitBlueprint> GetAllUnitBlueprints()
		{
			return LandfallContentDatabase.GetUnitBlueprints().Concat(UserContentDatabase.GetUnitBlueprints());
		}

		public UnitBlueprint GetLandfallUnitBlueprint(string name)
		{
			return (from x in GetAllUnitBlueprints()
				where x.Entity.Name == name
				select x).FirstOrDefault();
		}

		public UnitBlueprint GetUnitBlueprint(DatabaseID id)
		{
			UnitBlueprint unitBlueprint = LandfallContentDatabase.GetUnitBlueprint(id);
			if (unitBlueprint != null)
			{
				return unitBlueprint;
			}
			return UserContentDatabase.GetUnitBlueprint(id);
		}

		public IEnumerable<UnitBlueprint> GetUnitBlueprintsByIds(IEnumerable<DatabaseID> ids)
		{
			return ids.Select(GetUnitBlueprint);
		}

		public IEnumerable<UnitBlueprint> GetUserUnitBlueprints()
		{
			return UserContentDatabase.GetUnitBlueprints();
		}

		public IEnumerable<UnitBlueprint> GetUserUnitBlueprintsByOnEnabled(bool onlyEnabled)
		{
			List<int> enabledMods = LocalUser.EnabledModIds;
			return from unitBlueprint in GetUserUnitBlueprints()
				where !onlyEnabled || enabledMods.Contains(unitBlueprint.ModID) || !unitBlueprint.IsModUnit
				select unitBlueprint;
		}

		public IEnumerable<UnitBlueprint> GetUserUnitBlueprintsByFilter(Filter filter)
		{
			IEnumerable<UnitBlueprint> enumerable = FilteredOnNamePart(GetUserUnitBlueprints(), filter.NamePart, filter.ExactNameMatch);
			List<int> enabledMods = LocalUser.EnabledModIds;
			switch (filter.WorkshopTypeFilter)
			{
			case WorkshopTypeFilter.Local:
				return enumerable.Where((UnitBlueprint unitBlueprint) => !unitBlueprint.IsModUnit);
			case WorkshopTypeFilter.Workshop:
				return enumerable.Where((UnitBlueprint unitBlueprint) => unitBlueprint.IsModUnit && enabledMods.Contains(unitBlueprint.ModID));
			case WorkshopTypeFilter.WorkshopSelf:
				return enumerable.Where((UnitBlueprint unitBlueprint) => unitBlueprint.LocalClientOwnedCustomContent);
			case WorkshopTypeFilter.AllWorkshop:
				return enumerable.Where((UnitBlueprint unitBlueprint) => unitBlueprint.IsModUnit || unitBlueprint.LocalClientOwnedCustomContent);
			default:
				return enumerable;
			}
		}

		public IEnumerable<UnitBlueprint> GetUserUnitBlueprintsByNamePartAndType(string namePart, WorkshopTypeFilter type)
		{
			return GetUserUnitBlueprintsByFilter(Filter.CreateMatchNamePartAndTypeFilter(namePart, type));
		}

		public UnitBlueprint GetUserUnitBlueprintByExactName(string name)
		{
			return GetUserUnitBlueprintsByFilter(Filter.CreateMatchLocalAndExactNameFilter(name)).FirstOrDefault();
		}

		public IEnumerable<UnitBlueprint> GetUserUnitBlueprintsByIdExcluded(DatabaseID id)
		{
			return from unitBlueprint in GetUserUnitBlueprints()
				where unitBlueprint.Entity.GUID != id
				select unitBlueprint;
		}

		public UnitBlueprint GetUserUnitBlueprintByUnlockKey(string unlockKey)
		{
			return (from unitBlueprint in GetUserUnitBlueprints()
				where unitBlueprint.Entity.UnlockKey?.Equals(unlockKey) ?? false
				select unitBlueprint).FirstOrDefault();
		}

		public void AddUserFaction(Faction faction)
		{
			UserContentDatabase.AddFaction(faction);
		}

		public void RemoveUserFaction(DatabaseID id)
		{
			UserContentDatabase.RemoveFaction(id);
		}

		public IEnumerable<Faction> GetAllFactions()
		{
			return LandfallContentDatabase.GetFactions().Concat(UserContentDatabase.GetFactions());
		}

		public Faction GetFaction(DatabaseID id)
		{
			Faction faction = LandfallContentDatabase.GetFaction(id);
			if (faction != null)
			{
				return faction;
			}
			return UserContentDatabase.GetFaction(id);
		}

		public IEnumerable<Faction> GetFactionsByIds(IEnumerable<DatabaseID> ids)
		{
			return ids.Select(GetFaction);
		}

		public Faction GetFactionByName(string factionName)
		{
			return GetAllFactions().FirstOrDefault((Faction faction) => faction.Entity.Name == factionName);
		}

		public IEnumerable<Faction> GetFactionsByUnitBlueprint(DatabaseID unitId)
		{
			return from faction in GetAllFactions()
				where faction.DoesIncludeUnit(unitId)
				select faction;
		}

		public Faction GetFactionByUnitBlueprint(DatabaseID unitId)
		{
			return GetFactionsByUnitBlueprint(unitId).FirstOrDefault();
		}

		public IEnumerable<Faction> GetFactionsOrderedByIndex()
		{
			return from faction in GetAllFactions()
				orderby faction.index
				select faction;
		}

		public IEnumerable<Faction> GetDisplayedFactionsOrderedByIndex()
		{
			return from faction in GetAllFactions()
				where faction.m_displayFaction
				orderby faction.index
				select faction;
		}

		public IEnumerable<Faction> GetFactionsByUnit(DatabaseID unitId)
		{
			return from faction in GetAllFactions()
				where faction.Units.Any((UnitBlueprint unit) => unit.Entity.GUID == unitId)
				select faction;
		}

		public IEnumerable<Faction> GetUserFactions()
		{
			return UserContentDatabase.GetFactions();
		}

		public IEnumerable<Faction> GetUserFactionsByOnEnabled(bool onlyEnabled)
		{
			List<int> enabledMods = LocalUser.EnabledModIds;
			return from faction in GetUserFactions()
				where !onlyEnabled || enabledMods.Contains(faction.modID) || !faction.IsModFaction
				select faction;
		}

		public IEnumerable<Faction> GetUserFactionsByFilter(Filter filter)
		{
			IEnumerable<Faction> enumerable = FilteredOnNamePart(GetUserFactions(), filter.NamePart, filter.ExactNameMatch);
			List<int> enabledMods = LocalUser.EnabledModIds;
			switch (filter.WorkshopTypeFilter)
			{
			case WorkshopTypeFilter.Local:
				return enumerable.Where((Faction faction) => !faction.IsModFaction);
			case WorkshopTypeFilter.Workshop:
				return enumerable.Where((Faction faction) => faction.IsModFaction && enabledMods.Contains(faction.modID));
			case WorkshopTypeFilter.All:
				return enumerable;
			default:
				throw new Exception("Not implemented, missing LocalClientOwnedCustomContent field");
			}
		}

		public IEnumerable<Faction> GetUserFactionsByNamePartAndType(string namePart, WorkshopTypeFilter type)
		{
			return GetUserFactionsByFilter(Filter.CreateMatchNamePartAndTypeFilter(namePart, type));
		}

		public IEnumerable<Faction> GetUserFactionsByNamePart(string namePart)
		{
			return GetUserFactionsByFilter(Filter.CreateMatchNamePartFilter(namePart));
		}

		public void AddUserCampaign(TABSCampaignAsset campaign)
		{
			UserContentDatabase.AddCampaign(campaign);
		}

		public void RemoveUserCampaign(DatabaseID id)
		{
			UserContentDatabase.RemoveCampaign(id);
		}

		public TABSCampaignAsset GetCampaign(DatabaseID id)
		{
			TABSCampaignAsset campaign = LandfallContentDatabase.GetCampaign(id);
			if (campaign != null)
			{
				return campaign;
			}
			return UserContentDatabase.GetCampaign(id);
		}

		public bool HasCampaign(DatabaseID id)
		{
			return GetCampaign(id) != null;
		}

		public IEnumerable<TABSCampaignAsset> GetUserCampaigns()
		{
			return UserContentDatabase.GetCampaigns();
		}

		public IEnumerable<TABSCampaignAsset> GetUserCampaignsByOnEnabled(bool onlyEnabled)
		{
			List<int> enabledMods = LocalUser.EnabledModIds;
			return from campaign in GetUserCampaigns()
				where !onlyEnabled || enabledMods.Contains(campaign.ModID) || !campaign.IsModCampaign
				select campaign;
		}

		public IEnumerable<TABSCampaignAsset> GetUserCampaignsByFilter(Filter filter)
		{
			IEnumerable<TABSCampaignAsset> enumerable = FilteredOnNamePart(GetUserCampaigns(), filter.NamePart, filter.ExactNameMatch);
			List<int> enabledMods = LocalUser.EnabledModIds;
			switch (filter.WorkshopTypeFilter)
			{
			case WorkshopTypeFilter.Local:
				return enumerable.Where((TABSCampaignAsset campaign) => !campaign.IsModCampaign);
			case WorkshopTypeFilter.Workshop:
				return enumerable.Where((TABSCampaignAsset campaign) => campaign.IsModCampaign && enabledMods.Contains(campaign.ModID));
			case WorkshopTypeFilter.WorkshopSelf:
				return enumerable.Where((TABSCampaignAsset campaign) => campaign.LocalClientOwnedCustomContent);
			case WorkshopTypeFilter.AllWorkshop:
				return enumerable.Where((TABSCampaignAsset campaign) => campaign.IsModCampaign || campaign.LocalClientOwnedCustomContent);
			default:
				return enumerable;
			}
		}

		public TABSCampaignAsset GetUserLocalCampaignByExactName(string campaignName)
		{
			return GetUserCampaignsByFilter(Filter.CreateMatchLocalAndExactNameFilter(campaignName)).FirstOrDefault();
		}

		public IEnumerable<TABSCampaignAsset> GetAllCampaigns()
		{
			return LandfallContentDatabase.GetCampaigns();
		}

		public IEnumerable<TABSCampaignLevelAsset> GetAllLandfallCampaignLevels()
		{
			return LandfallContentDatabase.GetCampaignLevels();
		}

		public void AddUserCampaignLevel(TABSCampaignLevelAsset campaignLevel)
		{
			UserContentDatabase.AddCampaignLevel(campaignLevel);
		}

		public void RemoveUserCampaignLevel(DatabaseID id, System.Action onDone)
		{
			UserContentDatabase.RemoveCampaignLevel(id, onDone);
		}

		public TABSCampaignLevelAsset GetCampaignLevel(DatabaseID id)
		{
			TABSCampaignLevelAsset campaignLevel = LandfallContentDatabase.GetCampaignLevel(id);
			if (campaignLevel != null)
			{
				return campaignLevel;
			}
			return UserContentDatabase.GetCampaignLevel(id);
		}

		public bool HasCampaignLevel(DatabaseID id)
		{
			return GetCampaignLevel(id) != null;
		}

		public IEnumerable<TABSCampaignLevelAsset> GetUserCampaignLevels()
		{
			return UserContentDatabase.GetCampaignLevels();
		}

		public IEnumerable<TABSCampaignLevelAsset> GetUserCampaignLevelsByOnEnabled(bool onlyEnabled)
		{
			List<int> enabledMods = LocalUser.EnabledModIds;
			return from campaign in GetUserCampaignLevels()
				where !onlyEnabled || enabledMods.Contains(campaign.ModID) || !campaign.IsModIOLevel
				select campaign;
		}

		public IEnumerable<TABSCampaignLevelAsset> GetUserCampaignLevelsByFilter(Filter filter)
		{
			IEnumerable<TABSCampaignLevelAsset> enumerable = FilteredOnNamePart(GetUserCampaignLevels(), filter.NamePart, filter.ExactNameMatch);
			List<int> enabledMods = LocalUser.EnabledModIds;
			switch (filter.WorkshopTypeFilter)
			{
			case WorkshopTypeFilter.Local:
				return enumerable.Where((TABSCampaignLevelAsset level) => !level.IsModIOLevel);
			case WorkshopTypeFilter.Workshop:
				return enumerable.Where((TABSCampaignLevelAsset level) => level.IsModIOLevel && enabledMods.Contains(level.ModID));
			case WorkshopTypeFilter.WorkshopSelf:
				return enumerable.Where((TABSCampaignLevelAsset level) => level.LocalClientOwnedCustomContent);
			case WorkshopTypeFilter.AllWorkshop:
				return enumerable.Where((TABSCampaignLevelAsset level) => level.IsModIOLevel || level.LocalClientOwnedCustomContent);
			default:
				return enumerable;
			}
		}

		public TABSCampaignLevelAsset GetUserCampaignLevelByExactNameAndType(string campaignLevelName, WorkshopTypeFilter type)
		{
			if (type == WorkshopTypeFilter.Local)
			{
				return GetUserCampaignLevelsByFilter(Filter.CreateMatchLocalAndExactNameFilter(campaignLevelName)).FirstOrDefault();
			}
			return null;
		}

		public void AddUserMap(CustomMap customMap)
		{
			UserContentDatabase.AddCustomMap(customMap);
		}

		public void RemoveUserMap(DatabaseID id, System.Action onDone)
		{
			UserContentDatabase.RemoveCustomMap(id, onDone);
		}

		public CustomMap GetUserMap(DatabaseID id)
		{
			return UserContentDatabase.GetCustomMap(id);
		}

		public IEnumerable<CustomMap> GetUserMaps()
		{
			return UserContentDatabase.GetCustomMaps();
		}

		public IEnumerable<CustomMap> GetUserMapsByOnEnabled(bool onlyEnabled)
		{
			List<int> enabledMods = LocalUser.EnabledModIds;
			return from map in GetUserMaps()
				where !onlyEnabled || enabledMods.Contains(map.ModID) || !map.IsModLevel()
				select map;
		}

		public IEnumerable<CustomMap> GetUserMapsByFilter(Filter filter)
		{
			IEnumerable<CustomMap> enumerable = FilteredOnNamePart(GetUserMaps(), filter.NamePart, filter.ExactNameMatch);
			List<int> enabledMods = LocalUser.EnabledModIds;
			switch (filter.WorkshopTypeFilter)
			{
			case WorkshopTypeFilter.Local:
				return enumerable.Where((CustomMap map) => !map.IsModLevel());
			case WorkshopTypeFilter.Workshop:
				return enumerable.Where((CustomMap map) => map.IsModLevel() && enabledMods.Contains(map.ModID));
			case WorkshopTypeFilter.WorkshopSelf:
				return enumerable.Where((CustomMap map) => map.LocalClientOwnedCustomContent);
			case WorkshopTypeFilter.AllWorkshop:
				return enumerable.Where((CustomMap map) => map.IsModLevel() || map.LocalClientOwnedCustomContent);
			default:
				return enumerable;
			}
		}

		public CustomMap GetUserMapByExactNameAndType(string customMapName, WorkshopTypeFilter type)
		{
			if (type == WorkshopTypeFilter.Local)
			{
				return GetUserMapsByFilter(Filter.CreateMatchLocalAndExactNameFilter(customMapName)).FirstOrDefault();
			}
			return null;
		}

		public TurningData GetTurningData(DatabaseID id)
		{
			return LandfallContentDatabase.GetTurningData(id);
		}

		public IEnumerable<GameObject> GetAllUnitBases()
		{
			return LandfallContentDatabase.GetUnitBases();
		}

		public IEnumerable<GameObject> GetAllWeapons()
		{
			return LandfallContentDatabase.GetWeapons();
		}

		public GameObject GetWeapon(DatabaseID id)
		{
			return LandfallContentDatabase.GetWeapon(id);
		}

		public IEnumerable<WeaponItem> GetEditorVisibleWeaponItemsOfType<T>() where T : Weapon
		{
			return from gameObject in GetAllWeapons()
				where gameObject.GetComponent<T>() != null
				select gameObject.GetComponent<WeaponItem>() into weaponItem
				where weaponItem != null && weaponItem.ShowInEditor
				select weaponItem;
		}

		public IEnumerable<GameObject> GetAllCombatMoves()
		{
			return LandfallContentDatabase.GetCombatMoves();
		}

		public IEnumerable<SpecialAbility> GetEditorVisibleSpecialAbilities()
		{
			return from gameObject in GetAllCombatMoves()
				select gameObject.GetComponent<SpecialAbility>() into specialAbility
				where specialAbility != null && specialAbility.ShowInEditor
				select specialAbility;
		}

		public IEnumerable<GameObject> GetAllCharacterProps()
		{
			return LandfallContentDatabase.GetCharacterProps();
		}

		public GameObject GetCharacterProp(DatabaseID id)
		{
			return LandfallContentDatabase.GetCharacterProp(id);
		}

		public IEnumerable<CharacterItem> GetEditorVisibleCharacterItemsOfType(UnitRig.GearType gearType)
		{
			return from gameObject in GetAllCharacterProps()
				select gameObject.GetComponent<CharacterItem>() into characterProp
				where characterProp != null && characterProp.GearT == gearType && characterProp.ShowInEditor
				select characterProp;
		}

		public IEnumerable<GameObject> GetAllProjectiles()
		{
			return LandfallContentDatabase.GetProjectiles();
		}

		public GameObject GetProjectile(DatabaseID id)
		{
			return LandfallContentDatabase.GetProjectile(id);
		}

		public IEnumerable<VoiceBundle> GetAllVoiceBundles()
		{
			return LandfallContentDatabase.GetVoiceBundles();
		}

		public VoiceBundle GetVoiceBundle(DatabaseID id)
		{
			return LandfallContentDatabase.GetVoiceBundle(id);
		}

		public IEnumerable<Faction> GetDefaultHotbarFactions()
		{
			return LandfallContentDatabase.GetDefaultHotbarFactionIds().Select(AssetLoader.GetAsset<Faction>);
		}

		public IEnumerable<FactionIcon> GetFactionIcons()
		{
			return LandfallContentDatabase.GetFactionIconIds().Select(AssetLoader.GetAsset<FactionIcon>);
		}

		public FactionIcon GetFactionIcon(DatabaseID id)
		{
			return AssetLoader.GetAsset<FactionIcon>(id);
		}

		public DatabaseID GetDefaultVoiceBundleId()
		{
			return LandfallContentDatabase.GetDefaultVoiceBundleId();
		}

		public SerializedHelpingData GetHelpingData()
		{
			return LandfallContentDatabase.GetHelpingData();
		}

		public CustomFactionColorDatabase GetCustomFactionColorDatabase()
		{
			return LandfallContentDatabase.GetCustomFactionColorDatabase();
		}

		public List<string> GetMainMenuScenes()
		{
			return LandfallContentDatabase.GetMainMenuScenes();
		}

		public UnitEditorColorPalette GetUnitEditorColorPalette()
		{
			return LandfallContentDatabase.GetUnitEditorColorPalette();
		}

		public string GetVersion()
		{
			return LandfallContentDatabase.GetVersion();
		}

		public UpgradeDataAsset GetUpgradeDataAsset()
		{
			return LandfallContentDatabase.GetUpgradeDataAsset();
		}

		public UnitBlueprint GetUnitEditorBlueprint()
		{
			return LandfallContentDatabase.GetUnitEditorBlueprint();
		}
	}
}
