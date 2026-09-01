using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct UserAchievementStoredData
	{
		public GameData game;

		public bool groupAchievement;

		public string achievementName;

		public uint currentProgress;

		public uint maxProgress;

		public UserAchievementStoredData(GameData game, bool groupAchievement, string achievementName, uint currentProgress, uint maxProgress)
		{
			this.game = default(GameData);
			this.groupAchievement = false;
			this.achievementName = null;
			this.currentProgress = 0u;
			this.maxProgress = 0u;
		}

		public UserAchievementStoredData(UserAchievementStored_t data)
		{
			game = default(GameData);
			groupAchievement = false;
			achievementName = null;
			currentProgress = 0u;
			maxProgress = 0u;
		}
	}
}
