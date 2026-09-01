using UnityEngine;

[AddComponentMenu("Achievements/Trigger/Generic/CompleteAllSecondaryObjectivesSS")]
internal class CompleteAllSecondaryObjectivesSS : AchievementTrigger
{
	internal override int AchievementId
	{
		get
		{
			return 52;
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
