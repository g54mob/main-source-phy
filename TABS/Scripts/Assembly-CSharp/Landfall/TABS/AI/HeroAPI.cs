using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Tags;
using Landfall.TABS.AI.Systems;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.AI
{
	public class HeroAPI : MonoBehaviour
	{
		private GameObjectEntity m_goEntity;

		private TargetObject m_targetObject;

		private void Awake()
		{
			m_goEntity = GetComponent<GameObjectEntity>();
			m_targetObject = GetComponent<TargetObject>();
		}

		private void Start()
		{
			InitializeECSUnit();
		}

		private void InitializeECSUnit()
		{
			if (World.Active == null)
			{
				Debug.LogError("Could not find an active ECS World, Hero API initialization failed", base.gameObject);
				return;
			}
			EntityManager entityManager = m_goEntity.EntityManager;
			if (entityManager == null)
			{
				Debug.LogError("Could not find an active ECS World, Hero API initialization failed", base.gameObject);
				return;
			}
			Entity entity = m_goEntity.Entity;
			entityManager.AddSharedComponentData(entity, new Landfall.TABS.AI.Components.Team
			{
				Value = (int)m_targetObject.Team
			});
			entityManager.AddComponentData(entity, new HipPosition
			{
				Value = m_targetObject.TransformToTarget.position
			});
			entityManager.AddComponentData(entity, new GroundPosition
			{
				Value = base.transform.position
			});
			entityManager.AddComponentData(entity, new PredictedPosition
			{
				Value = base.transform.position
			});
			entityManager.AddComponentData(entity, new TargetPriority
			{
				Value = m_targetObject.TargetPriority
			});
			entityManager.AddComponentData(entity, new TargetThickness
			{
				Value = m_targetObject.Thickness
			});
			entityManager.AddComponentData(entity, new AttackThickness
			{
				Value = m_targetObject.Thickness
			});
			entityManager.AddComponentData(entity, new BeingTargetedBy
			{
				Value = 0f
			});
			entityManager.AddComponent(entity, ComponentType.Create<UnitTag>());
			entityManager.AddComponentData(entity, new IsTarget
			{
				Targetee = Entity.Null
			});
			World.Active.GetOrCreateManager<TeamSystem>().AddUnit(entity, base.gameObject, base.gameObject.transform, GetComponentInChildren<Rigidbody>(), null, m_targetObject.Team, null, excludeFromWinCondition: false);
		}

		private void OnDestroy()
		{
			if (!(m_goEntity == null) && m_goEntity.EntityManager != null && m_goEntity.EntityManager.IsCreated)
			{
				World.Active.GetOrCreateManager<TeamSystem>().RemoveEntity(m_goEntity.Entity, m_targetObject.Team, null);
			}
		}

		private void Update()
		{
			EntityManager entityManager = m_goEntity.EntityManager;
			Entity entity = m_goEntity.Entity;
			entityManager.SetComponentData(entity, new HipPosition
			{
				Value = m_targetObject.TransformToTarget.position
			});
			entityManager.SetComponentData(entity, new PredictedPosition
			{
				Value = base.transform.position
			});
			entityManager.SetComponentData(entity, new GroundPosition
			{
				Value = base.transform.position
			});
		}
	}
}
