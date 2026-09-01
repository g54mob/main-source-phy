using Cpp2ILInjected;

namespace SleepyNodes;

public class State_PauseTimer : StateNode
{
	public StateNode To;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public override void OnEnter(NodeExecutionState state)
	{
		base.OnEnter(state);
		if (CounterBatteryTimer._003CInstance_003Ek__BackingField != null)
		{
			CounterBatteryTimer._003CInstance_003Ek__BackingField.PauseTimer();
		}
	}

	public override void OnExecute(NodeExecutionState state)
	{
		//IL_0038: Expected I, but got O
		//IL_0048: Expected O, but got I
		//IL_0058: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7B8]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ r9_v1 (Il2CppClass<SleepyNodes.State_PauseTimer>)+218]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ r9_v1 (Il2CppClass<SleepyNodes.State_PauseTimer>)+220]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v34 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
