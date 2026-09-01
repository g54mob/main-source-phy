using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteSPLevelUnder2Seconds")]
internal class CompleteSPLevelUnder2Seconds : AchievementTrigger
{
	private const float MaximumCompletionTime = 2f;

	internal override int AchievementId
	{
		get
		{
			return 0;
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (completionTime < 2f)
		{
			Trigger();
		}
	}
}
