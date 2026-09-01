using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_MissionFailed : StateNode
{
	public StateNode To;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0043: Expected O, but got I4
		//IL_004e: Expected O, but got I4
		//IL_00b3: Expected I, but got O
		//IL_00c3: Expected O, but got I
		//IL_00d3: Expected O, but got I
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		while (true)
		{
			base.OnEnter(state);
			MissionManager._003CInstance_003Ek__BackingField.MarkMissionFailed();
			MissionGraphNotificationListener[] array = Object.FindObjectsByType<MissionGraphNotificationListener>(FindObjectsInactive.Include, FindObjectsSortMode.None);
			object obj = array + 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < array.Length)
			{
				((MissionGraphNotificationListener)obj)?.Trigger("MissionFailed");
				obj2++;
				obj += 8;
				obj3 = obj2;
			}
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r9_v1 (Il2CppClass<SleepyNodes.State_MissionFailed>)+218]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r9_v1 (Il2CppClass<SleepyNodes.State_MissionFailed>)+220]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v178 @ rax_v14 (should have been resolved before IL gen)");
		}
	}
}
