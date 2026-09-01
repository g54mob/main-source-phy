using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CompleteDukesFreightersWithoutFlyingBlocks")]
internal class CompleteDukesFreightersWithoutFlyingBlocks : AchievementTrigger
{
	private const int DukesFreighterIndex = 19;

	internal override int AchievementId
	{
		get
		{
			return 17;
		}
	}

	public CompleteDukesFreightersWithoutFlyingBlocks()
	{
		Debug.LogError("[Achievement] outdated achievment");
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex != 19)
		{
			return;
		}
		List<BlockType> list = new List<BlockType>();
		list.Add(BlockType.Balloon);
		list.Add(BlockType.FlyingBlock);
		list.Add(BlockType.Propeller);
		list.Add(BlockType.SmallPropeller);
		list.Add(BlockType.WingPanel);
		list.Add(BlockType.Wing);
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
