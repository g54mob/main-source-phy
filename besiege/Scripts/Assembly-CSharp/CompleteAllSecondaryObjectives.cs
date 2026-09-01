using UnityEngine;

[AddComponentMenu("Achievements/Trigger/Generic/CompleteAllSecondaryObjectives")]
internal class CompleteAllSecondaryObjectives : AchievementTrigger
{
	internal override int AchievementId
	{
		get
		{
			return 51;
		}
	}

	public void TriggerAchievement()
	{
		Trigger();
	}

	public void SetValueAchievement(int newValue)
	{
		SetValue(newValue);
	}
}
