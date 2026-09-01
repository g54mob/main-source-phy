using Steamworks;

namespace Heathen.SteamworksIntegration
{
	public struct RankChange
	{
		public string LeaderboardName;

		public SteamLeaderboard_t LeaderboardId;

		public LeaderboardEntry OldEntry;

		public LeaderboardEntry NewEntry;

		public int RankDelta => 0;

		public int ScoreDeta => 0;
	}
}
