using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Modifiers;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Landfall.TABS.AI.Systems.Modifiers
{
	[UpdateAfter(typeof(UpdateBarrier))]
	[UpdateBefore(typeof(PreLateUpdateBarrier))]
	public class ConfusedMovementSystem : JobComponentSystem
	{
		private struct Filter
		{
			public EntityArray Entities;

			public ComponentDataArray<Direction> Directions;

			[ReadOnly]
			public ComponentDataArray<ConfusedMovement> ConfusedMovements;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		private struct Job : IJobParallelFor
		{
			public EntityArray Entities;

			public ComponentDataArray<Direction> Directions;

			[ReadOnly]
			public ComponentDataArray<ConfusedMovement> ConfusedMovements;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public EntityCommandBuffer.Concurrent CommandBuffer;

			public void Execute(int index)
			{
				Entity e = Entities[index];
				Direction component = Directions[index];
				component.Value += ConfusedMovements[index].Value;
				CommandBuffer.SetComponent(index, e, component);
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
				ConfusedMovements = m_filter.ConfusedMovements,
				CommandBuffer = m_barrier.CreateCommandBuffer().ToConcurrent()
			}.Schedule(m_filter.Length, 12, inputDeps);
		}
	}
}
