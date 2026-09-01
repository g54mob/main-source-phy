using System.Collections.Generic;
using UnityEngine;

public class BlockNode
{
	public BlockBehaviour Block;

	public BlockPrefab Prefab;

	public BlockType Type;

	public bool hasPosition;

	public List<BlockLink> Neighbours;

	private MyBounds myBounds;

	private int nodeIndex;

	private Vector3 _position;

	private readonly bool hasBounds;

	private readonly bool hasBVC;

	public Vector3 Position
	{
		get
		{
			if (hasPosition)
			{
				return _position;
			}
			if (!hasBounds || !hasBVC)
			{
				hasPosition = true;
				return _position;
			}
			Vector3 center = myBounds.localBounds.center;
			_position.Set(center.x, center.y, center.z);
			hasPosition = true;
			return _position;
		}
	}

	public BlockNode(BlockBehaviour block)
	{
		Block = block;
		Prefab = block.Prefab;
		hasBVC = Block.Prefab.hasBVC;
		nodeIndex = Block.NodeIndex;
		Neighbours = new List<BlockLink>();
		myBounds = block.myBounds;
		hasBounds = block.Prefab.hasMyBounds;
		Type = block.Prefab.Type;
		_position = block.transform.position;
	}

	public byte[] Encode(int prefixSize)
	{
		byte[][] array = new byte[Neighbours.Count][];
		int num = 0;
		int num2;
		for (int i = 0; i < Neighbours.Count; i++)
		{
			BlockLink blockLink = Neighbours[i];
			num2 = 0;
			int buildIndex = blockLink.Other.Block.BuildIndex;
			List<byte> list = new List<byte>();
			for (int j = 0; j < blockLink.Triggers.Count; j++)
			{
				BlockTrigger blockTrigger = blockLink.Triggers[j];
				if (blockTrigger.isOwnLink)
				{
					byte item = (byte)((blockTrigger.Index << 1) | (blockTrigger.isDynamic ? 1 : 0));
					list.Add(item);
				}
			}
			int num3 = NetworkCompression.PackedUIntLength(list.Count, true);
			int num4 = NetworkCompression.PackedUIntLength(buildIndex, true);
			byte[] array2 = new byte[num4 + num3 + list.Count];
			NetworkCompression.PackUInt(buildIndex, array2, num2, true, num4);
			num2 += num4;
			NetworkCompression.PackUInt(list.Count, array2, num2, true, num3);
			num2 += num3;
			for (int k = 0; k < list.Count; k++)
			{
				array2[num2++] = list[k];
			}
			array[i] = array2;
			num += array2.Length;
		}
		int count = array.Length;
		int num5 = NetworkCompression.PackedUIntLength(count, true);
		byte[] array3 = new byte[prefixSize + num5 + num];
		num2 = prefixSize;
		NetworkCompression.PackUInt(count, array3, num2, true, num5);
		num2 += num5;
		NetworkCompression.WriteArray(array, array3, num2);
		return array3;
	}

	private int CompareNode(BlockLink a, BlockLink b)
	{
		return a.Other.nodeIndex.CompareTo(b.Other.nodeIndex);
	}

	public void Link(BlockNode node, TriggerSetJointBase trigger, bool isOwn)
	{
		bool flag = false;
		BlockLink blockLink = null;
		foreach (BlockLink neighbour in Neighbours)
		{
			if (neighbour.Other == node)
			{
				blockLink = neighbour;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			blockLink = new BlockLink(node);
			int i;
			for (i = 0; i < Neighbours.Count && Neighbours[i].Other.nodeIndex < blockLink.Other.nodeIndex; i++)
			{
			}
			Neighbours.Insert(i, blockLink);
		}
		blockLink.AddTrigger(trigger, isOwn);
	}

	public bool Unlink(BlockNode node)
	{
		for (int i = 0; i < Neighbours.Count; i++)
		{
			BlockLink blockLink = Neighbours[i];
			if (blockLink.Other == node)
			{
				Neighbours.Remove(blockLink);
				return true;
			}
		}
		return false;
	}
}
