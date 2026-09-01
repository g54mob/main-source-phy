using Cpp2ILInjected;

namespace SleepyNodes;

public class ObjectiveResultNode : ObjectiveStateNode
{
	public ObjectiveGraph.ObjectiveResults Result;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_00af: Expected I, but got O
		//IL_00bf: Expected O, but got I
		//IL_00cf: Expected O, but got I
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0067: Expected O, but got I
		while (true)
		{
			base.OnEnter(state);
			ObjectiveGraph objectiveGraph = (ObjectiveGraph)graph;
			if ((object)graph != null)
			{
				nint num = (nint)typeof(ObjectiveGraph);
				nint num2 = (nint)objectiveGraph;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r8_v4 (Il2CppClass<SleepyNodes.ObjectiveGraph>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r9_v4 (Il2CppClass<SleepyNodes.ObjectiveGraph>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r8_v4 (Il2CppClass<SleepyNodes.ObjectiveGraph>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r9_v4 (Il2CppClass<SleepyNodes.ObjectiveGraph>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v6+FFFFFFF8+v46 @ rax_v5*8]");
					if (0 == (nint)typeof(ObjectiveGraph))
					{
						((ObjectiveGraph)graph).Finish(Result);
					}
				}
			}
			nint num4 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r9_v2 (Il2CppClass<SleepyNodes.ObjectiveResultNode>)+218]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r9_v2 (Il2CppClass<SleepyNodes.ObjectiveResultNode>)+220]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v95 @ rax_v4 (should have been resolved before IL gen)");
		}
	}
}
