using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteSPLevelUnder6Score")]
internal class CompleteSPLevelUnder3Blocks : AchievementTrigger
{
	private const float MaxBlockScore = 6f;

	internal override int AchievementId
	{
		get
		{
			return 1;
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if ((float)machine.BlocksCost <= 6f)
		{
			Trigger();
		}
	}
}
