using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using DM;
using Landfall.TABS.AI;
using Landfall.TABS.AI.Components.Modifiers;
using Landfall.TABS.AI.Components.Tags;
using Landfall.TABS.GameMode;
using Landfall.TABS.RuntimeCleanup;
using Landfall.TABS.UnitEditor;
using Landfall.TABS.Workshop;
using ModIO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TFBGames;
using UnityEngine;
using UnityEngine.Serialization;

namespace Landfall.TABS
{
	[Serializable]
	[CreateAssetMenu(fileName = "Standard Unit Blueprint", menuName = "Units/StandardBlueprint", order = 1)]
	public class UnitBlueprint : SerializedScriptableObject, IDatabaseEntity
	{
		[Serializable]
		public class ProjectMarsData
		{
			[SerializeField]
			[Tooltip("Please see the comments in the code.")]
			protected bool m_recordConditionals;

			[SerializeField]
			[Tooltip("Please see the comments in the code.")]
			protected bool m_recordTarget;

			[SerializeField]
			[Tooltip("Please see the comments in the code.")]
			protected bool m_recordLookDirection;

			public bool RecordConditionals => m_recordConditionals;

			public bool RecordTarget => m_recordTarget;

			public bool RecordLookDirection => m_recordLookDirection;
		}

		private const string DefaultFootsteps = "Footsteps/Normal";

		public const string UnitBaseFieldName = "m_unitBase";

		public const string RightWeaponFieldName = "m_rightWeapon";

		public const string LeftWeaponFieldName = "m_leftWeapon";

		public const string PrimaryPropsFieldName = "m_props_primary";

		[Tooltip("Indicates how much of the total unit cap this unit consumes.Currently only applies to LowEnd platforms.")]
		[SerializeField]
		private float foodCost = 1f;

		public float costTweak;

		public uint forceCost;

		[SerializeField]
		private DatabaseEntity m_entity;

		[SerializeField]
		private bool m_isSecret;

		[SerializeField]
		private bool excludeFromOnlineMultiplayer;

		[Header("Prefab Data")]
		[SerializeField]
		[FormerlySerializedAs("UnitBase")]
		private GameObject m_unitBase;

		private DatabaseID m_unitBaseDatabaseId;

		[SerializeField]
		private Mesh m_placementMesh;

		public bool holdinigWithTwoHands = true;

		[SerializeField]
		[FormerlySerializedAs("RightWeapon")]
		private GameObject m_rightWeapon;

		private DatabaseID m_rightWeaponDatabaseId;

		public PropItemData RightWeaponData;

		[SerializeField]
		[FormerlySerializedAs("LeftWeapon")]
		private GameObject m_leftWeapon;

		private DatabaseID m_leftWeaponDatabaseId;

		public PropItemData LeftWeaponData;

		public GameObject[] objectsToSpawnAsChildren;

		[SerializeField]
		[PreviouslySerializedAs("Riders")]
		[FormerlySerializedAs("Riders")]
		private UnitBlueprint[] Riders;

		public UnitBlueprint Mount;

		private bool m_hasCustomRider;

		private DatabaseID m_customRider;

		public float health = 100f;

		private float _attackSpeedMultiplier = 1f;

		private float _damageMultiplaier = 1f;

		private float _healthMultiplier = 1f;

		private float _rangeMultiplier = 1f;

		[SerializeField]
		private string vocalRef;

		[SerializeField]
		private string deathRef;

		[SerializeField]
		private string footRef;

		[SerializeField]
		private AudioPathData vocalPathData;

		[SerializeField]
		private AudioPathData deathPathData;

		[SerializeField]
		private AudioPathData footPathData;

		public VoiceBundle voiceBundle;

		public float movementSpeedMuiltiplier = 1f;

		public float animationMultiplier = 1f;

		public float stepMultiplier = 1f;

		public float balanceMultiplier = 1f;

		public float balanceForceMultiplier = 1f;

		public List<IMovementComponent> MovementComponents;

		public NavmeshType NavmeshType;

		public ITargetingComponent TargetingComponent = default(EnemyLeastWeightTargeting);

		public float ArmLimitMultiplier = 1f;

		public float neckLimitMultiplier = 1f;

		public bool scaleWeapons;

		public float weaponSeparation = 0.5f;

		public AnimationCurve attackDistanceCurve;

		public float massMultiplier = 1f;

		public float minSizeRandom = 0.8f;

		public float maxSizeRandom = 1.2f;

		public float sizeMultiplier = 1f;

		public float dragMultiplier = 1f;

		public float targetingPriorityMultiplier = 1f;

		[SerializeField]
		[FormerlySerializedAs("fistRef")]
		private GameObject m_fistRef;

		private DatabaseID m_fistRefDatabaseId;

		public bool excludeFromWinCondition;

		public bool removeCloseRangeMiss;

		[SerializeField]
		[FormerlySerializedAs("m_props")]
		private GameObject[] m_props_primary;

		private DatabaseID[] m_propDatabaseIds;

		public PropItemData[] m_propData;

		public bool Head;

		public bool Hip;

		public bool Torso;

		public bool ArmLeft;

		public bool ElbowLeft;

		public bool ArmRight;

		public bool ElbowRight;

		public bool LegLeft;

		public bool KneeLeft;

		public bool LegRight;

		public bool KneeRight;

		public bool WristLeft;

		public bool WristRight;

		public bool FootLeft;

		public bool FootRight;

		[Header("Turn Speed or Turning Data")]
		public TurningData turningData;

		public float turnSpeed = 18f;

		public AnimationCurve turnSpeedCurve;

		[Header("Project Mars")]
		public ProjectMarsData projectMarsData;

		private float voicePitch = 1f;

		private uint cost;

		private bool calculatedCost;

		[NonSerialized]
		public bool useCustomCost;

		[SerializeField]
		private string unitDescription;

		public DatabaseEntity Entity => m_entity;

		public bool IsSecret => m_isSecret;

		public int FoodCost => 1;

		public bool ExcludeFromOnlineMultiplayer => excludeFromOnlineMultiplayer;

		public Mesh PlacementMesh
		{
			get
			{
				if (m_placementMesh == null)
				{
					m_placementMesh = UnitBase.GetComponent<Unit>().PlacementMesh;
				}
				return m_placementMesh;
			}
		}

		public GameObject UnitBase
		{
			get
			{
				return m_unitBase ?? ContentDatabase.Instance().GetGameObject(m_unitBaseDatabaseId);
			}
			set
			{
				m_unitBase = value;
			}
		}

		public GameObject RightWeapon
		{
			get
			{
				return m_rightWeapon ?? ContentDatabase.Instance().GetGameObject(m_rightWeaponDatabaseId);
			}
			set
			{
				m_rightWeapon = value;
			}
		}

		public GameObject LeftWeapon
		{
			get
			{
				return m_leftWeapon ?? ContentDatabase.Instance().GetGameObject(m_leftWeaponDatabaseId);
			}
			set
			{
				m_leftWeapon = value;
			}
		}

		public bool HasMissingComponents { get; set; }

		public UnitBlueprint[] UnitRiders
		{
			get
			{
				if (m_hasCustomRider)
				{
					return new UnitBlueprint[1] { ContentDatabase.Instance().GetUnitBlueprint(m_customRider) };
				}
				return Riders;
			}
		}

		public float attackSpeedMultiplier
		{
			get
			{
				if (_attackSpeedMultiplier == 0f)
				{
					return 1f;
				}
				return _attackSpeedMultiplier;
			}
			set
			{
				_attackSpeedMultiplier = value;
			}
		}

		public float damageMultiplier
		{
			get
			{
				if (_damageMultiplaier == 0f)
				{
					return 1f;
				}
				return _damageMultiplaier;
			}
			set
			{
				_damageMultiplaier = value;
			}
		}

		public float healthMultiplier
		{
			get
			{
				if (_healthMultiplier == 0f)
				{
					return 1f;
				}
				return _healthMultiplier;
			}
			set
			{
				_healthMultiplier = value;
			}
		}

		public float rangeMultiplier
		{
			get
			{
				if (_rangeMultiplier == 0f)
				{
					return 1f;
				}
				return _rangeMultiplier;
			}
			set
			{
				_rangeMultiplier = value;
			}
		}

		public GameObject fistRef
		{
			get
			{
				return m_fistRef ?? ContentDatabase.Instance().GetGameObject(m_fistRefDatabaseId);
			}
			set
			{
				m_fistRef = value;
			}
		}

		public GameObject[] m_props
		{
			get
			{
				return m_props_primary ?? ContentDatabase.Instance().GetGameObjects(m_propDatabaseIds);
			}
			set
			{
				m_props_primary = value;
			}
		}

		public ProjectileEntity rightProjectile { get; set; }

		public ProjectileEntity leftProjectile { get; set; }

		public bool forceNoRandomSize { get; set; }

		public float VoicePitch
		{
			get
			{
				return voicePitch;
			}
			set
			{
				voicePitch = value;
			}
		}

		public AudioPathData VocalPathData => vocalPathData;

		public string VocalRef
		{
			get
			{
				if ((bool)voiceBundle)
				{
					return voiceBundle.VocalRef;
				}
				return vocalRef;
			}
		}

		public string DeathRef
		{
			get
			{
				if ((bool)voiceBundle)
				{
					return voiceBundle.DeathRef;
				}
				return deathRef;
			}
		}

		public float TurnSpeed
		{
			get
			{
				if (turningData != null)
				{
					return turningData.TurnSpeed;
				}
				return turnSpeed;
			}
		}

		public AnimationCurve TurnSpeedCurve
		{
			get
			{
				if (turningData != null)
				{
					return turningData.TurnSpeedCurve;
				}
				return turnSpeedCurve;
			}
		}

		public int customCost { get; set; }

		public string UnitDescription
		{
			get
			{
				return unitDescription;
			}
			set
			{
				unitDescription = value;
			}
		}

		public bool LocalClientOwnedCustomContent { get; private set; }

		public bool IsCustomUnit { get; private set; }

		public string FilePath { get; private set; }

		public bool IsModUnit => ModID != 0;

		public int ModID { get; private set; }

		public string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(Entity.Name))
				{
					return Entity.Name;
				}
				return base.name;
			}
		}

		private bool HasVoiceBundle()
		{
			return voiceBundle != null;
		}

		[OnDeserializing]
		private void OdinInitialiseVariables()
		{
			voicePitch = 1f;
		}

		public bool AllUnitPropsAndWeaponsColorsAreValid()
		{
			UnitEditorColorPalette unitEditorColorPalette = ContentDatabase.Instance().GetUnitEditorColorPalette();
			if (RightWeaponData != null && !ValidateColours(unitEditorColorPalette, RightWeaponData.m_colors))
			{
				return false;
			}
			if (LeftWeaponData != null && !ValidateColours(unitEditorColorPalette, LeftWeaponData.m_colors))
			{
				return false;
			}
			for (int i = 0; i < m_propData.Length; i++)
			{
				if (!ValidateColours(unitEditorColorPalette, m_propData[i].m_colors))
				{
					return false;
				}
			}
			return true;
		}

		private bool ValidateColours(UnitEditorColorPalette colorPalette, int[] colors)
		{
			foreach (int num in colors)
			{
				if (num < -1 || num >= colorPalette.Colors.Length)
				{
					return false;
				}
			}
			return true;
		}

		public bool Validate()
		{
			bool flag = true;
			if (!HasVoiceBundle())
			{
				flag &= AudioPathData.ValidateAndAssignPathData(vocalRef, ref vocalPathData, this);
				flag &= AudioPathData.ValidateAndAssignPathData(deathRef, ref deathPathData, this);
				return flag & AudioPathData.ValidateAndAssignPathData(footRef, ref footPathData, this);
			}
			flag &= AudioPathData.ValidateAndAssignPathData(voiceBundle.VocalRef, ref vocalPathData, this);
			flag &= AudioPathData.ValidateAndAssignPathData(voiceBundle.DeathRef, ref deathPathData, this);
			if (footRef == null)
			{
				footRef = "Footsteps/Normal";
			}
			return flag & AudioPathData.ValidateAndAssignPathData(footRef, ref footPathData, this);
		}

		public void DoMultipliers(Unit unit)
		{
			DataHandler componentInChildren = unit.GetComponentInChildren<DataHandler>();
			componentInChildren.maxHealth *= healthMultiplier;
			componentInChildren.health *= healthMultiplier;
			WeaponHandler componentInChildren2 = unit.GetComponentInChildren<WeaponHandler>();
			if ((bool)componentInChildren2)
			{
				if ((bool)componentInChildren2.leftWeapon)
				{
					componentInChildren2.leftWeapon.maxRange *= rangeMultiplier;
					componentInChildren2.leftWeapon.internalCooldown /= attackSpeedMultiplier;
					componentInChildren2.leftWeapon.levelMultiplier *= damageMultiplier;
				}
				if ((bool)componentInChildren2.rightWeapon)
				{
					componentInChildren2.rightWeapon.maxRange *= rangeMultiplier;
					componentInChildren2.rightWeapon.internalCooldown /= attackSpeedMultiplier;
					componentInChildren2.rightWeapon.levelMultiplier *= damageMultiplier;
				}
				if ((bool)componentInChildren2.leftWeapon)
				{
					componentInChildren2.leftWeapon.gameObject.FetchComponent<Level>().levelMultiplier *= damageMultiplier;
				}
				if ((bool)componentInChildren2.rightWeapon)
				{
					componentInChildren2.rightWeapon.gameObject.FetchComponent<Level>().levelMultiplier *= damageMultiplier;
				}
			}
		}

		public GameObject[] Spawn(Vector3 posistion, Quaternion rotation, Team team, out Unit unitToSpawn, float sentSizeMulti = 1f, bool isCampaignUnit = false, bool spawnRiders = true, int martianInstanceId = 0, UnitPoolInfo? poolInfo = null)
		{
			List<GameObject> list = new List<GameObject>();
			Weapon weapon = null;
			Weapon weapon2 = null;
			if (UnitBase == null)
			{
				Debug.LogError("UnitBase is Null: " + Entity.Name);
			}
			GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
			GameObject gameObject = UnityEngine.Object.Instantiate(UnitBase, posistion, rotation);
			SettingsInstance settingsInstance = service.GetSettingsInstance("VIDEO_SPAWN");
			GameModeService service2 = ServiceLocator.GetService<GameModeService>();
			BaseGameMode currentGameMode = service2.CurrentGameMode;
			bool flag = service2.IsGameModeRestricted();
			if (settingsInstance.currentValue == 0 || currentGameMode is MainMenuGameMode)
			{
				PlacementSpawnEffects componentInChildren = gameObject.GetComponentInChildren<PlacementSpawnEffects>();
				if ((bool)componentInChildren)
				{
					UnityEngine.Object.Destroy(componentInChildren.gameObject);
				}
			}
			SetTeamColors(gameObject, team);
			list.Add(gameObject);
			float sizeRand = 1f;
			sizeRand = ((!(currentGameMode.GetType() == typeof(OnlineMultiplayerGameMode))) ? RandomiseUnitSize(sizeRand) : (sentSizeMulti * sizeMultiplier));
			sizeRand *= sentSizeMulti;
			if (forceNoRandomSize)
			{
				sizeRand = sentSizeMulti * sizeMultiplier;
			}
			if (Bugs._DLC_ACTIVATED && !flag)
			{
				SettingsInstance settingsInstance2 = service.GetSettingsInstance("BUG_BIG_UNITS");
				SettingsInstance settingsInstance3 = service.GetSettingsInstance("BUG_SMALL_UNITS");
				if (settingsInstance2.currentValue == 1)
				{
					sizeRand *= 1.5f;
				}
				if (settingsInstance3.currentValue == 1)
				{
					sizeRand /= 1.5f;
				}
			}
			gameObject.SetActive(value: false);
			gameObject.transform.localScale *= sizeRand;
			gameObject.SetActive(value: true);
			unitToSpawn = gameObject.GetComponent<Unit>();
			unitToSpawn.InitializeUnit(team);
			unitToSpawn.unitBlueprint = this;
			unitToSpawn.Team = team;
			unitToSpawn.data.health = health * sizeRand * sizeRand;
			unitToSpawn.RemoteInstanceId = (short)martianInstanceId;
			unitToSpawn.PoolInfo = poolInfo;
			unitToSpawn.SetEnableSyncTransforms(!unitToSpawn.IsInPool);
			unitToSpawn.OriginatesFromClient = martianInstanceId != 0;
			if (targetingPriorityMultiplier != 0f)
			{
				unitToSpawn.targetingPriorityMultiplier = targetingPriorityMultiplier;
			}
			if (isCampaignUnit)
			{
				unitToSpawn.SetCampaignUnit();
			}
			TrySpawnAdditionalObjects(team, unitToSpawn);
			UnitSounds componentInChildren2 = unitToSpawn.GetComponentInChildren<UnitSounds>();
			if ((bool)componentInChildren2)
			{
				ValidateAudioPaths();
				if (Bugs._DLC_ACTIVATED && !flag)
				{
					SettingsInstance settingsInstance4 = service.GetSettingsInstance("BUG_BIG_UNITS");
					SettingsInstance settingsInstance5 = service.GetSettingsInstance("BUG_SMALL_UNITS");
					if (settingsInstance4.currentValue == 1 && settingsInstance5.currentValue != 1)
					{
						vocalPathData.EffectPitch = 0.5f;
					}
					if (settingsInstance5.currentValue == 1 && settingsInstance4.currentValue != 1)
					{
						vocalPathData.EffectPitch = 2f;
					}
				}
				componentInChildren2.VocalPathData = vocalPathData;
				componentInChildren2.FootPathData = footPathData;
				componentInChildren2.DeathPathData = deathPathData;
				if (VoicePitch <= 0f)
				{
					VoicePitch = 1f;
				}
				componentInChildren2.Pitch = VoicePitch;
			}
			Balance componentInChildren3 = unitToSpawn.GetComponentInChildren<Balance>();
			if (balanceMultiplier == 0f)
			{
				balanceMultiplier = 1f;
			}
			if (balanceForceMultiplier == 0f)
			{
				balanceForceMultiplier = 1f;
			}
			if ((bool)componentInChildren3)
			{
				componentInChildren3.allowedFootDistance *= balanceMultiplier * sizeMultiplier;
				componentInChildren3.allowedLegAngle *= balanceMultiplier;
				componentInChildren3.allowedTorsoAngle *= balanceMultiplier;
				componentInChildren3.forceMultiplier *= balanceForceMultiplier;
			}
			if ((bool)Mount)
			{
				GameObject[] array = gameObject.AddComponent<Mount>().EnterMount(Mount, null, 0);
				for (int i = 0; i < array.Length; i++)
				{
					list.Add(array[i]);
				}
			}
			UnitBlueprint[] unitRiders = UnitRiders;
			if (unitRiders != null && spawnRiders)
			{
				RiderHolder riderHolder = gameObject.AddComponent<RiderHolder>();
				List<GameObject> list2 = new List<GameObject>();
				for (int j = 0; j < unitRiders.Length; j++)
				{
					if (!(unitRiders[j] == null))
					{
						Unit unitToSpawn2;
						GameObject[] array2 = unitRiders[j].Spawn(posistion, rotation, team, out unitToSpawn2, 1f, isCampaignUnit: false, spawnRiders: false);
						if (isCampaignUnit)
						{
							unitToSpawn2.SetCampaignUnit();
						}
						list2.Add(array2[0]);
						for (int k = 0; k < array2.Length; k++)
						{
							list.Add(array2[k]);
						}
						unitToSpawn2.gameObject.AddComponent<Mount>().EnterMount(null, unitToSpawn, j);
						for (int l = 0; l < array2.Length; l++)
						{
							list.Add(array2[l]);
						}
						unitToSpawn2.IsRider = true;
						if (martianInstanceId != 0)
						{
							unitToSpawn2.IsRiderWithLinkedMount = true;
						}
					}
				}
				riderHolder.Addriders(list2);
			}
			if ((bool)RightWeapon)
			{
				weapon = SetWeapon(unitToSpawn, team, RightWeapon, RightWeaponData, HoldingHandler.HandType.Right, rotation, list);
				if ((bool)weapon)
				{
					if (scaleWeapons)
					{
						weapon.transform.localScale *= sentSizeMulti;
					}
					unitToSpawn.targetYourFriends = weapon.m_weaponTargetType == Weapon.WeaponTargetType.Fridemies;
				}
				if ((bool)weapon && weapon.GetType() == typeof(RangeWeapon) && rightProjectile != null)
				{
					((RangeWeapon)weapon).ObjectToSpawn = rightProjectile.gameObject;
				}
			}
			if ((bool)LeftWeapon)
			{
				weapon2 = SetWeapon(unitToSpawn, team, LeftWeapon, LeftWeaponData, HoldingHandler.HandType.Left, rotation, list);
				if ((bool)weapon2)
				{
					if (scaleWeapons)
					{
						weapon2.transform.localScale *= sentSizeMulti;
					}
					if (!unitToSpawn.targetYourFriends && weapon != null)
					{
						unitToSpawn.targetYourFriends = weapon.m_weaponTargetType == Weapon.WeaponTargetType.Fridemies;
					}
				}
				if ((bool)weapon2 && weapon2.GetType() == typeof(RangeWeapon) && leftProjectile != null)
				{
					((RangeWeapon)weapon2).ObjectToSpawn = leftProjectile.gameObject;
				}
			}
			if (!RightWeapon && !LeftWeapon)
			{
				SetWeapon(unitToSpawn, team, null, null, HoldingHandler.HandType.Right, rotation, list);
			}
			CurveAnimation[] componentsInChildren = gameObject.GetComponentsInChildren<CurveAnimation>();
			for (int m = 0; m < componentsInChildren.Length; m++)
			{
				componentsInChildren[m].multiplier *= animationMultiplier;
			}
			Unit component = gameObject.GetComponent<Unit>();
			if ((bool)component)
			{
				component.thickness += Mathf.Clamp((sizeMultiplier - 1f) * 0.3f, 0f, 100f);
			}
			RigidbodyHolder componentInChildren4 = gameObject.GetComponentInChildren<RigidbodyHolder>();
			if ((bool)componentInChildren4 && dragMultiplier != 0f)
			{
				componentInChildren4.dragMultiplier *= dragMultiplier;
			}
			MovementHandler componentInChildren5 = gameObject.GetComponentInChildren<MovementHandler>();
			if ((bool)componentInChildren5)
			{
				componentInChildren5.multiplier *= movementSpeedMuiltiplier;
			}
			StepHandler componentInChildren6 = gameObject.GetComponentInChildren<StepHandler>();
			if ((bool)componentInChildren6)
			{
				componentInChildren6.multiplier *= stepMultiplier;
			}
			WheelMovementHandler componentInChildren7 = gameObject.GetComponentInChildren<WheelMovementHandler>();
			if ((bool)componentInChildren7)
			{
				componentInChildren7.multiplier *= movementSpeedMuiltiplier;
			}
			if (ArmLimitMultiplier != 1f && ArmLimitMultiplier != 0f)
			{
				if ((bool)unitToSpawn.data.rightArm)
				{
					SetGlobalJointSettings.SetJointMultiplier(ArmLimitMultiplier, unitToSpawn.data.rightArm.GetComponent<Rigidbody>());
				}
				if ((bool)unitToSpawn.data.leftArm)
				{
					SetGlobalJointSettings.SetJointMultiplier(ArmLimitMultiplier, unitToSpawn.data.leftArm.GetComponent<Rigidbody>());
				}
			}
			if (neckLimitMultiplier != 1f && neckLimitMultiplier != 0f && (bool)unitToSpawn.data.head)
			{
				SetGlobalJointSettings.SetJointMultiplier(neckLimitMultiplier, unitToSpawn.data.head.GetComponent<Rigidbody>());
			}
			unitToSpawn.SetMassMultiplier(massMultiplier * sizeRand * sizeRand);
			DoMultipliers(unitToSpawn);
			SpawnProps(gameObject, unitToSpawn.RigType, team);
			if ((bool)weapon2 && unitToSpawn.m_AttackDistance < weapon2.maxRange)
			{
				unitToSpawn.m_AttackDistance = weapon2.maxRange;
			}
			if ((bool)weapon && unitToSpawn.m_AttackDistance < weapon.maxRange)
			{
				unitToSpawn.m_AttackDistance = weapon.maxRange;
			}
			unitToSpawn.m_PreferedDistance = unitToSpawn.m_AttackDistance - 0.3f;
			GameObject[] array3 = list.ToArray();
			ServiceLocator.GetService<RuntimeGarbageCollector>().AddGameObjects(array3);
			unitToSpawn.spawnedObjects = array3;
			LodGroupMerger component2 = gameObject.GetComponent<LodGroupMerger>();
			if (component2 != null)
			{
				component2.ConsolidateChildLodGroups();
			}
			return array3;
		}

		private void TrySpawnAdditionalObjects(Team team, Unit unitToSpawn)
		{
			if (objectsToSpawnAsChildren == null)
			{
				return;
			}
			for (int i = 0; i < objectsToSpawnAsChildren.Length; i++)
			{
				if (!(objectsToSpawnAsChildren[i] == null))
				{
					GameObject g = UnityEngine.Object.Instantiate(objectsToSpawnAsChildren[i], unitToSpawn.transform.position, unitToSpawn.transform.rotation, unitToSpawn.transform);
					SetTeamColors(g, team);
				}
			}
		}

		private void ValidateAudioPaths()
		{
			bool num = vocalPathData == null || vocalPathData.Category == null;
			bool flag = footPathData == null || footPathData.Category == null;
			bool flag2 = deathPathData == null || deathPathData.Category == null;
			if (num || flag || flag2)
			{
				Validate();
			}
		}

		public void SetNoCustomRider()
		{
			m_hasCustomRider = false;
			m_customRider = default(DatabaseID);
		}

		public void SetCustomRider(DatabaseID rider)
		{
			m_hasCustomRider = true;
			m_customRider = rider;
		}

		private float RandomiseUnitSize(float sizeRand)
		{
			if (minSizeRandom != maxSizeRandom)
			{
				sizeRand = NormalRandom.GetRandom(minSizeRandom, maxSizeRandom) * sizeMultiplier;
			}
			if (UnityEngine.Random.Range(0, 10000) == 0)
			{
				sizeRand *= ((UnityEngine.Random.value < 0.7f) ? 0.1f : 2f);
			}
			return sizeRand;
		}

		private void SetTeamColors(GameObject g, Team team)
		{
			TeamColor[] componentsInChildren = g.GetComponentsInChildren<TeamColor>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetTeamColor(team);
			}
		}

		public Weapon SetWeapon(Unit unit, Team team, GameObject weaponObject, PropItemData weaponData, HoldingHandler.HandType handType, Quaternion rotation, List<GameObject> objects, bool isUnitEditor = false)
		{
			WeaponHandler componentInChildren = unit.GetComponentInChildren<WeaponHandler>();
			if (!componentInChildren)
			{
				MultipleWeaponHandler componentInChildren2 = unit.GetComponentInChildren<MultipleWeaponHandler>();
				if ((bool)componentInChildren2)
				{
					componentInChildren2.SetWeapon(weaponObject, handType, isUnitEditor);
				}
				return null;
			}
			if ((bool)fistRef)
			{
				componentInChildren.fistRefernce = fistRef;
			}
			Torso componentInChildren3 = unit.GetComponentInChildren<Torso>();
			if ((bool)weaponObject)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(weaponObject, componentInChildren3.transform.position + componentInChildren3.transform.forward * 0.5f, rotation);
				gameObject.gameObject.transform.SetParent(unit.transform);
				objects.Add(gameObject);
				Weapon component = gameObject.GetComponent<Weapon>();
				WeaponItem component2 = gameObject.GetComponent<WeaponItem>();
				Holdable component3 = gameObject.GetComponent<Holdable>();
				gameObject.GetComponent<MeleeWeapon>();
				RangeWeapon component4 = gameObject.GetComponent<RangeWeapon>();
				if (removeCloseRangeMiss && (bool)component4)
				{
					component4.extraCDInMelee = 0f;
					component4.extraSpreadInMelee = 0f;
				}
				if ((bool)component3)
				{
					if (component3.useAlternaticeForIceGiant && (bool)unit.GetComponent<IceGiant>())
					{
						component3.holdableData.relativePosition = component3.iceGiantRelativePosition;
					}
					if (handType == HoldingHandler.HandType.Right)
					{
						unit.holdingHandler.rightHandActivity = HoldingHandler.HandActivity.HoldingRightObject;
						if (holdinigWithTwoHands && !LeftWeapon)
						{
							unit.holdingHandler.leftHandActivity = HoldingHandler.HandActivity.HoldingRightObject;
						}
						if ((bool)LeftWeapon)
						{
							component3.holdableData.relativePosition.x += weaponSeparation;
						}
						unit.holdingHandler.rightObject = component3;
					}
					else
					{
						unit.holdingHandler.leftHandActivity = HoldingHandler.HandActivity.HoldingLeftObject;
						unit.holdingHandler.leftObject = component3;
						component3.holdableData.relativePosition.x += weaponSeparation;
						component3.holdableData.relativePosition.x *= -1f;
						component3.holdableData.upRotation.x *= -1f;
						component3.holdableData.forwardRotation.x *= -1f;
					}
					component3.holdingHandler = unit.holdingHandler;
				}
				else
				{
					Transform transform = null;
					transform = ((handType != HoldingHandler.HandType.Right) ? unit.GetComponentInChildren<HandLeft>().transform : unit.GetComponentInChildren<HandRight>().transform);
					gameObject.transform.position = transform.position;
					gameObject.transform.rotation = transform.rotation;
					gameObject.transform.parent = transform;
				}
				componentInChildren.SetWeapon(component, handType);
				component2.Initialize(team);
				if (weaponData != null)
				{
					component2.SetPropData(weaponData, team);
				}
				return component;
			}
			return null;
		}

		private void SpawnProps(GameObject unitRoot, Stitcher.TransformCatalog.RigType rigType, Team team)
		{
			try
			{
				if (m_props != null && m_props.Length != 0)
				{
					UnitRig component = unitRoot.GetComponent<UnitRig>();
					if (!(component == null))
					{
						component.SpawnProps(m_props, m_propData, rigType, team);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Spawning prop failed: " + ex.Message + Environment.NewLine + ex.StackTrace);
			}
		}

		public uint GetUnitCost(bool includeRider = true)
		{
			if (forceCost != 0)
			{
				return forceCost;
			}
			if (!useCustomCost)
			{
				return GetAutoCost(includeRider);
			}
			return (ushort)customCost;
		}

		public GameObject[] Spawn(Vector3 posistion, Quaternion rotation, Team team, float sendSizeMulti = 1f, UnitPoolInfo? poolInfo = null)
		{
			Unit unitToSpawn = null;
			return Spawn(posistion, rotation, team, out unitToSpawn, sendSizeMulti, isCampaignUnit: false, spawnRiders: true, 0, poolInfo);
		}

		public void DebugIDs()
		{
			Debug.Log("ID: " + Entity.GUID);
		}

		public SerializedUnitBlueprint SerializedUnit(DatabaseID fallbackVoiceBundleId)
		{
			SerializedUnitBlueprint serializedUnitBlueprint = new SerializedUnitBlueprint();
			serializedUnitBlueprint.customCost = customCost;
			serializedUnitBlueprint.useCustomCost = useCustomCost;
			serializedUnitBlueprint.m_name = m_entity.Name;
			serializedUnitBlueprint.m_id = m_entity.GUID;
			if (UnitBase != null)
			{
				serializedUnitBlueprint.m_unitBase = UnitBase.GetComponent<Unit>().Entity.GUID;
			}
			serializedUnitBlueprint.m_holdingWithTwoHands = holdinigWithTwoHands;
			if (RightWeapon != null)
			{
				serializedUnitBlueprint.m_rightWeapon = RightWeapon.GetComponent<WeaponItem>().Entity.GUID;
			}
			serializedUnitBlueprint.m_rightWeaponData = RightWeaponData;
			if (LeftWeapon != null)
			{
				serializedUnitBlueprint.m_leftWeapon = LeftWeapon.GetComponent<WeaponItem>().Entity.GUID;
			}
			serializedUnitBlueprint.m_leftWeaponData = LeftWeaponData;
			serializedUnitBlueprint.m_hasRider = m_hasCustomRider;
			if (m_hasCustomRider)
			{
				serializedUnitBlueprint.m_rider = m_customRider;
			}
			serializedUnitBlueprint.m_health = health;
			if (movementSpeedMuiltiplier != 0f)
			{
				serializedUnitBlueprint.m_movementSpeedMuiltiplier = movementSpeedMuiltiplier;
			}
			if (animationMultiplier != 0f)
			{
				serializedUnitBlueprint.m_animationMultiplier = animationMultiplier;
			}
			serializedUnitBlueprint.m_stepMultiplier = stepMultiplier;
			serializedUnitBlueprint.m_targetingPriorityMultiplier = targetingPriorityMultiplier;
			serializedUnitBlueprint.m_armLimitMultiplier = ArmLimitMultiplier;
			serializedUnitBlueprint.m_attackDistanceCurve = attackDistanceCurve;
			if (massMultiplier != 0f)
			{
				serializedUnitBlueprint.m_massMultiplier = massMultiplier;
			}
			serializedUnitBlueprint.m_sizeMultiplier = sizeMultiplier;
			if (fistRef != null)
			{
				serializedUnitBlueprint.m_fistRef = fistRef.GetComponent<WeaponItem>().Entity.GUID;
			}
			serializedUnitBlueprint.m_props = (from prop in m_props?.Where((GameObject obj) => obj != null)
				select prop.GetComponent<PropItem>().Entity.GUID).ToArray();
			serializedUnitBlueprint.m_propData = m_propData;
			if (turningData != null)
			{
				serializedUnitBlueprint.m_turningData = turningData.Entity.GUID;
			}
			serializedUnitBlueprint.m_turnSpeed = turnSpeed;
			serializedUnitBlueprint.m_turnSpeedCurve = turnSpeedCurve;
			serializedUnitBlueprint.m_combatMoves = (from objectToSpawnAsChild in objectsToSpawnAsChildren?.Where((GameObject obj) => obj != null)
				select objectToSpawnAsChild.GetComponent<SpecialAbility>().Entity.GUID).ToArray();
			serializedUnitBlueprint.m_movementTypes = MovementComponents?.Select((IMovementComponent movementType) => movementType.GetType().AssemblyQualifiedName).ToArray();
			serializedUnitBlueprint.m_targetingType = TargetingComponent.GetType().AssemblyQualifiedName;
			serializedUnitBlueprint.unitDescription = UnitDescription;
			if ((bool)voiceBundle)
			{
				serializedUnitBlueprint.voiceBundle = voiceBundle.Entity.GUID;
			}
			serializedUnitBlueprint.voicePitch = voicePitch;
			serializedUnitBlueprint.attackSpeedMultiplier = attackSpeedMultiplier;
			serializedUnitBlueprint.damageMultiplier = damageMultiplier;
			if ((bool)leftProjectile)
			{
				serializedUnitBlueprint.leftProjectile = leftProjectile.Entity.GUID;
			}
			else
			{
				serializedUnitBlueprint.leftProjectile = default(DatabaseID);
			}
			if ((bool)rightProjectile)
			{
				serializedUnitBlueprint.rightProjectile = rightProjectile.Entity.GUID;
			}
			else
			{
				serializedUnitBlueprint.rightProjectile = default(DatabaseID);
			}
			return serializedUnitBlueprint;
		}

		public void SetIconPath(string iconPath)
		{
			Entity.SetSpriteIconPath(iconPath);
		}

		public void SetIconTexture(Texture2D tex)
		{
			if (!(tex == null))
			{
				Entity.SpriteIcon = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
			}
		}

		public static UnitBlueprint DeserializedUnit(SerializedUnitBlueprint data, AssetLoader assetLoader, LandfallContentDatabase landfallContentDatabase)
		{
			UnitBlueprint unitBlueprint = ScriptableObject.CreateInstance<UnitBlueprint>();
			unitBlueprint.HasMissingComponents = false;
			unitBlueprint.m_entity = new DatabaseEntity(WorkshopContentType.Unit);
			unitBlueprint.m_entity.Name = data.m_name;
			unitBlueprint.m_entity.GUID = data.m_id;
			unitBlueprint.customCost = data.customCost;
			unitBlueprint.useCustomCost = data.useCustomCost;
			unitBlueprint.m_unitBaseDatabaseId = data.m_unitBase;
			unitBlueprint.holdinigWithTwoHands = data.m_holdingWithTwoHands;
			if (!IsValidUnitComponent(assetLoader, data.m_rightWeapon))
			{
				AssignValuesForMissingComponent(unitBlueprint, ref unitBlueprint.m_rightWeaponDatabaseId, ref unitBlueprint.RightWeaponData, "Right Weapon");
			}
			else
			{
				unitBlueprint.m_rightWeaponDatabaseId = data.m_rightWeapon;
				unitBlueprint.RightWeaponData = data.m_rightWeaponData;
			}
			if (!IsValidUnitComponent(assetLoader, data.m_leftWeapon))
			{
				AssignValuesForMissingComponent(unitBlueprint, ref unitBlueprint.m_leftWeaponDatabaseId, ref unitBlueprint.LeftWeaponData, "Left Weapon");
			}
			else
			{
				unitBlueprint.m_leftWeaponDatabaseId = data.m_leftWeapon;
				unitBlueprint.LeftWeaponData = data.m_leftWeaponData;
			}
			unitBlueprint.m_hasCustomRider = data.m_hasRider;
			if (data.m_hasRider)
			{
				if (data.m_rider == data.m_id)
				{
					Debug.LogError("RIDER AND UNIT HAS SAME ID. PLEASE DON'T!");
					data.m_hasRider = false;
				}
				else if (!IsValidUnitComponent(assetLoader, unitBlueprint.m_customRider))
				{
					AssignValuesForMissingComponent(unitBlueprint, ref unitBlueprint.m_customRider, "Custom Rider");
				}
				else
				{
					unitBlueprint.m_customRider = data.m_rider;
				}
			}
			unitBlueprint.health = data.m_health;
			unitBlueprint.movementSpeedMuiltiplier = data.m_movementSpeedMuiltiplier;
			unitBlueprint.animationMultiplier = data.m_animationMultiplier;
			unitBlueprint.stepMultiplier = data.m_stepMultiplier;
			unitBlueprint.targetingPriorityMultiplier = data.m_targetingPriorityMultiplier;
			unitBlueprint.ArmLimitMultiplier = data.m_armLimitMultiplier;
			unitBlueprint.attackDistanceCurve = data.m_attackDistanceCurve;
			unitBlueprint.massMultiplier = data.m_massMultiplier;
			unitBlueprint.sizeMultiplier = data.m_sizeMultiplier;
			unitBlueprint.maxSizeRandom = 1.2f;
			unitBlueprint.minSizeRandom = 0.8f;
			if (!IsValidUnitComponent(assetLoader, data.m_fistRef))
			{
				AssignValuesForMissingComponent(unitBlueprint, ref unitBlueprint.m_fistRefDatabaseId, "Fist Ref");
			}
			else
			{
				unitBlueprint.m_fistRefDatabaseId = data.m_fistRef;
			}
			if (data.m_props == null)
			{
				unitBlueprint.m_propDatabaseIds = data.m_props;
				unitBlueprint.m_propData = data.m_propData;
			}
			else
			{
				List<DatabaseID> list = new List<DatabaseID>();
				List<PropItemData> list2 = new List<PropItemData>();
				for (int i = 0; i < data.m_props.Length; i++)
				{
					if (assetLoader.Exists(data.m_props[i]))
					{
						list.Add(data.m_props[i]);
						list2.Add((i < data.m_propData.Length) ? data.m_propData[i] : new PropItemData());
					}
					else
					{
						unitBlueprint.HasMissingComponents = true;
					}
				}
				unitBlueprint.m_propDatabaseIds = list.ToArray();
				unitBlueprint.m_propData = list2.ToArray();
			}
			unitBlueprint.turningData = landfallContentDatabase.GetTurningData(data.m_turningData);
			unitBlueprint.turnSpeed = data.m_turnSpeed;
			unitBlueprint.turnSpeedCurve = data.m_turnSpeedCurve;
			unitBlueprint.UnitDescription = data.unitDescription;
			unitBlueprint.voiceBundle = landfallContentDatabase.GetVoiceBundle(data.voiceBundle);
			unitBlueprint.attackSpeedMultiplier = data.attackSpeedMultiplier;
			unitBlueprint.damageMultiplier = data.damageMultiplier;
			if (!IsValidUnitComponent(assetLoader, data.leftProjectile))
			{
				unitBlueprint.HasMissingComponents = true;
				unitBlueprint.leftProjectile = null;
			}
			else
			{
				unitBlueprint.leftProjectile = landfallContentDatabase.GetProjectile(data.leftProjectile)?.GetComponent<ProjectileEntity>();
			}
			if (!IsValidUnitComponent(assetLoader, data.rightProjectile))
			{
				unitBlueprint.HasMissingComponents = true;
				unitBlueprint.rightProjectile = null;
			}
			else
			{
				unitBlueprint.rightProjectile = landfallContentDatabase.GetProjectile(data.rightProjectile)?.GetComponent<ProjectileEntity>();
			}
			unitBlueprint.footRef = "Footsteps/Normal";
			if (data.m_movementTypes != null)
			{
				List<IMovementComponent> list3 = new List<IMovementComponent>();
				for (int j = 0; j < data.m_movementTypes.Length; j++)
				{
					Type type = Type.GetType(data.m_movementTypes[j]);
					list3.Add((IMovementComponent)Activator.CreateInstance(type));
				}
				unitBlueprint.MovementComponents = list3;
			}
			if (!string.IsNullOrEmpty(data.m_targetingType))
			{
				Type type2 = Type.GetType(data.m_targetingType);
				if (type2 == null)
				{
					Debug.LogError("Error getType: " + data.m_targetingType);
				}
				else
				{
					unitBlueprint.TargetingComponent = (ITargetingComponent)Activator.CreateInstance(type2);
				}
			}
			if (data.m_combatMoves != null)
			{
				List<GameObject> list4 = new List<GameObject>();
				for (int k = 0; k < data.m_combatMoves.Length; k++)
				{
					DatabaseID databaseID = data.m_combatMoves[k];
					if (assetLoader.Exists(databaseID))
					{
						list4.Add(landfallContentDatabase.GetCombatMove(databaseID));
					}
					else
					{
						unitBlueprint.HasMissingComponents = true;
					}
				}
				unitBlueprint.objectsToSpawnAsChildren = list4.Where((GameObject x) => x != null).ToArray();
			}
			unitBlueprint.voicePitch = data.voicePitch;
			unitBlueprint.Validate();
			return unitBlueprint;
		}

		public UnitBlueprint(UnitBlueprint blueprint)
		{
			m_entity = new DatabaseEntity(WorkshopContentType.Unit);
			m_entity.GenerateNewID();
			SetValues(blueprint);
		}

		public void SetValues(UnitBlueprint blueprint)
		{
			m_entity.Name = blueprint.m_entity.Name;
			m_entity.SpriteIcon = blueprint.m_entity.SpriteIcon;
			UnitBase = blueprint.UnitBase;
			holdinigWithTwoHands = blueprint.holdinigWithTwoHands;
			RightWeapon = blueprint.RightWeapon;
			RightWeaponData = blueprint.RightWeaponData;
			LeftWeapon = blueprint.LeftWeapon;
			LeftWeaponData = blueprint.LeftWeaponData;
			Riders = blueprint.Riders;
			Mount = blueprint.Mount;
			health = blueprint.health;
			movementSpeedMuiltiplier = blueprint.movementSpeedMuiltiplier;
			animationMultiplier = blueprint.animationMultiplier;
			stepMultiplier = blueprint.stepMultiplier;
			targetingPriorityMultiplier = blueprint.targetingPriorityMultiplier;
			ArmLimitMultiplier = blueprint.ArmLimitMultiplier;
			attackDistanceCurve = blueprint.attackDistanceCurve;
			massMultiplier = blueprint.massMultiplier;
			sizeMultiplier = blueprint.sizeMultiplier;
			fistRef = blueprint.fistRef;
			m_props = blueprint.m_props;
			m_propData = blueprint.m_propData;
			turningData = blueprint.turningData;
			turnSpeed = blueprint.turnSpeed;
			turnSpeedCurve = blueprint.turnSpeedCurve;
			TargetingComponent = blueprint.TargetingComponent;
			MovementComponents = blueprint.MovementComponents;
		}

		private static GameObject GetGameObject<T>(DatabaseID guid, IEnumerable<GameObject> objectList) where T : IDatabaseEntity
		{
			if (guid != default(DatabaseID))
			{
				return objectList.FirstOrDefault((GameObject g) => g.GetComponent<T>() != null && g.GetComponent<T>().Entity.GUID == guid);
			}
			return null;
		}

		public uint GetAutoCost(bool includeRider = true)
		{
			if (!calculatedCost)
			{
				cost = 0u;
				if ((bool)UnitBase)
				{
					Cost component = UnitBase.GetComponent<Cost>();
					if ((bool)component)
					{
						cost += (ushort)(float)component.m_cost;
					}
				}
				if ((bool)RightWeapon)
				{
					Cost component = RightWeapon.GetComponent<Cost>();
					if ((bool)component)
					{
						cost += component.m_cost;
					}
				}
				if ((bool)LeftWeapon)
				{
					Cost component = LeftWeapon.GetComponent<Cost>();
					if ((bool)component)
					{
						cost += component.m_cost;
					}
				}
				if ((bool)fistRef)
				{
					Cost component = fistRef.GetComponent<Cost>();
					if ((bool)component)
					{
						cost += component.m_cost;
					}
				}
				if ((bool)Mount)
				{
					cost += Mount.GetUnitCost();
				}
				if (includeRider && UnitRiders != null)
				{
					for (int i = 0; i < UnitRiders.Length; i++)
					{
						if (UnitRiders[i] != null)
						{
							cost += UnitRiders[i].GetUnitCost(includeRider: false);
						}
					}
				}
				if (objectsToSpawnAsChildren != null && objectsToSpawnAsChildren.Length != 0)
				{
					for (int j = 0; j < objectsToSpawnAsChildren.Length; j++)
					{
						if (!(objectsToSpawnAsChildren[j] == null))
						{
							Cost component = objectsToSpawnAsChildren[j].GetComponent<Cost>();
							if ((bool)component)
							{
								cost += component.m_cost;
							}
						}
					}
				}
				try
				{
					float costMultiplier = UnitBase.GetComponent<Unit>().costMultiplier;
					cost += (uint)(health * 0.5f * costMultiplier * sizeMultiplier * sizeMultiplier);
					cost += (uint)costTweak;
					cost = (uint)((float)cost * (1f + (massMultiplier - 1f) * 0.01f * costMultiplier));
					cost = (uint)(10f * Mathf.Round((float)cost / 10f));
				}
				catch (Exception value)
				{
					Console.WriteLine(value);
					if (m_unitBase == null)
					{
						Console.WriteLine(Entity.Name + " has empty unit base");
					}
					throw;
				}
			}
			return cost;
		}

		public void SetCustomUnit(int id, string filePath)
		{
			IsCustomUnit = true;
			ModID = id;
			FilePath = filePath;
		}

		public void SetModProfile(ModProfile profile)
		{
			if (profile != null && LocalUser.AuthenticationState == AuthenticationState.ValidToken && LocalUser.instance.profile != null)
			{
				LocalClientOwnedCustomContent = profile.submittedBy.id == LocalUser.instance.profile.id;
			}
		}

		public static string GetCustomUnitPath(string fileName)
		{
			string unitDirectoryPath = CustomContentFilePaths.UnitDirectoryPath;
			string text = Path.Combine(unitDirectoryPath, fileName);
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			service.CreateDirectory(unitDirectoryPath, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			service.CreateDirectory(text, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			return string.Format(Path.Combine(text + "/", "{0}{1}"), fileName, CustomContentFilePaths.FileEndingUnit);
		}

		public static string GetCustomUnitPath()
		{
			string text = Path.Combine(GamePaths.DataPath, "../../CustomUnits/");
			ServiceLocator.GetService<FileIOWrapper>().CreateDirectory(text, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
			return text;
		}

		public DateTime GetWriteTime()
		{
			if (IsCustomUnit)
			{
				return new FileInfo(FilePath).LastWriteTimeUtc;
			}
			return DateTime.UtcNow;
		}

		private static bool IsValidUnitComponent(AssetLoader assetLoader, DatabaseID componentDatabaseID)
		{
			if (!CharacterItemExtensions.ShouldCustomContentBeValidated())
			{
				return true;
			}
			if (!componentDatabaseID.IsDefaultID())
			{
				return assetLoader.Exists(componentDatabaseID);
			}
			return true;
		}

		private static void AssignValuesForMissingComponent<T>(UnitBlueprint unitBlueprint, ref DatabaseID databaseID, ref T objectData, string errorItemName) where T : class
		{
			AssignValuesForMissingComponent(unitBlueprint, ref databaseID, errorItemName);
			objectData = null;
		}

		private static void AssignValuesForMissingComponent(UnitBlueprint unitBlueprint, ref DatabaseID databaseID, string errorItemName)
		{
			unitBlueprint.HasMissingComponents = true;
			databaseID = DatabaseID.DefaultUnassignedID;
		}
	}
}
