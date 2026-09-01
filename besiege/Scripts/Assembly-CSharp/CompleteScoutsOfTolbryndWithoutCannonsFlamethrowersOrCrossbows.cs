using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/Level Specific/CompleteScoutsOfTolbryndWithoutCannonsFlamethrowersOrCrossbows")]
internal class CompleteScoutsOfTolbryndWithoutCannonsFlamethrowersOrCrossbows : LevelAchievementTrigger
{
	private const int ScoutsOfTolbryndIndex = 17;

	internal override int AchievementId
	{
		get
		{
			return 11;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 17;
		}
	}

	public CompleteScoutsOfTolbryndWithoutCannonsFlamethrowersOrCrossbows()
	{
		Debug.LogError("[Achievement] outdated achievment");
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex != 17)
		{
			return;
		}
		List<BlockType> list = new List<BlockType>();
		list.Add(BlockType.ShrapnelCannon);
		list.Add(BlockType.Cannon);
		list.Add(BlockType.Flamethrower);
		list.Add(BlockType.Crossbow);
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
