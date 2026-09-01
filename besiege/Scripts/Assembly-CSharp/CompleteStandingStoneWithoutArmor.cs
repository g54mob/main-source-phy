using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Achievements/Achievement/CompleteStandingStoneWithoutArmor")]
internal class CompleteStandingStoneWithoutArmor : LevelAchievementTrigger
{
	private const int StandingStoneIndex = 6;

	internal override int AchievementId
	{
		get
		{
			return 15;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 6;
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex != 6)
		{
			return;
		}
		List<BlockType> list = new List<BlockType>();
		list.Add(BlockType.ArmorPlateLarge);
		list.Add(BlockType.ArmorPlateRound);
		list.Add(BlockType.ArmorPlateSmall);
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
