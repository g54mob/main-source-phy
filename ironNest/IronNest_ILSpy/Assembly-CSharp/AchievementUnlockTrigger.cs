using System;
using Steamworks;
using UnityEngine;

public class AchievementUnlockTrigger : MonoBehaviour
{
	private AchievementType achievementType;

	public unsafe void UnlockAchievement()
	{
		//IL_0046: Expected O, but got Ref
		AchievementsManager instance = AchievementsManager.Instance;
		AchievementUnlockEvent achievementUnlockEvent = new AchievementUnlockEvent(AchievementType.ACHIEVEMENT_1_CLEAR);
		achievementUnlockEvent._003CAchievementType_003Ek__BackingField = achievementType;
		AchievementsService achievementsService = instance.achievementsService;
		if (achievementsService.isInitialized && !achievementsService.HasUnlockedAchievement(achievementType))
		{
			object obj = default(object);
			string achievement = ((Enum)(&obj)).ToString();
			bool flag = SteamUserStats.SetAchievement(achievement);
			achievementsService.storeStatsRequested = true;
			achievementsService.updateStatsTimer = 0f;
		}
	}
}
