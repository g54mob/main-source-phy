using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_StopTimer : StateNode
{
	public StateNode To;

	public bool RemoveFromScene;

	public float RemoveFromSceneDelay = 1f;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_00a0: Expected I, but got O
		base.OnEnter(state);
		if (CounterBatteryTimer._003CInstance_003Ek__BackingField != null)
		{
			CounterBatteryTimer._003CInstance_003Ek__BackingField.PermanentlyStop();
			if (RemoveFromScene)
			{
				GameObject gameObject = CounterBatteryTimer._003CInstance_003Ek__BackingField.gameObject;
				Object.Destroy(gameObject, RemoveFromSceneDelay);
			}
		}
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v148 @ r9_v1 (Il2CppClass<SleepyNodes.State_StopTimer>)+218] (should have been resolved before IL gen)");
		Cpp2ILHelpers.NoteDecompilerIssue("Warning: Method ends with non empty stack (-38), the output could be wrong!");
		/*Error: End of method reached without returning.*/;
	}
}
