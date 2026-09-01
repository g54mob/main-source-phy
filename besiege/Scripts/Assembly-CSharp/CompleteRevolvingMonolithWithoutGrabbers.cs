using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CompleteRevolvingMonolithWithoutGrabbers")]
internal class CompleteRevolvingMonolithWithoutGrabbers : LevelAchievementTrigger
{
	private const int RevolvingMonolithIndex = 42;

	internal override int AchievementId
	{
		get
		{
			return 27;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 42;
		}
	}

	public CompleteRevolvingMonolithWithoutGrabbers()
	{
		Debug.LogError("[Achievement] outdated achievment");
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex != 42)
		{
			return;
		}
		List<BlockType> list = new List<BlockType>();
		list.Add(BlockType.Grabber);
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
