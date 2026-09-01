using System;

public class GetSessionKeyResponse
{
	private Guid _003CSessionId_003Ek__BackingField;

	private DateTime _003CExpiresAtUtc_003Ek__BackingField;

	public unsafe Guid SessionId
	{
		get
		{
			//IL_000f: Expected I4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Guid guid = default(Guid);
			((Guid*)(nint)guid)->_a = (int)_003CSessionId_003Ek__BackingField;
			return guid;
		}
		set
		{
			//IL_000f: Expected O, but got I4
			_003CSessionId_003Ek__BackingField = (Guid)value._a;
		}
	}

	public DateTime ExpiresAtUtc
	{
		get
		{
			return _003CExpiresAtUtc_003Ek__BackingField;
		}
		set
		{
			_003CExpiresAtUtc_003Ek__BackingField = value;
		}
	}
}
