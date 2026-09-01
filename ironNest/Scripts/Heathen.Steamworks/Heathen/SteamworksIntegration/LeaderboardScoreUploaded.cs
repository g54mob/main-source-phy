using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LeaderboardScoreUploaded
	{
		public LeaderboardScoreUploaded_t Data;

		public readonly bool Success => false;

		public readonly bool ScoreChanged => false;

		public readonly LeaderboardData Leaderboard => default(LeaderboardData);

		public readonly int Score => 0;

		public readonly int GlobalRankNew => 0;

		public readonly int GlobalRankPrevious => 0;

		public static implicit operator LeaderboardScoreUploaded(LeaderboardScoreUploaded_t native)
		{
			return default(LeaderboardScoreUploaded);
		}

		public static implicit operator LeaderboardScoreUploaded_t(LeaderboardScoreUploaded heathen)
		{
			return default(LeaderboardScoreUploaded_t);
		}
	}
}
