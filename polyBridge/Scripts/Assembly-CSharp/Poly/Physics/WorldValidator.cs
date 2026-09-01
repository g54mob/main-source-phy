using System.Collections.Generic;
using Poly.Extension;

namespace Poly.Physics
{
	public static class WorldValidator
	{
		public static void ValidateNodesAndEdges(World world)
		{
			NodeHandle[] nodes = world.nodeHandles.ToArray();
			EdgeHandle[] edges = world.edgeHandles.ToArray();
			Rigidbody[] bodies = world.bodies.ToArray();
			ValidateWorldIndices(nodes, edges, bodies);
			ValidateClosedAndCompleteReferenceGroup(nodes, edges);
			ValidateBackLinksFromEdgesToNodes(nodes, edges);
			ValidateNodeMasses(nodes);
			ValidateVirtualMassCachedInEdges(edges);
		}

		public static void ValidateWorldIdxOnly(World world)
		{
		}

		private static void ValidateWorldIndices(NodeHandle[] nodes, EdgeHandle[] edges, Rigidbody[] bodies)
		{
			for (int i = 0; i < nodes.Length; i++)
			{
				_ = nodes[i];
			}
			for (int j = 0; j < edges.Length; j++)
			{
				_ = edges[j];
			}
			for (int k = 0; k < bodies.Length; k++)
			{
				_ = bodies[k];
			}
		}

		private static void ValidateClosedAndCompleteReferenceGroup(NodeHandle[] nodes, EdgeHandle[] edges)
		{
			HashSet<NodeHandle> hashSet = new HashSet<NodeHandle>();
			HashSet<EdgeHandle> hashSet2 = new HashSet<EdgeHandle>();
			NodeHandle[] array = nodes;
			foreach (NodeHandle item in array)
			{
				hashSet.Add(item);
			}
			EdgeHandle[] array2 = edges;
			foreach (EdgeHandle item2 in array2)
			{
				hashSet2.Add(item2);
			}
			Dictionary<NodeHandle, int> dictionary = new Dictionary<NodeHandle, int>();
			Dictionary<EdgeHandle, int> dictionary2 = new Dictionary<EdgeHandle, int>();
			array = nodes;
			for (int i = 0; i < array.Length; i++)
			{
				foreach (EdgeHandle edge in array[i].edges)
				{
					dictionary2.TryGetValue(edge, out var value);
					dictionary2[edge] = value + 1;
				}
			}
			array2 = edges;
			foreach (EdgeHandle edgeHandle in array2)
			{
				dictionary.TryGetValue(edgeHandle.node0, out var value2);
				dictionary[edgeHandle.node0] = value2 + 1;
				dictionary.TryGetValue(edgeHandle.node1, out value2);
				dictionary[edgeHandle.node1] = value2 + 1;
			}
			array = nodes;
			for (int i = 0; i < array.Length; i++)
			{
				int count = array[i].edges.Count;
				_ = 0;
			}
			array2 = edges;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
		}

		private static void ValidateBackLinksFromEdgesToNodes(NodeHandle[] nodes, EdgeHandle[] edges)
		{
			foreach (NodeHandle nodeHandle in nodes)
			{
				foreach (EdgeHandle edge in nodeHandle.edges)
				{
					if (nodeHandle != edge.node0)
					{
						_ = 1;
					}
					else
						_ = 0;
				}
			}
		}

		private static void ValidateNodeMasses(NodeHandle[] nodes)
		{
			foreach (NodeHandle nodeHandle in nodes)
			{
				if (!nodeHandle.isKinematic)
				{
					if (nodeHandle.mass != 0f)
					{
						nodeHandle.solverNode.invMass.IsEqual(1f / nodeHandle.mass);
					}
					else
						_ = 0;
				}
			}
		}

		private static void ValidateVirtualMassCachedInEdges(EdgeHandle[] edges)
		{
			foreach (EdgeHandle edgeHandle in edges)
			{
				float num = edgeHandle.node0.solverNode.invMass + edgeHandle.node1.solverNode.invMass;
				edgeHandle.solverEdge.virtualMass.IsEqual((num != 0f) ? (1f / num) : 0f);
			}
		}
	}
}
