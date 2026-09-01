namespace TFBGames
{
	public interface ITabsAchievements
	{
		void UnlockAchievement(string id);

		void AdvanceAchievementProgress(string id, int progressAmount);
	}
}
