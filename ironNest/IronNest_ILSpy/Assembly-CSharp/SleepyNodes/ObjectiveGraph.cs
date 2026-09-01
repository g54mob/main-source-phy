using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class ObjectiveGraph : StateGraph
{
	public enum ObjectiveResults
	{
		Success,
		Failure
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<Node> _003C_003E9__6_0;

		public static Converter<Node, ObjectiveEntry> _003C_003E9__6_1;

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
				nint num = (nint)typeof(ObjectiveEntry);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.ObjectiveEntry>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.ObjectiveEntry>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(ObjectiveEntry);
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

		internal ObjectiveEntry _003Cget_EntryPoint_003Eb__6_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(ObjectiveEntry);
			if ((object)x == null)
			{
				return (ObjectiveEntry)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.ObjectiveEntry>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.ObjectiveEntry>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(ObjectiveEntry))
				{
					return (ObjectiveEntry)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			ObjectiveEntry result = default(ObjectiveEntry);
			return result;
		}
	}

	[NonSerialized]
	private ObjectiveEntry _EntryPoint;

	[NonSerialized]
	public bool IsActivated;

	[NonSerialized]
	public MissionGraph ParentGraph;

	[NonSerialized]
	public State_Objective ParentNode;

	public override List<Type> NodeRestriction
	{
		get
		{
			List<Type> list = new List<Type>();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ObjectiveEntry));
			if (list != null)
			{
				list.Add(typeFromHandle);
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(StateNode));
				list.Add(typeFromHandle2);
				Type typeFromHandle3 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ObjectiveStateNode));
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
				return list;
			}
			return (List<Type>)(object)new NullReferenceException();
		}
	}

	public new ObjectiveEntry EntryPoint
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
							nint num = (nint)typeof(ObjectiveEntry);
							nint num2 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.ObjectiveEntry>)+130]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.ObjectiveEntry>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
								bool flag2 = 0 == (nint)typeof(ObjectiveEntry);
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
					Converter<Node, ObjectiveEntry> converter = _003C_003Ec._003C_003E9__6_1;
					if (_003C_003Ec._003C_003E9__6_1 == null)
					{
						converter = (_003C_003Ec._003C_003E9__6_1 = delegate(Node x)
						{
							//IL_00ad: Expected I, but got O
							//IL_0012: Expected I, but got O
							//IL_0022: Expected O, but got I
							//IL_005e: Expected O, but got I
							nint num = (nint)typeof(ObjectiveEntry);
							if ((object)x == null)
							{
								return (ObjectiveEntry)x;
							}
							nint num2 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.ObjectiveEntry>)+130]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.ObjectiveEntry>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
								if (0 == (nint)typeof(ObjectiveEntry))
								{
									return (ObjectiveEntry)x;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
							ObjectiveEntry result = default(ObjectiveEntry);
							return result;
						});
					}
					if (list != null)
					{
						List<ObjectiveEntry> list2 = list.ConvertAll(converter);
						if (list2 != null)
						{
							if (list2._size > 1)
							{
								Debug.LogWarning("State Grapth has more than one entry point");
							}
							if (list2._size == 1)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								ObjectiveEntry entryPoint = default(ObjectiveEntry);
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
				return (ObjectiveEntry)(object)new NullReferenceException();
			}
			goto IL_016f;
			IL_016f:
			return _EntryPoint;
		}
	}

	public unsafe void SendNotification(string notifID)
	{
		//IL_00ca: Expected O, but got Ref
		//IL_00e8: Expected O, but got I
		if (!IsActivated)
		{
			return;
		}
		StateNode.NodeExecutionState currentState = CurrentState;
		if (CurrentState != null && (object)currentState.Node != null)
		{
			currentState.Node.OnNotification(CurrentState, notifID);
		}
		if (SideExecutionPaths != null)
		{
			Dictionary<string, StateNode.NodeExecutionState>.ValueCollection values = SideExecutionPaths.Values;
			if (values != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
				Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator enumerator = default(Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator);
				object obj = default(object);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = obj == null;
					Dictionary<string, StateNode.NodeExecutionState> dictionary = (Dictionary<string, StateNode.NodeExecutionState>)(&enumerator);
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ stack_20_v5+18]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ stack_20_v5+18]");
						if ((nint)0 != 0)
						{
							object obj3 = obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v330 @ r9_v6+1D8] (should have been resolved before IL gen)");
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				EventData_Notification eventData_Notification = new EventData_Notification();
				if (eventData_Notification != null)
				{
					eventData_Notification._003CNotificationID_003Ek__BackingField = notifID;
					CheckEvents(eventData_Notification);
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void CheckEvents(EventNode.EventData evt)
	{
		//IL_00a0: Expected O, but got Ref
		if (!IsActivated)
		{
			return;
		}
		List<EventNode> eventNodes = base.EventNodes;
		if (eventNodes == null)
		{
			return;
		}
		List<EventNode> eventNodes2 = base.EventNodes;
		if (eventNodes2._size <= 0)
		{
			return;
		}
		List<EventNode> eventNodes3 = base.EventNodes;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<EventNode>.Enumerator enumerator = default(List<EventNode>.Enumerator);
		object obj = default(object);
		object obj4 = default(object);
		object obj6 = default(object);
		string text = default(string);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				object obj2 = (object)(&enumerator);
				if (flag)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ stack_8_v4+49]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ stack_8_v4+4B]");
					if ((nint)0 == 0)
					{
						continue;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ stack_8_v4+48]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ stack_8_v4+4A]");
					if ((nint)0 != 0)
					{
						continue;
					}
				}
				object obj3 = obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v402 @ r8_v7+238] (should have been resolved before IL gen)");
				if (obj4 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
					object obj5 = obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v405 @ rdx_v14+1B8] (should have been resolved before IL gen)");
					string message = "[EVENT] Starting: " + text;
					Debug.Log(message);
					StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
					object obj7 = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v421 @ r8_v11+248] (should have been resolved before IL gen)");
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public void StartObjective(MissionGraph missionGraph, State_Objective parentNode)
	{
		//IL_001e: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		ParentGraph = missionGraph;
		ParentNode = parentNode;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rdx_v2 (Il2CppClass<SleepyNodes.ObjectiveGraph>)+1F8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rdx_v2 (Il2CppClass<SleepyNodes.ObjectiveGraph>)+200]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v15 @ rax_v3 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
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
		ObjectiveEntry entryPoint = EntryPoint;
		if (entryPoint != null)
		{
			ResetNodes();
			IsActivated = true;
			StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
			CurrentState = newState;
			SideExecutionPaths.Clear();
			ObjectiveEntry entryPoint2 = EntryPoint;
			entryPoint2.Run(CurrentState);
		}
	}

	public void Finish(ObjectiveResults result)
	{
		IsActivated = false;
		ParentNode.OnResult(this, result);
	}

	public override void Update()
	{
		if (IsActivated)
		{
			base.Update();
		}
	}
}
