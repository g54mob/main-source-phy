using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace SleepyNodes;

public class State_ConditionBranch : StateNode
{
	public StateNode To;

	public StateNode OnFail;

	public TargetSelection EntityFilter;

	public ConditionSet Conditions;

	public override void ResetNode()
	{
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_006c: Expected I, but got O
		//IL_007c: Expected O, but got I
		//IL_008c: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A761]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		while (true)
		{
			base.OnEnter(state);
			List<MapEntity> filteredEntities = EntityFilter.Resolve(FireMission._003CInstance_003Ek__BackingField, state);
			bool flag = Conditions.Resolve(state, filteredEntities);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v146 @ rcx_v7 (Il2CppClass<SleepyNodes.State_ConditionBranch>)+220]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v146 @ rcx_v7 (Il2CppClass<SleepyNodes.State_ConditionBranch>)+218]");
			object obj2 = 0;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v103 @ r10_v1 (should have been resolved before IL gen)");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v103 @ r10_v1 (should have been resolved before IL gen)");
		}
	}

	public State_ConditionBranch()
	{
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
