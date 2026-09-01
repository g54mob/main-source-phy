using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CompleteMidlandPatrolTentsIntact")]
internal class CompleteMidlandPatrolTentsIntact : LevelAchievementTrigger
{
	private const int MidlandPatrol = 23;

	internal override int AchievementId
	{
		get
		{
			return 50;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 23;
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex == 23 && Object.FindObjectsOfType<AISpawner>().Length == 6)
		{
			Trigger();
		}
	}
}
