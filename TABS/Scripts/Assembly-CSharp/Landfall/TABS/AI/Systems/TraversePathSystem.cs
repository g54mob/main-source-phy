using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Pathfinding;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Landfall.TABS.AI.Systems
{
	[UpdateBefore(typeof(UpdateBarrier))]
	[UpdateAfter(typeof(CalculateNavPathSystem))]
	public class TraversePathSystem : JobComponentSystem
	{
		private struct Filter
		{
			public EntityArray Entities;

			public ComponentDataArray<Direction> Directions;

			public ComponentDataArray<CurrentWaypoint> CurrentWaypoints;

			public ComponentDataArray<HipPosition> Positions;

			public BufferArray<PathPoint> PathBuffers;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		private struct Job : IJobParallelFor
		{
			public EntityArray Entities;

			public ComponentDataArray<Direction> Directions;

			public ComponentDataArray<HipPosition> Positions;

			public ComponentDataArray<CurrentWaypoint> CurrentWaypoints;

			public BufferArray<PathPoint> PathBuffers;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public EntityCommandBuffer.Concurrent CommandBuffer;

			public void Execute(int index)
			{
				Entity e = Entities[index];
				Direction component = Directions[index];
				HipPosition hipPosition = Positions[index];
				CurrentWaypoint component2 = CurrentWaypoints[index];
				DynamicBuffer<PathPoint> dynamicBuffer = PathBuffers[index];
				if (dynamicBuffer.Length != 0 && component2.Value < dynamicBuffer.Length - 1)
				{
					float3 value = dynamicBuffer[component2.Value].Value;
					float3 value2 = dynamicBuffer[component2.Value + 1].Value;
					float3 obj = ProjectPointOntoLine(value, value2, hipPosition.Value);
					float num = math.length(value2 - value);
					if (math.length(obj - value) > num)
					{
						component2.Value++;
					}
					float3 x = value2 - hipPosition.Value;
					x.y = 0f;
					x = math.normalize(x);
					component.LastValue = component.Value;
					component.Value = x;
					CommandBuffer.SetComponent(index, e, component2);
					CommandBuffer.SetComponent(index, e, component);
				}
			}

			public float3 ProjectPointOntoLine(float3 a, float3 b, float3 p)
			{
				float3 float5 = b - a;
				float3 x = p - a;
				return a + math.dot(x, float5) / math.dot(float5, float5) * float5;
			}
		}

		[Inject]
		private Filter m_filter;

		[Inject]
		private UpdateBarrier m_barrier;

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			return new Job
			{
				Entities = m_filter.Entities,
				Directions = m_filter.Directions,
				Positions = m_filter.Positions,
				CurrentWaypoints = m_filter.CurrentWaypoints,
				PathBuffers = m_filter.PathBuffers,
				CommandBuffer = m_barrier.CreateCommandBuffer().ToConcurrent()
			}.Schedule(m_filter.Length, 12, inputDeps);
		}
	}
}
