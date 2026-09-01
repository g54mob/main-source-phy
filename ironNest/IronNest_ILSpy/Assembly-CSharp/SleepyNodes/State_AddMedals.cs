using Cpp2ILInjected;

namespace SleepyNodes;

public class State_AddMedals : StateNode
{
	public StateNode To;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0042: Expected I, but got O
		//IL_0052: Expected O, but got I
		//IL_0062: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A78B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		base.OnEnter(state);
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ r9_v1 (Il2CppClass<SleepyNodes.State_AddMedals>)+218]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ r9_v1 (Il2CppClass<SleepyNodes.State_AddMedals>)+220]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v37 @ rax_v3 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
