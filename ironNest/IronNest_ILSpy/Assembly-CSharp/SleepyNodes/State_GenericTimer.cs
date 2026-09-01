using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_GenericTimer : StateNode
{
	public StateNode To;

	public string TimerID;

	public float InitialSeconds;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0091: Expected I, but got O
		//IL_00a1: Expected O, but got I
		//IL_00b1: Expected O, but got I
		while (true)
		{
			base.OnEnter(state);
			FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
			FireMission.TimerValue timerValue = new FireMission.TimerValue();
			timerValue.InitialSeconds = InitialSeconds;
			timerValue.CurrentSeconds = InitialSeconds;
			double timeAsDouble = Time.timeAsDouble;
			timerValue.StartedAt = timeAsDouble;
			fireMission.RunningTimers.set_Item(TimerID, timerValue);
			EventData_GenericTimerStarted eventData_GenericTimerStarted = new EventData_GenericTimerStarted();
			eventData_GenericTimerStarted.TimerID = TimerID;
			FireMission._003CInstance_003Ek__BackingField.ProcessEvent(eventData_GenericTimerStarted);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r9_v3 (Il2CppClass<SleepyNodes.State_GenericTimer>)+218]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r9_v3 (Il2CppClass<SleepyNodes.State_GenericTimer>)+220]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v170 @ rax_v17 (should have been resolved before IL gen)");
		}
	}
}
