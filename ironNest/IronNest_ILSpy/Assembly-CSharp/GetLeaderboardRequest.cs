using System;
using Cpp2ILInjected;

public class GetLeaderboardRequest
{
	private int _003CAmount_003Ek__BackingField;

	private Guid _003CUserId_003Ek__BackingField;

	private Gamemodes _003CGamemode_003Ek__BackingField;

	private LeaderboardPeriod _003CPeriod_003Ek__BackingField;

	private DateTime? _003CDayUtc_003Ek__BackingField;

	public int Amount
	{
		get
		{
			return _003CAmount_003Ek__BackingField;
		}
		set
		{
			_003CAmount_003Ek__BackingField = value;
		}
	}

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

	public Gamemodes Gamemode
	{
		get
		{
			return _003CGamemode_003Ek__BackingField;
		}
		set
		{
			_003CGamemode_003Ek__BackingField = value;
		}
	}

	public LeaderboardPeriod Period
	{
		get
		{
			return _003CPeriod_003Ek__BackingField;
		}
		set
		{
			_003CPeriod_003Ek__BackingField = value;
		}
	}

	public DateTime? DayUtc
	{
		get
		{
			//IL_0010: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+30]");
			GetLeaderboardRequest getLeaderboardRequest = (GetLeaderboardRequest)0;
			return (DateTime?)this;
		}
		set
		{
			_003CDayUtc_003Ek__BackingField = value;
		}
	}
}
