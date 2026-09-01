using System.Collections.Generic;
using UnityEngine;

public class EntityChildManager
{
	public List<NetworkEntity> children;

	private NetworkController networkController;

	private uint childAmount;

	private NetworkEntity entity;

	private bool isTracking;

	private CustomLevel level;

	public uint ChildAmount
	{
		get
		{
			return childAmount;
		}
	}

	public EntityChildManager(NetworkBlock netBlock, NetworkController controller, bool track)
	{
		networkController = controller;
		children = new List<NetworkEntity>();
		childAmount = (uint)netBlock.children.Length;
		entity = netBlock;
		isTracking = track;
		level = CustomLevel.Instance;
	}

	public void ClearChildren()
	{
		if (children.Count == 0)
		{
			return;
		}
		networkController.TryRemoveRange(children[0], (uint)children.Count);
		for (int num = children.Count - 1; num >= 0; num--)
		{
			LevelEntity levelEntity = children[num] as LevelEntity;
			if (levelEntity.needsTracking)
			{
				level.RemoveSimTrack(levelEntity);
			}
			levelEntity.isDestroyed = true;
			Object.Destroy(levelEntity.gameObject);
		}
		children.Clear();
	}

	public void InitLevelChildren(LevelEntity parent)
	{
		if (children.Count <= 0)
		{
			childAmount++;
			parent.baseEntity = entity;
			parent.hasBase = true;
			parent.Init(entity.id + childAmount, networkController, null, isTracking);
			networkController.Add(parent);
			children.Add(parent);
			for (int i = 0; i < parent.children.Length; i++)
			{
				LevelEntity levelEntity = parent.children[i] as LevelEntity;
				childAmount++;
				levelEntity.baseEntity = entity;
				levelEntity.hasBase = true;
				levelEntity.Init(entity.id + childAmount, networkController, null, isTracking);
				networkController.Add(levelEntity);
				children.Add(levelEntity);
			}
		}
	}

	public void InitBlockChildren(NetworkBlock parent)
	{
		if (children.Count <= 0)
		{
			for (int i = 0; i < parent.children.Length; i++)
			{
				NetworkBlock networkBlock = parent.children[i];
				childAmount++;
				networkBlock.hasBase = true;
				networkBlock.baseEntity = parent;
				networkBlock.Init(entity.id + childAmount, networkController, null, isTracking);
				networkController.Add(networkBlock);
				children.Add(networkBlock);
			}
		}
	}
}
