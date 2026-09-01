using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Modifiers;
using Landfall.TABS.AI.Components.Pathfinding;
using Landfall.TABS.AI.Components.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Systems.Modifiers
{
	[UpdateAfter(typeof(UpdateBarrier))]
	[UpdateBefore(typeof(PreLateUpdateBarrier))]
	public class KeepRangedDistanceSystem : JobComponentSystem
	{
		private struct Filter
		{
			public EntityArray Entities;

			public ComponentDataArray<Velocity> Velocities;

			public ComponentDataArray<Direction> Directions;

			[ReadOnly]
			public ComponentDataArray<Range> Ranges;

			[ReadOnly]
			public ComponentDataArray<CanSeeTarget> CanSeeTargets;

			[ReadOnly]
			public ComponentDataArray<KeepRangedDistance> KeepRangedDistance;

			[ReadOnly]
			public ComponentDataArray<PathDistance> PathDistances;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public ComponentDataArray<TargetData> TargetData;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		private struct Job : IJobParallelFor
		{
			public EntityArray Entities;

			public ComponentDataArray<Velocity> Velocities;

			public ComponentDataArray<Direction> Directions;

			[ReadOnly]
			public ComponentDataArray<Range> Ranges;

			[ReadOnly]
			public ComponentDataArray<TargetData> TargetDatas;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public ComponentDataArray<CanSeeTarget> CanSeeTargets;

			[ReadOnly]
			public ComponentDataArray<KeepRangedDistance> KeepRangedDistances;

			[ReadOnly]
			public ComponentDataArray<PathDistance> PathDistances;

			[ReadOnly]
			public ComponentDataFromEntity<PredictedPosition> TargetPredictedPositions;

			[ReadOnly]
			public ComponentDataFromEntity<HipPosition> TargetHipPositions;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public EntityCommandBuffer.Concurrent CommandBuffer;

			public void Execute(int index)
			{
				HasTargetTag hasTargetTag = HasTargetTags[index];
				if (hasTargetTag.Target == Entity.Null || !TargetPredictedPositions.Exists(hasTargetTag.Target))
				{
					return;
				}
				Entity entity = Entities[index];
				HipPosition hipPosition = TargetHipPositions[entity];
				Velocity component = Velocities[index];
				_ = Ranges[index];
				CanSeeTarget canSeeTarget = CanSeeTargets[index];
				KeepRangedDistance keepRangedDistance = KeepRangedDistances[index];
				PathDistance pathDistance = PathDistances[index];
				TargetData targetData = TargetDatas[index];
				PredictedPosition predictedPosition = TargetPredictedPositions[hasTargetTag.Target];
				HipPosition hipPosition2 = TargetHipPositions[hasTargetTag.Target];
				float num = math.distance(hipPosition2.Value, hipPosition.Value);
				float num2 = math.distance(predictedPosition.Value, hipPosition.Value);
				bool num3 = pathDistance.Distance * 0.8f > num && targetData.TargetInPreferredRange == 0 && canSeeTarget.CanSee == 1;
				bool flag = num2 < num;
				if (num3)
				{
					if (targetData.DistanceToTarget <= keepRangedDistance.WaitingDistance && flag)
					{
						component.Value = new float3(0f, 0f, 0f);
						CommandBuffer.SetComponent(index, entity, component);
						Direction component2 = Directions[index];
						float3 x = hipPosition2.Value - hipPosition.Value;
						component2.Value = math.normalize(x);
						CommandBuffer.SetComponent(index, entity, component2);
					}
				}
				else if (targetData.TargetInPreferredRange == 1 && canSeeTarget.CanSee == 1)
				{
					component.Value = new float3(0f, 0f, 0f);
					CommandBuffer.SetComponent(index, entity, component);
				}
			}
		}

		[Inject]
		private Filter m_filter;

		[Inject]
		private PreLateUpdateBarrier m_barrier;

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			ComponentDataFromEntity<PredictedPosition> componentDataFromEntity = GetComponentDataFromEntity<PredictedPosition>(isReadOnly: true);
			ComponentDataFromEntity<HipPosition> componentDataFromEntity2 = GetComponentDataFromEntity<HipPosition>(isReadOnly: true);
			EntityCommandBuffer entityCommandBuffer = m_barrier.CreateCommandBuffer();
			return new Job
			{
				Entities = m_filter.Entities,
				Ranges = m_filter.Ranges,
				Velocities = m_filter.Velocities,
				Directions = m_filter.Directions,
				HasTargetTags = m_filter.HasTargetTags,
				TargetDatas = m_filter.TargetData,
				CanSeeTargets = m_filter.CanSeeTargets,
				KeepRangedDistances = m_filter.KeepRangedDistance,
				PathDistances = m_filter.PathDistances,
				TargetPredictedPositions = componentDataFromEntity,
				TargetHipPositions = componentDataFromEntity2,
				CommandBuffer = entityCommandBuffer.ToConcurrent()
			}.Schedule(m_filter.Length, 12, inputDeps);
		}
	}
}
