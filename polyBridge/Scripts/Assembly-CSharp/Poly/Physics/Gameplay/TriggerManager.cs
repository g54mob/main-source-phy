using System.Collections.Generic;
using Poly.Base;
using Poly.Collide;
using Poly.Math;
using UnityEngine;

namespace Poly.Physics.Gameplay
{
	public class TriggerManager : Singleton<TriggerManager>
	{
		public List<Trigger> triggers = new List<Trigger>();

		public int edgesPerUpdate = 100;

		private List<EdgeHandle> edges = new List<EdgeHandle>();

		private int nextIdx = -1;

		public bool areNewEdgesBroken { get; private set; }

		public void Clear()
		{
			triggers.Clear();
			edges.Clear();
			nextIdx = -1;
		}

		public void UpdateOverlapChecks(World world)
		{
			areNewEdgesBroken = false;
			if (triggers.Count == 0)
			{
				return;
			}
			if (nextIdx < 0)
			{
				edges.Clear();
				edges.AddRange(world.edgeHandles);
				nextIdx = 0;
			}
			int num = Mathf.Min(nextIdx + edgesPerUpdate, edges.Count);
			for (int i = nextIdx; i < num; i++)
			{
				EdgeHandle edgeHandle = edges[i];
				if (!edgeHandle.world || edgeHandle.material.strength == float.PositiveInfinity)
				{
					continue;
				}
				Transform2 wTe;
				PolygonShape polygonB = PolygonIntersection.CreatePolygon_LOCAL_ONLY_PolyB_Only(edgeHandle, out wTe);
				bool flag = false;
				foreach (Trigger trigger in triggers)
				{
					ref Transform2 t = ref trigger.t2;
					foreach (PolygonShape shape in trigger.shapes)
					{
						flag = PolygonIntersection.Overlap(shape, ref t, polygonB, ref wTe);
						if (flag)
						{
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
				if (flag)
				{
					if (edgeHandle.unityEdgeComponent.userData != null)
					{
						areNewEdgesBroken = true;
					}
					EdgeHandle edgeHandle2 = edgeHandle;
					List<IEdgeBreakListener> edgeBreakListeners = SingletonBehaviour<World>.instance.edgeBreakListeners;
					bool flag2 = true;
					for (int j = 0; j < edgeBreakListeners.Count; j++)
					{
						IEdgeBreakListener edgeBreakListener = edgeBreakListeners[j];
						flag2 &= edgeBreakListener.OnEdgeBroken(edgeHandle2);
					}
					if (flag2)
					{
						SingletonBehaviour<World>.instance.RemoveEdge(edgeHandle2);
						World.DestroyEdge(edgeHandle2);
					}
				}
			}
			nextIdx = num;
			if (num == edges.Count)
			{
				edges.Clear();
				nextIdx = -1;
			}
		}
	}
}
