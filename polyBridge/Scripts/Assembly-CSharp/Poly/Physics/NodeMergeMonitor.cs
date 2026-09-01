using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Poly.Physics
{
	[Serializable]
	public class NodeMergeMonitor
	{
		public enum State
		{
			Inactive = 0,
			WaitForHydraulicsToNearEndOfMotion = 1,
			MonitorNodesForJustInTimeMerging = 2
		}

		internal struct NodePair
		{
			public NodeHandle a;

			public NodeHandle b;

			public float distSqr;

			public int CalcUid_Temp()
			{
				short num = System.Math.Min(a.worldIdx, b.worldIdx);
				int num2 = System.Math.Max(a.worldIdx, b.worldIdx);
				return (num << 16) + num2;
			}

			public bool IsDoubleAnchor()
			{
				if (a.isSplittableAnchor)
				{
					return b.isSplittableAnchor;
				}
				return false;
			}

			public static bool IsDoubleAnchor(NodeHandle a, NodeHandle b)
			{
				if (a.isSplittableAnchor)
				{
					return b.isSplittableAnchor;
				}
				return false;
			}
		}

		private static List<NodePair> nodePair_buffer = new List<NodePair>();

		private static List<NodePair> nodePair_buffer2 = new List<NodePair>();

		private static Dictionary<int, float> newDistSqr_buffer = new Dictionary<int, float>();

		private static Dictionary<int, float> newDistSqr_buffer_double = new Dictionary<int, float>();

		private static int lastNodePair_bufferIdx;

		[NonSerialized]
		public NodeMergeConfig config;

		public State state;

		private Hydraulics[] hydraulicsArray;

		private List<NodeHandle> splitNodes;

		private Dictionary<int, float> nodePairUidToDistSqr = new Dictionary<int, float>();

		private HashSet<int> mergedNodePairUids = new HashSet<int>();

		private HashSet<NodeHandle> _mergedNodes = new HashSet<NodeHandle>();

		public void StartMonitoring(Hydraulics[] hydraulicsArray, NodeHandle[] splitNodes)
		{
			_mergedNodes.Clear();
			this.hydraulicsArray = hydraulicsArray;
			this.splitNodes = splitNodes.ToList();
			state = State.WaitForHydraulicsToNearEndOfMotion;
			nodePairUidToDistSqr.Clear();
			mergedNodePairUids.Clear();
		}

		public void EndMonitoringAndClear()
		{
			hydraulicsArray = null;
			splitNodes = null;
			nodePairUidToDistSqr.Clear();
			mergedNodePairUids.Clear();
			state = State.Inactive;
		}

		public NodeHandle[] GetMergedNodesCopy()
		{
			return _mergedNodes.ToArray();
		}

		public void FixedUpdate_Manual()
		{
			if (!config.enableJustInTimeMerging)
			{
				return;
			}
			switch (state)
			{
			case State.WaitForHydraulicsToNearEndOfMotion:
				if (config.progressFractionToStartMerging <= CalcProgress())
				{
					state = State.MonitorNodesForJustInTimeMerging;
					hydraulicsArray = null;
				}
				break;
			case State.MonitorNodesForJustInTimeMerging:
				MonitorNodes();
				break;
			}
		}

		private float CalcProgress()
		{
			if (hydraulicsArray.Length == 0)
			{
				return 0f;
			}
			if (hydraulicsArray.Length == 1)
			{
				return hydraulicsArray[0].CalcProgress();
			}
			return hydraulicsArray.Min((Hydraulics h) => h.CalcProgress());
		}

		public void MergeAllInProximity(float immediateMergeMaxdistance)
		{
			splitNodes.Sort(CompareNodesByTheirPositionX);
			List<NodePair> nodePairs = FindNodePairsWithinDistance(splitNodes, immediateMergeMaxdistance);
			MergeClosestPairs(nodePairs);
		}

		private void MonitorNodes()
		{
			splitNodes.Sort(CompareNodesByTheirPositionX);
			List<NodePair> nodesInProximity = FindNodePairsWithinDistance(splitNodes, config.maxMergeDistance);
			List<NodePair> nodePairs = FindSeparatingPairs(nodesInProximity);
			MergeClosestPairs(nodePairs);
		}

		private static int CompareNodesByTheirPositionX(NodeHandle a, NodeHandle b)
		{
			if (a.pos.x < b.pos.x)
			{
				return -1;
			}
			if (a.pos.x > b.pos.x)
			{
				return 1;
			}
			return 0;
		}

		private static int CompareNodePairsByTheirDistanceSqr(NodePair a, NodePair b)
		{
			if (a.distSqr < b.distSqr)
			{
				return -1;
			}
			if (a.distSqr > b.distSqr)
			{
				return 1;
			}
			return 0;
		}

		private static List<NodePair> FindNodePairsWithinDistance(List<NodeHandle> nodes, float maxDist)
		{
			float num = maxDist * maxDist;
			nodePair_buffer.Clear();
			List<NodePair> list = nodePair_buffer;
			for (int i = 0; i < nodes.Count - 1; i++)
			{
				NodeHandle nodeHandle = nodes[i];
				for (int j = i + 1; j < nodes.Count; j++)
				{
					NodeHandle nodeHandle2 = nodes[j];
					if (!(maxDist < nodeHandle2.pos.x - nodeHandle.pos.x))
					{
						float sqrMagnitude = (nodeHandle2.pos - nodeHandle.pos).sqrMagnitude;
						if (sqrMagnitude <= num)
						{
							list.Add(new NodePair
							{
								a = nodeHandle,
								b = nodeHandle2,
								distSqr = sqrMagnitude
							});
						}
					}
				}
			}
			return list;
		}

		private List<NodePair> FindSeparatingPairs(List<NodePair> nodesInProximity)
		{
			lastNodePair_bufferIdx = 1 - lastNodePair_bufferIdx;
			List<NodePair> list = nodePair_buffer2;
			Dictionary<int, float> dictionary = ((lastNodePair_bufferIdx == 0) ? newDistSqr_buffer : newDistSqr_buffer_double);
			list.Clear();
			dictionary.Clear();
			for (int i = 0; i < nodesInProximity.Count; i++)
			{
				NodePair nodePair = nodesInProximity[i];
				int num = nodePair.CalcUid_Temp();
				float num2 = float.PositiveInfinity;
				if (nodePairUidToDistSqr.Keys.Contains(num))
				{
					num2 = nodePairUidToDistSqr[num];
				}
				if (num2 < nodePair.distSqr)
				{
					NodePair item = nodePair;
					if (config.usePrevClosestDistance_experimental)
					{
						item.distSqr = num2;
						nodePair.a.solverNode.pos -= nodePair.a.solverNode.vel;
						nodePair.b.solverNode.pos -= nodePair.b.solverNode.vel;
					}
					list.Add(item);
				}
				else
				{
					dictionary.Add(num, nodePair.distSqr);
				}
			}
			nodePairUidToDistSqr = dictionary;
			return list;
		}

		private void MergeClosestPairs(List<NodePair> nodePairs)
		{
			nodePairs.Sort(CompareNodePairsByTheirDistanceSqr);
			for (int i = 0; i < nodePairs.Count; i++)
			{
				NodePair nodePair = nodePairs[i];
				if (!IsPairConnected(nodePair) && !nodePair.IsDoubleAnchor() && !mergedNodePairUids.Contains(nodePair.CalcUid_Temp()))
				{
					mergedNodePairUids.Add(nodePair.CalcUid_Temp());
					_mergedNodes.Add(nodePair.a);
					_mergedNodes.Add(nodePair.b);
					if (config.mergeCallback != null)
					{
						config.mergeCallback(nodePair.a, nodePair.b, Mathf.Sqrt(nodePair.distSqr), _mergedNodes);
					}
				}
			}
		}

		internal static bool IsPairConnected(NodeHandle a, NodeHandle b)
		{
			if (b.edges.Count < a.edges.Count)
			{
				Values.Swap(ref a, ref b);
			}
			bool result = false;
			foreach (EdgeHandle edge in a.edges)
			{
				if (edge.GetOther(a) == b)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private static bool IsPairConnected(NodePair nodePair)
		{
			return IsPairConnected(nodePair.a, nodePair.b);
		}
	}
}
