using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_Newspaper : StateNode
{
	public StateNode To;

	public string NotifID;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0034: Expected O, but got I4
		//IL_003d: Expected O, but got I4
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		base.OnEnter(state);
		EndMissionRef[] array = Object.FindObjectsByType<EndMissionRef>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		object obj = array + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < array.Length)
		{
			GameObject gameObject = ((Component)obj).gameObject;
			gameObject.SetActive(value: true);
			obj3++;
			obj += 8;
			obj2 = obj3;
		}
	}

	public override void OnExecute(NodeExecutionState state)
	{
	}

	public override void OnNotification(NodeExecutionState state, string notif)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7AD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (notif == NotifID)
		{
			base.OnExit(state, "To");
		}
	}

	public State_Newspaper()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7AE]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		NotifID = "Newspaper Finished";
		base._002Ector();
	}
}
