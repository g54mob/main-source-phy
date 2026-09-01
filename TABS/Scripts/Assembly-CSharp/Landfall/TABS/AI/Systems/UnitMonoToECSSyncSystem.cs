using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Modifiers;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.AI.Systems
{
	[UpdateInGroup(typeof(BeforeUpdate))]
	public class UnitMonoToECSSyncSystem : ComponentSystem
	{
		private struct Filter
		{
			public EntityArray Entities;

			public ComponentArray<UnitAPI> UnitAPIs;

			public ComponentDataArray<GroundPosition> GroundPositions;

			public ComponentDataArray<Health> Health;

			public ComponentDataArray<PredictedPosition> PredictedPositions;

			public ComponentDataArray<HipPosition> HipPositions;

			public ComponentDataArray<WeaponPosition> WeaponPositions;

			public ComponentDataArray<BeingTargetedBy> EnemiesTargetingMes;

			public ComponentDataArray<ConfusedMovement> ConfusedMovements;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		private struct HeroFilter
		{
			public EntityArray Entities;

			public ComponentArray<HeroAPI> HeroAPIs;

			public ComponentArray<TargetObject> TargetObject;

			public ComponentDataArray<Health> Health;

			public ComponentDataArray<HipPosition> SearchPositions;

			public ComponentDataArray<BeingTargetedBy> EnemiesTargetingMes;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		[Inject]
		private Filter m_filter;

		[Inject]
		private HeroFilter m_heroFilter;

		protected override void OnUpdate()
		{
			for (int i = 0; i < m_filter.Length; i++)
			{
				Entity entity = m_filter.Entities[i];
				UnitAPI unitAPI = m_filter.UnitAPIs[i];
				GroundPosition componentData = m_filter.GroundPositions[i];
				Health componentData2 = m_filter.Health[i];
				PredictedPosition componentData3 = m_filter.PredictedPositions[i];
				HipPosition componentData4 = m_filter.HipPositions[i];
				ConfusedMovement componentData5 = m_filter.ConfusedMovements[i];
				WeaponPosition componentData6 = m_filter.WeaponPositions[i];
				componentData.Value = unitAPI.GetGroundPosition();
				componentData2.CurrentHealth = unitAPI.GetCurrentHealth();
				componentData2.MaxHealth = unitAPI.GetMaxHealth();
				componentData3.Value = unitAPI.GetPredictedPosition();
				componentData4.Value = unitAPI.GetHipPosition();
				componentData5.Value = unitAPI.GetUnitConfusion();
				componentData6.Value = unitAPI.GetRightWeaponPosition();
				base.EntityManager.SetComponentData(entity, componentData);
				base.EntityManager.SetComponentData(entity, componentData2);
				base.EntityManager.SetComponentData(entity, componentData3);
				base.EntityManager.SetComponentData(entity, componentData4);
				base.EntityManager.SetComponentData(entity, componentData5);
				base.EntityManager.SetComponentData(entity, componentData6);
				unitAPI.IncrementAttackCounter(Time.deltaTime);
			}
			for (int j = 0; j < m_heroFilter.Length; j++)
			{
				Entity entity2 = m_heroFilter.Entities[j];
				_ = m_heroFilter.HeroAPIs[j];
				Health componentData7 = m_heroFilter.Health[j];
				HipPosition componentData8 = m_heroFilter.SearchPositions[j];
				TargetObject targetObject = m_heroFilter.TargetObject[j];
				componentData7.CurrentHealth = targetObject.CurrentHealth;
				componentData7.MaxHealth = targetObject.MaxHealth;
				componentData8.Value = targetObject.TransformToTarget.position;
				base.EntityManager.SetComponentData(entity2, componentData7);
				base.EntityManager.SetComponentData(entity2, componentData8);
			}
		}
	}
}
