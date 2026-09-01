using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace SleepyNodes;

[Serializable]
public abstract class EventNode : StateNode
{
	public class EventData
	{
	}

	public StateNode To;

	public bool OnlyOnce;

	public bool EnableOnStart = true;

	[NonSerialized]
	private bool AlreadyTriggered;

	[NonSerialized]
	private bool EventEnabled;

	public override void ResetNode()
	{
		AlreadyTriggered = false;
	}

	public bool CheckShouldRun(EventData data)
	{
		//IL_008a: Expected I, but got O
		//IL_009a: Expected O, but got I
		//IL_00aa: Expected O, but got I
		if ((!EnableOnStart && !EventEnabled) || (OnlyOnce && AlreadyTriggered))
		{
			return false;
		}
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ r8_v1 (Il2CppClass<SleepyNodes.EventNode>)+238]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ r8_v1 (Il2CppClass<SleepyNodes.EventNode>)+240]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v74 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected abstract bool ShouldRun(EventData data);

	public unsafe virtual void Run(NodeExecutionState state)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected Ref, but got Unknown
		AlreadyTriggered = true;
		StateNode connectedNode = GetConnectedNode<StateNode>("To", out *(string*)(state + 40));
		To = connectedNode;
		if (To != null)
		{
			To.OnEnter(state);
		}
	}

	public sealed override void OnEnter(NodeExecutionState state)
	{
		//IL_000f: Expected I, but got O
		//IL_002a: Expected O, but got I
		//IL_003a: Expected O, but got I
		base.OnEnter(state);
		nint num = (nint)this;
		EventEnabled = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ r9_v1 (Il2CppClass<SleepyNodes.EventNode>)+218]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ r9_v1 (Il2CppClass<SleepyNodes.EventNode>)+220]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v13 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public sealed override void OnExit(NodeExecutionState state, StateNode To, string alternatePort = "")
	{
		//IL_006a: Expected I, but got O
		//IL_0072: Expected I, but got O
		//IL_0082: Expected O, but got I
		//IL_00be: Expected O, but got I
		//IL_019c: Expected O, but got I
		//IL_0140: Expected O, but got I
		//IL_015a: Expected O, but got I
		state.lastFieldPort = alternatePort;
		if (To == null)
		{
			NodeGraph nodeGraph = graph;
			if ((object)graph == null)
			{
				return;
			}
			nint num = (nint)typeof(StateGraph);
			nint num2 = (nint)nodeGraph;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ rdx_v6 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r8_v6 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ rdx_v6 (Il2CppClass<SleepyNodes.StateGraph>)+130]");
			if (num3 < 0)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r8_v6 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v11+FFFFFFF8+v175 @ rax_v10*8]");
			if (0 != (nint)typeof(StateGraph))
			{
				return;
			}
			if (!string.IsNullOrWhiteSpace(state.ID))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rbx_v5 (SleepyNodes.NodeGraph)+40]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rbx_v5 (SleepyNodes.NodeGraph)+40]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ rcx_v13+10]");
					if ((string)0 == state.ID)
					{
						_ = 0;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rbx_v5 (SleepyNodes.NodeGraph)+48]");
				bool flag = ((Dictionary<string, NodeExecutionState>)0).Remove(state.ID);
			}
			else
			{
				_ = 0;
			}
		}
		else
		{
			To.OnEnter(state);
		}
	}

	public sealed override void OnExecute(NodeExecutionState state)
	{
	}

	protected EventNode()
	{
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
