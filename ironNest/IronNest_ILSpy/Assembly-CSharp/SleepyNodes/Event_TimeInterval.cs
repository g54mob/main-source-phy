using System;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class Event_TimeInterval : EventNode
{
	public bool TriggerOnStart;

	public float MinSeconds = 60f;

	public float MaxSeconds = 60f;

	private float NextTrigger = -1f;

	public override void ResetNode()
	{
		base.AlreadyTriggered = false;
		NextTrigger = -1f;
	}

	protected override bool ShouldRun(EventData data)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_010f: Invalid comparison between F4 and I
		if (data != null)
		{
			nint num = (nint)typeof(EventData_Timer);
			nint num2 = (nint)data;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v2 (Il2CppClass<SleepyNodes.EventData_Timer>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v2 (Il2CppClass<SleepyNodes.EventData_Timer>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v5+FFFFFFF8+v44 @ rax_v4*8]");
				if (0 == (nint)typeof(EventData_Timer))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804CEE2Dh\"");
					if (NextTrigger == -1f)
					{
						float num4 = UnityEngine.Random.Range(MinSeconds, MaxSeconds);
						float num5 = num4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
						float nextTrigger = num5 + 0f;
						NextTrigger = nextTrigger;
						return TriggerOnStart;
					}
					float nextTrigger2 = NextTrigger;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
					if (nextTrigger2 < 0f)
					{
						float num6 = UnityEngine.Random.Range(MinSeconds, MaxSeconds);
						float num7 = num6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
						float nextTrigger3 = num7 + 0f;
						NextTrigger = nextTrigger3;
						return true;
					}
				}
			}
		}
		return false;
	}

	public Event_TimeInterval()
	{
		EnableOnStart = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
