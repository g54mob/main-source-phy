using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class ImpactGraph : StateGraph
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<Node> _003C_003E9__4_0;

		public static Converter<Node, State_Objective> _003C_003E9__4_1;

		public static Predicate<Node> _003C_003E9__5_0;

		public static Converter<Node, State_Objective> _003C_003E9__5_1;

		public static Func<Node, bool> _003C_003E9__7_0;

		public static Func<Node, State_ImpactStart> _003C_003E9__7_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003COnNotification_003Eb__4_0(Node x)
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
				nint num = (nint)typeof(State_Objective);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(State_Objective);
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

		internal State_Objective _003COnNotification_003Eb__4_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(State_Objective);
			if ((object)x == null)
			{
				return (State_Objective)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(State_Objective))
				{
					return (State_Objective)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			State_Objective result = default(State_Objective);
			return result;
		}

		internal bool _003CCheckEvents_003Eb__5_0(Node x)
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
				nint num = (nint)typeof(State_Objective);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(State_Objective);
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

		internal State_Objective _003CCheckEvents_003Eb__5_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(State_Objective);
			if ((object)x == null)
			{
				return (State_Objective)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(State_Objective))
				{
					return (State_Objective)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			State_Objective result = default(State_Objective);
			return result;
		}

		internal bool _003CStartImpact_003Eb__7_0(Node x)
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
				nint num = (nint)typeof(State_ImpactStart);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_ImpactStart>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_ImpactStart>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(State_ImpactStart);
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

		internal State_ImpactStart _003CStartImpact_003Eb__7_1(Node x)
		{
			//IL_00ad: Expected I, but got O
			//IL_0012: Expected I, but got O
			//IL_0022: Expected O, but got I
			//IL_005e: Expected O, but got I
			nint num = (nint)typeof(State_ImpactStart);
			if ((object)x == null)
			{
				return (State_ImpactStart)x;
			}
			nint num2 = (nint)x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_ImpactStart>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_ImpactStart>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
				if (0 == (nint)typeof(State_ImpactStart))
				{
					return (State_ImpactStart)x;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			State_ImpactStart result = default(State_ImpactStart);
			return result;
		}
	}

	public override List<Type> NodeRestriction
	{
		get
		{
			List<Type> list = new List<Type>();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_ImpactStart));
			if (list != null)
			{
				list.Add(typeFromHandle);
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(StateNode));
				list.Add(typeFromHandle2);
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

	public unsafe void OnNotification(string notifID)
	{
		//IL_00a1: Expected O, but got Ref
		//IL_00bf: Expected O, but got I
		//IL_0150: Expected I, but got O
		//IL_031b: Expected I, but got O
		//IL_0331: Expected O, but got I
		//IL_0357: Expected O, but got I
		//IL_036e: Expected O, but got I
		//IL_01a1: Expected O, but got I4
		//IL_03ac: Expected I, but got O
		//IL_03c2: Expected O, but got I
		//IL_01d1: Expected O, but got I4
		//IL_023d: Expected O, but got I4
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
				State_Objective state_Objective = default(State_Objective);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = (object)state_Objective == null;
					Dictionary<string, StateNode.NodeExecutionState> dictionary = (Dictionary<string, StateNode.NodeExecutionState>)(&enumerator);
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ stack_20_v7 (SleepyNodes.State_Objective)+18]");
						object obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ stack_20_v7 (SleepyNodes.State_Objective)+18]");
						if ((nint)0 != 0)
						{
							object obj2 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v442 @ r9_v10+1D8] (should have been resolved before IL gen)");
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				Predicate<Node> match = _003C_003Ec._003C_003E9__4_0;
				if (_003C_003Ec._003C_003E9__4_0 == null)
				{
					Predicate<Node> predicate = (_003C_003Ec._003C_003E9__4_0 = delegate(Node x)
					{
						//IL_0013: Expected I, but got O
						//IL_001b: Expected I, but got O
						//IL_002b: Expected O, but got I
						//IL_0067: Expected O, but got I
						//IL_008c: Expected O, but got I4
						bool flag5 = (object)x == null;
						Node node = null;
						Node node2;
						if (!flag5)
						{
							nint num5 = (nint)typeof(State_Objective);
							nint num6 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
							object obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
							if (num7 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj4 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
								bool flag6 = 0 == (nint)typeof(State_Objective);
								node2 = (Node)1;
								if (flag6)
								{
									goto IL_00d3;
								}
							}
							node2 = null;
							goto IL_00d3;
						}
						goto IL_00f5;
						IL_00d3:
						bool flag7 = (object)node2 == null;
						node = null;
						if (!flag7)
						{
							node = x;
						}
						goto IL_00f5;
						IL_00f5:
						bool flag8 = (object)node == null;
						return !flag8;
					});
					nint num = (nint)typeof(_003C_003Ec);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ rax_v52 (Il2CppClass<SleepyNodes.ImpactGraph+<>c>)+B8]");
					Dictionary<string, StateNode.NodeExecutionState> dictionary = (Dictionary<string, StateNode.NodeExecutionState>)((nint)0 + (nint)8);
					match = predicate;
				}
				if (nodes != null)
				{
					List<Node> list = nodes.FindAll(match);
					nint num2 = (nint)typeof(_003C_003Ec);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v529 @ rax_v22 (Il2CppClass<SleepyNodes.ImpactGraph+<>c>)+B8]");
					nint num3 = 0;
					bool flag2 = ((Dictionary<string, StateNode.NodeExecutionState>)num3)._buckets != null;
					Converter<Node, State_Objective> converter = (Converter<Node, State_Objective>)(object)((Dictionary<string, StateNode.NodeExecutionState>)num3)._buckets;
					if (!flag2)
					{
						Converter<Node, State_Objective> converter2 = (_003C_003Ec._003C_003E9__4_1 = delegate(Node x)
						{
							//IL_00ad: Expected I, but got O
							//IL_0012: Expected I, but got O
							//IL_0022: Expected O, but got I
							//IL_005e: Expected O, but got I
							nint num5 = (nint)typeof(State_Objective);
							if ((object)x == null)
							{
								return (State_Objective)x;
							}
							nint num6 = (nint)x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
							object obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
							nint num7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
							if (num7 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
								object obj4 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
								if (0 == (nint)typeof(State_Objective))
								{
									return (State_Objective)x;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
							State_Objective result = default(State_Objective);
							return result;
						});
						nint num4 = (nint)typeof(_003C_003Ec);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v601 @ rax_v41 (Il2CppClass<SleepyNodes.ImpactGraph+<>c>)+B8]");
						Dictionary<string, StateNode.NodeExecutionState> dictionary = (Dictionary<string, StateNode.NodeExecutionState>)((nint)0 + (nint)16);
						converter = converter2;
					}
					if (list != null)
					{
						List<State_Objective> list2 = list.ConvertAll(converter);
						bool flag3 = list2 == null;
						Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator enumerator2 = (Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator)0;
						if (!flag3)
						{
							bool flag4 = list2._size <= 0;
							enumerator2 = (Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator)0;
							if (!flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								List<State_Objective>.Enumerator enumerator3 = default(List<State_Objective>.Enumerator);
								while (enumerator3.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									if ((object)state_Objective != null)
									{
										state_Objective.SendNotification(notifID);
										continue;
									}
									throw new NullReferenceException();
								}
								enumerator3.Dispose();
								enumerator2 = (Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator)0;
							}
						}
						EventData_Notification eventData_Notification = new EventData_Notification();
						if (eventData_Notification != null)
						{
							eventData_Notification._003CNotificationID_003Ek__BackingField = notifID;
							CheckEvents(eventData_Notification);
							return;
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public void CheckEvents(EventNode.EventData evt)
	{
		//IL_0120: Expected I, but got O
		//IL_0383: Expected O, but got I
		//IL_0383: Expected O, but got I
		//IL_019f: Expected I, but got O
		List<EventNode> eventNodes = base.EventNodes;
		State_Objective state_Objective = default(State_Objective);
		if (eventNodes != null)
		{
			List<EventNode> eventNodes2 = base.EventNodes;
			if (eventNodes2._size > 0)
			{
				List<EventNode> eventNodes3 = base.EventNodes;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<EventNode>.Enumerator enumerator = default(List<EventNode>.Enumerator);
				object obj = default(object);
				object obj3 = default(object);
				string text = default(string);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if ((object)state_Objective != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ stack_20_v11 (SleepyNodes.State_Objective)+49]");
						if ((nint)0 == 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ stack_20_v11 (SleepyNodes.State_Objective)+4B]");
							if ((nint)0 == 0)
							{
								continue;
							}
						}
						if ((object)state_Objective.Objective != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ stack_20_v11 (SleepyNodes.State_Objective)+4A]");
							if ((nint)0 != 0)
							{
								continue;
							}
						}
						nint num = (nint)state_Objective;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v841 @ r8_v26 (Il2CppClass<SleepyNodes.State_Objective>)+238] (should have been resolved before IL gen)");
						if (obj != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
							object obj2 = obj3;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v947 @ rdx_v40+1B8] (should have been resolved before IL gen)");
							string message = "[EVENT] Starting: " + text;
							Debug.Log(message);
							StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
							nint num2 = (nint)state_Objective;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v972 @ r8_v30 (Il2CppClass<SleepyNodes.State_Objective>)+248] (should have been resolved before IL gen)");
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
			}
		}
		Predicate<Node> match = _003C_003Ec._003C_003E9__5_0;
		if (_003C_003Ec._003C_003E9__5_0 == null)
		{
			match = (_003C_003Ec._003C_003E9__5_0 = delegate(Node x)
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
					nint num3 = (nint)typeof(State_Objective);
					nint num4 = (nint)x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
					if (num5 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
						object obj5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
						bool flag2 = 0 == (nint)typeof(State_Objective);
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
		List<Node> list = nodes.FindAll(match);
		Converter<Node, State_Objective> converter = _003C_003Ec._003C_003E9__5_1;
		if (_003C_003Ec._003C_003E9__5_1 == null)
		{
			converter = (_003C_003Ec._003C_003E9__5_1 = delegate(Node x)
			{
				//IL_00ad: Expected I, but got O
				//IL_0012: Expected I, but got O
				//IL_0022: Expected O, but got I
				//IL_005e: Expected O, but got I
				nint num3 = (nint)typeof(State_Objective);
				if ((object)x == null)
				{
					return (State_Objective)x;
				}
				nint num4 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_Objective>)+130]");
				if (num5 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
					if (0 == (nint)typeof(State_Objective))
					{
						return (State_Objective)x;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				State_Objective result = default(State_Objective);
				return result;
			});
		}
		List<State_Objective> list2 = list.ConvertAll(converter);
		if (list2 != null && list2._size > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<State_Objective>.Enumerator enumerator2 = default(List<State_Objective>.Enumerator);
			while (enumerator2.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((object)state_Objective != null)
				{
					state_Objective.CheckEvents(evt);
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator2.Dispose();
		}
		if (CurrentState != null)
		{
			StateNode.NodeExecutionState currentState = CurrentState;
			if (currentState.ListeningToEvents)
			{
				currentState.Node.OnEvent(evt, CurrentState);
			}
		}
		Dictionary<string, StateNode.NodeExecutionState>.ValueCollection values = SideExecutionPaths.Values;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
		Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator enumerator3 = default(Dictionary<string, StateNode.NodeExecutionState>.ValueCollection.Enumerator);
		IntPtr intPtr = default(IntPtr);
		while (true)
		{
			if (enumerator3.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ stack_20_v7 (Il2CppMethodInfo)+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ stack_20_v7 (Il2CppMethodInfo)+18]");
					if ((nint)0 == 0)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ stack_20_v7 (Il2CppMethodInfo)+18]");
					((StateNode)0).OnEvent(evt, (StateNode.NodeExecutionState)(nint)intPtr);
				}
				continue;
			}
			enumerator3.Dispose();
			return;
		}
		throw new NullReferenceException();
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

	public unsafe List<MapEntity> StartImpact(ShellDefinition shell, Vector2 impactLocation)
	{
		//IL_03f8: Expected I, but got O
		//IL_006f: Expected O, but got Ref
		//IL_0452: Expected I, but got O
		//IL_0155: Expected O, but got I4
		//IL_0102: Expected O, but got I
		//IL_010b: Expected O, but got I4
		//IL_0530: Expected I, but got O
		//IL_0561: Expected O, but got I
		//IL_0229: Expected O, but got I
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_0201: Expected I, but got O
		//IL_032a: Expected O, but got I
		//IL_034a: Expected O, but got I
		ResetNodes();
		List<MapEntity> list = new List<MapEntity>();
		Func<Node, bool> predicate = _003C_003Ec._003C_003E9__7_0;
		if (_003C_003Ec._003C_003E9__7_0 == null)
		{
			Func<Node, bool> func = (_003C_003Ec._003C_003E9__7_0 = delegate(Node x)
			{
				//IL_0013: Expected I, but got O
				//IL_001b: Expected I, but got O
				//IL_002b: Expected O, but got I
				//IL_0067: Expected O, but got I
				//IL_008c: Expected O, but got I4
				bool flag7 = (object)x == null;
				Node node = null;
				Node node2;
				if (!flag7)
				{
					nint num4 = (nint)typeof(State_ImpactStart);
					nint num5 = (nint)x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_ImpactStart>)+130]");
					object obj14 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
					nint num6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.State_ImpactStart>)+130]");
					if (num6 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
						object obj15 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
						bool flag8 = 0 == (nint)typeof(State_ImpactStart);
						node2 = (Node)1;
						if (flag8)
						{
							goto IL_00d3;
						}
					}
					node2 = null;
					goto IL_00d3;
				}
				goto IL_00f5;
				IL_00d3:
				bool flag9 = (object)node2 == null;
				node = null;
				if (!flag9)
				{
					node = x;
				}
				goto IL_00f5;
				IL_00f5:
				bool flag10 = (object)node == null;
				return !flag10;
			});
			nint num = unchecked((nint)null);
			predicate = func;
		}
		IEnumerable<Node> enumerable = Enumerable.Where(nodes, predicate);
		Func<Node, State_ImpactStart> selector = _003C_003Ec._003C_003E9__7_1;
		if (_003C_003Ec._003C_003E9__7_1 == null)
		{
			Func<Node, State_ImpactStart> func2 = (_003C_003Ec._003C_003E9__7_1 = delegate(Node x)
			{
				//IL_00ad: Expected I, but got O
				//IL_0012: Expected I, but got O
				//IL_0022: Expected O, but got I
				//IL_005e: Expected O, but got I
				nint num4 = (nint)typeof(State_ImpactStart);
				if ((object)x == null)
				{
					return (State_ImpactStart)x;
				}
				nint num5 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_ImpactStart>)+130]");
				object obj14 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<SleepyNodes.State_ImpactStart>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj15 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v5+FFFFFFF8+v42 @ rcx_v2*8]");
					if (0 == (nint)typeof(State_ImpactStart))
					{
						return (State_ImpactStart)x;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				State_ImpactStart result = default(State_ImpactStart);
				return result;
			});
			nint num = unchecked((nint)null);
			selector = func2;
		}
		IEnumerable<State_ImpactStart> enumerable2 = Enumerable.Select(enumerable, selector);
		bool flag = enumerable2 == null;
		IEnumerable<Node> enumerable3 = enumerable;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			object obj = (object)(&obj2);
			List<MapEntity> list2 = null;
			object obj3 = default(object);
			State_ImpactStart state_ImpactStart = default(State_ImpactStart);
			object obj13 = default(object);
			while (true)
			{
				object obj5;
				object obj12;
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj3 == null)
					{
						break;
					}
					bool flag2 = obj2 == null;
					list2 = null;
					if (!flag2)
					{
						object obj4 = obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v368 @ r10_v9+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0142;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v368 @ r10_v9+B0]");
						obj5 = 0;
						object obj6 = 0;
						while (true)
						{
							object obj7 = obj6 + obj6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v542 @ r8_v20+v731 @ rcx_v46*8]");
							if (0 == (nint)typeof(IEnumerator<State_ImpactStart>))
							{
								break;
							}
							obj6++;
							object obj8 = obj6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v368 @ r10_v9+12E]");
							if ((nint)obj8 < 0)
							{
								continue;
							}
							goto IL_0142;
						}
						object obj9 = obj6 + obj6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v542 @ r8_v20+8+v786 @ rcx_v48*8]");
						object obj10 = (nint)0 << 4;
						object obj11 = obj10 + 312;
						obj12 = obj11 + obj4;
						goto IL_04c9;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
				IL_04c9:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v791 @ rdx_v25] (should have been resolved before IL gen)");
				StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
				if (newState != null)
				{
					if (SideExecutionPaths != null)
					{
						SideExecutionPaths.set_Item(newState.ID, newState);
						if ((object)state_ImpactStart != null)
						{
							List<MapEntity> collection = state_ImpactStart.StartImpact(newState, shell, impactLocation);
							if (list != null)
							{
								list.AddRange(collection);
								nint num = (nint)impactLocation;
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
				IL_0142:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj5 = 0;
				obj12 = obj13;
				goto IL_04c9;
			}
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			bool flag3 = SideExecutionPaths == null;
			enumerable3 = (IEnumerable<Node>)SideExecutionPaths;
			if (!flag3)
			{
				int count = SideExecutionPaths.Count;
				if (count <= 0)
				{
					goto IL_034f;
				}
				nint num2 = (nint)typeof(FireMission);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v855 @ rax_v37 (Il2CppClass<FireMission>)+B8]");
				nint num3 = 0;
				FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
				bool flag4 = (object)FireMission._003CInstance_003Ek__BackingField == null;
				enumerable3 = (IEnumerable<Node>)num3;
				if (!flag4)
				{
					bool flag5 = fireMission.RunningImpactGraphs == null;
					enumerable3 = (IEnumerable<Node>)fireMission.RunningImpactGraphs;
					if (!flag5)
					{
						if (fireMission.RunningImpactGraphs.Contains(this))
						{
							goto IL_034f;
						}
						enumerable3 = (IEnumerable<Node>)FireMission._003CInstance_003Ek__BackingField;
						if ((object)FireMission._003CInstance_003Ek__BackingField != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ rcx_v19 (System.Collections.Generic.IEnumerable`1<SleepyNodes.Node>)+88]");
							bool flag6 = (nint)0 == 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ rcx_v19 (System.Collections.Generic.IEnumerable`1<SleepyNodes.Node>)+88]");
							enumerable3 = (IEnumerable<Node>)0;
							if (!flag6)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ rcx_v19 (System.Collections.Generic.IEnumerable`1<SleepyNodes.Node>)+88]");
								((List<ImpactGraph>)0).Add(this);
								goto IL_034f;
							}
						}
					}
				}
			}
		}
		throw new NullReferenceException();
		IL_034f:
		return list;
	}

	public override void Run()
	{
	}
}
