using System;

public class GenerateDiscordLinkCodeResponse
{
	private bool _003CSuccess_003Ek__BackingField;

	private string _003CKey_003Ek__BackingField;

	private DateTime _003CExpiresAtUtc_003Ek__BackingField;

	public bool Success
	{
		get
		{
			return _003CSuccess_003Ek__BackingField;
		}
		set
		{
			_003CSuccess_003Ek__BackingField = value;
		}
	}

	public string Key
	{
		get
		{
			return _003CKey_003Ek__BackingField;
		}
		set
		{
			_003CKey_003Ek__BackingField = value;
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
