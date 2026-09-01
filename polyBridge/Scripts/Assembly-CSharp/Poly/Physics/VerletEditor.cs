using System.Collections.Generic;
using System.Linq;
using Poly.Base;
using Poly.Extension;
using UnityEngine;

namespace Poly.Physics
{
	[ExecuteInEditMode]
	public class VerletEditor : SingletonBehaviour<VerletEditor>
	{
		public Material dynamicNodeMaterial;

		public Material fixedNodeMaterial;

		public Material splitDynamicNodeMaterial;

		public Material splitFixedNodeMaterial;

		public Material rigidbodyNodeMaterial;

		public EdgeMaterial pinMaterial;

		public float maxStressOnAnyJointSinceStart;

		internal World world => SingletonBehaviour<World>.instance;

		public static List<EdgeHandle> SeparateSplitNodeParts(List<List<NodeHandle>> nodeClusters, World world, List<EdgeHandle> outPinsRemovedImmediately)
		{
			List<EdgeHandle> list = new List<EdgeHandle>();
			foreach (List<NodeHandle> nodeCluster in nodeClusters)
			{
				bool flag = AreClusterAndPinnedPartsConnectedToSprings(nodeCluster);
				foreach (NodeHandle item in nodeCluster)
				{
					if (!item || item.pins.Count <= 0)
					{
						continue;
					}
					bool isSplittableAnchor = item.isSplittableAnchor;
					EdgeHandle[] array = item.pins.ToArray();
					foreach (EdgeHandle edgeHandle in array)
					{
						if (nodeCluster.Contains(edgeHandle.GetOther(item)))
						{
							continue;
						}
						edgeHandle.node0.pins.Remove(edgeHandle);
						edgeHandle.node1.pins.Remove(edgeHandle);
						NodeHandle other = edgeHandle.GetOther(item);
						if (isSplittableAnchor && other.isKinematic)
						{
							other.SetKinematic(isKinematic: false);
							if ((bool)other.unityNodeComponent)
							{
								other.unityNodeComponent.UpdateRendererMaterial();
							}
						}
						if (flag)
						{
							if ((bool)edgeHandle)
							{
								SingletonBehaviour<World>.instance.RemoveEdge(edgeHandle);
								World.DestroyEdge(edgeHandle);
								outPinsRemovedImmediately.Add(edgeHandle);
							}
						}
						else
						{
							list.Add(edgeHandle);
						}
					}
					if (!isSplittableAnchor && item.isKinematic)
					{
						item.SetKinematic(isKinematic: false);
						if ((bool)item.unityNodeComponent)
						{
							item.unityNodeComponent.UpdateRendererMaterial();
						}
					}
				}
			}
			return list;
		}

		public static List<NodeHandle> FindAllNodesPinnedToCluster(List<NodeHandle> cluster)
		{
			HashSet<NodeHandle> hashSet = new HashSet<NodeHandle>();
			List<NodeHandle> list = new List<NodeHandle>();
			list.AddRange(cluster);
			hashSet.AddRange(cluster);
			while (0 < list.Count)
			{
				NodeHandle nodeHandle = list.PopLast();
				foreach (EdgeHandle pin in nodeHandle.pins)
				{
					NodeHandle other = pin.GetOther(nodeHandle);
					if (!hashSet.Contains(other))
					{
						list.Add(other);
						hashSet.Add(other);
					}
				}
			}
			return hashSet.ToList();
		}

		public static bool AreClusterAndPinnedPartsConnectedToSprings(List<NodeHandle> cluster)
		{
			foreach (NodeHandle item in FindAllNodesPinnedToCluster(cluster))
			{
				foreach (EdgeHandle edge in item.edges)
				{
					if (edge.material.isSpring)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
