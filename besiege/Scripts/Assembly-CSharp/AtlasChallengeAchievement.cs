using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/AtlasChallengeAchievement")]
internal class AtlasChallengeAchievement : LevelAchievementTrigger
{
	internal override int AchievementId
	{
		get
		{
			return 30;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 52;
		}
	}

	public void ExternalTrigger()
	{
		audioSource.Play();
		if (!Completed() && WinScreen.IsValid(Machine.Active()))
		{
			Trigger();
		}
	}
}
