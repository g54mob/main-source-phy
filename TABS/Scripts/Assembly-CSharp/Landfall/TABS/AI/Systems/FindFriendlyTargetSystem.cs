using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Systems
{
	[UpdateBefore(typeof(PreUpdateBarrier))]
	public class FindFriendlyTargetSystem : JobComponentSystem
	{
		private struct UnitFilter
		{
			public EntityArray Entities;

			[ReadOnly]
			public SharedComponentDataArray<Landfall.TABS.AI.Components.Team> Teams;

			public ComponentDataArray<GroundPosition> Positions;

			public ComponentDataArray<PredictedPosition> PredictedPositions;

			public ComponentDataArray<Range> Ranges;

			public ComponentDataArray<Health> Healths;

			public ComponentDataArray<HipPosition> SearchPositions;

			public ComponentDataArray<Direction> Directions;

			public ComponentDataArray<Velocity> Velocities;

			public ComponentDataArray<UnitTag> UnitTags;

			[ReadOnly]
			public ComponentDataArray<FriendlyLeastHealthTargeting> TargetFriendTag;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;

			public readonly int GroupIndex;
		}

		private struct TargetFilter
		{
			public EntityArray Entities;

			[ReadOnly]
			public SharedComponentDataArray<Landfall.TABS.AI.Components.Team> Teams;

			public ComponentDataArray<Range> Ranges;

			public ComponentDataArray<Health> Healths;

			public ComponentDataArray<HipPosition> SearchPositions;

			public ComponentDataArray<UnitTag> UnitTags;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;

			public readonly int GroupIndex;
		}

		private struct Job : IJobParallelFor
		{
			public EntityArray UnitEntities;

			public ComponentDataArray<PredictedPosition> UnitPositions;

			[ReadOnly]
			public EntityArray TargetEntities;

			[ReadOnly]
			public ComponentDataArray<HipPosition> TargetPositions;

			[ReadOnly]
			public ComponentDataArray<Health> TargetHealths;

			[ReadOnly]
			public ComponentDataArray<Range> TargetRanges;

			public EntityCommandBuffer.Concurrent CommandBuffer;

			[ReadOnly]
			public ComponentDataFromEntity<HasTargetTag> HasTargetTag;

			public void Execute(int index)
			{
				Entity entity = UnitEntities[index];
				PredictedPosition predictedPosition = UnitPositions[index];
				Entity entity2 = Entity.Null;
				int num = -1;
				float num2 = float.MinValue;
				float value = 0f;
				for (int i = 0; i < TargetEntities.Length; i++)
				{
					float3 value2 = TargetPositions[i].Value;
					Health health = TargetHealths[i];
					Range range = TargetRanges[i];
					float num3 = math.length(value2 - predictedPosition.Value);
					Entity entity3 = TargetEntities[i];
					if (!entity3.Equals(entity))
					{
						float maxHealth = health.MaxHealth;
						maxHealth += (health.MaxHealth - health.CurrentHealth) * 3f;
						maxHealth -= num3 * 2f;
						maxHealth -= range.AttackRange;
						if (maxHealth > num2)
						{
							num2 = maxHealth;
							entity2 = entity3;
							num = i;
						}
					}
				}
				if (num > -1)
				{
					CommandBuffer.SetComponent(index, entity, new AttackThickness
					{
						Value = value
					});
					CommandBuffer.SetComponent(index, entity, new HasTargetTag
					{
						Target = entity2
					});
					CommandBuffer.SetComponent(index, entity2, new IsTarget
					{
						Targetee = entity
					});
				}
				else
				{
					CommandBuffer.SetComponent(index, entity, new AttackThickness
					{
						Value = 0f
					});
				}
			}
		}

		[Inject]
		private UnitFilter m_filter;

		[Inject]
		private TargetFilter m_targetFilter;

		[Inject]
		private PreUpdateBarrier m_barrier;

		protected override void OnCreateManager()
		{
			base.OnCreateManager();
		}

		protected override void OnDestroyManager()
		{
			base.OnDestroyManager();
		}

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			EntityCommandBuffer entityCommandBuffer = m_barrier.CreateCommandBuffer();
			ComponentDataFromEntity<HasTargetTag> componentDataFromEntity = GetComponentDataFromEntity<HasTargetTag>(isReadOnly: true);
			ComponentGroup obj = base.ComponentGroups[m_targetFilter.GroupIndex];
			ComponentGroup componentGroup = base.ComponentGroups[m_filter.GroupIndex];
			componentGroup.SetFilter(new Landfall.TABS.AI.Components.Team
			{
				Value = 0
			});
			obj.SetFilter(new Landfall.TABS.AI.Components.Team
			{
				Value = 0
			});
			EntityArray entityArray = componentGroup.GetEntityArray();
			ComponentDataArray<PredictedPosition> componentDataArray = componentGroup.GetComponentDataArray<PredictedPosition>();
			EntityArray entityArray2 = obj.GetEntityArray();
			ComponentDataArray<HipPosition> componentDataArray2 = obj.GetComponentDataArray<HipPosition>();
			ComponentDataArray<Health> componentDataArray3 = obj.GetComponentDataArray<Health>();
			ComponentDataArray<Range> componentDataArray4 = obj.GetComponentDataArray<Range>();
			obj.ResetFilter();
			obj.SetFilter(new Landfall.TABS.AI.Components.Team
			{
				Value = 1
			});
			componentGroup.ResetFilter();
			componentGroup.SetFilter(new Landfall.TABS.AI.Components.Team
			{
				Value = 1
			});
			EntityArray entityArray3 = componentGroup.GetEntityArray();
			ComponentDataArray<PredictedPosition> componentDataArray5 = componentGroup.GetComponentDataArray<PredictedPosition>();
			EntityArray entityArray4 = obj.GetEntityArray();
			ComponentDataArray<HipPosition> componentDataArray6 = obj.GetComponentDataArray<HipPosition>();
			ComponentDataArray<Health> componentDataArray7 = obj.GetComponentDataArray<Health>();
			ComponentDataArray<Range> componentDataArray8 = obj.GetComponentDataArray<Range>();
			Job jobData = new Job
			{
				UnitEntities = entityArray,
				UnitPositions = componentDataArray,
				TargetPositions = componentDataArray2,
				TargetEntities = entityArray2,
				TargetHealths = componentDataArray3,
				TargetRanges = componentDataArray4,
				CommandBuffer = entityCommandBuffer.ToConcurrent(),
				HasTargetTag = componentDataFromEntity
			};
			return new Job
			{
				UnitEntities = entityArray3,
				UnitPositions = componentDataArray5,
				TargetEntities = entityArray4,
				TargetPositions = componentDataArray6,
				TargetHealths = componentDataArray7,
				TargetRanges = componentDataArray8,
				CommandBuffer = entityCommandBuffer.ToConcurrent(),
				HasTargetTag = componentDataFromEntity
			}.Schedule(dependsOn: jobData.Schedule(entityArray.Length, 12, inputDeps), arrayLength: entityArray3.Length, innerloopBatchCount: 12);
		}
	}
}
