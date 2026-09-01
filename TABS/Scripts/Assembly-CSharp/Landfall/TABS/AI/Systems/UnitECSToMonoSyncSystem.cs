using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Modifiers;
using Landfall.TABS.AI.Components.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABS.AI.Systems
{
	[UpdateAfter(typeof(PreLateUpdateBarrier))]
	[UpdateBefore(typeof(PostLateUpdateBarrier))]
	public class UnitECSToMonoSyncSystem : ComponentSystem
	{
		private struct Filter
		{
			public EntityArray Entities;

			public ComponentArray<UnitAPI> UnitAPIs;

			[ReadOnly]
			public ComponentDataArray<Direction> Directions;

			[ReadOnly]
			public ComponentDataArray<Velocity> Velocities;

			[ReadOnly]
			public ComponentDataArray<CanSeeTarget> CanSeeTargets;

			[ReadOnly]
			public ComponentDataArray<AttackThickness> AttackThicknesses;

			[ReadOnly]
			public ComponentDataArray<Range> Ranges;

			[ReadOnly]
			public ComponentDataArray<HipPosition> Positions;

			[ReadOnly]
			public ComponentDataArray<CanSeeAttackCooldown> CanSeeCooldowns;

			[ReadOnly]
			public ComponentDataArray<TargetData> TargetDatas;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		[Inject]
		private Filter m_filter;

		[Inject]
		private TeamSystem m_teamSystem;

		[Inject]
		private PreLateUpdateBarrier m_barrier;

		private ComponentDataFromEntity<HasTargetTag> m_hasTargetDataFromEntity;

		private static float MAX_CANSEE_VALUE = 1f;

		protected override void OnCreateManager()
		{
			base.OnCreateManager();
		}

		protected override void OnStartRunning()
		{
			base.OnStartRunning();
		}

		protected override void OnUpdate()
		{
			ComponentDataFromEntity<GroundPosition> componentDataFromEntity = GetComponentDataFromEntity<GroundPosition>(isReadOnly: true);
			ComponentDataFromEntity<KeepRangedDistance> componentDataFromEntity2 = GetComponentDataFromEntity<KeepRangedDistance>(isReadOnly: true);
			m_barrier.CreateCommandBuffer();
			for (int num = 0; num < m_filter.Length; num++)
			{
				Entity entity = m_filter.Entities[num];
				HasTargetTag hasTargetTag = m_filter.HasTargetTags[num];
				UnitAPI unitAPI = m_filter.UnitAPIs[num];
				Direction componentData = m_filter.Directions[num];
				Velocity velocity = m_filter.Velocities[num];
				_ = m_filter.AttackThicknesses[num];
				CanSeeTarget canSeeTarget = m_filter.CanSeeTargets[num];
				Range range = m_filter.Ranges[num];
				HipPosition hipPosition = m_filter.Positions[num];
				TargetData targetData = m_filter.TargetDatas[num];
				CanSeeAttackCooldown componentData2 = m_filter.CanSeeCooldowns[num];
				int num2;
				if (m_filter.CanSeeTargets[num].CanSee != 1)
				{
					num2 = 0;
					if (num2 == 0)
					{
						componentData2.Value -= Time.deltaTime;
						goto IL_0134;
					}
				}
				else
				{
					num2 = 1;
				}
				componentData2.Value += Time.deltaTime;
				goto IL_0134;
				IL_0134:
				componentData2.Value = Mathf.Clamp(componentData2.Value, 0f, MAX_CANSEE_VALUE);
				bool canSeeTarget2 = (byte)num2 != 0;
				bool flag = componentData2.Value > MAX_CANSEE_VALUE * 0.5f;
				if (!componentDataFromEntity2.Exists(entity))
				{
					flag = true;
				}
				base.EntityManager.SetComponentData(entity, componentData2);
				if (hasTargetTag.Target != Entity.Null && componentDataFromEntity.Exists(hasTargetTag.Target))
				{
					GroundPosition groundPosition = componentDataFromEntity[hasTargetTag.Target];
					if (targetData.TargetInPreferredRange == 1 && canSeeTarget.CanSee == 1 && range.AttackRange >= 5f)
					{
						componentData.Value = math.normalize(groundPosition.Value - hipPosition.Value);
						base.EntityManager.SetComponentData(entity, componentData);
					}
					if (!unitAPI.IsRemotelyControlled)
					{
						unitAPI.SetLookDirection(componentData.Value);
						unitAPI.SetMovementSpeed(velocity.Value.z);
					}
					unitAPI.SetCanSeeTarget(canSeeTarget2);
					if (targetData.TargetInAttackRange == 1)
					{
						unitAPI.DecrementInRangeAttackCounter(Time.deltaTime);
					}
					else
					{
						unitAPI.ResetInRangeAttackCounter();
					}
					if (!unitAPI.IsRemotelyControlled)
					{
						Rigidbody mainRig = m_teamSystem.GetMainRig(hasTargetTag.Target);
						DataHandler dataHandler = m_teamSystem.GetDataHandler(hasTargetTag.Target);
						if (mainRig != null && flag)
						{
							unitAPI.SetAttackTarget(mainRig.position, mainRig, dataHandler, targetData, canSeeTarget2);
						}
					}
				}
			}
		}
	}
}
