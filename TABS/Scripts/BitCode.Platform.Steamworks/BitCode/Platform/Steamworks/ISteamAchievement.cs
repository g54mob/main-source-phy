namespace BitCode.Platform.Steamworks
{
	public interface ISteamAchievement : IAchievement
	{
		string AchievementId { get; }

		bool ShowProgressOverlay { get; }

		int DisplayOverlayInterval { get; }

		uint MaxProgressValue { get; }
	}
}
