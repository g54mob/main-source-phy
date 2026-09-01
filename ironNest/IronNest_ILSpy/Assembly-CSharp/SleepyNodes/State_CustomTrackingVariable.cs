using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_CustomTrackingVariable : StateNode
{
	public enum Operations
	{
		Set,
		Add,
		Subtract
	}

	public enum Sources
	{
		Inline,
		CurrentTime,
		FilterCount
	}

	public StateNode To;

	public string TrackingVariable;

	public Operations Operation;

	public Sources Source;

	public float Value;

	public TargetSelection Filter;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0043: Expected O, but got I4
		//IL_00e5: Expected O, but got I4
		//IL_0187: Expected I, but got O
		//IL_0197: Expected O, but got I
		//IL_01a7: Expected O, but got I
		//IL_0077: Expected F4, but got I4
		//IL_00ab: Expected F4, but got I4
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected F4, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A78C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		while (true)
		{
			base.OnEnter(state);
			bool flag = Source == Sources.Inline;
			float num;
			if (!flag)
			{
				object obj = Source - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						num = 0f;
					}
					else
					{
						List<MapEntity> list = Filter.Resolve(FireMission._003CInstance_003Ek__BackingField, state);
						num = list._size;
					}
				}
				else
				{
					float time = Time.time;
					num = time;
				}
			}
			else
			{
				num = Value;
			}
			bool flag2 = Operation == Operations.Set;
			if (!flag2)
			{
				object obj2 = Operation - 1;
				if (!flag2)
				{
					if ((nint)obj2 == 1)
					{
						float num2 = num;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
						float value = num2 ^ 0;
						MissionManager._003CInstance_003Ek__BackingField.ModifyCustomTrackingValue(TrackingVariable, value);
					}
				}
				else
				{
					MissionManager._003CInstance_003Ek__BackingField.ModifyCustomTrackingValue(TrackingVariable, num);
				}
			}
			else
			{
				MissionManager._003CInstance_003Ek__BackingField.SetCustomTrackingValue(TrackingVariable, num);
			}
			nint num3 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ r9_v3 (Il2CppClass<SleepyNodes.State_CustomTrackingVariable>)+218]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ r9_v3 (Il2CppClass<SleepyNodes.State_CustomTrackingVariable>)+220]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v294 @ rax_v6 (should have been resolved before IL gen)");
		}
	}

	public float ResolveValue(NodeExecutionState state)
	{
		//IL_0015: Expected O, but got I4
		//IL_0046: Expected F4, but got I4
		//IL_0072: Expected F4, but got I4
		bool flag = Source == Sources.Inline;
		if (!flag)
		{
			object obj = Source - 1;
			if (!flag)
			{
				if ((nint)obj == 1)
				{
					List<MapEntity> list = Filter.Resolve(FireMission._003CInstance_003Ek__BackingField, state);
					return list._size;
				}
				return 0f;
			}
			return Time.time;
		}
		return Value;
	}
}
