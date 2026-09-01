using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Systems
{
	[UpdateAfter(typeof(PreUpdateBarrier))]
	[UpdateBefore(typeof(UpdateBarrier))]
	public class CalculateMovementSpeed : JobComponentSystem
	{
		private struct Filter
		{
			public EntityArray Entities;

			public ComponentDataArray<Velocity> Velocities;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		private struct Job : IJobParallelFor
		{
			public EntityCommandBuffer.Concurrent CommandBuffer;

			public EntityArray Entities;

			public ComponentDataArray<Velocity> Velocities;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public void Execute(int index)
			{
				Entity e = Entities[index];
				Velocity component = Velocities[index];
				if (HasTargetTags[index].Target == Entity.Null)
				{
					component.Value = float3.zero;
					CommandBuffer.SetComponent(index, e, component);
				}
				else
				{
					component.Value = new float3(0f, 0f, 1f);
					CommandBuffer.SetComponent(index, e, component);
				}
			}
		}

		[Inject]
		private Filter m_filter;

		[Inject]
		private UpdateBarrier m_barrier;

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			GetComponentDataFromEntity<GroundPosition>(isReadOnly: true);
			EntityCommandBuffer entityCommandBuffer = m_barrier.CreateCommandBuffer();
			return new Job
			{
				Entities = m_filter.Entities,
				Velocities = m_filter.Velocities,
				HasTargetTags = m_filter.HasTargetTags,
				CommandBuffer = entityCommandBuffer.ToConcurrent()
			}.Schedule(m_filter.Length, 12, inputDeps);
		}
	}
}
