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
	public class FindFriendlyHighestPriceTargetSystem : JobComponentSystem
	{
		private struct UnitFilter
		{
			public EntityArray Entities;

			[ReadOnly]
			public SharedComponentDataArray<Landfall.TABS.AI.Components.Team> Teams;

			public ComponentDataArray<GroundPosition> Positions;

			public ComponentDataArray<PredictedPosition> PredictedPositions;

			public ComponentDataArray<Range> Ranges;

			[ReadOnly]
			public ComponentDataArray<UnitType> UnitTypes;

			public ComponentDataArray<UnitTag> UnitTags;

			[ReadOnly]
			public ComponentDataArray<FriendlyHighestPriceTargeting> TargetFriendTag;

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

			public SubtractiveComponent<IsDead> _IsDead;

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
			public ComponentDataFromEntity<HasTargetTag> HasTargetTag;

			public void Execute(int index)
			{
				Entity entity = UnitEntities[index];
				PredictedPosition predictedPosition = UnitPositions[index];
				UnitType unitType = UnitTypes[index];
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
						float value2 = TargetCosts[i].Value;
						value2 -= num3 * 2f;
						if (unitType.Value == TargetUnitTypes[i].Value)
						{
							value2 -= 100000f;
						}
						if (value2 > num2)
						{
							num2 = value2;
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
			ComponentDataArray<UnitType> componentDataArray2 = componentGroup.GetComponentDataArray<UnitType>();
			EntityArray entityArray2 = obj.GetEntityArray();
			ComponentDataArray<HipPosition> componentDataArray3 = obj.GetComponentDataArray<HipPosition>();
			ComponentDataArray<Range> componentDataArray4 = obj.GetComponentDataArray<Range>();
			ComponentDataArray<UnitCost> componentDataArray5 = obj.GetComponentDataArray<UnitCost>();
			ComponentDataArray<UnitType> componentDataArray6 = obj.GetComponentDataArray<UnitType>();
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
			ComponentDataArray<PredictedPosition> componentDataArray7 = componentGroup.GetComponentDataArray<PredictedPosition>();
			ComponentDataArray<UnitType> componentDataArray8 = componentGroup.GetComponentDataArray<UnitType>();
			EntityArray entityArray4 = obj.GetEntityArray();
			ComponentDataArray<HipPosition> componentDataArray9 = obj.GetComponentDataArray<HipPosition>();
			ComponentDataArray<Range> componentDataArray10 = obj.GetComponentDataArray<Range>();
			ComponentDataArray<UnitCost> componentDataArray11 = obj.GetComponentDataArray<UnitCost>();
			ComponentDataArray<UnitType> componentDataArray12 = obj.GetComponentDataArray<UnitType>();
			Job jobData = new Job
			{
				UnitEntities = entityArray,
				UnitPositions = componentDataArray,
				UnitTypes = componentDataArray2,
				TargetPositions = componentDataArray3,
				TargetEntities = entityArray2,
				TargetRanges = componentDataArray4,
				TargetCosts = componentDataArray5,
				TargetUnitTypes = componentDataArray6,
				CommandBuffer = entityCommandBuffer.ToConcurrent(),
				HasTargetTag = componentDataFromEntity
			};
			return new Job
			{
				UnitEntities = entityArray3,
				UnitPositions = componentDataArray7,
				UnitTypes = componentDataArray8,
				TargetEntities = entityArray4,
				TargetPositions = componentDataArray9,
				TargetRanges = componentDataArray10,
				TargetCosts = componentDataArray11,
				TargetUnitTypes = componentDataArray12,
				CommandBuffer = entityCommandBuffer.ToConcurrent(),
				HasTargetTag = componentDataFromEntity
			}.Schedule(dependsOn: jobData.Schedule(entityArray.Length, 12, inputDeps), arrayLength: entityArray3.Length, innerloopBatchCount: 12);
		}
	}
}
