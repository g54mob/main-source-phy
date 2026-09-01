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
	public class FindNearestFriendTargetSystem : JobComponentSystem
	{
		private struct UnitFilter
		{
			public EntityArray Entities;

			[ReadOnly]
			public SharedComponentDataArray<Landfall.TABS.AI.Components.Team> Teams;

			public ComponentDataArray<GroundPosition> Positions;

			public ComponentDataArray<PredictedPosition> PredictedPositions;

			public ComponentDataArray<Range> Ranges;

			public ComponentDataArray<HipPosition> SearchPositions;

			public ComponentDataArray<Direction> Directions;

			public ComponentDataArray<Velocity> Velocities;

			[ReadOnly]
			public ComponentDataArray<FindNearestFriendTargeting> UnitTargetingTypes;

			[ReadOnly]
			public ComponentDataArray<UnitType> UnitTypes;

			public ComponentDataArray<UnitTag> UnitTags;

			[ReadOnly]
			public ComponentDataArray<FindNearestFriendTargeting> TargetFriendTag;

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

			[ReadOnly]
			public ComponentDataArray<HipPosition> SearchPositions;

			[ReadOnly]
			public ComponentDataArray<UnitCost> UnitCosts;

			[ReadOnly]
			public ComponentDataArray<UnitType> UnitTypes;

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
			public ComponentDataArray<FindNearestFriendTargeting> UnitTargetingTypes;

			[ReadOnly]
			public ComponentDataArray<UnitType> UnitTypes;

			[ReadOnly]
			public EntityArray TargetEntities;

			[ReadOnly]
			public ComponentDataArray<HipPosition> TargetPositions;

			[ReadOnly]
			public ComponentDataArray<UnitCost> TargetCosts;

			[ReadOnly]
			public ComponentDataArray<Range> TargetRanges;

			[ReadOnly]
			public ComponentDataArray<UnitType> TargetUnitTypes;

			public EntityCommandBuffer.Concurrent CommandBuffer;

			[ReadOnly]
			public ComponentDataFromEntity<HasTargetTag> HasTargetTags;

			public void Execute(int index)
			{
				Entity entity = UnitEntities[index];
				PredictedPosition predictedPosition = UnitPositions[index];
				UnitType unitType = UnitTypes[index];
				FindNearestFriendTargeting findNearestFriendTargeting = UnitTargetingTypes[index];
				Entity entity2 = Entity.Null;
				int num = -1;
				float num2 = float.MinValue;
				float value = 0f;
				for (int i = 0; i < TargetEntities.Length; i++)
				{
					float num3 = math.length(TargetPositions[i].Value - predictedPosition.Value);
					Entity entity3 = TargetEntities[i];
					if (!entity3.Equals(entity))
					{
						Range range = TargetRanges[i];
						_ = TargetCosts[i];
						float num4 = 0f - num3;
						num4 -= num3 * 2f;
						num4 += range.AttackRange * 0.5f;
						if (unitType.Value == TargetUnitTypes[i].Value)
						{
							num4 -= 100000f;
						}
						if (findNearestFriendTargeting.PrioritizeMount == 1 && TargetUnitTypes[i].IsRider == 1)
						{
							num4 -= 20f;
						}
						if (num4 > num2)
						{
							num = i;
							num2 = num4;
							entity2 = entity3;
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
			ComponentGroup componentGroup = base.ComponentGroups[m_targetFilter.GroupIndex];
			ComponentGroup obj = base.ComponentGroups[m_filter.GroupIndex];
			ComponentDataFromEntity<HasTargetTag> componentDataFromEntity = GetComponentDataFromEntity<HasTargetTag>(isReadOnly: true);
			obj.SetFilter(new Landfall.TABS.AI.Components.Team
			{
				Value = 0
			});
			componentGroup.SetFilter(new Landfall.TABS.AI.Components.Team
			{
				Value = 0
			});
			EntityArray entityArray = obj.GetEntityArray();
			ComponentDataArray<PredictedPosition> componentDataArray = obj.GetComponentDataArray<PredictedPosition>();
			ComponentDataArray<UnitType> componentDataArray2 = obj.GetComponentDataArray<UnitType>();
			ComponentDataArray<FindNearestFriendTargeting> componentDataArray3 = obj.GetComponentDataArray<FindNearestFriendTargeting>();
			EntityArray entityArray2 = componentGroup.GetEntityArray();
			ComponentDataArray<HipPosition> componentDataArray4 = componentGroup.GetComponentDataArray<HipPosition>();
			ComponentDataArray<Range> componentDataArray5 = componentGroup.GetComponentDataArray<Range>();
			ComponentDataArray<UnitCost> componentDataArray6 = componentGroup.GetComponentDataArray<UnitCost>();
			ComponentDataArray<UnitType> componentDataArray7 = componentGroup.GetComponentDataArray<UnitType>();
			componentGroup.ResetFilter();
			componentGroup.SetFilter(new Landfall.TABS.AI.Components.Team
			{
				Value = 1
			});
			obj.ResetFilter();
			obj.SetFilter(new Landfall.TABS.AI.Components.Team
			{
				Value = 1
			});
			EntityArray entityArray3 = obj.GetEntityArray();
			ComponentDataArray<PredictedPosition> componentDataArray8 = obj.GetComponentDataArray<PredictedPosition>();
			ComponentDataArray<UnitType> componentDataArray9 = obj.GetComponentDataArray<UnitType>();
			ComponentDataArray<FindNearestFriendTargeting> componentDataArray10 = obj.GetComponentDataArray<FindNearestFriendTargeting>();
			EntityArray entityArray4 = componentGroup.GetEntityArray();
			ComponentDataArray<HipPosition> componentDataArray11 = componentGroup.GetComponentDataArray<HipPosition>();
			ComponentDataArray<Range> componentDataArray12 = componentGroup.GetComponentDataArray<Range>();
			ComponentDataArray<UnitCost> componentDataArray13 = componentGroup.GetComponentDataArray<UnitCost>();
			ComponentDataArray<UnitType> componentDataArray14 = componentGroup.GetComponentDataArray<UnitType>();
			Job jobData = new Job
			{
				UnitEntities = entityArray,
				UnitPositions = componentDataArray,
				UnitTypes = componentDataArray2,
				UnitTargetingTypes = componentDataArray3,
				TargetPositions = componentDataArray4,
				TargetEntities = entityArray2,
				TargetRanges = componentDataArray5,
				TargetCosts = componentDataArray6,
				TargetUnitTypes = componentDataArray7,
				CommandBuffer = entityCommandBuffer.ToConcurrent(),
				HasTargetTags = componentDataFromEntity
			};
			return new Job
			{
				UnitEntities = entityArray3,
				UnitPositions = componentDataArray8,
				UnitTypes = componentDataArray9,
				UnitTargetingTypes = componentDataArray10,
				TargetEntities = entityArray4,
				TargetPositions = componentDataArray11,
				TargetRanges = componentDataArray12,
				TargetCosts = componentDataArray13,
				TargetUnitTypes = componentDataArray14,
				CommandBuffer = entityCommandBuffer.ToConcurrent(),
				HasTargetTags = componentDataFromEntity
			}.Schedule(dependsOn: jobData.Schedule(entityArray.Length, 12, inputDeps), arrayLength: entityArray3.Length, innerloopBatchCount: 12);
		}
	}
}
