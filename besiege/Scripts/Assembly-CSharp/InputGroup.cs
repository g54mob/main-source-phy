using System.Collections.Generic;
using UnityEngine;

public class InputGroup
{
	public class BlockEntry
	{
		public MKey key;

		public BlockBehaviour block;

		public bool Compare(BlockEntry other)
		{
			return Compare(other.block, other.key);
		}

		public bool Compare(BlockBehaviour b, MKey k)
		{
			return block == b && key == k;
		}
	}

	public MKey key;

	public List<KeyCode> otherKeys = new List<KeyCode>();

	public List<BlockEntry> blockList;

	public string CustomName;

	public int State;

	public bool dropdownOpen;

	public bool IsChanged()
	{
		return !string.IsNullOrEmpty(CustomName) || State != 0;
	}

	public void AddOtherKeys(MKey k)
	{
		for (int i = 0; i < k.KeysCount; i++)
		{
			KeyCode item = k.GetKey(i);
			if (!key.HasKey(item) && !otherKeys.Contains(item))
			{
				otherKeys.Add(item);
			}
		}
	}

	public bool HasEmptyKey()
	{
		return key.KeysCount == 1 && key.GetKey(0) == KeyCode.None;
	}

	public bool ContainsEntry(BlockBehaviour block, MKey key)
	{
		for (int i = 0; i < blockList.Count; i++)
		{
			BlockEntry blockEntry = blockList[i];
			if (blockEntry.Compare(block, key))
			{
				return true;
			}
		}
		return false;
	}

	public bool ContainsGroup(InputGroup otherGroup)
	{
		for (int i = 0; i < otherGroup.blockList.Count; i++)
		{
			BlockEntry blockEntry = otherGroup.blockList[i];
			bool flag = false;
			for (int j = 0; j < blockList.Count; j++)
			{
				BlockEntry other = blockList[j];
				if (blockEntry.Compare(other))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	public bool EqualGroup(InputGroup otherGroup)
	{
		if (blockList.Count != otherGroup.blockList.Count || key.Ignored != otherGroup.key.Ignored)
		{
			return false;
		}
		for (int i = 0; i < otherGroup.blockList.Count; i++)
		{
			BlockEntry blockEntry = otherGroup.blockList[i];
			if (!ContainsEntry(blockEntry.block, blockEntry.key))
			{
				return false;
			}
		}
		return true;
	}
}
