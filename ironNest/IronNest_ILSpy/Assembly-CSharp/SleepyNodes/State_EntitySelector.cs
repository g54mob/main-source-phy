using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace SleepyNodes;

public class State_EntitySelector : StateNode
{
	public StateNode To;

	public TargetSelection EntitySelection;

	public EntityContextKeys ContextKey;

	public override void ResetNode()
	{
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0137: Expected I, but got O
		//IL_0147: Expected O, but got I
		//IL_0157: Expected O, but got I
		//IL_004d: Expected O, but got I4
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected I4, but got Unknown
		//IL_00d8: Expected O, but got I4
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		List<MapEntity> list2 = default(List<MapEntity>);
		while (true)
		{
			base.OnEnter(state);
			List<MapEntity> list = EntitySelection.Resolve(FireMission._003CInstance_003Ek__BackingField, state);
			if (list != null)
			{
				object obj = list._size - 1;
				int num = list._size ^ 1;
				int num2 = list._size ^ obj;
				int num3 = num & num2;
				bool flag = num3 < 0;
				bool flag2 = (nint)obj < 0;
				bool flag3 = list._size == 1;
				EntityContextKeys contextKey;
				List<MapEntity> value;
				if (!flag3)
				{
					bool flag4 = flag2 == flag;
					object obj2 = !flag4;
					object obj3 = obj2 | flag3;
					if (obj3 != null)
					{
						goto IL_0132;
					}
					contextKey = ContextKey;
					value = list;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					contextKey = ContextKey;
					value = list2;
				}
				state.Set(contextKey, (MapEntity)(object)value);
			}
			goto IL_0132;
			IL_0132:
			nint num4 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r9_v4 (Il2CppClass<SleepyNodes.State_EntitySelector>)+218]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r9_v4 (Il2CppClass<SleepyNodes.State_EntitySelector>)+220]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v185 @ rax_v9 (should have been resolved before IL gen)");
		}
	}

	public State_EntitySelector()
	{
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
