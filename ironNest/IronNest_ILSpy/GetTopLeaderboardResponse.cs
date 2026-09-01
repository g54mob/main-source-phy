using System.Collections.Generic;
using Cpp2ILInjected;

public class GetTopLeaderboardResponse
{
	private List<LeaderboardEntryResponse> _003CEntries_003Ek__BackingField;

	public List<LeaderboardEntryResponse> Entries
	{
		get
		{
			return _003CEntries_003Ek__BackingField;
		}
		set
		{
			_003CEntries_003Ek__BackingField = value;
		}
	}

	public GetTopLeaderboardResponse()
	{
		List<LeaderboardEntryResponse> list = new List<LeaderboardEntryResponse>();
		_003CEntries_003Ek__BackingField = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
