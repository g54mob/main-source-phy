using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_AddPunchcard : StateNode
{
	public StateNode To;

	public PunchcardDefinitionV2 Punchcard;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_00a2: Expected I, but got O
		//IL_00b2: Expected O, but got I
		//IL_00c2: Expected O, but got I
		while (true)
		{
			base.OnEnter(state);
			RequisitionConsoleManager requisitionConsoleManager = Object.FindFirstObjectByType<RequisitionConsoleManager>();
			if (requisitionConsoleManager != null && Punchcard != null)
			{
				List<PunchcardDefinitionV2> list = new List<PunchcardDefinitionV2>();
				list.Add(Punchcard);
				requisitionConsoleManager.EnsureCards(list);
			}
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ r9_v1 (Il2CppClass<SleepyNodes.State_AddPunchcard>)+218]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ r9_v1 (Il2CppClass<SleepyNodes.State_AddPunchcard>)+220]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v141 @ rax_v7 (should have been resolved before IL gen)");
		}
	}
}
