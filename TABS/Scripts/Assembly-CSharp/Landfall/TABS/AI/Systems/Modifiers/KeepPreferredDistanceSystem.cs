using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Modifiers;
using Landfall.TABS.AI.Components.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Systems.Modifiers
{
	[UpdateAfter(typeof(UpdateBarrier))]
	[UpdateBefore(typeof(PreLateUpdateBarrier))]
	public class KeepPreferredDistanceSystem : JobComponentSystem
	{
		private struct Filter
		{
			public EntityArray Entities;

			public ComponentDataArray<Velocity> Velocities;

			[ReadOnly]
			public ComponentDataArray<Range> Ranges;

			[ReadOnly]
			public ComponentDataArray<TargetData> TargetDatas;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public ComponentDataArray<KeepPreferredDistance> KeepPrefferedDistance;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		private struct Job : IJobParallelFor
		{
			public EntityArray Entities;

			public ComponentDataArray<Velocity> Velocities;

			[ReadOnly]
			public ComponentDataArray<Range> Ranges;

			[ReadOnly]
			public ComponentDataArray<TargetData> TargetDatas;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public EntityCommandBuffer.Concurrent CommandBuffer;

			public void Execute(int index)
			{
				Entity e = Entities[index];
				Velocity component = Velocities[index];
				_ = Ranges[index];
				if (!(HasTargetTags[index].Target == Entity.Null) && TargetDatas[index].TargetInPreferredRange == 1)
				{
					component.Value = new float3(0f, 0f, -1f);
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
			EntityCommandBuffer entityCommandBuffer = m_barrier.CreateCommandBuffer();
			return new Job
			{
				Entities = m_filter.Entities,
				Ranges = m_filter.Ranges,
				Velocities = m_filter.Velocities,
				HasTargetTags = m_filter.HasTargetTags,
				TargetDatas = m_filter.TargetDatas,
				CommandBuffer = entityCommandBuffer.ToConcurrent()
			}.Schedule(m_filter.Length, 12, inputDeps);
		}
	}
}
