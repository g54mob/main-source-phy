using Cpp2ILInjected;
using UnityEngine;

public class AchievementSetDataTrigger : MonoBehaviour
{
	private UserStat userStat;

	private int progress;

	public unsafe void SetDataAchievement()
	{
		AchievementsManager instance = AchievementsManager.Instance;
		int num = default(int);
		AchievementSetStatEvent achievementSetStatEvent = new AchievementSetStatEvent((UserStat)0, num);
		achievementSetStatEvent._003CUserStat_003Ek__BackingField = userStat;
		achievementSetStatEvent._003CProgress_003Ek__BackingField = progress;
		bool flag = (object)AchievementsManager.Instance == null;
		AchievementSetStatEvent achievementSetStatEvent2 = achievementSetStatEvent;
		if (!flag)
		{
			AchievementsService achievementsService = instance.achievementsService;
			bool flag2 = (object)instance.achievementsService == null;
			achievementSetStatEvent2 = achievementSetStatEvent;
			if (!flag2)
			{
				if (!achievementsService.isInitialized)
				{
					return;
				}
				bool flag3 = achievementsService.statsChangesCache == null;
				achievementSetStatEvent2 = (AchievementSetStatEvent)(object)achievementsService.statsChangesCache;
				if (!flag3)
				{
					object obj = default(object);
					object obj2 = default(object);
					achievementsService.statsChangesCache.set_Item((UserStat)(int)(&obj), (int)(&obj2));
					return;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7620");
	}
}
