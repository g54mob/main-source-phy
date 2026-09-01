using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;

namespace SleepyNodes;

public class OperationGraph : StateGraph
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Node, MissionNode> _003C_003E9__8_0;

		public static Func<MissionNode, bool> _003C_003E9__8_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal MissionNode _003Cget_Missions_003Eb__8_0(Node x)
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			if ((object)x != null)
			{
				nint num = (nint)typeof(MissionNode);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v2 (Il2CppClass<SleepyNodes.MissionNode>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v2 (Il2CppClass<SleepyNodes.MissionNode>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v5+FFFFFFF8+v39 @ rax_v4*8]");
					if (0 == (nint)typeof(MissionNode))
					{
						return (MissionNode)x;
					}
				}
			}
			return null;
		}

		internal bool _003Cget_Missions_003Eb__8_1(MissionNode x)
		{
			return x != null;
		}
	}

	public string OperationID;

	public string displayName;

	public string description;

	public override List<Type> NodeRestriction
	{
		get
		{
			List<Type> list = new List<Type>();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(MissionNode));
			if (list != null)
			{
				list.Add(typeFromHandle);
				return list;
			}
			return (List<Type>)(object)new NullReferenceException();
		}
	}

	public override List<Type> NodeTypeExludes => new List<Type>();

	public List<MissionNode> Missions
	{
		get
		{
			Func<Node, MissionNode> selector = _003C_003Ec._003C_003E9__8_0;
			if (_003C_003Ec._003C_003E9__8_0 == null)
			{
				selector = (_003C_003Ec._003C_003E9__8_0 = delegate(Node x)
				{
					//IL_0013: Expected I, but got O
					//IL_001b: Expected I, but got O
					//IL_002b: Expected O, but got I
					//IL_0067: Expected O, but got I
					if ((object)x != null)
					{
						nint num = (nint)typeof(MissionNode);
						nint num2 = (nint)x;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v2 (Il2CppClass<SleepyNodes.MissionNode>)+130]");
						object obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node>)+130]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v2 (Il2CppClass<SleepyNodes.MissionNode>)+130]");
						if (num3 >= 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node>)+C8]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v5+FFFFFFF8+v39 @ rax_v4*8]");
							if (0 == (nint)typeof(MissionNode))
							{
								return (MissionNode)x;
							}
						}
					}
					return (MissionNode)null;
				});
			}
			IEnumerable<MissionNode> source = Enumerable.Select(nodes, selector);
			Func<MissionNode, bool> predicate = _003C_003Ec._003C_003E9__8_1;
			if (_003C_003Ec._003C_003E9__8_1 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__8_1 = (MissionNode x) => x != null);
			}
			IEnumerable<MissionNode> source2 = Enumerable.Where(source, predicate);
			return Enumerable.ToList(source2);
		}
	}

	public OperationGraph()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A6F7]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		OperationID = "ID";
		displayName = "New Operation";
		base._002Ector();
	}
}
