using Cpp2ILInjected;

namespace SleepyNodes;

public class State_WaitForNotification : StateNode
{
	public StateNode To;

	public string NotifID;

	public override void OnEnter(NodeExecutionState state)
	{
		base.OnEnter(state);
	}

	public override void OnExecute(NodeExecutionState state)
	{
	}

	public override void OnNotification(NodeExecutionState state, string notif)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7CB]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (notif == NotifID)
		{
			base.OnExit(state, "To");
		}
	}

	public State_WaitForNotification()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A7CC]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		NotifID = "";
		base._002Ector();
	}
}
