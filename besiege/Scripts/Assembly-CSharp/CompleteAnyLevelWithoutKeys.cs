using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteAnyLevelWithoutKeys")]
internal class CompleteAnyLevelWithoutKeys : CompleteLevelWithoutKeys
{
	internal override int AchievementId
	{
		get
		{
			return 22;
		}
	}
}
