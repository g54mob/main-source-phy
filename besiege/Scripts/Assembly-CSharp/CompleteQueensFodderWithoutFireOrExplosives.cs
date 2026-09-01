using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CompleteQueensFodderWithoutFireOrExplosives")]
internal class CompleteQueensFodderWithoutFireOrExplosives : LevelAchievementTrigger
{
	private const int QueensFodderIndex = 4;

	internal override int AchievementId
	{
		get
		{
			return 10;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 4;
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex != 4)
		{
			return;
		}
		List<BlockType> list = new List<BlockType>();
		list.Add(BlockType.Bomb);
		list.Add(BlockType.FlameBall);
		list.Add(BlockType.Flamethrower);
		list.Add(BlockType.Rocket);
		list.Add(BlockType.Torch);
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
