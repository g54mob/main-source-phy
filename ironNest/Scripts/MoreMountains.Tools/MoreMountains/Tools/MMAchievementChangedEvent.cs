namespace MoreMountains.Tools
{
	public struct MMAchievementChangedEvent
	{
		public MMAchievement Achievement;

		private static MMAchievementChangedEvent e;

		public MMAchievementChangedEvent(MMAchievement newAchievement)
		{
			Achievement = null;
		}

		public static void Trigger(MMAchievement newAchievement)
		{
		}
	}
}
