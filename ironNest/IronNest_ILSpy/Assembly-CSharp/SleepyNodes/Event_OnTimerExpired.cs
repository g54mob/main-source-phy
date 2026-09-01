using System;
using Cpp2ILInjected;

namespace SleepyNodes;

public class Event_OnTimerExpired : EventNode
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
		//IL_010b: Expected I4, but got O
		//IL_00d4: Expected O, but got I
		if (data != null)
		{
			nint num = (nint)typeof(EventData_Notification);
			nint num2 = (nint)data;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rdx_v2 (Il2CppClass<SleepyNodes.EventData_Notification>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rdx_v2 (Il2CppClass<SleepyNodes.EventData_Notification>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rax_v5+FFFFFFF8+v41 @ rax_v4*8]");
				if (0 == (nint)typeof(EventData_Notification))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
						bool flag = ((string)0).Equals("TimerExpired", StringComparison.OrdinalIgnoreCase);
						bool flag2 = !flag;
						return !flag2;
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
			}
		}
		return false;
	}

	public Event_OnTimerExpired()
	{
		EnableOnStart = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
