using System;
using System.Collections.Generic;
using BitCode.Extensions;
using Landfall.MonoBatch;
using Landfall.TABC;
using Landfall.TABS.AI;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameMode;
using Landfall.TABS.WinConditions;
using TFBGames;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS
{
	public class Unit : BatchedMonobehaviour, IDatabaseEntity, IRemotelyControllable
	{
		public enum UnitType
		{
			Meat = 0,
			Warmachine = 1
		}

		public delegate void EnableSyncTransformsChangedEventHandler(Unit unit, bool enableSyncTransforms);

		[SerializeField]
		private DatabaseEntity m_entity;

		[SerializeField]
		private Stitcher.TransformCatalog.RigType m_rigType;

		public UnitType unitType;

		public Team Team;

		public UnitBlueprint unitBlueprint;

		[HideInInspector]
		public bool targetYourFriends;

		[HideInInspector]
		public GameObject[] spawnedObjects;

		[HideInInspector]
		public DataHandler data;

		private Vector3 predicedPosition;

		private bool hasPridictedPos;

		[HideInInspector]
		public Transform Hip;

		[HideInInspector]
		public Transform Head;

		[HideInInspector]
		public GeneralInput Input;

		[HideInInspector]
		public WeaponHandler WeaponHandler;

		[Header("Occupation")]
		[SerializeField]
		private PlacementSquare m_placementSquare;

		[SerializeField]
		private Mesh m_placementMesh;

		[Header("Stats")]
		public float costMultiplier = 1f;

		[Header("Stats")]
		public float thickness;

		public float m_PreferedDistance = 2f;

		public float m_AttackDistance = 1.5f;

		[SerializeField]
		private float m_inRangeAttackCooldown;

		private float m_maxInRangeAttackCooldown;

		[HideInInspector]
		public bool dead;

		[HideInInspector]
		public float fleesWithinThisManyMeters;

		[HideInInspector]
		public float targetingPriorityMultiplier = 1f;

		[HideInInspector]
		public bool hasBeeninRange;

		[HideInInspector]
		public float attackCounter;

		public float lowestAttackTimeOfWeapons = 2f;

		[HideInInspector]
		public float turnSpeedMultiplier = 1f;

		[HideInInspector]
		public float turnSpeed;

		[HideInInspector]
		public HoldingHandler holdingHandler;

		private DataHandler dataHandler;

		[HideInInspector]
		public bool neverStopRunning;

		private SpawnFreezeBody[] spawnFreezeBodies;

		[HideInInspector]
		public bool wasPlaced;

		public Vector3 unitConfusion = Vector3.zero;

		[HideInInspector]
		public bool isUnitCopy;

		public UnitAPI api;

		private UnitsSpawnMonitor m_unitsSpawnMonitor;

		private ReferenceType<Unit> m_runtimeReference;

		private List<Renderer> cachedRenderers = new List<Renderer>();

		private List<ParticleSystem> allParticleEffects = new List<ParticleSystem>();

		private GameModeService m_gameModeService;

		private TeamSystem m_teamSystem;

		private TABCUnitUI m_unitUI;

		private PlacementSpawnEffects spawnEffects;

		private IHighlight m_highlighter;

		private Action<Vector3, Rigidbody, Vector3> AttackAction;

		private Action<Rigidbody, Rigidbody> WasAttackedAction;

		private Action<Rigidbody, Rigidbody> WasDamagedAction;

		public Action<float> WasDealtDamageAction;

		public Animator[] attackAnimator;

		public float attackSpeedMultiplier = 1f;

		public int level = 1;

		public float damageDealt;

		private List<ushort> m_projectileNetworkIds;

		public DatabaseEntity Entity => m_entity;

		public bool CampaignUnit { get; private set; }

		public Vector3 PredicedPosition
		{
			get
			{
				if (!hasPridictedPos && (bool)data.mainRig)
				{
					predicedPosition = data.mainRig.position + data.mainRig.velocity * 0.3f;
					hasPridictedPos = true;
				}
				return predicedPosition;
			}
		}

		public Stitcher.TransformCatalog.RigType RigType => m_rigType;

		public PlacementSquare PlacementSquare => m_placementSquare;

		public Mesh PlacementMesh => m_placementMesh;

		public float InRangeAttackCooldown
		{
			get
			{
				return m_inRangeAttackCooldown;
			}
			set
			{
				m_inRangeAttackCooldown = value;
			}
		}

		public float MaxInRangeAttackCooldown
		{
			get
			{
				return m_maxInRangeAttackCooldown;
			}
			set
			{
				m_maxInRangeAttackCooldown = value;
			}
		}

		public Action<float> DealDamageAction { get; internal set; }

		public int RuntimeIndex { get; set; }

		public bool IsSpawnedInBlindPlacement { get; set; }

		public UnitSpawnSource SpawnSource { get; private set; }

		public PlacementSpawnEffects SpawnEffects => spawnEffects;

		public ReferenceType<Unit> RuntimeReference
		{
			get
			{
				return m_runtimeReference;
			}
			set
			{
				m_runtimeReference = value;
			}
		}

		public bool IsRemotelyControlled { get; private set; }

		public ulong NetworkId { get; set; }

		public ushort SmallNetworkId { get; set; }

		public ushort CopyOfSmallNetworkId { get; set; }

		public Vector3 CopyOfUnitSpawnPosition { get; set; }

		public short RemoteInstanceId { get; set; }

		public short InstanceId { get; set; }

		public bool IsLocalCopyOfRemoteUnit { get; set; }

		public bool IsRiderWithLinkedMount { get; set; }

		public bool IsRider { get; set; }

		public bool IsInPool => PoolInfo.HasValue;

		public UnitPoolInfo? PoolInfo { get; set; }

		public bool OriginatesFromClient { get; set; }

		public bool EnableSyncTransforms { get; private set; } = true;

		public event EnableSyncTransformsChangedEventHandler EnableSyncTransformsChanged;

		public void SetCampaignUnit()
		{
			CampaignUnit = true;
		}

		private void Awake()
		{
			api = GetComponent<UnitAPI>();
			m_maxInRangeAttackCooldown = m_inRangeAttackCooldown;
			data = GetComponentInChildren<DataHandler>();
			dataHandler = GetComponentInChildren<DataHandler>();
			WeaponHandler = GetComponentInChildren<WeaponHandler>();
			Input = GetComponentInChildren<GeneralInput>();
			Hip = GetComponentInChildren<Hip>().transform;
			Head = GetComponentInChildren<Head>().transform;
			holdingHandler = GetComponentInChildren<HoldingHandler>();
			spawnFreezeBodies = GetComponentsInChildren<SpawnFreezeBody>();
			m_highlighter = GetComponent<IHighlight>();
			if (m_highlighter == null)
			{
				m_highlighter = base.gameObject.GetOrAddComponent<Outline>();
			}
			m_highlighter.EndHighlight();
			m_unitsSpawnMonitor = ServiceLocator.GetService<UnitsSpawnMonitor>();
			m_unitsSpawnMonitor?.OnUnitAwake(this);
		}

		public void GetAllRenderers()
		{
			Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null && componentsInChildren.Length != 0)
			{
				AddRenderersToShowHide(componentsInChildren);
			}
			ParticleSystem[] componentsInChildren2 = GetComponentsInChildren<ParticleSystem>();
			if (componentsInChildren2 != null && componentsInChildren2.Length != 0)
			{
				ParticleSystem[] array = componentsInChildren2;
				foreach (ParticleSystem particleSystem in array)
				{
					if (!(particleSystem.GetComponentInParent<PlacementSpawnEffects>() != null))
					{
						allParticleEffects.Add(particleSystem);
					}
				}
			}
			if (!(WeaponHandler != null))
			{
				return;
			}
			Weapon leftWeapon = WeaponHandler.leftWeapon;
			if (leftWeapon != null)
			{
				Renderer[] componentsInChildren3 = leftWeapon.GetComponentsInChildren<Renderer>();
				if (componentsInChildren3 != null && componentsInChildren3.Length != 0)
				{
					AddRenderersToShowHide(componentsInChildren3);
				}
			}
			Weapon rightWeapon = WeaponHandler.rightWeapon;
			if (rightWeapon != null)
			{
				Renderer[] componentsInChildren4 = rightWeapon.GetComponentsInChildren<Renderer>();
				if (componentsInChildren4 != null && componentsInChildren4.Length != 0)
				{
					AddRenderersToShowHide(componentsInChildren4);
				}
			}
		}

		public void AddRenderersToShowHide(Renderer[] renderers)
		{
			if (renderers == null || renderers.Length == 0)
			{
				return;
			}
			foreach (Renderer renderer in renderers)
			{
				if (renderer != null && renderer.enabled && !cachedRenderers.Contains(renderer))
				{
					cachedRenderers.Add(renderer);
				}
			}
		}

		public void AddRenderersToShowHide(Renderer[] renderers, bool hideImmediately)
		{
			AddRenderersToShowHide(renderers);
			if (hideImmediately)
			{
				ShowRenderers(enable: false);
			}
		}

		public void ShowRenderers(bool enable)
		{
			SetRenderersEnabled(enable);
			SetParticleEffectsEnabled(enable);
			SetHealthBarActive(enable);
		}

		private void SetRenderersEnabled(bool enable)
		{
			if (cachedRenderers == null || cachedRenderers.Count <= 0)
			{
				return;
			}
			foreach (Renderer cachedRenderer in cachedRenderers)
			{
				if (cachedRenderer != null)
				{
					cachedRenderer.enabled = enable;
				}
			}
		}

		private void SetParticleEffectsEnabled(bool enable)
		{
			if (allParticleEffects == null || allParticleEffects.Count <= 0)
			{
				return;
			}
			foreach (ParticleSystem allParticleEffect in allParticleEffects)
			{
				allParticleEffect.gameObject.SetActive(enable);
			}
		}

		public override void BatchedUpdate()
		{
			hasPridictedPos = false;
		}

		public void DestroyUnit()
		{
			if (spawnedObjects != null)
			{
				for (int i = 0; i < spawnedObjects.Length; i++)
				{
					if (spawnedObjects[i] != null)
					{
						UnityEngine.Object.Destroy(spawnedObjects[i]);
					}
				}
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		protected override void Start()
		{
			base.Start();
			InitializeUnit(Team);
			m_gameModeService = ServiceLocator.GetService<GameModeService>();
			m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
			spawnEffects = GetComponentInChildren<PlacementSpawnEffects>();
			turnSpeedMultiplier = UnityEngine.Random.Range(0.9f, 1.1f);
			turnSpeed = unitBlueprint.TurnSpeed * unitBlueprint.TurnSpeedCurve.Evaluate(UnityEngine.Random.value);
			ServiceLocator.GetService<UnitHealthbars>().HandleUnitSpawned(this);
			m_unitsSpawnMonitor?.OnUnitStart(this);
		}

		public void FreezeBody()
		{
			for (int i = 0; i < spawnFreezeBodies.Length; i++)
			{
				if (spawnFreezeBodies[i] != null && spawnFreezeBodies[i].FreezeBody != null)
				{
					if (spawnFreezeBodies[i].freezeY)
					{
						spawnFreezeBodies[i].FreezeBody.constraints = RigidbodyConstraints.FreezePosition;
					}
					else
					{
						spawnFreezeBodies[i].FreezeBody.constraints = (RigidbodyConstraints)10;
					}
					if (spawnFreezeBodies[i].freezeRotation)
					{
						spawnFreezeBodies[i].FreezeBody.constraints = spawnFreezeBodies[i].FreezeBody.constraints | RigidbodyConstraints.FreezeRotation;
					}
				}
			}
		}

		public void SetHighlight(Color highlightColor)
		{
			if (m_highlighter != null)
			{
				m_highlighter.BeginHighlight();
				m_highlighter.SetHighlightColor(highlightColor);
			}
		}

		public void RemoveHighlight()
		{
			if (m_highlighter != null)
			{
				m_highlighter.EndHighlight();
			}
		}

		public void UnfreezeBody()
		{
			for (int i = 0; i < spawnFreezeBodies.Length; i++)
			{
				if ((bool)spawnFreezeBodies[i])
				{
					spawnFreezeBodies[i].FreezeBody.constraints = RigidbodyConstraints.None;
				}
			}
		}

		public void InitializeUnit(Team team)
		{
			Team = team;
			dead = false;
			dataHandler.team = team;
		}

		public void InitializeHealthBar(TABCUnitUI ui)
		{
			m_unitUI = ui;
		}

		public void SetHealthBarActive(bool active)
		{
			if (!(m_unitUI == null))
			{
				m_unitUI.gameObject.SetActive(active);
			}
		}

		public void AddAttackAction(Action<Vector3, Rigidbody, Vector3> action)
		{
			AttackAction = (Action<Vector3, Rigidbody, Vector3>)Delegate.Combine(AttackAction, action);
		}

		public void RemoveAttackAction(Action<Vector3, Rigidbody, Vector3> action)
		{
			AttackAction = (Action<Vector3, Rigidbody, Vector3>)Delegate.Remove(AttackAction, action);
		}

		public void Attack(Vector3 position, Rigidbody targetRig, Vector3 forceDirection)
		{
			if ((bool)WeaponHandler && !(attackCounter * WeaponHandler.attackSpeedMultiplier < lowestAttackTimeOfWeapons / 2f))
			{
				attackCounter = lowestAttackTimeOfWeapons * UnityEngine.Random.Range(-0.2f, 0.2f);
				WeaponHandler.Attack(position, targetRig, forceDirection);
				if (AttackAction != null)
				{
					AttackAction(position, targetRig, forceDirection);
				}
			}
		}

		public void AddWasAttackedAction(Action<Rigidbody, Rigidbody> action)
		{
			WasAttackedAction = (Action<Rigidbody, Rigidbody>)Delegate.Combine(WasAttackedAction, action);
		}

		public void RemoveWasAttackedAction(Action<Rigidbody, Rigidbody> action)
		{
			WasAttackedAction = (Action<Rigidbody, Rigidbody>)Delegate.Remove(WasAttackedAction, action);
		}

		public void WasAttacked(Rigidbody enemyWeapon, Rigidbody enemyTorso)
		{
			if (WasAttackedAction != null)
			{
				WasAttackedAction(enemyWeapon, enemyTorso);
			}
		}

		public void AddWasDamagedAction(Action<Rigidbody, Rigidbody> action)
		{
			WasDamagedAction = (Action<Rigidbody, Rigidbody>)Delegate.Combine(WasDamagedAction, action);
		}

		public void RemoveWasDamagedAction(Action<Rigidbody, Rigidbody> action)
		{
			WasDamagedAction = (Action<Rigidbody, Rigidbody>)Delegate.Remove(WasDamagedAction, action);
		}

		public bool WasDamaged(Rigidbody enemyWeapon, Rigidbody enemyTorso)
		{
			if (WasDamagedAction != null)
			{
				WasDamagedAction(enemyWeapon, enemyTorso);
			}
			return false;
		}

		public void DealDamage(float dealtDamage)
		{
			damageDealt += dealtDamage;
			DealDamageAction?.Invoke(dealtDamage);
		}

		public void WasDamaged(float damage)
		{
			WasDealtDamageAction?.Invoke(damage);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_unitsSpawnMonitor?.OnUnitDestroyed(this);
		}

		public void SetMassMultiplier(float massMultiplier)
		{
			GetComponentInChildren<RigidbodyHolder>().massMultiplier *= massMultiplier;
			if ((bool)WeaponHandler)
			{
				if ((bool)WeaponHandler.rightWeapon && (bool)unitBlueprint.RightWeapon && (bool)WeaponHandler.rightWeapon.rigidbody)
				{
					WeaponHandler.rightWeapon.rigidbody.mass *= massMultiplier;
				}
				if ((bool)WeaponHandler.leftWeapon && (bool)unitBlueprint.LeftWeapon && (bool)WeaponHandler.leftWeapon.rigidbody)
				{
					WeaponHandler.leftWeapon.rigidbody.mass *= massMultiplier;
				}
			}
		}

		private void OnDrawGizmos()
		{
			m_placementSquare.OnDrawGizmos(base.transform.position, base.transform.lossyScale, base.transform.rotation);
		}

		public void AddAttackSpeed(float delta)
		{
			attackSpeedMultiplier += delta;
			if ((bool)WeaponHandler)
			{
				WeaponHandler.SetAttackSpeedMultiplier(Mathf.Clamp(attackSpeedMultiplier, 0f, 40000f));
			}
			if (attackAnimator == null || attackAnimator.Length == 0)
			{
				return;
			}
			for (int i = 0; i < attackAnimator.Length; i++)
			{
				if (!(attackAnimator[i] == null))
				{
					attackAnimator[i].speed = attackSpeedMultiplier;
				}
			}
		}

		public void SetSpawnSource(UnitSpawnSource spawnSource)
		{
			SpawnSource = spawnSource;
		}

		public void SetIsRemotelyControlled(bool isRemotelyControlled)
		{
			IsRemotelyControlled = isRemotelyControlled;
		}

		public Transform GetNetworkSyncedTransform()
		{
			if (!(data != null) || !(data.mainRig != null))
			{
				return null;
			}
			return data.mainRig.transform;
		}

		public bool WasHitByProjectileRemotely(Projectile projectile)
		{
			if (projectile == null || m_projectileNetworkIds == null || !projectile.NetworkId.HasValue)
			{
				return false;
			}
			return m_projectileNetworkIds.Contains(projectile.NetworkId.Value);
		}

		public void OnHitByProjectileRemotely(ushort projectileNetworkId)
		{
			if (m_projectileNetworkIds == null)
			{
				m_projectileNetworkIds = new List<ushort>();
				m_projectileNetworkIds.Add(projectileNetworkId);
			}
			else if (!m_projectileNetworkIds.Contains(projectileNetworkId))
			{
				m_projectileNetworkIds.Add(projectileNetworkId);
			}
		}

		public void SetEnableSyncTransforms(bool enable)
		{
			if (EnableSyncTransforms != enable)
			{
				EnableSyncTransforms = enable;
				this.EnableSyncTransformsChanged?.Invoke(this, enable);
			}
		}
	}
}
