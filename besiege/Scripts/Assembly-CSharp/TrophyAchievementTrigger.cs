using UnityEngine;

[AddComponentMenu("Achievements/Trigger/TrophyAchievementTrigger")]
internal class TrophyAchievementTrigger : LevelAchievementTrigger
{
	public int level;

	public int AchievementID = -1;

	internal override int AchievementId
	{
		get
		{
			return AchievementID;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return level;
		}
	}

	public void ExternalTrigger()
	{
		audioSource.Play();
		if (AchievementID != -1 && !Completed() && WinScreen.IsValid(Machine.Active()))
		{
			Trigger();
		}
	}
}
