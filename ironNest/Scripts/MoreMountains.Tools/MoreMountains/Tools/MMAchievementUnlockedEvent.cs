namespace MoreMountains.Tools
{
	public struct MMAchievementUnlockedEvent
	{
		public MMAchievement Achievement;

		private static MMAchievementUnlockedEvent e;

		public MMAchievementUnlockedEvent(MMAchievement newAchievement)
		{
			Achievement = null;
		}

		public static void Trigger(MMAchievement newAchievement)
		{
		}
	}
}
