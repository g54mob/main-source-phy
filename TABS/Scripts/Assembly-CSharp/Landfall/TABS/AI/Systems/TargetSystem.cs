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
	public class TargetSystem : JobComponentSystem
	{
		private struct HasTargetFilter
		{
			public EntityArray Entities;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public ComponentDataArray<PredictedPosition> Positions;

			[ReadOnly]
			public ComponentDataArray<Range> Ranges;

			public ComponentDataArray<TargetData> TargetDatas;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;
		}

		private struct Job : IJobParallelFor
		{
			public EntityCommandBuffer.Concurrent CommandBuffer;

			public EntityArray Entities;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public ComponentDataArray<PredictedPosition> Positions;

			[ReadOnly]
			public ComponentDataArray<Range> Ranges;

			public ComponentDataArray<TargetData> TargetDatas;

			[ReadOnly]
			public ComponentDataFromEntity<PredictedPosition> TargetPositions;

			[ReadOnly]
			public ComponentDataFromEntity<AttackThickness> TargetThicknesses;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			private static float k_AttackRange = 0.3f;

			public void Execute(int index)
			{
				HasTargetTag hasTargetTag = HasTargetTags[index];
				if (hasTargetTag.Target == Entity.Null)
				{
					return;
				}
				PredictedPosition predictedPosition = Positions[index];
				Range range = Ranges[index];
				TargetData component = TargetDatas[index];
				if (TargetPositions.Exists(hasTargetTag.Target) && TargetThicknesses.Exists(hasTargetTag.Target))
				{
					PredictedPosition predictedPosition2 = TargetPositions[hasTargetTag.Target];
					AttackThickness attackThickness = TargetThicknesses[hasTargetTag.Target];
					component.DistanceToTarget = math.distance(predictedPosition2.Value, predictedPosition.Value);
					component.DistanceToTarget -= attackThickness.Value;
					component.DistanceToTarget -= k_AttackRange;
					if (component.DistanceToTarget + k_AttackRange <= range.PreferredRange)
					{
						component.TargetInPreferredRange = 1;
					}
					else
					{
						component.TargetInPreferredRange = 0;
					}
					if (component.DistanceToTarget <= range.AttackRange)
					{
						component.TargetInAttackRange = 1;
					}
					else
					{
						component.TargetInAttackRange = 0;
					}
					CommandBuffer.SetComponent(index, Entities[index], component);
				}
			}
		}

		private NativeHashMap<Entity, Entity> m_targetMapping;

		[Inject]
		private HasTargetFilter m_hasTargetFilter;

		[Inject]
		private PreUpdateBarrier m_barrier;

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			ComponentDataFromEntity<PredictedPosition> componentDataFromEntity = GetComponentDataFromEntity<PredictedPosition>(isReadOnly: true);
			ComponentDataFromEntity<AttackThickness> componentDataFromEntity2 = GetComponentDataFromEntity<AttackThickness>(isReadOnly: true);
			EntityCommandBuffer entityCommandBuffer = m_barrier.CreateCommandBuffer();
			return new Job
			{
				Entities = m_hasTargetFilter.Entities,
				CommandBuffer = entityCommandBuffer.ToConcurrent(),
				HasTargetTags = m_hasTargetFilter.HasTargetTags,
				Positions = m_hasTargetFilter.Positions,
				TargetPositions = componentDataFromEntity,
				TargetThicknesses = componentDataFromEntity2,
				Ranges = m_hasTargetFilter.Ranges,
				TargetDatas = m_hasTargetFilter.TargetDatas
			}.Schedule(m_hasTargetFilter.Entities.Length, 32, inputDeps);
		}
	}
}
