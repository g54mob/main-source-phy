using System;
using System.Collections.Generic;
using Cpp2ILInjected;

[Serializable]
public class ReloadStateDef
{
	public string stateKey;

	public string displayName;

	public List<string> triggers;

	public bool isReloadCompleteState;

	public bool autoAdvanceToNextState;

	public LookAtTarget advanceButton;

	public ReloadStateDef()
	{
		List<string> list = new List<string>();
		triggers = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
