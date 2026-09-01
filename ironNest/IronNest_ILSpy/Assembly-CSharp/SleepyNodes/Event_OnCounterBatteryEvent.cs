using System;
using Cpp2ILInjected;

namespace SleepyNodes;

public class Event_OnCounterBatteryEvent : EventNode
{
	public EventData_CounterBatteryEvent.EventTypes ListenerType;

	public override void ResetNode()
	{
		base.AlreadyTriggered = false;
	}

	protected override bool ShouldRun(EventData data)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_00d3: Expected O, but got I
		if (data != null)
		{
			nint num = (nint)typeof(EventData_CounterBatteryEvent);
			nint num2 = (nint)data;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<EventData_CounterBatteryEvent>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<EventData_CounterBatteryEvent>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v5+FFFFFFF8+v42 @ rax_v4*8]");
				if (0 == (nint)typeof(EventData_CounterBatteryEvent))
				{
					if (ListenerType == EventData_CounterBatteryEvent.EventTypes.Any)
					{
						return true;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
					object obj3 = (nint)0 - (nint)ListenerType;
					return obj3 == null;
				}
			}
		}
		return false;
	}

	public Event_OnCounterBatteryEvent()
	{
		EnableOnStart = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
