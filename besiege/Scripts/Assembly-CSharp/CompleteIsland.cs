using System.Collections.Generic;
using GameGrind;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteIsland")]
internal abstract class CompleteIsland : AchievementTrigger
{
	private const int TotalLevels = 70;

	private const int CaravanLevelIndex = 54;

	private const int MinefieldLevelIndex = 2;

	internal abstract Island TargetIsland { get; }

	private List<int> GetIslandLevelIndices()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < 70; i++)
		{
			if (ReferenceMaster.LevelToIsland(i + 1) == TargetIsland)
			{
				list.Add(i);
			}
		}
		return list;
	}

	private bool TryGetIsLevelCompleted(int levelIndex)
	{
		if (levelIndex < 0 || levelIndex >= LEVELLORD.levelsComplete.Length)
		{
			return false;
		}
		return LEVELLORD.levelsComplete[levelIndex] == 1;
	}

	private bool IsLevelCompleted(int levelIndex)
	{
		return TryGetIsLevelCompleted(levelIndex);
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		int i = levelIndex + 1;
		if (ReferenceMaster.LevelToIsland(i) != TargetIsland)
		{
			return;
		}
		List<int> islandLevelIndices = GetIslandLevelIndices();
		int num = 0;
		foreach (int item in islandLevelIndices)
		{
			if (IsLevelCompleted(item))
			{
				num++;
			}
		}
		int num2 = num;
		Achievement achievement = Journal.GetAchievement(AchievementId);
		if (num2 > achievement.value)
		{
			int value = ((achievement.value != 0) ? (achievement.value + 1) : num2);
			Journal.SetValue(achievement, value);
		}
	}
}
