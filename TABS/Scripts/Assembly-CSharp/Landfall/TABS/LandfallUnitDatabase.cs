using System;
using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS.UnitEditor;
using Landfall.TABS.Workshop;
using UnityEngine;
using UnityEngine.Serialization;

namespace Landfall.TABS
{
	[CreateAssetMenu(fileName = "Landfall Unit Database", menuName = "TABS/LandfallUnitDatabase", order = 1)]
	public class LandfallUnitDatabase : ScriptableObject
	{
		public string m_version = "0.1.0";

		[SerializeField]
		private int m_earlyAccesFactions = 20;

		[SerializeField]
		private int m_earlyAccessCampaigns = 20;

		[SerializeField]
		private List<UnitBlueprint> Units = new List<UnitBlueprint>();

		[SerializeField]
		private List<Faction> Factions = new List<Faction>();

		[SerializeField]
		private List<GameObject> UnitBases = new List<GameObject>();

		[SerializeField]
		private List<GameObject> CombatMoves = new List<GameObject>();

		[SerializeField]
		private List<GameObject> CharacterProps = new List<GameObject>();

		[SerializeField]
		private List<GameObject> Weapons = new List<GameObject>();

		[SerializeField]
		private List<GameObject> Projectiles = new List<GameObject>();

		[SerializeField]
		private List<TurningData> TurningDatas = new List<TurningData>();

		[SerializeField]
		private List<MapAsset> Maps = new List<MapAsset>();

		[SerializeField]
		private List<TABSCampaignAsset> Campaigns = new List<TABSCampaignAsset>();

		[SerializeField]
		private List<TABSCampaignLevelAsset> CampaignLevels = new List<TABSCampaignLevelAsset>();

		[SerializeField]
		private List<VoiceBundle> VoiceBundles = new List<VoiceBundle>();

		[SerializeField]
		private List<Faction> DefaultHotbarFactions;

		[SerializeField]
		private List<FactionIcon> FactionIcons;

		[SerializeField]
		private SerializedHelpingData HelpingData;

		[SerializeField]
		[FormerlySerializedAs("CustomFactionColorDatabase")]
		private CustomFactionColorDatabase m_customFactionColorDatabase;

		[HideInInspector]
		[FormerlySerializedAs("MainMenuScenes")]
		public List<string> m_mainMenuScenes;

		public static readonly string unitPath = "Assets/2 Units/";

		public static readonly string factionsPath = "Assets/2 Units/Factions/";

		public static readonly string unitBasePath = "Assets/1 Prefabs/0 UnitBases/";

		public static readonly string combatMovesPath = "Assets/1 Prefabs/4 Moves/";

		public static readonly string propPath = "Assets/1 Prefabs/8 CharacterProps/";

		public static readonly string weaponPath = "Assets/1 Prefabs/1 Weapons/";

		public static readonly string projectilePath = "Assets/1 Prefabs/2 Projectiles/";

		public static readonly string turningDataPath = "Assets/2 Units/Data/";

		public static readonly string iconsPath = "Assets/2 Units/Icons/";

		public static readonly string mapsPath = "Assets/12 Maps/";

		public static readonly string campaignsPath = "Assets/13 Campaigns/";

		public static readonly string bakedAssetCachePath = "Assets/VertexBake/Cache/BakeCache.asset";

		public static readonly string voiceBundlesPath = "Assets/10 Audio/VoiceBundles/";

		public static readonly string factionIconsPath = "Assets/8 Data/FactionIcons";

		public static readonly string mainMenuScenePath = "Assets/11 Scenes/MainMenuScenes/";

		public UnitEditorColorPalette colorPalette;

		public UnitBlueprint m_unitEditorBlueprint;

		public GameObject defaultUnitBase;

		public TurningData defaultTurningData;

		[SerializeField]
		private UpgradeDataAsset m_upgradeDataAsset;

		[SerializeField]
		private VoiceBundle defaultVoiceBundle;

		private static LandfallUnitDatabase unitDatabase;

		public CustomFactionColorDatabase CustomFactionColorDatabase => ContentDatabase.Instance().GetCustomFactionColorDatabase();

		public List<string> MainMenuScenes => ContentDatabase.Instance().GetMainMenuScenes();

		public IEnumerable<IDatabaseEntity> UnitList => ContentDatabase.Instance().GetAllUnitBlueprints();

		public IEnumerable<IDatabaseEntity> FactionList => ContentDatabase.Instance().GetAllFactions();

		public IEnumerable<GameObject> UnitBaseList => ContentDatabase.Instance().GetAllUnitBases();

		public IEnumerable<GameObject> WeaponList => ContentDatabase.Instance().GetAllWeapons();

		public IEnumerable<GameObject> CombatMoveList => ContentDatabase.Instance().GetAllCombatMoves();

		public IEnumerable<GameObject> CharacterPropList => ContentDatabase.Instance().GetAllCharacterProps();

		public VoiceBundle[] VoiceBundlesList => ContentDatabase.Instance().GetAllVoiceBundles().ToArray();

		public FactionIcon[] FactionIconsList => ContentDatabase.Instance().GetFactionIcons().ToArray();

		public List<MapAsset> MapList => ContentDatabase.Instance().GetAllMapAssetsOrdered().ToList();

		public List<TABSCampaignAsset> LandfallCampaignList => ContentDatabase.Instance().GetAllCampaigns().ToList();

		public List<TABSCampaignLevelAsset> LandfallCampaignLevelList => CampaignLevels;

		public UpgradeDataAsset UpgradeData => m_upgradeDataAsset;

		private static LandfallUnitDatabase GetDatabaseInternal()
		{
			if (unitDatabase == null)
			{
				unitDatabase = Resources.Load<LandfallUnitDatabase>("Landfall Unit Database");
			}
			return unitDatabase;
		}

		public static LandfallUnitDatabase GetDatabase()
		{
			if (Application.isPlaying)
			{
				Debug.LogError("Old database loaded! Remove the call");
			}
			return GetDatabaseInternal();
		}

		public void AddUnitWithID(UnitBlueprint unit)
		{
			ContentDatabase.Instance().AddUserUnitBlueprint(unit);
		}

		public void AddFactionWithID(Faction faction)
		{
			ContentDatabase.Instance().AddUserFaction(faction);
		}

		public void AddCampaignWithID(TABSCampaignAsset campaign)
		{
			ContentDatabase.Instance().AddUserCampaign(campaign);
		}

		public void AddCampaignLevelWithID(TABSCampaignLevelAsset campaignLevel)
		{
			ContentDatabase.Instance().AddUserCampaignLevel(campaignLevel);
		}

		public TABSCampaignAsset[] GetCustomCampaignsContainingLevel(TABSCampaignLevelAsset levelAsset)
		{
			TABSCampaignAsset[] customCampaigns = GetCustomCampaigns();
			if (customCampaigns != null && customCampaigns.Length != 0)
			{
				return customCampaigns.Where((TABSCampaignAsset c) => c.LevelsInCampaign.Contains(levelAsset)).ToArray();
			}
			return null;
		}

		public void RemoveFactionWithGUID(DatabaseID id)
		{
			ContentDatabase.Instance().RemoveUserFaction(id);
		}

		public void RemoveCampaignLevelWithGUID(DatabaseID id, Action onDone)
		{
			if (ContentDatabase.Instance().HasCampaignLevel(id))
			{
				ContentDatabase.Instance().RemoveUserCampaignLevel(id, onDone);
				return;
			}
			Debug.LogError("Trying to remove non-existing campaignlevel with id: " + id);
			onDone?.Invoke();
		}

		public DatabaseID GetDefaultVoiceBundle()
		{
			return ContentDatabase.Instance().GetDefaultVoiceBundleId();
		}

		public void RemoveCampaignWithGUID(DatabaseID id)
		{
			ContentDatabase.Instance().RemoveUserCampaign(id);
		}

		public void RemoveUnitWithGUID(DatabaseID id)
		{
			ContentDatabase.Instance().RemoveUserUnitBlueprintAndEmptyFactionsCreated(id);
		}

		public UnitBlueprint GetUnitByGUID(DatabaseID id)
		{
			return ContentDatabase.Instance().GetUnitBlueprint(id);
		}

		public UnitBlueprint GetUnitByIDs(int id, int modId)
		{
			return ContentDatabase.Instance().GetUnitBlueprint(new DatabaseID
			{
				m_ID = id,
				m_modID = modId
			});
		}

		public UnitBlueprint GetUnitFromUnlockKey(string unlockKey)
		{
			return ContentDatabase.Instance().GetUserUnitBlueprintByUnlockKey(unlockKey);
		}

		public UnitBlueprint GetUnitByName(string unitName)
		{
			return ContentDatabase.Instance().GetUserUnitBlueprintByExactName(unitName);
		}

		public Faction GetFactionByGUID(DatabaseID id)
		{
			return ContentDatabase.Instance().GetFaction(id);
		}

		public VoiceBundle GetVoiceBundle(DatabaseID voiceBundleId)
		{
			return ContentDatabase.Instance().GetVoiceBundle(voiceBundleId);
		}

		public FactionIcon GetFactionIcon(DatabaseID iconID)
		{
			return ContentDatabase.Instance().GetFactionIcon(iconID);
		}

		public Faction GetFactionByUnit(UnitBlueprint blueprint)
		{
			return ContentDatabase.Instance().GetFactionByUnitBlueprint(blueprint.Entity.GUID);
		}

		public Faction[] GetFactionsByUnit(DatabaseID unit)
		{
			return ContentDatabase.Instance().GetFactionsByUnit(unit).ToArray();
		}

		public bool HasCampaign(DatabaseID id)
		{
			return ContentDatabase.Instance().HasCampaign(id);
		}

		public TABSCampaignLevelAsset GetCampaignLevelByName(string levelName, WorkshopTypeFilter type)
		{
			return ContentDatabase.Instance().GetUserCampaignLevelByExactNameAndType(levelName, type);
		}

		public TABSCampaignAsset GetCampaignByName(string campaignName, WorkshopTypeFilter type)
		{
			if (type != WorkshopTypeFilter.Local)
			{
				throw new Exception("Not implemented, WorkshopTypeFilter.Local should be local");
			}
			return ContentDatabase.Instance().GetUserLocalCampaignByExactName(campaignName);
		}

		public TABSCampaignAsset GetCampaignByGUID(DatabaseID id)
		{
			return ContentDatabase.Instance().GetCampaign(id);
		}

		public bool HasCampaignLevel(DatabaseID id)
		{
			return ContentDatabase.Instance().HasCampaignLevel(id);
		}

		public TABSCampaignLevelAsset GetCampaignLevelByGUID(DatabaseID id)
		{
			return ContentDatabase.Instance().GetCampaignLevel(id);
		}

		public UnitBlueprint[] GetCustomUnits(string nameFilter)
		{
			return ContentDatabase.Instance().GetUserUnitBlueprintsByNamePartAndType(nameFilter, WorkshopTypeFilter.All).ToArray();
		}

		public UnitBlueprint[] GetCustomUnits(string nameFilter, WorkshopTypeFilter type)
		{
			return ContentDatabase.Instance().GetUserUnitBlueprintsByNamePartAndType(nameFilter, type).ToArray();
		}

		public UnitBlueprint[] GetCustomUnits(DatabaseID id)
		{
			return ContentDatabase.Instance().GetUserUnitBlueprintsByIdExcluded(id).ToArray();
		}

		public UnitBlueprint[] GetCustomUnits(bool excludeDisabled = true)
		{
			return ContentDatabase.Instance().GetUserUnitBlueprintsByOnEnabled(excludeDisabled).ToArray();
		}

		public TABSCampaignAsset[] GetCustomCampaigns(bool excludeDisabled = true)
		{
			return ContentDatabase.Instance().GetUserCampaignsByOnEnabled(excludeDisabled).ToArray();
		}

		public TABSCampaignAsset[] GetCustomCampaigns(string nameFilter, WorkshopTypeFilter type)
		{
			return ContentDatabase.Instance().GetUserCampaignsByFilter(new Filter
			{
				NamePart = nameFilter,
				WorkshopTypeFilter = type
			}).ToArray();
		}

		public TABSCampaignAsset[] GetCustomCampaigns(WorkshopTypeFilter type)
		{
			return ContentDatabase.Instance().GetUserCampaignsByFilter(new Filter
			{
				WorkshopTypeFilter = type
			}).ToArray();
		}

		public TABSCampaignLevelAsset[] GetCustomCampaignLevels(bool excludeDisabled = true)
		{
			return ContentDatabase.Instance().GetUserCampaignLevelsByOnEnabled(excludeDisabled).ToArray();
		}

		public TABSCampaignLevelAsset[] GetCustomCampaignLevels(string nameFilter, WorkshopTypeFilter type)
		{
			return ContentDatabase.Instance().GetUserCampaignLevelsByFilter(new Filter
			{
				NamePart = nameFilter,
				WorkshopTypeFilter = type
			}).ToArray();
		}

		public TABSCampaignLevelAsset[] GetCustomCampaignLevels(WorkshopTypeFilter type)
		{
			return ContentDatabase.Instance().GetUserCampaignLevelsByFilter(new Filter
			{
				WorkshopTypeFilter = type
			}).ToArray();
		}

		public List<MapAsset> GetLevelsByType(MapAsset.MapType type, bool onlyUnlocked = true)
		{
			return ContentDatabase.Instance().GetMapAssetsByType(type, onlyUnlocked).ToList();
		}

		public Faction[] GetFactions()
		{
			return ContentDatabase.Instance().GetFactionsOrderedByIndex().ToArray();
		}

		public Faction[] GetDisplayFactions()
		{
			return ContentDatabase.Instance().GetDisplayedFactionsOrderedByIndex().ToArray();
		}

		public MapAsset GetMap(int index)
		{
			return ContentDatabase.Instance().GetMapAssetByIndex(index);
		}

		public MapAsset GetMap(DatabaseID id)
		{
			if (id == default(DatabaseID))
			{
				return ContentDatabase.Instance().GetMapAssetByIndex(0);
			}
			return ContentDatabase.Instance().GetMapAsset(id);
		}

		public MapAsset GetMapOfTypeAtIndex(MapAsset.MapType mapType, int index)
		{
			return ContentDatabase.Instance().GetMapAssetByTypeAndMapIndex(mapType, index);
		}

		public int GetMapCount()
		{
			return ContentDatabase.Instance().GetMapAssetCount();
		}

		public void RemoveFaction(Faction faction)
		{
			for (int i = 0; i < Factions.Count; i++)
			{
				if (faction == Factions[i])
				{
					Factions.RemoveAt(i);
					break;
				}
			}
		}

		public void RemoveUnit(UnitBlueprint unit)
		{
			ContentDatabase.Instance().RemoveUserUnitBlueprintAndEmptyFactionsCreated(unit.Entity.GUID);
		}

		public List<CharacterItem> GetPropsOfType(UnitRig.GearType gearType)
		{
			return ContentDatabase.Instance().GetEditorVisibleCharacterItemsOfType(gearType).ToList();
		}

		public List<CharacterItem> GetWeaponsOfType<T>() where T : Weapon
		{
			return ContentDatabase.Instance().GetEditorVisibleWeaponItemsOfType<T>().Select((Func<WeaponItem, CharacterItem>)((WeaponItem weaponItem) => weaponItem))
				.ToList();
		}

		public List<CharacterItem> GetSpecialAbilities()
		{
			return ContentDatabase.Instance().GetEditorVisibleSpecialAbilities().Select((Func<SpecialAbility, CharacterItem>)((SpecialAbility specialAbility) => specialAbility))
				.ToList();
		}

		public Faction GetFactionByName(string factionName)
		{
			return ContentDatabase.Instance().GetFactionByName(factionName);
		}

		public ProjectileEntity[] GetProjectiles()
		{
			return (from projectile in ContentDatabase.Instance().GetAllProjectiles()
				select projectile.GetComponent<ProjectileEntity>()).ToArray();
		}

		public void VerifyNoMultipliers()
		{
			for (int i = 0; i < Units.Count; i++)
			{
				if (Units[i].attackSpeedMultiplier != 0f)
				{
					Debug.LogError("Failed on unit: " + Units[i], Units[i]);
				}
				if (Units[i].healthMultiplier != 0f)
				{
					Debug.LogError("Failed on unit: " + Units[i], Units[i]);
				}
				if (Units[i].rangeMultiplier != 0f)
				{
					Debug.LogError("Failed on unit: " + Units[i], Units[i]);
				}
				if (Units[i].damageMultiplier != 0f)
				{
					Debug.LogError("Failed on unit: " + Units[i], Units[i]);
				}
				Debug.Log(Units[i], Units[i]);
			}
		}

		public Faction[] GetDefaultHotbarUnits()
		{
			return ContentDatabase.Instance().GetDefaultHotbarFactions().ToArray();
		}

		public Faction[] GetCustomFactions(bool excludeDisabled = true)
		{
			return ContentDatabase.Instance().GetUserFactionsByOnEnabled(excludeDisabled).ToArray();
		}

		public Faction[] GetCustomFactions(string filter)
		{
			return ContentDatabase.Instance().GetUserFactionsByNamePart(filter).ToArray();
		}

		public Faction[] GetCustomFactions(string nameFilter, WorkshopTypeFilter type)
		{
			return ContentDatabase.Instance().GetUserFactionsByNamePartAndType(nameFilter, type).ToArray();
		}

		internal GameObject GetWeaponByID(DatabaseID id)
		{
			return ContentDatabase.Instance().GetWeapon(id);
		}

		public static SerializedHelpingData GetHelpingData()
		{
			return ContentDatabase.Instance().GetHelpingData();
		}

		public Faction[] GetFactionsByGUID(DatabaseID[] factionsIDs)
		{
			return ContentDatabase.Instance().GetFactionsByIds(factionsIDs).ToArray();
		}

		public UnitBlueprint[] GetUnitsByGUID(AllowedUnitWrapper[] unitIDs)
		{
			return ContentDatabase.Instance().GetUnitBlueprintsByIds(unitIDs.Select((AllowedUnitWrapper allowedUnit) => allowedUnit.ID)).ToArray();
		}
	}
}
