using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_RandomBranch : StateNode
{
	public enum SelectionTypes
	{
		Random,
		RoundRobin,
		Weighted
	}

	[Serializable]
	public class Path
	{
		public int Weight;
	}

	public SelectionTypes SelectionType;

	public int[] To;

	private int lastIndex;

	public override void ResetNode()
	{
		//IL_000f: Expected I4, but got I8
		lastIndex = -1;
	}

	public override void OnEnter(NodeExecutionState state)
	{
		//IL_0015: Expected O, but got I4
		//IL_0065: Expected O, but got I4
		//IL_031a: Expected O, but got I4
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_035b: Expected O, but got I4
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Expected O, but got Unknown
		//IL_0125: Expected O, but got I4
		//IL_0509: Expected O, but got I4
		//IL_044c: Expected I, but got O
		//IL_0454: Expected I, but got O
		//IL_0464: Expected O, but got I
		//IL_04a0: Expected O, but got I
		//IL_06c4: Expected I4, but got I8
		//IL_04ed: Expected I4, but got O
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		base.OnEnter(state);
		bool flag = SelectionType == SelectionTypes.Random;
		NodePort nodePort = default(NodePort);
		NodePort nodePort2;
		int num;
		object message;
		if (!flag)
		{
			object obj = SelectionType - 1;
			if (flag)
			{
				int[] to = To;
				if (++lastIndex == to.Length)
				{
					lastIndex = 0;
				}
				IEnumerable<NodePort> dynamicOutputs = base.DynamicOutputs;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AE970");
				List<NodePort.PortConnection> connections = nodePort.connections;
				bool flag2 = connections._size < 0;
				bool flag3 = connections._size == 0;
				bool flag4 = !flag2;
				bool flag5 = !flag3;
				object obj2 = flag5 & flag4;
				bool flag6 = nodePort == null;
				bool flag7 = !flag6;
				object obj3 = flag7 & obj2;
				bool flag8 = obj3 == null;
				object obj4 = !flag8;
				nodePort2 = nodePort;
				num = 0;
				if (obj4 != null)
				{
					goto IL_0233;
				}
				goto IL_037a;
			}
			if ((nint)obj == 1)
			{
				int[] to2 = To;
				int[] to3 = To;
				object obj5 = 32;
				int num2 = 0;
				int num3 = 0;
				for (int num4 = 0; num4 < to2.Length; num4 = num2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ r10_v8 (System.Int32[])+v326 @ r8_v16]");
					bool flag9 = (nint)0 < (nint)0;
					int num5 = 0;
					if (!flag9)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ r10_v8 (System.Int32[])+v326 @ r8_v16]");
						num5 = 0;
					}
					num2++;
					num3 += num5;
					obj5 += 4;
				}
				if (num3 > 0)
				{
					int num6 = UnityEngine.Random.Range(0, num3);
					int[] to4 = To;
					int[] to5 = To;
					int num7 = 0;
					object obj6 = 32;
					int num8 = 0;
					int num9 = 0;
					int num10;
					while (true)
					{
						bool flag10 = num9 >= to4.Length;
						num10 = 0;
						if (flag10)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ r9_v15 (System.Int32[])+v330 @ rbx_v12]");
						bool flag11 = (nint)0 < (nint)0;
						int num11 = 0;
						if (!flag11)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ r9_v15 (System.Int32[])+v330 @ rbx_v12]");
							num11 = 0;
						}
						num7 += num11;
						if (num6 >= num7)
						{
							num8++;
							obj6 += 4;
							num9 = num8;
							continue;
						}
						num10 = num8;
						break;
					}
					lastIndex = num10;
					Node._003Cget_DynamicOutputs_003Ed__12 obj7 = new Node._003Cget_DynamicOutputs_003Ed__12(0);
					obj7._003C_003E1__state = -2;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
					int num12 = default(int);
					obj7._003C_003El__initialThreadId = num12;
					obj7._003C_003E4__this = this;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AE970");
					if (nodePort != null)
					{
						List<NodePort.PortConnection> connections2 = nodePort.connections;
						bool flag12 = connections2._size <= 0;
						nodePort2 = nodePort;
						num = 0;
						if (!flag12)
						{
							goto IL_0233;
						}
					}
					message = "Weighted path not connected";
				}
				else
				{
					message = "All weights are zero";
				}
				goto IL_0615;
			}
		}
		int[] to6 = To;
		int num13 = UnityEngine.Random.Range(0, to6.Length);
		lastIndex = num13;
		IEnumerable<NodePort> dynamicOutputs2 = base.DynamicOutputs;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AE970");
		NodePort connection;
		if (nodePort != null)
		{
			List<NodePort.PortConnection> connections3 = nodePort.connections;
			if (connections3._size > 0)
			{
				connection = nodePort.Connection;
				nodePort2 = nodePort;
				num = 0;
				goto IL_05e6;
			}
		}
		goto IL_037a;
		IL_037a:
		message = "Random path not connected";
		goto IL_0615;
		IL_05e6:
		Node node = connection._node;
		int num17;
		if ((object)connection._node != null)
		{
			nint num14 = (nint)typeof(StateNode);
			nint num15 = (nint)node;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v832 @ r8_v7 (Il2CppClass<SleepyNodes.StateNode>)+130]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v833 @ r9_v6 (Il2CppClass<SleepyNodes.Node>)+130]");
			nint num16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v832 @ r8_v7 (Il2CppClass<SleepyNodes.StateNode>)+130]");
			if (num16 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v833 @ r9_v6 (Il2CppClass<SleepyNodes.Node>)+C8]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v875 @ rax_v16+FFFFFFF8+v834 @ rax_v12*8]");
				bool flag13 = 0 == (nint)typeof(StateNode);
				num17 = 1;
				if (flag13)
				{
					goto IL_0660;
				}
			}
			num17 = num;
			goto IL_0660;
		}
		goto IL_06d3;
		IL_06d3:
		NodePort connection2 = nodePort2.Connection;
		base.OnExit(state, (StateNode)num, connection2._fieldName);
		return;
		IL_0660:
		if (num17 != 0)
		{
			num = (int)connection._node;
		}
		goto IL_06d3;
		IL_0615:
		Debug.LogError(message);
		return;
		IL_0233:
		connection = nodePort2.Connection;
		goto IL_05e6;
	}

	public override void OnExecute(NodeExecutionState state)
	{
	}

	public State_RandomBranch()
	{
		//IL_000f: Expected I4, but got I8
		lastIndex = -1;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
