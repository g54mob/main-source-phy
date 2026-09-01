using UnityEngine;

[AddComponentMenu("Achievements/Trigger/Level Specific/CompleteTreeOfAkhmoraWithoutProjectiles")]
internal class CompleteTreeOfAkhmoraWithoutProjectiles : LevelAchievementTrigger
{
	private const int TreeOfAkhmoraIndex = 48;

	internal override int AchievementId
	{
		get
		{
			return 29;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 48;
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex == 48 && !machine.hasFiredProjectiles)
		{
			Trigger();
		}
	}
}
