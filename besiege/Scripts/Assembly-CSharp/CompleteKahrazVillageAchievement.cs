using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CompleteKahrazVillageAchievement")]
internal class CompleteKahrazVillageAchievement : LevelAchievementTrigger
{
	private const int KahrazVillageIndex = 51;

	private const float MaxCompletionTime = 30f;

	internal override int AchievementId
	{
		get
		{
			return 34;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 51;
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex == 51 && !(completionTime > 30f))
		{
			Trigger();
		}
	}
}
