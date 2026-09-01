using GameGrind;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteCampaign")]
internal class CompleteCampaign : CompleteIsland
{
	internal override Island TargetIsland
	{
		get
		{
			return Island.Krolmar;
		}
	}

	internal override int AchievementId
	{
		get
		{
			return 32;
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		int num = levelIndex + 1;
		if (ReferenceMaster.LevelToIsland(num) != TargetIsland)
		{
			return;
		}
		int i;
		for (i = num; ReferenceMaster.LevelToIsland(i + 1) == TargetIsland; i++)
		{
		}
		if (i == num)
		{
			Achievement achievement = Journal.GetAchievement(AchievementId);
			if (achievement.value != 1)
			{
				Journal.SetValue(achievement, 1);
			}
		}
	}
}
