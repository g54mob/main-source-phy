using System;

public class GetSessionKeyRequest
{
	private Guid _003CUserId_003Ek__BackingField;

	private Gamemodes _003CGamemode_003Ek__BackingField;

	private string _003CPerformanceStatsJson_003Ek__BackingField;

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

	public string PerformanceStatsJson
	{
		get
		{
			return _003CPerformanceStatsJson_003Ek__BackingField;
		}
		set
		{
			_003CPerformanceStatsJson_003Ek__BackingField = value;
		}
	}
}
