using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CompleteOldMiningSiteWithoutKeys")]
internal class CompleteOldMiningSiteWithoutKeys : CompleteLevelWithoutKeys
{
	internal override int AchievementId
	{
		get
		{
			return 23;
		}
	}

	public CompleteOldMiningSiteWithoutKeys()
	{
		Debug.LogError("[Achievement] outdated achievment");
	}
}
