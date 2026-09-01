using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_UnlockSceneObject : StateNode
{
	public StateNode To;

	public string ObjectID;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_00cd: Expected I, but got O
		//IL_00dd: Expected O, but got I
		//IL_00ed: Expected O, but got I
		//IL_008f: Expected I, but got O
		//IL_009f: Expected O, but got I
		//IL_00af: Expected O, but got I
		while (true)
		{
			base.OnEnter(state);
			if (!string.IsNullOrEmpty(ObjectID))
			{
				if (ProgressionManager._003CInstance_003Ek__BackingField != null)
				{
					bool flag = ProgressionManager._003CInstance_003Ek__BackingField.UnlockSceneObject(ObjectID);
					ProgressionManager._003CInstance_003Ek__BackingField.SaveProgression();
					UnlockableSceneObject.RefreshAll();
				}
				else
				{
					string message = "[State_UnlockSceneObject] ProgressionManager missing for '" + ObjectID + "'.";
					Debug.LogWarning(message);
				}
				nint num = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ r9_v4 (Il2CppClass<SleepyNodes.State_UnlockSceneObject>)+218]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ r9_v4 (Il2CppClass<SleepyNodes.State_UnlockSceneObject>)+220]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v143 @ rax_v13 (should have been resolved before IL gen)");
			}
			Debug.LogWarning("[State_UnlockSceneObject] ObjectID is empty.");
			nint num2 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ r9_v1 (Il2CppClass<SleepyNodes.State_UnlockSceneObject>)+218]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ r9_v1 (Il2CppClass<SleepyNodes.State_UnlockSceneObject>)+220]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v115 @ rax_v6 (should have been resolved before IL gen)");
		}
	}
}
