using System.Collections.Generic;
using System.Linq;
using Pb;
using Poly.Base;
using Poly.Extension;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game.Segmentation
{
	public class BridgeSegmentation
	{
		public const bool enabled = false;

		public const bool useValidPivotsToFilterAngleMonitoring = false;

		public static bool allowPromotion;

		public static bool allowSprings;

		public static bool allowRopes;

		public float desiredLabelWidthInMeters = 0.6f;

		public Dictionary<NodeHandle, MergedNode> mergedNodes = new Dictionary<NodeHandle, MergedNode>();

		public Dictionary<EdgeHandle, RigidChunk> rigidChunks = new Dictionary<EdgeHandle, RigidChunk>();

		public HashSet<MergedNode> pivots = new HashSet<MergedNode>();

		public void CalcOrImproveSegmentation()
		{
			World instance = SingletonBehaviour<World>.instance;
			List<EdgeHandle> list = instance.edgeHandles.FindAll((EdgeHandle e) => e.isDynamic);
			List<EdgeHandle> allNonPinsDynamic = list.Where((EdgeHandle e) => !e.material.isPin).ToList();
			if (mergedNodes.Count == 0)
			{
				mergedNodes = CalcMergedNodes(instance, list, allNonPinsDynamic);
			}
			pivots.Clear();
		}

		private static bool ShouldProcessEdge(EdgeHandle edge)
		{
			if (allowRopes || !edge.material.isRope)
			{
				if (!allowSprings)
				{
					return !edge.material.isSpring;
				}
				return true;
			}
			return false;
		}

		private static bool ShouldProcessChunk(RigidChunk chunk)
		{
			if (allowRopes || !chunk.hasRopes)
			{
				if (!allowSprings)
				{
					return !chunk.hasSprings;
				}
				return true;
			}
			return false;
		}

		public static Dictionary<NodeHandle, MergedNode> CalcMergedNodes(World world, List<EdgeHandle> allDynamic, List<EdgeHandle> allNonPinsDynamic)
		{
			List<EdgeHandle> list = world.edgeHandles.FindAll((EdgeHandle e) => e.material.isPin);
			Dictionary<NodeHandle, MergedNode> dictionary = new Dictionary<NodeHandle, MergedNode>();
			HashSet<NodeHandle> hashSet = new HashSet<NodeHandle>();
			hashSet.UnionWith(allDynamic.Select((EdgeHandle e) => e.node0));
			hashSet.UnionWith(allDynamic.Select((EdgeHandle e) => e.node1));
			hashSet.UnionWith(list.Select((EdgeHandle e) => e.node0));
			hashSet.UnionWith(list.Select((EdgeHandle e) => e.node1));
			foreach (NodeHandle item in hashSet)
			{
				dictionary.Add(item, new MergedNode(item));
			}
			foreach (EdgeHandle item2 in list)
			{
				MergedNode value;
				bool flag = dictionary.TryGetValue(item2.node0, out value);
				MergedNode value2;
				bool flag2 = dictionary.TryGetValue(item2.node1, out value2);
				if (value != value2 && flag && flag2 && item2.node0.pins.Contains(item2))
				{
					MergedNode.Merge(value, value2, dictionary);
				}
			}
			foreach (EdgeHandle item3 in allNonPinsDynamic)
			{
				dictionary[item3.node0].edges.Add(item3);
				dictionary[item3.node1].edges.Add(item3);
				dictionary[item3.node0].otherEdgeNode.Add(item3.GetOther(item3.node0));
				dictionary[item3.node1].otherEdgeNode.Add(item3.GetOther(item3.node1));
			}
			return dictionary;
		}

		private static Dictionary<EdgeHandle, RigidChunk> CalcRigidChunks(World world, List<EdgeHandle> allNonPinsDynamic, Dictionary<NodeHandle, MergedNode> mergedNodes, bool improveOnly = false, Dictionary<EdgeHandle, RigidChunk> existingChunks = null)
		{
			Dictionary<EdgeHandle, RigidChunk> dictionary;
			if (improveOnly)
			{
				dictionary = existingChunks;
			}
			else
			{
				dictionary = new Dictionary<EdgeHandle, RigidChunk>();
				foreach (EdgeHandle item in allNonPinsDynamic)
				{
					RigidChunk rigidChunk = new RigidChunk(item, mergedNodes);
					dictionary.Add(item, rigidChunk);
					mergedNodes[item.node0].AddLinkToChunk(rigidChunk);
					mergedNodes[item.node1].AddLinkToChunk(rigidChunk);
				}
			}
			Dictionary<NodeHandle, EdgeHandle> dictionary2 = new Dictionary<NodeHandle, EdgeHandle>();
			int num;
			int num2;
			do
			{
				num = 0;
				num2 = 0;
				foreach (EdgeHandle item2 in allNonPinsDynamic)
				{
					if (!ShouldProcessEdge(item2))
					{
						continue;
					}
					dictionary2.Clear();
					List<EdgeHandle> edges = mergedNodes[item2.node0].edges;
					List<EdgeHandle> edges2 = mergedNodes[item2.node1].edges;
					List<NodeHandle> otherEdgeNode = mergedNodes[item2.node0].otherEdgeNode;
					List<NodeHandle> otherEdgeNode2 = mergedNodes[item2.node1].otherEdgeNode;
					for (int i = 0; i < otherEdgeNode2.Count; i++)
					{
						if (edges2[i] != item2 && ShouldProcessEdge(edges2[i]))
						{
							dictionary2.Add(otherEdgeNode2[i], edges2[i]);
						}
					}
					for (int j = 0; j < otherEdgeNode.Count; j++)
					{
						if (edges[j] == item2 || !ShouldProcessEdge(edges[j]))
						{
							continue;
						}
						EdgeHandle value;
						bool num3 = dictionary2.TryGetValue(otherEdgeNode[j], out value);
						bool flag = num3 && AreNonDegenerate(item2, edges[j], value);
						if (num3 && flag)
						{
							RigidChunk rigidChunk2 = dictionary[item2];
							RigidChunk rigidChunk3 = dictionary[edges[j]];
							if (rigidChunk2 != rigidChunk3)
							{
								RigidChunk.Merge(rigidChunk2, rigidChunk3, mergedNodes, dictionary);
								num++;
							}
							rigidChunk2 = dictionary[item2];
							RigidChunk rigidChunk4 = dictionary[value];
							if (rigidChunk2 != rigidChunk4)
							{
								RigidChunk.Merge(rigidChunk2, rigidChunk4, mergedNodes, dictionary);
								num++;
							}
						}
					}
					List<EdgeHandle> list = edges;
					List<NodeHandle> list2 = otherEdgeNode;
					NodeHandle key = item2.node0;
					for (int k = 0; k < 2; k++)
					{
						for (int l = 0; l < list.Count; l++)
						{
							EdgeHandle edgeHandle = list[l];
							RigidChunk rigidChunk5 = dictionary[item2];
							RigidChunk rigidChunk6 = dictionary[edgeHandle];
							if (rigidChunk6 == rigidChunk5 || edgeHandle == item2 || !ShouldProcessEdge(edgeHandle))
							{
								continue;
							}
							MergedNode mergedNode = mergedNodes[list2[l]];
							if (mergedNode.attachedChunkToNumLinks.ContainsKey(rigidChunk5))
							{
								RigidChunk.Merge(rigidChunk5, rigidChunk6, mergedNodes, dictionary);
								rigidChunk5 = dictionary[item2];
								rigidChunk6 = dictionary[edgeHandle];
								num++;
								continue;
							}
							List<EdgeHandle> edges3 = mergedNode.edges;
							List<NodeHandle> otherEdgeNode3 = mergedNode.otherEdgeNode;
							for (int m = 0; m < edges3.Count; m++)
							{
								EdgeHandle edgeHandle2 = edges3[m];
								RigidChunk rigidChunk7 = dictionary[edgeHandle2];
								if (rigidChunk7 == rigidChunk5 || edgeHandle2 == edgeHandle || !ShouldProcessEdge(edgeHandle2))
								{
									continue;
								}
								MergedNode mergedNode2 = mergedNodes[otherEdgeNode3[m]];
								if (mergedNode2 != mergedNodes[key])
								{
									MergedNode mergedNode3 = mergedNodes[key];
									bool flag2 = false;
									EdgeHandle a = null;
									for (int n = 0; n < mergedNode3.otherEdgeNode.Count; n++)
									{
										NodeHandle nodeHandle = mergedNode3.otherEdgeNode[n];
										foreach (NodeHandle node in mergedNode2.nodes)
										{
											if (nodeHandle == node)
											{
												flag2 = true;
												a = mergedNode3.edges[n];
											}
										}
									}
									if ((!flag2 || AreNonDegenerate(a, edgeHandle, edgeHandle2)) && mergedNode2.attachedChunkToNumLinks.ContainsKey(rigidChunk5))
									{
										if (rigidChunk6 != rigidChunk5)
										{
											RigidChunk.Merge(rigidChunk5, rigidChunk6, mergedNodes, dictionary);
											rigidChunk5 = dictionary[item2];
											rigidChunk6 = dictionary[edgeHandle];
											rigidChunk7 = dictionary[edgeHandle2];
											num++;
										}
										if (rigidChunk7 != rigidChunk5)
										{
											RigidChunk.Merge(rigidChunk5, rigidChunk7, mergedNodes, dictionary);
											rigidChunk5 = dictionary[item2];
											rigidChunk6 = dictionary[edgeHandle];
											rigidChunk7 = dictionary[edgeHandle2];
											num++;
										}
									}
								}
								else if (rigidChunk7 != rigidChunk6)
								{
									RigidChunk.Merge(rigidChunk7, rigidChunk6, mergedNodes, dictionary);
									rigidChunk5 = dictionary[item2];
									rigidChunk6 = dictionary[edgeHandle];
									rigidChunk7 = dictionary[edgeHandle2];
									num++;
								}
							}
						}
						list = edges2;
						list2 = otherEdgeNode2;
						key = item2.node1;
					}
				}
				foreach (MergedNode mergedNode4 in new HashSet<MergedNode>(mergedNodes.Values.Where((MergedNode mn) => 1 < mn.attachedChunkToNumLinks.Count && !mn.isFixedSingleNode)))
				{
					if (1 < mergedNode4.attachedChunkToNumLinks.Count && !mergedNode4.isFixedSingleNode)
					{
						RigidChunk[] array = mergedNode4.attachedChunkToNumLinks.Keys.Where((RigidChunk rc) => ShouldProcessChunk(rc) && (rc.hasOneFixedAnchor || rc.isFixed)).ToArray();
						if (1 < array.Length)
						{
							bool num4 = array.Any((RigidChunk rc) => rc.isFixed);
							HashSet<MergedNode> hashSet = new HashSet<MergedNode>(array.SelectMany((RigidChunk chunk) => chunk.fixedKnots.Union(chunk.secondaryFixedKnots)));
							hashSet.Remove(mergedNode4);
							bool flag3 = 2 <= hashSet.Count;
							if (num4 || flag3)
							{
								HashSet<RigidChunk> chunksWithFixedNodes = new HashSet<RigidChunk>(hashSet.SelectMany((MergedNode node) => node.attachedChunkToNumLinks.Keys));
								array = array.Where((RigidChunk chunk) => chunksWithFixedNodes.Contains(chunk)).ToArray();
								for (int num5 = 1; num5 < array.Length; num5++)
								{
									array[num5] = RigidChunk.Merge(array[num5 - 1], array[num5], mergedNodes, dictionary);
									num++;
								}
							}
						}
					}
					if (allowPromotion && 1 < mergedNode4.attachedChunkToNumLinks.Count && !mergedNode4.isFixedSingleNode && !mergedNode4.isPromotedFixed && mergedNode4.attachedChunkToNumLinks.Keys.Any((RigidChunk chunk) => chunk.isFixed))
					{
						mergedNode4.isPromotedFixed = true;
						mergedNode4.attachedChunkToNumLinks.Keys.Where((RigidChunk chunk) => ShouldProcessChunk(chunk) && !chunk.isFixed).ToArray().ForEach(delegate(RigidChunk chunk)
						{
							chunk.AddSecondaryFixedKnot(mergedNode4);
						});
						num2++;
					}
				}
				RigidChunk[] array2 = new HashSet<RigidChunk>(dictionary.Values).Where((RigidChunk rc) => ShouldProcessChunk(rc) && rc.isFixed).ToArray();
				for (int num6 = 1; num6 < array2.Length; num6++)
				{
					array2[num6] = RigidChunk.Merge(array2[num6 - 1], array2[num6], mergedNodes, dictionary);
					num++;
				}
			}
			while (0 < num + num2);
			return dictionary;
		}

		private static bool AreNonDegenerate(EdgeHandle a, EdgeHandle b, EdgeHandle c)
		{
			float a2 = a.originalLength;
			float b2 = b.originalLength;
			float b3 = c.originalLength;
			if (b2 < a2)
			{
				Values.Swap(ref a2, ref b2);
			}
			if (b3 < a2)
			{
				Values.Swap(ref a2, ref b3);
			}
			if (b3 < b2)
			{
				Values.Swap(ref b2, ref b3);
			}
			float num = 0.5f * (a2 + b2 + b3);
			float num2 = UnityEngine.Mathf.Sqrt(num * (num - a2) * (num - b2) * (num - b3));
			float num3 = 2f * num2 / b3;
			return 0.1f * Pb.Mathf.Min(b3, 2f) <= num3;
		}

		private static void IdentifyPivotKnots(Dictionary<NodeHandle, MergedNode> mergedNodes, Dictionary<EdgeHandle, RigidChunk> rigidChunks, bool onlyPivotsWithRoads, HashSet<MergedNode> pivots)
		{
			foreach (MergedNode item in new HashSet<MergedNode>(mergedNodes.Values))
			{
				if (item.edges.Count == 0)
				{
					continue;
				}
				RigidChunk rigidChunk = rigidChunks[item.edges[0]];
				bool flag = false;
				bool flag2 = item.edges[0].material.enableCollision;
				for (int i = 1; i < item.edges.Count; i++)
				{
					if (rigidChunk != rigidChunks[item.edges[i]])
					{
						flag = true;
						if (!onlyPivotsWithRoads || flag2)
						{
							break;
						}
					}
					flag2 |= item.edges[i].material.enableCollision;
				}
				if (flag && (flag2 || !onlyPivotsWithRoads))
				{
					pivots.Add(item);
				}
			}
		}

		private static void ExcludePivotsThatContainStaticOrDynamicAnchors(HashSet<MergedNode> pivots)
		{
			List<MergedNode> list = new List<MergedNode>();
			foreach (MergedNode pivot in pivots)
			{
				foreach (NodeHandle node in pivot.nodes)
				{
					if (node.isAnchor)
					{
						list.Add(pivot);
						break;
					}
				}
			}
			foreach (MergedNode item in list)
			{
				pivots.Remove(item);
			}
		}

		public void Clear()
		{
			mergedNodes.Clear();
			rigidChunks.Clear();
			pivots.Clear();
		}

		public void DrawGizmos()
		{
		}

		public void DrawRigidChunks(Font font, Color backgroundColor, int fontSize, float labelVerticalOffset, float desiredLabelWidthInMeters = 0f)
		{
			if (desiredLabelWidthInMeters == 0f)
			{
				desiredLabelWidthInMeters = this.desiredLabelWidthInMeters;
			}
			int num = 0;
			num = 0;
			foreach (RigidChunk item in new HashSet<RigidChunk>(rigidChunks.Values))
			{
				Color atMod = ColorEx.retroMetroSet.GetAtMod(num);
				atMod.a = 0.25f;
				DrawGuiTextUtil.InitGuiStyle(font, atMod, fontSize);
				foreach (EdgeHandle edge in item.edges)
				{
					Vec2 pos_slow = edge.pos_slow;
					DrawGuiTextUtil.DisplayGuiLabel_Slow(text: num + (item.isFixed ? "F" : (item.hasOneFixedAnchor ? "H" : "")), posInWorld: pos_slow - 1.5f * Vector3.forward, desiredLabelWidthInMeters: desiredLabelWidthInMeters);
				}
				num++;
			}
			Color pink = ColorEx.pink;
			pink.a = 0.75f;
			DrawGuiTextUtil.InitGuiStyle(font, pink, fontSize);
			foreach (MergedNode pivot in pivots)
			{
				Vec2 pos = pivot.nodes[0].pos;
				string text = "[]";
				DrawGuiTextUtil.DisplayGuiLabel_Slow(pos - 1.5f * Vector3.forward, text, desiredLabelWidthInMeters);
			}
		}
	}
}
