using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_AddRequisitionPoints : StateNode
{
	public enum Operations
	{
		Add,
		Spend,
		Set
	}

	public StateNode To;

	public Operations Operation;

	public ContextVariableOrInline_Int Amount;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_00c2: Expected I, but got O
		//IL_00d2: Expected O, but got I
		//IL_00e2: Expected O, but got I
		//IL_0069: Expected O, but got I4
		base.OnEnter(state);
		MissionStatsTracker missionStatsTracker = Object.FindFirstObjectByType<MissionStatsTracker>();
		int amount = default(int);
		if (missionStatsTracker != null)
		{
			bool flag = Operation == Operations.Add;
			if (!flag)
			{
				object obj = Operation - 1;
				if (flag)
				{
					goto IL_00ec;
				}
				if ((nint)obj == 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
					missionStatsTracker.SetRequisitionPoints(amount, inital: false);
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
				missionStatsTracker.AddRequisitionPoints(amount);
			}
		}
		goto IL_00bd;
		IL_00bd:
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r9_v2 (Il2CppClass<SleepyNodes.State_AddRequisitionPoints>)+218]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r9_v2 (Il2CppClass<SleepyNodes.State_AddRequisitionPoints>)+220]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v166 @ rax_v7 (should have been resolved before IL gen)");
		goto IL_00ec;
		IL_00ec:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
		bool flag2 = missionStatsTracker.SpendPoints(amount);
		goto IL_00bd;
	}
}
