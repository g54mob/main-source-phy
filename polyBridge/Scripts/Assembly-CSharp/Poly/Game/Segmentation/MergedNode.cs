using System.Collections.Generic;
using Poly.Physics;

namespace Poly.Game.Segmentation
{
	public class MergedNode
	{
		public List<NodeHandle> nodes = new List<NodeHandle>();

		public List<EdgeHandle> edges = new List<EdgeHandle>();

		public List<NodeHandle> otherEdgeNode = new List<NodeHandle>();

		public Dictionary<RigidChunk, int> attachedChunkToNumLinks = new Dictionary<RigidChunk, int>();

		public bool isFixedSingleNode;

		public bool isPromotedFixed;

		public float cached_connectivityFactorForRoad { get; set; }

		public float cached_connectivityFactorForNonRoad { get; set; }

		public int debug_numMidNodes { get; set; }

		public MergedNode(NodeHandle node)
		{
			nodes.Add(node);
			isFixedSingleNode = node.isKinematic;
		}

		public void AddLinkToChunk(RigidChunk chunk, int num = 1)
		{
			attachedChunkToNumLinks.TryGetValue(chunk, out var value);
			attachedChunkToNumLinks[chunk] = value + num;
		}

		public void RemoveLinkToChunk(RigidChunk chunk)
		{
			if (attachedChunkToNumLinks.TryGetValue(chunk, out var value))
			{
				if (1 < value)
				{
					attachedChunkToNumLinks[chunk] = value - 1;
				}
				else
				{
					attachedChunkToNumLinks.Remove(chunk);
				}
			}
		}

		public void SwapLinkedChunk(RigidChunk oldChunk, RigidChunk newChunk)
		{
			RemoveLinkToChunk(oldChunk);
			AddLinkToChunk(newChunk);
		}

		public static void Merge(MergedNode a, MergedNode b, Dictionary<NodeHandle, MergedNode> mergedNodes)
		{
			Values.SwapIf(a.nodes.Count < b.nodes.Count, ref a, ref b);
			a.nodes.AddRange(b.nodes);
			b.nodes.ForEach(delegate(NodeHandle n)
			{
				mergedNodes[n] = a;
			});
			b.nodes.Clear();
		}
	}
}
