using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace SleepyNodes;

public class State_CardActionStart : StateNodeEntry
{
	[Serializable]
	public class PunchardVariableSetup
	{
		public string PunchardVariableID;

		public PunchcardVariable.VariableTypes VariableType;
	}

	public List<PunchardVariableSetup> Variables;

	public override void Run(StateNode.NodeExecutionState state)
	{
		//IL_0073: Expected I, but got O
		//IL_0081: Expected I, but got O
		//IL_0091: Expected O, but got I
		//IL_0051: Expected O, but got I4
		//IL_0015: Expected O, but got I
		//IL_003a: Expected O, but got I4
		NodeGraph nodeGraph = graph;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 88 Invalid \"Jump target not found in method: 0x1804EF3DB\"");
		nint num = (nint)nodeGraph;
		nint num2 = (nint)typeof(StateGraph);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rdx_v1 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v1 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rdx_v1 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
		object obj3;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r9_v1 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v12+FFFFFFF8+v68 @ rax_v3*8]");
			bool flag = 0 == (nint)typeof(StateGraph);
			obj3 = 1;
			if (flag)
			{
				goto IL_00bd;
			}
		}
		obj3 = 0;
		goto IL_00bd;
		IL_00bd:
		if (obj3 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 144 Invalid \"Jump target not found in method: 0x1804EF3DB\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 156 Invalid \"Jump target not found in method: 0x1804EF3FC\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		}
		List<PunchardVariableSetup>.Enumerator enumerator = default(List<PunchardVariableSetup>.Enumerator);
		do
		{
			bool flag2 = enumerator.MoveNext();
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 182 Invalid \"Jump target not found in method: 0x1804EF39F\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 198 Invalid \"Jump target not found in method: 0x1804EF421\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ stack_-48_v2+18]");
		}
		while ((nint)0 > (nint)5);
	}

	public State_CardActionStart()
	{
		List<PunchardVariableSetup> variables = new List<PunchardVariableSetup>();
		Variables = variables;
		base._002Ector();
	}
}
