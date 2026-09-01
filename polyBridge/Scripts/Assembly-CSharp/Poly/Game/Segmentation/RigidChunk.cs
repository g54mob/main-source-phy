using System.Collections.Generic;
using System.Linq;
using Poly.Physics;

namespace Poly.Game.Segmentation
{
	public class RigidChunk
	{
		public List<EdgeHandle> edges = new List<EdgeHandle>();

		public List<EdgeHandle> singleEdgesToFixedNodes = new List<EdgeHandle>();

		public HashSet<MergedNode> fixedKnots = new HashSet<MergedNode>();

		public HashSet<MergedNode> secondaryFixedKnots = new HashSet<MergedNode>();

		public bool hasOneFixedAnchor;

		public bool isFixed;

		public bool hasSprings;

		public bool hasRopes;

		public RigidChunk(EdgeHandle edge, Dictionary<NodeHandle, MergedNode> mergedNodes)
		{
			edges.Add(edge);
			bool isFixedSingleNode = mergedNodes[edge.node0].isFixedSingleNode;
			bool isFixedSingleNode2 = mergedNodes[edge.node1].isFixedSingleNode;
			if (isFixedSingleNode)
			{
				fixedKnots.Add(mergedNodes[edge.node0]);
			}
			if (isFixedSingleNode2)
			{
				fixedKnots.Add(mergedNodes[edge.node1]);
			}
			hasOneFixedAnchor = isFixedSingleNode || isFixedSingleNode2;
			isFixed = isFixedSingleNode && isFixedSingleNode2;
			hasSprings = edge.material.isSpring;
			hasRopes = edge.material.isRope;
		}

		public static RigidChunk Merge(RigidChunk a, RigidChunk b, Dictionary<NodeHandle, MergedNode> mergedNodes, Dictionary<EdgeHandle, RigidChunk> rigidChunks)
		{
			Values.SwapIf(a.edges.Count < b.edges.Count, ref a, ref b);
			foreach (EdgeHandle edge in b.edges)
			{
				mergedNodes[edge.node0].SwapLinkedChunk(b, a);
				mergedNodes[edge.node1].SwapLinkedChunk(b, a);
			}
			a.edges.AddRange(b.edges);
			b.edges.ForEach(delegate(EdgeHandle e)
			{
				rigidChunks[e] = a;
			});
			a.singleEdgesToFixedNodes.AddRange(b.singleEdgesToFixedNodes);
			b.singleEdgesToFixedNodes.Clear();
			a.hasOneFixedAnchor |= b.hasOneFixedAnchor;
			a.isFixed |= b.isFixed;
			a.fixedKnots.UnionWith(b.fixedKnots);
			a.secondaryFixedKnots.Union(b.secondaryFixedKnots);
			a.isFixed |= 2 <= a.fixedKnots.Count + a.secondaryFixedKnots.Count;
			a.hasSprings |= b.hasSprings;
			a.hasRopes |= b.hasRopes;
			b.edges.Clear();
			return a;
		}

		public void AddSecondaryFixedKnot(MergedNode fixedKnot)
		{
			secondaryFixedKnots.Add(fixedKnot);
			hasOneFixedAnchor = true;
			isFixed |= 2 <= fixedKnots.Count + secondaryFixedKnots.Count;
		}
	}
}
