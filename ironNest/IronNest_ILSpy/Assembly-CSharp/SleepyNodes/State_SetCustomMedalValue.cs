using Cpp2ILInjected;

namespace SleepyNodes;

public class State_SetCustomMedalValue : StateNode
{
	public StateNode To;

	public string Key;

	public float Value;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0064: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A790]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		while (true)
		{
			base.OnEnter(state);
			MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
			MissionManager.MissionState currentMissionState = missionManager.CurrentMissionState;
			currentMissionState.TrackingValues.SetCustomValue(Key, Value);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v125 @ r9_v2 (Il2CppClass<SleepyNodes.State_SetCustomMedalValue>)+218] (should have been resolved before IL gen)");
		}
	}
}
