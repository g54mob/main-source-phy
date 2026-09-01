using Cpp2ILInjected;

namespace SleepyNodes;

public class State_Blocker : StateNode
{
	public StateNode To;

	public StateNode Block;

	public bool InvertBlocking;

	private bool blocked;

	public override void ResetNode()
	{
		blocked = InvertBlocking;
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_004f: Expected I, but got O
		//IL_008e: Expected O, but got I
		//IL_009e: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7C6]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		base.OnEnter(state);
		bool flag = state.lastFieldPort == "Block";
		nint num = (nint)this;
		if (!flag)
		{
			if (blocked != flag)
			{
				goto IL_00d7;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v4 (Il2CppClass<SleepyNodes.State_Blocker>)+218]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v4 (Il2CppClass<SleepyNodes.State_Blocker>)+220]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v99 @ rax_v9 (should have been resolved before IL gen)");
		}
		bool flag2 = !InvertBlocking;
		blocked = flag2;
		goto IL_00d7;
		IL_00d7:
		base.OnExit(state, null, null);
	}
}
