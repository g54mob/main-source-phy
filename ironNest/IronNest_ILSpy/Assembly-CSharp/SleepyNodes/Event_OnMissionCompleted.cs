using System;
using Cpp2ILInjected;

namespace SleepyNodes;

public class Event_OnMissionCompleted : EventNode
{
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
		if (data != null)
		{
			nint num = (nint)typeof(EventData_MissionCompleted);
			nint num2 = (nint)data;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v2 (Il2CppClass<EventData_MissionCompleted>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v2 (Il2CppClass<EventData_MissionCompleted>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v5+FFFFFFF8+v39 @ rax_v4*8]");
				if (0 == (nint)typeof(EventData_MissionCompleted))
				{
					return true;
				}
			}
		}
		return false;
	}

	public Event_OnMissionCompleted()
	{
		EnableOnStart = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
