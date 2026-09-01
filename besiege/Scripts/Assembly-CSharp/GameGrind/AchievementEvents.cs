using UnityEngine;

namespace GameGrind
{
	public class AchievementEvents : MonoBehaviour
	{
		public delegate void AchievementValueChange(Achievement achievement);

		public delegate void AchievementGrant(Achievement achievement);

		public static event AchievementValueChange OnAchievementChange;

		public static event AchievementGrant OnAchievementGrant;

		public static void AchievementValueChanged(Achievement achievement)
		{
			if (AchievementEvents.OnAchievementChange != null)
			{
				AchievementEvents.OnAchievementChange(achievement);
			}
		}

		public static void AchievementGranted(Achievement achievement)
		{
			if (AchievementEvents.OnAchievementGrant != null)
			{
				AchievementEvents.OnAchievementGrant(achievement);
			}
		}
	}
}
