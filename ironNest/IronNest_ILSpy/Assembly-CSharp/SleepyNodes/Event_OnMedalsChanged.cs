using System;
using Cpp2ILInjected;

namespace SleepyNodes;

public class Event_OnMedalsChanged : EventNode
{
	public bool FilterByMedalID;

	public string MedalID;

	public bool FilterByChange;

	public EventData_MedalsChanged.Changes Change;

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
		//IL_00cf: Expected O, but got I
		if (data != null)
		{
			nint num = (nint)typeof(EventData_MedalsChanged);
			nint num2 = (nint)data;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<EventData_MedalsChanged>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<EventData_MedalsChanged>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v5+FFFFFFF8+v42 @ rax_v4*8]");
				if (0 == (nint)typeof(EventData_MedalsChanged))
				{
					if (FilterByMedalID)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+10]");
						if (!string.Equals((string)0, MedalID, StringComparison.OrdinalIgnoreCase))
						{
							goto IL_013a;
						}
					}
					if (FilterByChange)
					{
						EventData_MedalsChanged.Changes change = Change;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+18]");
						if ((nint)change != (nint)0)
						{
							goto IL_013a;
						}
					}
					return true;
				}
			}
		}
		goto IL_013a;
		IL_013a:
		return false;
	}

	public Event_OnMedalsChanged()
	{
		EnableOnStart = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
