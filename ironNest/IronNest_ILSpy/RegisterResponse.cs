using System;

public class RegisterResponse
{
	private Guid _003CUserId_003Ek__BackingField;

	private string _003CUsername_003Ek__BackingField;

	private bool _003CDiscordLinked_003Ek__BackingField;

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

	public string Username
	{
		get
		{
			return _003CUsername_003Ek__BackingField;
		}
		set
		{
			_003CUsername_003Ek__BackingField = value;
		}
	}

	public bool DiscordLinked
	{
		get
		{
			return _003CDiscordLinked_003Ek__BackingField;
		}
		set
		{
			_003CDiscordLinked_003Ek__BackingField = value;
		}
	}
}
