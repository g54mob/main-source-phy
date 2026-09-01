namespace GameGrind
{
	public class AchievementController
	{
		public static int CurrentAchievementScore { get; set; }

		public static void CheckForCompletion(Achievement achievement)
		{
			if (achievement.value >= achievement.neededValue)
			{
				if (!achievement.completed)
				{
					GrantWithScore(achievement);
				}
				else
				{
					AchievementEvents.AchievementValueChanged(achievement);
				}
			}
			else
			{
				achievement.completed = false;
			}
		}

		public static void GrantWithScore(Achievement achievement)
		{
			CurrentAchievementScore += achievement.points;
			Grant(achievement);
		}

		public static void Grant(Achievement achievement)
		{
			achievement.value = achievement.neededValue;
			achievement.completed = true;
			AchievementEvents.AchievementValueChanged(achievement);
			AchievementEvents.AchievementGranted(achievement);
		}

		public static void Revoke(Achievement achievement, bool resetValue)
		{
			achievement.completed = false;
			if (resetValue)
			{
				achievement.value = 0;
			}
			CurrentAchievementScore -= achievement.points;
		}
	}
}
