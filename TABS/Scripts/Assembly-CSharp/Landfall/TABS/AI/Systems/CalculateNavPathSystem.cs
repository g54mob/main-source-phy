using System.Collections.Generic;
using EzECS.Barriers;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Events;
using Landfall.TABS.AI.Components.Pathfinding;
using Landfall.TABS.AI.Components.Tags;
using Pathfinding;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABS.AI.Systems
{
	[UpdateAfter(typeof(PreUpdateBarrier))]
	[UpdateBefore(typeof(UpdateBarrier))]
	public class CalculateNavPathSystem : ComponentSystem
	{
		private struct Filter
		{
			public EntityArray Entities;

			public ComponentDataArray<PathSettings> PathSettings;

			[ReadOnly]
			public ComponentDataArray<GroundPosition> GroundPositions;

			[ReadOnly]
			public ComponentDataArray<HasTargetTag> HasTargetTags;

			[ReadOnly]
			public ComponentDataArray<Navmesh> NavmeshTypes;

			[ReadOnly]
			public SubtractiveComponent<IsInPool> IsInPool;

			public readonly int Length;
		}

		[Inject]
		private Filter m_filter;

		[Inject]
		private UpdateBarrier m_barrier;

		private Dictionary<int, Entity> m_pathIDs = new Dictionary<int, Entity>();

		private Seeker m_seeker;

		private AstarPath m_pathObject;

		private bool m_hasNavmesh;

		private bool m_hasLargeUnitNavmesh;

		protected override void OnStartRunning()
		{
			m_seeker = Object.FindObjectOfType<Seeker>();
			m_pathObject = Object.FindObjectOfType<AstarPath>();
			m_hasNavmesh = m_pathObject != null;
			base.OnCreateManager();
		}

		protected override void OnUpdate()
		{
			if (m_pathObject != null)
			{
				m_hasLargeUnitNavmesh = ((AstarData.active.data.graphs.Length > 1) ? true : false);
				for (int i = 0; i < m_filter.Length; i++)
				{
					Entity entity = m_filter.Entities[i];
					PathSettings componentData = m_filter.PathSettings[i];
					Navmesh navmesh = m_filter.NavmeshTypes[i];
					componentData.CurrentRate -= Time.deltaTime;
					if (componentData.CurrentRate <= 0f)
					{
						GroundPosition groundPosition = m_filter.GroundPositions[i];
						HasTargetTag hasTargetTag = m_filter.HasTargetTags[i];
						if (!base.EntityManager.Exists(hasTargetTag.Target))
						{
							continue;
						}
						GroundPosition componentData2 = base.EntityManager.GetComponentData<GroundPosition>(hasTargetTag.Target);
						componentData.CurrentRate = componentData.RepathRate;
						ABPath aBPath = ABPath.Construct(groundPosition.Value, componentData2.Value, OnPathComplete);
						NNConstraint none = NNConstraint.None;
						int num = (int)navmesh.Value;
						if (!m_hasLargeUnitNavmesh)
						{
							num = 0;
						}
						int num2 = 1 << num;
						none.graphMask = num2;
						aBPath.nnConstraint = none;
						m_pathIDs.Add(aBPath.pathID, entity);
						AstarPath.StartPath(aBPath);
					}
					base.EntityManager.SetComponentData(entity, componentData);
				}
				return;
			}
			for (int j = 0; j < m_filter.Length; j++)
			{
				Entity entity2 = m_filter.Entities[j];
				HipPosition componentData3 = base.EntityManager.GetComponentData<HipPosition>(entity2);
				HasTargetTag componentData4 = base.EntityManager.GetComponentData<HasTargetTag>(entity2);
				if (base.EntityManager.Exists(componentData4.Target))
				{
					GroundPosition componentData5 = base.EntityManager.GetComponentData<GroundPosition>(componentData4.Target);
					DynamicBuffer<PathPoint> buffer = base.EntityManager.GetBuffer<PathPoint>(entity2);
					buffer.Clear();
					buffer.Add(new PathPoint
					{
						Value = componentData3.Value
					});
					buffer.Add(new PathPoint
					{
						Value = componentData5.Value
					});
					float3 x = componentData5.Value - componentData3.Value;
					base.EntityManager.SetComponentData(entity2, new PathDistance
					{
						Distance = math.length(x)
					});
					base.EntityManager.SetComponentData(entity2, new CurrentWaypoint
					{
						Value = 0
					});
				}
			}
		}

		private void OnPathComplete(Path path)
		{
			m_seeker.RunModifiers(Seeker.ModifierPass.PostProcess, path);
			int pathID = path.pathID;
			Entity entity = m_pathIDs[pathID];
			m_pathIDs.Remove(pathID);
			if (!base.EntityManager.Exists(entity))
			{
				return;
			}
			if (path.CompleteState == PathCompleteState.Complete || path.CompleteState == PathCompleteState.Partial)
			{
				float num = 0f;
				DynamicBuffer<PathPoint> buffer = base.EntityManager.GetBuffer<PathPoint>(entity);
				buffer.Clear();
				List<Vector3> vectorPath = path.vectorPath;
				for (int i = 0; i < vectorPath.Count; i++)
				{
					buffer.Add(new PathPoint
					{
						Value = vectorPath[i]
					});
					if (i > 0)
					{
						Vector3 vector = vectorPath[i - 1];
						float magnitude = (vectorPath[i] - vector).magnitude;
						num += magnitude;
					}
				}
				base.EntityManager.SetComponentData(entity, new CurrentWaypoint
				{
					Value = 0
				});
				base.EntityManager.SetComponentData(entity, new PathDistance
				{
					Distance = num
				});
			}
			else
			{
				HipPosition componentData = base.EntityManager.GetComponentData<HipPosition>(entity);
				HasTargetTag componentData2 = base.EntityManager.GetComponentData<HasTargetTag>(entity);
				GroundPosition componentData3 = base.EntityManager.GetComponentData<GroundPosition>(componentData2.Target);
				DynamicBuffer<PathPoint> buffer2 = base.EntityManager.GetBuffer<PathPoint>(entity);
				buffer2.Clear();
				buffer2.Add(new PathPoint
				{
					Value = componentData.Value
				});
				buffer2.Add(new PathPoint
				{
					Value = componentData3.Value
				});
				float3 x = componentData3.Value - componentData.Value;
				base.EntityManager.SetComponentData(entity, new PathDistance
				{
					Distance = math.length(x)
				});
				base.EntityManager.SetComponentData(entity, new CurrentWaypoint
				{
					Value = 0
				});
			}
		}
	}
}
