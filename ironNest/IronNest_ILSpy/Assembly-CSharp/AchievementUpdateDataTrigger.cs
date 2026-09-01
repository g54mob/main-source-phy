using Cpp2ILInjected;
using UnityEngine;

public class AchievementUpdateDataTrigger : MonoBehaviour
{
	private UserStat userStat;

	private int progressDifference;

	public unsafe void UpdateData()
	{
		AchievementsManager instance = AchievementsManager.Instance;
		int num = default(int);
		AchievementUpdateStatEvent achievementUpdateStatEvent = new AchievementUpdateStatEvent((UserStat)0, num);
		achievementUpdateStatEvent._003CUserStat_003Ek__BackingField = this.userStat;
		achievementUpdateStatEvent._003CProgressDifference_003Ek__BackingField = progressDifference;
		AchievementsService achievementsService = instance.achievementsService;
		if (achievementsService.isInitialized)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
			object obj = default(object);
			UserStat userStat = default(UserStat);
			achievementsService.statsChangesCache.set_Item((UserStat)(int)(&obj), (int)(&userStat));
		}
	}
}
