using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_AddShell : StateNode
{
	public StateNode To;

	public ShellDefinition Shell;

	public ContextVariableOrInline_ShellSlot Slot;

	public ContextVariableOrInline_ShellSource Source;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public override void OnEnter(NodeExecutionState state)
	{
		base.OnEnter(state);
		ShellSlotPool shellSlotPool = Object.FindFirstObjectByType<ShellSlotPool>();
		if (shellSlotPool != null && Shell != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
			object obj = default(object);
			ShellSlotPool.ShellSource source2;
			ShellSlotPool.ShellInsertionMode mode;
			if (obj == null)
			{
				ContextVariableOrInline_ShellSource source = Source;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ r9_v9 (SleepyNodes.ContextVariableOrInline_ShellSource)+14]");
				source2 = ShellSlotPool.ShellSource.Mission;
				mode = ShellSlotPool.ShellInsertionMode.LeftOnly;
			}
			else
			{
				if ((nint)obj != 1)
				{
					goto IL_00bf;
				}
				ContextVariableOrInline_ShellSource source3 = Source;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ r9_v7 (SleepyNodes.ContextVariableOrInline_ShellSource)+14]");
				source2 = ShellSlotPool.ShellSource.Mission;
				mode = ShellSlotPool.ShellInsertionMode.RightOnly;
			}
			ref CylinderShellSelector usedSelector = default(ref CylinderShellSelector);
			ref int slotIndex = default(ref int);
			bool flag = shellSlotPool.InsertShell(Shell, mode, source2, out usedSelector, out slotIndex);
		}
		goto IL_00bf;
		IL_00bf:
		base.OnExit(state, "To");
	}
}
