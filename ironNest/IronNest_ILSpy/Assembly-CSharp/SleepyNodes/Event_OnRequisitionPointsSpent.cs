using System;
using Cpp2ILInjected;

namespace SleepyNodes;

public class Event_OnRequisitionPointsSpent : EventNode
{
	public string ContextKey;

	private EventData_RequisitionPointsSpent lastTriggered;

	public override void ResetNode()
	{
		lastTriggered = null;
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
			nint num = (nint)typeof(EventData_RequisitionPointsSpent);
			nint num2 = (nint)data;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<SleepyNodes.EventData_RequisitionPointsSpent>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<SleepyNodes.EventData_RequisitionPointsSpent>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v5+FFFFFFF8+v42 @ rax_v4*8]");
				if (0 == (nint)typeof(EventData_RequisitionPointsSpent))
				{
					lastTriggered = (EventData_RequisitionPointsSpent)data;
					return true;
				}
			}
		}
		return false;
	}

	public unsafe override void Run(NodeExecutionState state)
	{
		if (lastTriggered != null)
		{
			object obj = default(object);
			state.Set(ContextKey, (int)(&obj));
			lastTriggered = null;
		}
		base.Run(state);
	}

	public Event_OnRequisitionPointsSpent()
	{
		EnableOnStart = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
