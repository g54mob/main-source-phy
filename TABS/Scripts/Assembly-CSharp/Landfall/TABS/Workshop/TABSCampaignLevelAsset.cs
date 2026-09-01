using System;
using System.Linq;
using DM;
using Landfall.TABS.WinConditions;
using ModIO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Landfall.TABS.Workshop
{
	[Serializable]
	[CreateAssetMenu(fileName = "Standard Campaign Level", menuName = "Campaign/StandardCampaignLevel", order = 1)]
	public class TABSCampaignLevelAsset : ScriptableObject, IDatabaseEntity
	{
		[Serializable]
		public class TABSLayoutUnit
		{
			public UnitBlueprint m_unitBlueprint;

			public Vector3 m_position;

			public bool CampaignUnit;

			public float Rotation = 999f;

			[SerializeField]
			private SerializedRuntimeReference m_serializedRuntimeReference;

			[NonSerialized]
			private ReferenceType<Unit> m_runtimeReference;

			public SerializedRuntimeReference SerializedRuntimeReference
			{
				set
				{
					m_serializedRuntimeReference = value;
				}
			}

			public ReferenceType<Unit> RuntimeReference
			{
				get
				{
					return m_runtimeReference;
				}
				set
				{
					m_runtimeReference = value;
					m_serializedRuntimeReference = new SerializedRuntimeReference(m_runtimeReference);
				}
			}

			public Quaternion GetRotation()
			{
				return Quaternion.Euler(0f, Rotation, 0f);
			}

			public void DeserializeRuntimeReference()
			{
				if (!(m_serializedRuntimeReference.Guid == string.Empty) && m_serializedRuntimeReference.Guid != null)
				{
					m_runtimeReference = Landfall.TABS.WinConditions.RuntimeReference.ToReferenceType<Unit>(m_serializedRuntimeReference);
				}
			}
		}

		[SerializeField]
		private DatabaseEntity m_entity;

		[FormerlySerializedAs("Budget")]
		public int m_budget;

		[SerializeField]
		private TABSSceneSettings m_SceneSettings;

		[SerializeField]
		private CampaignInfo m_campaignInfo;

		public TABSLayoutUnit[] BlueUnits;

		public TABSLayoutUnit[] RedUnits;

		[SerializeField]
		private MapAsset m_mapAsset;

		public DatabaseID CustomMap;

		public UnitBlueprint[] AllowedUnits;

		public Faction[] AllowedFactions;

		public DatabaseEntity Entity => m_entity;

		public MapAsset MapAsset
		{
			get
			{
				return m_mapAsset;
			}
			set
			{
				m_mapAsset = value;
			}
		}

		public CampaignInfo CampaignInfo
		{
			get
			{
				return m_campaignInfo;
			}
			set
			{
				m_campaignInfo = value;
			}
		}

		public TABSSceneSettings SceneSettings
		{
			get
			{
				return m_SceneSettings;
			}
			set
			{
				m_SceneSettings = value;
			}
		}

		public bool LocalClientOwnedCustomContent { get; private set; }

		public bool IsCustomCampaignLevel { get; private set; }

		public int ModID { get; private set; }

		public ModProfile ModProfile { get; private set; }

		public bool IsModIOLevel => ModID > 2;

		public string FilePath { get; private set; }

		public void SetCustomIDButOnlySometimes(int modID)
		{
			ModID = modID;
		}

		public int GetTotalUnits()
		{
			int num = 0;
			int num2 = 0;
			if (RedUnits != null)
			{
				num = RedUnits.Length;
			}
			if (BlueUnits != null)
			{
				num2 = BlueUnits.Length;
			}
			return num + num2;
		}

		public void SetCustomUnit(int id, ModProfile profile)
		{
			IsCustomCampaignLevel = true;
			ModID = id;
			ModProfile = profile;
			if (CustomMap != default(DatabaseID) && ModID != 0)
			{
				CustomMap.m_modID = ModID;
			}
			if (profile != null && CustomContentLoaderModIO.LocalModIOUser != null)
			{
				LocalClientOwnedCustomContent = profile.submittedBy.id == CustomContentLoaderModIO.LocalModIOUser.id;
			}
		}

		public void SetIconPath(string iconPath)
		{
			Entity.SetSpriteIconPath(iconPath);
		}

		public void InitEntity()
		{
			m_entity = new DatabaseEntity(WorkshopContentType.Battle);
		}

		public bool IsAllowed(UnitBlueprint unit)
		{
			if (unit == null || unit.Entity == null)
			{
				return false;
			}
			SettingsInstance settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_RESTRICT_UNITS");
			if (settingsInstance != null && settingsInstance.currentValue == 1)
			{
				return true;
			}
			if (AllowedUnits == null || AllowedUnits.Length == 0)
			{
				return true;
			}
			UnitBlueprint[] allowedUnits = AllowedUnits;
			foreach (UnitBlueprint unitBlueprint in allowedUnits)
			{
				if (!(unitBlueprint == null) && unitBlueprint.Entity != null && unit.Entity.GUID == unitBlueprint.Entity.GUID)
				{
					return true;
				}
			}
			return false;
		}

		private UnitLayout SerializeUnitLayout(TABSLayoutUnit[] units)
		{
			UnitLayout unitLayout = new UnitLayout();
			if (units == null)
			{
				unitLayout.Units = new UnitLayout.Unit[0];
				return unitLayout;
			}
			unitLayout.Units = new UnitLayout.Unit[units.Length];
			for (int i = 0; i < unitLayout.Units.Length; i++)
			{
				TABSLayoutUnit tABSLayoutUnit = units[i];
				unitLayout.Units[i] = new UnitLayout.Unit(tABSLayoutUnit.m_position, tABSLayoutUnit.Rotation, tABSLayoutUnit.m_unitBlueprint, tABSLayoutUnit.RuntimeReference);
			}
			return unitLayout;
		}

		public CampaignLevel SerializeCampaignLevel(string iconPath = null)
		{
			CampaignLevel campaignLevel = new CampaignLevel();
			campaignLevel.BlueUnits = SerializeUnitLayout(BlueUnits);
			campaignLevel.RedUnits = SerializeUnitLayout(RedUnits);
			campaignLevel.ID = m_entity.GUID;
			campaignLevel.MapID = m_mapAsset.Entity.GUID;
			campaignLevel.LevelName = m_entity.Name;
			campaignLevel.IconPath = iconPath;
			campaignLevel.Budget = m_budget;
			campaignLevel.BattleDescription = m_campaignInfo.Description;
			campaignLevel.VersionNumber = CustomContentData.Version_Number;
			campaignLevel.AllowedFactions = new DatabaseID[AllowedFactions.Length];
			for (int i = 0; i < campaignLevel.AllowedFactions.Length; i++)
			{
				campaignLevel.AllowedFactions[i] = AllowedFactions[i].Entity.GUID;
			}
			campaignLevel.AllowedUnits = new AllowedUnitWrapper[AllowedUnits.Length];
			for (int j = 0; j < campaignLevel.AllowedUnits.Length; j++)
			{
				campaignLevel.AllowedUnits[j] = new AllowedUnitWrapper
				{
					ID = AllowedUnits[j].Entity.GUID,
					ModID = AllowedUnits[j].ModID
				};
			}
			campaignLevel.SceneSettings = m_SceneSettings;
			campaignLevel.CustomMapID = CustomMap;
			return campaignLevel;
		}

		public static TABSCampaignLevelAsset DeserializeCampaignLevel(CampaignLevel level, string filePath, LandfallContentDatabase landfallContentDatabase, UserContentDatabase userContentDatabase)
		{
			CampaignHandler.CheckVersion(level, filePath);
			TABSCampaignLevelAsset tABSCampaignLevelAsset = ScriptableObject.CreateInstance<TABSCampaignLevelAsset>();
			tABSCampaignLevelAsset.m_entity = new DatabaseEntity(WorkshopContentType.Battle);
			tABSCampaignLevelAsset.m_entity.Name = level.LevelName;
			tABSCampaignLevelAsset.m_entity.GUID = level.ID;
			if (!string.IsNullOrEmpty(level.IconPath))
			{
				tABSCampaignLevelAsset.m_entity.SetSpriteIconPath(level.IconPath);
			}
			tABSCampaignLevelAsset.m_budget = level.Budget;
			tABSCampaignLevelAsset.m_campaignInfo = default(CampaignInfo);
			tABSCampaignLevelAsset.m_campaignInfo.Description = level.BattleDescription;
			tABSCampaignLevelAsset.BlueUnits = new TABSLayoutUnit[0];
			tABSCampaignLevelAsset.RedUnits = new TABSLayoutUnit[0];
			if (level.BlueUnits != null)
			{
				tABSCampaignLevelAsset.BlueUnits = new TABSLayoutUnit[level.BlueUnits.Units.Length];
				for (int i = 0; i < tABSCampaignLevelAsset.BlueUnits.Length; i++)
				{
					if (level.BlueUnits.Units[i] != null)
					{
						tABSCampaignLevelAsset.BlueUnits[i] = new TABSLayoutUnit();
						tABSCampaignLevelAsset.BlueUnits[i].m_position = level.BlueUnits.Units[i].Position;
						tABSCampaignLevelAsset.BlueUnits[i].Rotation = level.BlueUnits.Units[i].Rotation;
						tABSCampaignLevelAsset.BlueUnits[i].m_unitBlueprint = GetUnitBlueprint(level.BlueUnits.Units[i].unitType);
						tABSCampaignLevelAsset.BlueUnits[i].SerializedRuntimeReference = level.BlueUnits.Units[i].SerializedRuntimeReference;
					}
				}
			}
			if (level.RedUnits != null)
			{
				tABSCampaignLevelAsset.RedUnits = new TABSLayoutUnit[level.RedUnits.Units.Length];
				for (int j = 0; j < tABSCampaignLevelAsset.RedUnits.Length; j++)
				{
					if (level.RedUnits.Units[j] != null)
					{
						tABSCampaignLevelAsset.RedUnits[j] = new TABSLayoutUnit();
						tABSCampaignLevelAsset.RedUnits[j].m_position = level.RedUnits.Units[j].Position;
						tABSCampaignLevelAsset.RedUnits[j].Rotation = level.RedUnits.Units[j].Rotation;
						tABSCampaignLevelAsset.RedUnits[j].m_unitBlueprint = GetUnitBlueprint(level.RedUnits.Units[j].unitType);
						tABSCampaignLevelAsset.RedUnits[j].SerializedRuntimeReference = level.RedUnits.Units[j].SerializedRuntimeReference;
					}
				}
			}
			tABSCampaignLevelAsset.AllowedUnits = level.AllowedUnits.Select((AllowedUnitWrapper unit) => GetUnitBlueprint(unit.ID)).ToArray();
			tABSCampaignLevelAsset.AllowedFactions = level.AllowedFactions.Select((DatabaseID id) => GetFaction(id)).ToArray();
			tABSCampaignLevelAsset.m_mapAsset = landfallContentDatabase.GetMapAsset(level.MapID);
			tABSCampaignLevelAsset.FilePath = filePath;
			tABSCampaignLevelAsset.SceneSettings = level.SceneSettings;
			tABSCampaignLevelAsset.CustomMap = level.CustomMapID;
			return tABSCampaignLevelAsset;
			Faction GetFaction(DatabaseID id)
			{
				Faction faction = landfallContentDatabase.GetFaction(id);
				if (faction == null && userContentDatabase != null)
				{
					faction = userContentDatabase.GetFaction(id);
				}
				return faction;
			}
			UnitBlueprint GetUnitBlueprint(DatabaseID id)
			{
				UnitBlueprint unitBlueprint = landfallContentDatabase.GetUnitBlueprint(id);
				if (unitBlueprint == null && userContentDatabase != null)
				{
					unitBlueprint = userContentDatabase.GetUnitBlueprint(id);
				}
				return unitBlueprint;
			}
		}

		public void SetValues(TABSCampaignLevelAsset other, bool keepID = false)
		{
			if (!keepID)
			{
				Entity.GUID = other.Entity.GUID;
			}
			RedUnits = other.RedUnits;
			BlueUnits = other.BlueUnits;
			m_budget = other.m_budget;
			Entity.Name = other.Entity.Name;
			m_mapAsset = other.m_mapAsset;
			AllowedUnits = other.AllowedUnits;
		}
	}
}
