using System.Collections.Generic;
using Cpp2ILInjected;

public class ClientCombinedLeaderboardResponse
{
	private List<LeaderboardEntryResponse> _003CDailyChallengeLeaderboard_003Ek__BackingField;

	private List<LeaderboardEntryResponse> _003CDailyChillLeaderboard_003Ek__BackingField;

	private GetMyLeaderboardResponse _003CDailyChallengeSelf_003Ek__BackingField;

	private GetMyLeaderboardResponse _003CDailyChillSelf_003Ek__BackingField;

	private GetMyLeaderboardResponse _003CAllTimeChallengeSelfBest_003Ek__BackingField;

	private GetMyLeaderboardResponse _003CAllTimeChillSelfBest_003Ek__BackingField;

	public List<LeaderboardEntryResponse> DailyChallengeLeaderboard
	{
		get
		{
			return _003CDailyChallengeLeaderboard_003Ek__BackingField;
		}
		set
		{
			_003CDailyChallengeLeaderboard_003Ek__BackingField = value;
		}
	}

	public List<LeaderboardEntryResponse> DailyChillLeaderboard
	{
		get
		{
			return _003CDailyChillLeaderboard_003Ek__BackingField;
		}
		set
		{
			_003CDailyChillLeaderboard_003Ek__BackingField = value;
		}
	}

	public GetMyLeaderboardResponse DailyChallengeSelf
	{
		get
		{
			return _003CDailyChallengeSelf_003Ek__BackingField;
		}
		set
		{
			_003CDailyChallengeSelf_003Ek__BackingField = value;
		}
	}

	public GetMyLeaderboardResponse DailyChillSelf
	{
		get
		{
			return _003CDailyChillSelf_003Ek__BackingField;
		}
		set
		{
			_003CDailyChillSelf_003Ek__BackingField = value;
		}
	}

	public GetMyLeaderboardResponse AllTimeChallengeSelfBest
	{
		get
		{
			return _003CAllTimeChallengeSelfBest_003Ek__BackingField;
		}
		set
		{
			_003CAllTimeChallengeSelfBest_003Ek__BackingField = value;
		}
	}

	public GetMyLeaderboardResponse AllTimeChillSelfBest
	{
		get
		{
			return _003CAllTimeChillSelfBest_003Ek__BackingField;
		}
		set
		{
			_003CAllTimeChillSelfBest_003Ek__BackingField = value;
		}
	}

	public ClientCombinedLeaderboardResponse()
	{
		List<LeaderboardEntryResponse> list = new List<LeaderboardEntryResponse>();
		_003CDailyChallengeLeaderboard_003Ek__BackingField = list;
		List<LeaderboardEntryResponse> list2 = new List<LeaderboardEntryResponse>();
		_003CDailyChillLeaderboard_003Ek__BackingField = list2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
