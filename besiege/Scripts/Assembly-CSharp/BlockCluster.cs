using System.Collections.Generic;
using UnityEngine;

public class BlockCluster
{
	public BlockNode Base;

	public List<BlockNode> Blocks;

	public float BlockWeight;

	public Vector3 CenterOffset;

	public Vector3 Center = Vector3.zero;

	private BlockLinkManager linkManager;

	public BlockCluster(BlockLinkManager manager)
	{
		Blocks = new List<BlockNode>();
		BlockWeight = 0f;
		linkManager = manager;
	}

	public void Add(BlockNode node)
	{
		Blocks.Add(node);
	}

	public void Remove(BlockNode node)
	{
		Blocks.Remove(node);
	}

	private void AddSize(Vector3 blockPos)
	{
		linkManager.minX = ((!(blockPos.x < linkManager.minX)) ? linkManager.minX : blockPos.x);
		linkManager.maxX = ((!(blockPos.x > linkManager.maxX)) ? linkManager.maxX : blockPos.x);
		linkManager.minY = ((!(blockPos.y < linkManager.minY)) ? linkManager.minY : blockPos.y);
		linkManager.maxY = ((!(blockPos.y > linkManager.maxY)) ? linkManager.maxY : blockPos.y);
		linkManager.minZ = ((!(blockPos.z < linkManager.minZ)) ? linkManager.minZ : blockPos.z);
		linkManager.maxZ = ((!(blockPos.z > linkManager.maxZ)) ? linkManager.maxZ : blockPos.z);
	}

	public void FindBase()
	{
		if (Blocks.Count == 0)
		{
			Debug.LogError("Block cluster has no blocks!");
			return;
		}
		List<BlockNode> list = Blocks.FindAll((BlockNode x) => x.Prefab.clusterBaseCandidate);
		if (list.Count == 0)
		{
			Base = Blocks[0];
			Vector3 position = Base.Position;
			AddSize(position);
			Center = position;
			return;
		}
		if (list.Count == 1)
		{
			Base = list[0];
			Vector3 position = Base.Position;
			AddSize(position);
			Center = position;
			return;
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		int count = Blocks.Count;
		for (int num4 = 0; num4 < count; num4++)
		{
			Vector3 position = Blocks[num4].Position;
			AddSize(position);
			num += position.x;
			num2 += position.y;
			num3 += position.z;
		}
		Center.Set(num / (float)count, num2 / (float)count, num3 / (float)count);
		float num5 = 0f;
		int index = -1;
		for (int num4 = 0; num4 < list.Count; num4++)
		{
			Vector3 position = list[num4].Position;
			float num6 = position.x - Center.x;
			float num7 = position.y - Center.y;
			float num8 = position.z - Center.z;
			float num9 = num6 * num6 + num7 * num7 + num8 * num8;
			if (num4 == 0 || num9 <= num5)
			{
				num5 = num9;
				index = num4;
			}
		}
		Base = list[index];
	}
}
