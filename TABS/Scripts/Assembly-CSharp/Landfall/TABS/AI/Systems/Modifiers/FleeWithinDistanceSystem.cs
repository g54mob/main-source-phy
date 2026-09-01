using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Modifiers;
using Landfall.TABS.AI.Components.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Landfall.TABS.AI.Systems.Modifiers
{
	[UpdateAfter(typeof(UpdateBarrier))]
	[UpdateBefore(typeof(PreLateUpdateBarrier))]
	public class FleeWithinDistanceSystem : JobComponentSystem
	{
		private struct Filter
		{
			public EntityArray Entities;

			public ComponentDataArray<Direction> Directions;

			[ReadOnly]
			public ComponentDataArray<FleeDistance> FleeDistances;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public ComponentDataArray<TargetData> TargetDatas;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		private struct Job : IJobParallelFor
		{
			public EntityArray Entities;

			public ComponentDataArray<Direction> Directions;

			[ReadOnly]
			public ComponentDataArray<FleeDistance> FleeDistances;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public ComponentDataArray<TargetData> TargetDatas;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public EntityCommandBuffer.Concurrent CommandBuffer;

			public void Execute(int index)
			{
				if (!(HasTargetTags[index].Target == Entity.Null))
				{
					Entity e = Entities[index];
					Direction component = Directions[index];
					FleeDistance fleeDistance = FleeDistances[index];
					if (TargetDatas[index].DistanceToTarget <= fleeDistance.Value)
					{
						component.Value.x *= -1f;
						component.Value.z *= -1f;
						component.Value.y *= -1f;
					}
					CommandBuffer.SetComponent(index, e, component);
				}
			}
		}

		[Inject]
		private Filter m_filter;

		[Inject]
		private PreLateUpdateBarrier m_barrier;

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			return new Job
			{
				Entities = m_filter.Entities,
				Directions = m_filter.Directions,
				FleeDistances = m_filter.FleeDistances,
				TargetDatas = m_filter.TargetDatas,
				HasTargetTags = m_filter.HasTargetTags,
				CommandBuffer = m_barrier.CreateCommandBuffer().ToConcurrent()
			}.Schedule(m_filter.Length, 12, inputDeps);
		}
	}
}
