using UnityEngine;

[AddComponentMenu("Achievements/Trigger/Level Specific/CompleteToweringEyeWithoutBeingHit")]
internal class CompleteToweringEyeWithoutBeingHit : LevelAchievementTrigger
{
	private const int ToweringEyeIndex = 44;

	internal override int AchievementId
	{
		get
		{
			return 28;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 44;
		}
	}

	public CompleteToweringEyeWithoutBeingHit()
	{
		Debug.LogError("[Achievement] outdated achievment");
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (levelIndex == 44 && !DesertMirror1.BlocksBeenHit)
		{
			Trigger();
		}
	}
}
