using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class StateGraph : NodeGraph
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<Node> _003C_003E9__4_0;

		public static Converter<Node, StateNodeEntry> _003C_003E9__4_1;

		public static Predicate<Node> _003C_003E9__7_0;

		public static Converter<Node, EventNode> _003C_003E9__7_1;

		public static Predicate<Node> _003C_003E9__13_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003Cget_EntryPoint_003Eb__4_0(Node x)
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			bool flag = (object)x == null;
			Node node = null;
			Node node2;
			if (!flag)
			{
				nint num = (nint)typeof(StateNodeEntry);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.StateNodeEntry>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.StateNodeEntry>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(StateNodeEntry);
					node2 = (Node)1;
					if (flag2)
					{
						goto IL_00d3;
					}
				}
				node2 = null;
				goto IL_00d3;
			}
			goto IL_00f5;
			IL_00d3:
			bool flag3 = (object)node2 == null;
			node = null;
			if (!flag3)
			{
				node = x;
			}
			goto IL_00f5;
			IL_00f5:
			bool flag4 = (object)node == null;
			return !flag4;
		}

		internal StateNodeEntry _003Cget_EntryPoint_003Eb__4_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(StateNodeEntry);
			if ((object)x == null)
			{
				return (StateNodeEntry)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.StateNodeEntry>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.StateNodeEntry>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(StateNodeEntry))
				{
					return (StateNodeEntry)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			StateNodeEntry result = default(StateNodeEntry);
			return result;
		}

		internal bool _003Cget_EventNodes_003Eb__7_0(Node x)
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			bool flag = (object)x == null;
			Node node = null;
			Node node2;
			if (!flag)
			{
				nint num = (nint)typeof(EventNode);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(EventNode);
					node2 = (Node)1;
					if (flag2)
					{
						goto IL_00d3;
					}
				}
				node2 = null;
				goto IL_00d3;
			}
			goto IL_00f5;
			IL_00d3:
			bool flag3 = (object)node2 == null;
			node = null;
			if (!flag3)
			{
				node = x;
			}
			goto IL_00f5;
			IL_00f5:
			bool flag4 = (object)node == null;
			return !flag4;
		}

		internal EventNode _003Cget_EventNodes_003Eb__7_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(EventNode);
			if ((object)x == null)
			{
				return (EventNode)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.EventNode>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.EventNode>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(EventNode))
				{
					return (EventNode)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			EventNode result = default(EventNode);
			return result;
		}

		internal bool _003CRun_003Eb__13_0(Node x)
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			bool flag = (object)x == null;
			Node node = null;
			Node node2;
			if (!flag)
			{
				nint num = (nint)typeof(VariableNode);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.VariableNode>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.VariableNode>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(VariableNode);
					node2 = (Node)1;
					if (flag2)
					{
						goto IL_00d3;
					}
				}
				node2 = null;
				goto IL_00d3;
			}
			goto IL_00f5;
			IL_00d3:
			bool flag3 = (object)node2 == null;
			node = null;
			if (!flag3)
			{
				node = x;
			}
			goto IL_00f5;
			IL_00f5:
			bool flag4 = (object)node == null;
			return !flag4;
		}
	}

	[NonSerialized]
	private StateNodeEntry _EntryPoint;

	[NonSerialized]
	private List<EventNode> _EventNodes;

	[NonSerialized]
	public StateNode.NodeExecutionState CurrentState;

	[NonSerialized]
	public Dictionary<string, StateNode.NodeExecutionState> SideExecutionPaths;

	public Dictionary<string, object> Variables;

	public override List<Type> NodeRestriction
	{
		get
		{
			List<Type> list = new List<Type>();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(StateNode));
			if (list != null)
			{
				list.Add(typeFromHandle);
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(StateNodeEntry));
				list.Add(typeFromHandle2);
				return list;
			}
			return (List<Type>)(object)new NullReferenceException();
		}
	}

	public StateNodeEntry EntryPoint
	{
		get
		{
			if (_EntryPoint == null)
			{
				Predicate<Node> match = _003C_003Ec._003C_003E9__4_0;
				if (_003C_003Ec._003C_003E9__4_0 == null)
				{
					match = (_003C_003Ec._003C_003E9__4_0 = delegate(Node x)
					{
						//IL_0013: Expected I, but got O
						//IL_001b: Expected I, but got O
						//IL_002b: Expected O, but got I
						//IL_0067: Expected O, but got I
						//IL_008c: Expected O, but got I4
						bool flag = (object)x == null;
						Node node = null;
						Node node2;
						if (!flag)
						{
							nint num = (nint)typeof(StateNodeEntry);
							nint num2 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.StateNodeEntry>)+130]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.StateNodeEntry>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
								bool flag2 = 0 == (nint)typeof(StateNodeEntry);
								node2 = (Node)1;
								if (flag2)
								{
									goto IL_00d3;
								}
							}
							node2 = null;
							goto IL_00d3;
						}
						goto IL_00f5;
						IL_00d3:
						bool flag3 = (object)node2 == null;
						node = null;
						if (!flag3)
						{
							node = x;
						}
						goto IL_00f5;
						IL_00f5:
						bool flag4 = (object)node == null;
						return !flag4;
					});
				}
				if (nodes != null)
				{
					List<Node> list = nodes.FindAll(match);
					Converter<Node, StateNodeEntry> converter = _003C_003Ec._003C_003E9__4_1;
					if (_003C_003Ec._003C_003E9__4_1 == null)
					{
						converter = (_003C_003Ec._003C_003E9__4_1 = delegate(Node x)
						{
							//IL_00ad: Expected I, but got O
							//IL_0012: Expected I, but got O
							//IL_0022: Expected O, but got I
							//IL_005e: Expected O, but got I
							nint num = (nint)typeof(StateNodeEntry);
							if ((object)x == null)
							{
								return (StateNodeEntry)x;
							}
							nint num2 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.StateNodeEntry>)+130]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.StateNodeEntry>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
								if (0 == (nint)typeof(StateNodeEntry))
								{
									return (StateNodeEntry)x;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
							StateNodeEntry result = default(StateNodeEntry);
							return result;
						});
					}
					if (list != null)
					{
						List<StateNodeEntry> list2 = list.ConvertAll(converter);
						if (list2 != null)
						{
							if (list2._size > 1)
							{
								Debug.LogError("State Grapth has more than one entry point");
							}
							if (list2._size == 0)
							{
								Debug.LogError("State Grapth has no entry points!");
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
							StateNodeEntry entryPoint = default(StateNodeEntry);
							_EntryPoint = entryPoint;
							goto IL_014a;
						}
					}
				}
				return (StateNodeEntry)(object)new NullReferenceException();
			}
			goto IL_014a;
			IL_014a:
			return _EntryPoint;
		}
	}

	public List<EventNode> EventNodes
	{
		get
		{
			if (_EventNodes == null)
			{
				Predicate<Node> match = _003C_003Ec._003C_003E9__7_0;
				if (_003C_003Ec._003C_003E9__7_0 == null)
				{
					match = (_003C_003Ec._003C_003E9__7_0 = delegate(Node x)
					{
						//IL_0013: Expected I, but got O
						//IL_001b: Expected I, but got O
						//IL_002b: Expected O, but got I
						//IL_0067: Expected O, but got I
						//IL_008c: Expected O, but got I4
						bool flag = (object)x == null;
						Node node = null;
						Node node2;
						if (!flag)
						{
							nint num = (nint)typeof(EventNode);
							nint num2 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode>)+130]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.EventNode>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
								bool flag2 = 0 == (nint)typeof(EventNode);
								node2 = (Node)1;
								if (flag2)
								{
									goto IL_00d3;
								}
							}
							node2 = null;
							goto IL_00d3;
						}
						goto IL_00f5;
						IL_00d3:
						bool flag3 = (object)node2 == null;
						node = null;
						if (!flag3)
						{
							node = x;
						}
						goto IL_00f5;
						IL_00f5:
						bool flag4 = (object)node == null;
						return !flag4;
					});
				}
				if (nodes != null)
				{
					List<Node> list = nodes.FindAll(match);
					Converter<Node, EventNode> converter = _003C_003Ec._003C_003E9__7_1;
					if (_003C_003Ec._003C_003E9__7_1 == null)
					{
						converter = (_003C_003Ec._003C_003E9__7_1 = delegate(Node x)
						{
							//IL_00ad: Expected I, but got O
							//IL_0012: Expected I, but got O
							//IL_0022: Expected O, but got I
							//IL_005e: Expected O, but got I
							nint num = (nint)typeof(EventNode);
							if ((object)x == null)
							{
								return (EventNode)x;
							}
							nint num2 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.EventNode>)+130]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.EventNode>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
								if (0 == (nint)typeof(EventNode))
								{
									return (EventNode)x;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
							EventNode result = default(EventNode);
							return result;
						});
					}
					if (list != null)
					{
						List<EventNode> eventNodes = list.ConvertAll(converter);
						_EventNodes = eventNodes;
						goto IL_0088;
					}
				}
				return (List<EventNode>)(object)new NullReferenceException();
			}
			goto IL_0088;
			IL_0088:
			return _EventNodes;
		}
	}

	public unsafe bool TryGetVariable<T>(string variableName, out T variable)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0056: Expected O, but got I
		//IL_020d: Expected O, but got I
		//IL_0082: Expected O, but got I8
		//IL_0252: Expected O, but got I
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		//IL_01e8: Expected I4, but got O
		//IL_0100: Expected O, but got I
		//IL_0100: Expected O, but got I
		//IL_0141: Expected O, but got I
		//IL_0141: Expected O, but got I
		//IL_015e: Expected O, but got I4
		//IL_015e: Expected O, but got Ref
		//IL_0180: Expected O, but got Ref
		//IL_0180: Expected O, but got Ref
		//IL_019d: Expected O, but got Ref
		//IL_019d: Expected O, but got Ref
		//IL_01ba: Expected O, but got Ref
		//IL_01ba: Expected O, but got I
		object value = default(object);
		object obj = (object)(&value);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj2 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj4;
		if ((nint)obj2 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
			object obj3 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
			if ((nint)obj3 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
			obj4 = (nint)0 + (nint)15;
			object obj5 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
			if ((nint)obj5 > 0)
			{
				goto IL_0276;
			}
		}
		obj4 = 1152921504606846960L;
		goto IL_0276;
		IL_0276:
		object obj6 = obj4 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		if (Variables != null)
		{
			ref object value2 = ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 64);
			if (Variables.TryGetValue(variableName, out value2))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+40]");
				if (((Dictionary<string, object>)0).TryGetValue((string)0, out value2))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+40]");
					nint num2 = 0;
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+48]");
					bool flag = ((Dictionary<string, object>)num2).TryGetValue((string)num3, out *(object*)null);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
					bool flag2 = ((Dictionary<string, object>)(&value)).TryGetValue((string)flag, out *(object*)null);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
					bool flag3 = ((Dictionary<string, object>)(&value)).TryGetValue((string)(&value), out *(object*)null);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
					bool flag4 = ((Dictionary<string, object>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref variable)).TryGetValue((string)(&value), out *(object*)null);
					bool flag5 = ((Dictionary<string, object>)0).TryGetValue((string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref variable), out value);
					return true;
				}
			}
			return false;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public void SetVariable(string variableName, object obj)
	{
		Variables.set_Item(variableName, obj);
	}

	public unsafe virtual void Run()
	{
		//IL_00c6: Expected O, but got Ref
		//IL_017d: Expected O, but got I
		StateNodeEntry entryPoint = EntryPoint;
		if (!(entryPoint != null))
		{
			return;
		}
		Dictionary<string, object> variables = new Dictionary<string, object>();
		Variables = variables;
		Predicate<Node> match = _003C_003Ec._003C_003E9__13_0;
		if (_003C_003Ec._003C_003E9__13_0 == null)
		{
			match = (_003C_003Ec._003C_003E9__13_0 = delegate(Node x)
			{
				//IL_0013: Expected I, but got O
				//IL_001b: Expected I, but got O
				//IL_002b: Expected O, but got I
				//IL_0067: Expected O, but got I
				//IL_008c: Expected O, but got I4
				bool flag2 = (object)x == null;
				Node node = null;
				Node node2;
				if (!flag2)
				{
					nint num = (nint)typeof(VariableNode);
					nint num2 = (nint)x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.VariableNode>)+130]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.VariableNode>)+130]");
					if (num3 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
						bool flag3 = 0 == (nint)typeof(VariableNode);
						node2 = (Node)1;
						if (flag3)
						{
							goto IL_00d3;
						}
					}
					node2 = null;
					goto IL_00d3;
				}
				goto IL_00f5;
				IL_00d3:
				bool flag4 = (object)node2 == null;
				node = null;
				if (!flag4)
				{
					node = x;
				}
				goto IL_00f5;
				IL_00f5:
				bool flag5 = (object)node == null;
				return !flag5;
			});
		}
		if (nodes != null)
		{
			List<Node> list = nodes.FindAll(match);
			if (list != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<Node>.Enumerator enumerator = default(List<Node>.Enumerator);
				UnityEngine.Object obj = default(UnityEngine.Object);
				object value = default(object);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = (object)obj == null;
					Dictionary<string, object> dictionary = (Dictionary<string, object>)(&enumerator);
					if (!flag)
					{
						string key = obj.name;
						NodePort port = ((Node)obj).GetPort("Value");
						if (port != null && port._direction == NodePort.IO.Output)
						{
							dictionary = (Dictionary<string, object>)(object)port._node;
							if ((object)port._node != null)
							{
								((Dictionary<TKey, TValue>)(object)port._node).set_Item((TKey)port, (TValue)0);
								if (Variables != null)
								{
									Variables.set_Item(key, value);
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
				CurrentState = newState;
				if (SideExecutionPaths != null)
				{
					SideExecutionPaths.Clear();
					StateNodeEntry entryPoint2 = EntryPoint;
					if ((object)entryPoint2 != null)
					{
						entryPoint2.Run(CurrentState);
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public virtual void Update()
	{
		//IL_00c1: Expected O, but got I4
		//IL_00ca: Expected O, but got I4
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_0131: Expected O, but got I
		//IL_015e: Expected O, but got I
		if (CurrentState != null)
		{
			StateNode.NodeExecutionState currentState = CurrentState;
			if (currentState.Node != null)
			{
				StateNode.NodeExecutionState currentState2 = CurrentState;
				currentState2.Node.OnExecute(CurrentState);
			}
		}
		if (SideExecutionPaths == null)
		{
			return;
		}
		int count = SideExecutionPaths.Count;
		if (count <= 0)
		{
			return;
		}
		Dictionary<string, StateNode.NodeExecutionState>.ValueCollection values = SideExecutionPaths.Values;
		List<StateNode.NodeExecutionState> list = Enumerable.ToList(values);
		object obj = 0;
		object obj2 = 0;
		object obj3 = default(object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ stack_8_v3+18]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ stack_8_v3+18]");
					object obj4 = 0;
					object obj5 = obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v359 @ r8_v7+1F8] (should have been resolved before IL gen)");
				}
			}
			obj++;
			obj2 = obj;
		}
	}

	public StateGraph()
	{
		Dictionary<string, StateNode.NodeExecutionState> sideExecutionPaths = new Dictionary<string, StateNode.NodeExecutionState>();
		SideExecutionPaths = sideExecutionPaths;
		Variables = new Dictionary<string, object>();
		List<Node> list = new List<Node>();
		nodes = list;
		base._003CNodeRestriction_003Ek__BackingField = new List<Type> { Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Node)) };
		base._003CNodeTypeExludes_003Ek__BackingField = new List<Type>();
		((ScriptableObject)this)._002Ector();
	}
}
