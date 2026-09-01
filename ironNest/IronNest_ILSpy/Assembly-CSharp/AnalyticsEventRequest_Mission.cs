using System;
using Cpp2ILInjected;

public class AnalyticsEventRequest_Mission
{
	private string _003CEventType_003Ek__BackingField;

	private string _003CDeviceId_003Ek__BackingField;

	private Guid _003CUserId_003Ek__BackingField;

	private string _003CMissionId_003Ek__BackingField;

	private double _003CValue_003Ek__BackingField;

	private string _003CPayload_003Ek__BackingField;

	private DateTime _003CCreatedAtUtc_003Ek__BackingField;

	public string EventType
	{
		get
		{
			return _003CEventType_003Ek__BackingField;
		}
		set
		{
			_003CEventType_003Ek__BackingField = value;
		}
	}

	public string DeviceId
	{
		get
		{
			return _003CDeviceId_003Ek__BackingField;
		}
		set
		{
			_003CDeviceId_003Ek__BackingField = value;
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

	public string MissionId
	{
		get
		{
			return _003CMissionId_003Ek__BackingField;
		}
		set
		{
			_003CMissionId_003Ek__BackingField = value;
		}
	}

	public double Value
	{
		get
		{
			return _003CValue_003Ek__BackingField;
		}
		set
		{
			_003CValue_003Ek__BackingField = value;
		}
	}

	public string Payload
	{
		get
		{
			return _003CPayload_003Ek__BackingField;
		}
		set
		{
			_003CPayload_003Ek__BackingField = value;
		}
	}

	public DateTime CreatedAtUtc
	{
		get
		{
			return _003CCreatedAtUtc_003Ek__BackingField;
		}
		set
		{
			_003CCreatedAtUtc_003Ek__BackingField = value;
		}
	}

	public AnalyticsEventRequest_Mission()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F5D]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_003CEventType_003Ek__BackingField = "";
		_003CDeviceId_003Ek__BackingField = "";
		_003CMissionId_003Ek__BackingField = "";
		_003CPayload_003Ek__BackingField = "{}";
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
