using System;
using Cpp2ILInjected;

public class PostLeaderboardScoreRequest
{
	private Guid _003CUserId_003Ek__BackingField;

	private Guid _003CSessionId_003Ek__BackingField;

	private bool _003CClientTampered_003Ek__BackingField;

	private LeaderboardRunData _003CRunData_003Ek__BackingField;

	private string _003CImageExtension_003Ek__BackingField;

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

	public bool ClientTampered
	{
		get
		{
			return _003CClientTampered_003Ek__BackingField;
		}
		set
		{
			_003CClientTampered_003Ek__BackingField = value;
		}
	}

	public LeaderboardRunData RunData
	{
		get
		{
			return _003CRunData_003Ek__BackingField;
		}
		set
		{
			_003CRunData_003Ek__BackingField = value;
		}
	}

	public string ImageExtension
	{
		get
		{
			return _003CImageExtension_003Ek__BackingField;
		}
		set
		{
			_003CImageExtension_003Ek__BackingField = value;
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

	public PostLeaderboardScoreRequest()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC77]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_003CImageExtension_003Ek__BackingField = "jpg";
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
