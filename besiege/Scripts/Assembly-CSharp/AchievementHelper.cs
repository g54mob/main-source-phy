using GameGrind;
using UnityEngine;

[AddComponentMenu("Achievements/AchievementHelper")]
public static class AchievementHelper
{
	public static void Increment(int achievementId)
	{
		Increment(achievementId, 1);
	}

	public static void Increment(int achievementId, int value)
	{
		if (!StatMaster.GodTools.HasBeenUsed)
		{
			Journal.Increment(achievementId, value);
		}
	}

	public static void SetValue(int achievementId, int value)
	{
		if (!StatMaster.GodTools.HasBeenUsed)
		{
			Journal.SetValue(achievementId, value);
		}
	}

	public static bool Completed(int achievementId)
	{
		return Journal.GetAchievement(achievementId).completed;
	}
}
