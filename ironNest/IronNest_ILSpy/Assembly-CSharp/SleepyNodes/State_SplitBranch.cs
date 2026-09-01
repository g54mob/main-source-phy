using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_SplitBranch : StateNode
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<string, object>, string> _003C_003E9__2_0;

		public static Func<KeyValuePair<string, object>, object> _003C_003E9__2_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003COnEnter_003Eb__2_0(KeyValuePair<string, object> entry)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
			string result = default(string);
			return result;
		}

		internal object _003COnEnter_003Eb__2_1(KeyValuePair<string, object> entry)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
			object result = default(object);
			return result;
		}
	}

	public bool InheritContextVariables;

	public string[] To;

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_03c9: Expected I, but got O
		//IL_03d9: Expected O, but got I
		//IL_03e9: Expected O, but got I
		//IL_0450: Expected I4, but got I8
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Expected O, but got Unknown
		//IL_0217: Expected I, but got O
		//IL_021f: Expected I, but got O
		//IL_022f: Expected O, but got I
		//IL_02af: Expected O, but got I4
		//IL_0121: Expected I, but got O
		//IL_0129: Expected I, but got O
		//IL_0139: Expected O, but got I
		//IL_026b: Expected O, but got I
		//IL_0175: Expected O, but got I
		//IL_02a1: Expected O, but got I4
		//IL_034d: Expected I, but got O
		Func<KeyValuePair<string, object>, string> func2 = default(Func<KeyValuePair<string, object>, string>);
		Node node2 = default(Node);
		int num = default(int);
		NodePort nodePort = default(NodePort);
		object arg = default(object);
		while (true)
		{
			base.OnEnter(state);
			string[] to = To;
			Func<KeyValuePair<string, object>, string> func = func2;
			Node node = node2;
			IEnumerable<KeyValuePair<string, object>> enumerable = null;
			IEnumerable<KeyValuePair<string, object>> enumerable2 = null;
			NodeExecutionState nodeExecutionState = state;
			for (; (nint)enumerable2 < to.Length; to = To, enumerable = (IEnumerable<KeyValuePair<string, object>>)(enumerable + 1), enumerable2 = enumerable)
			{
				Node._003Cget_DynamicOutputs_003Ed__12 obj = new Node._003Cget_DynamicOutputs_003Ed__12(0);
				obj._003C_003E1__state = -2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				obj._003C_003El__initialThreadId = num;
				obj._003C_003E4__this = this;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AE970");
				NodePort connection;
				NodeExecutionState newState;
				object obj6;
				nint num2;
				if (nodePort != null && nodePort.IsConnected)
				{
					connection = nodePort.Connection;
					Node node3 = connection._node;
					newState = NodeExecutionState.NewState;
					if (!InheritContextVariables)
					{
						if ((object)connection._node == null)
						{
							node = null;
							num2 = 0;
						}
						else
						{
							nint num3 = (nint)typeof(StateNode);
							nint num4 = (nint)node3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v448 @ rdx_v29 (Il2CppClass<SleepyNodes.StateNode>)+130]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v449 @ r8_v18 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v448 @ rdx_v29 (Il2CppClass<SleepyNodes.StateNode>)+130]");
							if (num5 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v449 @ r8_v18 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj3 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v524 @ rax_v69+FFFFFFF8+v450 @ rax_v66*8]");
								if (0 == (nint)typeof(StateNode))
								{
									node = null;
									node = connection._node;
									num2 = 0;
									goto IL_045f;
								}
							}
							node = null;
							num2 = 0;
						}
						goto IL_045f;
					}
					if ((object)connection._node == null)
					{
						node = null;
						goto IL_04a9;
					}
					nint num6 = (nint)typeof(StateNode);
					nint num7 = (nint)node3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v464 @ rdx_v28 (Il2CppClass<SleepyNodes.StateNode>)+130]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v465 @ r8_v17 (Il2CppClass<SleepyNodes.Node>)+130]");
					nint num8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v464 @ rdx_v28 (Il2CppClass<SleepyNodes.StateNode>)+130]");
					if (num8 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v465 @ r8_v17 (Il2CppClass<SleepyNodes.Node>)+C8]");
						object obj5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v573 @ rax_v64+FFFFFFF8+v466 @ rax_v60*8]");
						if (0 == (nint)typeof(StateNode))
						{
							obj6 = 1;
							goto IL_0487;
						}
					}
					obj6 = 0;
					goto IL_0487;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				string message = $"Path {arg} not connected";
				Debug.LogError(message);
				IEnumerable<KeyValuePair<string, object>> enumerable3 = enumerable;
				continue;
				IL_0487:
				bool flag = obj6 == null;
				node = null;
				if (!flag)
				{
					node = connection._node;
				}
				goto IL_04a9;
				IL_04a9:
				func = _003C_003Ec._003C_003E9__2_0;
				if (_003C_003Ec._003C_003E9__2_0 == null)
				{
					func = (_003C_003Ec._003C_003E9__2_0 = delegate
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
						string result = default(string);
						return result;
					});
				}
				Func<KeyValuePair<string, object>, object> elementSelector = _003C_003Ec._003C_003E9__2_1;
				if (_003C_003Ec._003C_003E9__2_1 == null)
				{
					elementSelector = (_003C_003Ec._003C_003E9__2_1 = delegate
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
						object result = default(object);
						return result;
					});
				}
				Dictionary<string, object> state2 = Enumerable.ToDictionary(nodeExecutionState.State, func, elementSelector);
				newState.State = state2;
				enumerable3 = nodeExecutionState.State;
				num2 = 0;
				nodeExecutionState = state;
				goto IL_045f;
				IL_045f:
				NodePort connection2 = nodePort.Connection;
				newState.lastFieldPort = connection2._fieldName;
				nint num9 = (nint)node;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v716 @ r8_v9 (Il2CppClass<SleepyNodes.Node>)+1E8] (should have been resolved before IL gen)");
			}
			nint num10 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v3 (Il2CppClass<SleepyNodes.State_SplitBranch>)+218]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v3 (Il2CppClass<SleepyNodes.State_SplitBranch>)+220]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v269 @ rax_v7 (should have been resolved before IL gen)");
		}
	}

	public State_SplitBranch()
	{
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
