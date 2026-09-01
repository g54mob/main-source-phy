public class PostLeaderboardScoreResponse
{
	private bool _003CAccepted_003Ek__BackingField;

	private bool _003CCheated_003Ek__BackingField;

	private LeaderboardEntryResponse _003CEntry_003Ek__BackingField;

	public bool Accepted
	{
		get
		{
			return _003CAccepted_003Ek__BackingField;
		}
		set
		{
			_003CAccepted_003Ek__BackingField = value;
		}
	}

	public bool Cheated
	{
		get
		{
			return _003CCheated_003Ek__BackingField;
		}
		set
		{
			_003CCheated_003Ek__BackingField = value;
		}
	}

	public LeaderboardEntryResponse Entry
	{
		get
		{
			return _003CEntry_003Ek__BackingField;
		}
		set
		{
			_003CEntry_003Ek__BackingField = value;
		}
	}
}
