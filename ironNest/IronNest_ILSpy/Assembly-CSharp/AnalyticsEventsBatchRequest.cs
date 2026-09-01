using System.Collections.Generic;
using Cpp2ILInjected;

public class AnalyticsEventsBatchRequest
{
	private List<AnalyticsEventRequest_Boot> _003CBootEvents_003Ek__BackingField;

	private List<AnalyticsEventRequest_Mission> _003CMissionEvents_003Ek__BackingField;

	private List<AnalyticsEventRequest_Generic> _003CGenericEvents_003Ek__BackingField;

	public List<AnalyticsEventRequest_Boot> BootEvents
	{
		get
		{
			return _003CBootEvents_003Ek__BackingField;
		}
		set
		{
			_003CBootEvents_003Ek__BackingField = value;
		}
	}

	public List<AnalyticsEventRequest_Mission> MissionEvents
	{
		get
		{
			return _003CMissionEvents_003Ek__BackingField;
		}
		set
		{
			_003CMissionEvents_003Ek__BackingField = value;
		}
	}

	public List<AnalyticsEventRequest_Generic> GenericEvents
	{
		get
		{
			return _003CGenericEvents_003Ek__BackingField;
		}
		set
		{
			_003CGenericEvents_003Ek__BackingField = value;
		}
	}

	public AnalyticsEventsBatchRequest()
	{
		List<AnalyticsEventRequest_Boot> list = new List<AnalyticsEventRequest_Boot>();
		_003CBootEvents_003Ek__BackingField = list;
		List<AnalyticsEventRequest_Mission> list2 = new List<AnalyticsEventRequest_Mission>();
		_003CMissionEvents_003Ek__BackingField = list2;
		List<AnalyticsEventRequest_Generic> list3 = new List<AnalyticsEventRequest_Generic>();
		_003CGenericEvents_003Ek__BackingField = list3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
