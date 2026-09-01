using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockLinkManager : MonoBehaviour
{
	[NonSerialized]
	public List<BlockNode> Nodes;

	[NonSerialized]
	public List<BlockCluster> Clusters;

	[NonSerialized]
	public int PerformanceIndex;

	[NonSerialized]
	public Vector3 Size;

	[NonSerialized]
	public Vector3 Center;

	[NonSerialized]
	public List<BlockNode> IgnoredNodes;

	[NonSerialized]
	private List<BlockNode> visitedNodes;

	[NonSerialized]
	private Queue<BlockNode> nodesToVisit;

	public float minX;

	public float maxX;

	public float minY;

	public float maxY;

	public float minZ;

	public float maxZ;

	protected void Awake()
	{
		Nodes = new List<BlockNode>();
		Clusters = new List<BlockCluster>();
		visitedNodes = new List<BlockNode>();
		nodesToVisit = new Queue<BlockNode>();
		IgnoredNodes = new List<BlockNode>();
		PerformanceIndex = 0;
	}

	public bool AddBlock(BlockBehaviour block, out BlockNode node)
	{
		node = null;
		if (block == null)
		{
			Debug.LogWarning("Adding a node to a null block doesn't work!");
			return false;
		}
		if (block.NodeIndex != -1)
		{
			node = Nodes[block.NodeIndex];
			return true;
		}
		block.NodeIndex = Nodes.Count;
		node = new BlockNode(block);
		Nodes.Add(node);
		return true;
	}

	public void RemoveBlock(BlockBehaviour block)
	{
		if (block.NodeIndex == -1)
		{
			if (StatMaster.Mode.isTranslating || StatMaster.Mode.isRotating || StatMaster.Mode.isScaling)
			{
				BlockType type = block.Prefab.Type;
				if (type != BlockType.BuildNode && type != BlockType.BuildEdge)
				{
					Debug.LogWarning("Block " + block.name + " doesn't have a blocklink-node!");
				}
			}
			return;
		}
		BlockNode blockNode = ((block.NodeIndex < 0 || block.NodeIndex >= Nodes.Count) ? null : Nodes[block.NodeIndex]);
		if (blockNode == null)
		{
			return;
		}
		int index = Nodes.IndexOf(blockNode);
		Nodes.RemoveAt(index);
		block.NodeIndex = -1;
		block.ClusterIndex = -1;
		for (index = 0; index < Nodes.Count; index++)
		{
			BlockNode blockNode2 = Nodes[index];
			blockNode2.Unlink(blockNode);
			if (index >= block.NodeIndex)
			{
				blockNode2.Block.NodeIndex = index;
			}
		}
	}

	public void Link(BlockBehaviour block1, BlockBehaviour block2, TriggerSetJointBase trigger)
	{
		BlockNode node;
		AddBlock(block1, out node);
		BlockNode node2;
		AddBlock(block2, out node2);
		node.Link(node2, trigger, true);
		node2.Link(node, trigger, false);
	}

	public void Reset()
	{
		for (int i = 0; i < Nodes.Count; i++)
		{
			BlockBehaviour block = Nodes[i].Block;
			if (block != null)
			{
				block.NodeIndex = -1;
				block.ClusterIndex = -1;
			}
		}
		Nodes.Clear();
		Clusters.Clear();
		IgnoredNodes.Clear();
		Size = Vector3.zero;
		Center = Vector3.zero;
	}

	public BlockCluster GetCluster(int index)
	{
		if (index < 0 || index >= Clusters.Count)
		{
			return null;
		}
		return Clusters[index];
	}

	public List<BlockLink> GetNeighbours(int index)
	{
		if (index < 0 || index >= Nodes.Count)
		{
			return null;
		}
		return Nodes[index].Neighbours;
	}

	public BlockBehaviour GetLabelTarget()
	{
		int num = 0;
		BlockCluster blockCluster = null;
		for (int i = 0; i < Clusters.Count; i++)
		{
			BlockCluster blockCluster2 = Clusters[i];
			int count = blockCluster2.Blocks.Count;
			if (i == 0 || count > num)
			{
				num = count;
				blockCluster = blockCluster2;
			}
		}
		if (blockCluster == null)
		{
			return null;
		}
		return blockCluster.Base.Block;
	}

	public BlockNode GetNode(int index)
	{
		if (index == -1 || index >= Nodes.Count)
		{
			return null;
		}
		return Nodes[index];
	}

	public BlockNode GetNode(BlockBehaviour block)
	{
		return GetNode(block.NodeIndex);
	}

	public BlockCluster GetCluster(BlockBehaviour block)
	{
		return GetCluster(block.ClusterIndex);
	}

	private void UpdateCluster(BlockNode start, BlockCluster cluster)
	{
		nodesToVisit.Clear();
		nodesToVisit.Enqueue(start);
		cluster.Blocks.Clear();
		while (nodesToVisit.Count > 0)
		{
			BlockNode blockNode = nodesToVisit.Dequeue();
			if (blockNode.Block.ClusterIndex != -1)
			{
				continue;
			}
			visitedNodes.Add(blockNode);
			blockNode.Block.ClusterIndex = Clusters.Count;
			cluster.Add(blockNode);
			foreach (BlockLink neighbour in blockNode.Neighbours)
			{
				if (!neighbour.isDynamic)
				{
					nodesToVisit.Enqueue(neighbour.Other);
				}
			}
		}
		cluster.FindBase();
	}

	public static bool IgnoreType(BlockType bType)
	{
		if (bType == BlockType.Pin || bType == BlockType.CameraBlock || bType == BlockType.BuildNode || bType == BlockType.BuildEdge)
		{
			return true;
		}
		return false;
	}

	private void CreateCluster(BlockNode start)
	{
		if (IgnoreType(start.Type))
		{
			start.Block.ClusterIndex = -2;
			visitedNodes.Add(start);
			IgnoredNodes.Add(start);
		}
		else
		{
			BlockCluster blockCluster = new BlockCluster(this);
			UpdateCluster(start, blockCluster);
			Clusters.Add(blockCluster);
		}
	}

	public void ShowDebug(BlockBehaviour block)
	{
		BlockNode blockNode = Nodes[block.NodeIndex];
		BlockCluster blockCluster = Clusters[block.ClusterIndex];
		BlockBehaviour block2 = blockCluster.Base.Block;
		Debug.Log(block.NodeIndex + " > " + block.name + " " + blockNode.Neighbours.Count, block.gameObject);
		Debug.Log("Cluster info: " + block.ClusterIndex + " " + block2.NodeIndex + " " + block2.name, block2.gameObject);
		for (int i = 0; i < blockNode.Neighbours.Count; i++)
		{
			BlockLink blockLink = blockNode.Neighbours[i];
			Debug.Log(i + ": " + blockLink.Other.Block.name + " " + blockLink.isDynamic, blockLink.Other.Block.gameObject);
		}
	}

	public void Analyze()
	{
		visitedNodes.Clear();
		Clusters.Clear();
		minZ = (minY = (minX = float.MaxValue));
		maxZ = (maxY = (maxX = float.MinValue));
		IgnoredNodes.Clear();
		PerformanceIndex = 0;
		if (Nodes.Count == 0)
		{
			Debug.LogWarning("Can't analyze machine, node list is empty!");
			return;
		}
		List<BlockNode>.Enumerator enumerator = Nodes.GetEnumerator();
		while (enumerator.MoveNext())
		{
			BlockNode current = enumerator.Current;
			current.hasPosition = false;
			current.Block.SetClusterIndexNoUpdate(-1);
		}
		BlockNode start = Nodes[0];
		while (visitedNodes.Count < Nodes.Count)
		{
			CreateCluster(start);
			if (visitedNodes.Count >= Nodes.Count)
			{
				continue;
			}
			foreach (BlockNode node in Nodes)
			{
				if (node.Block.ClusterIndex != -1)
				{
					continue;
				}
				start = node;
				break;
			}
		}
		float num = Nodes.Count - IgnoredNodes.Count;
		for (int i = 0; i < Clusters.Count; i++)
		{
			BlockCluster blockCluster = Clusters[i];
			blockCluster.BlockWeight = (float)blockCluster.Blocks.Count / num;
			blockCluster.CenterOffset = blockCluster.Base.Block.transform.InverseTransformPoint(blockCluster.Center);
		}
		Size = new Vector3(maxX - minX, maxY - minY, maxZ - minZ);
		Center = new Vector3((maxX + minX) / 2f, (maxY + minY) / 2f, (maxZ + minZ) / 2f);
	}

	public int GetTotalBlocks()
	{
		return Nodes.Count - IgnoredNodes.Count;
	}
}
