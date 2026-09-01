using System;

public class ClientCombinedLeaderboardRequest
{
	private Guid _003CUserId_003Ek__BackingField;

	private DateTime? _003CDayUtc_003Ek__BackingField;

	public unsafe Guid UserId
	{
		get
		{
			//IL_000f: Expected I4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Guid guid = default(Guid);
			((Guid*)(nint)guid)->_a = (int)_003CUserId_003Ek__BackingField;
			return guid;
		}
		set
		{
			//IL_000f: Expected O, but got I4
			_003CUserId_003Ek__BackingField = (Guid)value._a;
		}
	}

	public DateTime? DayUtc
	{
		get
		{
			//IL_0006: Expected O, but got I
			ClientCombinedLeaderboardRequest clientCombinedLeaderboardRequest = (ClientCombinedLeaderboardRequest)0;
			return (DateTime?)this;
		}
		set
		{
			_003CDayUtc_003Ek__BackingField = value;
		}
	}
}
