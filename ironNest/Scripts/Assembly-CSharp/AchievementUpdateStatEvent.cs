public class AchievementUpdateStatEvent
{
	public UserStat UserStat { get; set; }

	public int ProgressDifference { get; set; }

	public AchievementUpdateStatEvent(UserStat userStat, int progressDifference)
	{
	}
}
