using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_AddLeaderboardPoints : StateNode
{
	public enum Operations
	{
		Add,
		Remove
	}

	public StateNode To;

	public Operations Operation;

	public ContextVariableOrInline_Int Amount;

	public string ActionName;

	public string ActionDetails;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0140: Expected I, but got O
		//IL_0150: Expected O, but got I
		//IL_0160: Expected O, but got I
		base.OnEnter(state);
		SteamLeaderboardScoreController steamLeaderboardScoreController = Object.FindFirstObjectByType<SteamLeaderboardScoreController>();
		int num = default(int);
		if (steamLeaderboardScoreController != null)
		{
			int amount;
			if (Operation == Operations.Add)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
				amount = num;
			}
			else
			{
				if (Operation != Operations.Remove)
				{
					goto IL_00bc;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
				amount = -num;
			}
			steamLeaderboardScoreController.AddToScore(amount);
		}
		goto IL_00bc;
		IL_00bc:
		if (!(LeaderboardManager.Instance != null))
		{
			goto IL_013b;
		}
		if (Operation != Operations.Add)
		{
			goto IL_016a;
		}
		string actionName = ActionName;
		string actionDetails = ActionDetails;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
		int scoreDelta = num;
		goto IL_01e3;
		IL_016a:
		if (Operation != Operations.Remove)
		{
			goto IL_013b;
		}
		actionName = ActionName;
		actionDetails = ActionDetails;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
		scoreDelta = -num;
		goto IL_01e3;
		IL_01e3:
		bool includeImage = default(bool);
		LeaderboardManager.Instance.RecordAction(actionName, actionDetails, scoreDelta, includeImage);
		goto IL_013b;
		IL_013b:
		nint num2 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v364 @ r9_v4 (Il2CppClass<SleepyNodes.State_AddLeaderboardPoints>)+218]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v364 @ r9_v4 (Il2CppClass<SleepyNodes.State_AddLeaderboardPoints>)+220]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v368 @ rax_v13 (should have been resolved before IL gen)");
		goto IL_016a;
	}
}
