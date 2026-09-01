using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class PunchcardGraph : StateGraph
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<Node> _003C_003E9__6_0;

		public static Converter<Node, State_CardActionStart> _003C_003E9__6_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003Cget_EntryPoint_003Eb__6_0(Node x)
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
				nint num = (nint)typeof(State_CardActionStart);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_CardActionStart>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_CardActionStart>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(State_CardActionStart);
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

		internal State_CardActionStart _003Cget_EntryPoint_003Eb__6_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(State_CardActionStart);
			if ((object)x == null)
			{
				return (State_CardActionStart)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_CardActionStart>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_CardActionStart>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(State_CardActionStart))
				{
					return (State_CardActionStart)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			State_CardActionStart result = default(State_CardActionStart);
			return result;
		}
	}

	[NonSerialized]
	private State_CardActionStart _EntryPoint;

	public bool IsActivated;

	public override List<Type> NodeRestriction
	{
		get
		{
			List<Type> list = new List<Type>();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_CardActionStart));
			if (list != null)
			{
				list.Add(typeFromHandle);
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(StateNode));
				list.Add(typeFromHandle2);
				Type typeFromHandle3 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(PunchcardVariableNode));
				list.Add(typeFromHandle3);
				return list;
			}
			return (List<Type>)(object)new NullReferenceException();
		}
	}

	public override List<Type> NodeTypeExludes
	{
		get
		{
			List<Type> list = new List<Type>();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_Objective));
			if (list != null)
			{
				list.Add(typeFromHandle);
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(EventNode));
				list.Add(typeFromHandle2);
				Type typeFromHandle3 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_Start));
				list.Add(typeFromHandle3);
				Type typeFromHandle4 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_End));
				list.Add(typeFromHandle4);
				Type typeFromHandle5 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ObjectiveResultNode));
				list.Add(typeFromHandle5);
				return list;
			}
			return (List<Type>)(object)new NullReferenceException();
		}
	}

	public new State_CardActionStart EntryPoint
	{
		get
		{
			if (_EntryPoint == null)
			{
				Predicate<Node> match = _003C_003Ec._003C_003E9__6_0;
				if (_003C_003Ec._003C_003E9__6_0 == null)
				{
					match = (_003C_003Ec._003C_003E9__6_0 = delegate(Node x)
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
							nint num = (nint)typeof(State_CardActionStart);
							nint num2 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_CardActionStart>)+130]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_CardActionStart>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
								bool flag2 = 0 == (nint)typeof(State_CardActionStart);
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
					Converter<Node, State_CardActionStart> converter = _003C_003Ec._003C_003E9__6_1;
					if (_003C_003Ec._003C_003E9__6_1 == null)
					{
						converter = (_003C_003Ec._003C_003E9__6_1 = delegate(Node x)
						{
							//IL_00ad: Expected I, but got O
							//IL_0012: Expected I, but got O
							//IL_0022: Expected O, but got I
							//IL_005e: Expected O, but got I
							nint num = (nint)typeof(State_CardActionStart);
							if ((object)x == null)
							{
								return (State_CardActionStart)x;
							}
							nint num2 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_CardActionStart>)+130]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_CardActionStart>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
								if (0 == (nint)typeof(State_CardActionStart))
								{
									return (State_CardActionStart)x;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
							State_CardActionStart result = default(State_CardActionStart);
							return result;
						});
					}
					if (list != null)
					{
						List<State_CardActionStart> list2 = list.ConvertAll(converter);
						if (list2 != null)
						{
							if (list2._size > 1)
							{
								Debug.LogWarning("State Grapth has more than one entry point");
							}
							if (list2._size == 1)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								State_CardActionStart entryPoint = default(State_CardActionStart);
								_EntryPoint = entryPoint;
							}
							if (list2._size == 0)
							{
								Debug.LogWarning("State Grapth has no entry points!");
							}
							goto IL_016f;
						}
					}
				}
				return (State_CardActionStart)(object)new NullReferenceException();
			}
			goto IL_016f;
			IL_016f:
			return _EntryPoint;
		}
	}

	public virtual void ResetNodes()
	{
		//IL_004f: Expected I, but got O
		//IL_005d: Expected I, but got O
		//IL_006d: Expected O, but got I
		//IL_00a9: Expected O, but got I
		//IL_010a: Expected I, but got O
		//IL_0118: Expected I, but got O
		//IL_0128: Expected O, but got I
		//IL_0164: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<Node>.Enumerator enumerator = default(List<Node>.Enumerator);
		object obj = default(object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			bool flag = obj == null;
			num = 0;
			if (flag)
			{
				continue;
			}
			num = (nint)obj;
			nint num2 = (nint)typeof(StateNode);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rdx_v6 (Il2CppClass<SleepyNodes.StateNode>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppMethodInfo)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rdx_v6 (Il2CppClass<SleepyNodes.StateNode>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppMethodInfo)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rax_v15+FFFFFFF8+v228 @ rax_v9*8]");
				if (0 == (nint)typeof(StateNode))
				{
					if (obj == null)
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v82 @ r8_v2 (Il2CppMethodInfo)+1C8] (should have been resolved before IL gen)");
				}
			}
			if (obj == null)
			{
				continue;
			}
			num = (nint)obj;
			nint num4 = (nint)typeof(EventNode);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdx_v8 (Il2CppClass<SleepyNodes.EventNode>)+130]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppMethodInfo)+130]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdx_v8 (Il2CppClass<SleepyNodes.EventNode>)+130]");
			if (num5 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppMethodInfo)+C8]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rax_v12+FFFFFFF8+v89 @ rax_v11*8]");
				if (0 == (nint)typeof(EventNode) && obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v82 @ r8_v2 (Il2CppMethodInfo)+1C8] (should have been resolved before IL gen)");
				}
			}
		}
		enumerator.Dispose();
	}

	public override void Run()
	{
		State_CardActionStart entryPoint = EntryPoint;
		if (entryPoint != null)
		{
			ResetNodes();
			IsActivated = true;
			StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
			CurrentState = newState;
			SideExecutionPaths.Clear();
			State_CardActionStart entryPoint2 = EntryPoint;
			entryPoint2.Run(CurrentState);
		}
	}

	public override void Update()
	{
		if (IsActivated)
		{
			base.Update();
		}
	}
}
