using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeController
{
	private class NodeContext
	{
		public List<BuildNodeBlock> Nodes;

		public List<BuildEdgeBlock> Edges;

		public List<LineRenderer> Lines;

		public LineRenderer CurrentLine;

		public Vector3 GhostPos;
	}

	private class NodeMergeEntry
	{
		public BuildNodeBlock Parent;

		public HashSet<BuildNodeBlock> Children = new HashSet<BuildNodeBlock>();

		public bool HasNode(BuildNodeBlock node)
		{
			return Parent == node || Children.Contains(node);
		}
	}

	private class EdgeMergeEntry
	{
		public BuildEdgeBlock Parent;

		public HashSet<BuildEdgeBlock> Children = new HashSet<BuildEdgeBlock>();

		public bool HasEdge(BuildEdgeBlock edge)
		{
			return Parent == edge || Children.Contains(edge);
		}
	}

	private class EdgeUpdateEntry
	{
		public BuildEdgeBlock Edge;

		public BuildNodeBlock Start;

		public BuildNodeBlock End;
	}

	public static float mergeThresholdSqr = 9.8E-05f;

	private NodeContext[] context;

	private Color mouseLineColor;

	private NodeBuildingGridController gridController;

	private Machine machine;

	private float lineWidth = 0.1f;

	private float dashScale = 2f;

	private float mouseLineAlpha;

	private List<BlockBehaviour> refreshBlocks = new List<BlockBehaviour>();

	private bool isRefreshing;

	private bool needsRefresh;

	private bool isInitialized;

	private bool isMerging;

	public bool IsBuilding
	{
		get
		{
			return context[0].Nodes.Count > 0;
		}
	}

	public bool IsMerging
	{
		get
		{
			return isMerging;
		}
	}

	public NodeController(Machine m)
	{
		machine = m;
	}

	public bool IsUsingBlock(BlockBehaviour block)
	{
		NodeContext[] array = context;
		foreach (NodeContext nodeContext in array)
		{
			if (block.Prefab.Type == BlockType.BuildEdge)
			{
				if (nodeContext.Edges.Contains(block as BuildEdgeBlock))
				{
					return true;
				}
			}
			else if (block.Prefab.Type == BlockType.BuildNode && nodeContext.Nodes.Contains(block as BuildNodeBlock))
			{
				return true;
			}
		}
		return false;
	}

	public static bool RaySphereIntersection(Ray ray, Vector3 point, float radius, out float d)
	{
		d = -1f;
		Vector3 vector = ray.origin - point;
		float num = Vector3.Dot(vector, ray.direction);
		float num2 = Vector3.Dot(vector, vector) - radius * radius;
		if (num2 > 0f && num > 0f)
		{
			return false;
		}
		float num3 = num * num - num2;
		if (num3 < 0f)
		{
			return false;
		}
		d = 0f - num - Mathf.Sqrt(num3);
		if (d < 0f)
		{
			d = 0f;
		}
		return true;
	}

	public void Refresh(BlockBehaviour block)
	{
		if (!refreshBlocks.Contains(block))
		{
			switch (block.Prefab.Type)
			{
			case BlockType.BuildNode:
				return;
			case BlockType.BuildEdge:
				refreshBlocks.Insert(0, block);
				break;
			case BlockType.BuildSurface:
				refreshBlocks.Add(block);
				break;
			}
			isRefreshing = true;
		}
	}

	public void CancelRefresh(BlockBehaviour block)
	{
		refreshBlocks.Remove(block);
	}

	public void Refresh()
	{
		if (RefreshBlocks(false, false))
		{
			needsRefresh = true;
		}
	}

	private void AutoRefresh()
	{
		if (!RefreshBlocks(true, true) && !needsRefresh)
		{
			return;
		}
		needsRefresh = false;
		if (StatMaster.isMP && machine.isLocalMachine)
		{
			byte[] bytes = BitConverter.GetBytes(machine.PlayerID);
			if (StatMaster.cachingTransformActions)
			{
				(machine as ServerMachine).CacheBlockTransformAction(RPCMessageType.RefreshBlocks, bytes);
			}
			else
			{
				NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.RefreshBlocks, bytes);
			}
		}
	}

	public IEnumerator IERefreshBlocks(bool reorder, bool resetList)
	{
		if (!isRefreshing)
		{
			yield break;
		}
		int surfaceSpawnCount = 0;
		bool orderChanged = false;
		NetworkAuxAddPiece auxAddPiece = NetworkAuxAddPiece.Instance;
		bool staggerSurfaceSpawn = auxAddPiece.receivedGameState && (machine.isLocalMachine || StatMaster.isHosting);
		bool even = false;
		for (int i = 0; i < refreshBlocks.Count; i++)
		{
			BlockBehaviour block = refreshBlocks[i];
			bool lastBlockSurface = false;
			BlockType type = block.Prefab.Type;
			if (type != BlockType.BuildEdge)
			{
				if (type != BlockType.BuildSurface)
				{
					continue;
				}
				lastBlockSurface = true;
				BuildSurface surface = block as BuildSurface;
				if (reorder && surface.needsSort)
				{
					if (machine.SortSurface(surface))
					{
						orderChanged = true;
					}
					surface.needsSort = false;
				}
				surface.UpdateSurface();
			}
			else
			{
				BuildEdgeBlock edge = block as BuildEdgeBlock;
				if (reorder && edge.needsSort)
				{
					if (machine.SortEdge(edge))
					{
						orderChanged = true;
					}
					edge.needsSort = false;
				}
				edge.UpdateEdge();
			}
			if (!staggerSurfaceSpawn)
			{
				continue;
			}
			surfaceSpawnCount++;
			if (!lastBlockSurface)
			{
				if (surfaceSpawnCount >= OptionsMaster.BesiegeConfig.MVBlocksPerFrame * 4)
				{
					surfaceSpawnCount = 0;
					yield return null;
				}
			}
			else if (surfaceSpawnCount >= OptionsMaster.BesiegeConfig.MVSurfacesPerFrame)
			{
				surfaceSpawnCount = 0;
				yield return null;
				if (even)
				{
					yield return null;
				}
				even = !even;
			}
		}
		if (reorder)
		{
			isRefreshing = false;
			if (orderChanged)
			{
				machine.UpdateIndices();
			}
		}
		if (resetList)
		{
			refreshBlocks.Clear();
		}
	}

	public bool RefreshBlocks(bool reorder, bool resetList)
	{
		if (!isRefreshing || (StatMaster.isMP && machine.spawningMachine))
		{
			return false;
		}
		bool flag = false;
		bool result = false;
		for (int i = 0; i < refreshBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = refreshBlocks[i];
			switch (blockBehaviour.Prefab.Type)
			{
			case BlockType.BuildEdge:
			{
				BuildEdgeBlock buildEdgeBlock = blockBehaviour as BuildEdgeBlock;
				if (reorder && buildEdgeBlock.needsSort)
				{
					if (machine.SortEdge(buildEdgeBlock))
					{
						flag = true;
						result = true;
					}
					buildEdgeBlock.needsSort = false;
				}
				if (buildEdgeBlock.UpdateEdge())
				{
					result = true;
				}
				break;
			}
			case BlockType.BuildSurface:
			{
				BuildSurface buildSurface = blockBehaviour as BuildSurface;
				if (reorder && buildSurface.needsSort)
				{
					if (machine.SortSurface(buildSurface))
					{
						flag = true;
						result = true;
					}
					buildSurface.needsSort = false;
				}
				if (buildSurface.UpdateSurface())
				{
					result = true;
				}
				break;
			}
			}
		}
		if (reorder)
		{
			isRefreshing = false;
			if (flag)
			{
				machine.UpdateIndices();
			}
		}
		if (resetList)
		{
			refreshBlocks.Clear();
		}
		return result;
	}

	public void RefreshVisuals()
	{
		if (!isRefreshing)
		{
			return;
		}
		for (int i = 0; i < refreshBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = refreshBlocks[i];
			switch (blockBehaviour.Prefab.Type)
			{
			case BlockType.BuildEdge:
			{
				BuildEdgeBlock buildEdgeBlock = blockBehaviour as BuildEdgeBlock;
				buildEdgeBlock.UpdateEdge();
				break;
			}
			case BlockType.BuildSurface:
			{
				BuildSurface buildSurface = blockBehaviour as BuildSurface;
				buildSurface.UpdateMesh();
				break;
			}
			}
		}
	}

	public void RefreshFragments()
	{
		for (int i = 0; i < refreshBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = refreshBlocks[i];
			BlockType type = blockBehaviour.Prefab.Type;
			if (type == BlockType.BuildSurface)
			{
				BuildSurface buildSurface = blockBehaviour as BuildSurface;
				buildSurface.GenerateFractureFragments();
			}
		}
	}

	public List<UndoAction> Merge(out Dictionary<BlockBehaviour, BlockBehaviour> mergeDict, out HashSet<BlockBehaviour> removeList)
	{
		if (UndoSystem.processing)
		{
			Debug.LogError("trying to merge during an undo/redo");
			mergeDict = new Dictionary<BlockBehaviour, BlockBehaviour>();
			removeList = new HashSet<BlockBehaviour>();
			return new List<UndoAction>();
		}
		isMerging = true;
		mergeDict = new Dictionary<BlockBehaviour, BlockBehaviour>();
		removeList = new HashSet<BlockBehaviour>();
		List<UndoAction> list = new List<UndoAction>();
		List<BuildNodeBlock> list2 = new List<BuildNodeBlock>();
		List<BuildNodeBlock> list3 = new List<BuildNodeBlock>();
		HashSet<BlockBehaviour> removeBlocks = new HashSet<BlockBehaviour>();
		HashSet<BuildEdgeBlock> hashSet = new HashSet<BuildEdgeBlock>();
		List<BuildEdgeBlock> list4 = new List<BuildEdgeBlock>();
		List<BuildSurface> list5 = new List<BuildSurface>();
		List<NodeMergeEntry> list6 = new List<NodeMergeEntry>();
		List<EdgeMergeEntry> list7 = new List<EdgeMergeEntry>();
		List<EdgeUpdateEntry> list8 = new List<EdgeUpdateEntry>();
		List<BlockBehaviour> list9 = new List<BlockBehaviour>();
		NodeMergeEntry nodeMergeEntry = null;
		EdgeMergeEntry edgeMergeEntry = null;
		bool flag = false;
		BlockBehaviour block;
		for (int i = 0; i < machine.BlockCount; i++)
		{
			if (machine.GetBlockFromIndex(i, out block) && block.Prefab.Type == BlockType.BuildNode)
			{
				if (block.IsSelected)
				{
					list3.Add(block as BuildNodeBlock);
				}
				else
				{
					list2.Add(block as BuildNodeBlock);
				}
			}
		}
		if (list3.Count == 0)
		{
			isMerging = false;
			return list;
		}
		for (int i = 0; i < list2.Count; i++)
		{
			BuildNodeBlock buildNodeBlock = list2[i];
			Vector3 position = buildNodeBlock.Position;
			for (int j = 0; j < list3.Count; j++)
			{
				BuildNodeBlock buildNodeBlock2 = list3[j];
				if ((buildNodeBlock2.Position - position).sqrMagnitude > mergeThresholdSqr)
				{
					continue;
				}
				flag = false;
				for (int k = 0; k < list6.Count; k++)
				{
					nodeMergeEntry = list6[k];
					if (nodeMergeEntry.HasNode(buildNodeBlock2) || nodeMergeEntry.HasNode(buildNodeBlock))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					if (nodeMergeEntry.Parent != buildNodeBlock2)
					{
						nodeMergeEntry.Children.Add(buildNodeBlock2);
					}
				}
				else
				{
					list6.Add(new NodeMergeEntry
					{
						Parent = buildNodeBlock,
						Children = new HashSet<BuildNodeBlock> { buildNodeBlock2 }
					});
				}
			}
		}
		for (int i = 0; i < list3.Count; i++)
		{
			BuildNodeBlock buildNodeBlock = list3[i];
			Vector3 position = buildNodeBlock.Position;
			for (int j = i + 1; j < list3.Count; j++)
			{
				BuildNodeBlock buildNodeBlock2 = list3[j];
				if ((buildNodeBlock2.Position - position).sqrMagnitude > mergeThresholdSqr)
				{
					continue;
				}
				flag = false;
				for (int l = 0; l < list6.Count; l++)
				{
					nodeMergeEntry = list6[l];
					if (nodeMergeEntry.HasNode(buildNodeBlock2) || nodeMergeEntry.HasNode(buildNodeBlock))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					if (nodeMergeEntry.Parent != buildNodeBlock2 && !nodeMergeEntry.Children.Contains(buildNodeBlock2))
					{
						nodeMergeEntry.Children.Add(buildNodeBlock2);
					}
					if (nodeMergeEntry.Parent != buildNodeBlock && !nodeMergeEntry.Children.Contains(buildNodeBlock))
					{
						nodeMergeEntry.Children.Add(buildNodeBlock);
					}
				}
				else
				{
					list6.Add(new NodeMergeEntry
					{
						Parent = buildNodeBlock,
						Children = new HashSet<BuildNodeBlock> { buildNodeBlock2 }
					});
				}
			}
		}
		foreach (NodeMergeEntry item in list6)
		{
			if (item.Parent.IsSelected)
			{
				foreach (BuildNodeBlock child in item.Children)
				{
					if (child.IsSelected)
					{
						continue;
					}
					item.Children.Add(item.Parent);
					item.Parent = child;
					item.Children.Remove(child);
					break;
				}
			}
			foreach (BuildNodeBlock child2 in item.Children)
			{
				if (!removeBlocks.Contains(child2))
				{
					removeBlocks.Add(child2);
					mergeDict.Add(child2, item.Parent);
				}
			}
		}
		for (int i = 0; i < machine.BlockCount; i++)
		{
			if (!machine.GetBlockFromIndex(i, out block) || block.Prefab.Type != BlockType.BuildSurface)
			{
				continue;
			}
			BuildSurface buildSurface = block as BuildSurface;
			if (!buildSurface.isValid)
			{
				continue;
			}
			bool flag2 = false;
			for (int j = 0; j < buildSurface.edges.Length; j++)
			{
				BuildEdgeBlock buildEdgeBlock = buildSurface.edges[j];
				if (!buildEdgeBlock.isValid)
				{
					continue;
				}
				if (removeBlocks.Contains(buildEdgeBlock.startNode) || removeBlocks.Contains(buildEdgeBlock.endNode))
				{
					if (!hashSet.Contains(buildEdgeBlock))
					{
						hashSet.Add(buildEdgeBlock);
					}
					if (!list4.Contains(buildEdgeBlock))
					{
						list4.Add(buildEdgeBlock);
					}
					flag2 = true;
					continue;
				}
				for (int m = 0; m < list6.Count; m++)
				{
					BuildNodeBlock parent = list6[m].Parent;
					if (buildEdgeBlock.startNode == parent || buildEdgeBlock.endNode == parent)
					{
						if (!hashSet.Contains(buildEdgeBlock))
						{
							hashSet.Add(buildEdgeBlock);
						}
						flag2 = true;
					}
				}
			}
			if (flag2 && !list5.Contains(buildSurface))
			{
				list5.Add(buildSurface);
			}
		}
		for (int i = 0; i < list4.Count; i++)
		{
			BuildEdgeBlock buildEdgeBlock = list4[i];
			if (!buildEdgeBlock.isValid)
			{
				removeBlocks.Add(buildEdgeBlock);
				continue;
			}
			BuildNodeBlock buildNodeBlock3 = buildEdgeBlock.startNode;
			BuildNodeBlock buildNodeBlock4 = buildEdgeBlock.endNode;
			bool flag4;
			bool flag3 = (flag4 = false);
			for (int n = 0; n < list6.Count; n++)
			{
				nodeMergeEntry = list6[n];
				if (!flag3 && nodeMergeEntry.HasNode(buildNodeBlock3))
				{
					buildNodeBlock3 = nodeMergeEntry.Parent;
					flag3 = true;
				}
				if (!flag4)
				{
					if (nodeMergeEntry.HasNode(buildNodeBlock4))
					{
						buildNodeBlock4 = nodeMergeEntry.Parent;
						flag4 = true;
					}
				}
				else if (flag3)
				{
					break;
				}
			}
			if (removeBlocks.Contains(buildNodeBlock3) || removeBlocks.Contains(buildNodeBlock4) || buildNodeBlock3 == buildNodeBlock4)
			{
				removeBlocks.Add(buildEdgeBlock);
			}
			else if (!(buildNodeBlock3 == buildEdgeBlock.startNode) || !(buildNodeBlock4 == buildEdgeBlock.endNode))
			{
				list8.Add(new EdgeUpdateEntry
				{
					Edge = buildEdgeBlock,
					Start = buildNodeBlock3,
					End = buildNodeBlock4
				});
			}
		}
		foreach (BuildEdgeBlock item2 in hashSet)
		{
			BuildEdgeBlock buildEdgeBlock = item2;
			Vector3 position = buildEdgeBlock.Position;
			BuildNodeBlock buildNodeBlock3 = buildEdgeBlock.startNode;
			BuildNodeBlock buildNodeBlock4 = buildEdgeBlock.endNode;
			for (int j = 0; j < list8.Count; j++)
			{
				EdgeUpdateEntry edgeUpdateEntry = list8[j];
				if (edgeUpdateEntry.Edge == buildEdgeBlock)
				{
					buildNodeBlock3 = edgeUpdateEntry.Start;
					buildNodeBlock4 = edgeUpdateEntry.End;
					break;
				}
			}
			for (int j = 0; j < list8.Count; j++)
			{
				EdgeUpdateEntry edgeUpdateEntry = list8[j];
				BuildEdgeBlock edge = edgeUpdateEntry.Edge;
				if (buildEdgeBlock == edge || removeBlocks.Contains(edge) || (!(buildNodeBlock3 == edgeUpdateEntry.Start) && !(buildNodeBlock3 == edgeUpdateEntry.End)) || (!(buildNodeBlock4 == edgeUpdateEntry.Start) && !(buildNodeBlock4 == edgeUpdateEntry.End)) || (edge.Position - position).sqrMagnitude > mergeThresholdSqr)
				{
					continue;
				}
				flag = false;
				for (int num = 0; num < list7.Count; num++)
				{
					edgeMergeEntry = list7[num];
					if (edgeMergeEntry.HasEdge(buildEdgeBlock))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					if (edgeMergeEntry.Parent != edge && !edgeMergeEntry.Children.Contains(edge))
					{
						edgeMergeEntry.Children.Add(edge);
					}
					if (edgeMergeEntry.Parent != buildEdgeBlock && !edgeMergeEntry.Children.Contains(buildEdgeBlock))
					{
						edgeMergeEntry.Children.Add(buildEdgeBlock);
					}
				}
				else
				{
					list7.Add(new EdgeMergeEntry
					{
						Parent = buildEdgeBlock,
						Children = new HashSet<BuildEdgeBlock> { edge }
					});
				}
			}
		}
		for (int i = 0; i < list7.Count; i++)
		{
			edgeMergeEntry = list7[i];
			if (edgeMergeEntry.Parent.IsSelected)
			{
				foreach (BuildEdgeBlock child3 in edgeMergeEntry.Children)
				{
					if (child3.IsSelected)
					{
						continue;
					}
					edgeMergeEntry.Children.Add(edgeMergeEntry.Parent);
					edgeMergeEntry.Parent = child3;
					edgeMergeEntry.Children.Remove(child3);
					break;
				}
			}
			foreach (BuildEdgeBlock child4 in edgeMergeEntry.Children)
			{
				if (!removeBlocks.Contains(child4))
				{
					removeBlocks.Add(child4);
					mergeDict.Add(child4, edgeMergeEntry.Parent);
				}
			}
		}
		for (int i = 0; i < list8.Count; i++)
		{
			EdgeUpdateEntry edgeUpdateEntry2 = list8[i];
			if (!removeBlocks.Contains(edgeUpdateEntry2.Edge))
			{
				BlockInfo blockInfo = BlockInfo.FromBlockBehaviour(edgeUpdateEntry2.Edge);
				BlockInfo blockInfo2 = new BlockInfo(blockInfo);
				BuildEdgeBlock.WriteData(blockInfo2.BlockData, edgeUpdateEntry2.Start, edgeUpdateEntry2.End);
				machine.EditBlockData(edgeUpdateEntry2.Edge, blockInfo2.BlockData);
				list.Add(new UndoActionEditSurface(machine, blockInfo2, blockInfo));
			}
		}
		list5.Sort((BuildSurface x, BuildSurface y) => x.BuildIndex.CompareTo(y.BuildIndex));
		for (int i = 0; i < list5.Count; i++)
		{
			BuildSurface buildSurface = list5[i];
			if (removeBlocks.Contains(buildSurface))
			{
				continue;
			}
			if (!buildSurface.isValid)
			{
				removeBlocks.Add(buildSurface);
				CheckIsolation(list9, removeBlocks, buildSurface);
				continue;
			}
			bool flag5 = false;
			List<BuildEdgeBlock> list10 = new List<BuildEdgeBlock>();
			for (int j = 0; j < buildSurface.edges.Length; j++)
			{
				BuildEdgeBlock buildEdgeBlock = buildSurface.edges[j];
				flag = false;
				for (int num2 = 0; num2 < list7.Count; num2++)
				{
					edgeMergeEntry = list7[num2];
					if (edgeMergeEntry.HasEdge(buildEdgeBlock))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					list10.Add(edgeMergeEntry.Parent);
					flag5 = true;
				}
				else if (!removeBlocks.Contains(buildEdgeBlock))
				{
					list10.Add(buildEdgeBlock);
				}
				else
				{
					flag5 = true;
				}
			}
			if (!flag5)
			{
				continue;
			}
			if (list10.Count < 3)
			{
				removeBlocks.Add(buildSurface);
				CheckIsolation(list9, removeBlocks, buildSurface);
				continue;
			}
			for (int j = 0; j < list5.Count; j++)
			{
				BuildSurface buildSurface2 = list5[j];
				bool flag6 = list10.Intersect(buildSurface2.edges).Count() > 2;
				if (i == j || !buildSurface.isValid || removeBlocks.Contains(buildSurface2) || !flag6)
				{
					continue;
				}
				if (buildSurface.IsSelected)
				{
					if (flag6)
					{
						mergeDict.Add(buildSurface2, buildSurface);
					}
					removeBlocks.Add(buildSurface2);
					CheckIsolation(list9, removeBlocks, buildSurface2);
					continue;
				}
				if (buildSurface2.IsSelected)
				{
					if (flag6 && !mergeDict.ContainsKey(buildSurface))
					{
						mergeDict.Add(buildSurface, buildSurface2);
					}
					removeBlocks.Add(buildSurface);
					CheckIsolation(list9, removeBlocks, buildSurface);
					break;
				}
				int num3 = 0;
				int num4 = 0;
				for (int m = 0; m < buildSurface.nodes.Length; m++)
				{
					if (buildSurface.edges[m].IsSelected || buildSurface.nodes[m].IsSelected)
					{
						num3++;
					}
				}
				for (int m = 0; m < buildSurface2.nodes.Length; m++)
				{
					if (buildSurface2.edges[m].IsSelected || buildSurface2.nodes[m].IsSelected)
					{
						num4++;
					}
				}
				if (num3 > num4)
				{
					if (flag6)
					{
						mergeDict.Add(buildSurface2, buildSurface);
					}
					removeBlocks.Add(buildSurface2);
					CheckIsolation(list9, removeBlocks, buildSurface2);
					continue;
				}
				if (flag6 && !mergeDict.ContainsKey(buildSurface))
				{
					mergeDict.Add(buildSurface, buildSurface2);
				}
				removeBlocks.Add(buildSurface);
				CheckIsolation(list9, removeBlocks, buildSurface);
				break;
			}
			BlockInfo blockInfo3 = BlockInfo.FromBlockBehaviour(buildSurface);
			BlockInfo blockInfo4 = new BlockInfo(blockInfo3);
			BuildSurface.WriteData(blockInfo4.BlockData, list10.ToArray());
			machine.EditBlockData(buildSurface, blockInfo4.BlockData);
			list.Add(new UndoActionEditSurface(machine, blockInfo4, blockInfo3));
		}
		for (int i = 0; i < list5.Count; i++)
		{
			BuildSurface buildSurface = list5[i];
			if (!removeBlocks.Contains(buildSurface))
			{
				for (int j = 0; j < buildSurface.edges.Length; j++)
				{
					BuildEdgeBlock buildEdgeBlock = buildSurface.edges[j];
					list9.Remove(buildEdgeBlock);
					list9.Remove(buildEdgeBlock.startNode);
					list9.Remove(buildEdgeBlock.endNode);
				}
			}
		}
		for (int i = 0; i < list9.Count; i++)
		{
			block = list9[i];
			if (removeBlocks.Contains(block))
			{
				continue;
			}
			removeBlocks.Add(block);
			if (mergeDict.ContainsKey(block))
			{
				mergeDict.Remove(block);
			}
			else
			{
				if (!mergeDict.ContainsValue(block))
				{
					continue;
				}
				List<BlockBehaviour> list11 = new List<BlockBehaviour>();
				foreach (KeyValuePair<BlockBehaviour, BlockBehaviour> item3 in mergeDict)
				{
					if (item3.Value == block)
					{
						list11.Add(item3.Key);
					}
				}
				for (int j = 0; j < list11.Count; j++)
				{
					mergeDict.Remove(list11[j]);
				}
			}
		}
		List<BlockBehaviour> list12 = removeBlocks.ToList();
		for (int i = 0; i < list12.Count; i++)
		{
			block = list12[i];
			if (!block.SurfaceType)
			{
				continue;
			}
			if (block.Prefab.Type == BlockType.BuildSurface)
			{
				BuildSurface buildSurface = block as BuildSurface;
				if (!buildSurface.isValid)
				{
					continue;
				}
				for (int j = 0; j < buildSurface.edges.Length; j++)
				{
					BuildEdgeBlock buildEdgeBlock = buildSurface.edges[j];
					if (!removeBlocks.Contains(buildEdgeBlock) && (!buildEdgeBlock.isValid || GetSurfaces(buildEdgeBlock).TrueForAll((BuildSurface x) => removeBlocks.Contains(x))))
					{
						removeBlocks.Add(buildEdgeBlock);
					}
				}
			}
			else
			{
				if (block.Prefab.Type != BlockType.BuildEdge)
				{
					continue;
				}
				BuildEdgeBlock buildEdgeBlock = block as BuildEdgeBlock;
				if (buildEdgeBlock.isValid)
				{
					if (!removeBlocks.Contains(buildEdgeBlock.startNode) && GetSurfaces(buildEdgeBlock.startNode).TrueForAll((BuildSurface x) => removeBlocks.Contains(x)))
					{
						removeBlocks.Add(buildEdgeBlock.startNode);
					}
					if (!removeBlocks.Contains(buildEdgeBlock.endNode) && GetSurfaces(buildEdgeBlock.endNode).TrueForAll((BuildSurface x) => removeBlocks.Contains(x)))
					{
						removeBlocks.Add(buildEdgeBlock.endNode);
					}
				}
			}
		}
		list12.Sort((BlockBehaviour x, BlockBehaviour y) => y.Prefab.Type.CompareTo(x.Prefab.Type));
		for (int i = 0; i < list12.Count; i++)
		{
			block = list12[i];
			if (block.IsSelected)
			{
				list.Add(new UndoActionDeselect(machine, block.Guid, block.IsSelectedExtra, block.SymmetryIndex, block.TransformMultiplier));
			}
			if (!mergeDict.ContainsKey(block))
			{
				removeList.Add(block);
			}
			list.Add(new UndoActionRemove(machine, BlockInfo.FromBlockBehaviour(block)));
			machine.RemoveBlock(block);
		}
		isMerging = false;
		if (list.Count > 0)
		{
			machine.UpdateIndices();
			machine.Analyze();
			machine.RebuildExistingClusters((from BlockBehaviour x in list5
				where !removeBlocks.Contains(x)
				select x).ToList());
		}
		return list;
	}

	private void CheckIsolation(List<BlockBehaviour> checkList, HashSet<BlockBehaviour> removeSet, BuildSurface surface)
	{
		for (int i = 0; i < surface.edges.Length; i++)
		{
			BuildEdgeBlock buildEdgeBlock = surface.edges[i];
			if (!checkList.Contains(buildEdgeBlock) && !removeSet.Contains(buildEdgeBlock))
			{
				checkList.Insert(0, buildEdgeBlock);
			}
			if (!checkList.Contains(buildEdgeBlock.startNode) && !removeSet.Contains(buildEdgeBlock.startNode))
			{
				checkList.Add(buildEdgeBlock.startNode);
			}
			if (!checkList.Contains(buildEdgeBlock.endNode) && !removeSet.Contains(buildEdgeBlock.endNode))
			{
				checkList.Add(buildEdgeBlock.endNode);
			}
		}
	}

	public void Initialize()
	{
		if (!machine.isLocalMachine)
		{
			return;
		}
		isInitialized = true;
		Machine obj = machine;
		obj.onBatchOperationComplete = (Action)Delegate.Combine(obj.onBatchOperationComplete, new Action(OnBatchOperationComplete));
		int num = 8;
		context = new NodeContext[num];
		for (int i = 0; i < num; i++)
		{
			LineRenderer lineRenderer = CreateLine();
			lineRenderer.material = ReferenceMaster.Instance.surfaceMouseGhost;
			lineRenderer.SetVertexCount(2);
			List<LineRenderer> list = new List<LineRenderer>(4);
			for (int j = 0; j < 4; j++)
			{
				list.Add(CreateLine());
			}
			if (i == 0)
			{
				mouseLineColor = lineRenderer.material.GetColor("_TintColor");
				mouseLineAlpha = mouseLineColor.a;
			}
			context[i] = new NodeContext
			{
				Nodes = new List<BuildNodeBlock>(4),
				Edges = new List<BuildEdgeBlock>(4),
				Lines = list,
				CurrentLine = lineRenderer
			};
		}
		gridController = new NodeBuildingGridController();
		gridController.SetActive(false);
	}

	public void Dispose()
	{
		if (!isInitialized)
		{
			return;
		}
		Machine obj = machine;
		obj.onBatchOperationComplete = (Action)Delegate.Remove(obj.onBatchOperationComplete, new Action(OnBatchOperationComplete));
		gridController.Dispose();
		for (int i = 0; i < context.Length; i++)
		{
			for (int j = 0; j < context[i].Lines.Count; j++)
			{
				if ((bool)context[i].Lines[j])
				{
					UnityEngine.Object.Destroy(context[i].Lines[j].gameObject);
				}
			}
			if ((bool)context[i].CurrentLine)
			{
				UnityEngine.Object.Destroy(context[i].CurrentLine.gameObject);
			}
		}
	}

	public void ResetPlacement()
	{
		if (isInitialized)
		{
			List<UndoAction> list = new List<UndoAction>();
			for (int num = context.Length - 1; num >= 0; num--)
			{
				list.AddRange(ClearPoints(num, true));
			}
			if (list.Count > 0)
			{
				machine.UndoSystem.AddActions(list);
			}
		}
	}

	private LineRenderer CreateLine()
	{
		GameObject gameObject = new GameObject("line", typeof(LineRenderer));
		LineRenderer component = gameObject.GetComponent<LineRenderer>();
		component.material = ReferenceMaster.Instance.surfaceEdgeGhost;
		component.SetWidth(lineWidth, lineWidth);
		component.gameObject.SetActive(false);
		return component;
	}

	private void OnBlockTypeChange(BlockType newType)
	{
		if (newType != BlockType.BuildNode)
		{
			ResetPlacement();
		}
	}

	private void OnInMenuChange()
	{
		ResetPlacement();
	}

	private void OnResetEditor()
	{
		ResetPlacement();
	}

	private void EnableGrid(BuildNodeBlock node)
	{
		gridController.SetActive(true);
		gridController.FocusOn(node);
		StatMaster.SelectedBlockChanged += OnBlockTypeChange;
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		instance.OnGhostChanged = (Action<bool, Transform>)Delegate.Combine(instance.OnGhostChanged, new Action<bool, Transform>(OnGhostChanged));
		StatMaster.inMenuChanged = (Action)Delegate.Combine(StatMaster.inMenuChanged, new Action(OnInMenuChange));
		ReferenceMaster.ResetEditor += OnResetEditor;
	}

	private void Clear()
	{
		gridController.SetActive(false);
		StatMaster.SelectedBlockChanged -= OnBlockTypeChange;
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		instance.OnGhostChanged = (Action<bool, Transform>)Delegate.Remove(instance.OnGhostChanged, new Action<bool, Transform>(OnGhostChanged));
		StatMaster.inMenuChanged = (Action)Delegate.Remove(StatMaster.inMenuChanged, new Action(OnInMenuChange));
		ReferenceMaster.ResetEditor -= OnResetEditor;
		for (int i = 0; i < context.Length; i++)
		{
			context[i].CurrentLine.gameObject.SetActive(false);
		}
		ResetOutOfBounds();
	}

	public void Toggle(BlockBehaviour block, bool isAdd, int index = 0)
	{
		if (isAdd && StatMaster.SelectedBlockId != BlockType.BuildNode)
		{
			SingleInstanceFindOnly<AddPiece>.Instance.SetBlockType(BlockType.BuildNode);
		}
		NodeContext nodeContext = context[index];
		if (block.Prefab.Type == BlockType.BuildNode)
		{
			BuildNodeBlock buildNodeBlock = block as BuildNodeBlock;
			buildNodeBlock.SetIsPreliminary(isAdd);
			if (isAdd)
			{
				if (index == 0)
				{
					if (nodeContext.Nodes.Count == 0)
					{
						EnableGrid(buildNodeBlock);
					}
					else
					{
						gridController.FocusOn(buildNodeBlock);
					}
				}
				nodeContext.Nodes.Add(buildNodeBlock);
			}
			else
			{
				nodeContext.Nodes.Remove(buildNodeBlock);
				if (nodeContext.Nodes.Count == 0)
				{
					Clear();
				}
			}
		}
		else
		{
			if (block.Prefab.Type != BlockType.BuildEdge)
			{
				return;
			}
			BuildEdgeBlock buildEdgeBlock = block as BuildEdgeBlock;
			if (isAdd || buildEdgeBlock.gameObject != null)
			{
				buildEdgeBlock.gameObject.SetActive(!isAdd);
			}
			if (isAdd)
			{
				CreateLine(nodeContext.Lines[nodeContext.Edges.Count], buildEdgeBlock);
				nodeContext.Edges.Add(buildEdgeBlock);
				return;
			}
			int num = nodeContext.Edges.IndexOf(buildEdgeBlock);
			if (num != -1)
			{
				RemoveLine(nodeContext.Lines[num]);
				nodeContext.Edges.Remove(buildEdgeBlock);
			}
		}
	}

	private void ResetOutOfBounds()
	{
		bool outOfBounds = StatMaster.Bounding.inRoof || StatMaster.Bounding.inGround || StatMaster.Bounding.inRightWall || StatMaster.Bounding.inLeftWall || StatMaster.Bounding.inFrontWall || StatMaster.Bounding.inBackWall;
		SingleInstanceFindOnly<AddPiece>.Instance.SetOutOfBounds(outOfBounds);
	}

	public void OnCtrlPressed()
	{
		for (int i = 0; i < context.Length; i++)
		{
			if (context[i].CurrentLine.gameObject.activeInHierarchy)
			{
				UpdateGhost(i, context[i].GhostPos, true);
			}
		}
	}

	public void UpdateGhost(int index, Vector3 pos, bool active)
	{
		NodeContext nodeContext = context[index];
		List<BuildNodeBlock> nodes = nodeContext.Nodes;
		GameObject gameObject = nodeContext.CurrentLine.gameObject;
		nodeContext.GhostPos = pos;
		if (!active || nodes.Count == 0)
		{
			if (gameObject.activeInHierarchy)
			{
				gameObject.SetActive(false);
				ResetOutOfBounds();
			}
			return;
		}
		if (!gameObject.activeInHierarchy)
		{
			gameObject.SetActive(true);
		}
		BuildNodeBlock buildNodeBlock = nodes[nodes.Count - 1];
		BuildEdgeBlock edge = null;
		bool flag = false;
		Vector3 vector = machine.BuildingMachine.InverseTransformPoint(pos);
		for (int i = 0; i < machine.BlockCount; i++)
		{
			BlockBehaviour block;
			if (machine.GetBlockFromIndex(i, out block) && block.Prefab.Type == BlockType.BuildNode && !(block.Position != vector))
			{
				BuildEdgeBlock edge2;
				if (FindEdge(buildNodeBlock, block as BuildNodeBlock, out edge2))
				{
					flag = true;
					edge = edge2;
				}
				break;
			}
		}
		if (flag)
		{
			CreateLine(nodeContext.CurrentLine, edge, true);
			return;
		}
		Vector3 position = buildNodeBlock.transform.position;
		LineRenderer currentLine = nodeContext.CurrentLine;
		float magnitude = (position - pos).magnitude;
		if (magnitude > 6f)
		{
			currentLine.material.SetColor("_TintColor", new Color(1f, 0.149f, 0.235f, mouseLineAlpha));
			SingleInstanceFindOnly<AddPiece>.Instance.SetOutOfBounds(true);
		}
		else
		{
			currentLine.material.SetColor("_TintColor", mouseLineColor);
			SingleInstanceFindOnly<AddPiece>.Instance.SetOutOfBounds(false);
		}
		Vector3[] array = new Vector3[2] { position, pos };
		currentLine.SetVertexCount(2);
		currentLine.SetPositions(array);
		currentLine.material.SetTextureScale("_MainTex", new Vector2((array[1] - array[0]).magnitude * dashScale, 0f));
	}

	private void OnGhostChanged(bool active, Transform ghost)
	{
		UpdateGhost(0, ghost.position, active);
	}

	private void CreateLine(LineRenderer line, BuildEdgeBlock edge, bool setScale = false)
	{
		line.gameObject.SetActive(true);
		int num = 2;
		Matrix4x4 localToWorldMatrix = edge.ParentMachine.BuildingMachine.localToWorldMatrix;
		Vector3[] array;
		if (edge.isStraight)
		{
			array = new Vector3[2]
			{
				localToWorldMatrix.MultiplyPoint3x4(edge.startNode.Position),
				localToWorldMatrix.MultiplyPoint3x4(edge.endNode.Position)
			};
		}
		else
		{
			edge.UpdateEdge();
			CancelRefresh(edge);
			float magnitude = (edge.Position - edge.startNode.Position + (edge.endNode.Position - edge.Position)).magnitude;
			if (setScale)
			{
				line.material.SetTextureScale("_MainTex", new Vector2(magnitude * dashScale, 0f));
			}
			num = Mathf.CeilToInt(magnitude / 0.2f);
			float num2 = (float)num - 1f;
			array = new Vector3[num];
			for (int i = 0; i < num; i++)
			{
				float t = (float)i / num2;
				array[i] = localToWorldMatrix.MultiplyPoint3x4(edge.Interp(t));
			}
		}
		line.SetVertexCount(num);
		line.SetPositions(array);
	}

	private void RemoveLine(LineRenderer line)
	{
		line.gameObject.SetActive(false);
	}

	private void OnBatchOperationComplete()
	{
		RefreshFragments();
		AutoRefresh();
		if (machine.isLoadingDifference)
		{
			ClearUnusedContexts();
		}
	}

	private void ClearUnusedContexts()
	{
		SymmetryController symmetryController = SingleInstanceFindOnly<AddPiece>.Instance.symmetryController;
		HashSet<int> hashSet = new HashSet<int>();
		if (symmetryController.axis[0] == 0f)
		{
			hashSet.Add(0);
			hashSet.Add(3);
			hashSet.Add(5);
			hashSet.Add(6);
		}
		if (symmetryController.axis[1] == 0f)
		{
			hashSet.Add(1);
			hashSet.Add(3);
			hashSet.Add(4);
			hashSet.Add(6);
		}
		if (symmetryController.axis[2] == 0f)
		{
			hashSet.Add(2);
			hashSet.Add(4);
			hashSet.Add(5);
			hashSet.Add(6);
		}
		List<UndoAction> list = new List<UndoAction>();
		foreach (int item in hashSet)
		{
			list.AddRange(ClearPoints(item + 1, true));
		}
		if (list.Count > 0)
		{
			machine.UndoSystem.AddActions(list);
			List<BuildNodeBlock> nodes = context[0].Nodes;
			if (nodes.Count > 0)
			{
				EnableGrid(nodes[nodes.Count - 1]);
			}
		}
	}

	public List<UndoAction> ClearPoints(int index, bool removeUnused = false)
	{
		List<UndoAction> list = new List<UndoAction>();
		NodeContext nodeContext = context[index];
		while (nodeContext.Edges.Count > 0)
		{
			BuildEdgeBlock buildEdgeBlock = nodeContext.Edges[nodeContext.Edges.Count - 1];
			list.Add(new UndoActionSurface(machine, buildEdgeBlock.Guid, index, false));
			Toggle(buildEdgeBlock, false, index);
			if (!removeUnused || IsUsed(buildEdgeBlock))
			{
				continue;
			}
			bool flag = true;
			for (int i = 0; i < context.Length; i++)
			{
				if (i != index && context[i].Edges.Contains(buildEdgeBlock))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				list.Add(new UndoActionRemove(machine, BlockInfo.FromBlockBehaviour(buildEdgeBlock)));
				machine.RemoveBlock(buildEdgeBlock);
			}
		}
		while (nodeContext.Nodes.Count > 0)
		{
			BuildNodeBlock buildNodeBlock = nodeContext.Nodes[nodeContext.Nodes.Count - 1];
			list.Add(new UndoActionSurface(machine, buildNodeBlock.Guid, index, false));
			Toggle(buildNodeBlock, false, index);
			if (!removeUnused || IsUsed(buildNodeBlock))
			{
				continue;
			}
			bool flag2 = true;
			for (int j = 0; j < context.Length; j++)
			{
				if (j != index && context[j].Nodes.Contains(buildNodeBlock))
				{
					flag2 = false;
					break;
				}
			}
			if (flag2)
			{
				list.Add(new UndoActionRemove(machine, BlockInfo.FromBlockBehaviour(buildNodeBlock)));
				machine.RemoveBlock(buildNodeBlock);
			}
		}
		return list;
	}

	public List<UndoAction> Select(BuildNodeBlock node, int index)
	{
		List<UndoAction> list = new List<UndoAction>();
		if (machine.isLoadingInfo)
		{
			return list;
		}
		NodeContext nodeContext = context[index];
		BuildEdgeBlock edge;
		UndoAction undoAction;
		BuildSurface surface;
		if (nodeContext.Nodes.Count == 3 && nodeContext.Nodes[0] == node)
		{
			if (FindOrCreateEdge(nodeContext.Nodes[2], node, out edge, out undoAction))
			{
				if (undoAction != null)
				{
					list.Add(undoAction);
				}
				list.Add(new UndoActionSurface(machine, edge.Guid, index, true));
				Toggle(edge, true, index);
				if (CreateSurface(nodeContext.Edges.ToArray(), out surface, out undoAction))
				{
					if (undoAction != null)
					{
						list.Add(undoAction);
					}
					list.AddRange(ClearPoints(index));
				}
			}
			return list;
		}
		if (nodeContext.Nodes.Contains(node))
		{
			list.AddRange(ClearPoints(index, true));
			return list;
		}
		if (nodeContext.Nodes.Count > 0 && FindOrCreateEdge(nodeContext.Nodes[nodeContext.Nodes.Count - 1], node, out edge, out undoAction))
		{
			if (undoAction != null)
			{
				list.Add(undoAction);
			}
			list.Add(new UndoActionSurface(machine, edge.Guid, index, true));
			Toggle(edge, true, index);
		}
		list.Add(new UndoActionSurface(machine, node.Guid, index, true));
		Toggle(node, true, index);
		if (nodeContext.Nodes.Count == 4)
		{
			if (FindOrCreateEdge(node, nodeContext.Nodes[0], out edge, out undoAction))
			{
				if (undoAction != null)
				{
					list.Add(undoAction);
				}
				list.Add(new UndoActionSurface(machine, edge.Guid, index, true));
				Toggle(edge, true, index);
				if (CreateSurface(nodeContext.Edges.ToArray(), out surface, out undoAction))
				{
					if (undoAction != null)
					{
						list.Add(undoAction);
					}
					list.AddRange(ClearPoints(index));
				}
			}
			return list;
		}
		return list;
	}

	public bool IsUsed(BlockBehaviour block)
	{
		for (int i = 0; i < machine.BlockCount; i++)
		{
			BlockBehaviour block2;
			if (!machine.GetBlockFromIndex(i, out block2) || block2.Prefab.Type != BlockType.BuildSurface)
			{
				continue;
			}
			BuildSurface buildSurface = block2 as BuildSurface;
			if (!buildSurface.isValid)
			{
				continue;
			}
			if (block.Prefab.Type == BlockType.BuildNode)
			{
				for (int j = 0; j < buildSurface.edges.Length; j++)
				{
					BuildEdgeBlock buildEdgeBlock = buildSurface.edges[j];
					if (buildEdgeBlock.isValid && (block == buildEdgeBlock.startNode || block == buildEdgeBlock.endNode))
					{
						return true;
					}
				}
			}
			else if (block.Prefab.Type == BlockType.BuildEdge)
			{
				for (int j = 0; j < buildSurface.edges.Length; j++)
				{
					BuildEdgeBlock buildEdgeBlock = buildSurface.edges[j];
					if (buildEdgeBlock.isValid && block == buildEdgeBlock)
					{
						return true;
					}
				}
			}
			else if (block.Prefab.Type == BlockType.BuildSurface)
			{
				return block == buildSurface;
			}
		}
		return false;
	}

	public List<BuildEdgeBlock> GetEdges(BuildNodeBlock node)
	{
		List<BuildEdgeBlock> list = new List<BuildEdgeBlock>();
		for (int i = 0; i < machine.BlockCount; i++)
		{
			BlockBehaviour block;
			if (machine.GetBlockFromIndex(i, out block) && block.Prefab.Type == BlockType.BuildEdge)
			{
				BuildEdgeBlock buildEdgeBlock = block as BuildEdgeBlock;
				if (buildEdgeBlock.isValid && (buildEdgeBlock.startNode == node || buildEdgeBlock.endNode == node))
				{
					list.Add(buildEdgeBlock);
				}
			}
		}
		return list;
	}

	public List<BuildSurface> GetSurfaces(BlockBehaviour block)
	{
		List<BuildSurface> list = new List<BuildSurface>();
		for (int i = 0; i < machine.BlockCount; i++)
		{
			BlockBehaviour block2;
			if (!machine.GetBlockFromIndex(i, out block2) || block2.Prefab.Type != BlockType.BuildSurface)
			{
				continue;
			}
			BuildSurface buildSurface = block2 as BuildSurface;
			if (!buildSurface.isValid)
			{
				continue;
			}
			bool flag = false;
			if (block.Prefab.Type == BlockType.BuildNode)
			{
				for (int j = 0; j < buildSurface.edges.Length; j++)
				{
					BuildEdgeBlock buildEdgeBlock = buildSurface.edges[j];
					if (buildEdgeBlock.isValid && (block == buildEdgeBlock.startNode || block == buildEdgeBlock.endNode))
					{
						flag = true;
						break;
					}
				}
			}
			else if (block.Prefab.Type == BlockType.BuildEdge)
			{
				for (int j = 0; j < buildSurface.edges.Length; j++)
				{
					BuildEdgeBlock buildEdgeBlock = buildSurface.edges[j];
					if (buildEdgeBlock.isValid && block == buildEdgeBlock)
					{
						flag = true;
						break;
					}
				}
			}
			else if (block.Prefab.Type == BlockType.BuildSurface)
			{
				flag = block == buildSurface;
			}
			if (flag)
			{
				list.Add(buildSurface);
			}
		}
		return list;
	}

	public void AddDependencies(List<BlockBehaviour> blocks)
	{
		for (int i = 0; i < machine.BlockCount; i++)
		{
			BlockBehaviour block;
			if (!machine.GetBlockFromIndex(i, out block) || !block.SurfaceType || blocks.Contains(block))
			{
				continue;
			}
			switch (block.Prefab.Type)
			{
			case BlockType.BuildNode:
			{
				BuildNodeBlock buildNodeBlock = block as BuildNodeBlock;
				List<BuildSurface> surfaces = GetSurfaces(buildNodeBlock);
				if (!blocks.Contains(buildNodeBlock) && surfaces.TrueForAll((BuildSurface s) => blocks.Contains(s)))
				{
					blocks.Add(buildNodeBlock);
				}
				break;
			}
			case BlockType.BuildEdge:
			{
				BuildEdgeBlock buildEdgeBlock = block as BuildEdgeBlock;
				if (!buildEdgeBlock.isValid)
				{
					break;
				}
				List<BuildSurface> surfaces2 = GetSurfaces(buildEdgeBlock);
				if (!blocks.Contains(buildEdgeBlock) && surfaces2.TrueForAll((BuildSurface s) => blocks.Contains(s)))
				{
					blocks.Add(buildEdgeBlock);
				}
				if (!blocks.Contains(buildEdgeBlock.startNode))
				{
					List<BuildSurface> surfaces = GetSurfaces(buildEdgeBlock.startNode);
					if (surfaces.TrueForAll((BuildSurface s) => blocks.Contains(s)))
					{
						blocks.Add(buildEdgeBlock.startNode);
					}
				}
				if (!blocks.Contains(buildEdgeBlock.endNode))
				{
					List<BuildSurface> surfaces = GetSurfaces(buildEdgeBlock.endNode);
					if (surfaces.TrueForAll((BuildSurface s) => blocks.Contains(s)))
					{
						blocks.Add(buildEdgeBlock.endNode);
					}
				}
				break;
			}
			case BlockType.BuildSurface:
			{
				BuildSurface buildSurface = block as BuildSurface;
				if (!buildSurface.isValid)
				{
					break;
				}
				bool flag = false;
				for (int j = 0; j < buildSurface.nodes.Length; j++)
				{
					if (blocks.Contains(buildSurface.nodes[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					for (int j = 0; j < buildSurface.edges.Length; j++)
					{
						if (blocks.Contains(buildSurface.edges[j]))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag || blocks.Contains(buildSurface))
				{
					break;
				}
				blocks.Add(buildSurface);
				for (int j = 0; j < buildSurface.nodes.Length; j++)
				{
					BuildNodeBlock buildNodeBlock = buildSurface.nodes[j];
					List<BuildSurface> surfaces = GetSurfaces(buildNodeBlock);
					if (!blocks.Contains(buildNodeBlock) && surfaces.TrueForAll((BuildSurface s) => blocks.Contains(s)))
					{
						blocks.Add(buildNodeBlock);
					}
				}
				for (int j = 0; j < buildSurface.edges.Length; j++)
				{
					BuildEdgeBlock buildEdgeBlock = buildSurface.edges[j];
					List<BuildSurface> surfaces2 = GetSurfaces(buildEdgeBlock);
					if (!blocks.Contains(buildEdgeBlock) && surfaces2.TrueForAll((BuildSurface e) => blocks.Contains(e)))
					{
						blocks.Add(buildEdgeBlock);
					}
				}
				break;
			}
			}
		}
		blocks.Sort((BlockBehaviour x, BlockBehaviour y) => x.Prefab.Type.CompareTo(y.Prefab.Type));
	}

	private bool FindEdge(BuildNodeBlock startNode, BuildNodeBlock endNode, out BuildEdgeBlock edge)
	{
		bool flag = InputManager.AdvancedBuilding.LeftCtrlKey();
		for (int i = 0; i < machine.BlockCount; i++)
		{
			BlockBehaviour block;
			if (!machine.GetBlockFromIndex(i, out block) || block.Prefab.Type != BlockType.BuildEdge)
			{
				continue;
			}
			BuildEdgeBlock buildEdgeBlock = block as BuildEdgeBlock;
			if (buildEdgeBlock.isValid && ((buildEdgeBlock.startNode == startNode && buildEdgeBlock.endNode == endNode) || (buildEdgeBlock.startNode == endNode && buildEdgeBlock.endNode == startNode)))
			{
				buildEdgeBlock.UpdatePlanar();
				if (!flag)
				{
					edge = buildEdgeBlock;
					return true;
				}
				if (buildEdgeBlock.isStraight)
				{
					edge = buildEdgeBlock;
					return true;
				}
			}
		}
		edge = null;
		return false;
	}

	private bool FindOrCreateEdge(BuildNodeBlock startNode, BuildNodeBlock endNode, out BuildEdgeBlock edge, out UndoAction undoAction)
	{
		undoAction = null;
		if (FindEdge(startNode, endNode, out edge))
		{
			return true;
		}
		Vector3 position = startNode.Position;
		Vector3 position2 = endNode.Position;
		Vector3 position3 = position + (position2 - position) * 0.5f;
		XDataHolder xDataHolder = new XDataHolder();
		BuildEdgeBlock.WriteData(xDataHolder, startNode, endNode);
		BlockInfo blockInfo = new BlockInfo();
		blockInfo.BlockData = xDataHolder;
		blockInfo.ID = BlockType.BuildEdge;
		blockInfo.Position = position3;
		BlockInfo blockInfo2 = blockInfo;
		BlockBehaviour block;
		if (!machine.AddBlock(blockInfo2, out block))
		{
			edge = null;
			return false;
		}
		edge = block as BuildEdgeBlock;
		undoAction = new UndoActionAdd(machine, blockInfo2);
		return true;
	}

	private bool CreateSurface(BuildEdgeBlock[] edges, out BuildSurface surface, out UndoAction undoAction)
	{
		undoAction = null;
		List<BuildSurface> surfaces = GetSurfaces(edges[0]);
		for (int i = 0; i < surfaces.Count; i++)
		{
			if (surfaces[i].edges.Intersect(edges.ToList()).Count() > 2)
			{
				surface = surfaces[i];
				return true;
			}
		}
		BuildEdgeBlock buildEdgeBlock = edges[0];
		BuildEdgeBlock buildEdgeBlock2 = edges[1];
		BuildNodeBlock buildNodeBlock = ((!(buildEdgeBlock.endNode == buildEdgeBlock2.startNode) && !(buildEdgeBlock.endNode == buildEdgeBlock2.endNode)) ? buildEdgeBlock.endNode : buildEdgeBlock.startNode);
		XDataHolder xDataHolder = new XDataHolder();
		BuildSurface.WriteData(xDataHolder, edges);
		Machine parentMachine = buildNodeBlock.ParentMachine;
		BlockPrefab blockPrefab = PrefabMaster.BlockPrefabs[73];
		BlockInfo blockInfo = new BlockInfo();
		blockInfo.BlockData = xDataHolder;
		blockInfo.ID = BlockType.BuildSurface;
		blockInfo.Position = buildNodeBlock.Position;
		blockInfo.Rotation = buildNodeBlock.Rotation;
		blockInfo.Skin = blockPrefab.VisualController.selectedSkin;
		BlockInfo blockInfo2 = blockInfo;
		BlockBehaviour block;
		if (!parentMachine.AddBlock(blockInfo2, out block))
		{
			surface = null;
			return false;
		}
		block.VisualController.PlaceFromPrefab();
		undoAction = new UndoActionAdd(parentMachine, blockInfo2);
		surface = block as BuildSurface;
		return true;
	}
}
