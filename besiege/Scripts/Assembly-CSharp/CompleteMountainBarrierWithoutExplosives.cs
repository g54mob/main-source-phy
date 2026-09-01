using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CompleteMountainBarrierWithoutExplosives")]
internal class CompleteMountainBarrierWithoutExplosives : LevelAchievementTrigger
{
	private const int MountainBarrierIndex = 39;

	internal override int AchievementId
	{
		get
		{
			return 26;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 39;
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex != 39)
		{
			return;
		}
		List<BlockType> list = new List<BlockType>();
		list.Add(BlockType.Bomb);
		list.Add(BlockType.Grenade);
		list.Add(BlockType.Rocket);
		List<BlockType> list2 = list;
		List<BlockBehaviour> buildingBlocks = machine.BuildingBlocks;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			if (list2.Contains(buildingBlocks[i].Prefab.Type))
			{
				return;
			}
		}
		Trigger();
	}
}
