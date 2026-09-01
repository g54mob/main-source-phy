using System;
using Cpp2ILInjected;

namespace SleepyNodes;

public class Event_OnGenericTimerReachedTime : EventNode
{
	public string TimerID;

	public float TimeReached;

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
		//IL_013d: Expected I4, but got O
		//IL_00d5: Expected O, but got I
		//IL_0104: Invalid comparison between F4 and I
		if (data != null)
		{
			nint num = (nint)typeof(EventData_GenericTimerTimeUpdate);
			nint num2 = (nint)data;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<EventData_GenericTimerTimeUpdate>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<EventData_GenericTimerTimeUpdate>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rax_v5+FFFFFFF8+v42 @ rax_v4*8]");
				if (0 == (nint)typeof(EventData_GenericTimerTimeUpdate))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
					if ((nint)0 == 0)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
					if (((string)0).Equals(TimerID, StringComparison.OrdinalIgnoreCase))
					{
						float timeReached = TimeReached;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+18]");
						if (!(timeReached > 0f))
						{
							OnlyOnce = true;
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	public Event_OnGenericTimerReachedTime()
	{
		EnableOnStart = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
