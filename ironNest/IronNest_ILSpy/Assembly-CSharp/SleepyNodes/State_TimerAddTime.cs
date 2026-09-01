using Cpp2ILInjected;

namespace SleepyNodes;

public class State_TimerAddTime : StateNode
{
	public enum ModifyTypes
	{
		Add,
		Set,
		Subtract,
		Reset
	}

	public StateNode To;

	public ModifyTypes ModifyType;

	public float Time;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_00cc: Expected I, but got O
		//IL_00dc: Expected O, but got I
		//IL_00ec: Expected O, but got I
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected F4, but got Unknown
		//IL_005c: Expected O, but got I4
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		base.OnEnter(state);
		float time;
		if (CounterBatteryTimer._003CInstance_003Ek__BackingField != null)
		{
			bool flag = ModifyType == ModifyTypes.Add;
			if (!flag)
			{
				object obj = ModifyType - 1;
				CounterBatteryTimer counterBatteryTimer2;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (flag)
					{
						goto IL_00fb;
					}
					if ((nint)obj2 != 1)
					{
						goto IL_00c7;
					}
					CounterBatteryTimer counterBatteryTimer = CounterBatteryTimer._003CInstance_003Ek__BackingField;
					time = counterBatteryTimer.totalDurationSeconds;
					counterBatteryTimer2 = CounterBatteryTimer._003CInstance_003Ek__BackingField;
				}
				else
				{
					counterBatteryTimer2 = CounterBatteryTimer._003CInstance_003Ek__BackingField;
					time = Time;
				}
				counterBatteryTimer2.SetTime(time);
			}
			else
			{
				CounterBatteryTimer._003CInstance_003Ek__BackingField.AddTime(Time);
			}
		}
		goto IL_00c7;
		IL_00c7:
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ r9_v1 (Il2CppClass<SleepyNodes.State_TimerAddTime>)+218]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ r9_v1 (Il2CppClass<SleepyNodes.State_TimerAddTime>)+220]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v175 @ rax_v8 (should have been resolved before IL gen)");
		goto IL_00fb;
		IL_00fb:
		float time2 = Time;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		time = time2 ^ 0;
		CounterBatteryTimer._003CInstance_003Ek__BackingField.AddTime(time);
		goto IL_00c7;
	}
}
