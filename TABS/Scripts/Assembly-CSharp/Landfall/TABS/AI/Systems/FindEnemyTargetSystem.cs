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
	public class FindEnemyTargetSystem : JobComponentSystem
	{
		private struct UnitFilter
		{
			public EntityArray Entities;

			[ReadOnly]
			public SharedComponentDataArray<Landfall.TABS.AI.Components.Team> Teams;

			public ComponentDataArray<GroundPosition> Positions;

			public ComponentDataArray<PredictedPosition> PredictedPositions;

			public ComponentDataArray<Range> Ranges;

			public ComponentDataArray<UnitTag> UnitTags;

			[ReadOnly]
			public ComponentDataArray<EnemyLeastWeightTargeting> TargetEnemyTag;

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

			public ComponentDataArray<HipPosition> Position;

			[ReadOnly]
			public ComponentDataArray<PredictedPosition> PredictedPositions;

			[ReadOnly]
			public ComponentDataArray<TargetPriority> TargetPriorities;

			[ReadOnly]
			public ComponentDataArray<TargetThickness> TargetThicknesses;

			public ComponentDataArray<BeingTargetedBy> EnemiesTargetingMes;

			public ComponentDataArray<UnitTag> _UnitTag;

			public SubtractiveComponent<IsDead> _IsDead;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;

			public readonly int GroupIndex;
		}

		private struct Job : IJob
		{
			public EntityArray UnitEntities;

			public ComponentDataArray<PredictedPosition> UnitPositions;

			public ComponentDataArray<Range> UnitRanges;

			[ReadOnly]
			public EntityArray TargetEntities;

			[ReadOnly]
			public ComponentDataArray<HipPosition> TargetPositions;

			[ReadOnly]
			public ComponentDataArray<TargetPriority> TargetPriorities;

			[ReadOnly]
			public ComponentDataArray<TargetThickness> TargetThicknesses;

			public NativeArray<BeingTargetedBy> TargetsBeingTargetedBy;

			public EntityCommandBuffer.Concurrent CommandBuffer;

			[ReadOnly]
			public ComponentDataFromEntity<HasTargetTag> HasTargetTag;

			[ReadOnly]
			public ComponentDataFromEntity<IsTarget> IsTargetTag;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public void Execute()
			{
				for (int i = 0; i < UnitEntities.Length; i++)
				{
					Entity entity = UnitEntities[i];
					PredictedPosition predictedPosition = UnitPositions[i];
					Range range = UnitRanges[i];
					Entity entity2 = Entity.Null;
					int num = -1;
					float num2 = float.MinValue;
					float value = 0f;
					for (int j = 0; j < TargetEntities.Length; j++)
					{
						float num3 = math.length(TargetPositions[j].Value - predictedPosition.Value);
						Entity entity3 = TargetEntities[j];
						float value2 = TargetsBeingTargetedBy[j].Value;
						float num4 = (0f - num3) / TargetPriorities[j].Value - value2 * range.AttackRange * 0.05f;
						if (num4 > num2)
						{
							num2 = num4;
							entity2 = entity3;
							num = j;
						}
					}
					if (num > -1)
					{
						CommandBuffer.SetComponent(i, entity, new AttackThickness
						{
							Value = value
						});
						BeingTargetedBy value3 = TargetsBeingTargetedBy[num];
						value3.Value += 1f;
						TargetsBeingTargetedBy[num] = value3;
						CommandBuffer.SetComponent(i, entity, new HasTargetTag
						{
							Target = entity2
						});
						CommandBuffer.SetComponent(i, entity2, new IsTarget
						{
							Targetee = entity
						});
					}
					else
					{
						CommandBuffer.SetComponent(i, entity, new AttackThickness
						{
							Value = 0f
						});
					}
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
			ComponentDataFromEntity<HasTargetTag> componentDataFromEntity = GetComponentDataFromEntity<HasTargetTag>(isReadOnly: true);
			ComponentDataFromEntity<IsTarget> componentDataFromEntity2 = GetComponentDataFromEntity<IsTarget>(isReadOnly: true);
			EntityCommandBuffer entityCommandBuffer = m_barrier.CreateCommandBuffer();
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
			componentGroup.GetComponentDataArray<GroundPosition>();
			ComponentDataArray<Range> componentDataArray = componentGroup.GetComponentDataArray<Range>();
			ComponentDataArray<PredictedPosition> componentDataArray2 = componentGroup.GetComponentDataArray<PredictedPosition>();
			EntityArray entityArray2 = obj.GetEntityArray();
			ComponentDataArray<HipPosition> componentDataArray3 = obj.GetComponentDataArray<HipPosition>();
			ComponentDataArray<TargetPriority> componentDataArray4 = obj.GetComponentDataArray<TargetPriority>();
			ComponentDataArray<TargetThickness> componentDataArray5 = obj.GetComponentDataArray<TargetThickness>();
			obj.GetComponentDataArray<PredictedPosition>();
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
			EntityArray entityArray3 = componentGroup.GetEntityArray();
			componentGroup.GetComponentDataArray<GroundPosition>();
			ComponentDataArray<Range> componentDataArray6 = componentGroup.GetComponentDataArray<Range>();
			ComponentDataArray<PredictedPosition> componentDataArray7 = componentGroup.GetComponentDataArray<PredictedPosition>();
			EntityArray entityArray4 = obj.GetEntityArray();
			ComponentDataArray<HipPosition> componentDataArray8 = obj.GetComponentDataArray<HipPosition>();
			ComponentDataArray<TargetPriority> componentDataArray9 = obj.GetComponentDataArray<TargetPriority>();
			ComponentDataArray<TargetThickness> componentDataArray10 = obj.GetComponentDataArray<TargetThickness>();
			obj.GetComponentDataArray<PredictedPosition>();
			NativeArray<BeingTargetedBy> targetsBeingTargetedBy = new NativeArray<BeingTargetedBy>(entityArray2.Length, Allocator.TempJob);
			NativeArray<BeingTargetedBy> targetsBeingTargetedBy2 = new NativeArray<BeingTargetedBy>(entityArray4.Length, Allocator.TempJob);
			Job jobData = new Job
			{
				UnitEntities = entityArray,
				UnitPositions = componentDataArray2,
				UnitRanges = componentDataArray,
				TargetPositions = componentDataArray8,
				TargetEntities = entityArray4,
				TargetPriorities = componentDataArray9,
				TargetsBeingTargetedBy = targetsBeingTargetedBy2,
				TargetThicknesses = componentDataArray10,
				CommandBuffer = entityCommandBuffer.ToConcurrent(),
				HasTargetTag = componentDataFromEntity,
				IsTargetTag = componentDataFromEntity2
			};
			Job jobData2 = new Job
			{
				UnitEntities = entityArray3,
				UnitPositions = componentDataArray7,
				UnitRanges = componentDataArray6,
				TargetEntities = entityArray2,
				TargetPositions = componentDataArray3,
				TargetPriorities = componentDataArray4,
				TargetsBeingTargetedBy = targetsBeingTargetedBy,
				TargetThicknesses = componentDataArray5,
				CommandBuffer = entityCommandBuffer.ToConcurrent(),
				HasTargetTag = componentDataFromEntity,
				IsTargetTag = componentDataFromEntity2
			};
			JobHandle dependsOn = jobData.Schedule(inputDeps);
			JobHandle result = jobData2.Schedule(dependsOn);
			dependsOn.Complete();
			result.Complete();
			for (int i = 0; i < entityArray2.Length; i++)
			{
				entityCommandBuffer.SetComponent(entityArray2[i], targetsBeingTargetedBy[i]);
			}
			for (int j = 0; j < entityArray4.Length; j++)
			{
				entityCommandBuffer.SetComponent(entityArray4[j], targetsBeingTargetedBy2[j]);
			}
			targetsBeingTargetedBy.Dispose();
			targetsBeingTargetedBy2.Dispose();
			return result;
		}
	}
}
