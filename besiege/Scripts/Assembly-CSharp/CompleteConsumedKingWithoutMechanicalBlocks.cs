using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CompleteConsumedKingWithoutMechanicalBlocks")]
internal class CompleteConsumedKingWithoutMechanicalBlocks : LevelAchievementTrigger
{
	private const int ConsumedKingIndex = 41;

	internal override int AchievementId
	{
		get
		{
			return 14;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 41;
		}
	}

	public CompleteConsumedKingWithoutMechanicalBlocks()
	{
		Debug.LogError("[Achievement] outdated achievment");
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex != 41)
		{
			return;
		}
		List<BlockType> list = new List<BlockType>();
		BlockMenuControl menu;
		if (!BlockMenuControl.GetMenu("t_MECHANICAL", out menu))
		{
			Debug.LogError("[Achievement] Couldn't unlock achievement " + AchievementId + " since Mechanical category couldn't be found!");
			return;
		}
		for (int i = 0; i < menu.buttons.Length; i++)
		{
			BlockType myIndex = (BlockType)menu.buttons[i].myIndex;
			BlockType blockType = myIndex;
			if (blockType != BlockType.Pin && blockType != BlockType.CameraBlock)
			{
				list.Add(myIndex);
			}
		}
		List<BlockBehaviour> buildingBlocks = machine.BuildingBlocks;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			if (list.Contains(buildingBlocks[i].Prefab.Type))
			{
				return;
			}
		}
		Trigger();
	}
}
