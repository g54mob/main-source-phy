using Steamworks;

namespace Heathen.SteamworksIntegration
{
	public struct LeaderboardUgcSet
	{
		public LeaderboardUGCSet_t Data;

		public EResult Result => default(EResult);

		public LeaderboardData Leaderboard => default(LeaderboardData);

		public static implicit operator LeaderboardUgcSet(LeaderboardUGCSet_t native)
		{
			return default(LeaderboardUgcSet);
		}

		public static implicit operator LeaderboardUGCSet_t(LeaderboardUgcSet heathen)
		{
			return default(LeaderboardUGCSet_t);
		}
	}
}
