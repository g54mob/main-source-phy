using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_AddPowderCharge : StateNode
{
	public StateNode To;

	public ContextVariableOrInline_Int Amount;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_007d: Expected I, but got O
		//IL_008d: Expected O, but got I
		//IL_009d: Expected O, but got I
		int num = default(int);
		while (true)
		{
			base.OnEnter(state);
			PowderChargeInventory powderChargeInventory = Object.FindFirstObjectByType<PowderChargeInventory>();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
			if (powderChargeInventory != null && num >= 1)
			{
				powderChargeInventory.AddCharges(num);
			}
			nint num2 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ r9_v3 (Il2CppClass<SleepyNodes.State_AddPowderCharge>)+218]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ r9_v3 (Il2CppClass<SleepyNodes.State_AddPowderCharge>)+220]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v130 @ rax_v10 (should have been resolved before IL gen)");
		}
	}
}
