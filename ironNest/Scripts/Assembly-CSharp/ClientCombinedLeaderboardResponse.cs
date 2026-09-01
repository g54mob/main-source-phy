using System.Collections.Generic;

public class ClientCombinedLeaderboardResponse
{
	public List<LeaderboardEntryResponse> DailyChallengeLeaderboard { get; set; }

	public List<LeaderboardEntryResponse> DailyChillLeaderboard { get; set; }

	public GetMyLeaderboardResponse DailyChallengeSelf { get; set; }

	public GetMyLeaderboardResponse DailyChillSelf { get; set; }

	public GetMyLeaderboardResponse AllTimeChallengeSelfBest { get; set; }

	public GetMyLeaderboardResponse AllTimeChillSelfBest { get; set; }
}
