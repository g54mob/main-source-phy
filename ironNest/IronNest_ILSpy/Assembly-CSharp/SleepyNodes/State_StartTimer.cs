using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_StartTimer : StateNode
{
	public StateNode To;

	public float InitalTime;

	public CounterBatteryTimer Prefab_BatteryTimer;

	private CounterBatteryTimer spawnedTimer;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		if (spawnedTimer != null)
		{
			GameObject gameObject = spawnedTimer.gameObject;
			Object.Destroy(gameObject);
			spawnedTimer = null;
		}
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_00b7: Expected I, but got O
		while (true)
		{
			base.OnEnter(state);
			if (Prefab_BatteryTimer == null)
			{
				CounterBatteryTimer prefab_BatteryTimer = Resources.Load<CounterBatteryTimer>("CounterBatteryTimer");
				Prefab_BatteryTimer = prefab_BatteryTimer;
			}
			if (Prefab_BatteryTimer != null)
			{
				CounterBatteryTimer counterBatteryTimer = Object.Instantiate(Prefab_BatteryTimer);
				spawnedTimer = counterBatteryTimer;
				spawnedTimer.Init(InitalTime);
			}
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v156 @ r9_v1 (Il2CppClass<SleepyNodes.State_StartTimer>)+218] (should have been resolved before IL gen)");
		}
	}
}
