using System;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using UnityEngine;

namespace DM
{
	public class LandfallContentDatabase
	{
		private Dictionary<DatabaseID, GameObject> m_unitBases;

		private Dictionary<DatabaseID, GameObject> m_weapons;

		private Dictionary<DatabaseID, GameObject> m_combatMoves;

		private Dictionary<DatabaseID, GameObject> m_characterProps;

		private Dictionary<DatabaseID, GameObject> m_projectiles;

		private Dictionary<DatabaseID, TurningData> m_turningDatas;

		private Dictionary<DatabaseID, VoiceBundle> m_voiceBundles;

		private Dictionary<DatabaseID, UnitBlueprint> m_unitBlueprints;

		private Dictionary<DatabaseID, Faction> m_factions;

		private MapAsset[] m_orderedMapAssets;

		private Dictionary<DatabaseID, int> m_mapAssetIndexLookup;

		private Dictionary<DatabaseID, TABSCampaignAsset> m_campaigns;

		private Dictionary<DatabaseID, TABSCampaignLevelAsset> m_campaignLevels;

		private List<DatabaseID> m_defaultHotbarFactionIds;

		private List<DatabaseID> m_factionIconIds;

		public DatabaseID m_defaultVoiceBundleId;

		public SerializedHelpingData m_helpingData;

		public CustomFactionColorDatabase m_customFactionColorDatabase;

		public List<string> m_mainMenuScenes;

		public UnitEditorColorPalette m_unitEditorColorPalette;

		public string m_version;

		public UpgradeDataAsset m_upgradeDataAsset;

		public UnitBlueprint m_unitEditorBlueprint;

		public LandfallContentDatabase(NonStreamableDatabaseAssets nonStreamableDatabaseAssets, AssetLoader assetLoader)
		{
			TextAsset textAsset = Resources.Load<TextAsset>("DMLandfallContentDatabase");
			LandfallContentDatabaseFile databaseFile = DatabaseSerializer.Deserialize<LandfallContentDatabaseFile>(textAsset.bytes);
			if (databaseFile.buildDateTime != nonStreamableDatabaseAssets.BuildDateTimeString)
			{
				throw new Exception($"ResourceDatabase and ContentDatabase has a different build times: {databaseFile.buildDateTime} != {nonStreamableDatabaseAssets.BuildDateTimeString}");
			}
			m_unitBases = LoadDatabaseDictionary<GameObject>(databaseFile.unitBaseGuids, assetLoader);
			m_weapons = LoadDatabaseDictionary<GameObject>(databaseFile.weaponGuids, assetLoader);
			m_combatMoves = LoadDatabaseDictionary<GameObject>(databaseFile.combatMoveGuids, assetLoader);
			m_characterProps = LoadDatabaseDictionary<GameObject>(databaseFile.characterPropGuids, assetLoader);
			m_projectiles = LoadDatabaseDictionary<GameObject>(databaseFile.projectileGuids, assetLoader);
			m_turningDatas = LoadDatabaseDictionary<TurningData>(databaseFile.turningDataGuids, assetLoader);
			m_voiceBundles = LoadDatabaseDictionary<VoiceBundle>(databaseFile.voiceBundleGuids, assetLoader);
			m_orderedMapAssets = databaseFile.mapAssetGuids.Select((LandfallContentDatabaseFile.LandfallGuid landfallGuid) => assetLoader.GetAsset<MapAsset>(ToDatabaseID(landfallGuid))).ToArray();
			m_mapAssetIndexLookup = Enumerable.Range(0, databaseFile.mapAssetGuids.Count).ToDictionary((int i) => ToDatabaseID(databaseFile.mapAssetGuids[i]), (int i) => i);
			m_unitBlueprints = LoadDatabaseDictionary<UnitBlueprint>(databaseFile.unitBlueprintGuids, assetLoader);
			m_factions = LoadDatabaseDictionary<Faction>(databaseFile.factionGuids, assetLoader);
			m_campaignLevels = LoadCampaignLevels(databaseFile.campaignLevelGuids, this);
			m_campaigns = LoadCampaigns(databaseFile.campaignGuids, this);
			m_defaultHotbarFactionIds = databaseFile.defaultHotbarFactionGuids.Select(ToDatabaseID).ToList();
			m_factionIconIds = databaseFile.factionIconGuids.Select(ToDatabaseID).ToList();
			m_defaultVoiceBundleId = ToDatabaseID(databaseFile.defaultVoiceBundleGuid);
			m_helpingData = nonStreamableDatabaseAssets.HelpingData;
			m_customFactionColorDatabase = nonStreamableDatabaseAssets.CustomFactionColorDatabase;
			m_mainMenuScenes = nonStreamableDatabaseAssets.MainMenuScenes;
			m_unitEditorColorPalette = nonStreamableDatabaseAssets.UnitEditorColorPalette;
			m_version = nonStreamableDatabaseAssets.Version;
			m_upgradeDataAsset = nonStreamableDatabaseAssets.UpgradeDataAsset;
			m_unitEditorBlueprint = nonStreamableDatabaseAssets.UnitEditorBlueprint;
		}

		private static DatabaseID ToDatabaseID(LandfallContentDatabaseFile.LandfallGuid landfallGuid)
		{
			return new DatabaseID
			{
				m_modID = landfallGuid.modId,
				m_ID = landfallGuid.id
			};
		}

		private static Dictionary<DatabaseID, TABSCampaignLevelAsset> LoadCampaignLevels(List<LandfallContentDatabaseFile.LandfallGuid> landfallGuids, LandfallContentDatabase landfallContentDatabase)
		{
			return landfallGuids.Select(ToDatabaseID).ToDictionary((DatabaseID id) => id, delegate(DatabaseID id)
			{
				string text = $"levels/{id}";
				return TABSCampaignLevelAsset.DeserializeCampaignLevel(JsonUtility.FromJson<CampaignLevel>(Resources.Load<TextAsset>(text).text), text, landfallContentDatabase, null);
			});
		}

		private static Dictionary<DatabaseID, TABSCampaignAsset> LoadCampaigns(List<LandfallContentDatabaseFile.LandfallGuid> landfallGuids, LandfallContentDatabase landfallContentDatabase)
		{
			return landfallGuids.Select(ToDatabaseID).ToDictionary((DatabaseID id) => id, (DatabaseID id) => TABSCampaignAsset.DeserializeCampaign(JsonUtility.FromJson<CampaignSequence>(Resources.Load<TextAsset>($"campaigns/{id}").text), landfallContentDatabase.GetCampaignLevel, isLandfallCampaign: true));
		}

		private static Dictionary<DatabaseID, T> LoadDatabaseDictionary<T>(List<LandfallContentDatabaseFile.LandfallGuid> landfallGuids, AssetLoader assetContentDatabase) where T : UnityEngine.Object
		{
			return landfallGuids.Select(ToDatabaseID).ToDictionary((DatabaseID id) => id, (DatabaseID id) => assetContentDatabase.GetAsset<T>(id));
		}

		public IEnumerable<MapAsset> GetMapAssetsOrdered()
		{
			return m_orderedMapAssets;
		}

		public MapAsset GetMapAsset(DatabaseID id)
		{
			if (!m_mapAssetIndexLookup.ContainsKey(id))
			{
				return null;
			}
			return m_orderedMapAssets[m_mapAssetIndexLookup[id]];
		}

		public MapAsset GetMapAssetByIndex(int index)
		{
			if (index < 0 || index >= m_orderedMapAssets.Length)
			{
				return null;
			}
			return m_orderedMapAssets[index];
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

		public TurningData GetTurningData(DatabaseID id)
		{
			if (!m_turningDatas.ContainsKey(id))
			{
				return null;
			}
			return m_turningDatas[id];
		}

		public IEnumerable<GameObject> GetUnitBases()
		{
			return m_unitBases.Values;
		}

		public IEnumerable<GameObject> GetWeapons()
		{
			return m_weapons.Values;
		}

		public GameObject GetWeapon(DatabaseID id)
		{
			if (!m_weapons.ContainsKey(id))
			{
				return null;
			}
			return m_weapons[id];
		}

		public IEnumerable<GameObject> GetCombatMoves()
		{
			return m_combatMoves.Values;
		}

		public GameObject GetCombatMove(DatabaseID id)
		{
			if (!m_combatMoves.ContainsKey(id))
			{
				return null;
			}
			return m_combatMoves[id];
		}

		public IEnumerable<GameObject> GetCharacterProps()
		{
			return m_characterProps.Values;
		}

		public GameObject GetCharacterProp(DatabaseID id)
		{
			if (!m_characterProps.ContainsKey(id))
			{
				return null;
			}
			return m_characterProps[id];
		}

		public IEnumerable<GameObject> GetProjectiles()
		{
			return m_projectiles.Values;
		}

		public GameObject GetProjectile(DatabaseID id)
		{
			if (!m_projectiles.ContainsKey(id))
			{
				return null;
			}
			return m_projectiles[id];
		}

		public IEnumerable<VoiceBundle> GetVoiceBundles()
		{
			return m_voiceBundles.Values;
		}

		public VoiceBundle GetVoiceBundle(DatabaseID id)
		{
			if (!m_voiceBundles.ContainsKey(id))
			{
				return null;
			}
			return m_voiceBundles[id];
		}

		public IEnumerable<DatabaseID> GetDefaultHotbarFactionIds()
		{
			return m_defaultHotbarFactionIds;
		}

		public IEnumerable<DatabaseID> GetFactionIconIds()
		{
			return m_factionIconIds;
		}

		public DatabaseID GetDefaultVoiceBundleId()
		{
			return m_defaultVoiceBundleId;
		}

		public SerializedHelpingData GetHelpingData()
		{
			return m_helpingData;
		}

		public CustomFactionColorDatabase GetCustomFactionColorDatabase()
		{
			return m_customFactionColorDatabase;
		}

		public List<string> GetMainMenuScenes()
		{
			return m_mainMenuScenes;
		}

		public UnitEditorColorPalette GetUnitEditorColorPalette()
		{
			return m_unitEditorColorPalette;
		}

		public string GetVersion()
		{
			return m_version;
		}

		public UpgradeDataAsset GetUpgradeDataAsset()
		{
			return m_upgradeDataAsset;
		}

		public UnitBlueprint GetUnitEditorBlueprint()
		{
			return m_unitEditorBlueprint;
		}
	}
}
