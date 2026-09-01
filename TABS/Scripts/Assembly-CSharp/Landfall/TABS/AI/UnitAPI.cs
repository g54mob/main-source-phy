using System;
using System.Reflection;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Events;
using Landfall.TABS.AI.Components.Modifiers;
using Landfall.TABS.AI.Components.Pathfinding;
using Landfall.TABS.AI.Components.Tags;
using Landfall.TABS.AI.Systems;
using Pathfinding;
using TFBGames;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABS.AI
{
	public class UnitAPI : MonoBehaviour, IRemotelyControllable
	{
		public delegate void SetAttackTargetDelegate(Unit unit, Vector3 targetPosition, Rigidbody targetMainRigidbody, DataHandler targetDataHandler, TargetData targetData, bool canSeeTarget);

		[HideInInspector]
		public bool forceSupressFromWinCondition;

		[SerializeField]
		private LayerMask m_raycastMask;

		private float m_canSeeAttackCooldown = 0.5f;

		private Unit m_unit;

		private GeneralInput m_input;

		private DataHandler m_unitData;

		private MovementHandler m_movementHandler;

		private GameObjectEntity m_goEntity;

		private int m_perlinOffset;

		private static int GLOBAL_OFFSET;

		private Path m_path;

		private bool m_isRangedUnit;

		private ITargetingComponent m_currentTargetingType;

		private ITargetingComponent m_defaultTargetingType;

		public bool Active { get; private set; } = true;

		public bool IsPossessed { get; private set; }

		public bool IsRangedUnit => m_isRangedUnit;

		public ITargetingComponent CurrentTargetingType => m_currentTargetingType;

		public ITargetingComponent DefaultTargetingType => m_defaultTargetingType;

		public bool IsRemotelyControlled { get; private set; }

		public event SetAttackTargetDelegate AttackTargetSet;

		public event Action<Unit, Vector3> LookDirectionSet;

		public event Action<Unit, float> MovementSpeedSet;

		private void Awake()
		{
			m_unit = GetComponent<Unit>();
			m_unitData = m_unit.data;
			m_input = m_unit.Input;
			m_goEntity = GetComponent<GameObjectEntity>();
			m_movementHandler = GetComponent<MovementHandler>();
			m_perlinOffset = GLOBAL_OFFSET++;
		}

		private void Start()
		{
			if (!m_unit.isUnitCopy)
			{
				m_goEntity = base.gameObject.AddComponent<GameObjectEntity>();
			}
			InitializeECSUnit();
		}

		public void SetActive(bool active, bool? isPossessed = null)
		{
			Active = active;
			if (isPossessed.HasValue)
			{
				IsPossessed = isPossessed.Value;
			}
		}

		public void SetIsRider(bool isRider, bool setComponentData = true)
		{
			if (setComponentData)
			{
				UnitType componentData = m_goEntity.EntityManager.GetComponentData<UnitType>(m_goEntity.Entity);
				componentData.IsRider = (isRider ? 1 : 0);
				m_goEntity.EntityManager.SetComponentData(m_goEntity.Entity, componentData);
			}
			if (m_unit != null)
			{
				m_unit.IsRider = isRider;
			}
		}

		public void SetIsDead()
		{
			if (m_goEntity == null)
			{
				Debug.LogError("GoEntity is null!");
			}
			else if (m_goEntity.EntityManager == null)
			{
				Debug.LogError("EntityManager is null!");
			}
			else
			{
				m_goEntity.EntityManager.AddComponentData(m_goEntity.Entity, default(IsDead));
			}
		}

		public void SetIsInPool()
		{
			if (m_goEntity == null)
			{
				Debug.LogError("GoEntity is null!");
			}
			else if (m_goEntity.EntityManager == null)
			{
				Debug.LogError("EntityManager is null!");
			}
			else
			{
				m_goEntity.EntityManager.AddComponentData(m_goEntity.Entity, default(IsInPool));
			}
		}

		public void SetIsOutOfPool()
		{
			if (m_goEntity == null)
			{
				Debug.LogError("GoEntity is null!");
			}
			else if (m_goEntity.EntityManager == null)
			{
				Debug.LogError("EntityManager is null!");
			}
			else if (m_goEntity.EntityManager.HasComponent<IsInPool>(m_goEntity.Entity))
			{
				m_goEntity.EntityManager.RemoveComponent<IsInPool>(m_goEntity.Entity);
			}
		}

		public void SetCanSeeTarget(bool canSee)
		{
			m_unitData.CanSeeTarget = canSee;
		}

		private void InitializeECSUnit()
		{
			if (World.Active == null)
			{
				Debug.LogError("Could not find an active ECS World, unit initialization failed", base.gameObject);
				return;
			}
			m_goEntity = base.gameObject.GetComponent<GameObjectEntity>();
			EntityManager entityManager = m_goEntity.EntityManager;
			if (entityManager == null)
			{
				Debug.LogError("Could not find an active ECS World, unit initialization failed", base.gameObject);
				return;
			}
			Entity entity = m_goEntity.Entity;
			entityManager.AddComponentData(entity, new Health
			{
				CurrentHealth = GetCurrentHealth(),
				MaxHealth = GetMaxHealth()
			});
			entityManager.AddSharedComponentData(entity, new Landfall.TABS.AI.Components.Team
			{
				Value = (int)m_unit.Team
			});
			entityManager.AddComponentData(entity, new Range
			{
				AttackRange = GetAttackRange(),
				PreferredRange = GetPrefferedRange()
			});
			entityManager.AddComponentData(entity, new GroundPosition
			{
				Value = GetGroundPosition()
			});
			entityManager.AddComponentData(entity, new PredictedPosition
			{
				Value = GetPredictedPosition()
			});
			entityManager.AddComponentData(entity, new HipPosition
			{
				Value = GetHipPosition()
			});
			entityManager.AddComponentData(entity, new WeaponPosition
			{
				Value = GetRightWeaponPosition()
			});
			entityManager.AddComponentData(entity, new HasTargetTag
			{
				Target = Entity.Null
			});
			entityManager.AddComponentData(entity, new IsTarget
			{
				Targetee = Entity.Null
			});
			entityManager.AddComponentData(entity, new TargetData
			{
				DistanceToTarget = float.MaxValue,
				TargetInAttackRange = 0,
				TargetInPreferredRange = 0
			});
			entityManager.AddComponentData(entity, new Velocity
			{
				Value = new float3(0f, 0f, 0f)
			});
			entityManager.AddComponentData(entity, new Direction
			{
				Value = m_unit.transform.root.forward
			});
			entityManager.AddComponentData(entity, new ConfusedMovement
			{
				Value = m_unit.unitConfusion
			});
			entityManager.AddComponentData(entity, new TargetPriority
			{
				Value = m_unit.targetingPriorityMultiplier
			});
			entityManager.AddComponentData(entity, new BeingTargetedBy
			{
				Value = 0f
			});
			entityManager.AddComponentData(entity, new CanSeeTarget
			{
				CanSee = 0,
				RaycastMask = m_raycastMask
			});
			entityManager.AddComponentData(entity, new TargetThickness
			{
				Value = m_unit.thickness
			});
			entityManager.AddComponentData(entity, new AttackThickness
			{
				Value = 0f
			});
			entityManager.AddComponentData(entity, new UnitCost
			{
				Value = m_unit.unitBlueprint.GetUnitCost()
			});
			entityManager.AddComponentData(entity, new PathDistance
			{
				Distance = 0f
			});
			entityManager.AddComponentData(entity, new CanSeeAttackCooldown
			{
				Value = 0.5f
			});
			entityManager.AddComponentData(entity, new Navmesh
			{
				Value = m_unit.unitBlueprint.NavmeshType
			});
			Mount component = m_unit.GetComponent<Mount>();
			int num = 0;
			if ((bool)component)
			{
				num = 1;
			}
			entityManager.AddComponentData(entity, new UnitType
			{
				Value = m_unit.unitBlueprint.Entity.GUID,
				IsRider = num
			});
			SetIsRider(num == 1, setComponentData: false);
			float currentRate = UnityEngine.Random.Range(0f, 0.5f);
			entityManager.AddBuffer<PathPoint>(entity);
			entityManager.AddComponentData(entity, new PathSettings
			{
				RepathRate = 0.5f,
				CurrentRate = currentRate
			});
			entityManager.AddComponentData(entity, new CurrentWaypoint
			{
				Value = 0
			});
			entityManager.AddComponent(entity, ComponentType.Create<UnitTag>());
			if (m_unit.unitBlueprint.MovementComponents == null)
			{
				Debug.LogError("Missing Movement Behaviour on unit: " + m_unit.unitBlueprint.Name);
			}
			else
			{
				for (int i = 0; i < m_unit.unitBlueprint.MovementComponents.Count; i++)
				{
					IMovementComponent movementComponent = m_unit.unitBlueprint.MovementComponents[i];
					Type type = movementComponent.GetType();
					if (type == typeof(KeepRangedDistance))
					{
						m_isRangedUnit = true;
					}
					CreateGenericAddcomponentData(type).Invoke(entityManager, new object[2] { entity, movementComponent });
				}
			}
			Type type2 = m_unit.unitBlueprint.TargetingComponent.GetType();
			CreateGenericAddcomponentData(type2).Invoke(entityManager, new object[2]
			{
				entity,
				m_unit.unitBlueprint.TargetingComponent
			});
			m_currentTargetingType = m_unit.unitBlueprint.TargetingComponent;
			m_defaultTargetingType = m_unit.unitBlueprint.TargetingComponent;
			bool excludeFromWinCondition = m_unit.unitBlueprint.excludeFromWinCondition;
			if (forceSupressFromWinCondition)
			{
				excludeFromWinCondition = true;
			}
			World.Active.GetOrCreateManager<TeamSystem>().AddUnit(entity, base.gameObject, base.gameObject.transform, m_unitData.mainRig, m_unitData, m_unit.Team, m_unit, excludeFromWinCondition);
		}

		public void UpdateECSValues()
		{
			if (World.Active == null)
			{
				Debug.LogError("Could not find an active ECS World, unit initialization failed", base.gameObject);
				return;
			}
			m_goEntity = base.gameObject.GetComponent<GameObjectEntity>();
			EntityManager entityManager = m_goEntity.EntityManager;
			if (entityManager == null)
			{
				Debug.LogError("Could not find an active ECS World, unit initialization failed", base.gameObject);
				return;
			}
			Entity entity = m_goEntity.Entity;
			entityManager.SetComponentData(entity, new Health
			{
				CurrentHealth = GetCurrentHealth(),
				MaxHealth = GetMaxHealth()
			});
			entityManager.SetComponentData(entity, new Range
			{
				AttackRange = GetAttackRange(),
				PreferredRange = GetPrefferedRange()
			});
			entityManager.SetComponentData(entity, new GroundPosition
			{
				Value = GetGroundPosition()
			});
			entityManager.SetComponentData(entity, new PredictedPosition
			{
				Value = GetPredictedPosition()
			});
			entityManager.SetComponentData(entity, new HipPosition
			{
				Value = GetHipPosition()
			});
			entityManager.SetComponentData(entity, new TargetPriority
			{
				Value = m_unit.targetingPriorityMultiplier
			});
			entityManager.SetComponentData(entity, new TargetThickness
			{
				Value = m_unit.thickness
			});
		}

		private MethodInfo CreateGenericAddcomponentData(Type componentType)
		{
			return typeof(EntityManager).GetMethod("AddComponentData").MakeGenericMethod(componentType);
		}

		private MethodInfo CreateGenericRemoveComponentData(Type componentType)
		{
			return typeof(EntityManager).GetMethod("RemoveComponent", new Type[1] { typeof(Entity) }).MakeGenericMethod(componentType);
		}

		private void OnDestroy()
		{
			World active = World.Active;
			if (active != null)
			{
				TeamSystem orCreateManager = active.GetOrCreateManager<TeamSystem>();
				if (m_goEntity != null)
				{
					orCreateManager?.RemoveEntity(m_goEntity.Entity, m_unit.Team, m_unit);
				}
			}
		}

		public void SetTargetingType(ITargetingComponent newTargetType)
		{
			m_goEntity.EntityManager.SetComponentData(m_goEntity.Entity, new HasTargetTag
			{
				Target = Entity.Null
			});
			CreateGenericRemoveComponentData(CurrentTargetingType.GetType()).Invoke(m_goEntity.EntityManager, new object[1] { m_goEntity.Entity });
			CreateGenericAddcomponentData(newTargetType.GetType()).Invoke(m_goEntity.EntityManager, new object[2] { m_goEntity.Entity, newTargetType });
			m_currentTargetingType = newTargetType;
		}

		public float GetAttackRange()
		{
			return m_unit.m_AttackDistance;
		}

		public float GetPrefferedRange()
		{
			return m_unit.m_PreferedDistance;
		}

		public float GetCurrentHealth()
		{
			return m_unitData.health;
		}

		public float GetMaxHealth()
		{
			return m_unitData.maxHealth;
		}

		public Vector3 GetGroundPosition()
		{
			return m_unitData.groundPosition;
		}

		public Vector3 GetPredictedPosition()
		{
			return m_unit.PredicedPosition;
		}

		public Vector3 GetHipPosition()
		{
			if (m_unitData != null && m_unitData.mainRig != null)
			{
				return m_unitData.mainRig.transform.position;
			}
			m_unitData = m_unit.data;
			if (m_unitData.mainRig != null)
			{
				return m_unitData.mainRig.transform.position;
			}
			return m_unitData.transform.root.position;
		}

		public Vector3 GetUnitConfusion()
		{
			return m_unit.unitConfusion;
		}

		public Vector3 GetRightWeaponPosition()
		{
			if (m_isRangedUnit)
			{
				if (!(m_unit.data.weaponHandler == null))
				{
					if (!(m_unit.data.weaponHandler.rightWeapon == null))
					{
						return m_unit.data.weaponHandler.rightWeapon.transform.position;
					}
					return GetHipPosition();
				}
				return GetHipPosition();
			}
			return GetHipPosition();
		}

		public void SetLookDirection(Vector3 localDirection, bool forceSet = false)
		{
			this.LookDirectionSet?.Invoke(m_unit, localDirection);
			if (Active || forceSet)
			{
				m_unitData.lookDirectionIntent = localDirection;
				m_unitData.angleToTarget = Vector3.Angle(m_unitData.characterForwardObject.forward, localDirection);
				float num = Mathf.PerlinNoise(Time.time + (float)m_perlinOffset * 100f, Time.time + (float)m_perlinOffset * 100f) * Mathf.PerlinNoise(Time.time + (float)(m_perlinOffset * 100), Time.time + (float)(m_perlinOffset * 100)) * 2f;
				num /= 6f;
				float maxDegreesDelta = m_unit.turnSpeed * num * m_unit.turnSpeedMultiplier * Time.deltaTime * 100f;
				if (!(localDirection == Vector3.zero))
				{
					m_input.SetRotation(Quaternion.RotateTowards(m_input.Direction.rotation, Quaternion.LookRotation(localDirection, Vector3.up), maxDegreesDelta));
				}
			}
		}

		public void SetMovementSpeed(float movementSpeed, bool forceSet = false)
		{
			this.MovementSpeedSet?.Invoke(m_unit, movementSpeed);
			if (Active || forceSet)
			{
				m_input.inputDirection = new Vector3(0f, 0f, movementSpeed);
			}
		}

		public void IncrementAttackCounter(float deltaTime)
		{
			m_unit.attackCounter += deltaTime;
		}

		public void DecrementInRangeAttackCounter(float deltaTime)
		{
			m_unit.InRangeAttackCooldown--;
		}

		public void ResetInRangeAttackCounter()
		{
			m_unit.InRangeAttackCooldown = m_unit.MaxInRangeAttackCooldown;
		}

		public void SetAttackTarget(float3 targetPos, Rigidbody mainRig, DataHandler unitData, TargetData targetData, bool canSeeTarget, bool startAttack = true)
		{
			bool flag = targetData.TargetInAttackRange == 1;
			m_unitData.targetMainRig = mainRig;
			m_unitData.targetData = unitData;
			m_unitData.distanceToTarget = targetData.DistanceToTarget;
			m_unitData.inRange = targetData.TargetInAttackRange == 1;
			this.AttackTargetSet?.Invoke(m_unit, targetPos, mainRig, unitData, targetData, canSeeTarget);
			if (Active && startAttack && canSeeTarget && flag && m_unit.InRangeAttackCooldown <= 0f)
			{
				_ = Vector3.zero;
				if ((bool)unitData)
				{
					targetPos = unitData.torso.position;
				}
				else if ((bool)m_unit.data.mainRig)
				{
					_ = m_unit.data.mainRig.transform.forward;
				}
				m_unit.Attack(targetPos, mainRig, Vector3.zero);
			}
		}

		public void SetIsRemotelyControlled(bool isRemotelyControlled)
		{
			IsRemotelyControlled = isRemotelyControlled;
			SetActive(!IsRemotelyControlled && !IsPossessed);
		}
	}
}
