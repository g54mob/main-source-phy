using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABS.AI.Systems
{
	[UpdateAfter(typeof(PreUpdateBarrier))]
	[UpdateBefore(typeof(UpdateBarrier))]
	public class CanSeeSystem : JobComponentSystem
	{
		private struct UnitFilter
		{
			public EntityArray Entities;

			public ComponentDataArray<CanSeeTarget> CanSeeTargets;

			[ReadOnly]
			public ComponentDataArray<WeaponPosition> Positions;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		private struct PrepareRaycastJob : IJobParallelFor
		{
			[ReadOnly]
			public ComponentDataArray<CanSeeTarget> CanSeeTargets;

			[ReadOnly]
			public ComponentDataArray<WeaponPosition> Positions;

			[ReadOnly]
			public ComponentDataFromEntity<HipPosition> TargetPositions;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[WriteOnly]
			public NativeArray<RaycastCommand> Commands;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public void Execute(int index)
			{
				WeaponPosition weaponPosition = Positions[index];
				HasTargetTag hasTargetTag = HasTargetTags[index];
				if (hasTargetTag.Target != Entity.Null && TargetPositions.Exists(hasTargetTag.Target))
				{
					float3 x = TargetPositions[hasTargetTag.Target].Value - weaponPosition.Value;
					float distance = math.length(x);
					x = math.normalize(x);
					if (x.Equals(float3.zero))
					{
						x = new float3(0f, 0f, 1f);
						distance = 1f;
					}
					int raycastMask = CanSeeTargets[index].RaycastMask;
					Commands[index] = new RaycastCommand(weaponPosition.Value, x, distance, raycastMask);
				}
			}
		}

		[Inject]
		private UnitFilter m_filter;

		[Inject]
		private UpdateBarrier m_barrier;

		private static int CONCURRENT_JOBS = 12;

		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			int length = m_filter.Length;
			ComponentDataFromEntity<HipPosition> componentDataFromEntity = GetComponentDataFromEntity<HipPosition>(isReadOnly: true);
			NativeArray<RaycastCommand> commands = new NativeArray<RaycastCommand>(length, Allocator.TempJob);
			NativeArray<RaycastHit> results = new NativeArray<RaycastHit>(length, Allocator.TempJob);
			new PrepareRaycastJob
			{
				Positions = m_filter.Positions,
				CanSeeTargets = m_filter.CanSeeTargets,
				TargetPositions = componentDataFromEntity,
				HasTargetTags = m_filter.HasTargetTags,
				Commands = commands
			}.Schedule(m_filter.Length, CONCURRENT_JOBS, inputDeps).Complete();
			RaycastCommand.ScheduleBatch(commands, results, CONCURRENT_JOBS, inputDeps).Complete();
			commands.Dispose();
			EntityCommandBuffer entityCommandBuffer = m_barrier.CreateCommandBuffer();
			for (int i = 0; i < results.Length; i++)
			{
				CanSeeTarget canSeeTarget = m_filter.CanSeeTargets[i];
				CanSeeTarget component = new CanSeeTarget
				{
					RaycastMask = canSeeTarget.RaycastMask
				};
				if (results[i].collider == null)
				{
					component.CanSee = 1;
				}
				else
				{
					component.CanSee = 0;
				}
				if (component.CanSee != canSeeTarget.CanSee)
				{
					entityCommandBuffer.SetComponent(m_filter.Entities[i], component);
				}
			}
			results.Dispose();
			return inputDeps;
		}
	}
}
