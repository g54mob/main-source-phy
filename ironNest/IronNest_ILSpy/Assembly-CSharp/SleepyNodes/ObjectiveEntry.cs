using System;
using Cpp2ILInjected;

namespace SleepyNodes;

[Serializable]
public class ObjectiveEntry : Node
{
	public StateNode To;

	public virtual void Run(StateNode.NodeExecutionState state)
	{
		//IL_015d: Expected I, but got O
		//IL_0026: Expected I, but got O
		//IL_0057: Expected I, but got O
		//IL_0079: Expected I, but got O
		//IL_008f: Expected I, but got O
		//IL_009f: Expected O, but got I
		//IL_00cb: Expected I, but got O
		//IL_00e9: Expected O, but got I
		//IL_0116: Expected I, but got O
		NodePort outputPort = GetOutputPort("To");
		bool flag = outputPort == null;
		nint num = unchecked((nint)null);
		Node node = this;
		if (!flag)
		{
			NodePort connection = outputPort.Connection;
			bool flag2 = connection == null;
			num = unchecked((nint)null);
			node = (Node)(object)outputPort;
			if (!flag2)
			{
				node = connection._node;
				nint num2 = (nint)typeof(StateNode);
				bool flag3 = (object)connection._node == null;
				num = (nint)typeof(StateNode);
				if (!flag3)
				{
					nint num3 = (nint)node;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ r8_v4 (Il2CppClass<SleepyNodes.StateNode>)+130]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ r9_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ r8_v4 (Il2CppClass<SleepyNodes.StateNode>)+130]");
					bool flag4 = num4 < 0;
					num = (nint)typeof(StateNode);
					if (!flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ r9_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v93 @ rax_v9+FFFFFFF8+v92 @ rax_v8*8]");
						bool flag5 = 0 != (nint)typeof(StateNode);
						num = (nint)typeof(StateNode);
						if (!flag5)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v86 @ r9_v1 (Il2CppClass<SleepyNodes.Node>)+1E8] (should have been resolved before IL gen)");
							goto IL_012e;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					return;
				}
			}
		}
		goto IL_012e;
		IL_012e:
		throw new NullReferenceException();
	}

	public override object GetValue(NodePort port)
	{
		return this;
	}
}
