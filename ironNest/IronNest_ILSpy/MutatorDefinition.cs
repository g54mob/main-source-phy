using Cpp2ILInjected;
using UnityEngine;

public class MutatorDefinition : ScriptableObject
{
	public string displayName;

	public string description;

	public MutatorDefinition()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A8DD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		displayName = "New Mutator";
		base._002Ector();
	}
}
